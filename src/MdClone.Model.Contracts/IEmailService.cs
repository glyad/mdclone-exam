using System.Threading;
using System.Threading.Tasks;

namespace MdClone.Model.Contracts
{
    public interface IEmailService
    {
        IEmailModel CreateNewEmail();

        IEmailRecipientModel CreateNewEmailRecipient(string address);

        Task SendEmail(IEmailModel emailModel, CancellationToken ct = default);

        Task<IEmailRecipientModel[]> GetRecipients(CancellationToken ct = default);

        Task<IAttachedFile> Attach(IEmailModel emailModel, string filename, CancellationToken ct = default);

        void Detach(IEmailModel emailModel, IAttachedFile attachedFile);
    }
}