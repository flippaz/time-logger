using System.Threading.Tasks;
using TimeLogger.Models;

namespace TimeLogger.Clients
{
    public interface IPublicHolidayClient
    {
        Task<PublicHolidayResponse> GetHolidays();
    }
}