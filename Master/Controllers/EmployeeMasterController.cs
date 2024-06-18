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
    public class EmployeeMasterController : ControllerBase
    {
        private readonly IEmployeeMasterRepository _empRepo;
        public EmployeeMasterController(IEmployeeMasterRepository empRepo)
        {
            _empRepo = empRepo;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll( string status)
        {
            try
            {
                EmployeeMaster user = new EmployeeMaster();
           

                user.emp_isactive = status;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAll";

                var createduser = await _empRepo.EmployeeMaster(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string emp_id)
        {
            EmployeeMaster user = new EmployeeMaster();
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
            
            user.emp_id = emp_id;
            user.BaseModel.OperationType = "Get";
            try
            {
                var parameter = await _empRepo.Get(user);
                return parameter;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("GetByCode")]
        public async Task<IActionResult> GetByCode( string emp_code)
        {
            EmployeeMaster user = new EmployeeMaster();
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
           
            user.emp_code = emp_code;
            user.BaseModel.OperationType = "GetByCode";
            try
            {
                var parameter = await _empRepo.Get(user);
                return parameter;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetByCodeAdd")]
        public async Task<IActionResult> GetByCodeAdd( string emp_code)
        {
            EmployeeMaster user = new EmployeeMaster();
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
           
            user.emp_code = emp_code;
            //user.BaseModel.Server_Value = "Server_1";
            user.BaseModel.OperationType = "GetByCodeAdd";
            try
            {
                var parameter = await _empRepo.GetEmp(user);
                return parameter;
            }
            catch (Exception)
            {
                throw;
            }
        }

      
       

               [HttpPost]
        public async Task<IActionResult> Insert([FromBody] EmployeeMaster user)
        {
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                if (user.emp_id == null)
                {
                    user.BaseModel.OperationType = "Insert";
                }
                else
                {
                    user.BaseModel.OperationType = "Update";
                }
                var createduser = await _empRepo.EmployeeMaster(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] EmployeeMaster user)
        {
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
            user.BaseModel.OperationType = "Delete";
            var productDetails = await _empRepo.EmployeeMaster(user);
            return productDetails;
        }

        [HttpGet("GetExcel")]
        public async Task<IActionResult> GetExcel( string status)
        {
            try
            {
                EmployeeMaster user = new EmployeeMaster();
                
                user.emp_isactive = status;

                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAll";
                dynamic createduser = await _empRepo.EmployeeMaster(user);
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
        public async Task<IActionResult> GetPdf(string status)
        {
            try
            {
                EmployeeMaster user = new EmployeeMaster();
               
                user.emp_isactive = status;

                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAll";
                dynamic createdUser = await _empRepo.EmployeeMaster(user);
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
                htmlContent += "        <h3 style='font-weight: bold;'>Employee Master</h3>";
                htmlContent += "    </div>";
                htmlContent += "    <table style='width:100%; border-collapse: collapse; margin-top: 10px'>";
                htmlContent += "        <thead>";
                htmlContent += "            <tr>";

                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Employee Code Code</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'> First Name</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Middle Name</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Last Name</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Job Title</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Designation</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Department</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>HOD</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>HOD To Employee</th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Address1 </th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Address2 </th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>City </th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>State </th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Country </th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Pincode </th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Mobile No </th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Office No </th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Email Id </th>";
                htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Is Active</th>";
                htmlContent += "            </tr>";
                htmlContent += "        </thead>";
                htmlContent += "        <tbody>";
                foreach (DataRow row in data.Rows)
                {


                    string? emp_code = row["emp_code"].ToString();
                    string? emp_fname = row["emp_fname"].ToString();
                    string? emp_mname = row["emp_mname"].ToString();
                    string? emp_lname = row["emp_lname"].ToString();
                    string? emp_job_title = row["emp_job_title"].ToString();
                    string? emp_des = row["emp_des"].ToString();
                    string? emp_dep = row["emp_dep"].ToString();
                    string? emp_hod = row["emp_hod"].ToString();
                    string? emp_hodToEmp = row["emp_hodToEmp"].ToString();
                    string? emp_add1 = row["emp_add1"].ToString();
                    string? emp_add2 = row["emp_add2"].ToString();
                    string? emp_city = row["emp_city"].ToString();
                    string? emp_state = row["emp_state"].ToString();
                    string? emp_country = row["emp_country"].ToString();
                    string? emp_pincode = row["emp_pincode"].ToString();
                    string? emp_mob_no = row["emp_mob_no"].ToString();
                    string? emp_off_no = row["emp_off_no"].ToString();
                    string? emp_email = row["emp_email"].ToString();
                    string? emp_isactive = row["emp_isactive"].ToString();



                     
      
       
     
       

        htmlContent += "<tr style='border: 1px solid black;'>";

                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_code + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_fname + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_mname + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_lname + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_job_title + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_des + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_dep + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_hod + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_hodToEmp + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_add1 + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_add2 + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_city + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_state + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_country + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_pincode + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_mob_no + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_off_no + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_email + "</td>";
                    htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + emp_isactive + "</td>";
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
