using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Api.Models
{
    public enum ErrorTypes
    {
        InternalServerError,
        UserSideError
    }

    public class ErrorResponseModel
    {
        public string ErrorId { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }

        private readonly IEnumerable<string> _probabbleResolves = new List<string>();

        public IEnumerable<string> ProbabbleResolves { get { return _probabbleResolves; } }
        public void AddProbabbleResolve(string resolve)
        {
            _probabbleResolves.Append(resolve);
        }
    }
}
