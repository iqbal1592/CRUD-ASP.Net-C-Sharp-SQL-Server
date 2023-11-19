using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
	public class Employee
	{
		#region Properties
		public int Id { get; set; }
		public String FirstName { get; set; }
		public String MiddleName { get; set; }
		public String LastName { get; set; }
		public String EmailAddress { get; set; }
		public bool IsActive { get; set; }
		public int EmpTypeId { get; set; }
		public int? DesignationId { get; set; }
		public int? Country { get; set; }
		public String Address { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public String Gender { get; set; }
		public String PassportNo { get; set; }
		#endregion

		#region Add
		/// <summary>
		/// Adds a new record.
		/// </summary>
		public void Add()
		{
			String sql =  @"INSERT INTO [Employee]
							(
								[FirstName],
								[MiddleName],
								[LastName],
								[EmailAddress],
								[IsActive],
								[EmpTypeId],
								[DesignationId],
								[Country],
								[Address],
								[DateOfBirth],
								[Gender],
								[PassportNo]
							)
							VALUES
							(
								@FirstName,
								@MiddleName,
								@LastName,
								@EmailAddress,
								@IsActive,
								@EmpTypeId,
								@DesignationId,
								@Country,
								@Address,
								@DateOfBirth,
								@Gender,
								@PassportNo
							);";

			sql += "SELECT SCOPE_IDENTITY();";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = FirstName;
					cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar, 50).Value = MiddleName == null ? (Object)DBNull.Value : MiddleName;
					cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = LastName;
					cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 100).Value = EmailAddress == null ? (Object)DBNull.Value : EmailAddress;
					cmd.Parameters.Add("@IsActive", SqlDbType.Bit, 1).Value = IsActive;
					cmd.Parameters.Add("@EmpTypeId", SqlDbType.Int, 4).Value = EmpTypeId;
					cmd.Parameters.Add("@DesignationId", SqlDbType.Int, 4).Value = DesignationId == null ? (Object)DBNull.Value : DesignationId;
					cmd.Parameters.Add("@Country", SqlDbType.Int, 4).Value = Country == null ? (Object)DBNull.Value : Country;
					cmd.Parameters.Add("@Address", SqlDbType.VarChar, 200).Value = Address == null ? (Object)DBNull.Value : Address;
					cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date, 3).Value = DateOfBirth == null ? (Object)DBNull.Value : DateOfBirth;
					cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 10).Value = Gender == null ? (Object)DBNull.Value : Gender;
					cmd.Parameters.Add("@PassportNo", SqlDbType.VarChar, 50).Value = PassportNo == null ? (Object)DBNull.Value : PassportNo;

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
			String sql =  @"UPDATE	[Employee]
							SET		[FirstName] = @FirstName,
									[MiddleName] = @MiddleName,
									[LastName] = @LastName,
									[EmailAddress] = @EmailAddress,
									[IsActive] = @IsActive,
									[EmpTypeId] = @EmpTypeId,
									[DesignationId] = @DesignationId,
									[Country] = @Country,
									[Address] = @Address,
									[DateOfBirth] = @DateOfBirth,
									[Gender] = @Gender,
									[PassportNo] = @PassportNo
							WHERE	[Id] = @Id;";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = Id;
					cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = FirstName;
					cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar, 50).Value = MiddleName == null ? (Object)DBNull.Value : MiddleName;
					cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = LastName;
					cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 100).Value = EmailAddress == null ? (Object)DBNull.Value : EmailAddress;
					cmd.Parameters.Add("@IsActive", SqlDbType.Bit, 1).Value = IsActive;
					cmd.Parameters.Add("@EmpTypeId", SqlDbType.Int, 4).Value = EmpTypeId;
					cmd.Parameters.Add("@DesignationId", SqlDbType.Int, 4).Value = DesignationId == null ? (Object)DBNull.Value : DesignationId;
					cmd.Parameters.Add("@Country", SqlDbType.Int, 4).Value = Country == null ? (Object)DBNull.Value : Country;
					cmd.Parameters.Add("@Address", SqlDbType.VarChar, 200).Value = Address == null ? (Object)DBNull.Value : Address;
					cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date, 3).Value = DateOfBirth == null ? (Object)DBNull.Value : DateOfBirth;
					cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 10).Value = Gender == null ? (Object)DBNull.Value : Gender;
					cmd.Parameters.Add("@PassportNo", SqlDbType.VarChar, 50).Value = PassportNo == null ? (Object)DBNull.Value : PassportNo;
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
			String sql =  @"DELETE	FROM [Employee]
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
		public static Employee Get(int id)
		{
			String sql =  @"SELECT	[Id],
									[FirstName],
									[MiddleName],
									[LastName],
									[EmailAddress],
									[IsActive],
									[EmpTypeId],
									[DesignationId],
									[Country],
									[Address],
									[DateOfBirth],
									[Gender],
									[PassportNo]
							FROM	[Employee]
							WHERE	[Id] = @Id;";

			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				Employee employee = new Employee();

				con.Open();

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 4).Value = id;

					using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
					{
						if (reader.Read())
						{
							employee.Id = Convert.ToInt32(reader["Id"]);
							employee.FirstName = reader["FirstName"].ToString();
							employee.MiddleName = reader["MiddleName"] == DBNull.Value ? null : reader["MiddleName"].ToString();
							employee.LastName = reader["LastName"].ToString();
							employee.EmailAddress = reader["EmailAddress"] == DBNull.Value ? null : reader["EmailAddress"].ToString();
							employee.IsActive = Convert.ToBoolean(reader["IsActive"]);
							employee.EmpTypeId = Convert.ToInt32(reader["EmpTypeId"]);
							employee.DesignationId = reader["DesignationId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["DesignationId"]);
							employee.Country = reader["Country"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["Country"]);
							employee.Address = reader["Address"] == DBNull.Value ? null : reader["Address"].ToString();
							employee.DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DateOfBirth"]);
							employee.Gender = reader["Gender"] == DBNull.Value ? null : reader["Gender"].ToString();
							employee.PassportNo = reader["PassportNo"] == DBNull.Value ? null : reader["PassportNo"].ToString();
						}
					}
				}

				return employee;
			}
		}
		#endregion

		#region Get All
		/// <summary>
		/// Gets all records.
		/// </summary>
		public static DataTable GetAll(int pageNo, int pageSize, out int totalRecordsCount, String sortColumn, String sortOrder, String firstName, String lastName)
		{
			String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

			using (SqlConnection con = new SqlConnection(connectionString))
			{
				DataTable dataTable = new DataTable();

				con.Open();

				String sql =  @"SELECT	COUNT(*)
								FROM	[Employee]
								WHERE	(@FirstName IS NULL OR [FirstName] LIKE @FirstName)
								AND		(@LastName IS NULL OR [LastName] LIKE @LastName);";

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = firstName == null ? (Object)DBNull.Value : "%" + firstName + "%";
					cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastName == null ? (Object)DBNull.Value : "%" + lastName + "%";

					totalRecordsCount = Convert.ToInt32(cmd.ExecuteScalar());
				}

				int pagesCount = (int)Math.Ceiling((double)totalRecordsCount / pageSize);
				pageNo = pageNo > pagesCount ? pagesCount : pageNo;
				int fromRecord = pageSize * (pageNo - 1) + 1;
				int toRecord = ((pageNo - 1) * pageSize) + pageSize;

				sql = @"SELECT	TBL.[Id],
								TBL.[FirstName],
								TBL.[LastName],
								TBL.[EmailAddress],
								TBL.[IsActive],
								TBL.[Gender]
						FROM (
							SELECT	[Id],
									[FirstName],
									[LastName],
									[EmailAddress],
									[IsActive],
									[Gender],
									ROW_NUMBER() OVER(ORDER BY
										-- SORT ASCENDING
										CASE WHEN @SortColumn = N'Id' AND @SortOrder = 'ASC' THEN [Id] END ASC,
										CASE WHEN @SortColumn = N'FirstName' AND @SortOrder = 'ASC' THEN [FirstName] END ASC,
										CASE WHEN @SortColumn = N'LastName' AND @SortOrder = 'ASC' THEN [LastName] END ASC,
										CASE WHEN @SortColumn = N'EmailAddress' AND @SortOrder = 'ASC' THEN [EmailAddress] END ASC,
										CASE WHEN @SortColumn = N'IsActive' AND @SortOrder = 'ASC' THEN [IsActive] END ASC,
										CASE WHEN @SortColumn = N'Gender' AND @SortOrder = 'ASC' THEN [Gender] END ASC,

										-- SORT DESCENDING
										CASE WHEN @SortColumn = N'Id' AND @SortOrder = 'DESC' THEN [Id] END DESC,
										CASE WHEN @SortColumn = N'FirstName' AND @SortOrder = 'DESC' THEN [FirstName] END DESC,
										CASE WHEN @SortColumn = N'LastName' AND @SortOrder = 'DESC' THEN [LastName] END DESC,
										CASE WHEN @SortColumn = N'EmailAddress' AND @SortOrder = 'DESC' THEN [EmailAddress] END DESC,
										CASE WHEN @SortColumn = N'IsActive' AND @SortOrder = 'DESC' THEN [IsActive] END DESC,
										CASE WHEN @SortColumn = N'Gender' AND @SortOrder = 'DESC' THEN [Gender] END DESC
									) AS RowNum
							FROM	[Employee]
							WHERE	(@FirstName IS NULL OR [FirstName] LIKE @FirstName)
							AND		(@LastName IS NULL OR [LastName] LIKE @LastName)) AS TBL
						WHERE TBL.RowNum BETWEEN @FromRecord AND @ToRecord
						ORDER BY RowNum;";

				using (SqlCommand cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = firstName == null ? (Object)DBNull.Value : "%" + firstName + "%";
					cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastName == null ? (Object)DBNull.Value : "%" + lastName + "%";
					cmd.Parameters.Add("@SortColumn", SqlDbType.NVarChar, 12).Value = sortColumn;
					cmd.Parameters.Add("@SortOrder", SqlDbType.VarChar, 4).Value = sortOrder;
					cmd.Parameters.Add("@FromRecord", SqlDbType.Int, 4).Value = fromRecord;
					cmd.Parameters.Add("@ToRecord", SqlDbType.Int, 4).Value = toRecord;

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