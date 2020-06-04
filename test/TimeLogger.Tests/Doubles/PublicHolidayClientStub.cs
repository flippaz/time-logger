using System.Threading.Tasks;
using TimeLogger.Clients;
using TimeLogger.Models;

namespace TimeLogger.Tests.Doubles
{
    public class PublicHolidayClientStub : IPublicHolidayClient
    {
        private PublicHolidayResponse _response;

        public Task<PublicHolidayResponse> GetHolidays()
        {
            return Task.FromResult(_response);
        }

        public PublicHolidayClientStub WithResponse(PublicHolidayResponse response)
        {
            _response = response;

            return this;
        }
    }
}