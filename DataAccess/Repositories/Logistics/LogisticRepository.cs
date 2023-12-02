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
                     on tracking.location_Id equals location.location_id
                     select new GetReturnModel
                     {
                         logistic_id = logistic.logistic_id,
                         estimated_delivery = logistic.estimated_delivery,
                         logistic_tracking_id = tracking.logistic_tracking_id,
                         arrive_date_time = tracking.arrive_datetime,
                         tracking_status = tracking.tracking_status,
                         location_id = tracking.location_Id,
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

        // 取得收件人
        // 新增物流
        // 新增物流 tracking

        trans.Commit();

        // 回傳物流編號
        return 0;
    }

    public IList<LogisticTracking> Get(IList<long> snoList)
    {
        var result = from tracking in this._posgresDbContext.LogisticTrackings
                     where snoList.Contains(tracking.logistic_id)
                     select tracking;

        return result.ToList();
    }
}
