using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using FiyiStore.Areas.BasicCore.Entities;
using FiyiStore.Areas.BasicCore.Interfaces;
using FiyiStore.Areas.FiyiStore.DTOs;
using FiyiStore.Areas.FiyiStore.Filters;
using FiyiStore.Areas.FiyiStore.Interfaces;
using FiyiStore.Areas.FiyiStore.Entities;
using FiyiStore.Library;
using MediatR;
using FiyiStore.Areas.FiyiStore.Actions.GetAll;

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

namespace FiyiStore.Areas.FiyiStore.Controllers
{
    [ApiController]
    [ClientFilter]
    public partial class ClientValuesController : ControllerBase
    {
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IFailureRepository _failureRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IClientService _clientService;
        private readonly IMediator _mediator;

        public ClientValuesController(IWebHostEnvironment WebHostEnvironment,
            IConfiguration configuration,
            IFailureRepository failureRepository,
            IClientRepository clientRepository,
            IClientService clientService,
            IMediator mediator)
        {
            _WebHostEnvironment = WebHostEnvironment;
            _configuration = configuration;
            _failureRepository = failureRepository;
            _clientRepository = clientRepository;
            _clientService = clientService;
            _mediator = mediator;
        }

        #region Queries
        [HttpGet("~/api/FiyiStore/Client/1/GetByClientId/{ClientId:int}")]
        public Client GetByClientId(int ClientId)
        {
            try
            {
                return _clientRepository.GetByClientId(ClientId);
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

        [HttpGet("~/api/FiyiStore/Client/1/GetAll")]
        public async Task<List<Client>> GetAll()
        {
            var response = await _mediator.Send(new GetAllRequest());

            return response.lstClient;
        }

        [HttpPost("~/api/FiyiStore/Client/1/GetAllPaginated")]
        public paginatedClientDTO GetAllPaginated([FromBody] paginatedClientDTO paginatedClientDTO)
        {
            try
            {
                return _clientRepository.GetAllByClientIdPaginated(paginatedClientDTO.TextToSearch,
                                            paginatedClientDTO.IsStrictSearch,
                                            paginatedClientDTO.PageIndex,
                                            paginatedClientDTO.PageSize);
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
        [HttpPost("~/api/FiyiStore/Client/1/AddOrUpdate")]
        public IActionResult AddOrUpdate()
        {
            try
            {
                //Get UserId from Session
                int UserId = HttpContext.Session.GetInt32("UserId") ?? 0;

                if(UserId == 0)
                {
                    return StatusCode(401, "Usuario no encontrado en sesión");
                }
                
                #region Pass data from client to server
                //ClientId
                int ClientId = Convert.ToInt32(HttpContext.Request.Form["fiyistore-client-clientid-input"]);
                
                string Name = HttpContext.Request.Form["fiyistore-client-name-input"];
                int Age = Convert.ToInt32(HttpContext.Request.Form["fiyistore-client-age-input"]);
                bool EsCasado = Convert.ToBoolean(HttpContext.Request.Form["fiyistore-client-escasado-input"]);
                DateTime BornDateTime = Convert.ToDateTime(HttpContext.Request.Form["fiyistore-client-borndatetime-input"]);
                decimal Height = Convert.ToDecimal(HttpContext.Request.Form["fiyistore-client-height-input"].ToString().Replace(".",","));
                string Email = HttpContext.Request.Form["fiyistore-client-email-input"];
                string ProfilePicture = HttpContext.Request.Form["fiyistore-client-profilepicture-input"];;
                if (HttpContext.Request.Form.Files.Count != 0)
                {
                    ProfilePicture = $@"/Uploads/FiyiStore/Client/{HttpContext.Request.Form.Files[0].FileName}";
                }
                string FavouriteColour = HttpContext.Request.Form["fiyistore-client-favouritecolour-input"];
                string Password = "";
                if (HttpContext.Request.Form["fiyistore-client-password-input"] != "")
                {
                    Password = Security.EncodeString(HttpContext.Request.Form["fiyistore-client-password-input"]); 
                }
                string PhoneNumber = HttpContext.Request.Form["fiyistore-client-phonenumber-input"];
                string Tags = HttpContext.Request.Form["fiyistore-client-tags-input"];
                string About = HttpContext.Request.Form["fiyistore-client-about-input"];
                string AboutInTextEditor = HttpContext.Request.Form["fiyistore-client-aboutintexteditor-input"];
                string WebPage = HttpContext.Request.Form["fiyistore-client-webpage-input"];
                TimeSpan BornTime = TimeSpan.Parse(HttpContext.Request.Form["fiyistore-client-borntime-input"]);
                string Colour = HttpContext.Request.Form["fiyistore-client-colour-input"];
                
                #endregion

                int NewEnteredId = 0;
                int RowsAffected = 0;

                if (ClientId == 0)
                {
                    //Insert
                    Client Client = new Client()
                    {
                        Active = true,
                        UserCreationId = UserId,
                        UserLastModificationId = UserId,
                        DateTimeCreation = DateTime.Now,
                        DateTimeLastModification = DateTime.Now,
                        Name = Name,
                        Age = Age,
                        EsCasado = EsCasado,
                        BornDateTime = BornDateTime,
                        Height = Height,
                        Email = Email,
                        ProfilePicture = ProfilePicture,
                        FavouriteColour = FavouriteColour,
                        Password = Password,
                        PhoneNumber = PhoneNumber,
                        Tags = Tags,
                        About = About,
                        AboutInTextEditor = AboutInTextEditor,
                        WebPage = WebPage,
                        BornTime = BornTime,
                        Colour = Colour,
                        
                    };
                    
                    NewEnteredId = _clientRepository.Add(Client);
                }
                else
                {
                    //Update
                    Client Client = _clientRepository.GetByClientId(ClientId);
                    
                    Client.UserLastModificationId = UserId;
                    Client.DateTimeLastModification = DateTime.Now;
                    Client.Name = Name;
                    Client.Age = Age;
                    Client.EsCasado = EsCasado;
                    Client.BornDateTime = BornDateTime;
                    Client.Height = Height;
                    Client.Email = Email;
                    Client.ProfilePicture = ProfilePicture;
                    Client.FavouriteColour = FavouriteColour;
                    Client.Password = Password;
                    Client.PhoneNumber = PhoneNumber;
                    Client.Tags = Tags;
                    Client.About = About;
                    Client.AboutInTextEditor = AboutInTextEditor;
                    Client.WebPage = WebPage;
                    Client.BornTime = BornTime;
                    Client.Colour = Colour;
                                       

                    RowsAffected = _clientRepository.Update(Client);
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
                            var FilePath = $@"{_WebHostEnvironment.WebRootPath}/Uploads/FiyiStore/Client/";

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

                if (ClientId == 0)
                {
                    return StatusCode(200, NewEnteredId); 
                }
                else
                {
                    return StatusCode(200, RowsAffected);
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

        [HttpDelete("~/api/FiyiStore/Client/1/DeleteByClientId/{ClientId:int}")]
        public IActionResult DeleteByClientId(int ClientId)
        {
            try
            {
                int RowsDeleted = _clientRepository.DeleteByClientId(ClientId);
                return StatusCode(200, RowsDeleted);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ajax"></param>
        /// <param name="DeleteType">Accept two values: All or NotAll</param>
        /// <returns></returns>
        [HttpPost("~/api/FiyiStore/Client/1/DeleteManyOrAll/{DeleteType}")]
        public IActionResult DeleteManyOrAll([FromBody] Ajax Ajax, string DeleteType)
        {
            try
            {
                _clientRepository.DeleteManyOrAll(Ajax, DeleteType);

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

        [HttpPost("~/api/FiyiStore/Client/1/CopyByClientId/{ClientId:int}")]
        public IActionResult CopyByClientId(int ClientId)
        {
            try
            {
                int NumberOfRegistersEntered = _clientRepository.CopyByClientId(ClientId);

                return StatusCode(200, NumberOfRegistersEntered);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ajax"></param>
        /// <param name="CopyType">Accept two values: All or NotAll</param>
        /// <returns></returns>
        [HttpPost("~/api/FiyiStore/Client/1/CopyManyOrAll/{CopyType}")]
        public IActionResult CopyManyOrAll([FromBody] Ajax Ajax, string CopyType)
        {
            try
            {
                int NumberOfRegistersEntered = _clientRepository.CopyManyOrAll(Ajax, CopyType);
                
                return StatusCode(200, NumberOfRegistersEntered);
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

        #region Exportations
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ajax"></param>
        /// <param name="ExportationType">Accept two values: All or NotAll</param>
        /// <returns></returns>
        [HttpPost("~/api/FiyiStore/Client/1/ExportAsPDF/{ExportationType}")]
        public IActionResult ExportAsPDF([FromBody] Ajax Ajax, string ExportationType)
        {
            try
            {
                DateTime Now = _clientService.ExportAsPDF(Ajax, ExportationType);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ajax"></param>
        /// <param name="ExportationType">Accept two values: All or NotAll</param>
        /// <returns></returns>
        [HttpPost("~/api/FiyiStore/Client/1/ExportAsExcel/{ExportationType}")]
        public IActionResult ExportAsExcel([FromBody] Ajax Ajax, string ExportationType)
        {
            try
            {
                DateTime Now = _clientService.ExportAsExcel(Ajax, ExportationType);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ajax"></param>
        /// <param name="ExportationType">Accept two values: All or NotAll</param>
        /// <returns></returns>
        [HttpPost("~/api/FiyiStore/Client/1/ExportAsCSV/{ExportationType}")]
        public IActionResult ExportAsCSV([FromBody] Ajax Ajax, string ExportationType)
        {
            try
            {
                DateTime Now = _clientService.ExportAsCSV(Ajax, ExportationType);

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