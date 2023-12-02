using Npgsql;

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

        // Construct the connection string
        NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder
        {
            Host = host,
            Port = int.Parse(port),
            Database = database,
            Username = user,
            Password = password
        };

        //if (useSshTunnel)
        //{
        //    builder.Enlist = false; // Important for Npgsql to work with SSH tunneling
        //    builder.Timeout = 15;

        //    // SSH tunnel options
        //    builder.SslMode = SslMode.Require;
        //    builder.TrustServerCertificate = true;

        //    // SSH connection options
        //    builder.Add("Tunnel", "true");
        //    builder.Add("TunnelHost", sshHost);
        //    builder.Add("TunnelPort", sshPort);
        //    builder.Add("TunnelUsername", sshUser);
        //    builder.Add("TunnelPrivateKeyPath", sshKeyPath);
        //}

        return builder.ToString();
    }
}
