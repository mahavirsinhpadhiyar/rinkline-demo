using System.Threading.Tasks;
using RinkLine.Models.TokenAuth;
using RinkLine.Web.Controllers;
using Shouldly;
using Xunit;

namespace RinkLine.Web.Tests.Controllers
{
    public class HomeController_Tests: RinkLineWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}