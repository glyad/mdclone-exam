using JetBrains.Annotations;
using MdClone.Model.Contracts;

namespace MdClone.Model
{
    [UsedImplicitly]
    internal class TableDataModel : AppModel, ITableDataModel
    {
        public string[] Header { get; [UsedImplicitly] set; }

        public IRowDataModel[] Rows { get; [UsedImplicitly] set; }
    }
}