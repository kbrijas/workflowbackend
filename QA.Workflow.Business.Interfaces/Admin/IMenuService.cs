using QA.Workflow.Business.Transfers.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QA.Workflow.Business.Interfaces.Admin
{
    public interface IMenuService
    {
        Task<List<MenuModel>> GetAllMenuAsync();
    }
}
