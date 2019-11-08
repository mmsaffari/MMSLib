using Microsoft.AspNetCore.Razor.TagHelpers;
using MMS.Core.TagHelpers.Alerts;
using System.Threading.Tasks;

namespace MMS.Core.TagHelpers {
	/// <summary>
	/// Create light alert
	/// Alert contents must be replaced between alert tags e.g. <![CDATA[<alert-success>job done!</alert-success>]]>
	/// </summary>
	public class AlertLightTagHelper : AlertTagHelper {
		/// <summary>
		/// Create light alert
		/// </summary>
		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
			base.Style = AlertStyles.Light;
			await base.ProcessAsync(context, output);
		}
	}
}
