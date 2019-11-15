using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.TagHelpers.PageControls {
	/// <summary>
	/// A display for phone number
	/// </summary>
	public class PhoneNumberTagHelper : TagHelper {
		/// <summary>
		/// Indicate wgether the phone number has been confirmed or not.
		/// </summary>
		public bool PhoneNumberConfirmed { get; set; }

		/// <summary>
		/// Process creating phone number tag helper
		/// </summary>
		/// <param name="context"></param>
		/// <param name="output"></param>
		/// <returns></returns>
		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
			output.TagName = "span";

			var content = await output.GetChildContentAsync();

			var target = content.GetContent();
			output.Content.SetContent(target.Replace("&#x2B;", "+"));
			output.Attributes.SetAttribute("dir", "ltr");

			if (PhoneNumberConfirmed)
				output.PreContent.SetHtmlContent("<span class=\"fas fa-check-circle text-success\"></span>");
			else
				output.PreContent.SetHtmlContent("<span class=\"fas fa-exclamation text-warning\"></span>");
		}

	}
}
