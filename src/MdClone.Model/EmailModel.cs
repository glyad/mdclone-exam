using JetBrains.Annotations;
using LogoFX.Client.Mvvm.Model;
using MdClone.Model.Contracts;
using MdClone.Model.Validation;

namespace MdClone.Model
{
    [UsedImplicitly]
    internal class EmailModel : EditableModel, IEmailModel
    {
        private string _to;
        private string _cc;
        private string _subject;
        private byte[] _message;

        [EmailValidation]
        public string To
        {
            get => _to;
            set => SetProperty(ref _to, value);
        }

        [StringValidation(IsNulOrEmptyAllowed = true, MaxLength = 63)]
        public string Cc
        {
            get => _cc;
            set => SetProperty(ref _cc, value);
        }

        [StringValidation(IsNulOrEmptyAllowed = false, MaxLength = 63)]
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
    }
}