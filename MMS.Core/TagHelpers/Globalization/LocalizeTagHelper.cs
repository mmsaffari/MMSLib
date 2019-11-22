using Microsoft.AspNetCore.Razor.TagHelpers;
using MMS.Core.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.TagHelpers.Globalization {
	/// <summary>
	/// Localization tag helper, localize text inside <![CDATA[<localize>Hello</localize>]]>
	/// </summary>
	public class LocalizeTagHelper : LocalizationTagHelperBase {
		/// <summary>
		/// inject SharedCultureLocalizer
		/// </summary>
		/// <param name="loc"></param>
		public LocalizeTagHelper(SharedCultureLocalizer loc) : base(loc) {
		}

		/// <summary>
		/// process localize tag helper
		/// </summary>
		/// <param name="context"></param>
		/// <param name="output"></param>
		/// <returns></returns>
		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
			//replace <localize> tag with <span>
			output.TagName = "";
			await base.ProcessAsync(context, output);
		}
	}
}
