using SRE.Program.WebAPI.BusinessLogics.Logistics.Entities;

namespace SRE.Program.WebAPI.BusinessLogics.Logistics.Services;

public interface ILogisticService
{
    DataEntity Query(long sn);

    IList<DataEntity> Fake(int num);
}
