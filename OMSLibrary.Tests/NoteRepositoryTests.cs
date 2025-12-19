using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Tests;

[TestClass]
public sealed class NoteRepositoryTests
{
    private IRepository<Note> _noteRepository;
    private IConfiguration _config;
    private IDataAccess _db;

    [TestInitialize]
    public void Setup()
    {
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        _db = new DataAccess.DataAccess(_config);
        _noteRepository = new NoteRepository(_db);
    }

    [TestMethod]
    public void InsertNote_ShouldInsertSuccessfully()
    {
        // Arrange
        var note = new Note(0, "Test note");

        // Act
        int id = _noteRepository.Insert(note);

        // Assert
        var retrieved = _noteRepository.GetById(id);
        Assert.IsNotNull(retrieved);
        Assert.AreEqual(note.Content, retrieved.Content);
    }

    [TestMethod]
    public void UpdateNote_ShouldUpdateSuccessfully()
    {
        // Arrange
        var note = new Note(0, "Original note");
        int id = _noteRepository.Insert(note);
        var updatedNote = new Note(id, "Updated note text");

        // Act
        _noteRepository.Update(updatedNote);

        // Assert
        var retrieved = _noteRepository.GetById(id);
        Assert.IsNotNull(retrieved);
        Assert.AreEqual("Updated note text", retrieved.Content);
    }

    [TestMethod]
    public void DeleteNote_ShouldDeleteSuccessfully()
    {
        // Arrange
        var note = new Note(0, "Note to delete");
        int id = _noteRepository.Insert(note);
        Assert.IsNotNull(_noteRepository.GetById(id));

        // Act
        _noteRepository.Delete(id);

        // Assert
        Assert.IsNull(_noteRepository.GetById(id));
    }
}
