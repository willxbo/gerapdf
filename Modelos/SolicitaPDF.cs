using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoletosPDF.Modelos
{
	public class SolicitaPDF
	{
		public int idCliente { get; set; }
		public string URL { get; set; }
		public string HTML { get; set; }
		public string Arquivo { get; set; }
	}
}
