using Master.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Master.Repository.Interface
{
    public interface ILoginRepository
    {
        public Task<IActionResult> Get(Login model);
    }
}
