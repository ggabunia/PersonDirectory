using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonDirectory.DataAccess;

namespace PersonDirectory.HelperClasses
{
    public static class ErrorLogService
    {
        public static void LogError(Exception ex)
        {
            ErrorLog errorLog = new ErrorLog()
            {
                Message = ex.Message,
                Source = ex.Source,
                StackTrace = ex.StackTrace,
                Time = DateTime.Now,
            };
            UnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.ErrorLogsRepository.Insert(errorLog);
            unitOfWork.Save();
        }
    }
}