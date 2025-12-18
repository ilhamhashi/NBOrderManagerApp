

namespace OrderManagerLibrary.Model.Classes;
/// <summary>
/// Represents an internal note related to a specific order,
/// used to store comments, special instructions or customer requests.
/// </summary>
public class Note
{
    public int Id { get; set; }
    public string Content { get; set; }


    /// <summary>
    /// Constructor used when loading an existing Note from the database as a foreign constraint.
    /// </summary>
    public Note(int id)
    {
        Id = id;
    }

    /// <summary>
    /// Constructor used when loading an existing Note from the database.
    /// </summary>
    public Note(int noteId, string content)
    {
        Id = noteId;
        Content = content;
    }

    /// <summary>
    /// Constructor used when creating a new Note to be saved to the database.
    /// Id is assigned by database. 
    /// </summary>
    public Note(string content)
    {
        Content = content;
    }
}
