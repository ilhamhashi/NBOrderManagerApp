namespace OrderManagerLibrary.Model.Interfaces;
public interface ICollectionType
{
    public int CollectionId { get; set; }
    public DateTime CollectionDate { get; set; }
    public int OrderId { get; set; }
}
