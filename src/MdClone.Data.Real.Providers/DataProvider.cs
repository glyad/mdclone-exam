using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using MdClone.Data.Contracts.Dto;
using MdClone.Data.Contracts.Providers;

namespace MdClone.Data.Real.Providers
{
	[UsedImplicitly]
	internal sealed class DataProvider : IDataProvider
    {
	    public IEnumerable<ISupportedFormatInfo> GetSupportedFormats()
        {
            var types = Assembly.GetAssembly(GetType())
                .GetTypes()
                .Where(type => type.GetInterfaces().Any(i => i == typeof(IDataReader)))
                .Where(type => type.GetCustomAttribute<ProvidesAttribute>() != null)
                .ToArray();

            return types.Select(type =>
            {
                var attr = type.GetCustomAttribute<ProvidesAttribute>();
                return new SupportedFormatInfo(attr.Format, attr.Name, attr.FileExtensions);
            });
        }

	    public TableDataDto LoadData(string filename)
        {
            var csv = new CsvReader(filename);

            var result = new TableDataDto
            {
                Header = csv.Header,
                Rows = csv.Rows.Select(row => new RowDataDto
                {
                    Items = row.Values
                        .Select((value, i) => new ItemDataDto
                        {
                            Header = csv.Header[i], 
                            Value = value
                        })
                        .ToArray()
                }).ToArray()
            };

            return result;
        }
    }
}