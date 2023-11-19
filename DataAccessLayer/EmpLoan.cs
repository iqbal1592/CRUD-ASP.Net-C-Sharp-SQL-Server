using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
	public class EmpLoan
	{
		#region Properties
		public int Id { get; set; }
		public int EmpId { get; set; }
		public DateTime LoanDate { get; set; }
		public int LoanTypeId { get; set; }
		public double Amount { get; set; }
		#endregion

		#region Add
		/// <summary>
		/// Adds a new record.
		/// </summary>
		public void Add()
		{
			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				String sql =  @"INSERT INTO [EmpLoan]
								(
									[EmpId],
									[LoanDate],
									[LoanTypeId],
									[Amount]
								)
								VALUES
								(
									@EmpId,
									@LoanDate,
									@LoanTypeId,
									@Amount
								);";

				sql += "SELECT SCOPE_IDENTITY();";

				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@EmpId", SqlDbType.Int, 4).Value = EmpId;
					cmd.Parameters.Add("@LoanDate", SqlDbType.Date, 3).Value = LoanDate;
					cmd.Parameters.Add("@LoanTypeId", SqlDbType.Int, 4).Value = LoanTypeId;
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
			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				String sql =  @"UPDATE	[EmpLoan]
								SET		[EmpId] = @EmpId,
										[LoanDate] = @LoanDate,
										[LoanTypeId] = @LoanTypeId,
										[Amount] = @Amount
								WHERE	[Id] = @Id;";

				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = Id;
					cmd.Parameters.Add("@EmpId", SqlDbType.Int, 4).Value = EmpId;
					cmd.Parameters.Add("@LoanDate", SqlDbType.Date, 3).Value = LoanDate;
					cmd.Parameters.Add("@LoanTypeId", SqlDbType.Int, 4).Value = LoanTypeId;
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
			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				String sql =  @"DELETE	FROM [EmpLoan]
								WHERE	[Id] = @Id;";

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
		public static EmpLoan Get(int id)
		{
			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				String sql =  @"SELECT	[Id],
										[EmpId],
										[LoanDate],
										[LoanTypeId],
										[Amount]
								FROM	[EmpLoan]
								WHERE	[Id] = @Id;";

				EmpLoan empLoan = new EmpLoan();

				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = id;

					using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
					{
						if (reader.Read())
						{
							empLoan.Id = Convert.ToInt32(reader["Id"]);
							empLoan.EmpId = Convert.ToInt32(reader["EmpId"]);
							empLoan.LoanDate = Convert.ToDateTime(reader["LoanDate"]);
							empLoan.LoanTypeId = Convert.ToInt32(reader["LoanTypeId"]);
							empLoan.Amount = Convert.ToDouble(reader["Amount"]);
						}
					}
				}

				return empLoan;
			}
		}
		#endregion

		#region Get All
		/// <summary>
		/// Gets all records.
		/// </summary>
		public static DataTable GetAll()
		{
			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				DataTable dataTable = new DataTable();

				con.Open();

				String sql = @"SELECT	[Id],
										[EmpId],
										[LoanDate],
										[LoanTypeId],
										[Amount]
								FROM	[EmpLoan];";

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