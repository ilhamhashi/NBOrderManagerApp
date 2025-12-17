namespace OrderManagerLibrary.Model.Classes;


/// <summary>
/// Represents the different statuses an order can have in the bakery system.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Order is in a draft state.
    /// Neither pickup nor payment has been confirmed,
    /// but at least one product and a customer has been registered.
    /// </summary>
    Draft,

    /// <summary>
    /// A part of the order total has been received,
    /// but there is still an outstanding balance.
    /// </summary>
    PartiallyPaid,

    /// <summary>
    /// The order is fully paid and production can begin.
    /// </summary>
    Paid,

    /// <summary>
    /// Production has completed the order,
    /// and it is ready to be picked up or delivered.
    /// </summary>
    ReadyForCollection,

    /// <summary>
    /// The customer has received their order.
    /// </summary>
    Completed
}

