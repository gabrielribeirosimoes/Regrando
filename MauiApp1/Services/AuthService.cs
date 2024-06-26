using System;

public class AuthService
{
    private bool isAuthenticated;

    public bool IsAuthenticated
    {
        get { return isAuthenticated; }
        private set
        {
            isAuthenticated = value;
            OnAuthStateChanged?.Invoke(this, isAuthenticated);
        }
    }

    public event EventHandler<bool> OnAuthStateChanged;

    public AuthService()
    {
        IsAuthenticated = false;
    }

    public void Login()
    {
        IsAuthenticated = true;
    }

    public void Logout()
    {
        IsAuthenticated = false;
    }
}
