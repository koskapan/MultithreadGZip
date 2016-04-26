using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultithreadGZip
{
    public class ExceptionMapper
    {
        public static string GetExceptionMessage(Exception ex, params object[] parameters)
        {
            return ex.Message;
        }
    }
}
