using System.Collections.Generic;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.Model;
using LogoFX.Core;
using MdClone.Model.Contracts;

namespace MdClone.Model
{
    [UsedImplicitly]
    internal class EmailModel : EditableModel, IEmailModel
    {
        private IEmailRecipientModel[] _to;
        private IEmailRecipientModel[] _cc;
        private string _subject;
        private byte[] _message;
        private readonly RangeObservableCollection<IAttachedFile> _attachedFiles;

        public EmailModel()
        {
            _attachedFiles = new RangeObservableCollection<IAttachedFile>();
        }

        public IEmailRecipientModel[] To
        {
            get => _to;
            set => SetProperty(ref _to, value);
        }

        public IEmailRecipientModel[] Cc
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