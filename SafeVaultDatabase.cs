using Microsoft.Data.Sqlite;

public class SafeVaultDatabase
{

    public SafeVaultDatabase(string? databasePath = null)
{
    connectionString = databasePath == null
        ? "Data Source=safevault_test.db"
        : $"Data Source={databasePath}";
}
    private readonly string connectionString =
        "Data Source=safevault_test.db";

    public void CreateDatabase()
    {
        using var connection =
            new SqliteConnection(connectionString);

        connection.Open();

        string query = @"
            CREATE TABLE IF NOT EXISTS Users (
                UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                Username TEXT NOT NULL,
                Email TEXT NOT NULL
            );
        ";

        using var command = new SqliteCommand(query, connection);

        command.ExecuteNonQuery();
    }

    public void AddUser(string username, string email)
    {
        using var connection =
            new SqliteConnection(connectionString);

        connection.Open();

        string query = @"
            INSERT INTO Users (Username, Email)
            VALUES (@username, @email);
        ";

        using var command = new SqliteCommand(query, connection);

        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@email", email);

        command.ExecuteNonQuery();
    }

    public int FindUser(string username)
    {
        using var connection =
            new SqliteConnection(connectionString);

        connection.Open();

        string query = @"
            SELECT COUNT(*)
            FROM Users
            WHERE Username = @username;
        ";

        using var command = new SqliteCommand(query, connection);

        command.Parameters.AddWithValue("@username", username);

        return System.Convert.ToInt32(command.ExecuteScalar());
    }
    public void CloseDatabase()
{
    SqliteConnection.ClearAllPools();
}
}
