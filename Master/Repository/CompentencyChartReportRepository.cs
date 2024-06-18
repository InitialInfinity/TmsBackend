using Context;
using Dapper;
using Master.Entity;
using Master.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Tokens;

namespace Master.Repository
{
    public class CompentencyChartReportRepository : ICompentencyChartReportRepository
    {
        private readonly DapperContext _context;
        public CompentencyChartReportRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Compentency(CompentencyChart model)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sqlConnection = (Microsoft.Data.SqlClient.SqlConnection)connection;
                    await sqlConnection.OpenAsync();
                    var queryResult = await connection.QueryMultipleAsync("procCompentencyChart", SetParameter(model),  commandType: CommandType.StoredProcedure);
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


		public DynamicParameters SetParameter(CompentencyChart user)
		{
			DynamicParameters parameters = new DynamicParameters();
			parameters.Add("@OperationType", user.BaseModel.OperationType, DbType.String);
			
			parameters.Add("@OutcomeId", dbType: DbType.Int32, direction: ParameterDirection.Output);
			parameters.Add("@OutcomeDetail", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);
			return parameters;
		}
	}
}
