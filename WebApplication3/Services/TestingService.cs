using System.Data.Common;
using System.Globalization;
using System.Transactions;
using test1template.Models;
using test1template.Services;
using Microsoft.Data.SqlClient;

namespace WorkshopApi.Services
{
    public class TestingService : ITestingService
    {
        private readonly string _connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";

        public async Task<SampleDTO?> GetSample(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            using var cmd = new SqlCommand("select * from Sample where Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = await cmd.ExecuteReaderAsync();
            
            if (!reader.Read()) return null;

            return new SampleDTO();
            // SQL queries and logic
            return null;
        }

        public async Task<string> AddSample(SampleDTO dto)
        {
            using var conn = new SqlConnection(_connectionString);
            await using SqlCommand command = new SqlCommand();
            command.Connection = conn;
            
            await conn.OpenAsync();

            DbTransaction transaction = await conn.BeginTransactionAsync();
            command.Transaction = transaction as SqlTransaction;
            try
            {

                //some code
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw;
            }
            


            // Logic to check constraints and insert
            return "Visit added successfully";
        }

        public async Task<string> DeleteSample(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            // Logic to delete
            return "Visit deleted successfully";
        }
    }
    
}
/*
 
 --------------
 Check if record exists by id
 _______________
    using var command = new SqlCommand("SELECT 1 FROM Sample WHERE idSample = @id", conn);
    command.Parameters.AddWithValue("@id", id);

    var exists = await command.ExecuteScalarAsync() is not null;
    if (!exists)
        throw new ArgumentException($"Sample with ID {id} does not exist.");
    
  
  --------------
 Check for duplicate
 _______________
    using var command = new SqlCommand("SELECT 1 FROM Sample WHERE id_sample = @id_sample AND id_extra_sample = @id_extra_sample", conn);
    command.Parameters.AddWithValue("@id_sample", dto.id_sample);
    command.Parameters.AddWithValue("@id_extra_sample", dto.id_extra_sample);

    if (await command.ExecuteScalarAsync() is not null)
        throw new ArgumentException("Sample is already registered."); 
    
    ------------------
    Check for valid foreign key
    -------------------
    using var command = new SqlCommand("SELECT 1 FROM Sample WHERE pesel = @pesel", conn);
    command.Parameters.AddWithValue("@pesel", dto.samplepesel);

    if (await mechCmd.ExecuteScalarAsync() is null)
    throw new ArgumentException("Sample with given pesel number not found.");
    
    ---------------
    Delete after checking for existense
    -----------------
    using var existsCmd = new SqlCommand("SELECT 1 FROM Visits WHERE IdVisit = @id", conn);
    existsCmd.Parameters.AddWithValue("@id", id);
    
    if (await existsCmd.ExecuteScalarAsync() is null)
        throw new ArgumentException("Visit not found.");
    
    using var deleteCmd = new SqlCommand("DELETE FROM Visits WHERE IdVisit = @id", conn);
    deleteCmd.Parameters.AddWithValue("@id", id);
    await deleteCmd.ExecuteNonQueryAsync();

    ----------------
    null safe read
    ----------------
    var date = reader["Date"] as DateTime?;
    var value = reader["Fee"] != DBNull.Value ? (decimal)reader["Fee"] : 0;

    
*/
