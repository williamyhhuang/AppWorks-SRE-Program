namespace SRE.Program.WebAPI.BusinessLogics.Common;

public class PostgresSettings
{
    public RDSSetting RDS { get; set; }

    public SSHSetting SSH { get; set; }
}

public class RDSSetting
{
    public string Host { get; set; }

    public int Port { get; set; }

    public string Database { get; set; }

    public string User { get; set; }

    public string Password { get; set; }
}

public class SSHSetting
{
    public string Host { get; set; }

    public int Port { get; set; }

    public string User { get; set; }

    public string KeyPath { get; set; }
}
