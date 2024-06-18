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
    public class KPIMasterRepository:IKPIMasterRepository
    {
        private readonly DapperContext _context;
        public KPIMasterRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> KPIMaster(KPIMaster model)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("proc_KPIMaster", SetParameter(model), commandType: CommandType.StoredProcedure);
                    var Model = queryResult.Read<Object>();
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
        public async Task<IActionResult> Get(KPIMaster model)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("proc_KPIMaster", SetParameter(model), commandType: CommandType.StoredProcedure);
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

        public DynamicParameters SetParameter(KPIMaster user)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
            parameters.Add("@k_id", user.k_id, DbType.Guid);
            parameters.Add("@UserId", user.UserId, DbType.Guid);
            parameters.Add("@k_emp_code", user.k_emp_code, DbType.String);
            parameters.Add("@k_emp_name", user.k_emp_name, DbType.String);
            parameters.Add("@k_kpi_code", user.k_kpi_code, DbType.String);
            parameters.Add("@k_kpi_code", user.k_kpi_code, DbType.String);
            parameters.Add("@k_kpi_des", user.k_kpi_des, DbType.String);
            parameters.Add("@k_department", user.k_department, DbType.String);
            parameters.Add("@k_designation", user.k_designation, DbType.String);
            parameters.Add("@k_occurance", user.k_occurance, DbType.String);
            parameters.Add("@k_uom", user.k_uom, DbType.String);
            parameters.Add("@k_targetdate", user.k_targetdate, DbType.Date);
            parameters.Add("@k_isactive", user.k_isactive, DbType.String);
            parameters.Add("@k_createddate", user.k_createddate, DbType.DateTime);
            parameters.Add("@k_updateddate", user.k_updateddate, DbType.DateTime);
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            return parameters;
        }
    }
}
