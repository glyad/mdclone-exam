using MdClone.Data.Contracts.Providers;

namespace MdClone.Data.Real.Providers
{
    internal sealed class SupportedFormatInfo : ISupportedFormatInfo
    {
	    internal SupportedFormatInfo(SupportedFormats key, string formatName, string[] allowedFileExtensions)
	    {
		    SupportedFormatKey = key;
		    SupportedFormatName = formatName;
		    AllowedFileExtensions = allowedFileExtensions;
	    }

	    public SupportedFormats SupportedFormatKey { get; }
        
        public string SupportedFormatName { get; }

        public string[] AllowedFileExtensions { get; }

		public override int GetHashCode() => SupportedFormatKey.GetHashCode();

		public override bool Equals(object obj)
		{
			return obj != null && SupportedFormatKey.Equals(((SupportedFormatInfo)obj).SupportedFormatKey);
		}
    }
}