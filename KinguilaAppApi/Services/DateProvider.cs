using System;

namespace KinguilaAppApi.Services
{
    public class DateProvider : IDateProvider
    {
        public DateTimeOffset GetCurrentDate()
        {
            return DateTimeOffset.Now;
        }

        public static DateProvider Default()
        {
            return new DateProvider();
        }
        
        private DateProvider() {}
    }
}