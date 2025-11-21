namespace OrderManagerLibrary.Model.Classes;
public class Note : INote
{
    public int NoteId { get; set; }
    public List<string> ImagePaths { get; set; }

    public Note(int noteId, string noteText, List<string> imagePaths)
    {
        NoteId = noteId;
        NoteText = noteText;
        ImagePaths = imagePaths;
    }
}
