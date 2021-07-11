namespace MdClone.Data.Real.Providers
{
    internal readonly struct DataRow
    {
        public DataRow(string[] values)
        {
            Values = values;
        }

        public string[] Values { get; }
    }
}