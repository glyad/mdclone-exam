using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MdClone.Model.Contracts;

namespace MdClone.Model
{
    internal class FileSearchService : IFileSearchService
    {
        Task<IFileModel[]> IFileSearchService.GetFiles(string path, IFileTypeModel fileType, CancellationToken ct) =>
            Task.Run(() =>
            {
                var fileModels = Directory.EnumerateFiles(path, fileType.Filter).Select(x => new FileModel {Name = x} as IFileModel).ToArray();
                ct.ThrowIfCancellationRequested();
                return fileModels;
            }, ct);
    }
}