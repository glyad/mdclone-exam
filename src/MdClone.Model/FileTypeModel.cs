using MdClone.Model.Contracts;

namespace MdClone.Model
{
    internal class FileTypeModel : AppModel, IFileTypeModel
    {
        public string Filter { get; set; }
        public string DisplayName { get; set; }
    }
}