namespace OrderManagerLibrary.Model.Interfaces;

public interface INote
{
    int NoteId { get; set; }
    string NoteText { get; set; }
    int OrderId { get; set; }
}
