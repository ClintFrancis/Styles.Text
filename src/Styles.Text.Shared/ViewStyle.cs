using System;
using System.Collections.Generic;

namespace Styles.Text
{
	public class ViewStyle<T>: IViewStyle
	{
		public ViewStyle(ITextStyle instance, T target, string styleID, string text, bool updateConstraints)
		{
		}

		public bool ContainsHtml
		{
			get
			{
				return true;
			}
		}

		public List<CssTag> CustomTags
		{
			get
			{
				return null;
			}

			set
			{
				var d = value;
			}
		}

		public bool EnableHtmlEditing
		{
			get
			{
				return false;
			}

			set
			{
				var d = value;
			}
		}

		public string StyleID
		{
			get
			{
				return null;
			}
		}

		public string TextValue
		{
			get
			{
				return null;
			}
		}

		public void Dispose(){}

		public void UpdateDisplay(){}

		public void UpdateFrame(){}

		public void UpdateText(string value = null){}
	}
}