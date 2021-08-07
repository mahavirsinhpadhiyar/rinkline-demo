using RinkLine.Configuration;
using RinkLine.Url;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RinkLine.Web.Host.Url
{
    public class WebUrlService : WebUrlServiceBase, IWebUrlService
    {
        public WebUrlService(
            IAppConfigurationAccessor configurationAccessor) :
            base(configurationAccessor)
        {
        }

        public override string WebSiteRootAddressFormatKey => "App:ClientRootAddress";

        public override string ServerRootAddressFormatKey => "App:ServerRootAddress";
    }
}
