using Context;
using Dapper;
using Master.Entity;
using Master.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Tokens;

namespace Master.Repository
{
    public class TrainingFormRepository:ITrainingFormRepository
    {

        private readonly DapperContext _context;
        public TrainingFormRepository(DapperContext context)
        {
            _context = context;
        }
      


        public async Task<IActionResult> TrainingForm(TrainingForm model)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("procTrainingForm", SetParameter(model), commandType: CommandType.StoredProcedure);
                    var Model = queryResult.Read<Object>();
                    var outcome = queryResult.ReadSingleOrDefault<Outcome>();
                    var outcomeId = outcome?.OutcomeId ?? 0;
                    var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
                    //var IDMail = outcome?.IDMail ?? string.Empty;
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
        public async Task<IActionResult> Get(TrainingForm model)
        {
            using (var connection = _context.CreateConnection())
            {
                //Guid? userIdValue = (Guid)user.UserId;
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("procTrainingForm", SetParameter(model), commandType: CommandType.StoredProcedure);
                    var Model = queryResult.ReadSingleOrDefault<Object>();
                    var outcome = queryResult.ReadSingleOrDefault<Outcome>();
                    var outcomeId = outcome?.OutcomeId ?? 0;
                    var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
				
					var result = new Result
                    {
                        Outcome = outcome,
                        Data = Model
                        //UserId = userIdValue
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

		public async Task<IActionResult> EmailTo(EmailConfigure model)
		{
			using (var connection = _context.CreateConnection())
			{
				//Guid? userIdValue = (Guid)user.UserId;
				try
				{
					var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
					await sqlConnection.OpenAsync();


					// Execute the stored procedure to retrieve email configuration
					//var queryResult = await connection.QueryAsync<EmailConfigure>("ProcEmailConfigure", commandType: CommandType.StoredProcedure);


					// Since it's assumed that only one EmailConfigure instance is expected,
					// you can use .FirstOrDefault() to get the first result.
					var queryResult = await connection.QueryMultipleAsync("ProcEmailConfigure", commandType: CommandType.StoredProcedure);
					var Model = queryResult.ReadSingleOrDefault<Object>();
					var outcome = queryResult.ReadSingleOrDefault<Outcome>();
					var outcomeId = outcome?.OutcomeId ?? 0;
					var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
					//var IDMail = outcome?.IDMail ?? string.Empty;
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
		public DynamicParameters SetParameter(TrainingForm user)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
            //parameters.Add("@UserId", user.UserId, DbType.Guid);
            parameters.Add("@UserId", user.UserId, DbType.Guid);
            parameters.Add("@tr_id", user.tr_id, DbType.Guid);
            parameters.Add("@tr_nature", user.tr_nature, DbType.String);
            parameters.Add("@tr_type", user.tr_type, DbType.String);
            parameters.Add("@td_dept", user.td_dept, DbType.String);
            parameters.Add("@tr_req_no", user.tr_req_no, DbType.String);
            parameters.Add("@tr_req_date", user.tr_req_date, DbType.Date);
            parameters.Add("@tr_hours", user.tr_hours, DbType.String);
            parameters.Add("@tr_days", user.tr_days, DbType.String);
            parameters.Add("@tr_action", user.tr_action, DbType.String);
            parameters.Add("@tr_remark", user.tr_remark, DbType.String);
            parameters.Add("@tr_topic_training", user.tr_topic_training, DbType.String);
            parameters.Add("@td_emp_code", user.td_emp_code, DbType.String);
           parameters.Add("@td_emp_name", user.td_emp_name, DbType.String);
           parameters.Add("@td_emp_des", user.td_emp_des, DbType.String);
           parameters.Add("@RoleId", user.roleid, DbType.String);
            parameters.Add("@tr_creadtedby", user.tr_creadtedby, DbType.String);
            parameters.Add("@tr_updatedby", user.tr_updatedby, DbType.String);
            parameters.Add("@tr_createddate", user.tr_createddate, DbType.DateTime);
            parameters.Add("@tr_updateddate", user.tr_updateddate, DbType.DateTime);
            parameters.Add("@tr_isactive", user.tr_isactive, DbType.String);

            if (user.DataTable != null && user.DataTable.Rows.Count > 0)
            {
                parameters.Add("@training", user.DataTable.AsTableValuedParameter("[dbo].[Tbl_training]"));
            }
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //parameters.Add("@IDMail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            return parameters;

           
       
      
    }
    }
}
