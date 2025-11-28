using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class NoteRepository : IRepository<Note>
{
    private readonly IDataAccess _db;

    public NoteRepository(IDataAccess db)
    {
        _db = db;
    }

    public int Insert(Note entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spNote_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter outputParam = new("@NoteId", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            command.Parameters.AddWithValue("@NoteText", entity.NoteText);
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            command.Parameters.Add(outputParam);
            connection.Open();
            command.ExecuteNonQuery();

            return (int)outputParam.Value;
        }
    }

    public void Update(Note entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spNote_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@NoteId", entity.NoteId);
            command.Parameters.AddWithValue("@NoteText", entity.NoteText);
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spNote_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@NoteId", keyValues[0]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public Note GetById(params object[] keyValues)
    {
        Note note = null;

        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spNote_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@NoteId", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                note = new Note(
                    (int)reader["NoteId"],
                    (string)reader["NoteText"],
                    (int)reader["OrderId"]

                );
            }
            return note;
        }
    }

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
                    (int)reader["NoteId"],
                    (string)reader["NoteText"],
                    (int)reader["OrderId"]
                ));
            }
            return notes;
        }
    }
}
