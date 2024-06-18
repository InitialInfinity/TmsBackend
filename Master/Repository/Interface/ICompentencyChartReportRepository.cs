using Master.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Master.Repository.Interface
{
    public interface ICompentencyChartReportRepository
    {
        public Task<IActionResult> Compentency(CompentencyChart model);
    }
}
