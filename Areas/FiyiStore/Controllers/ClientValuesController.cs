using Microsoft.AspNetCore.Mvc;
using FiyiStore.Areas.FiyiStore.DTOs;
using FiyiStore.Areas.FiyiStore.Filters;
using FiyiStore.Areas.FiyiStore.Interfaces;
using FiyiStore.Areas.FiyiStore.Entities;
using FiyiStore.Library;

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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IClientRepository _clientRepository;
        private readonly IClientService _clientService;

        public ClientValuesController(IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration,
            IClientRepository clientRepository,
            IClientService clientService)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _clientRepository = clientRepository;
            _clientService = clientService;
        }

        #region Queries
        [HttpGet("~/api/FiyiStore/Client/1/GetByClientId/{clientId:int}")]
        public Client GetByClientId(int clientId)
        {
            return _clientRepository.GetByClientId(clientId);
        }

        [HttpGet("~/api/FiyiStore/Client/1/GetAll")]
        public List<Client> GetAll()
        {
            return _clientRepository.GetAll();
        }

        [HttpPost("~/api/FiyiStore/Client/1/GetAllPaginated")]
        public paginatedClientDTO GetAllPaginated([FromBody] paginatedClientDTO paginatedClientDTO)
        {
            return _clientRepository.GetAllByClientIdPaginated(paginatedClientDTO.TextToSearch,
                                            paginatedClientDTO.IsStrictSearch,
                                            paginatedClientDTO.PageIndex,
                                            paginatedClientDTO.PageSize);
        }
        #endregion

        #region Non-Queries
        [HttpPost("~/api/FiyiStore/Client/1/AddOrUpdate")]
        public IActionResult AddOrUpdate()
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
                            var FilePath = $@"{_webHostEnvironment.WebRootPath}/Uploads/FiyiStore/Client/";

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

        [HttpDelete("~/api/FiyiStore/Client/1/DeleteByClientId/{clientId:int}")]
        public IActionResult DeleteByClientId(int clientId)
        {
            int RowsDeleted = _clientRepository.DeleteByClientId(clientId);
            return StatusCode(200, RowsDeleted);
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
            string RowsDeleted = _clientRepository.DeleteManyOrAll(Ajax, DeleteType);

            return StatusCode(200, RowsDeleted);
        }

        [HttpPost("~/api/FiyiStore/Client/1/CopyByClientId/{clientId:int}")]
        public IActionResult CopyByClientId(int clientId)
        {
            int NumberOfRegistersEntered = _clientRepository.CopyByClientId(clientId);

            return StatusCode(200, NumberOfRegistersEntered);
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
            int NumberOfRegistersEntered = _clientRepository.CopyManyOrAll(Ajax, CopyType);

            return StatusCode(200, NumberOfRegistersEntered);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ajax">Used when is NotAll option selected</param>
        /// <param name="exportationFile">Can be Excel, PDF o CSV</param>
        /// <param name="exportationType">Can be All or NotAll</param>
        /// <returns></returns>
        [HttpPost("~/api/FiyiStore/Client/1/Export/{exportationFile}/{exportationType}")]
        public IActionResult Export([FromBody] Ajax Ajax, string exportationFile, string exportationType)
        {
            DateTime Now;

            if (exportationFile == "Excel")
            {
                Now = _clientService.ExportAsExcel(Ajax, exportationType);
            }
            else if (exportationFile == "PDF")
            {
                Now = _clientService.ExportAsPDF(Ajax, exportationType);
            }
            else if(exportationFile == "CSV")
            {
                Now = _clientService.ExportAsCSV(Ajax, exportationType);
            }
            else
            {
                return StatusCode(400);
            }
            
            return StatusCode(200, Now.ToString());
        }
    }
}