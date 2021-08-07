using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyBlog.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Filters
{
    public class MvcExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _envoriment;
        private readonly IModelMetadataProvider _metadataprovider;
        private readonly ILogger _logger;

        public MvcExceptionFilter(IHostEnvironment envoriment, IModelMetadataProvider metadataprovider, ILogger<MvcExceptionFilter> logger)
        {
            _envoriment = envoriment;
            _metadataprovider = metadataprovider;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (_envoriment.IsProduction())
            {
                context.ExceptionHandled = true;
                var mvcErrorModel = new MvcErrorModel();
                switch (context.Exception)
                {
                    case SqlNullValueException:
                        mvcErrorModel.Message = "Üzgünüz, işleminiz sırasında beklenmedik bir veritabanı hatası oluştu. Sorunu en kısa zamanda çözeceğiz.";
                        mvcErrorModel.Detail = context.Exception.Message;
                        _logger.LogError(context.Exception, context.Exception.Message);
                        break;
                    case NullReferenceException:
                        mvcErrorModel.Message = "Üzgünüz, işleminiz sırasında beklenmedik null veriye rastlandı. Sorunu en kısa zamanda çözeceğiz.";
                        mvcErrorModel.Detail = context.Exception.Message;
                        _logger.LogError(context.Exception, context.Exception.Message);
                        break;
                    default:
                        mvcErrorModel.Message = "Üzgünüz, işleminiz sırasında bir hata oluştu. Sorunu en kısa zamanda çözeceğiz.";
                        _logger.LogError(context.Exception, context.Exception.Message);
                        break;
                }
                var result = new ViewResult { ViewName = "Error" };
                result.ViewData = new ViewDataDictionary(_metadataprovider, context.ModelState);
                result.ViewData.Add("MvcErrorModel", mvcErrorModel);
                context.Result = result;
            }
        }
    }
}
