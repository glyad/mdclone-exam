using JetBrains.Annotations;
using MdClone.Model.Contracts;

namespace MdClone.Model
{
    [UsedImplicitly]
    internal class RowDataModel : AppModel, IRowDataModel
    {
        public IItemDataModel[] Items { get; [UsedImplicitly] set; }
    }
}