using MdClone.Data.Contracts.Providers;

namespace MdClone.Data.Real.Providers
{
    internal sealed class SupportedFormatInfo : ISupportedFormatInfo
    {
        public SupportedFormats SupportedFormatKey { get; set; }
        
        public string SupportedFormatName { get; set; }

        public string[] AllowedFileExtensions { get; set; }
    }
}