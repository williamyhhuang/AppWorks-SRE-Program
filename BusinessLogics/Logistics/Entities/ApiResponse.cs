namespace SRE.Program.WebAPI.BusinessLogics.Logistics.Entities;

public class ApiResponse
{
    public string status { get; set; } = null!;

    public object? data { get; set; }

    public ErrorEntity? error { get; set; }
}

public class DataEntity
{
    public long sno { get; set; }

    public string tracking_status { get; set; }

    public DateTime estimated_delivery { get; set; }

    public IList<DetailEntity> details { get; set; }

    public RecipientEntity recipient { get; set; }

    public LocationEntity current_location { get; set; }
}

public class DetailEntity
{
    public long id { get; set; }

    public DateTime date { get; set; }

    public DateTime time { get; set; }

    public string status { get; set; }

    public long location_id { get; set; }

    public string location_title { get; set; }
}

public class RecipientEntity
{
    public long id { get; set; }

    public string name { get; set; }

    public string address { get; set; }

    public string phone { get; set; }
}

public class LocationEntity
{
    public long location_id { get; set; }

    public string title { get; set; }

    public string city { get; set; }

    public string address { get; set; }
}

public class ErrorEntity
{
    public int code { get; set; }

    public string message { get; set; }
}