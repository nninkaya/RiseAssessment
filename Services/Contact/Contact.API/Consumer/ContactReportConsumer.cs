using Contact.API.Infrastructure.Dtos;
using Contracts;
using MassTransit;

namespace Contact.API.Consumer
{
    public class ContactReportConsumer : IConsumer<ContactReportMessage>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ContactReportConsumer(IContactRepository contactRepository, IPublishEndpoint publishEndpoint)
        {
            _contactRepository = contactRepository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<ContactReportMessage> context)
        {
            Console.WriteLine(context.Message.ReportId);

            var report = _contactRepository.GetReport();

            if (report.Result.ResultCode != 0) return;

            var reportList = new List<Contracts.ReportDto>();
            
            foreach (var item in report.Result.Items)
            {
                reportList.Add(new Contracts.ReportDto
                {
                    Location = item.Location,
                    NumberOfContacts = item.NumberOfContacts,
                    NumberOfPhones = item.NumberOfPhones
                });
            } 
            
            var contactReport = new ReportResponseMessage
            {
                ReportId = context.Message.ReportId,
                Report = reportList
            };

            await _publishEndpoint.Publish<ReportResponseMessage>(new
            {
                ReportId = contactReport.ReportId,
                Report = report.Result.Items
            });

        }
    }
}
