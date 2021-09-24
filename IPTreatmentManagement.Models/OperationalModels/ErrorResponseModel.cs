using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.OperationalModels
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
        public int ApplicationStatusCode { get; set; }

        public Dictionary<string, string> ErrorDetails { get; set; } = new Dictionary<string, string>();
    }
}
