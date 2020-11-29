using Microsoft.AspNetCore.Mvc;
using System;

namespace BoletosPDF.Modelos
{
	public class Retorno
	{
		public string caminhoFisico { get; set; }
		public string URL { get; set; }
		public bool Novo { get; set; }

	}
}