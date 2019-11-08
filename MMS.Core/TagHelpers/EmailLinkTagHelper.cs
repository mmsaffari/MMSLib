using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.TagHelpers {
	/// <summary>
	/// <para>Creates an email link with circled check mark
	/// if the email is confirmed; otherwise shows an exclamation mark.</para>
	/// <para>Uses Font-Awesome by default; but can be changed.</para>
	/// </summary>
	public class EmailLinkTagHelper : TagHelper {
		/// <summary>
		/// <para>Indicates whether the email is confirmed or not or it's unknown.</para>
		/// </summary>
		public bool? EmailConfirmed { get; set; }

		/// <summary>
		/// Icon font's base class. Default is "fas"
		/// </summary>
		public string IconFontBaseClass { get; set; } = "fas";

		/// <summary>
		/// Confirmed check mark icon's css class. Default is "fa-check-circle"
		/// </summary>
		public string CheckCssClass { get; set; } = "fa-check-circle";

		/// <summary>
		/// <b>NOT Confirmed</b> check mark icon's css class. Default is "fa-exclamation"
		/// </summary>
		public string ExclamationCssClass { get; set; } = "fa-exclamation";

		/// <summary>
		/// process in creating email tag helper
		/// </summary>
		/// <param name="context"></param>
		/// <param name="output"></param>
		/// <returns></returns>
		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
			output.TagName = "a";                                 // Replaces <email> with <a> tag

			var content = await output.GetChildContentAsync();
			var target = content.GetContent();

			output.Attributes.SetAttribute("href", "mailto:" + target);
			output.Content.SetContent(target);

			if (EmailConfirmed.HasValue) {
				if (EmailConfirmed.Value)
					output.PreContent.SetHtmlContent($"<span class=\"{IconFontBaseClass} {CheckCssClass} text-success\"></span>");
				else
					output.PreContent.SetHtmlContent($"<span class=\"{IconFontBaseClass} {ExclamationCssClass} text-warning\"></span>");
			}
		}

	}
}
