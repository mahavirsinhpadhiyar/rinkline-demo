using Abp.IO.Extensions;
using Abp.MultiTenancy;
using Abp.Reflection.Extensions;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace RinkLine.Authorization
{
    public class EmailTemplateProvider : IEmailTemplateProvider
    {
        private readonly ITenantCache _tenantCache;
        private readonly ConcurrentDictionary<string, string> _defaultTemplates;

        public EmailTemplateProvider(ITenantCache tenantCache)
        {
            _tenantCache = tenantCache;
            _defaultTemplates = new ConcurrentDictionary<string, string>();
        }

        public string GetDefaultTemplate(int? tenantId)
        {
            var tenancyKey = tenantId.HasValue ? tenantId.Value.ToString() : "host";

            return _defaultTemplates.GetOrAdd(tenancyKey, key =>
            {
                using (var stream = typeof(EmailTemplateProvider).GetAssembly().GetManifestResourceStream("BladePortBackOffice.Net.Emailing.EmailTemplates.default.html"))
                {
                    var bytes = stream.GetAllBytes();
                    var template = Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);
                    template = template.Replace("{THIS_YEAR}", DateTime.Now.Year.ToString());
                    return template.Replace("{EMAIL_LOGO_URL}", "");
                }
            });
        }

        //private string GetTenantLogoUrl(int? tenantId)
        //{
        //    if (!tenantId.HasValue)
        //    {
        //        return _webUrlService.GetServerRootAddress().EnsureEndsWith('/') + "TenantCustomization/GetTenantLogo?skin=light";
        //    }

        //    var tenant = _tenantCache.Get(tenantId.Value);
        //    return _webUrlService.GetServerRootAddress(tenant.TenancyName).EnsureEndsWith('/') + "TenantCustomization/GetTenantLogo?skin=light&tenantId=" + tenantId.Value;
        //}
    }
}
