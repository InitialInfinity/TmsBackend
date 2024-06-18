using Master.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Master.Repository.Interface
{
    public interface IApproveStagesRepositorycs
    {

        public Task<IActionResult> ApproveStages(ApproveStages model);
        public Task<IActionResult> Get(ApproveStages model);
    }
}
