﻿using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

using MMS.Core.Localization.DataAnnotations.Attributes;

using System;
using System.Collections.Generic;
using System.Text;

namespace MMS.Core.Localization.DataAnnotations.Adapters {
	internal class ExStringLengthAttributeAdapter : AttributeAdapterBase<ExStringLengthAttribute> {
		private readonly int MaxLenght;
		public ExStringLengthAttributeAdapter(ExStringLengthAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer) {
			MaxLenght = attribute.MaximumLength;
		}

		public override void AddValidation(ClientModelValidationContext context) {
			if (context == null)
				throw new NullReferenceException(nameof(context));

			MergeAttribute(context.Attributes, "data-val", "true");
			MergeAttribute(context.Attributes, "data-val-length", GetErrorMessage(context));
			MergeAttribute(context.Attributes, "data-val-length-max", $"{MaxLenght}");
		}

		public override string GetErrorMessage(ModelValidationContextBase validationContext) {
			if (validationContext == null)
				throw new NullReferenceException(nameof(validationContext));

			return GetErrorMessage(validationContext.ModelMetadata, validationContext.ModelMetadata.GetDisplayName(), MaxLenght);
		}
	}
}
