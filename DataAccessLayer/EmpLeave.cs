using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
	public class EmpLeave
	{
		#region Properties
		public int Id { get; set; }
		public int EmpId { get; set; }
		public int LeaveTypeId { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
		#endregion

		#region Add
		/// <summary>
		/// Adds a new record.
		/// </summary>
		public void Add()
		{
			String sql =  @"INSERT INTO [EmpLeave]
							(
								[Id],
								[EmpId],
								[LeaveTypeId],
								[DateFrom],
								[DateTo]
							)
							VALUES
							(
								@Id,
								@EmpId,
								@LeaveTypeId,
								@DateFrom,
								@DateTo
							);";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = Id;
					cmd.Parameters.Add("@EmpId", SqlDbType.Int, 4).Value = EmpId;
					cmd.Parameters.Add("@LeaveTypeId", SqlDbType.Int, 4).Value = LeaveTypeId;
					cmd.Parameters.Add("@DateFrom", SqlDbType.Date, 3).Value = DateFrom;
					cmd.Parameters.Add("@DateTo", SqlDbType.Date, 3).Value = DateTo;
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
			String sql =  @"UPDATE	[EmpLeave]
							SET		[EmpId] = @EmpId,
									[LeaveTypeId] = @LeaveTypeId,
									[DateFrom] = @DateFrom,
									[DateTo] = @DateTo
							WHERE	[Id] = @Id;";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = Id;
					cmd.Parameters.Add("@EmpId", SqlDbType.Int, 4).Value = EmpId;
					cmd.Parameters.Add("@LeaveTypeId", SqlDbType.Int, 4).Value = LeaveTypeId;
					cmd.Parameters.Add("@DateFrom", SqlDbType.Date, 3).Value = DateFrom;
					cmd.Parameters.Add("@DateTo", SqlDbType.Date, 3).Value = DateTo;
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
			String sql =  @"DELETE	FROM [EmpLeave]
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
		public static EmpLeave Get(int id)
		{
			String sql =  @"SELECT	[Id],
									[EmpId],
									[LeaveTypeId],
									[DateFrom],
									[DateTo]
							FROM	[EmpLeave]
							WHERE	[Id] = @Id;";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				EmpLeave empLeave = new EmpLeave();

				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = id;

					using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
					{
						if (reader.Read())
						{
							empLeave.Id = Convert.ToInt32(reader["Id"]);
							empLeave.EmpId = Convert.ToInt32(reader["EmpId"]);
							empLeave.LeaveTypeId = Convert.ToInt32(reader["LeaveTypeId"]);
							empLeave.DateFrom = Convert.ToDateTime(reader["DateFrom"]);
							empLeave.DateTo = Convert.ToDateTime(reader["DateTo"]);
						}
					}
				}

				return empLeave;
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
									[LeaveTypeId],
									[DateFrom],
									[DateTo]
							FROM	[EmpLeave];";

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