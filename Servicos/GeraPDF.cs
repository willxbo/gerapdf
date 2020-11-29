using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BoletosPDF.Servicos
{
	public class GeraPDF : ControllerBase
	{
		private static IWebHostEnvironment _env;

		public GeraPDF(IWebHostEnvironment environment)
		{
			_env = environment;
		}

		public  static string retornaHTML(string endereco)
		{
			using (WebClient client = new WebClient())
			{
				client.Headers.Add(HttpRequestHeader.UserAgent,"Mozilla/5.0 (compatible; http://example.org/)");
				string html =  client.DownloadString(endereco);
				return html;
			}
		}

		public static string RetornaPDF(string endereco, string arquivo, string pasta, string strCaminho, string html)
		{
			//try
			//{
			if(html == ""){
				html = retornaHTML(endereco);
			}
			string baseUrl = endereco;
				bool nullRoot = true;
				string retorno = "";
				var caminhoFisico = @strCaminho;
				var caminho = "";
				if (nullRoot)
				{
					caminho = Path.Combine(caminhoFisico, pasta);
				}

				if (!Directory.Exists(caminho))
				{
					System.IO.Directory.CreateDirectory(caminho);
				}
				bool existeArquivo = System.IO.File.Exists(caminho + "\\" + arquivo);
				if (!existeArquivo)
				{
					//PdfDocument pdf = PdfGenerator.GeneratePdf(html, PageSize.A4);
					//pdf.Save(caminho + arquivo);
					SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
					SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html, baseUrl);
					doc.Save(caminho + "\\" + arquivo);
					doc.Close();
					retorno = "pdfGerado";

				}
				else
				{
					retorno = "pdfExistente";

				}
				return retorno;
			//}
			//catch (Exception)
			//{
//
//	return "Erro";
//}
//
		}
	}
}