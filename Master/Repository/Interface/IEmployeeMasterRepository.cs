using Master.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Master.Repository.Interface
{
    public interface IEmployeeMasterRepository
    {
        public Task<IActionResult> EmployeeMaster(EmployeeMaster model);
        public Task<IActionResult> Get(EmployeeMaster model);
        public Task<IActionResult> GetEmp(EmployeeMaster model);
    }
}
