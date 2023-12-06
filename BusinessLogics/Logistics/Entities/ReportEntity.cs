namespace SRE.Program.WebAPI.BusinessLogics.Logistics.Entities;

public class ReportEntity
{
    public DateTime created_at { get; set; }

    public Dictionary<string, int> trackingSummary { get; set; }
}
