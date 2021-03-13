using AutoMapper;
using QA.Framework.DataEntities.Entities;
using QA.Workflow.Business.Interfaces.Admin;
using QA.Workflow.Business.Transfers.Admin;
using QA.Workflow.Business.Transfers.Shared;
using QA.Workflow.Data.Interfaces.Admin;
using QR.Workflow.Infrastructure.Repository;
using QR.Workflow.Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QA.Workflow.Business.Admin
{
    public class MenuService : IMenuService
    {
        private readonly IRepository<MenuMaster> _repositoryMenu;
        private readonly IMenuRepository _menuRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MenuService(IRepository<MenuMaster> repositoryMenu,
                           IMenuRepository menuRepo,
                           IUnitOfWork unitOfWork,
                           IMapper mapper)
        {
            _repositoryMenu = repositoryMenu;
            _menuRepo = menuRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<MenuModel>> GetAllMenuAsync()
        {
            var data = await _repositoryMenu.GetAllAsync();
            var menuList = data.Where(m => (bool)m.IsActive && m.ParentSeqNo == null);
            return _mapper.Map<List<MenuModel>>(data.Where(x => x.IsActive == true));
        }

        public async Task<List<MasterDataModel>> GetAllMenuDropDownAsync()
        {
            return await _menuRepo.GetAllMenuDropDownAsync();
        }
    }
}
