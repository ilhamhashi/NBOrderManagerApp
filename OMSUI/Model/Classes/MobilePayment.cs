using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMSDesktopUI.Model.Classes
{
    public class MobilePayment : IPaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string Name { get; set; }
    }
}
