using Context;
using Dapper;
using Master.API.Entity;
using Master.Entity;
using Master.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Tokens;

namespace Master.Repository
{
    public class EmployeeMasterRepository:IEmployeeMasterRepository
    {
        private readonly DapperContext _context;
        public EmployeeMasterRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> EmployeeMaster(EmployeeMaster model)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("proc_EmployeeMasterM", SetParameter(model), commandType: CommandType.StoredProcedure);
                    var Model = queryResult.Read<Object>();
                    var outcome = queryResult.ReadSingleOrDefault<Outcome>();
                    var outcomeId = outcome?.OutcomeId ?? 0;
                    //var outcomeId = outcome?.OutcomeId ?? 1;
                    var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
                    var result = new Result
                    {
                        Outcome = outcome,
                        Data = Model
                    };

                    if (outcomeId == 1)
                    {
                        return new ObjectResult(result)
                        {
                            StatusCode = 200
                        };
                    }
					else if (outcomeId == 2)
					{
						return new ObjectResult(result)
						{
							StatusCode = 409
						};
					}
					else
                    {
                        return new ObjectResult(result)
                        {
                            StatusCode = 400
                        };
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        

        public async Task<IActionResult> Get(EmployeeMaster model)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("proc_EmployeeMasterM", SetParameter(model), commandType: CommandType.StoredProcedure);
                    var Model = queryResult.ReadSingleOrDefault<Object>();
                    var outcome = queryResult.ReadSingleOrDefault<Outcome>();
                    var outcomeId = outcome?.OutcomeId ?? 0;
                    var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
                    var result = new Result
                    {
                        Outcome = outcome,
                        Data = Model
                    };

                    if (outcomeId == 1)
                    {
                        return new ObjectResult(result)
                        {
                            StatusCode = 200
                        };
                    }
                    else
                    {
                        return new ObjectResult(result)
                        {
                            StatusCode = 400
                        };
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        //public async Task<IActionResult> GetEmp(EmployeeMaster model)
        //{
        //    using (IDbConnection connection = _context.CreateConnection(model.BaseModel.Server_Value))
        //    {
        //        try
        //        {
        //            var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
        //            await sqlConnection.OpenAsync();
        //            var queryResult = await connection.QueryMultipleAsync("ProcGetEmployee", SetParameter1(model), commandType: CommandType.StoredProcedure);
        //            var Model = queryResult.ReadSingleOrDefault<Object>();
        //            var outcome = queryResult.ReadSingleOrDefault<Outcome>();
        //            var outcomeId = outcome?.OutcomeId ?? 0;
        //            var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
        //            var result = new Result
        //            {
        //                Outcome = outcome,
        //                Data = Model
        //            };

        //            if (outcomeId == 1)
        //            {
        //                return new ObjectResult(result)
        //                {
        //                    StatusCode = 200
        //                };
        //            }
        //            else
        //            {
        //                return new ObjectResult(result)
        //                {
        //                    StatusCode = 400
        //                };
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }
        //}


		public async Task<IActionResult> GetEmp(EmployeeMaster model)
		{
			using (var connection = _context.CreateConnection())
			{
				try
				{
					var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
					await sqlConnection.OpenAsync();
					var queryResult = await connection.QueryMultipleAsync("ProcGetEmployee", SetParameter1(model), commandType: CommandType.StoredProcedure);
					var Model = queryResult.ReadSingleOrDefault<Object>();
					var outcome = queryResult.ReadSingleOrDefault<Outcome>();
					var outcomeId = outcome?.OutcomeId ?? 0;
					var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
					var result = new Result
					{
						Outcome = outcome,
						Data = Model
					};

					if (outcomeId == 1)
					{
						return new ObjectResult(result)
						{
							StatusCode = 200
						};
					}
					else
					{
						return new ObjectResult(result)
						{
							StatusCode = 400
						};
					}
				}
				catch (Exception)
				{
					throw;
				}
			}
		}
		public DynamicParameters SetParameter(EmployeeMaster user)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
            parameters.Add("@emp_id", user.emp_id, DbType.String);
            parameters.Add("@UserId", user.UserId, DbType.String);
            parameters.Add("@emp_code", user.emp_code, DbType.String);
            parameters.Add("@emp_fname", user.emp_fname, DbType.String);
            parameters.Add("@emp_mname", user.emp_mname, DbType.String);
            parameters.Add("@emp_lname", user.emp_lname, DbType.String);
            parameters.Add("@emp_job_title", user.emp_job_title, DbType.String);
            parameters.Add("@emp_des", user.emp_des, DbType.String);
            parameters.Add("@emp_dep", user.emp_dep, DbType.String);
            parameters.Add("@emp_hod", user.emp_hod, DbType.String);
            parameters.Add("@emp_hodToEmp", user.emp_hodToEmp, DbType.String);
            parameters.Add("@emp_add1", user.emp_add1, DbType.String);
            parameters.Add("@emp_add2", user.emp_add2, DbType.String);
            parameters.Add("@emp_city", user.emp_city, DbType.String);
            parameters.Add("@emp_city_name", user.emp_city_name, DbType.String);
            parameters.Add("@emp_state", user.emp_state, DbType.String);
            parameters.Add("@emp_country", user.emp_country, DbType.String);
            parameters.Add("@emp_pincode", user.emp_pincode, DbType.String);
            parameters.Add("@emp_mob_no", user.emp_mob_no, DbType.String);
            parameters.Add("@emp_off_no", user.emp_off_no, DbType.String);
            parameters.Add("@emp_email", user.emp_email, DbType.String);
            parameters.Add("@emp_joiningDate", user.emp_joiningDate, DbType.DateTime);
            parameters.Add("@emp_isactive", user.emp_isactive, DbType.String);
            parameters.Add("@emp_createddate", user.emp_createddate, DbType.DateTime);
            parameters.Add("@emp_updateddate", user.emp_updateddate, DbType.DateTime);
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            return parameters;
        }


		public DynamicParameters SetParameter1(EmployeeMaster user)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
			parameters.Add("@emp_code", user.emp_code, DbType.String);
			parameters.Add("@emp_fname", user.emp_fname, DbType.String);
			parameters.Add("@emp_mname", user.emp_mname, DbType.String);
			parameters.Add("@emp_lname", user.emp_lname, DbType.String);
			
			parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
			parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
			return parameters;
		}
	}
}

