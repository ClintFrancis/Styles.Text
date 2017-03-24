using System;
using System.Collections.Generic;

namespace Styles.Text
{
	public interface IStyleManager : IDisposable
	{
		T Create<T>(string styleID, string text = "", List<CssTag> customTags = null, bool useExistingStyles = true, bool enableHtmlEditing = false);
		void Add<T>(T target, string styleID, string text = "", List<CssTag> customTags = null, bool useExistingStyles = true, bool enableHtmlEditing = false);
		void UpdateText(object target, string text);
		void UpdateAll();
		void UpdateFrames();
	}
}
