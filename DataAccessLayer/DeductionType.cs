using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
	public class DeductionType
	{
		#region Properties
		public int Id { get; set; }
		public String Name { get; set; }
		#endregion

		#region Add
		/// <summary>
		/// Adds a new record.
		/// </summary>
		public void Add()
		{
			String sql =  @"INSERT INTO [DeductionType]
							(
								[Id],
								[Name]
							)
							VALUES
							(
								@Id,
								@Name
							);";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = Id;
					cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = Name;
					cmd.ExecuteNonQuery();
				}

				con.Close();
			}
		}
		#endregion

		#region Update
		/// <summary>
		/// Updates an existing record.
		/// </summary>
		public void Update()
		{
			String sql =  @"UPDATE	[DeductionType]
							SET		[Name] = @Name
							WHERE	[Id] = @Id;";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = Id;
					cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = Name;
					cmd.ExecuteNonQuery();
				}

				con.Close();
			}
		}
		#endregion

		#region Delete
		/// <summary>
		/// Deletes an existing record.
		/// </summary>
		public static void Delete(int id)
		{
			String sql =  @"DELETE	FROM [DeductionType]
							WHERE	[Id] = @Id;";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = id;
					cmd.ExecuteNonQuery();
				}

				con.Close();
			}
		}
		#endregion

		#region Get
		/// <summary>
		/// Gets an existing record.
		/// </summary>
		public static DeductionType Get(int id)
		{
			String sql =  @"SELECT	[Id],
									[Name]
							FROM	[DeductionType]
							WHERE	[Id] = @Id;";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				DeductionType deductionType = new DeductionType();

				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = id;

					using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
					{
						if (reader.Read())
						{
							deductionType.Id = Convert.ToInt32(reader["Id"]);
							deductionType.Name = reader["Name"].ToString();
						}
					}
				}

				return deductionType;
			}
		}
		#endregion

		#region Get All
		/// <summary>
		/// Gets all records.
		/// </summary>
		public static DataTable GetAll()
		{
			String sql =  @"SELECT	[Id],
									[Name]
							FROM	[DeductionType];";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				DataTable dataTable = new DataTable();

				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
					{
						dataTable.Load(reader);
					}
				}

				return dataTable;
			}
		}
		#endregion
	}
}