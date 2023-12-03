namespace SRE.Program.WebAPI.DataAccess.Models;

public class LogisticTracking
{
    public long logistic_tracking_id { get; set; }

    public long logistic_id { get; set; }

    public DateTime arrive_datetime { get; set; }

    public long recipient_id { get; set; }

    public long location_id { get; set; }

    public string tracking_status { get; set; }
}
