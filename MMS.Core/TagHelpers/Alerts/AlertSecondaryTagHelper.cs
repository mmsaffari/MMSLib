using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace MMS.Core.TagHelpers.Alerts {
	/// <summary>
	/// Create secondary alert
	/// Alert contents must be replaced between alert tags e.g. <![CDATA[<alert-success>job done!</alert-success>]]>
	/// </summary>
	public class AlertSecondaryTagHelper : AlertTagHelper {
		/// <summary>
		/// Create secondary alert
		/// </summary>
		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
			base.Style = AlertStyles.Secondary;
			await base.ProcessAsync(context, output);
		}
	}
}
