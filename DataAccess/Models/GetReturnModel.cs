using System.Security.Permissions;

namespace SRE.Program.WebAPI.DataAccess.Models;

public class GetReturnModel
{
    public long logistic_id { get; set; }

    public DateTime estimated_delivery { get; set; }

    public long logistic_tracking_id { get; set; }

    public DateTime arrive_date_time { get; set; }

    public string tracking_status { get; set; }

    public long location_id { get; set; }

    public string location_title { get; set; }

    public long recipient_id { get; set; }

    public string recipient_name { get; set; }

    public string recipient_address { get; set; }

    public string recipient_phone { get; set; }
}
