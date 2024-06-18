using Common;
using Master.Entity;
using Master.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection.Metadata;
using Tokens;

namespace Master.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompentencyChartReportController : ControllerBase
    {
        private readonly ICompentencyChartReportRepository _compRepo;

        public CompentencyChartReportController(ICompentencyChartReportRepository compRepo)
        {
            _compRepo = compRepo;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                CompentencyChart user = new CompentencyChart();
               
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAll";
                var createduser = await _compRepo.Compentency(user);
                var data = ((Microsoft.AspNetCore.Mvc.ObjectResult)createduser).Value;
                return Ok(data);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("GetPdf")]
        public async Task<IActionResult> GetPdf()
        {

            var document = new Document();

            CompentencyChart user = new CompentencyChart();
            

            string base64Pdf = null;
            // Fetch data from the repository
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }
           
            user.BaseModel.OperationType = "GetAll";

          
            dynamic createduser = await _compRepo.Compentency(user);
            dynamic data12 = ((Tokens.Result)((Microsoft.AspNetCore.Mvc.ObjectResult)createduser).Value).Data;

            DataTable data = new DataTable();

            if (data12 is List<object> dataList)
            {


                if (dataList.Count > 0)
                {
                    // Assuming the objects in the list have the same structure, use the first object to create columns
                    var firstItem = dataList[0] as IDictionary<string, object>;
                    if (firstItem != null)
                    {
                        foreach (var kvp in firstItem)
                        {
                            data.Columns.Add(kvp.Key);
                        }
                    }

                    // Populate the DataTable with data from the list
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
                ExportRepository exportRepository = new ExportRepository();
                // Generate the Excel file
                byte[] excelfile = exportRepository.ExportToPdfAsync(data);

                // Convert the Excel file to Base64
                if (excelfile != null)
                {
                    base64Pdf = Convert.ToBase64String(excelfile);
                }
            }



           

            string htmlContent = "<div style='margin-top: 5rem; padding-left: 3rem; padding-right: 3rem; margin-bottom: 5rem; '>";
            htmlContent += "    <div style='text-align: center; line-height: 1; margin-bottom: 2rem;'>";
            htmlContent += "        <h3 style='font-weight: bold;'>Customer Details</h3>";
            htmlContent += "    </div>";
            htmlContent += "    <div class='table-responsive'>";
            htmlContent += "    <div class='table-container' style='max-width: 100%; overflow-x: auto;'>";
            htmlContent += "        <table class='responsive-table' style='width: 100%;'>";
            htmlContent += "            <thead>";
            htmlContent += "            <tr>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 30%;'>Sr.No</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 20%;'>Training need assessment date</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Date of training</th>";
            //htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;'>Alternate Contact</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Training start</th>";

            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 20%;'>Training end</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Training hours</th>";

            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Awareness, CH, Annex, WI & FT WI/</br>SOP, DPR/FM & SAP, Special Training</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Issue No</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Rev No.</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Rev Date</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Other</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Training Topic</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Training Given By</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Emp Code</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Name of Employees</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Designation</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Department</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Number Question in Training Evaluation</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Marks Obtained in Training Evaluation</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Result</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Hard Copy Location</th>";
            htmlContent += "                <th style='border: 1px solid black; background-color: gray; color: white; padding: 8px;width: 10%;'>Soft Copy Location</th>";
            
            htmlContent += "            </tr>";
            htmlContent += "        </thead>";
            htmlContent += "        <tbody style='max-height: 300px; overflow-y: auto; display: block;'>";






            foreach (DataRow row in data.Rows)
            {
                string DateofTrainning = row["DateofTrainning"].ToString();
                string StartTime = row["StartTime"].ToString();
                string EndTime = row["EndTime"].ToString();
                string TrainingTopic = row["TrainingTopic"].ToString();
                string EmpCode = row["EmpCode"].ToString();
                string NameOfEmp = row["NameOfEmp"].ToString();
                string Designation = row["Designation"].ToString();
                string Department = row["Department"].ToString();
                string NoOfQues = row["NoOfQues"].ToString();
                string Marks = row["Marks"].ToString();
                string Result = row["Result"].ToString();
              




                htmlContent += "<tr style='border: 0px solid black;'>";
         
                htmlContent += "    <td style='border: 1px solid black; padding: 8px; white-space: normal;'>" + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px; white-space: normal;'>"  + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px; white-space: normal;'>"  + DateofTrainning + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>" + StartTime + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>" + EndTime + "</td>";
                //htmlContent += "    <td style='border: 1px solid black; padding: 8px;'>" + alternatecontact + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + "</td>";

                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>" + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>" + "</td>";

                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>" + TrainingTopic + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + EmpCode + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + NameOfEmp + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + Designation + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + Department + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + NoOfQues + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + Marks + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + Result + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + "</td>";
                htmlContent += "    <td style='border: 1px solid black; padding: 8px;white-space: normal;'>"  + "</td>";
                htmlContent += "</tr>";
            }


            htmlContent += "        </tbody>";

            htmlContent += "    </table>";
            htmlContent += "</div>";
            htmlContent += "</div>";
            htmlContent += "</div>";

            string date = DateTime.Now.ToString("dd-MM-yyyy--HH-mm");

            return Ok(htmlContent);

        }
    }
}
