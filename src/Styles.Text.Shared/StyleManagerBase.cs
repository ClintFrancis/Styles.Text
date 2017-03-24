using System;
using System.Collections.Generic;

namespace Styles.Text
{
	public abstract class StyleManagerBase : IStyleManager
	{
		protected Dictionary<object, IViewStyle> _views;
		protected ITextStyle _instance;

		public StyleManagerBase(ITextStyle instance)
		{
			_instance = instance;
			_views = new Dictionary<object, IViewStyle>();
			_instance.StylesChanged += TextStyle_Instance_StylesChanged;
		}

		public T Create<T>(string styleID, string text = "", List<CssTag> customTags = null, bool useExistingStyles = true, bool enableHtmlEditing = false)
		{
			var target = _instance.Create<T>(styleID, text, customTags, useExistingStyles);
			_instance.SetBaseStyle(styleID, ref customTags);

			var viewStyle = CreateViewStyle(target, styleID, text, customTags, useExistingStyles, enableHtmlEditing);
			_views.Add(target, viewStyle);

			return target;
		}

		public void Add<T>(T target, string styleID, string text = "", List<CssTag> customTags = null, bool useExistingStyles = true, bool enableHtmlEditing = false)
		{
			// Set the base style for the field
			_instance.SetBaseStyle(styleID, ref customTags);

			var viewStyle = CreateViewStyle(target, styleID, text, customTags, useExistingStyles, enableHtmlEditing);

			_views.Add(target, viewStyle);
			viewStyle.UpdateText();
			viewStyle.UpdateDisplay();
		}

		protected abstract IViewStyle CreateViewStyle(object target, string styleID, string text, List<CssTag> customTags, bool useExistingStyles = true, bool enableHtmlEditing = false);

		public void UpdateText(object target, string text)
		{
			var viewStyle = _views[target];
			if (viewStyle == null)
			{
				return;
			}

			viewStyle.UpdateText(text);
			viewStyle.UpdateDisplay();
		}

		/// <summary>
		/// Updates the styling and display of all registered text containers
		/// </summary>
		public void UpdateAll()
		{
			// Update the Attrib strings first as they can take some time
			foreach (var item in _views.Values)
			{
				item.UpdateText();
			}

			// Update the displays after so they change all at once
			foreach (var item in _views.Values)
			{
				item.UpdateDisplay();
			}
		}

		/// <summary>
		/// Updates the frames of any text containers with line heights smaller than the fonts default
		/// </summary>
		public void UpdateFrames()
		{
			// Update the frames of any linespaced itemss
			foreach (var item in _views.Values)
			{
				item.UpdateFrame();
			}
		}

		/// <summary>
		/// Releases all resource used by the <see cref="Occur.TextStyles.iOS.StyleManager"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Occur.TextStyles.iOS.StyleManager"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Occur.TextStyles.iOS.StyleManager"/> in an unusable state.
		/// After calling <see cref="Dispose"/>, you must release all references to the
		/// <see cref="Occur.TextStyles.iOS.StyleManager"/> so the garbage collector can reclaim the memory that the
		/// <see cref="Occur.TextStyles.iOS.StyleManager"/> was occupying.</remarks>
		public void Dispose()
		{
			foreach (var item in _views.Values)
			{
				item.Dispose();
			}

			_views.Clear();
			_views = null;

			_instance.StylesChanged -= TextStyle_Instance_StylesChanged;
		}

		void TextStyle_Instance_StylesChanged(object sender, EventArgs e)
		{
			UpdateAll();
		}

	}
}

