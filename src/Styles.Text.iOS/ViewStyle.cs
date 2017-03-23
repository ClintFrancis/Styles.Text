using System;
using System.Collections.Generic;
using Foundation;
using Styles.Text;
using Styles;
using UIKit;

namespace Styles.iOS
{
	[Foundation.Preserve (AllMembers = true)]
	class ViewStyle : ViewStyleBase<UITextField>, IDisposable
	{
		public NSAttributedString AttributedValue { get; private set; }

		bool _updateConstraints;

		TextStyle _instance;

		bool _enableHtmlEditing;
		override public bool EnableHtmlEditing {
			get { return _enableHtmlEditing; }
			set {
				_enableHtmlEditing = value;

				if (Target != null) {
					if (value) {
						Target.EditingChanged += TextEditingChanged;
						Target.EditingDidBegin += TextEditingStarted;
						Target.EditingDidEnd += TextEditingEnded;

					} else {
						Target.EditingChanged -= TextEditingChanged;
						Target.EditingDidBegin -= TextEditingStarted;
						Target.EditingDidEnd -= TextEditingEnded;
					}
				}
			}
		}

		public ViewStyle(ITextStyle instance, UITextField target, string styleID, string text, bool updateConstraints):base(instance, target, styleID, text)
		{
			_updateConstraints = updateConstraints;
		}

		override public void UpdateText (string value = null)
		{
			if (!String.IsNullOrEmpty (value)) {
				_rawText = value;
				ContainsHtml = (!String.IsNullOrEmpty (value) && Common.MatchHtmlTags.IsMatch (value));
			} else {
				return;
			}

			var style = _instance.GetStyle (StyleID);
			TextValue = TextStyle.ParseString (style, _rawText);

			AttributedValue = ContainsHtml ? _instance.CreateHtmlString (TextValue, CustomTags) : _instance.CreateStyledString (style, TextValue);
		}

		override public void UpdateFrame ()
		{
			var style = _instance.GetStyle (StyleID);

			// Offset the frame if needed
			if (_updateConstraints && style.LineHeight < 0f) {
				var heightOffset = style.GetLineHeightOffset ();
				var targetFrame = Target.Frame;
				targetFrame.Height = (nfloat)Math.Ceiling (targetFrame.Height) + heightOffset;

				if (Target.Constraints.Length > 0) {
					foreach (var constraint in Target.Constraints) {
						if (constraint.FirstAttribute == NSLayoutAttribute.Height) {
							constraint.Constant = targetFrame.Height;
							break;
						}
					}
				} else {
					Target.Frame = targetFrame;
				}
			}
		}

		override public void Dispose ()
		{
			EnableHtmlEditing = false;
			Target = null;
		}

		void TextEditingChanged (object sender, EventArgs e)
		{
			_rawText = Target.Text;
		}

		void TextEditingStarted (object sender, EventArgs e)
		{
			_instance.StyleUITextField (Target, StyleID, _rawText, ignoreHtml: true);
		}

		void TextEditingEnded (object sender, EventArgs e)
		{
			_instance.StyleUITextField (Target, StyleID, _rawText, CustomTags);
		}
	}
}

