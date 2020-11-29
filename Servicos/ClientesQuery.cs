using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using BoletosPDF.Context;
using MySqlConnector;

namespace BoletosPDF.Servicos
{
	public class ClientesQuery
	{
		public AppDb Db { get; }

		public ClientesQuery(AppDb db)
		{
			Db = db;
		}

		public async Task<GeraBoleto> FindOneAsync(int id)
		{
			using var cmd = Db.Connection.CreateCommand();
			cmd.CommandText = @"SELECT `IdCliente`, `Cliente`, `Pasta`,`Url` ,`caminhoFisico` FROM `GeraBoleto` WHERE `IdCliente` = @id";
			cmd.Parameters.Add(new MySqlParameter
			{
				ParameterName = "@id",
				DbType = DbType.Int32,
				Value = id,
			});
			var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
			return result.Count > 0 ? result[0] : null;
		}

		private async Task<List<GeraBoleto>> ReadAllAsync(DbDataReader reader)
		{
			var posts = new List<GeraBoleto>();
			using (reader)
			{
				while (await reader.ReadAsync())
				{
					var post = new GeraBoleto(Db)
					{
						idCliente = reader.GetInt32(0),
						Cliente = reader.GetString(1),
						Pasta = reader.GetString(2),
						Url = reader.GetString(3),
						caminhoFisico = reader.GetString(4),
					};
					posts.Add(post);
				}
			}
			return posts;
		}
	}
}