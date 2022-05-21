using AdopPix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdopPix.Procedure.IProcedure
{
    public interface IAuctionBidProcedure
    {
        Task Create(AuctionBid entity);
        Task<AuctionBid> FindMaxAmountByAuctionId(string auctionId);
        Task<List<AuctionBid>> FindUserLoseAuction(string auctionId);
        Task<List<AuctionBidFindByAuctionId>> FindByAuctionId(string auctionId);
    }
}
