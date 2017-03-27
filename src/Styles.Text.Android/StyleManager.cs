using System;
using System.Collections.Generic;
using System.Text;
using Android.Widget;

namespace Styles.Text
{
	public class StyleManager : StyleManagerBase
	{
		public StyleManager(ITextStyle instance) : base(instance)
		{
		}

		override protected IViewStyle CreateViewStyle(object target, string styleID, string text, List<CssTag> customTags, bool useExistingStyles = true, bool enableHtmlEditing = false)
		{
			IViewStyle viewStyle = new ViewStyle(_instance, target as TextView, styleID, text, true)
			{
				CustomTags = customTags,
				EnableHtmlEditing = enableHtmlEditing
			};

			return viewStyle;
		}
	}
}



