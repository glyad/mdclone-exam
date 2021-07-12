using System.Threading;
using System.Threading.Tasks;

namespace MdClone.Model.Contracts
{
    public interface IDataService
    {
        Task<ITableDataModel> LoadData(IFileModel fileModel, CancellationToken ct = default);

        IFileTypeModel[] FileTypes { get; }
    }
}