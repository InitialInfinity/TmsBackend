
using Master.Entity;
using Microsoft.AspNetCore.Mvc;


namespace Master.Repository.Interface
{ 
    public interface IParameterValueMasterRepository
    {
        public Task<IActionResult> ParameterValue(ParameterValueMaster model);
        public Task<IActionResult> Get(ParameterValueMaster model);
    }
}
