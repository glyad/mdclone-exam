using System.Threading;
using System.Threading.Tasks;

namespace MdClone.Model.Contracts
{
    public interface IFileSearchService
    {
        Task<IFileModel[]> GetFiles(string path, IFileTypeModel fileType, CancellationToken ct = default);
    }
}