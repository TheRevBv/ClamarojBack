using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClamarojBack.Utils
{
    public class SqlUtil
    {
        private readonly string connectionString;

        public SqlUtil()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            this.connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public DataTable CallSqlFunctionData(string functionName, SqlParameter[]? parameters)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new(functionName, connection);
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            DataTable dataTable = new();

            try
            {
                connection.Open();
                SqlDataAdapter adapter = new(command);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return dataTable;
        }

        public String CallSqlFunctionValue(string functionName, SqlParameter[]? parameters)
        {
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

        public void CallSqlProcedure(string procedureName, SqlParameter[]? parameters)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = new(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
