using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
	public class EmpBonus
	{
		#region Properties
		public int Id { get; set; }
		public int EmpId { get; set; }
		public int BonusType { get; set; }
		public DateTime DateApproved { get; set; }
		public double Amount { get; set; }
		#endregion

		#region Add
		/// <summary>
		/// Adds a new record.
		/// </summary>
		public void Add()
		{
			String sql =  @"INSERT INTO [EmpBonus]
							(
								[Id],
								[EmpId],
								[BonusType],
								[DateApproved],
								[Amount]
							)
							VALUES
							(
								@Id,
								@EmpId,
								@BonusType,
								@DateApproved,
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
					cmd.Parameters.Add("@BonusType", SqlDbType.Int, 4).Value = BonusType;
					cmd.Parameters.Add("@DateApproved", SqlDbType.Date, 3).Value = DateApproved;
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
			String sql =  @"UPDATE	[EmpBonus]
							SET		[EmpId] = @EmpId,
									[BonusType] = @BonusType,
									[DateApproved] = @DateApproved,
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
					cmd.Parameters.Add("@BonusType", SqlDbType.Int, 4).Value = BonusType;
					cmd.Parameters.Add("@DateApproved", SqlDbType.Date, 3).Value = DateApproved;
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
			String sql =  @"DELETE	FROM [EmpBonus]
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
		public static EmpBonus Get(int id)
		{
			String sql =  @"SELECT	[Id],
									[EmpId],
									[BonusType],
									[DateApproved],
									[Amount]
							FROM	[EmpBonus]
							WHERE	[Id] = @Id;";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				EmpBonus empBonus = new EmpBonus();

				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = id;

					using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
					{
						if (reader.Read())
						{
							empBonus.Id = Convert.ToInt32(reader["Id"]);
							empBonus.EmpId = Convert.ToInt32(reader["EmpId"]);
							empBonus.BonusType = Convert.ToInt32(reader["BonusType"]);
							empBonus.DateApproved = Convert.ToDateTime(reader["DateApproved"]);
							empBonus.Amount = Convert.ToDouble(reader["Amount"]);
						}
					}
				}

				return empBonus;
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
									[BonusType],
									[DateApproved],
									[Amount]
							FROM	[EmpBonus];";

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