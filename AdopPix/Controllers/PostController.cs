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
        // แทน i procedure แบบย่อ
        private readonly INavbarService navbarService;
        private readonly IPostProcedure postProcedure;
        private readonly UserManager<User> userManager;
        private readonly IImageService imageService;
        private readonly IAuctionProcedure auctionProcedure;

        // แทน procedure แบบย่อ
        public PostController(IPostProcedure post,
                              INavbarService navbarService,
                              UserManager<User> userManager,
                              IImageService imageService,
                              IAuctionProcedure auctionProcedure)
        {
            this.postProcedure = post;
            this.navbarService = navbarService;
            this.userManager = userManager;
            this.imageService = imageService;
            this.auctionProcedure = auctionProcedure;
        }

        // กำหนด http
        [HttpGet("Post/Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);
            return View();
        }


        [HttpPost]
        // สร้างโพส รับ title , description , รูป
        public async Task<IActionResult> Create(string Title, string Description, IFormFile Image)
        {
            // กำหนดนามสกุลรูป ลง list extension
            string[] extension = { ".png", ".jpg" };
            string fileName = string.Empty;
            // เงื่อนไข หากได้รับข้อมูลเข้ามา ให้นำชื่อรูปใส่ลง fileName
            if (imageService.ValidateExtension(extension, Image))
            {
                fileName = await imageService.UploadImageAsync(Image);
            }
            // หากไม่มี ให้แจ้งเตือนว่าไม่ซัพพอท แล้วรีเทิน
            else
            {
                ModelState.AddModelError("AvatarFile", "We support .png, .jpg");
                return View();
            }
            // กำหนดเวลาปัจจุบันลง time
            DateTime time = DateTime.Now;
            // หา user ปัจจุบัน ว่าเป็นใครที่กำลังใช้งานอยู่
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            // กำหนดตัวแปร โดยเอาตัวแปรใน Post Model เป็นหลัก มาใส่ค่า ไว้ส่งให้ DB
            Post postDetail = new Post
            {
                // นำค่าที่รับเข้ามาแต่ละอันใส่เข้าไปใน postDetail
                Title = Title,
                Description = Description,
                UserId = user.Id,
                Created = time
            };
            // เรียกใช้ CreateAsync ของ PostProcedure โดยส่ง postDetail ไป 
            await postProcedure.CreateAsync(postDetail);
            // กำหนดตัวแปร โดยเอาตัวแปรใน PostImage Model เป็นหลัก มาใส่ค่า ไว้ส่งให้ DB
            PostImage postImageDetail = new PostImage
            {
                PostId = postDetail.PostId,
                Created = time,
                ImageId = fileName
            };
            // เรียกใช้ CreateImageAsync ของ PostProcedure โดยส่ง postImageDetail ไป
            await postProcedure.CreateImageAsync(postImageDetail);
            // โหลด service ของ navbar 
            ViewData["NavbarDetail"] = await navbarService.FindByNameAsync(User.Identity.Name);
            // พากลับไปหน้า แสดงผลงานทั่วไป
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

            ShowDirectPostViewModel showDirectPostViewModel = new ShowDirectPostViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                UserName = user.UserName,
                ImageName = image.ImageId,
                UserId =post.UserId,
                Create = post.Created,
                LikeCount = like
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
