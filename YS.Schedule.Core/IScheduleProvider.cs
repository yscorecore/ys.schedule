using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace YS.Schedule
{
    public interface IScheduleProvider
    {
        Task<IEnumerable<string>> GetAllScheduleJobs();
    }
}