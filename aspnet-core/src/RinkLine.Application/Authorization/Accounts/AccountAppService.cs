using System;
using System.Net.Mail;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Extensions;
using Abp.Net.Mail;
using Abp.Net.Mail.Smtp;
using Abp.UI;
using Abp.Zero.Configuration;
using RinkLine.Authorization.Accounts.Dto;
using RinkLine.Authorization.Users;
using RinkLine.Url;

namespace RinkLine.Authorization.Accounts
{
    public class AccountAppService : RinkLineAppServiceBase, IAccountAppService
    {
        // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
        public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";

        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly UserEmailer _userEmailer;
        private readonly IEmailSender _emailSender;
        private readonly ISmtpEmailSender _smtpEmailSender;
        public IAppUrlService AppUrlService { get; set; }
        public AccountAppService(
            UserRegistrationManager userRegistrationManager,
            //UserEmailer userEmailer,
            IEmailSender emailSender,
            ISmtpEmailSender smtpEmailSender
             )
        {
            _userRegistrationManager = userRegistrationManager;
            //_userEmailer = userEmailer;
            emailSender = _emailSender;
            _smtpEmailSender = smtpEmailSender;
        }

        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                true // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }

        public async Task<ForgotPassword> SendPasswordResetCode(ForgotPassword forgotPassword)
        {
            try
            {
                var user = await UserManager.FindByEmailAsync(forgotPassword.EmailAddress);

                if (user == null)
                {
                    throw new UserFriendlyException("User not found!");
                }

                user.SetNewPasswordResetCode();

                //Send an email to user with the below password reset code
                //await _userEmailer.SendPasswordResetLinkAsync(user, "http://localhost:4200/account/reset-password?Id=userId&Code=confirmationCode");

                //await _userEmailer.SendPasswordResetLinkAsync(
                //user,
                //AppUrlService.CreatePasswordResetUrlFormat(AbpSession.TenantId)
                //);

                _emailSender.Send(
                    from: "padhiyarmahavirsinh@gmail.com",
                    to: forgotPassword.EmailAddress,
                    subject: "You have a new task!",
                    body: $"Click here: <b></b>",
                    isBodyHtml: true
                );

                //using (MailMessage mail = new MailMessage())
                //{
                //    mail.From = new MailAddress(emailFromAddress);
                //    mail.To.Add(emailToAddress);
                //    mail.Subject = subject;
                //    mail.Body = body;
                //    mail.IsBodyHtml = true;
                //    //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                //    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                //    {
                //        smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                //        smtp.EnableSsl = enableSSL;
                //        smtp.Send(mail);
                //    }
                //}

                forgotPassword.SentSuccessfully = true;
            }
            catch (Exception ex)
            {
                forgotPassword.SentSuccessfully = false;
            }
            return forgotPassword;
        }

        public async Task ResetPasswordAsync(ResetPassword resetPassword)
        {
            var user = await UserManager.GetUserByIdAsync(resetPassword.UserId);            if (user == null || user.PasswordResetCode.IsNullOrEmpty() || user.PasswordResetCode != resetPassword.ResetCode)            {                throw new UserFriendlyException(L("InvalidPasswordResetCode"), L("InvalidPasswordResetCode_Detail"));            }

            user.PasswordResetCode = null;            user.IsEmailConfirmed = true;
            //user.ShouldChangePasswordOnNextLogin = false;

            await UserManager.ChangePasswordAsync(user, resetPassword.NewPassword);            await UserManager.UpdateAsync(user);
        }
    }
}
