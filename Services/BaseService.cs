using Kinvo.Utilities.Util;
using System;

namespace WaiterSummoner.Services
{
    public abstract class BaseService
    {
        protected TimeSpan GetRemainingTimeToday()
        {
            var currentBrazilianDateTime = DateTimeUtil.GetSouthAmericaDateTimeNow().TimeOfDay;
            var remainingTimeToday = TimeSpan.FromHours(24) - currentBrazilianDateTime;

            return remainingTimeToday;
        }
    }
}
