using AdopPix.Models;
using AdopPix.Models.ViewModels;
using AdopPix.Procedure.IProcedure;
using AdopPix.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AdopPix.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly INotificationService notificationService;
        private readonly UserManager<User> userManager;
        private readonly INavbarService navbarService;
        private readonly IAuctionProcedure auctionProcedure;
        private readonly IPostProcedure postProcedure;

        public HomeController(INotificationService notificationService,
                              UserManager<User> userManager,
                              IAuctionProcedure auctionProcedure,
                              IPostProcedure postProcedure,
                              INavbarService navbarService)
        {
            this.notificationService = notificationService;
            this.userManager = userManager;
            this.auctionProcedure = auctionProcedure;
            this.postProcedure = postProcedure;
            this.navbarService = navbarService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);


            var posts = await postProcedure.FindAllAsync();
            var postimages = await postProcedure.GetAllImageAsync();
            var allAuctions = await auctionProcedure.GetAllAsync();
            var allAuctionImages = await auctionProcedure.GetAllImageAsync();
            var allAuctionUsers = await auctionProcedure.GetAllUserDetailAsync();
            var allAuctionImagesUser = await auctionProcedure.GetAllUserImageDetailAsync();

            ViewData["imageAuctions"] = allAuctionImages;
            ViewData["userAuctions"] = allAuctionUsers;
            ViewData["userimageAuctions"] = allAuctionImagesUser;
            ViewData["allposts"] = posts;
            ViewData["allimageposts"] = postimages;


            return View(allAuctions);




        }
        [HttpPost]
        public async Task<IActionResult> Index(string UserId, string Description, string RedirectToUrl)
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            await notificationService.NotificationByUserIdAsync(user.Id, UserId, Description, RedirectToUrl);
            return View();
        }


        [Route("Post")]
        public async Task<IActionResult> Post()
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);
            return View();
        }        



        [Route("TestNoti")]
        public async Task<IActionResult> TestSenderNoti()
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);
            return View();
        }
        public async Task<IActionResult> TestSenderNoti(string UserId, string Description, string RedirectToUrl)
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            await notificationService.NotificationByUserIdAsync(user.Id, UserId, Description, RedirectToUrl);
            return View();
        }


        public async Task<IActionResult> Privacy()
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
