using System.Threading;
using System.Threading.Tasks;
using MdClone.Data.Contracts.Providers;
using MdClone.Model.Contracts;
using MdClone.Model.Mappers;

namespace MdClone.Model
{
    internal sealed class EmailService : IEmailService
    {
        private readonly IEmailProvider _emailProvider;
        private readonly EmailModelMapper _emailModelMapper;

        public EmailService(IEmailProvider emailProvider, EmailModelMapper emailModelMapper)
        {
            _emailProvider = emailProvider;
            _emailModelMapper = emailModelMapper;
        }

        IEmailModel IEmailService.CreateNewEmail()
        {
            var emailModel = new EmailModel();
            return emailModel;
        }

        Task IEmailService.SendEmail(IEmailModel emailModel, CancellationToken ct)
        {
            return Task.Run(() => _emailProvider.SendEmail(_emailModelMapper.MapToDto(emailModel)), ct);
        }
    }
}