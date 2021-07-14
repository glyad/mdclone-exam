using System.Collections.Generic;
using LogoFX.Client.Mvvm.Model.Contracts;

namespace MdClone.Model.Contracts
{
    public interface IEmailModel : IAppModel, IEditableModel
    {
        IEmailRecipientsModel To { get; }

        IEmailRecipientsModel Cc { get; }

        string Subject { get; set; }

        byte[] Message { get; set; }

        IEnumerable<IAttachedFile> AttachedFiles { get; }
    }
}