using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData.ConfigClasses
{

    public enum Result
    {
        Ok,
        Error,
        NotFound,
        BadRequest,
        Exception,
        InvalidPassword
    }

    public class Response
    {
        public Result Result { get; set; }
        public string data { get; set; }
    }
}
