using Master.Entity;
using Microsoft.AspNetCore.Mvc;


namespace Master.Repository.Interface
{
    public interface ICityMasterRepository
    {
        public Task<IActionResult> City(CityMaster model);
        public Task<IActionResult> Get(CityMaster model);
    }
}
