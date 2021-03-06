using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.OperationalModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
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
                    //context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    //    new {controller = "User", action = "login"}));
                    context.Result = new ChallengeResult(CookieAuthenticationDefaults.AuthenticationScheme);
                    return;
                }

                var errorContent = error.GetContentAsAsync<ErrorResponseModel>().Result;

                if (errorContent != null)
                {
                    if (Enum.IsDefined(typeof(ApplicationStatusCodes), errorContent.ApplicationStatusCode) &&
                        context.HttpContext.Request.Method== HttpMethods.Post)
                    {
                        if(errorContent.Message != null)
                            context.ModelState.AddModelError("", errorContent.Message);

                        foreach (var errorDetail in errorContent.ErrorDetails)
                        {
                            context.ModelState.AddModelError(errorDetail.Key, errorDetail.Value);
                        }

                        var userSideResponse = new ViewResult()
                        {
                            ViewName = context.RouteData.Values["action"].ToString(),
                            ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
                        };
                        context.Result = userSideResponse;
                        context.ExceptionHandled = true;
                        return;
                    }

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
