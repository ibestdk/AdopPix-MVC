using AdopPix.Models;
using AdopPix.Models.ViewModels;
using System.Threading.Tasks;

namespace AdopPix.Procedure.IProcedure
{
    public interface IUserProfileProcedure
    {
        Task CreateAsync(UserProfile entity);
        Task<UserProfile> FindByIdAsync(string userId);
        Task<UserLikeViewModel> FindByUserNameAsync(string Username);
        Task UpdateAsync(UserProfile entity);
    }
}
