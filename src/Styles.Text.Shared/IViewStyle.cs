using System;
using System.Collections.Generic;

namespace Styles.Text
{
	public interface IViewStyle:IDisposable
	{
		string StyleID { get; }

		string TextValue { get; }

		List<CssTag> CustomTags { get; set; }

		bool ContainsHtml { get; }

		bool EnableHtmlEditing { get; set; }

		void UpdateText(string value = null);

		void UpdateFrame();

		void UpdateDisplay();
	}
}
