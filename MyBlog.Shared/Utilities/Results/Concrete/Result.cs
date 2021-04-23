using MyBlog.Shared.Utilities.Results.Abstract;
using MyBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Shared.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public ResultStatus resultStatus { get; }

        public string Message { get; }

        public Exception Exception { get; }


        public Result(ResultStatus _resultStatus)
        {
            resultStatus = _resultStatus;
        }
        public Result(ResultStatus _resultStatus, string message)
        {
            resultStatus = _resultStatus;
            Message = message;
        }
        public Result(ResultStatus _resultStatus, string message, Exception exception)
        {
            resultStatus = _resultStatus;
            Message = message;
            Exception = exception;
        }
    }
}
