
using Master.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Master.Repository.Interface
{
    public interface ITrainingScheduleRepository
    {
        public Task<IActionResult> TrainingSchedule(TrainingSchedule model);
        public Task<IActionResult> Get(TrainingSchedule user);
        public Task<IActionResult> GetEmail(EmailConfigure user);
    }
}
