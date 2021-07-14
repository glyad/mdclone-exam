using MdClone.Model.Contracts;

namespace MdClone.Model
{
    internal class AttachedFile : AppModel, IAttachedFile
    {
        public AttachedFile(string filename)
        {
            Filename = filename;
        }

        public string Filename { get; }

        public long FileSize { get; set; }
    }
}