using Contact.API.Infrastructure.Dtos;
using Contact.API.Infrastructure.Results;
using Newtonsoft.Json;

namespace Contact.API.Infrastructure.Repositories
{
    public interface IContactRepository
    {
        Task<ServiceItemResult<ContactDto>> GetByIdAsync(int id);
        Task<ServiceListResult<ContactDto>> GetAllAsync();
        Task<ServiceResult> InsertContactAsync(ContactDto contactDto);
        Task<ServiceResult> InsertContactInfoAsync(ContactInfoDto contactInfoDto);
        Task<ServiceResult> UpdateContactAsync(ContactDto contactDto);
        Task<ServiceResult> UpdateContactInfoAsync(ContactInfoDto contactInfoDto);
        Task<ServiceResult> DeleteContactAsync(int id);
        Task<ServiceResult> DeleteContactInfoAsync(int id);
        Task<ServiceListResult<ReportDto>> GetReport();
    }


    public class ContactRepository : IContactRepository
    {
        private readonly ILogger<ContactRepository> _logger;
        private readonly ContactDbContext _context;
        private readonly IMapper _mapper;

        public ContactRepository(ILogger<ContactRepository> logger, IServiceProvider service, IMapper mapper)
        {
            _logger = logger;
            _context = service.GetService<ContactDbContext>();
            _mapper = mapper;

            //InitializeDb();
        }

        public async Task<ServiceItemResult<ContactDto>> GetByIdAsync(int id)
        {
            _logger.LogWarning($"GetByIdAsync Request: {id} - Time: {DateTime.Now}");

            var result = new ServiceItemResult<ContactDto>();

            try
            {
                var contact = await _context.Contacts.Include("ContactInfos").FirstOrDefaultAsync(f => f.Id == id);

                if (contact != null)
                {
                    //var contactDto = _mapper.Map<ContactDto>(contact);

                    var contactDto = new ContactDto
                    {
                        Id = contact.Id,
                        FirstName = contact.FirstName,
                        MiddleName = contact.MiddleName,
                        LastName = contact.LastName,
                        Company = contact.Company
                    };
                    foreach (var info in contact.ContactInfos)
                    {
                        contactDto.ContactInfos.Add(new ContactInfoDto { Id = info.Id, InfoTypeId = info.InfoTypeId, Value = info.Value });
                    }

                    result.Item = contactDto;
                }

                _logger.LogWarning($"GetByIdAsync Response: {JsonConvert.SerializeObject(result)}");
            }
            catch (Exception e)
            {
                _logger.LogError($"GetByIdAsync Error: {e.Message} - {e.StackTrace} - {e.InnerException}");

                result.ResultCode = 3;
                result.ResultMessage = e.Message;
            }

            return result;
        }

        public async Task<ServiceListResult<ContactDto>> GetAllAsync()
        {
            _logger.LogWarning($"GetAllAsync Request Time: {DateTime.Now}");

            var result = new ServiceListResult<ContactDto>();

            try
            {
                var contacts = _context.Contacts.Include("ContactInfos").ToList();

                foreach (var contact in contacts)
                {
                    var contactDto = new ContactDto
                    {
                        Id = contact.Id,
                        FirstName = contact.FirstName,
                        MiddleName = contact.MiddleName,
                        LastName = contact.LastName,
                        Company = contact.Company,
                    };
                    foreach (var info in contact.ContactInfos)
                    {
                        contactDto.ContactInfos.Add(new ContactInfoDto { Id = info.Id, InfoTypeId = info.InfoTypeId, Value = info.Value });
                    }

                    result.Items.Add(contactDto);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"GetAllAsync Error: {e.Message} - {e.StackTrace} - {e.InnerException}");

                result.ResultCode = 3;
                result.ResultMessage = e.Message;
            }

            return result;
        }

        public async Task<ServiceResult> InsertContactAsync(ContactDto contactDto)
        {
            _logger.LogWarning($"InsertAsync Request: {JsonConvert.SerializeObject(contactDto)}");

            var result = new ServiceResult();
            try
            {
                if (contactDto == null)
                {
                    throw new NotImplementedException( "Veri yapisi hatali");
                }

                //var contact = _mapper.Map<Models.Contact>(contactDto);
                var contact = new Models.Contact
                {
                    FirstName = contactDto.FirstName,
                    MiddleName = contactDto.MiddleName,
                    LastName = contactDto.LastName,
                    Company = contactDto.Company
                };

                _context.Contacts.Add(contact);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"InsertAsync Error: {e.Message} - {e.StackTrace} - {e.InnerException}");

                result.ResultCode = 3;
                result.ResultMessage = e.Message;
            }

            return result;
        }

