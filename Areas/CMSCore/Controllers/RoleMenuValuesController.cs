using FiyiStore.Areas.BasicCore.Interfaces;
using FiyiStore.Areas.CMSCore.Filters;
using FiyiStore.Areas.CMSCore.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using FiyiStore.Areas.BasicCore.Entities;
using FiyiStore.Areas.CMSCore.DTOs;
using FiyiStore.Areas.CMSCore.Repositories;
using FiyiStore.Areas.CMSCore.Entities;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2022
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStore.Areas.CMSCore.Controllers
{
    [ApiController]
    [RoleMenuFilter]
    public class RoleMenuValuesController : ControllerBase
    {
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly IFailureRepository _failureRepository;
        private readonly IRoleMenuRepository _roleMenuRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMenuRepository _menuRepository;

        public RoleMenuValuesController(IWebHostEnvironment WebHostEnvironment,
            IFailureRepository failureRepository,
            IRoleMenuRepository roleMenuRepository,
            IRoleRepository roleRepository,
            IMenuRepository menuRepository)
        {
            _WebHostEnvironment = WebHostEnvironment;
            _failureRepository = failureRepository;
            _roleMenuRepository = roleMenuRepository;
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
        }

        [HttpGet("~/api/CMSCore/RoleMenu/1/ChooseMenuesByRoleId/{RoleId:int}")]
        public List<MenuWithStateDTO> ChooseMenuesByRoleId(int RoleId)
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                List<Menu> lstAllMenu = _menuRepository
                                            .GetAll();

                return _roleMenuRepository
                            .GetAllWithStateByRoleId(RoleId, lstAllMenu);
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                Failure Failure = new()
                {
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = HttpContext.Session.GetInt32("UserId") ?? 1,
                    UserLastModificationId = HttpContext.Session.GetInt32("UserId") ?? 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now,
                };
                _failureRepository.Add(Failure);
                return null;
            }
        }

        [HttpPost("~/api/CMSCore/RoleMenu/1/InsertPermissions/")]

        public IActionResult InsertPermissions()
        {
            try
            {
                int RoleId = Convert.ToInt32(HttpContext.Request.Form["RoleId"]);

                int i = 0;
                bool[] Selected = new bool[HttpContext.Request.Form["IsSelected"].Count];
                int[] MenuId = new int[HttpContext.Request.Form["MenuId"].Count];

                foreach (var selected in HttpContext.Request.Form["IsSelected"])
                {
                    Selected[i] = Convert.ToBoolean(selected);
                    i += 1;
                }

                i = 0;
                foreach (var menuId in HttpContext.Request.Form["MenuId"])
                {
                    MenuId[i] = Convert.ToInt32(menuId);
                    i += 1;
                }

                for (i = 0; i < MenuId.Length; i++)
                {
                    _roleMenuRepository.UpdateByRoleIdByMenuId(RoleId, MenuId[i], Selected[i]);
                }

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                Failure Failure = new()
                {
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = HttpContext.Session.GetInt32("UserId") ?? 1,
                    UserLastModificationId = HttpContext.Session.GetInt32("UserId") ?? 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now,
                };
                _failureRepository.Add(Failure);
                return StatusCode(500, ex);
            }
        }
    }
}