using Microsoft.AspNetCore.Mvc;
using Master.Entity;

namespace Master.Repository.Interface
{
    public interface IDashboardRepository
    {
        public Task<IActionResult> Get(Dashboard model);
    }
}
