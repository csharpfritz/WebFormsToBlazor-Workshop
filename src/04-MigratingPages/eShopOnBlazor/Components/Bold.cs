using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopOnBlazor.Components
{

	public class Bold : ComponentBase
	{

		[Parameter] public RenderFragment ChildContent { get; set; }

		[Parameter] public string CssClass { get; set; }

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{

			builder.OpenElement(0, "strong");
			builder.AddAttribute(1, "class", CssClass);
			builder.AddContent(2, ChildContent);
			builder.CloseElement();

			base.BuildRenderTree(builder);
		}

	}
}
