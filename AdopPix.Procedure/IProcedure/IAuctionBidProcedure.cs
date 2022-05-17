using AdopPix.Models;
using System.Threading.Tasks;

namespace AdopPix.Procedure.IProcedure
{
    public interface IAuctionBidProcedure
    {
        Task Create(AuctionBid entity);
        Task<AuctionBid> FindMaxAmountByAuctionId(string auctionId);
    }
}
