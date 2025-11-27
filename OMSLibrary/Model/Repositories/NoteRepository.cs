using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class NoteRepository : IRepository<Note>
{
    private readonly SqlConnection _connection;

    public NoteRepository(ISqlDataAccess sqlDataAccess)
    {
        _connection = sqlDataAccess.GetSqlConnection();
    }
    public int Insert(Note entity)
    {
        using SqlCommand command = new SqlCommand("spNote_Insert", _connection);
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter outputParam = new("@NoteId", SqlDbType.Int);
        outputParam.Direction = ParameterDirection.Output;
        command.Parameters.AddWithValue("@NoteText", entity.NoteText);
        command.Parameters.AddWithValue("@OrderId", entity.OrderId);
        command.Parameters.Add(outputParam);

        _connection.Open();
        command.ExecuteNonQuery();
       

        return (int)outputParam.Value;
    }
    

    public void Update(Note entity)
    {
        using SqlCommand command = new SqlCommand("spNote_Update", _connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@NoteId", entity.NoteId);
        command.Parameters.AddWithValue("@NoteText", entity.NoteText);
        command.Parameters.AddWithValue("@OrderId", entity.OrderId);
        

        _connection.Open();
        command.ExecuteNonQuery();
    }   
    public void Delete(int id)
    {
        using SqlCommand command = new SqlCommand("spNote_Delete", _connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@NoteId", id);

        _connection.Open();
        command.ExecuteNonQuery();
    }

 
    public Note GetById(int id)
    {
        Note note = null;

        using SqlCommand command = new SqlCommand("spNote_GetById", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@NoteId", id);

        _connection.Open();
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
    

    public IEnumerable<Note> GetAll()
    {
        var notes = new List<Note>();

        using SqlCommand command = new("spNote_GetAll", _connection);
        command.CommandType = CommandType.StoredProcedure;

        _connection.Open();
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



