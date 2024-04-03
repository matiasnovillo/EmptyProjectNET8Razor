using FiyiStore.Areas.BasicCore.DTOs;
using FiyiStore.Areas.CMSCore.Entities;
using FiyiStore.Areas.CMSCore.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FiyiStore.Areas.CMSCore.Pages
{
    public class PermissionsModel : PageModel
    {
        public IRoleRepository _roleRepository;
        public IUserRepository _userRepository;
        public IRoleMenuRepository _roleMenuRepository;

        public PermissionsModel(IRoleRepository roleRepository,
            IUserRepository userRepository,
            IRoleMenuRepository roleMenuRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _roleMenuRepository = roleMenuRepository;
        }

        public void OnGet()
        {
            List<Role> lstRole = _roleRepository.GetAll();

            if (lstRole != null)
            {
                foreach (Role role in lstRole)
                {
                    ViewData["Roles"] += $@"
    <a class=""btn btn-info role-a"" 
    href=""javascript:void(0)"">
        {role.Name}
    </a>
    <input type=""hidden"" value=""{role.RoleId}""/>
                    ";
                }
            }

            #region Show folders and pages
            int UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            User user = _userRepository.GetByUserId(UserId);

            ViewData["Email"] = user.Email;

            List<folderForDashboard> lstFoldersAndPages = _roleMenuRepository
                                                                .GetAllPagesAndFoldersForDashboardByRoleId(user.RoleId);

            if (lstFoldersAndPages != null)
            {
                foreach (folderForDashboard folderandpages in lstFoldersAndPages)
                {
                    ViewData["FoldersAndPagesSideNav"] += $@"
<li class=""nav-item mb-1 mt-4 mx-4"">
    <span class=""text-white ms-2 ps-1"">
        <i class=""fas fa-folder""></i>&nbsp;
        {folderandpages.Folder.Name}
    </span>
</li>";

                    foreach (itemForFolderForDashboard item in folderandpages.Pages)
                    {
                        ViewData["FoldersAndPagesSideNav"] += $@"
<li class=""nav-item mx-6"">
    <a class=""btn-link text-white btn-sm""
        href=""{item.URLPath}"">
        <i class=""fas fa-file""></i>&nbsp;
        {@item.Name}
    </a>
</li>";
                    }
                }
            } 
            #endregion
        }
    }
}
