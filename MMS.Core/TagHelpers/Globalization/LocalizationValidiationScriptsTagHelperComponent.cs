using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MMS.Core.Properties;
using MMS.Core.TagHelpers.Globalization.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace MMS.Core.TagHelpers.Globalization {
	/// <summary>
	/// Inserts localized scripts requeired for client side validation into the relevant tag
	/// </summary>
	public class LocalizationValidationScriptsTagHelperComponent : TagHelperComponent {

		/// <summary>
		/// Default script culture
		/// </summary>
		public static string DEFAULT_CULTURE = "en";

		private readonly IWebHostEnvironment _hosting;

		/// <summary>
		/// Inserts localized scripts requeired for client side validation into the relevant tag
		/// </summary>
		/// <param name="hosting"></param>
		public LocalizationValidationScriptsTagHelperComponent(IWebHostEnvironment hosting) {
			_hosting = hosting;
		}

		/// <summary>
		/// Default order is 0 we change it to 1!
		/// </summary>
		public override int Order => 1;

		/// <summary>
		/// Generates the output
		/// </summary>
		/// <param name="context"></param>
		/// <param name="output"></param>
		public override void Process(TagHelperContext context, TagHelperOutput output) {
			if (string.Equals(context.TagName, "localization-validation-scripts", StringComparison.OrdinalIgnoreCase)) {
				//read source attribute
				var sourceAttribute = GetAttribute(context, "source", LocalizationValidationScriptsTagHelperComponentTagHelper.DEFAULT_SOURCE);

				//get the value of the source property
				Enum.TryParse<ScriptSource>(sourceAttribute.Value.ToString(), out ScriptSource _scriptSource);
				//assign relevant script file accordingly
				var _script = _scriptSource == ScriptSource.JsDeliver ? Resources.LocalizationValidationScripts_jsdeliver : Resources.LocalizationValidationScripts_local;

				//read cldr-core-version attribute
				var cldrCoreVersionAttribute = GetAttribute(context, "cldr-core-version", LocalizationValidationScriptsTagHelperComponentTagHelper.DEFAULT_CLDR_VERSION);
				var cldrCoreVersion = cldrCoreVersionAttribute.Value.ToString();

				var culture = _scriptSource == ScriptSource.JsDeliver ? CultureInfo.CurrentCulture.Name : GetCultureName();

				output.PostContent.AppendHtml(_script
					.Replace("{culture}", culture)
					.Replace("{cldr-core-version}", cldrCoreVersion)
				);
			}
		}

		private TagHelperAttribute GetAttribute(TagHelperContext context, string tagName, object defaultValue) {
			//try getting the attibute from the tag's attributes
			context.AllAttributes.TryGetAttribute(tagName, out TagHelperAttribute attribute);
			// If the attribute is not defined, return the default value.
			return attribute ?? new TagHelperAttribute(tagName, defaultValue);
		}

		/// <summary>
		/// find json files related to the current culture, if not found return parent culture, if not found return default culture.
		/// see ClientSideValidationScripts.html for how to configure paths
		/// </summary>
		/// <returns>culture name e.g. tr</returns>
		private string GetCultureName() {
			// use this pattern to check if the relevant json folder are available
			const string localePattern = "lib\\cldr-data\\main\\{0}";
			var currentCulture = CultureInfo.CurrentCulture;
			var cultureToUse = DEFAULT_CULTURE; //Default regionalisation to use

			if (Directory.Exists(Path.Combine(_hosting.WebRootPath, string.Format(localePattern, currentCulture.Name))))
				cultureToUse = currentCulture.Name;
			else if (Directory.Exists(Path.Combine(_hosting.WebRootPath, string.Format(localePattern, currentCulture.TwoLetterISOLanguageName))))
				cultureToUse = currentCulture.TwoLetterISOLanguageName;

			return cultureToUse;
		}
	}
}
