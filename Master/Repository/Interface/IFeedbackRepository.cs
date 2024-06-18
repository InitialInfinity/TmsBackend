using Master.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Master.Repository.Interface
{
    public interface IFeedbackRepository
    {
        public Task<IActionResult> Feedback(FeedbackForm model);
        public Task<IActionResult> Get(FeedbackForm user);
    }
}
