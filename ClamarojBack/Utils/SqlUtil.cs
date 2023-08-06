using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml;

namespace ClamarojBack.Utils
{
    public class SqlUtil
    {
        //private readonly string connectionString;
        private readonly IConfiguration configuration;

        public SqlUtil(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<Dictionary<string, object>>> CallSqlFunctionDataAsync(string functionName, SqlParameter[]? parameters)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            List<Dictionary<string, object>> results = new();
            using SqlConnection connection = new(connectionString);
            using var command = connection.CreateCommand();
            var commandText = $"SELECT * FROM {functionName}(";

            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    commandText += $"{parameters[i].ParameterName},";
                }
                command.Parameters.AddRange(parameters);
            }
            commandText = commandText.TrimEnd(',') + ")";
            command.CommandText = commandText;
            command.CommandType = CommandType.Text;

            try
            {
                await connection.OpenAsync(); // Open the connection asynchronously
                using SqlDataReader reader = await command.ExecuteReaderAsync(); // Execute reader asynchronously

                while (await reader.ReadAsync()) // Read asynchronously
                {
                    Dictionary<string, object> row = new();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader.GetName(i), reader.GetValue(i));
                    }
                    results.Add(row);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return results;
        }

        public String CallSqlFunctionValue(string functionName, SqlParameter[]? parameters)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new(functionName, connection);
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            String result = "";

            try
            {
                connection.Open();
                result = command.ExecuteScalar().ToString()!;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }
        //Funcionando
        public async Task CallSqlProcedureAsync(string procedureName, SqlParameter[]? parameters)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            try
            {
                await connection.OpenAsync(); // Abrir la conexión de forma asíncrona
                await command.ExecuteNonQueryAsync(); // Ejecutar el comando de forma asíncrona
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
