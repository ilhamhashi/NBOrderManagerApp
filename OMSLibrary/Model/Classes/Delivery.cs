using OrderManagerLibrary.Model.Interfaces;
using System.Collections;

namespace OrderManagerLibrary.Model.Classes;

public class Delivery : ICollectionType
{
    public int CollectionId { get; set; }
    public DateTime CollectionDate { get; set; }
    public int OrderId { get; set; }
}
