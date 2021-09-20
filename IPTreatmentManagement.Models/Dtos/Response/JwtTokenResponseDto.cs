using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Models.Dtos.Response
{
    public class JwtTokenResponseDto
    {
        public string UserName { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ExpieryTime { get; set; }
        public string JwtToken { get; set; }
    }
}
