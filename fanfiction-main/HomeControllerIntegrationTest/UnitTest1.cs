using NUnit.Framework;
using fanfiction;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Moq;
using fanfiction.Models.User.Inretfaces;
using System.Linq;
using fanfiction.Models.Fanfiction;
using fanfiction.Models.User;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using fanfiction.Controllers;
using fanfiction.Data;
using Microsoft.AspNetCore.Mvc;

namespace HomeControllerIntegrationTest
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public async Task CheckStatusHomeOk()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            HttpClient httpClient = webHost.CreateClient();

            //act

            HttpResponseMessage response = await httpClient.GetAsync("/Home/SignIn");
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        public async Task CheckStatusViewFanficOk()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            HttpClient httpClient = webHost.CreateClient();

            //act

            HttpResponseMessage response = await httpClient.GetAsync("/Fanfiction/ViewFanfic?fanficId=3");
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        public async Task CheckStatusAddFanficOk()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            HttpClient httpClient = webHost.CreateClient();

            //act

            HttpResponseMessage response = await httpClient.GetAsync("/Fanfiction/AddFanfic");
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        public async Task CheckStatusViewFandomsOk()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            HttpClient httpClient = webHost.CreateClient();

            //act

            HttpResponseMessage response = await httpClient.GetAsync("/Fanfiction/ViewFandoms");
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        public async Task CheckStatusAddFandomOk()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            HttpClient httpClient = webHost.CreateClient();

            //act

            HttpResponseMessage response = await httpClient.GetAsync("/Fanfiction/AddFandom");
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        public async Task CheckStatusViewGenresOk()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            HttpClient httpClient = webHost.CreateClient();

            //act

            HttpResponseMessage response = await httpClient.GetAsync("/Fanfiction/ViewGenres");
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        public async Task CheckStatusAddGenreOk()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            HttpClient httpClient = webHost.CreateClient();

            //act

            HttpResponseMessage response = await httpClient.GetAsync("/Fanfiction/AddGenre");
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        public async Task CheckStatusSettingsOk()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            HttpClient httpClient = webHost.CreateClient();

            //act

            HttpResponseMessage response = await httpClient.GetAsync("/Profile/Settings");
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        public async Task CheckStatusUserListOk()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            HttpClient httpClient = webHost.CreateClient();

            //act

            HttpResponseMessage response = await httpClient.GetAsync("/Profile/UserList");
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        public async Task CheckStatusMarksOk()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            HttpClient httpClient = webHost.CreateClient();

            //act

            HttpResponseMessage response = await httpClient.GetAsync("/Profile/Marks");
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        public async Task CheckStatusProfileOk()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            HttpClient httpClient = webHost.CreateClient();

            //act

            HttpResponseMessage response = await httpClient.GetAsync("/Profile/Marks");
            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        //private readonly UserManager<ApplicationUser> _userManager;

        //private readonly SignInManager<ApplicationUser> _signInManager;
        //private ApplicationDbContext _context;

        [Test]
        public async Task MoqLogoutUser()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder => 
            {
                builder.ConfigureServices(async services =>
                {
                    var orderServices = services.SingleOrDefault(d => d.ServiceType == typeof(fanfiction.Models.User.Inretfaces.IActionResult));
                    services.Remove(orderServices);
                    var moqContext = new Mock<ApplicationDbContext>();
                    var moqUser = new Mock<UserManager<ApplicationUser>>();
                    var moqSignIn = new Mock<SignInManager<ApplicationUser>>();
                    moqUser.Setup(_ => _.GetUserId(ClaimsPrincipal.Current)).Returns("");
                    moqUser.Setup(c => c.FindByIdAsync("")).Returns(new Task<ApplicationUser>(()=>null));
                    moqSignIn.Setup(m => m.SignOutAsync()).Returns(Task.CompletedTask);
                    var controller = new FanfictionController(moqContext.Object,moqUser.Object, moqSignIn.Object);

                    //act
                    var result =await controller.LogoutUser();
                    //assert
                    Assert.False(result);

                });
            });  
        }
        [Test]
        public async Task MoqCheckGenre()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    var orderServices = services.SingleOrDefault(d => d.ServiceType == typeof(fanfiction.Models.User.Inretfaces.IActionResult));
                    services.Remove(orderServices);
                    var moqContext = new Mock<ApplicationDbContext>();
                    var moqUser = new Mock<UserManager<ApplicationUser>>();
                    var moqSignIn = new Mock<SignInManager<ApplicationUser>>();
                    var moqGenre = new Mock<Genre>();
                    
                   
                    moqUser.Setup(_ => _.GetUserId(ClaimsPrincipal.Current)).Returns("");
                    moqUser.Setup(c => c.FindByIdAsync("")).Returns(new Task<ApplicationUser>(() => null));
                    moqSignIn.Setup(m => m.SignOutAsync()).Returns(Task.CompletedTask);
                    
                    var controller = new FanfictionController(moqContext.Object, moqUser.Object, moqSignIn.Object);

                    //act
                    var result = await controller.CheckGenre(moqGenre.Object);
                    //assert
                    Assert.IsTrue(result);

                });
            });
        }
        [Test]
        public async Task MoqCheckFandom()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    var orderServices = services.SingleOrDefault(d => d.ServiceType == typeof(fanfiction.Models.User.Inretfaces.IActionResult));
                    services.Remove(orderServices);
                    var moqContext = new Mock<ApplicationDbContext>();
                    var moqUser = new Mock<UserManager<ApplicationUser>>();
                    var moqSignIn = new Mock<SignInManager<ApplicationUser>>();
                    var moqFandom = new Mock<Fandom>();



                    moqUser.Setup(_ => _.GetUserId(ClaimsPrincipal.Current)).Returns("");
                    moqUser.Setup(c => c.FindByIdAsync("")).Returns(new Task<ApplicationUser>(() => null));
                    moqSignIn.Setup(m => m.SignOutAsync()).Returns(Task.CompletedTask);

                    var controller = new FanfictionController(moqContext.Object, moqUser.Object, moqSignIn.Object);

                    //act
                    var result = await controller.CheckFandom(moqFandom.Object);
                    //assert
                    Assert.IsTrue(result);

                });
            });
        }
        [Test]
        public async Task MoqCheckFandomUpd()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    var orderServices = services.SingleOrDefault(d => d.ServiceType == typeof(fanfiction.Models.User.Inretfaces.IActionResult));
                    services.Remove(orderServices);
                    var moqContext = new Mock<ApplicationDbContext>();
                    var moqUser = new Mock<UserManager<ApplicationUser>>();
                    var moqSignIn = new Mock<SignInManager<ApplicationUser>>();
                    var moqFandom = new Mock<Fandom>();



                    moqUser.Setup(_ => _.GetUserId(ClaimsPrincipal.Current)).Returns("");
                    moqUser.Setup(c => c.FindByIdAsync("")).Returns(new Task<ApplicationUser>(() => null));
                    moqSignIn.Setup(m => m.SignOutAsync()).Returns(Task.CompletedTask);

                    var controller = new FanfictionController(moqContext.Object, moqUser.Object, moqSignIn.Object);

                    //act
                    var result = await controller.CheckFandomUpdate(moqFandom.Object);
                    //assert
                    Assert.IsTrue(result);

                });
            });
        }
        [Test]
        public async Task MoqAddFandomInDb()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    var orderServices = services.SingleOrDefault(d => d.ServiceType == typeof(fanfiction.Models.User.Inretfaces.IActionResult));
                    services.Remove(orderServices);
                    var moqContext = new Mock<ApplicationDbContext>();
                    var moqUser = new Mock<UserManager<ApplicationUser>>();
                    var moqSignIn = new Mock<SignInManager<ApplicationUser>>();
                    var moqFandom = new Mock<Fandom>();



                    moqUser.Setup(_ => _.GetUserId(ClaimsPrincipal.Current)).Returns("");
                    moqUser.Setup(c => c.FindByIdAsync("")).Returns(new Task<ApplicationUser>(() => null));
                    moqSignIn.Setup(m => m.SignOutAsync()).Returns(Task.CompletedTask);

                    var controller = new FanfictionController(moqContext.Object, moqUser.Object, moqSignIn.Object);

                    //act
                    var result = await controller.CheckFandom(moqFandom.Object);
                    //assert
                    Assert.IsTrue(result);

                });
            });
        }
        [Test]
        public async Task MoqCheckGenreUpd()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    var orderServices = services.SingleOrDefault(d => d.ServiceType == typeof(fanfiction.Models.User.Inretfaces.IActionResult));
                    services.Remove(orderServices);
                    var moqContext = new Mock<ApplicationDbContext>();
                    var moqUser = new Mock<UserManager<ApplicationUser>>();
                    var moqSignIn = new Mock<SignInManager<ApplicationUser>>();
                    var moqGenre = new Mock<Genre>();


                    moqUser.Setup(_ => _.GetUserId(ClaimsPrincipal.Current)).Returns("");
                    moqUser.Setup(c => c.FindByIdAsync("")).Returns(new Task<ApplicationUser>(() => null));
                    moqSignIn.Setup(m => m.SignOutAsync()).Returns(Task.CompletedTask);

                    var controller = new FanfictionController(moqContext.Object, moqUser.Object, moqSignIn.Object);

                    //act
                    var result = await controller.CheckGenreUpdate(moqGenre.Object);
                    //assert
                    Assert.IsTrue(result);

                });
            });
        }
        [Test]
        public async Task MoqUpdateFanfic() 
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    var orderServices = services.SingleOrDefault(d => d.ServiceType == typeof(fanfiction.Models.User.Inretfaces.IActionResult));
                    services.Remove(orderServices);
                    var moqContext = new Mock<ApplicationDbContext>();
                    var moqUser = new Mock<UserManager<ApplicationUser>>();
                    var moqSignIn = new Mock<SignInManager<ApplicationUser>>();
                    var moqAddFanfic = new Mock<AddFanfic>();


                    moqUser.Setup(_ => _.GetUserId(ClaimsPrincipal.Current)).Returns("");
                    moqUser.Setup(c => c.FindByIdAsync("")).Returns(new Task<ApplicationUser>(() => null));
                    moqSignIn.Setup(m => m.SignOutAsync()).Returns(Task.CompletedTask);

                    var controller = new FanfictionController(moqContext.Object, moqUser.Object, moqSignIn.Object);

                    //act
                    var result = await controller.UpdateFanfic(moqAddFanfic.Object);
                    //assert
                    Assert.IsTrue(result);

                });
            });
        }

        [Test]
        public async Task MoqRole()
        {
            //arrange
            WebApplicationFactory<Startup> webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    var orderServices = services.SingleOrDefault(d => d.ServiceType == typeof(fanfiction.Models.User.Inretfaces.IActionResult));
                    services.Remove(orderServices);
                    var moqContext = new Mock<ApplicationDbContext>();
                    var moqUser = new Mock<UserManager<ApplicationUser>>();
                    var moqSignIn = new Mock<SignInManager<ApplicationUser>>();
                    var moqAddFanfic = new Mock<AddFanfic>();


                    moqUser.Setup(_ => _.GetUserId(ClaimsPrincipal.Current)).Returns("");
                    moqUser.Setup(c => c.FindByIdAsync("")).Returns(new Task<ApplicationUser>(() => null));
                    moqSignIn.Setup(m => m.SignOutAsync()).Returns(Task.CompletedTask);

                    var controller = new FanfictionController(moqContext.Object, moqUser.Object, moqSignIn.Object);

                    //act
                    var result = await controller.Role();
                    //assert
                    Assert.IsTrue(result);

                });
            });
        }
    }
}