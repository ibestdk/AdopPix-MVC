using System.Collections.Generic;

namespace AdopPix.Models.ViewModels
{
    public class TopUpViewModel
    {
        public decimal CurrentMoney { get; set; }
        public decimal Money { get; set; }
        public List<PaymentViewModel> Log { get; set; }
    }
}
