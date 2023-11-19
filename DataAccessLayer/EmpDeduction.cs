using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
	public class EmpDeduction
	{
		#region Properties
		public int Id { get; set; }
		public int EmpId { get; set; }
		public int DeductionType { get; set; }
		public DateTime DeductionDate { get; set; }
		public double Amount { get; set; }
		#endregion

		#region Add
		/// <summary>
		/// Adds a new record.
		/// </summary>
		public void Add()
		{
			String sql =  @"INSERT INTO [EmpDeduction]
							(
								[Id],
								[EmpId],
								[DeductionType],
								[DeductionDate],
								[Amount]
							)
							VALUES
							(
								@Id,
								@EmpId,
								@DeductionType,
								@DeductionDate,
								@Amount
							);";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = Id;
					cmd.Parameters.Add("@EmpId", SqlDbType.Int, 4).Value = EmpId;
					cmd.Parameters.Add("@DeductionType", SqlDbType.Int, 4).Value = DeductionType;
					cmd.Parameters.Add("@DeductionDate", SqlDbType.Date, 3).Value = DeductionDate;
					cmd.Parameters.Add("@Amount", SqlDbType.Float, 8).Value = Amount;
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
			String sql =  @"UPDATE	[EmpDeduction]
							SET		[EmpId] = @EmpId,
									[DeductionType] = @DeductionType,
									[DeductionDate] = @DeductionDate,
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
					cmd.Parameters.Add("@DeductionType", SqlDbType.Int, 4).Value = DeductionType;
					cmd.Parameters.Add("@DeductionDate", SqlDbType.Date, 3).Value = DeductionDate;
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
			String sql =  @"DELETE	FROM [EmpDeduction]
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
		public static EmpDeduction Get(int id)
		{
			String sql =  @"SELECT	[Id],
									[EmpId],
									[DeductionType],
									[DeductionDate],
									[Amount]
							FROM	[EmpDeduction]
							WHERE	[Id] = @Id;";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				EmpDeduction empDeduction = new EmpDeduction();

				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = id;

					using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
					{
						if (reader.Read())
						{
							empDeduction.Id = Convert.ToInt32(reader["Id"]);
							empDeduction.EmpId = Convert.ToInt32(reader["EmpId"]);
							empDeduction.DeductionType = Convert.ToInt32(reader["DeductionType"]);
							empDeduction.DeductionDate = Convert.ToDateTime(reader["DeductionDate"]);
							empDeduction.Amount = Convert.ToDouble(reader["Amount"]);
						}
					}
				}

				return empDeduction;
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
									[DeductionType],
									[DeductionDate],
									[Amount]
							FROM	[EmpDeduction];";

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