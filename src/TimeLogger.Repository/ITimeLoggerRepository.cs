using System;

namespace TimeLogger.Repository
{
    public interface ITimeLoggerRepository
    {
        void InsertLogTime(DateTime logDateTime, string action);
    }
}