using AxiAuth;

class Program
{
    public static api auth = new api(
        name: "name",
        ownerid: "ownerid",
        secret: "yoursecret",
        version: "1.0"
    );

    static void Main(string[] args)
    {
        Console.WriteLine("Connecting to auth server...");
        auth.init();
        auth.fetchStats();

        var serverVersion = string.IsNullOrEmpty(auth.app_data.version)
            ? "(unknown)"
            : auth.app_data.version;

        Console.WriteLine($"App: {auth.name} | server version: {serverVersion} | client: {auth.version}");

        ShowMenu();
    }

    static void ShowMenu()
    {
        Console.WriteLine("\n─────────────────────────────────────");
        Console.WriteLine("  1. Login");
        Console.WriteLine("  2. Register");
        Console.WriteLine("  3. License Key");
        Console.WriteLine("─────────────────────────────────────");
        Console.Write("Choose option: ");

        string choice = Console.ReadLine()?.Trim();

        switch (choice)
        {
            case "1": HandleLogin(); break;
            case "2": HandleRegister(); break;
            case "3": HandleLicense(); break;
            default:
                Console.WriteLine("Invalid option.");
                ShowMenu();
                break;
        }
    }

    static void HandleLogin()
    {
        Console.Write("\nUsername: ");
        string username = Console.ReadLine()?.Trim();

        Console.Write("Password: ");
        string password = ReadPassword();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Username/password cannot be empty.");
            Console.ReadKey();
            return;
        }

        auth.login(username, password);

        if (!auth.response.success)
        {
            auth.log("login_failed");
            Console.WriteLine("Failed: " + auth.response.message);
            Console.ReadKey();
            return;
        }

        auth.log("login_success");
        Console.WriteLine("Login successful: " + auth.response.message);

        StartApp();
    }

    static void HandleRegister()
    {
        Console.Write("\nUsername: ");
        string username = Console.ReadLine()?.Trim();

        Console.Write("Password: ");
        string password = ReadPassword();

        Console.Write("License Key: ");
        string key = Console.ReadLine()?.Trim();

        Console.Write("Email (optional, press Enter to skip): ");
        string email = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(key))
        {
            Console.WriteLine("Username, password, and key cannot be empty.");
            Console.ReadKey();
            return;
        }

        auth.register(username, password, key ?? "");

        if (!auth.response.success)
        {
            auth.log("register_failed");
            Console.WriteLine("Failed: " + auth.response.message);
            Console.ReadKey();
            return;
        }

        auth.log("register_success");
        Console.WriteLine("Register successful: " + auth.response.message);
        Console.WriteLine("You can now login with your credentials.");
        Console.ReadKey();
        ShowMenu();
    }

    static void HandleLicense()
    {
        Console.Write("\nEnter License Key: ");
        string licenseKey = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(licenseKey))
        {
            Console.WriteLine("License key cannot be empty.");
            Console.ReadKey();
            return;
        }

        auth.license(licenseKey);

        if (!auth.response.success)
        {
            auth.log("license_failed");
            Console.WriteLine("Failed: " + auth.response.message);
            Console.ReadKey();
            return;
        }

        auth.log("license_success");
        Console.WriteLine("License activated: " + auth.response.message);

        if (!string.IsNullOrEmpty(auth.user_data.username))
            Console.WriteLine($"User: {auth.user_data.username}");

        StartApp();
    }

    static void StartApp()
    {
        Console.WriteLine("\n✓ Authentication successful! Starting app...");
        Console.WriteLine("─────────────────────────────────────");
        Console.WriteLine("Hello from your protected app!");
        Console.ReadKey();
    }



    

    static string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password[..^1];
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return password;
    }
}