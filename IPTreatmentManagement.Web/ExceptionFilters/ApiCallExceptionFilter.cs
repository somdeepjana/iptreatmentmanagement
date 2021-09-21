using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.OperationalModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Refit;

namespace IPTreatmentManagement.Web.ExceptionFilters
{
    public class ApiCallExceptionFilter : IExceptionFilter
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public ApiCallExceptionFilter(IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException error)
            {
                if (error.StatusCode == HttpStatusCode.Unauthorized)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                        new {controller = "User", action = "login"}));
                    return;
                }

                var errorContent = error.GetContentAsAsync<ErrorResponseModel>().Result;

                if (errorContent != null)
                {
                    var responseView = new ViewResult
                    {
                        ViewName = "UserErrorView",
                        ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
                        {
                            Model = errorContent
                        }
                    };
                    context.Result = responseView;
                    context.ExceptionHandled = true;
                }
            }
        }
    }
}
