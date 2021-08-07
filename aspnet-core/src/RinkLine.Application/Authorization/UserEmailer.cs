using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Net.Mail;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using RinkLine.Authorization.Users;
using RinkLine.Editions;
using RinkLine.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RinkLine.Authorization
{
    public class UserEmailer : IUserEmailer
    {
        private readonly IEmailSender _emailSender;
        private readonly IRepository<Tenant> _tenantRepository;
        private readonly ICurrentUnitOfWorkProvider _unitOfWorkProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        //private readonly ISettingManager _settingManager;
        //private readonly EditionManager _editionManager;
        private readonly UserManager _userManager;
        private readonly IAbpSession _abpSession;
        private readonly IEmailTemplateProvider _emailTemplateProvider;

        public UserEmailer(
            //IEmailTemplateProvider emailTemplateProvider,
            IEmailSender emailSender,
            IRepository<Tenant> tenantRepository,
            ICurrentUnitOfWorkProvider unitOfWorkProvider,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            EditionManager editionManager,
            UserManager userManager,
            IAbpSession abpSession,
            IEmailTemplateProvider emailTemplateProvider)
        {
            //_emailTemplateProvider = emailTemplateProvider;
            _emailSender = emailSender;
            _tenantRepository = tenantRepository;
            _unitOfWorkProvider = unitOfWorkProvider;
            _unitOfWorkManager = unitOfWorkManager;
            //_settingManager = settingManager;
            //_editionManager = editionManager;
            _userManager = userManager;
            _abpSession = abpSession;
            _emailTemplateProvider = emailTemplateProvider;
        }

        [UnitOfWork]
        public virtual async Task SendEmailActivationLinkAsync(User user, string link, string plainPassword = null)
        {
            //await CheckMailSettingsEmptyOrNull();

            if (user.EmailConfirmationCode.IsNullOrEmpty())
            {
                throw new Exception("EmailConfirmationCode should be set in order to send email activation link.");
            }

            link = link.Replace("{userId}", user.Id.ToString());
            link = link.Replace("{confirmationCode}", Uri.EscapeDataString(user.EmailConfirmationCode));

            //if (user.TenantId.HasValue)
            //{
            //    link = link.Replace("{tenantId}", user.TenantId.ToString());
            //}

            link = link;

            //var tenancyName = GetTenancyNameOrNull(user.TenantId);
            var emailTemplate = GetTitleAndSubTitle(user.TenantId, "Reset Password", "Sub Title");
            var mailMessage = new StringBuilder();
            mailMessage.AppendLine("Please click link to enter new password: " + link);

            //mailMessage.AppendLine("<b>" + L("NameSurname") + "</b>: " + user.Name + " " + user.Surname + "<br />");
            //mailMessage.AppendLine("<b>" + ("NameSurname") + "</b>: " + user.Name + " " + user.Surname + "<br />");

            ////if (!tenancyName.IsNullOrEmpty())
            ////{
            ////    mailMessage.AppendLine("<b>" + L("TenancyName") + "</b>: " + tenancyName + "<br />");
            ////}

            //mailMessage.AppendLine("<b>" + L("UserName") + "</b>: " + user.UserName + "<br />");

            //if (!plainPassword.IsNullOrEmpty())
            //{
            //    mailMessage.AppendLine("<b>" + L("Password") + "</b>: " + plainPassword + "<br />");
            //}

            //mailMessage.AppendLine("<br />");
            //mailMessage.AppendLine(L("EmailActivation_ClickTheLinkBelowToVerifyYourEmail") + "<br /><br />");
            //mailMessage.AppendLine("<a style=\"" + emailButtonStyle + "\" bg-color=\"" + emailButtonColor + "\" href=\"" + link + "\">" + L("Verify") + "</a>");
            //mailMessage.AppendLine("<br />");
            //mailMessage.AppendLine("<br />");
            //mailMessage.AppendLine("<br />");
            //mailMessage.AppendLine("<span style=\"font-size: 9pt;\">" + L("EmailMessage_CopyTheLinkBelowToYourBrowser") + "</span><br />");
            //mailMessage.AppendLine("<span style=\"font-size: 8pt;\">" + link + "</span>");

            await ReplaceBodyAndSend(user.EmailAddress, "Reset Password", emailTemplate, mailMessage);
        }

        public Task SendPasswordResetLinkAsync(User user, string link = null)
        {
            throw new NotImplementedException();
        }


        private async Task ReplaceBodyAndSend(string emailAddress, string subject, StringBuilder emailTemplate, StringBuilder mailMessage)
        {
            emailTemplate.Replace("{EMAIL_BODY}", mailMessage.ToString());
            await _emailSender.SendAsync(new MailMessage
            {
                To = { emailAddress },
                Subject = subject,
                Body = emailTemplate.ToString(),
                IsBodyHtml = true
            });
        }

        private StringBuilder GetTitleAndSubTitle(int? tenantId, string title, string subTitle)
        {
            var emailTemplate = new StringBuilder(_emailTemplateProvider.GetDefaultTemplate(tenantId));
            emailTemplate.Replace("{EMAIL_TITLE}", title);
            emailTemplate.Replace("{EMAIL_SUB_TITLE}", subTitle);

            return emailTemplate;
        }
    }
}
