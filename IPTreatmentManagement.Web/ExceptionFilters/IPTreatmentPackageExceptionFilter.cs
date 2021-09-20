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
using Refit;

namespace IPTreatmentManagement.Web.ExceptionFilters
{
    public class IPTreatmentPackageExceptionFilter : IExceptionFilter
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public IPTreatmentPackageExceptionFilter(IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException error)
            {
                if(error.StatusCode== HttpStatusCode.Unauthorized)
                    return;

                //var error= context.Exception as ApiException;
                var errorContent = error.GetContentAsAsync<ErrorResponseModel>().Result;
                if (errorContent.ApplicationStatusCode == (int)ApplicationStatusCodes.IPTreatmentPackageEntityNotFound)
                {
                    var responseView = new ViewResult();
                    responseView.ViewName = "UserErrorView";
                    responseView.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
                    responseView.ViewData.Model = errorContent;
                    context.Result = responseView;
                }
            }
        }
    }
}
