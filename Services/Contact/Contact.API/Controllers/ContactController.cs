using Contact.API.Infrastructure.Constants;
using Contact.API.Infrastructure.Dtos;
using Contact.API.Infrastructure.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contact.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactRepository _contactRepository;

        public ContactController(ILogger<ContactController> logger, IContactRepository contactRepository)
        {
            _logger = logger;
            _contactRepository = contactRepository;
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServiceListResult of Models.Contact></returns>
        /// <remarks>Sorgulanmak istenen kisinin Id bilgisi gonderilir. Gonderilen kisinin telefon defterinde kayitli bilgilerini doner </remarks>
        [HttpGet("GetByIdAsync/{id:int}")]
        [ProducesResponseType(typeof(ServiceListResult<ContactDto>), statusCode: 200)]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _contactRepository.GetByIdAsync(id);

            if (result.ResultCode != 0)
            {
                _logger.LogError(result.ResultMessage);

                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <returns></returns>
        /// <remarks>Telefon defterine kayitli tum kisilerin listini iletisim bilgileri ile birlikte doner</remarks>
        [HttpGet("GetAllAsync")]
        [ProducesResponseType(typeof(ServiceListResult<ContactDto>), statusCode: 200)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _contactRepository.GetAllAsync();

            if (result.ResultCode != 0)
            {
                _logger.LogError(result.ResultMessage);

                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// InsertContactAsync
        /// </summary>
        /// <param name="contact"></param>
        /// <returns> </returns>
        /// <remarks>Telefon defterine eklenmek istenen kisi bilgileri gonderilir</remarks>
        [HttpPost("InsertContactAsync")]
        [ProducesResponseType(typeof(ServiceResult), statusCode: 200)]
        public async Task<IActionResult> InsertContactAsync(ContactDto contact)
        {
            if (contact == null)
            {
                var error = new ServiceResult()
                {
                    ResultCode = 3,
                    ResultMessage = "Veri yapisi hatali"
                };
                return BadRequest(error);
            }

            var result = await _contactRepository.InsertContactAsync(contact);

            return Ok(result);
        }

        /// <summary>
        /// InsertContactInfoAsync
        /// </summary>
        /// <param name="contact"></param>
        /// <returns> </returns>
        /// <remarks>Telefon defterine eklenmek istenen iletisim bilgileri gonderilir</remarks>
        [HttpPost("InsertContactInfoAsync")]
        [ProducesResponseType(typeof(ServiceResult), statusCode: 200)]
        public async Task<IActionResult> InsertContactInfoAsync(ContactInfoDto contactInfo)
        {
            if (contactInfo == null)
            {
                var error = new ServiceResult()
                {
                    ResultCode = 3,
                    ResultMessage = "Veri yapisi hatali"
                };
                return BadRequest(error);
            }

            var result = await _contactRepository.InsertContactInfoAsync(contactInfo);

            return Ok(result);
        }
        /// <summary>
        /// UpdateContactAsync
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        /// <remarks>Degisiklik yapilmak istenen kisinin bilgisi gonderilir</remarks>
        [HttpPut("UpdateContactAsync")]
        [ProducesResponseType(typeof(ServiceResult), statusCode: 200)]
        public async Task<IActionResult> UpdateContactAsync(ContactDto contact)
        {
            if (contact == null)
            {
                var error = new ServiceResult()
                {
                    ResultCode = 3,
                    ResultMessage = "Veri yapisi hatali"
                };
                return BadRequest(error);
            }

            var result = await _contactRepository.UpdateContactAsync(contact);

            return Ok(result);
        }

        /// <summary>
        /// UpdateContactInfoAsync
        /// </summary>
        /// <param name="contactInfo"></param>
        /// <returns></returns>
        /// <remarks>Degisiklik yapilmak istenen iletisim kayit bilgisi gonderilir</remarks>
        [HttpPut("UpdateContactInfoAsync")]
        [ProducesResponseType(typeof(ServiceResult), statusCode: 200)]
        public async Task<IActionResult> UpdateContactInfoAsync(ContactInfoDto contactInfo)
        {
            if (contactInfo == null)
            {
                return BadRequest(Messages.ModelError);
            }

            var result = await _contactRepository.UpdateContactInfoAsync(contactInfo);

            if (result.ResultCode != 0)
            {
                return BadRequest(result.ResultMessage);
            }

            return Ok(result);
        }

        /// <summary>
        /// DeleteContactAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>Silinmek istenen kisiye ait Id bilgisi gonderilir. Kisi ve kisiye ait tum rehber kayitlari silinir</remarks>
        [HttpDelete("DeleteContactAsync/{id:int}")]
        [ProducesResponseType(typeof(ServiceResult), statusCode: 200)]
        public async Task<IActionResult> DeleteContactAsync(int id)
        {
            var result = await _contactRepository.DeleteContactAsync(id);

            if (result.ResultCode != 0)
            {
                return BadRequest(result.ResultMessage);
            }

            return Ok(result);
        }

        /// <summary>
        /// DeleteContactInfoAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>Silinmek istenen iletisim bilgisinin Id bilgisi gonderilir. </remarks>
        [HttpDelete("DeleteContactInfoAsync/{id:int}")]
        [ProducesResponseType(typeof(ServiceResult), statusCode: 200)]
        public async Task<IActionResult> DeleteContactInfoAsync(int id)
        {
            var result = await _contactRepository.DeleteContactInfoAsync(id);

            if (result.ResultCode != 0)
            {
                return BadRequest(result.ResultMessage);
            }

            return Ok(result);
        }

        [HttpGet("GetReport")]
        public async Task<IActionResult> GetReport()
        {
            var result = await _contactRepository.GetReport();

            if (result.ResultCode != 0)
            {
                return BadRequest(result.ResultMessage);
            }

            return Ok(result);
        }
    }
}