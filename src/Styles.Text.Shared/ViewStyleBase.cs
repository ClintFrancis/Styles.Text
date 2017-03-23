using System;
using System.Collections.Generic;

namespace Styles.Text
{
	public interface IViewStyle
	{
		string StyleID { get; }

		string TextValue { get;}

		List<CssTag> CustomTags { get; set; }

		bool ContainsHtml { get; }

		bool EnableHtmlEditing { get; set; }

		void UpdateText(string value = null);

		void UpdateFrame();

		void UpdateDisplay();

		void Dispose();
	}

	public abstract class ViewStyleBase<T>:IViewStyle, IDisposable
	{
		public string StyleID { get; protected set; }

		public string TextValue { get; protected set; }

		public List<CssTag> CustomTags { get; set; }

		public bool ContainsHtml { get; protected set; }

		protected string _rawText;

		ITextStyle _instance;

		public abstract bool EnableHtmlEditing
		{
			get;
			set;
		}

		public T Target { get; protected set; }

		public ViewStyleBase(ITextStyle instance, T target, string styleID, string text)
		{
			_instance = instance;

			StyleID = styleID;
			Target = target;
			UpdateText(text);
		}

		public abstract void UpdateText(string value = null);

		public abstract void UpdateFrame();

		public void UpdateDisplay()
		{
			_instance.Style(Target, StyleID, _rawText, CustomTags);
		}

		public abstract void Dispose();
	}
}
