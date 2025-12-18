using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
/// <summary>
/// Repository responsible for handling database operations related to Notes.
/// Provides CRUD functionality using stored procedures.
/// </summary>
public class NoteRepository : IRepository<Note>
{
    private readonly IDataAccess _db;

    /// <summary>
    /// Initializes the repository with access to the database connection provider.
    /// Uses dependency injection for improved testability.
    /// </summary>
    public NoteRepository(IDataAccess db)
    {
        _db = db;
    }

    /// <summary>
    /// Inserts a new Note into the database and returns the generated NoteId.
    /// </summary>
    public int Insert(Note entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spNote_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter outputParam = new("@Id", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            command.Parameters.AddWithValue("@Content", entity.Content);
            command.Parameters.Add(outputParam);
            connection.Open();
            command.ExecuteNonQuery();

            return (int)outputParam.Value;
        }
    }

    /// <summary>
    /// Updates an existing note in the database.
    /// </summary>
    public void Update(Note entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spNote_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Content", entity.Content);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Deletes a note (NoteId required).
    /// </summary>
    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spNote_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", keyValues[0]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Retrieves a single note by NoteId.
    /// Returns null if no record exists.
    /// </summary>
    public Note GetById(params object[] keyValues)
    {
        Note note = null;

        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spNote_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                note = new Note(
                    (int)reader["Id"],
                    (string)reader["Content"]);
            }
            return note;
        }
    }

    /// <summary>
    /// Retrieves all notes from the database.
    /// </summary>
    public IEnumerable<Note> GetAll()
    {
        var notes = new List<Note>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spNote_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                notes.Add(new Note(
                    (int)reader["Id"],
                    (string)reader["Content"]
                ));
            }
            return notes;
        }
    }
}
