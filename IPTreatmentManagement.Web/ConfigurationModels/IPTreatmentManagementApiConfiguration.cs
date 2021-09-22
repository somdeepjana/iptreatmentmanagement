using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPTreatmentManagement.Web.ConfigurationModels
{
    public class IPTreatmentManagementApiConfiguration
    {
        public string BaseUrl { get; set; }

        public Uri BaseUrlUri
        {
            get
            {
                return  new Uri(BaseUrl);
            }
        }
    }
}
