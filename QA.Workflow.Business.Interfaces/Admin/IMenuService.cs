using QA.Workflow.Business.Transfers.Admin;
using QA.Workflow.Business.Transfers.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QA.Workflow.Business.Interfaces.Admin
{
    public interface IMenuService
    {
        Task<List<MenuModel>> GetAllMenuAsync();
        Task<List<MasterDataModel>> GetAllMenuDropDownAsync();
    }
}
