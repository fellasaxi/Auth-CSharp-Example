# AXIAuth - C# Authentication Example

A simple C# console application demonstrating how to integrate **AxiAuth** authentication into your .NET project.

## Features

- 🔐 **Login** — Authenticate with username and password
- 📝 **Register** — Create a new account with a license key
- 🔑 **License Key** — Activate directly using a license key
- ⚡ **Auto-Login** — Automatically login on startup using saved credentials
- 👁️ **Password Masking** — Secure password input

## Requirements

- .NET 6.0 or higher
- AxiAuth library

## Usage

1. Clone the repository
```bash
   git clone https://github.com/fellasaxi/Auth-CSharp-Example.git
```
2. Open the solution in Visual Studio
3. Build and run the project

## Configuration

Edit the credentials in `Program.cs`:
```csharp
public static api auth = new api(
    name: "YOUR_APP_NAME",
    ownerid: "YOUR_OWNER_ID",
    secret: "YOUR_SECRET",
    version: "1.0"
);
```

## License

This project is for educational purposes only.
