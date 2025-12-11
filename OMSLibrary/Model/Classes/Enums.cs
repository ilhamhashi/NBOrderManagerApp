namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents the different statuses an order can have in the bakery system.
/// </summary>
public enum OrderStatus
{/// <summary>
 /// Order has been created but not finalized
 /// (no payment or confirmation yet).
 /// </summary>
    Draft,

    /// <summary>
    /// The customer has paid part of the total amount,
    /// but there is still an outstanding balance.
    /// </summary>
    PartiallyPaid,

    /// <summary>
    /// The order is fully paid and production can begin.
    /// </summary>
    FullyPaid,

    /// <summary>
    /// The order is baked and ready to be delivered or picked up.
    /// </summary>
    ReadyForCollection,

    /// <summary>
    /// The order has been delivered or picked up by the customer.
    /// </summary>
    Completed
}



