using QA.Workflow.Business.Transfers.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QA.Workflow.Data.Interfaces.Admin
{
    public interface IMenuRepository
    {
        Task<List<MasterDataModel>> GetAllMenuDropDownAsync();
    }
}
