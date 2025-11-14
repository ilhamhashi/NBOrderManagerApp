using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMSDesktopUI.Model.Interfaces
{
    public interface ICollectionType
    {
        public int CollectionId { get; set; }
        public DateTime CollectionDate { get; set; }
        public int OrderId { get; set; }
    }
}
