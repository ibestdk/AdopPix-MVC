using AdopPix.Hubs;
using AdopPix.Services.IServices;
using AdopPix.Services.ModelService;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdopPix.Services
{
    public class AuctionHubService : IAuctionHubService
    {
        private readonly IHubContext<AuctionHub> hubContext;

        public AuctionHubService(IHubContext<AuctionHub> hubContext)
        {
            this.hubContext = hubContext;
        }
        public async Task UpdateClientsAsync(UpdateClitentViewModel model)
        {
            await hubContext.Clients.All.SendAsync(model.auctionId, new { username = model.UserName, 
                                                                          avatarName = model.AvatarName,
                                                                          amount = model.Amount,
                                                                          created = model.Created
                                                                        });
        }
    }
}
