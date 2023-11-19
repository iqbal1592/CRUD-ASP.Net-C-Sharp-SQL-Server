using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
	public class EmpSalary
	{
		#region Properties
		public int Id { get; set; }
		public int EmpId { get; set; }
		public int Month { get; set; }
		public int Year { get; set; }
		public double Amount { get; set; }
		#endregion

		#region Add
		/// <summary>
		/// Adds a new record.
		/// </summary>
		public void Add()
		{
			String sql =  @"INSERT INTO [EmpSalary]
							(
								[EmpId],
								[Month],
								[Year],
								[Amount]
							)
							VALUES
							(
								@EmpId,
								@Month,
								@Year,
								@Amount
							);";

			sql += "SELECT SCOPE_IDENTITY();";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@EmpId", SqlDbType.Int, 4).Value = EmpId;
					cmd.Parameters.Add("@Month", SqlDbType.Int, 4).Value = Month;
					cmd.Parameters.Add("@Year", SqlDbType.Int, 4).Value = Year;
					cmd.Parameters.Add("@Amount", SqlDbType.Float, 8).Value = Amount;

					// Execute the insert statement and get value of the identity column.
					Id = Convert.ToInt32(cmd.ExecuteScalar());
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
			String sql =  @"UPDATE	[EmpSalary]
							SET		[EmpId] = @EmpId,
									[Month] = @Month,
									[Year] = @Year,
									[Amount] = @Amount
							WHERE	[Id] = @Id;";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = Id;
					cmd.Parameters.Add("@EmpId", SqlDbType.Int, 4).Value = EmpId;
					cmd.Parameters.Add("@Month", SqlDbType.Int, 4).Value = Month;
					cmd.Parameters.Add("@Year", SqlDbType.Int, 4).Value = Year;
					cmd.Parameters.Add("@Amount", SqlDbType.Float, 8).Value = Amount;
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
			String sql =  @"DELETE	FROM [EmpSalary]
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
		public static EmpSalary Get(int id)
		{
			String sql =  @"SELECT	[Id],
									[EmpId],
									[Month],
									[Year],
									[Amount]
							FROM	[EmpSalary]
							WHERE	[Id] = @Id;";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				EmpSalary empSalary = new EmpSalary();

				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = id;

					using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
					{
						if (reader.Read())
						{
							empSalary.Id = Convert.ToInt32(reader["Id"]);
							empSalary.EmpId = Convert.ToInt32(reader["EmpId"]);
							empSalary.Month = Convert.ToInt32(reader["Month"]);
							empSalary.Year = Convert.ToInt32(reader["Year"]);
							empSalary.Amount = Convert.ToDouble(reader["Amount"]);
						}
					}
				}

				return empSalary;
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
									[EmpId],
									[Month],
									[Year],
									[Amount]
							FROM	[EmpSalary];";

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