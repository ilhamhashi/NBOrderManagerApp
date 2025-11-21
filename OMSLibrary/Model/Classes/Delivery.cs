using OrderManagerLibrary.Model.Interfaces;
namespace OrderManagerLibrary.Model.Classes;

public class Delivery : ICollectionType
{
    public int CollectionId { get; set; }
    public DateTime CollectionDate { get; set; }
    public int OrderId { get; set; }
    public string Neighborhood { get; set; }

    public Delivery(int collectionId, DateTime collectionDate, int orderId, string neighborhood)
    {
        CollectionId = collectionId;
        CollectionDate = collectionDate;
        OrderId = orderId;
        Neighborhood = neighborhood;
    }
}
