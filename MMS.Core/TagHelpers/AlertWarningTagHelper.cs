using Microsoft.AspNetCore.Razor.TagHelpers;
using MMS.Core.TagHelpers.Alerts;
using System.Threading.Tasks;

namespace MMS.Core.TagHelpers {
	/// <summary>
	/// Create warning alert
	/// Alert contents must be replaced between alert tags e.g. <![CDATA[<alert-success>job done!</alert-success>]]>
	/// </summary>
	public class AlertWarningTagHelper : AlertTagHelper {
		/// <summary>
		/// Create danger alert
		/// </summary>
		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
			base.Style = AlertStyles.Warning;
			await base.ProcessAsync(context, output);
		}
	}
}
