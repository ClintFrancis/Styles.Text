using System;
using System.Collections.Generic;
using Android.Text;
using Android.Widget;

namespace Styles.Text
{
	class ViewStyle : IViewStyle
	{
		public string StyleID { get; set; }

		public string TextValue { get; private set; }

		public ISpanned AttributedValue { get; private set; }

		public List<CssTag> CustomTags { get; set; }

		public TextView Target { get; set; }

		public bool ContainsHtml { get; private set; }

		string _rawText;

		bool _updateConstraints;

		TextStyle _instance;

		bool _enableHtmlEditing;
		public bool EnableHtmlEditing {
			get { return _enableHtmlEditing; }
			set {
				_enableHtmlEditing = value;

				if (Target != null) {
					if (value) {
						Target.TextChanged += TextEditingChanged;
						Target.FocusChange += TextFocusChanged;

					} else {
						Target.TextChanged -= TextEditingChanged;
						Target.FocusChange -= TextFocusChanged;
					}
				}
			}
		}

		public ViewStyle (ITextStyle instance, TextView target, string styleID, string text, bool updateConstraints)
		{
			_instance = instance as TextStyle;
			_updateConstraints = updateConstraints;
			_rawText = text;

			Target = target;
			StyleID = styleID;
			UpdateText (text);
		}

		public void UpdateText (string value = null)
		{
			if (!String.IsNullOrEmpty (value)) {
				_rawText = value;
				ContainsHtml = (!String.IsNullOrEmpty (value) && Common.MatchHtmlTags.IsMatch (value));
			} else {
				return;
			}

			var style = _instance.GetStyle (StyleID);
			TextValue = TextStyle.ParseString (style, _rawText);

			AttributedValue = ContainsHtml ? _instance.CreateHtmlString (TextValue, StyleID, CustomTags) : _instance.CreateStyledString (style, TextValue);
		}

		public void UpdateDisplay ()
		{
			_instance.Style (Target, StyleID, _rawText, CustomTags, true);
		}

		public void Dispose ()
		{
			EnableHtmlEditing = false;
			Target = null;
		}

		void TextEditingChanged (object sender, EventArgs e)
		{
			_rawText = Target.Text;
		}

		void TextFocusChanged (object sender, Android.Views.View.FocusChangeEventArgs e)
		{
			if (Target.IsFocused) {
				Target.Text = TextValue;
			} else {
				_instance.Style (Target, StyleID, _rawText, CustomTags);
			}
		}

		public void UpdateFrame()
		{
			// Does nothing on Android
		}
	}
}

