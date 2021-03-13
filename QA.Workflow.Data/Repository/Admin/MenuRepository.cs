using Dapper;
using QA.Workflow.Business.Transfers.Shared;
using QA.Workflow.Data.Interfaces.Admin;
using QR.Workflow.Infrastructure.Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QA.Workflow.Data.Repository.Admin
{
    public class MenuRepository : IMenuRepository
    {
        private IDapperExtension _dapperExtension;

        public MenuRepository(IDapperExtension dapperExtension)
        {
            _dapperExtension = dapperExtension;
        }

        public async Task<List<MasterDataModel>> GetAllMenuDropDownAsync()
        {
            DynamicParameters perameters = new DynamicParameters();
            return await _dapperExtension.ExecuteNonQueryReaderAsync<MasterDataModel>("spGetMenuDropdown", perameters);
        }
    }
}
