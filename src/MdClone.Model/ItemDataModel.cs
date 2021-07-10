using JetBrains.Annotations;
using MdClone.Model.Contracts;

namespace MdClone.Model
{
    [UsedImplicitly]
    internal class ItemDataModel : AppModel, IItemDataModel
    {
        public string Header { get; [UsedImplicitly] set; }
        
        public string Value { get; [UsedImplicitly] set; }
    }
}