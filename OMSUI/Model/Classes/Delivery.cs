using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMSDesktopUI.Model.Interfaces;

namespace OMSDesktopUI.Model.Classes
{
    public class Delivery : ICollectionType
    {
        public int CollectionId { get; set; }
        public DateTime CollectionDate { get; set; }
        public int OrderId { get; set; }
    }
}
