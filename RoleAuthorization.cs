
public class RoleAuthorization
{
    public bool CanAccessAdminDashboard(User? user)
    {
        if (user == null)
        {
            return false;
        }

        return user.Role == "Admin";
    }

    public bool CanAccessUserDashboard(User? user)
    {
        if (user == null)
        {
            return false;
        }

        return user.Role == "User" || user.Role == "Admin";
    }
}