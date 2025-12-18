namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a pickup collection for an order, including the pickup date
/// and whether delivery is included and the location for the pickup
/// </summary>
public class PickUp
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsDelivery { get; set; }
    public string Location { get; set; }


    /// <summary>
    /// Creates a pickup object using given parameters
    /// </summary>
    public PickUp(int id, DateTime date, bool isDelivery, string location)
    {
        Id = id;
        Date = date;
        IsDelivery = isDelivery;
        Location = location;
    }

    /// <summary>
    /// Creates a pickup object using given parameters
    /// </summary>

    public PickUp(DateTime date, bool isDelivery, string location)
    {
        Date = date;
        IsDelivery = isDelivery;
        Location = location;
    }

    public PickUp(int id)
    {
        Id = id;
    }
}
