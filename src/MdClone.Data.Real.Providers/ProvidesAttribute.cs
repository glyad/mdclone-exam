using System;
using MdClone.Data.Contracts.Providers;

namespace MdClone.Data.Real.Providers
{
	
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	internal sealed class ProvidesAttribute : Attribute
	{
		
		public ProvidesAttribute(string name, SupportedFormats format, params string[] fileExtensions)
        {
            Name = name;
			Format = format;
			FileExtensions = fileExtensions;
		}
        public string Name { get; }

		public SupportedFormats Format { get; }

        public string[] FileExtensions { get; }
	}

}