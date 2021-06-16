using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CustomerLibrary.Data
{
    public class NoteRepository : BaseRepository
    {
        public int Create(Note note)
        {
            using var connection = GetConnection();

            return Create(note, connection);
        }

        public int Create(Note note, SqlConnection connection, SqlTransaction transaction = null)
        {
            var newNoteId = 0;

            var sql = @"INSERT INTO [dbo].[Notes] (
		                    [CustomerID],
		                    [Note]
	                    ) VALUES (
		                    @CustomerID,
		                    @Note
	                    );
                        SELECT CAST(scope_identity() AS int)";

            var command = new SqlCommand(sql, connection, transaction);

            var customerIdParam = new SqlParameter("@CustomerID", SqlDbType.Int)
            {
                Value = note.CustomerId
            };

            var noteParam = new SqlParameter("@Note", SqlDbType.NVarChar, 500)
            {
                Value = note.NoteText
            };

            command.Parameters.Add(customerIdParam);
            command.Parameters.Add(noteParam);

            var response = command.ExecuteScalar();
            if (response is not null)
            {
                newNoteId = (int) response;
            }

            note.NoteId = newNoteId;

            return newNoteId;
        }

        public Note Read(int noteId)
        {
            using var connection = GetConnection();

            return Read(noteId, connection);
        }

        public Note Read(int noteId, SqlConnection connection, SqlTransaction transaction = null)
        {
            var sql = @"SELECT * FROM [dbo].[Notes]
	                    WHERE [NoteID] = @NoteID";

            var command = new SqlCommand(sql, connection, transaction);

            var noteIdParam = new SqlParameter("@NoteID", SqlDbType.Int)
            {
                Value = noteId
            };

            command.Parameters.Add(noteIdParam);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Note
                    {
                        NoteId = (int) reader["NoteID"],
                        CustomerId = (int) reader["CustomerID"],
                        NoteText = reader["Note"]?.ToString()
                    };
                }
            }

            return null;
        }

        public List<Note> ReadByCustomerId(int customerId)
        {
            using var connection = GetConnection();

            return ReadByCustomerId(customerId, connection);
        }

        public List<Note> ReadByCustomerId(int customerId, SqlConnection connection, SqlTransaction transaction = null)
        {
            var sql = @"SELECT * FROM [dbo].[Notes]
	                    WHERE [CustomerID] = @CustomerID";

            var command = new SqlCommand(sql, connection, transaction);

            var customerIdParam = new SqlParameter("@CustomerID", SqlDbType.Int)
            {
                Value = customerId
            };

            command.Parameters.Add(customerIdParam);

            var notes = new List<Note>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    notes.Add(new Note
                    {
                        NoteId = (int)reader["NoteID"],
                        CustomerId = (int)reader["CustomerID"],
                        NoteText = reader["Note"]?.ToString()
                    });
                }
            }

            return notes;
        }

        public void Update(Note note)
        {
            using var connection = GetConnection();

            Update(note, connection);
        }

        public void Update(Note note, SqlConnection connection, SqlTransaction transaction = null)
        {
            var sql = @"UPDATE [dbo].[Notes]
	                    SET [CustomerID] = @CustomerID,
		                    [Note] = @Note
	                    WHERE [NoteID] = @NoteID";

            var command = new SqlCommand(sql, connection, transaction);

            var noteIdParam = new SqlParameter("@NoteID", SqlDbType.Int)
            {
                Value = note.NoteId
            };

            var customerIdParam = new SqlParameter("@CustomerID", SqlDbType.Int)
            {
                Value = note.CustomerId
            };

            var noteParam = new SqlParameter("@Note", SqlDbType.NVarChar, 100)
            {
                Value = note.NoteText
            };

            command.Parameters.Add(noteIdParam);
            command.Parameters.Add(customerIdParam);
            command.Parameters.Add(noteParam);

            command.ExecuteNonQuery();
        }

        public void Delete(int noteId)
        {
            using var connection = GetConnection();

            Delete(noteId, connection);
        }

        public void Delete(int noteId, SqlConnection connection, SqlTransaction transaction = null)
        {
            var sql = @"DELETE FROM [dbo].[Notes]
	                    WHERE [NoteID] = @NoteID";

            var noteIdParam = new SqlParameter("@NoteID", SqlDbType.Int)
            {
                Value = noteId
            };

            var command = new SqlCommand(sql, connection, transaction);

            command.Parameters.Add(noteIdParam);

            command.ExecuteNonQuery();
        }

        public void DeleteAll()
        {
            using var connection = GetConnection();

            DeleteAll(connection);
        }

        public void DeleteAll(SqlConnection connection, SqlTransaction transaction = null)
        {
            var sql = @"DELETE FROM [dbo].[Notes]";

            var command = new SqlCommand(sql, connection, transaction);

            command.ExecuteNonQuery();
        }
    }
}