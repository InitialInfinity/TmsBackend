using Context;
using Dapper;
using Master.Entity;
using Master.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Tokens;

namespace Master.Repository
{
    public class FeedbackRepository:IFeedbackRepository
    {
        private readonly DapperContext _context;
        public FeedbackRepository(DapperContext context)
        {
            _context = context;
        }

  

        public async Task<IActionResult> Feedback(FeedbackForm model)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("procFeedbackForm", SetParameter(model), commandType: CommandType.StoredProcedure);
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
        public async Task<IActionResult> Get(FeedbackForm model)
        {
            using (var connection = _context.CreateConnection())
            {
                //Guid? userIdValue = (Guid)user.UserId;
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("procFeedbackForm", SetParameter(model), commandType: CommandType.StoredProcedure);
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

        public DynamicParameters SetParameter(FeedbackForm user)
        {

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
           
            parameters.Add("@UserId", user.UserId, DbType.Guid);
            parameters.Add("@fb_id", user.fb_id, DbType.Guid);
            parameters.Add("@fb_name", user.fb_name, DbType.String);
            parameters.Add("@fb_no", user.fb_no, DbType.String);
            parameters.Add("@fb_empCode", user.fb_empCode, DbType.String);
            parameters.Add("@fb_trnNo", user.fb_trnNo, DbType.String);
            parameters.Add("@fb_title", user.fb_title, DbType.String);
          
            parameters.Add("@fb_date", user.fb_date, DbType.Date);
            parameters.Add("@fb_givenBy", user.fb_givenBy, DbType.String);
           
            parameters.Add("@fb_creadtedby", user.fb_creadtedby, DbType.String);
            parameters.Add("@fb_updatedby", user.fb_updatedby, DbType.String);
            parameters.Add("@fb_createddate", user.fb_createddate, DbType.DateTime);
            parameters.Add("@fb_updateddate", user.fb_updateddate, DbType.DateTime);
            parameters.Add("@fb_isactive", user.fb_isactive, DbType.String);

            if (user.DataTable != null && user.DataTable.Rows.Count > 0)
            {
                parameters.Add("@Feedback", user.DataTable.AsTableValuedParameter("[dbo].[Tbl_Feedback]"));
            }
            parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //parameters.Add("@IDMail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
            return parameters;




        }
    }
}
