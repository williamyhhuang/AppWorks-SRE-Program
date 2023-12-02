namespace SRE.Program.WebAPI.DataAccess.Models;

public class Logistic
{
    public long logistic_id { get; set; }

    public long recipient_id { get; set; }

    public DateTime estimated_delivery { get; set; }
}
