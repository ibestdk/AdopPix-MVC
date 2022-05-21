using System;

namespace AdopPix.Models
{
    public class AuctionBidFindByAuctionId
    {
        public string AvatarName { get; set; }
        public string UserName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Created { get; set; }
    }
}
