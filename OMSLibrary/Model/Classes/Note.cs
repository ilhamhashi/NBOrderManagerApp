namespace OrderManagerLibrary.Model.Classes;
public class Note
{
    public int NoteId { get; set; }
    public string NoteText { get; set; }
    public List<string> ImagePaths { get; set; }
}
