using Npgsql;
using Renci.SshNet;

namespace SRE.Program.WebAPI.Extensions;

public class DatabaseConnectionExtension
{
    public static string GetConnectionString(IConfiguration configuration)
    {
        string host = configuration.GetSection("PostgresSettings:RDS:Host").Value;
        string port = configuration.GetSection("PostgresSettings:RDS:Port").Value;
        string database = configuration.GetSection("PostgresSettings:RDS:Database").Value;
        string user = configuration.GetSection("PostgresSettings:RDS:User").Value;
        string password = configuration.GetSection("PostgresSettings:RDS:Password").Value;

        // If using SSH tunneling
        bool useSshTunnel = true;
        string sshHost = configuration.GetSection("PostgresSettings:SSH:Host").Value;
        string sshPort = configuration.GetSection("PostgresSettings:SSH:Port").Value;
        string sshUser = configuration.GetSection("PostgresSettings:SSH:User").Value;
        string sshKeyPath = configuration.GetSection("PostgresSettings:SSH:KeyPath").Value;

        // Construct the connection string for Npgsql
        var c = $"Host={host};Port={port};Database={database};Username={user};Password={password};;SSL Mode=Require;Trust Server Certificate=true;";
        return c;
    }

    private static void SetupSshTunnel(string sshHost, int sshPort, string sshUsername, string sshPrivateKeyPath, string remoteHost, int remotePort, int localPort)
    {
        var keyFile = new PrivateKeyFile(sshPrivateKeyPath);
        var connectionInfo = new Renci.SshNet.ConnectionInfo(
    sshHost,
    sshPort,
    sshUsername,
    new AuthenticationMethod[]
    {
                // Key-based authentication
                new PrivateKeyAuthenticationMethod(sshUsername, keyFile)
    }
);

        using (var client = new SshClient(connectionInfo))
        {
            client.Connect();

            var portForwarded = new ForwardedPortLocal("localhost", (uint)localPort, remoteHost, (uint)remotePort);
            client.AddForwardedPort(portForwarded);

            portForwarded.Start();

            Console.WriteLine($"SSH Tunnel established. Local port: {localPort}, Remote host: {remoteHost}, Remote port: {remotePort}");

            // Note: Keep this tunnel open as long as you need to access the database

            // Don't forget to close the tunnel when done
            //Console.WriteLine("Press any key to close the tunnel...");
            //Console.ReadKey();

            //portForwarded.Stop();
            //client.Disconnect();
        }
    }
}
