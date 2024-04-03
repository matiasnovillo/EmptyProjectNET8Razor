using FiyiStore.Areas.CMSCore.Entities;
using FiyiStore.Areas.CMSCore.DTOs;
using FiyiStore.Areas.BasicCore.DTOs;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2024
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStore.Areas.CMSCore.Interfaces
{
    public interface IRoleMenuRepository
    {
        public IQueryable<RoleMenu> AsQueryable();

        #region Queries
        public int Count();

        public RoleMenu? GetById(int roleId);

        public List<RoleMenu> GetAll();

        public paginatedRoleMenuDTO GetAllByRoleMenuIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);

        public List<MenuWithStateDTO> GetAllWithStateByRoleId(int roleId, List<Menu> lstMenu);

        public List<folderForDashboard> GetAllPagesAndFoldersForDashboardByRoleId(int roleId);

        public List<Menu> GetAllByRoleId(int roleId, List<Menu> lstMenu);

        public bool UpdateByRoleIdByMenuId(int roleId, int menuId, bool selected);
        #endregion

        #region Non-Queries
        public bool Add(RoleMenu roleMenu);

        public bool Update(RoleMenu roleMenu);

        public bool DeleteByRoleMenuId(int roleMenuId);

        public bool DeleteByMenuIdAndRoleId(int roleId, int menuId); 
        #endregion
    }
}
