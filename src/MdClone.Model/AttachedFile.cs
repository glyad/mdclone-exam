using JetBrains.Annotations;
using MdClone.Model.Contracts;

namespace MdClone.Model
{
    internal class AttachedFile : AppModel, IAttachedFile
    {
        public AttachedFile(string filename)
        {
            Filename = filename;
        }

        public string Filename { [UsedImplicitly] get; }

        public long FileSize { get; set; }
    }
}