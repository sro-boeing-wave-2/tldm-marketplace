using System;
using MarketPlaceBackend.Controllers;
using MarketPlaceBackend.Models;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using MarketPlaceBackend.Services;

namespace MarketPlaceBackend.Tests
{
    public class MarketPlaceUnitTest
    {
        public ApplicationsController GetController()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MarketPlaceBackendContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            var marketPlaceContext = new MarketPlaceBackendContext(optionsBuilder.Options);
            CreateSampleData(optionsBuilder.Options);
            var _service = new ApplicationService(marketPlaceContext);
            return new ApplicationsController(_service);
        }

        public void CreateSampleData(DbContextOptions<MarketPlaceBackendContext> options)
        {
            using (var _context = new MarketPlaceBackendContext(options))
            {
                var ApplicationsToAdd = new List<Application>
                {
                    new Application()
                    {
                        Id="abcd-efgh",
                        Name="Github",
                        Info="Github Integration",
                        AppUrl="www.github.com",
                        Developer="Mr. XYZ",
                        LogoUrl="www.logo.com"
                    },
                    new Application()
                    {
                        Id="wxyz-abcd",
                        Name="Google Drive",
                        Info="Google Drive Integration",
                        AppUrl="www.googledrive.com",
                        Developer="Mr. ABC",
                        LogoUrl="www.glogo.com"
                    }
                };
                _context.Application.AddRange(ApplicationsToAdd);
                _context.SaveChanges();
            }
        }

        [Fact]
        public async void TestForGetAll()
        {
            var _appcontroller = GetController();
            var Res = await _appcontroller.GetApplication();
            var ListOfApplications = Res as List<Application>;
            Assert.Equal(2, ListOfApplications.Count);
        }

        [Fact]
        public async void TestForGetById()
        {
            var _appcontroller = GetController();
            var Res = await _appcontroller.GetApplication("abcd-efgh");
            var AppOkObject = Res as OkObjectResult;
            var App = AppOkObject.Value as Application;
            App.Name.Should().Be("Github");
            App.Info.Should().Be("Github Integration");
            App.AppUrl.Should().Be("www.github.com");
            App.Developer.Should().Be("Mr. XYZ");
            App.LogoUrl.Should().Be("www.logo.com");
        }

        [Fact]
        public async void TestForPost()
        {
            var _appcontroller = GetController();
            Application app = new Application()
            {
                Id = "efgh-ijkl",
                Name = "Google Photos",
                Info = "Google Photos Integration",
                AppUrl = "www.googlephotos.com",
                Developer = "Mr. Google",
                LogoUrl = "www.googlelogo.com"
            };
            var Result = await _appcontroller.PostApplication(app);
            var AppCreatedAtActionObject = Result as CreatedAtActionResult;
            var App = AppCreatedAtActionObject.Value as Application;
            App.Name.Should().Be("Google Photos");
            App.Info.Should().Be("Google Photos Integration");
            App.AppUrl.Should().Be("www.googlephotos.com");
            App.Developer.Should().Be("Mr. Google");
            App.LogoUrl.Should().Be("www.googlelogo.com");
        }

        [Fact]
        public async void DeleteById()
        {
            var _controller = GetController();
            var result = await _controller.DeleteApplication("abcd-efgh");
            var resultAsOkObjectResult = result as OkObjectResult;
            Assert.Equal(200, resultAsOkObjectResult.StatusCode);
        }

        [Fact]
        public async void TestingPut()
        {
            var _controller = GetController();
            var App = new Application()
            {
                Id = "wxyz-abcd",
                Name = "Google Drive Name Updated"
            };
            var result = await _controller.PutApplication("wxyz-abcd", App);
            var resultAsOkObjectResult = result as NoContentResult;
            Assert.Equal(204, resultAsOkObjectResult.StatusCode);

            var checkingForUpdatedData = await _controller.GetApplication("wxyz-abcd");
            var AppOkObject = checkingForUpdatedData as OkObjectResult;
            var AppObject = AppOkObject.Value as Application;
            AppObject.Name.Should().Be("Google Drive Name Updated");
            AppObject.Info.Should().Be("Google Drive Integration");
            AppObject.Developer.Should().Be("Mr. ABC");
            AppObject.AppUrl.Should().Be("www.googledrive.com");
            AppObject.LogoUrl.Should().Be("www.glogo.com");
        }
    }
}
