using Common;
using Master.Entity;
using Master.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Tokens;

namespace Master.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KPIMasterController : ControllerBase
    {
        private readonly IKPIMasterRepository _kpiRepo;
        public KPIMasterController(IKPIMasterRepository kpiRepo)
        {
            _kpiRepo = kpiRepo;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(Guid UserId, string status)
        {
            try
            {
                KPIMaster user = new KPIMaster();
                user.UserId = UserId;

                user.k_isactive = status;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAll";

                var createduser = await _kpiRepo.KPIMaster(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        



        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid? UserId, Guid k_id)
        {
            KPIMaster user = new KPIMaster();
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
            user.UserId = UserId;
            user.k_id = k_id;
            user.BaseModel.OperationType = "Get";
            try
            {
                var parameter = await _kpiRepo.Get(user);
                return parameter;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] KPIMaster user)
        {
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                if (user.k_id == null)
                {
                    user.BaseModel.OperationType = "Insert";
                }
                else
                {
                    user.BaseModel.OperationType = "Update";
                }
                var createduser = await _kpiRepo.KPIMaster(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] KPIMaster user)
        {
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
            user.BaseModel.OperationType = "Delete";
            var productDetails = await _kpiRepo.KPIMaster(user);
            return productDetails;
        }

        [HttpGet("GetExcel")]
        public async Task<IActionResult> GetExcel(Guid UserId, string status)
        {
            try
            {
                KPIMaster user = new KPIMaster();
                user.UserId = UserId;
                user.k_isactive = status;

                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAll";
                dynamic createduser = await _kpiRepo.KPIMaster(user);
                dynamic data1 = ((Tokens.Result)((Microsoft.AspNetCore.Mvc.ObjectResult)createduser).Value).Data;
                DataTable data = new DataTable();
                if (data1 is List<object> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        var firstItem = dataList[0] as IDictionary<string, object>;
                        if (firstItem != null)
                        {
                            foreach (var kvp in firstItem)
                            {
                                data.Columns.Add(kvp.Key);
                            }
                        }
                        foreach (var item in dataList)
                        {
                            var values = item as IDictionary<string, object>;
                            if (values != null)
                            {
                                var row = data.NewRow();
                                foreach (var kvp in values)
                                {
                                    row[kvp.Key] = kvp.Value;
                                }
                                data.Rows.Add(row);
                            }
                        }
                    }
                }
                ExportRepository exportRepository = new ExportRepository();
                var result = new Result
                {
                    Data = exportRepository.DataTableToJsonObj(data)
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("GetPdf")]
        public async Task<IActionResult> GetPdf(Guid? UserId, string status)
        {
            try
            {
                KPIMaster user = new KPIMaster();
                user.UserId = UserId;
                user.k_isactive = status;

                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAll";
                dynamic createdUser = await _kpiRepo.KPIMaster(user);
                dynamic data12 = ((Tokens.Result)((Microsoft.AspNetCore.Mvc.ObjectResult)createdUser).Value).Data;
                DataTable data = new DataTable();
                if (data12 is List<object> dataList)
                {
                    if (dataList.Count > 0)
                    {
                        var firstItem = dataList[0] as IDictionary<string, object>;
                        if (firstItem != null)
                        {
                            foreach (var kvp in firstItem)
                            {
                                data.Columns.Add(kvp.Key);
                            }
                        }
                        foreach (var item in dataList)
                        {
                            var values = item as IDictionary<string, object>;
                            if (values != null)
                            {
                                var row = data.NewRow();
                                foreach (var kvp in values)
                                {
                                    row[kvp.Key] = kvp.Value;
                                }
                                data.Rows.Add(row);
                            }
                        }
                    }
                }
                string htmlContent = "<div style='margin-top: 5rem; padding-left: 3rem; padding-right: 3rem; margin-bottom: 5rem; border: double;'>";
                htmlContent += "    <div style='text-align: center; line-height: 1; margin-bottom: 2rem;'>";
                htmlContent += "        <h3 style='font-weight: bold;'>Designation Master</h3>";
                htmlContent += "    </div>";
                htmlContent += "    <table style='width:100%; border-collapse: collapse; margin-top: 10px'>";
                htmlContent += "        <thead>";
                htmlContent += "            <tr>";

                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Employee Code Code</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Employee Name</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>KPI Code</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>KPI Name</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Department Name</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>UOM</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Target Date</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Is Active</th>";
                htmlContent += "            </tr>";
                htmlContent += "        </thead>";
                htmlContent += "        <tbody>";
                foreach (DataRow row in data.Rows)
                {
   

                string? k_emp_code = row["k_emp_code"].ToString();
                    string? k_emp_name = row["k_emp_name"].ToString();
                    string? k_kpi_code = row["k_kpi_code"].ToString();
                    string? k_kpi_name = row["k_kpi_name"].ToString();
                    string? k_department = row["k_department"].ToString();
                    string? k_designation = row["k_designation"].ToString();
                    string? k_targetdate = row["k_targetdate"].ToString();
               
                    string? k_uom = row["k_uom"].ToString();
                    string? k_isactive = row["k_isactive"].ToString();

                    htmlContent += "<tr style='border: 1px solid black;'>";

                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + k_emp_code + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + k_emp_name + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + k_kpi_code + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + k_kpi_name + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + k_department + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + k_designation + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + k_targetdate + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + k_uom + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + k_isactive + "</td>";
                    htmlContent += "</tr>";
                }
                htmlContent += "        </tbody>";
                htmlContent += "    </table>";
                htmlContent += "</div>";

                string date = DateTime.Now.ToString("dd-MM-yyyy--HH-mm");

                return Ok(htmlContent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
