using Master.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Master.Repository.Interface
{
    public interface IKPIMasterRepository
    {
        public Task<IActionResult>KPIMaster(KPIMaster model);
        public Task<IActionResult> Get(KPIMaster model);
    }
}
