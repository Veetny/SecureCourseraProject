using MySql.Data.MySqlClient;

public class UserRepository
{
    private readonly string connectionString;

    public UserRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public bool UsernameExists(string username)
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT COUNT(*) FROM Users WHERE Username = @username";

            using (var command = new MySqlCommand(query, connection))
            {
                // Username passes safely
                command.Parameters.AddWithValue("@username", username);

                int result = System.Convert.ToInt32(
                    command.ExecuteScalar()
                );

                return result > 0;
            }
        }
    }
}