        public async Task<ServiceResult> InsertContactInfoAsync(ContactInfoDto contactInfoDto)
        {
            _logger.LogWarning($"InsertContactInfoAsync Request: {JsonConvert.SerializeObject(contactInfoDto)}");

            var result = new ServiceResult();

            try
            {
                if (contactInfoDto == null)
                {
                    throw new NotImplementedException("Veri yapisi hatali");
                }

                //var contactInfo = _mapper.Map<ContactInfo>(contactInfoDto);
                var contactInfo = new ContactInfo
                {
                    ContactId = contactInfoDto.ContactId.Value,
                    InfoTypeId = contactInfoDto.InfoTypeId,
                    Value = contactInfoDto.Value
                };

                _context.ContactInfo.Add(contactInfo);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"InsertContactInfoAsync Error: {e.Message} - {e.StackTrace} - {e.InnerException}");

                result.ResultCode = 3;
                result.ResultMessage = e.Message;
            }

            return result;
        }

        public async Task<ServiceResult> UpdateContactAsync(ContactDto contactDto)
        {
            _logger.LogWarning($"UpdateContactAsync Request: {JsonConvert.SerializeObject(contactDto)}");

            var result = new ServiceResult();

            try
            {
                if (contactDto == null || contactDto != null && !contactDto.Id.HasValue)
                {
                    throw new NotImplementedException("Veri yapisi hatali");
                }

                var contact = await _context.Contacts.Include("ContactInfos").FirstOrDefaultAsync(f => f.Id == contactDto.Id.Value);

                if(contact == null)
                {
                    throw new NotImplementedException("Kayit bulunamadi");
                }

                contact.FirstName = contactDto.FirstName;   
                contact.MiddleName = contactDto.MiddleName;
                contact.LastName = contactDto.LastName;
                contact.Company = contactDto.Company;

                _context.Entry(contact).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"UpdateContactAsync Error: {e.Message} - {e.StackTrace} - {e.InnerException}");

                result.ResultCode = 3;
                result.ResultMessage = e.Message;
            }

            return result;
        }

        public async Task<ServiceResult> UpdateContactInfoAsync(ContactInfoDto contactInfoDto)
        {
            _logger.LogWarning($"UpdateContactInfoAsync Request: {JsonConvert.SerializeObject(contactInfoDto)}");

            var result = new ServiceResult();

            try
            {
                if (contactInfoDto == null || contactInfoDto != null && !contactInfoDto.Id.HasValue)
                {
                    throw new NotImplementedException("Veri yapisi hatali");
                }

                var info = _context.ContactInfo.FirstOrDefault(i => i.Id == contactInfoDto.Id);

                if (info == null)
                {
                    throw new NotImplementedException("Kayit bulunamadi");
                }

                info.Value = contactInfoDto.Value;
                
                _context.Entry(info).State = EntityState.Modified;
                
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"UpdateContactInfoAsync Error: {e.Message} - {e.StackTrace} - {e.InnerException}");

                result.ResultCode = 3;
                result.ResultMessage = e.Message;
            }

            return result;
        }

        public async Task<ServiceResult> DeleteContactAsync(int id)
        {
            _logger.LogWarning($"DeleteContactAsync Request: {id} - Time: {DateTime.Now}");

            var result = new ServiceResult();

            try
            {
                var contact = _context.Contacts.Include("ContactInfos").FirstOrDefault(c => c.Id == id);

                if(contact == null)
                {
                    throw new NotImplementedException("Veri bulunamadi");
                }

                _context.ContactInfo.RemoveRange(contact.ContactInfos);

                _context.Contacts.Remove(contact);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"DeleteContactAsync Error: {e.Message} - {e.StackTrace} - {e.InnerException}");

                result.ResultCode = 3;
                result.ResultMessage = e.Message;
            }

            return result;
        }

        public async Task<ServiceResult> DeleteContactInfoAsync(int id)
        {
            _logger.LogWarning($"DeleteContactInfoAsync Request: {id} - Time: {DateTime.Now}");

            var result = new ServiceResult();

            try
            {
                var info = _context.ContactInfo.FirstOrDefault(i => i.Id == id);
                
                if(info == null)
                {
                    throw new NotImplementedException("Kayit bulunamadi");
                }

                _context.ContactInfo.Remove(info);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"DeleteContactInfoAsync Error: {e.Message} - {e.StackTrace} - {e.InnerException}");

                result.ResultCode = 3;
                result.ResultMessage = e.Message;
            }

            return result;
        }

        public async Task<ServiceListResult<ReportDto>> GetReport()
        {
            var result = new ServiceListResult<ReportDto>();

            try
            {
                var phoneTypes = new int[] { 2, 3};

                var locations = (from contactInfo in _context.ContactInfo
                                 join infoType in _context.InfoTypes on contactInfo.InfoTypeId equals infoType.Id
                                 where infoType.Id == 4
                                 select new
                                 {
                                     contactInfo.Value
                                 }).Distinct().OrderBy(o => o.Value).ToList();
                
                foreach (var location in locations)
                {

                    var reportDto = new ReportDto();
                    reportDto.Location = location.Value;


                    var contacts = (from contact in _context.Contacts
                                    join contactInfo in _context.ContactInfo on contact.Id equals contactInfo.ContactId
                                    where contactInfo.Value == location.Value
                                    select contact.Id
                                    ).ToList();
                    reportDto.NumberOfContacts = contacts.Count();

                    var phones = _context.ContactInfo.Where(ci => phoneTypes.Contains(ci.InfoTypeId) && contacts.Contains(ci.ContactId)).ToList();
                    reportDto.NumberOfPhones = phones.Count();

                    result.Items.Add(reportDto);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"GetReport Error: {e.Message} - {e.StackTrace} - {e.InnerException}");

                result.ResultCode = 3;
                result.ResultMessage = e.Message;
            }

            return result;
        }

        #region Helpers
        private async Task InitializeDb()
        {
            var contacts = new List<Models.Contact>
            {
                new Models.Contact { Id = 3, FirstName = "Steve", MiddleName = "", LastName = "Jobs", Company = "Apple" },
                new Models.Contact { Id = 4, FirstName = "Bill", MiddleName = "", LastName = "Gates", Company = "Microsoft" },
                new Models.Contact { Id = 5, FirstName = "Sergey", MiddleName = "", LastName = "Brin", Company = "Google" },
                new Models.Contact { Id = 6, FirstName = "Larry", MiddleName = "", LastName = "Page", Company = "Google" },
                new Models.Contact { Id = 7, FirstName = "Jeff", MiddleName = "", LastName = "Bezos", Company = "Amazon" },
                new Models.Contact { Id = 8, FirstName = "Elon", MiddleName = "", LastName = "Musk", Company = "Tesla" }
            };

            foreach (var contact in contacts)
            {
                if (!_context.Contacts.Any(c => c.Id == contact.Id))
                {
                    _context.Contacts.Add(contact);
                }
            }
            _context.SaveChanges();

            var infoTypes = new List<InfoType>
            {
                new InfoType { Id = 1, Culture = "tr", Name = "email", DisplayName = "E-Posta" },
                new InfoType { Id = 2, Culture = "tr", Name = "cellphone", DisplayName = "Cep No" },
                new InfoType { Id = 3, Culture = "tr", Name = "telephone", DisplayName = "Telefon No" },
                new InfoType { Id = 4, Culture = "tr", Name = "location", DisplayName = "Konum" }
            };

            foreach (var infoType in infoTypes)
            {
                if(!_context.InfoTypes.Any(t => t.Id == infoType.Id))
                {
                    _context.InfoTypes.Add(infoType);
                }
            }
            _context.SaveChanges();

            var contactInfos = new List<ContactInfo>
            {
                new ContactInfo { Id = 1, ContactId = 2, InfoTypeId = 1, Value = "necdet.inkaya@inkaya.com" },
                new ContactInfo { Id = 2, ContactId = 2, InfoTypeId = 2, Value = "+12005550000" },
                new ContactInfo { Id = 3, ContactId = 2, InfoTypeId = 4, Value = "Istanbul, TR" },
                new ContactInfo { Id = 4, ContactId = 3, InfoTypeId = 1, Value = "steve.jobs@apple.com" },
                new ContactInfo { Id = 5, ContactId = 3, InfoTypeId = 2, Value = "+12015551111" },
                new ContactInfo { Id = 6, ContactId = 3, InfoTypeId = 4, Value = "Cupertino, CA" },
                new ContactInfo { Id = 7, ContactId = 4, InfoTypeId = 1, Value = "bill.gates@microsoft.com" },
                new ContactInfo { Id = 8, ContactId = 4, InfoTypeId = 2, Value = "+12025552222" },
                new ContactInfo { Id = 9, ContactId = 4, InfoTypeId = 4, Value = "Redmond, WA" },
                new ContactInfo { Id = 10, ContactId = 5, InfoTypeId = 1, Value = "sergey.brin@google.com" },
                new ContactInfo { Id = 11, ContactId = 5, InfoTypeId = 2, Value = "+12035553333" },
                new ContactInfo { Id = 12, ContactId = 5, InfoTypeId = 4, Value = "Mountain View, CA" },
                new ContactInfo { Id = 13, ContactId = 6, InfoTypeId = 1, Value = "larry.page@google.com" },
                new ContactInfo { Id = 14, ContactId = 6, InfoTypeId = 2, Value = "+12045554444" },
                new ContactInfo { Id = 15, ContactId = 6, InfoTypeId = 4, Value = "Mountain View, CA" },
                new ContactInfo { Id = 16, ContactId = 7, InfoTypeId = 1, Value = "jeff.bezos@amazon.com" },
                new ContactInfo { Id = 17, ContactId = 7, InfoTypeId = 2, Value = "+12055555555" },
                new ContactInfo { Id = 18, ContactId = 7, InfoTypeId = 4, Value = "Seattle, WA" },
                new ContactInfo { Id = 19, ContactId = 8, InfoTypeId = 1, Value = "elon.mask@tesla.com" },
                new ContactInfo { Id = 20, ContactId = 8, InfoTypeId = 2, Value = "+12065556666" },
                new ContactInfo { Id = 21, ContactId = 8, InfoTypeId = 4, Value = "Austin, TX" }
            };

            foreach (var contactInfo in contactInfos) 
            {
                if(!_context.ContactInfo.Any(c => c.Id == contactInfo.Id))
                {
                    _context.ContactInfo.Add(contactInfo);
                }
            }
            _context.SaveChanges();
        }
        #endregion
    }
}
