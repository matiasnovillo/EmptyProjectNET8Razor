using FiyiStore.Areas.BasicCore.DTOs;
using FiyiStore.Areas.CMSCore.Entities;
using FiyiStore.Areas.CMSCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FiyiStore.Areas.CMSCore.Pages
{
    public class DashboardModel : PageModel
    {
        public IUserRepository _userRepository;
        public IRoleMenuRepository _roleMenuRepository;

        public DashboardModel(IUserRepository userRepository,
            IRoleMenuRepository roleMenuRepository)
        {
            _userRepository = userRepository;
            _roleMenuRepository = roleMenuRepository;
        }

        public void OnGet()
        {
            int UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            User user = _userRepository.GetByUserId(UserId);

            ViewData["Email"] = user.Email;

            List<folderForDashboard> lstFoldersAndPages = _roleMenuRepository
                                                                .GetAllPagesAndFoldersForDashboardByRoleId(user.RoleId);

            if (lstFoldersAndPages != null)
            {
                foreach (folderForDashboard folderandpages in lstFoldersAndPages)
                {
                    ViewData["FoldersAndPagesDashboard"] += $@"
<h6 class=""mt-4"">
    <i class=""fas fa-folder""></i>&nbsp;
    {folderandpages.Folder.Name}
    </h6>
";



                    ViewData["FoldersAndPagesSideNav"] += $@"
<li class=""nav-item mb-1 mt-4 mx-4"">
    <span class=""text-white ms-2 ps-1"">
        <i class=""fas fa-folder""></i>&nbsp;
        {folderandpages.Folder.Name}
    </span>
</li>";

                    foreach (itemForFolderForDashboard item in folderandpages.Pages)
                    {
                        ViewData["FoldersAndPagesDashboard"] += $@"
<a class=""btn bg-gradient-dark mx-1 my-1""
    href=""{item.URLPath}"">
        <i class=""fas fa-file""></i>&nbsp;
        {item.Name}
</a>"
;

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
        }
    }
}
