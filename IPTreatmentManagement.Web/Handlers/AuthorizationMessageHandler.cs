using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using IPTreatmentManagement.Models.Dtos.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace IPTreatmentManagement.Web.Handlers
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthorizationMessageHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //var jwtToken = _contextAccessor.HttpContext.Session.GetString("jwtToken");
            var jwtToken = _contextAccessor.HttpContext?.User.FindFirst("jwtToken")?.Value;

            request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
                jwtToken);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
