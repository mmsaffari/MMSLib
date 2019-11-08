using Microsoft.AspNetCore.Razor.TagHelpers;
using MMS.Core.TagHelpers.Alerts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Core.TagHelpers {
	/// <summary>
	/// Create primary alert
	/// Alert contents must be replaced between alert tags e.g. <![CDATA[<alert-success>job done!</alert-success>]]>
	/// </summary>
	public class AlertPrimaryTagHelper : AlertTagHelper {
		/// <summary>
		/// Create primary alert
		/// </summary>
		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
			base.Style = AlertStyles.Primary;
			await base.ProcessAsync(context, output);
		}
	}
}
