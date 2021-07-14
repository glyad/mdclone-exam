using System.IO;
using System.Linq;
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
        private readonly EmailRecipientModelMapper _emailRecipientModelMapper;

        public EmailService(IEmailProvider emailProvider, EmailModelMapper emailModelMapper, EmailRecipientModelMapper emailRecipientModelMapper)
        {
            _emailProvider = emailProvider;
            _emailModelMapper = emailModelMapper;
            _emailRecipientModelMapper = emailRecipientModelMapper;
        }

        IEmailModel IEmailService.CreateNewEmail()
        {
            var emailModel = new EmailModel();
            return emailModel;
        }

        IEmailRecipientModel IEmailService.CreateNewEmailRecipient(string address)
        {
            var emailRecipient = new EmailRecipientModel {Address = address};
            emailRecipient.ClearDirty();
            return emailRecipient;
        }

        Task IEmailService.SendEmail(IEmailModel emailModel, CancellationToken ct)
        {
            return Task.Run(() => _emailProvider.SendEmail(_emailModelMapper.MapToDto(emailModel)), ct);
        }

        Task<IEmailRecipientModel[]> IEmailService.GetRecipients(CancellationToken ct)
        {
            return Task.Run(() => _emailProvider.GetRecipients().Select(dto => _emailRecipientModelMapper.MapToModel(dto)).ToArray(), ct);
        }

        async Task<IAttachedFile> IEmailService.Attach(IEmailModel emailModel, string filename, CancellationToken ct)
        {
            var result = await Task.Run(() =>
            {
                var fileInfo = new FileInfo(filename);

                Thread.Sleep(1000);

                var attachedFile = new AttachedFile(filename)
                {
                    FileSize = fileInfo.Length,
                    Name = fileInfo.Name
                };

                return (IAttachedFile) attachedFile;
            }, ct);

            ((EmailModel)emailModel).AttachedFiles.Add(result);

            return result;
        }

        void IEmailService.Detach(IEmailModel emailModel, IAttachedFile attachedFile)
        {
            ((EmailModel) emailModel).AttachedFiles.Remove(attachedFile);
        }
    }
}