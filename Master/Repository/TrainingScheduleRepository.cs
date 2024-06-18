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
    public class TrainingScheduleRepository:ITrainingScheduleRepository
    {
        private readonly DapperContext _context;
        public TrainingScheduleRepository(DapperContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> TrainingSchedule(TrainingSchedule model)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("procTrainingScedule", SetParameter(model), commandType: CommandType.StoredProcedure);
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
        public async Task<IActionResult> Get(TrainingSchedule model)
        {
            using (var connection = _context.CreateConnection())
            {
                //Guid? userIdValue = (Guid)user.UserId;
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("procTrainingScedule", SetParameter(model), commandType: CommandType.StoredProcedure);
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


		public async Task<IActionResult> GetEmail(EmailConfigure model)
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
					var queryResult = await connection.QueryMultipleAsync("ProcEmailConfigure",  commandType: CommandType.StoredProcedure);
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
		public DynamicParameters SetParameter(TrainingSchedule user)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
            //parameters.Add("@UserId", user.UserId, DbType.Guid);
            parameters.Add("@UserId", user.UserId, DbType.Guid);
            parameters.Add("@ts_id", user.ts_id, DbType.Guid);
            parameters.Add("@ts_training_no", user.ts_training_no, DbType.String);
            parameters.Add("@ts_trainer_name", user.ts_trainer_name, DbType.String);
            parameters.Add("@ts_training_dept", user.ts_training_dept, DbType.String);
            parameters.Add("@ts_req_by", user.ts_req_by, DbType.String);
            parameters.Add("@ts_topic", user.ts_topic, DbType.String);
            parameters.Add("@ts_topic_name", user.ts_topic_name, DbType.String);
            parameters.Add("@ts_no_que", user.ts_no_que, DbType.String);
            parameters.Add("@ts_training_agency", user.ts_training_agency, DbType.String);
            parameters.Add("@ts_training_type", user.ts_training_type, DbType.String);
            parameters.Add("@ts_reoccurence", user.ts_reoccurence, DbType.String);
            parameters.Add("@ts_dt_tm_fromtraining", user.ts_dt_tm_fromtraining, DbType.DateTime);
            parameters.Add("@ts_dt_tm_totraining", user.ts_dt_tm_totraining, DbType.DateTime);
            parameters.Add("@ts_status", user.ts_status, DbType.String);
			parameters.Add("@ts_remark", user.ts_remark, DbType.String);
			parameters.Add("@RoleId", user.roleid, DbType.String);
			parameters.Add("@ts_creadtedby", user.ts_creadtedby, DbType.String);
            parameters.Add("@ts_updatedby", user.ts_updatedby, DbType.String);
            parameters.Add("@ts_createddate", user.ts_createddate, DbType.DateTime);
            parameters.Add("@ts_updateddate", user.ts_updateddate, DbType.DateTime);
            parameters.Add("@ts_isactive", user.ts_isactive, DbType.String);
            parameters.Add("@ts_action", user.ts_action, DbType.String);
            parameters.Add("@tss_tag", user.ts_tag, DbType.String);
            parameters.Add("@emp_code", user.ts_empid, DbType.String);

            if (user.DataTable != null && user.DataTable.Rows.Count > 0)
            {
                parameters.Add("@TrainingSubSchedule", user.DataTable.AsTableValuedParameter("[dbo].[tbl_TrainingSubSchedule]"));
            }
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //parameters.Add("@IDMail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            return parameters;




    }



        public DynamicParameters SetParameter2(EmailConfigure user)
        {

            DynamicParameters parameters = new DynamicParameters();
			parameters.Add("id", user.id, DbType.Guid);
			parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
			//parameters.Add("@IDMail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
			parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
			return parameters;
		}
		}
}
