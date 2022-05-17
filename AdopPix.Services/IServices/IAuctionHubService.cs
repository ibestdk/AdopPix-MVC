using AdopPix.Services.ModelService;
using System.Threading.Tasks;

namespace AdopPix.Services.IServices
{
    public interface IAuctionHubService
    {
        Task UpdateClientsAsync(UpdateClitentViewModel model);
    }
}
