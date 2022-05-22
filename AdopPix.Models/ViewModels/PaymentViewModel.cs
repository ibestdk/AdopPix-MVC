using System;

namespace AdopPix.Models.ViewModels
{
    public class PaymentViewModel
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime Created { get; set; }
    }
}
