using System;

namespace AdopPix.Services.ModelService
{
    public class UpdateClitentViewModel
    {
        public string auctionId { get; set; }
        public string UserName { get; set; }
        public string AvatarName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Created { get; set; }
    }
}
