using SRE.Program.WebAPI.BusinessLogics.Caches;
using SRE.Program.WebAPI.BusinessLogics.Logistics.Entities;
using SRE.Program.WebAPI.DataAccess.Models;
using SRE.Program.WebAPI.DataAccess.Repositories.Logistics;

namespace SRE.Program.WebAPI.BusinessLogics.Logistics.Services;

public class LogisticService : ILogisticService
{
    private ILogger<LogisticService> _logger;

    private IRedisCacheService _redisCacheService;


    private ILogisticRepository _logisticRepository;

    public LogisticService(
        ILogger<LogisticService> logger,
        IRedisCacheService redisCacheService,
        ILogisticRepository logisticRepository)
    {
        this._logger = logger;
        this._redisCacheService = redisCacheService;
        this._logisticRepository = logisticRepository;
    }

    public DataEntity Query(long sn)
    {
        DataEntity? result;

        // 從 cache 拿
        result = this.GetData(sn);

        // 如果都沒有，則報錯
        if (result == null)
        {
            throw new ApplicationException("Tracking number not found");
        }

        return result;
    }

    public IList<DataEntity> Fake(int num)
    {
        IList<long> snoList = new List<long>();

        for (int i = 0; i < num; i++)
        {
            var sno = this._logisticRepository.InsertFakeData();

            snoList.Add(sno);
        }

        // 依物流編號查詢
        var trackings = this._logisticRepository.Get(snoList);
        var result = trackings.GroupBy(i => i.logistic_id)
                              .Select(i => i.OrderBy(x => x.arrive_datetime).Last())
                              .Select(i => new DataEntity
                              {
                                  sno = i.logistic_id,
                                  tracking_status = i.tracking_status
                              });

        return result.ToList();
    }

    private DataEntity? GetData(long sn)
    {
        DataEntity? result = null;

        // 從 cache 拿
        //result = _redisCacheService.Get<DataEntity>(sn.ToString());

        // 如果 cache 沒有，則從 DB 拿
        if (result == null)
        {
            var metaData = this._logisticRepository.Get(sn);

            if (metaData.Any())
            {
                result = this.ArrangeData(metaData);

                // 放進 redis 中
                //this._redisCacheService.Set(sn.ToString(), metaData);
            }
        }

        return result;
    }

    private DataEntity ArrangeData(IList<GetReturnModel> metaData)
    {
        return new DataEntity
        {
            sno = metaData.First().logistic_id,
            tracking_status = metaData.Last().tracking_status,
            estimated_delivery = metaData.Last().estimated_delivery,
            recipient = new RecipientEntity
            {
                id = metaData.Last().recipient_id,
                address = metaData.Last().recipient_address,
                phone = metaData.Last().recipient_phone,
                name = metaData.Last().recipient_name
            },
            details = metaData.Select(i => new DetailEntity
            {
                id = i.logistic_tracking_id,
                date = i.arrive_date_time,
                status = i.tracking_status,
                location_id = i.location_id,
                location_title = i.location_title
            }).ToList(),
            current_location = new LocationEntity
            {
                location_id = metaData.Last().location_id,
                title = metaData.Last().location_title
            }
        };
    }
}
