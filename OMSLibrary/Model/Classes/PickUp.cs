using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Classes;
public class PickUp : ICollectionType
{
    public int CollectionId { get; set; }
    public DateTime CollectionDate { get; set; }
    public int OrderId { get; set; }
}
