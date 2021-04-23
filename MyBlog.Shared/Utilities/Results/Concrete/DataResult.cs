using MyBlog.Shared.Utilities.Results.Abstract;
using MyBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Shared.Utilities.Results.Concrete
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(ResultStatus _resultStatus,T data)
        {
            resultStatus = _resultStatus;
            Data = data;
        }
        public DataResult(ResultStatus _resultStatus, string message ,T data)
        {
            resultStatus = _resultStatus;
            Message = message;
            Data = data;
        }
        public DataResult(ResultStatus _resultStatus, string message, T data,Exception exception)
        {
            resultStatus = _resultStatus;
            Message = message;
            Data = data;
            Exception = exception;
        }
        public T Data { get; }

        public ResultStatus resultStatus { get; }

        public string Message { get; }

        public Exception Exception { get; }
    }
}
