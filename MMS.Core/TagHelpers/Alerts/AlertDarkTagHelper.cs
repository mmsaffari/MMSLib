using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace MMS.Core.TagHelpers.Alerts {
	/// <summary>
	/// Create dark alert
	/// Alert contents must be replaced between alert tags e.g. <![CDATA[<alert-success>job done!</alert-success>]]>
	/// </summary>
	public class AlertDarkTagHelper : AlertTagHelper {
		/// <summary>
		/// Create dark alert
		/// </summary>
		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
			base.Style = AlertStyles.Dark;
			await base.ProcessAsync(context, output);
		}
	}
}
