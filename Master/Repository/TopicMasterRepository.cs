using Context;
using Dapper;
using Master.Entity;
using Master.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Tokens;

namespace Master.Repository
{
    public class TopicMasterRepository : ITopicMasterRepository
    {
        private readonly DapperContext _context;
        public TopicMasterRepository(DapperContext context)
        {
            _context = context;
        }
		//public async Task<IActionResult> TopicMaster(TopicMaster model)
		//{
		//    using (var connection = _context.CreateConnection())
		//    {
		//        //Guid? userIdValue = (Guid)user.UserId;
		//        try
		//        {
		//            var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
		//            await sqlConnection.OpenAsync();

		//            var queryResult = await connection.QueryMultipleAsync("procTopicMaster", SetParameter(model), commandType: CommandType.StoredProcedure);
		//            var Model = queryResult.Read<Object>();
		//            var outcome = queryResult.ReadSingleOrDefault<Outcome>();
		//            var outcomeId = outcome?.OutcomeId ?? 0;
		//            var outcomeDetail = outcome?.OutcomeDetail ?? string.Empty;
		//            var result = new Result
		//            {

		//                Outcome = outcome,
		//                Data = Model
		//                //UserId = userIdValue
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









		//public async Task<IActionResult> TopicMaster(TopicMaster model)
		//{
		//    using (var connection = _context.CreateConnection())
		//    {
		//        try
		//        {
		//            var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
		//            await sqlConnection.OpenAsync();
		//            var queryResult = await connection.QueryMultipleAsync("procTopicMaster", SetParameter(model), commandType: CommandType.StoredProcedure);
		//            var Model = queryResult.Read<Object>();
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

		public async Task<IActionResult> TopicMaster(TopicMaster model)
		{
			using (var connection = _context.CreateConnection())
			{
				try
				{
					var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
					await sqlConnection.OpenAsync();
					var queryResult = await connection.QueryMultipleAsync("procTopicMaster", SetParameter(model), commandType: CommandType.StoredProcedure);
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
					if (outcomeId == 2)
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
		
		public async Task<IActionResult> Get(TopicMaster model)
        {
            using (var connection = _context.CreateConnection())
            {
                //Guid? userIdValue = (Guid)user.UserId;
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("procTopicMaster", SetParameter(model), commandType: CommandType.StoredProcedure);
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
        public DynamicParameters SetParameter(TopicMaster user)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
            parameters.Add("@UserId", user.UserId, DbType.Guid);
            parameters.Add("@t_id", user.t_id, DbType.Guid);
            parameters.Add("@t_code", user.t_code, DbType.String);
            parameters.Add("@t_description", user.t_description, DbType.String);
            parameters.Add("@t_department", user.t_department, DbType.String);
            parameters.Add("@t_trainingtype", user.t_trainingtype, DbType.String);
            parameters.Add("@t_duration", user.t_duration, DbType.String);
            parameters.Add("@t_creadtedby", user.t_creadtedby, DbType.String);
            parameters.Add("@t_updatedby", user.t_updatedby, DbType.String);
            parameters.Add("@t_createddate", user.t_createddate, DbType.DateTime);
            parameters.Add("@t_updateddate", user.t_updateddate, DbType.DateTime);
            parameters.Add("@t_isactive", user.t_isactive, DbType.String);

            //parameters.Add("@s_id", user.s_id, DbType.Guid);
            //parameters.Add("@s_subject", user.s_subject, DbType.String);
            //parameters.Add("@s_content", user.s_content, DbType.String);
            //parameters.Add("@s_subcontent", user.s_subcontent, DbType.String);
            //parameters.Add("@s_creadtedby", user.s_creadtedby, DbType.String);
            //parameters.Add("@s_updatedby", user.s_updatedby, DbType.String);
            //parameters.Add("@s_createddate", user.s_createddate, DbType.DateTime);
            //parameters.Add("@s_updateddate", user.s_updateddate, DbType.DateTime);
            //parameters.Add("@s_isactive", user.s_isactive, DbType.String);
            //parameters.Add("@s_t_id", user.s_t_id, DbType.String);

            if (user.DataTable != null && user.DataTable.Rows.Count > 0)
            {
                parameters.Add("@subject", user.DataTable.AsTableValuedParameter("[dbo].[Tbl_subject]"));
            }
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            return parameters;

        }
    }
}
