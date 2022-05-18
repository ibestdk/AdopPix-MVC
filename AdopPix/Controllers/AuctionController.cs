using AdopPix.Models;
using AdopPix.Models.ViewModels;
using AdopPix.Procedure.IProcedure;
using AdopPix.Services.IServices;
using AdopPix.Services.ModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdopPix.Controllers
{
    public class AuctionController : Controller
    {
        private readonly INavbarService navbarService;
        private readonly IAuctionProcedure auctionProcedure;
        private readonly UserManager<User> userManager;
        private readonly IImageService imageService;
        private readonly IAuctionBidProcedure auctionBidProcedure;
        private readonly IAuctionHubService auctionHubService;
        private readonly INotificationService notificationService;
        private readonly IUserProfileProcedure userProfileProcedure;
        private string GenerateAuctionId()
        {
            string[] dateTime = DateTime.Now.ToString().Split(' ');
            string[] ddmmyyyy = dateTime[0].Split('/');
            string[] hhmmss = dateTime[1].Split(':');
            return $"auction-{string.Join("", ddmmyyyy)}{string.Join("", hhmmss)}";
        }
        public AuctionController(INavbarService navbarService, 
                                 IUserProfileProcedure userProfileProcedure, 
                                 UserManager<User> userManager, 
                                 IAuctionProcedure auctionProcedure, 
                                 IImageService imageService,
                                 IAuctionBidProcedure auctionBidProcedure,
                                 IAuctionHubService auctionHubService,
                                 INotificationService notificationService)
        {
            this.navbarService = navbarService;
            this.userManager = userManager;
            this.userProfileProcedure = userProfileProcedure;
            this.auctionProcedure = auctionProcedure;
            this.imageService = imageService;
            this.auctionBidProcedure = auctionBidProcedure;
            this.auctionHubService = auctionHubService;
            this.notificationService = notificationService;
        }
        //-----------------------------------------------------------------------------------------------------------
        public async Task<IActionResult> Index()
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);


            var allAuctions = await auctionProcedure.GetAllAsync();
            var allAuctionEnds = await auctionProcedure.GetAllAuctionEnd();
            var allAuctionImages = await auctionProcedure.GetAllImageAsync();
            var allAuctionUsers = await auctionProcedure.GetAllUserDetailAsync();
            var allAuctionImagesUser = await auctionProcedure.GetAllUserImageDetailAsync();

            ViewData["imageAuctions"] = allAuctionImages;
            ViewData["userAuctions"] = allAuctionUsers;
            ViewData["userimageAuctions"] = allAuctionImagesUser;
            ViewData["AuctionsEnds"] = allAuctionEnds;


            return View(allAuctions);
        }

        //-----------------------------------------------------------------------------------------------------------
        public async Task<IActionResult> Create()
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);
            return View();
        }
        //-----------------------------------------------------------------------------------------------------------


        [HttpPost("Auction/Create")]
        public async Task<IActionResult> Create(AuctionViewModel auctionViewModel)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);
            string imageAuctionName = auctionViewModel.AuctionImage;

            if (auctionViewModel.AuctionFile != null)
            {
                string[] extension = { ".png", ".jpg" };
                if (imageService.ValidateExtension(extension, auctionViewModel.AuctionFile))
                {
                    imageAuctionName = await imageService.UploadAuctionImageAsync(auctionViewModel.AuctionFile);
                }
                else
                {
                    ModelState.AddModelError("AvatarFile", "We support .png, .jpg");
                    return View(auctionViewModel);
                }
            }


            var generateAuctionId = GenerateAuctionId();
            Auction auction = new Auction
            {
                AuctionId = generateAuctionId,
                UserId = user.Id,
                Title = auctionViewModel.Title,
                HourId = auctionViewModel.HourId,
                OpeningPrice = auctionViewModel.OpeningPrice,
                HotClose = auctionViewModel.HotClose,
                Description = auctionViewModel.Description,
                Created = DateTime.Now,

            };
            await auctionProcedure.CreateAsync(auction);
            AuctionImage auctionImage = new AuctionImage
            {
                AuctionId = generateAuctionId,
                ImageId = imageAuctionName,
                ImageTypeId = auctionViewModel.ImageTypeId,
                Created = auctionViewModel.Created,
            };
            await auctionProcedure.CreateImageAsync(auctionImage);

            return Redirect("/Auction");
        }




        //-----------------------------------------------------------------------------------------------------------

        [HttpGet("Auction/Post/{aucId}")]
        public async Task<IActionResult> AuctionPost(string aucId)
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);
            //var user = await userManager.FindByNameAsync(auctionViewModel.AuctionId);
            var auctionpost = await auctionProcedure.FindByIdAsync(aucId);
            if(auctionpost == null)
            {
                return null;
            }
            var auctionimage = await auctionProcedure.FindImageByIdAsync(aucId);
            var userProfiles = await userProfileProcedure.FindByIdAsync(auctionpost.UserId);
            var users = await userManager.FindByIdAsync(auctionpost.UserId);
            var user = users.UserName;

            var bidData = await auctionBidProcedure.FindMaxAmountByAuctionId(aucId);

            string maxBidUsername = string.Empty;
            decimal lastPrice = 0;

            if (bidData != null)
            {
                var maxBidUser = await userManager.FindByIdAsync(bidData.UserId);
                maxBidUsername = maxBidUser.UserName;
                lastPrice = bidData.Amount;
            }
            
            
            AuctionViewModel auction = new AuctionViewModel
            {
                AvaterName = userProfiles.AvatarName,
                UserName = user,
                AuctionId = auctionpost.AuctionId,
                UserId = auctionpost.UserId,
                Title = auctionpost.Title,
                HourId = auctionpost.HourId,
                OpeningPrice = auctionpost.OpeningPrice,
                Price = lastPrice,
                HotClose = auctionpost.HotClose,
                Description = auctionpost.Description,
                Created = auctionpost.Created,
                ImageId = auctionimage.ImageId,
                LastBid = maxBidUsername,
            };

            ViewBag.WinningBid = false;
            var winningBid = await auctionProcedure.WinningBidderFindByAuctionId(auctionpost.AuctionId);
            if (winningBid != null)
            {
                ViewBag.WinningBid = true;
            }

            if(auctionpost.StopTime != null)
            {
                auction.StopTime = (DateTime)auctionpost.StopTime;
            }

            if(TempData["ErrorBid"] != null)
            {
                ViewBag.ErrorBid = TempData["ErrorBid"];
            }

            return View(auction);
        }


        //-----------------------------------------------------------------------------------------------------------
       
        
        [HttpGet("/Auction/Edit/{postId}")]
        public async Task<IActionResult> Edit(string postId = "")
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);

            var post = await auctionProcedure.FindByIdAsync(postId);
            if (post == null)
            {
                return null;
            }

            var image = await auctionProcedure.FindImageByIdAsync(postId);
            var userProfiles = await userProfileProcedure.FindByIdAsync(post.UserId);
            var users = await userManager.FindByIdAsync(post.UserId);
            AuctionViewModel edit = new AuctionViewModel
            {
                AvaterName = userProfiles.AvatarName,
                UserName = users.UserName,
                AuctionId = post.AuctionId,
                Title = post.Title,
                Description = post.Description,
                ImageId = image.ImageId
            };

            return View(edit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AuctionViewModel model)
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);

            var auctiondetail = await auctionProcedure.FindByIdAsync(model.AuctionId);
            if (auctiondetail != null)
            {
                Auction auctionModel = new Auction()
                {
                    AuctionId = auctiondetail.AuctionId,
                    Title = model.Title,
                    Description = model.Description
                };
                await auctionProcedure.UpdateAuctionAsync(auctionModel);
                return RedirectToAction("Post", "Auction", new { id = auctiondetail.AuctionId });
            }
            return View(model);
        }

        //-----------------------------------------------------------------------------------------------------------


        [HttpGet("Auction/Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            // หาว่าต้องลบโพสไหน
            var post = await auctionProcedure.FindByIdAsync(id);
            // หาว่าต้องลบภาพจากโพสไหน
            var postImage = await auctionProcedure.FindImageByIdAsync(id);
            // เช็คว่าเป็น null ไหม
            if (post == null)
            {
                return null;
            }
            // เช็คว่าเป็น null ไหม
            if (postImage == null)
            {
                return null;
            }

            // ลบภาพของโพสตามที่ตรวจเจอ
            await auctionProcedure.DeleteImageAsync(postImage);
            // ลบโพสตามที่ตรวจเจอ
            await auctionProcedure.DeleteAuctionAsync(post);

            return Redirect("/Auction");
        }

        [HttpPost("Auction/Bid/{auctionId}")]
        public async Task<IActionResult> BidByAuctionId(string auctionId, int amount)
        {
            var auction = await auctionProcedure.FindByIdAsync(auctionId);
            if (auction == null) return NotFound();

            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user == null) return NotFound();

            var profile = await userProfileProcedure.FindByIdAsync(user.Id);
            if (profile == null) return NotFound();

            var winningBid = await auctionProcedure.WinningBidderFindByAuctionId(auction.AuctionId);
            if (winningBid != null)
            {
                TempData["ErrorBid"] = "This auction has ended.";
                return Redirect($"/Auction/Post/{auctionId}");
            }

            if(profile.Money < amount)
            {
                TempData["ErrorBid"] = "the money is not enough";
                return Redirect($"/Auction/Post/{auctionId}");
            }

            if(auction.OpeningPrice > amount)
            {
                TempData["ErrorBid"] = $"The starting price is at {auction.OpeningPrice}";
                return Redirect($"/Auction/Post/{auctionId}");
            }

            DateTime createdAt = DateTime.Now;
            if (auction.StartTime == null)
            {
                if (auction.OpeningPrice > amount)
                {
                    TempData["ErrorBid"] = "price too low.";
                    return Redirect($"/Auction/Post/{auctionId}");
                }

                DateTime dateTime = DateTime.Now;
                int hour = (auction.HourId == 1) ? 24 : 48;

                await auctionProcedure.InitialTime(auction.AuctionId, dateTime, dateTime.AddHours(hour));

                AuctionBid auctionBid = new AuctionBid()
                {
                    UserId = user.Id,
                    AuctionId = auctionId,
                    Amount = amount,
                    Created = createdAt,
                };

                auction.Status = 0;
                
                await auctionBidProcedure.Create(auctionBid);
            }
            else
            {
                var bidData = await auctionBidProcedure.FindMaxAmountByAuctionId(auctionId);
                if (bidData.Amount >= amount)
                {
                    TempData["ErrorBid"] = "price too low.";
                    return Redirect($"/Auction/Post/{auctionId}");
                }
                auction.Status = 1;
                if (amount >= auction.HotClose)
                {
                    auction.Status = 2;
                    WinningBidder bidder = new WinningBidder()
                    {
                        UserId = user.Id,
                        AuctionId = auctionId,
                        amount = amount,
                        Created = DateTime.Now,
                    };

                    var userWinning = await userProfileProcedure.FindByIdAsync(auction.UserId);
                    userWinning.Money += amount;
                    await userProfileProcedure.UpdateAsync(userWinning);

                    await auctionProcedure.WinningBidderCreate(bidder);

                    var loseAuctions = await auctionBidProcedure.FindUserLoseAuction(auction.AuctionId);
                    if(loseAuctions != null)
                    {
                        foreach(var item in loseAuctions)
                        {
                            var loseUser = await userProfileProcedure.FindByIdAsync(item.UserId);
                            loseUser.Money += item.Amount;
                            await userProfileProcedure.UpdateAsync(loseUser);
                            await notificationService.NotificationByUserIdAsync(auction.UserId, item.UserId, $"คุณเเพ้การประมูล", $"/Auction/Post/{auctionId}");
                        }
                    }
                    
                    var auctionimage = await auctionProcedure.FindImageByIdAsync(auction.AuctionId);
                    await notificationService.NotificationByUserIdAsync(auction.UserId, user.Id, $"คุณชนะการประมูล", $"https://adoppix.s3.ap-southeast-1.amazonaws.com/{auctionimage.ImageId}");
                    await notificationService.NotificationByUserIdAsync(user.Id, auction.UserId, $"ปิดการประมูลของคุณที่ราคา {amount}", $"/Auction/Post/{auctionId}");

                }

                AuctionBid auctionBid = new AuctionBid()
                {
                    UserId = user.Id,
                    AuctionId = auctionId,
                    Amount = amount,
                    Created = createdAt,
                };
                await auctionBidProcedure.Create(auctionBid);
            }

            profile.Money -= amount;
            await userProfileProcedure.UpdateAsync(profile);

            UpdateClitentViewModel model = new UpdateClitentViewModel()
            {
                auctionId = auctionId,
                UserName = user.UserName,
                AvatarName = profile.AvatarName,
                Amount = amount,
                Created = createdAt
            };
            await auctionProcedure.UpdateAuctionAsync(auction);
            await auctionHubService.UpdateClientsAsync(model);

            return Redirect($"/Auction/Post/{auctionId}");
        }
    }
}
