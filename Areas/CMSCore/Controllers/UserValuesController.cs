using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FiyiStore.Areas.CMSCore.Filters;
using FiyiStore.Areas.CMSCore.Interfaces;
using FiyiStore.Library;
using FiyiStore.Areas.CMSCore.Entities;
using FiyiStore.Areas.BasicCore.Entities;
using FiyiStore.Areas.BasicCore.Interfaces;
using FiyiStore.Areas.CMSCore.DTOs;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2023
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStore.Areas.CMSCore.Controllers
{
    [ApiController]
    [UserFilter]
    public partial class UserValuesController : ControllerBase
    {
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IFailureRepository _failureRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        
        public UserValuesController(IWebHostEnvironment WebHostEnvironment, 
            IConfiguration configuration,
            IFailureRepository failureRepository,
            IUserRepository userRepository,
            IUserService userService) 
        {
            _WebHostEnvironment = WebHostEnvironment;
            _configuration = configuration;
            _failureRepository = failureRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        #region Queries
        [HttpGet("~/api/CMSCore/User/1/GetByUserId/{UserId:int}")]
        public User GetByUserId(int UserId)
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                return _userRepository.GetByUserId(UserId);
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

        [HttpGet("~/api/CMSCore/User/1/GetAll")]
        public List<User> GetAll()
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                return _userRepository.GetAll();
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

        [HttpGet("~/api/CMSCore/User/1/GetAllPaginated")]
        public paginatedUserDTO GetAllPaginated([FromBody] paginatedUserDTO paginatedUserDTO)
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                return _userRepository.GetAllByUserIdPaginated(paginatedUserDTO.TextToSearch,
                                            paginatedUserDTO.IsStrictSearch,
                                            paginatedUserDTO.PageIndex,
                                            paginatedUserDTO.PageSize);
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
        #endregion

        #region Non-Queries
        [HttpPost("~/api/CMSCore/User/1/AddOrUpdate")]
        public IActionResult InsertOrUpdateAsync()
        {
            try
            {
                //Get user from session
                int UserIdSession = HttpContext.Session.GetInt32("UserId") ?? 0;

                if(UserIdSession == 0)
                {
                    return StatusCode(401, "Usuario no encontrado en sesión");
                }
                
                #region Pass data from client to server
                //UserId
                int UserId = Convert.ToInt32(HttpContext.Request.Form["cmscore-user-userid-input"]);
                
                string FantasyName = HttpContext.Request.Form["cmscore-user-fantasyname-input"];
                string Email = HttpContext.Request.Form["cmscore-user-email-input"];
                string Password = "";
                if (HttpContext.Request.Form["cmscore-user-password-input"] != "")
                {
                    Password = Security.EncodeString(HttpContext.Request.Form["cmscore-user-password-input"]); 
                }
                int RoleId = 0; 
                if (Convert.ToInt32(HttpContext.Request.Form["cmscore-user-roleid-input"]) != 0)
                {
                    RoleId = Convert.ToInt32(HttpContext.Request.Form["cmscore-user-roleid-input"]);
                }
                else
                { return StatusCode(400, "It's not allowed to save zero values in RoleId"); }
                string RegistrationToken = HttpContext.Request.Form["cmscore-user-registrationtoken-input"];
                
                #endregion

                if (UserId == 0)
                {
                    //Insert
                    User user = new()
                    {
                        Active = true,
                        UserCreationId = UserIdSession,
                        UserLastModificationId = UserIdSession,
                        DateTimeCreation = DateTime.Now,
                        DateTimeLastModification = DateTime.Now,
                        Email = Email,
                        Password = Password,
                        RoleId = RoleId
                    };
                    
                    _userRepository.Add(user);
                }
                else
                {
                    //Update
                    User user = _userRepository.GetByUserId(UserId);

                    user.UserLastModificationId = UserId;
                    user.DateTimeLastModification = DateTime.Now;
                    user.Email = Email;
                    user.Password = Password;
                    user.RoleId = RoleId;
                                       
                    _userRepository.Update(user);
                }
                

                //Look for sent files
                if (HttpContext.Request.Form.Files.Count != 0)
                {
                    int i = 0; //Used to iterate in HttpContext.Request.Form.Files
                    foreach (var File in Request.Form.Files)
                    {
                        if (File.Length > 0)
                        {
                            var FileName = HttpContext.Request.Form.Files[i].FileName;
                            var FilePath = $@"{_WebHostEnvironment.WebRootPath}/Uploads/CMSCore/User/";

                            using (var FileStream = new FileStream($@"{FilePath}{FileName}", FileMode.Create))
                            {
                                
                                File.CopyToAsync(FileStream); // Read file to stream
                                byte[] array = new byte[FileStream.Length]; // Stream to byte array
                                FileStream.Seek(0, SeekOrigin.Begin);
                                FileStream.Read(array, 0, array.Length);
                            }

                            i += 1;
                        }
                    }
                }

                return StatusCode(200, "OK");
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
                return StatusCode(500, ex.Message);
            }
        }

        //[Produces("text/plain")] For production mode, uncomment this line
        [HttpDelete("~/api/CMSCore/User/1/DeleteByUserId/{UserId:int}")]
        public IActionResult DeleteByUserId(int UserId)
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                _userRepository.DeleteByUserId(UserId);

                return StatusCode(200, "OK");
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
                return StatusCode(500, ex.Message);
            }
        }

        //[Produces("text/plain")] For production mode, uncomment this line
        [HttpPost("~/api/CMSCore/User/1/CopyByUserId/{UserId:int}")]
        public IActionResult CopyByUserId(int UserId)
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                _userRepository.CopyByUserId(UserId);

                return StatusCode(200, "OK");
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("~/api/CMSCore/User/1/Login")]
        public IActionResult Login()
        {
            try
            {
                string Email = HttpContext.Request.Form["email"];
                string Password = HttpContext.Request.Form["password"];

                Password = Security.EncodeString(Password);

                User userToLogin = _userRepository.GetByEmailAndPassword(Email, Password);

                if (userToLogin.UserId != 0)
                {
                    HttpContext.Session.SetInt32("UserId", userToLogin.UserId);
                    return StatusCode(200, "/CMSCore/Dashboard");
                }
                else
                {
                    return StatusCode(200, "Usuario no encontrado");
                }
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
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("~/api/CMSCore/User/1/Logout")]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return StatusCode(200, "/Index");
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
                return StatusCode(500, ex.Message);
            }

        }
        #endregion

        #region Other actions
        [HttpPost("~/api/CMSCore/User/1/ExportAsPDF")]
        public IActionResult ExportAsPDF()
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                DateTime Now = _userService.ExportAsPDF();

                return StatusCode(200, new Ajax() { AjaxForString = Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") });
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("~/api/CMSCore/User/1/ExportAsExcel")]
        public IActionResult ExportAsExcel()
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                DateTime Now = _userService.ExportAsExcel();

                return StatusCode(200, new Ajax() { AjaxForString = Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") });
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
                return StatusCode(500, ex.Message);
            }
        }

        //[Produces("text/plain")] For production mode, uncomment this line
        [HttpPost("~/api/CMSCore/User/1/ExportAsCSV")]
        public IActionResult ExportAsCSV()
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                DateTime Now = _userService.ExportAsCSV();

                return StatusCode(200, new Ajax() { AjaxForString = Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") });
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
                return StatusCode(500, ex.Message);
            }
        }
        #endregion
    }
}