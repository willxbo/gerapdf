using BoletosPDF.Context;
using MySqlConnector;
using System.Data;

namespace BoletosPDF
{
	public class GeraBoleto
	{
		public int idCliente { get; set; }

		public string Cliente { get; set; }
		public string Pasta { get; set; }
		public string Url { get; set; }
		public string caminhoFisico { get; set; }
		internal AppDb Db { get; set; }

		public GeraBoleto()
		{
		}

		internal GeraBoleto(AppDb db)
		{
			Db = db;
		}

		private void BindId(MySqlCommand cmd)
		{
			cmd.Parameters.Add(new MySqlParameter
			{
				ParameterName = "@id",
				DbType = DbType.Int32,
				Value = idCliente,
			});
		}

		private void BindParams(MySqlCommand cmd)
		{
			cmd.Parameters.Add(new MySqlParameter
			{
				ParameterName = "@cliente",
				DbType = DbType.String,
				Value = Cliente,
			});
			cmd.Parameters.Add(new MySqlParameter
			{
				ParameterName = "@pasta",
				DbType = DbType.String,
				Value = Pasta,
			});
		}
	}
}