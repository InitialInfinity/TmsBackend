using Master.API.Entity;
using Master.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Master.Repository.Interface
{
    public interface ICompentencyMasterRepository
    {
        public Task<IActionResult> Compentency(CompentencyMaster model);
        public Task<IActionResult> Get(CompentencyMaster model);
    }
}
