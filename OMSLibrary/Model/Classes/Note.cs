using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Classes;
public class Note : INote
{
    public int NoteId { get; set; }
    public string NoteText { get; set; }
    public int OrderId { get; set; }
    //public List<string> ImagePaths { get; set; }

    public Note(int noteId, string noteText, int orderId)
    {
        NoteId = noteId;
        NoteText = noteText;
        OrderId = orderId;
        //ImagePaths = imagePaths;
    }

    public Note(string noteText)
    {
        NoteText = noteText;
        //ImagePaths = imagePaths;
    }
}
