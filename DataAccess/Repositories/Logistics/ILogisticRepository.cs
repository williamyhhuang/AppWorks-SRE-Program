using SRE.Program.WebAPI.BusinessLogics.Logistics.Entities;
using SRE.Program.WebAPI.DataAccess.Models;

namespace SRE.Program.WebAPI.DataAccess.Repositories.Logistics;

public interface ILogisticRepository
{
    IList<GetReturnModel> Get(long sn);

    long InsertFakeData();

    IList<LogisticTracking> Get(IList<long> snoList);
}
