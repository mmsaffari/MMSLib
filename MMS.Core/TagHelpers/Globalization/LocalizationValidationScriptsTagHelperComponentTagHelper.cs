using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using MMS.Core.TagHelpers.Globalization.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MMS.Core.TagHelpers.Globalization {
	/// <summary>
	/// Creates localization validation scripts tag to place client side validiation scripts inside
	/// </summary>
	[HtmlTargetElement("localization-validation-scripts")]
	public class LocalizationValidationScriptsTagHelperComponentTagHelper : TagHelperComponentTagHelper {

		/// <summary>
		/// Default script source
		/// </summary>
		public static ScriptSource DEFAULT_SOURCE => ScriptSource.JsDeliver;
		/// <summary>
		/// Default JsDeliver CLDR version
		/// </summary>
		public static string DEFAULT_CLDR_VERSION => "35.1.0";

		/// <summary>
		/// (Optional) Defines where to load scripts from, Local [under <![CDATA[~/wwwroot/lib/cldr-data/main/<culture>]]>] or JsDelivr.
		/// <para>Default: JsDelivr</para>
		/// </summary>
		[HtmlAttributeName("source")]
		public ScriptSource Source { get; set; } = DEFAULT_SOURCE;

		/// <summary>
		/// (Optional) Set cldr version to load.
		/// <para>Default: 35.1.0</para>
		/// </summary>
		[HtmlAttributeName("cldr-core-version")]
		public string CldrVersion { get; set; } = DEFAULT_CLDR_VERSION;

		/// <summary>
		/// creates localization validation scripts tag to place client side validiation scripts inside
		/// </summary>
		public LocalizationValidationScriptsTagHelperComponentTagHelper(ITagHelperComponentManager manager, ILoggerFactory loggerFactory) : base(manager, loggerFactory) {
		}
	}
}
