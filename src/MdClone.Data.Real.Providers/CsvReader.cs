using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MdClone.Data.Contracts.Providers;

namespace MdClone.Data.Real.Providers
{
    [Provides("Comma-separated values", SupportedFormats.Csv, "*.csv")]
    internal sealed class CsvReader : IDataReader
    {
        private const char Delimiter = ',';

        private readonly string _filename;

        public CsvReader(string filename)
        {
            _filename = filename;
            Load();
        }

        public string[] Header { get; private set; }

        public DataRow[] Rows { get; private set; }

        private string NextWord(string line, ref int index)
        {
            while (index < line.Length && line[index] == ' ')
            {
                index += 1;
            }

            if (index >= line.Length)
            {
                return null;
            }

            var isQuotes = false;
            var result = "";
            while (index < line.Length)
            {
                var ch = line[index];
                if (ch == '"')
                {
                    if (isQuotes)
                    {
                        if (index < line.Length - 1 && line[index + 1] == '"')
                        {
                            index += 1;
                            result += '"';
                        }
                        else
                        {
                            isQuotes = false;
                        }
                    }
                    else
                    {
                        isQuotes = true;
                    }

                    index += 1;
                    continue;
                }

                if (ch == Delimiter && !isQuotes)
                {
                    index += 1;
                    break;
                }

                result += ch;
                index += 1;
            }

            return result;
        }

        private string[] SplitLine(string line)
        {
            var result = new List<string>();

            var index = 0;
            var item = NextWord(line, ref index);
            while (item != null)
            {
                result.Add(item);
                item = NextWord(line, ref index);
            }

            return result.ToArray();
        }

        private void Load()
        {
            var lines = File.ReadLines(_filename, Encoding.UTF7).ToArray();
            Header = SplitLine(lines[0]);
            Rows = lines.Skip(1).Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => new DataRow(SplitLine(x))).ToArray();
        }
    }
}