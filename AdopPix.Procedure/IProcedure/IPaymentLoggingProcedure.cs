using AdopPix.Models;
using AdopPix.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdopPix.Procedure.IProcedure
{
    public interface IPaymentLoggingProcedure
    {
        Task CreateAsync(PaymentLogging entity);
        Task<List<PaymentViewModel>> FindByUserIdAsync(string userId);
    }
}
