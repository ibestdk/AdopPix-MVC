using AdopPix.Models;
using AdopPix.Models.ViewModels;
using AdopPix.Procedure;
using AdopPix.Procedure.IProcedure;
using AdopPix.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdopPix.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly INavbarService navbarService;
        private readonly IPostProcedure postProcedure;
        private readonly UserManager<User> userManager;
        private readonly IImageService imageService;
        private readonly IAuctionProcedure auctionProcedure;
        private readonly IUserProfileProcedure userProfileProcedure;

        public PostController(IPostProcedure post,
                              INavbarService navbarService,
                              UserManager<User> userManager,
                              IImageService imageService,
                              IUserProfileProcedure userProfileProcedure,
                              IAuctionProcedure auctionProcedure)
        {
            this.postProcedure = post;
            this.navbarService = navbarService;
            this.userManager = userManager;
            this.imageService = imageService;
            this.auctionProcedure = auctionProcedure;
            this.userProfileProcedure = userProfileProcedure;
        }

        [HttpGet("Post/Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Title, string Description, IFormFile Image)
        {
            string[] extension = { ".png", ".jpg" };
            string fileName = string.Empty;
            if (imageService.ValidateExtension(extension, Image))
            {
                fileName = await imageService.UploadImageAsync(Image);
            }
            else
            {
                ModelState.AddModelError("AvatarFile", "We support .png, .jpg");
                return View();
            }
            DateTime time = DateTime.Now;
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            Post postDetail = new Post
            {
                Title = Title,
                Description = Description,
                UserId = user.Id,
                Created = time
            };
            string postId = await postProcedure.CreateAsync(postDetail);

            PostImage postImageDetail = new PostImage
            {
                PostId = postId,
                Created = time,
                ImageId = fileName
            };
            await postProcedure.CreateImageAsync(postImageDetail);
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);

            return Redirect("/illustration");
        }

        [HttpGet("illustration")]

        public async Task<IActionResult> Index()
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);

            List<PostViewModel> postViewModels = new List<PostViewModel>();
            var posts = await postProcedure.FindAllAsync();
            var allUsers = await auctionProcedure.GetAllUserDetailAsync();
            var allImagesUser = await auctionProcedure.GetAllUserImageDetailAsync();

            foreach (var item in posts)
            {

                var image = await postProcedure.FindImageByPostIdAsync(item.PostId);
                PostViewModel postViewModel = new PostViewModel
                {
                    Title = item.Title,
                    ImageName = image.ImageId,
                    PostId = item.PostId,
                    UserId = item.UserId
                };
                postViewModels.Add(postViewModel);
            }
            ViewData["userAuctions"] = allUsers;
            ViewData["userimageAuctions"] = allImagesUser;

            return View(postViewModels);
        }

        [HttpGet("Illustration/Post/{id}")]
        public async Task<IActionResult> FindById(string id = "")
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);

            var post = await postProcedure.FindByPostId(id);
            if (post == null)
            {
                return null;
            }

            var user = await userManager.FindByIdAsync(post.UserId);
            var image = await postProcedure.FindImageByPostIdAsync(id);
            var allImagesUser = await auctionProcedure.GetAllUserImageDetailAsync();
            var like = await postProcedure.ShowLikeById(id);
            var useridcheck = await userProfileProcedure.FindByUserNameAsync(User.Identity.Name);
            var likeStatus = await postProcedure.CheckLikeStatusById(id, useridcheck.UserId);
            var UserLikeStatus = 0;
            if (likeStatus != null)
            {
                UserLikeStatus = 1;
            }
            else
            {

                UserLikeStatus = 0;


            }

            ShowDirectPostViewModel showDirectPostViewModel = new ShowDirectPostViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                UserName = user.UserName,
                ImageName = image.ImageId,
                UserId =post.UserId,
                Create = post.Created,
                LikeCount = like,
                UserLikeStatus = UserLikeStatus

            };



            ViewData["Post"] = showDirectPostViewModel;
            ViewData["userimageAuctions"] = allImagesUser;

            return View();
        }

        [HttpGet("/illustration/remove/{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            // หาว่าต้องลบโพสไหน
            var post = await postProcedure.FindByPostId(id);
            // หาว่าต้องลบภาพจากโพสไหน
            var postImage = await postProcedure.FindImageByPostIdAsync(id);
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
            await postProcedure.DeleteImageAsync(postImage);
            // ลบโพสตามที่ตรวจเจอ
            await postProcedure.DeletePostAsync(post);



            return Redirect("/illustration");
        }

        [HttpGet("/post/[action]/{postId}")]
        public async Task<IActionResult> Edit(string postId = "")
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);

            var post = await postProcedure.FindByPostId(postId);
            if (post == null)
            {
                return null;
            }

            var image = await postProcedure.FindImageByPostIdAsync(postId);
            var users = await userManager.FindByIdAsync(post.UserId);
            var allImagesUser = await auctionProcedure.GetAllUserImageDetailAsync();
            var allAuctionUsers = await auctionProcedure.GetAllUserDetailAsync();


            EditViewModel edit = new EditViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                ImageName = image.ImageId,
                UserId = post.UserId,


            };
            ViewData["userimageAuctions"] = allImagesUser;
            ViewData["userAuctions"] = allAuctionUsers;
            return View(edit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);
            var allImagesUser = await auctionProcedure.GetAllUserImageDetailAsync();
            var post = await postProcedure.FindByPostId(model.PostId);
            if (post != null)
            {
                Post postModel = new Post()
                {
                    PostId = post.PostId,
                    Title = model.Title,
                    Description = model.Description
                };
                await postProcedure.UpdatePostAsync(postModel);
                ViewData["userimageAuctions"] = allImagesUser;


                return RedirectToAction("Post", "illustration", new { id = postModel.PostId });
            }
            return View(model);
        }

        public async Task<IActionResult> Like(string postId)
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var posts = await postProcedure.FindByPostId(postId);

            var likeStatus = await postProcedure.CheckLikeStatusById(posts.PostId, user.Id);
            
            if (likeStatus != null)
            {
                PostLike postLikeDetail = new PostLike
                {
                    PostId = posts.PostId,
                    UserId = user.Id,
                };
                await postProcedure.UnLikeAsync(postLikeDetail);
                return RedirectToAction("Post", "illustration", new { id = postId });
            }
            else
            {
                PostLike postLikeDetail = new PostLike
                {
                    PostId = posts.PostId,
                    UserId = user.Id,
                    Created = DateTime.Now
                };
                await postProcedure.LikeAsync(postLikeDetail);
                return RedirectToAction("Post", "illustration", new { id = postId });
            }
        }

    }
}
