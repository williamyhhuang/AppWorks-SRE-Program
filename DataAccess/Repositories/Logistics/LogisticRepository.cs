using SRE.Program.WebAPI.BusinessLogics.Logistics.Entities;
using SRE.Program.WebAPI.DataAccess.Models;

namespace SRE.Program.WebAPI.DataAccess.Repositories.Logistics;

public class LogisticRepository : ILogisticRepository
{
    private PosgresDbContext _posgresDbContext;

    public LogisticRepository(PosgresDbContext posgresDbContext)
    {
        this._posgresDbContext = posgresDbContext;
    }

    public IList<GetReturnModel> Get(long sn)
    {
        var result = from logistic in this._posgresDbContext.Logistics
                     join tracking in this._posgresDbContext.LogisticTrackings
                     on logistic.logistic_id equals tracking.logistic_id
                     join recipient in this._posgresDbContext.Recipients
                     on logistic.recipient_id equals recipient.recipient_id
                     join location in this._posgresDbContext.Locations
                     on tracking.location_id equals location.location_id
                     select new GetReturnModel
                     {
                         logistic_id = logistic.logistic_id,
                         estimated_delivery = logistic.estimated_delivery,
                         logistic_tracking_id = tracking.logistic_tracking_id,
                         arrive_date_time = tracking.arrive_datetime,
                         tracking_status = tracking.tracking_status,
                         location_id = tracking.location_id,
                         location_title = location.location_title,
                         recipient_id = logistic.recipient_id,
                         recipient_name = recipient.recipient_name,
                         recipient_address = recipient.recipient_address,
                         recipient_phone = recipient.recipient_phone
                     };

        return result.ToList();
    }

    public long InsertFakeData()
    {
        using var trans = this._posgresDbContext.Database.BeginTransaction();

        var rand = new Random();
        var num = rand.Next(1, 10);

        // 取得收件人
        var recipient = this.GetRandomRecipient();

        // 新增物流
        var logistic = new Logistic()
        {
            recipient_id = recipient.recipient_id,
            estimated_delivery = DateTime.Now.AddDays(num),
        };
        this.Insert(logistic);

        // 新增物流 tracking
        for (int i = 0; i < num; i++)
        {
            var n = rand.Next(1, 10);
            var arriveDateTime = DateTime.Now.AddDays(n);
            var locationId = this.GetRandomLocation().location_id;
            var status = this.RandomEnumValue<tracking_status_enum>().ToString();

            var tracking = new LogisticTracking
            {
                logistic_id = logistic.logistic_id,
                arrive_datetime = arriveDateTime,
                location_id = locationId,
                recipient_id = recipient.recipient_id,
                tracking_status = status
            };

            this.Insert(tracking);
        }

        trans.Commit();

        // 回傳物流編號
        return logistic.logistic_id;
    }

    public IList<LogisticTracking> Get(IList<long> snoList)
    {
        var result = from tracking in this._posgresDbContext.LogisticTrackings
                     where snoList.Contains(tracking.logistic_id)
                     select tracking;

        return result.ToList();
    }

    public Recipient GetRandomRecipient()
    {
        var randNum = new Random();

        var result = from r in this._posgresDbContext.Recipients
                     where r.recipient_id == randNum.Next(1, 10)
                     select r;

        return result.First();
    }

    public Location GetRandomLocation()
    {
        var randNum = new Random();

        var result = from l in this._posgresDbContext.Locations
                     where l.location_id == randNum.Next(1, 10)
                     select l;

        return result.First();
    }

    public void Insert(Logistic logistic)
    {
        this._posgresDbContext.Logistics.Add(logistic);
        this._posgresDbContext.SaveChanges();
    }

    public void Insert(LogisticTracking tracking)
    {
        this._posgresDbContext.LogisticTrackings.Add(tracking);
        this._posgresDbContext.SaveChanges();
    }

    private T RandomEnumValue<T>()
    {
        Random _R = new Random();
        var v = Enum.GetValues(typeof(T));

        return (T)v.GetValue(_R.Next(v.Length));
    }
}
