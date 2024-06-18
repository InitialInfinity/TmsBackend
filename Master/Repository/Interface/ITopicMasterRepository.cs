using Master.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Master.Repository.Interface
{
    public interface ITopicMasterRepository
    {
        public Task<IActionResult> TopicMaster(TopicMaster model);
        public Task<IActionResult> Get(TopicMaster user);
    }
}
