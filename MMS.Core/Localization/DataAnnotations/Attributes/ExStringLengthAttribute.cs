using MMS.Core.Localization.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MMS.Core.Localization.DataAnnotations.Attributes {
	/// <summary>
	/// Specifies the minimum and maximum length of characters that are allowed in a data field. 
	/// And provides a localized error message
	/// </summary>
	public sealed class ExStringLengthAttribute : StringLengthAttribute {
		/// <summary>
		/// Initializes a new instance of the LazZiya.ExpressLocalization.DataAnnotations.ExStringLengthAttribute 
		/// class by using a specified maximum length.
		/// </summary>
		/// <param name="maximumLength">The maximum length of a string.</param>
		public ExStringLengthAttribute(int maximumLength) : base(maximumLength) {
			this.ErrorMessage = ErrorMessage ?? DataAnnotationsErrorMessages.StringLengthAttribute_ValidationError;
		}
	}
}
