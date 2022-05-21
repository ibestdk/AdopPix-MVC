﻿using System;

namespace AdopPix.Models.ViewModels
{
    public class ShowDirectPostViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string PostId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public int LikeCount { get; set; }
        public DateTime Create { get; set; }    
    }
}
