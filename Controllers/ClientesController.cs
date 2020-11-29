
using System.Threading.Tasks;
using BoletosPDF.Context;
using BoletosPDF.Modelos;
using BoletosPDF.Servicos;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BoletosPDF.Controllers
{
	[Route("clientes")]
	[ApiController]

	public class ClientesController : ControllerBase
	{
		public AppDb Db { get; }
		public ClientesController(AppDb db)
		{
			Db = db;
		}


		// GET api/<ClientesController>/5
		[HttpGet("{id}/{url}/{arquivo}")]
		public async Task<ActionResult<Retorno>> GetAsync(int id, string url, string arquivo)
		{
			var novaUrl = System.Net.WebUtility.UrlDecode(url);
			await Db.Connection.OpenAsync();
			var query = new ClientesQuery(Db);
			var result = await query.FindOneAsync(id);
			if (result is null)
				return new NotFoundResult();

			var pasta = result.Pasta;
			var caminhoFisico = result.caminhoFisico;
			string pdfgerado = GeraPDF.RetornaPDF(novaUrl, arquivo, pasta, caminhoFisico,"");
			if (pdfgerado == "pdfGerado" || pdfgerado == "pdfExistente")
			{
				bool pdfNovo = false;
				if (pdfgerado == "pdfGerado"){
					pdfNovo = true;
				}
				var urlDestino = result.Url + "/" + pasta + "/" + arquivo;
				var retorno = new Retorno();
				retorno.URL = urlDestino;
				retorno.caminhoFisico = caminhoFisico;
				retorno.Novo = pdfNovo;
				return retorno;
			}
			else{
				return new OkObjectResult("Erro");
			}
		}

		[HttpGet("{id}/cliente")]
		public async Task<ActionResult<GeraBoleto>> GetAsyncCliente(int id)
		{
			await Db.Connection.OpenAsync();
			var query = new ClientesQuery(Db);
			var result = await query.FindOneAsync(id);
			if (result is null)
				return new NotFoundResult();

			var retorno = new GeraBoleto();
			retorno.idCliente = result.idCliente;
			retorno.Cliente = result.Cliente;
			retorno.Pasta = result.Pasta;
			retorno.caminhoFisico = result.caminhoFisico;
			retorno.Url = result.Url;
			return retorno;

		}

	}
}
