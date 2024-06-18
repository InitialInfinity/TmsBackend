using Master.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Master.Repository.Interface
{
    public interface ITrainingFormRepository
    {
        public Task<IActionResult> TrainingForm(TrainingForm model);
        public Task<IActionResult> Get(TrainingForm user);

		public Task<IActionResult> EmailTo(EmailConfigure user);
	}
}
