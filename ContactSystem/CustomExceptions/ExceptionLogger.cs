using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactSystem.CustomExceptions
{
    public class ExceptionLogger
    {
       public string  ExceptionMessage { get; set; }
        public string ExceptionStackTrack { get; set; }
        public string ControllerName { get; set; }
        public string ActionMethodName { get; set; }
        public DateTime ExceptionLogTime { get; set; }

    }
}