using System.Collections.Generic;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.Model;
using LogoFX.Core;
using MdClone.Model.Contracts;
using MdClone.Model.Validation;

namespace MdClone.Model
{
    [UsedImplicitly]
    internal class EmailModel : EditableModel, IEmailModel
    {
        private IEmailRecipientsModel _to;
        private IEmailRecipientsModel _cc;
        private string _subject;
        private byte[] _message;
        private readonly RangeObservableCollection<IAttachedFile> _attachedFiles;

        public EmailModel()
        {
            _attachedFiles = new RangeObservableCollection<IAttachedFile>();
            _to = new EmailRecipientsModel();
            _cc = new EmailRecipientsModel();
        }

        [EmailRecipientsValidation]
        public IEmailRecipientsModel To
        {
            get => _to;
            set => SetProperty(ref _to, value);
        }

        public IEmailRecipientsModel Cc
        {
            get => _cc;
            set => SetProperty(ref _cc, value);
        }

        public string Subject
        {
            get => _subject;
            set => SetProperty(ref _subject, value);
        }

        public byte[] Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public ICollection<IAttachedFile> AttachedFiles => _attachedFiles;

        IEnumerable<IAttachedFile> IEmailModel.AttachedFiles => AttachedFiles;
    }
}