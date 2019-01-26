using System;

namespace KinguilaAppApi.Services
{
    public interface IDateProvider
    {
        DateTimeOffset GetCurrentDate();
    }
}