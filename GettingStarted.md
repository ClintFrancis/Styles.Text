# Getting Started with TextStyle

TextStyle allows you to effortlessly create, manage and style text in iOS and Android using CSS style sheets.

 * Create rich HTML text displays with full CSS styling
 * Style new or existing text displays (UILabel, UITextView, UITextField)
 * Manage and display style changes on existing fields
 * Change text styles at runtime
 * Light-weight and simple to use

---

**Load the CSS file**
Before any text displays are creates, TextStyle needs the CSS style set.
```csharp
var style = File.ReadAllText ("StyleOne.css");
TextStyle.Instance.SetCSS (style);
```


**Create an iOS UILabel using the 'h1' css style**
Using TextStyle you can easily create new text displays. Simply specify the type of field you wish to create (UILabel, UITextView, UITextField) and pass in the name of the CSS selector. You can optionally pass text to populate the display, and add custom CSS tags.
```csharp
var headingLabel = TextStyle.Create<UILabel> ("h1", "Hello World");
```

**Style an existing text display**
To style existing text displays pass TextStyle a reference to the display, the name of the CSS selector. You can optionally provide new text but if none is supplied TextStyle will use any text that has already been set on the display.

```csharp
var headingLabel = new UILabel();
headingLabel.Text = "Hello World";

TextStyle.Style<UILabel> (headingLabel, "h1");
```

**Using Custom CSS Tags**
Custom CSS tags can be used to either create a new CSS rule or alter an existing rule. If a rule has not been declared in the original CSS it is added to the text display,  if the rule has already been declared the new style will overwrite any existing properties of the existing rule.

```csharp
var htmlText = "This is a <spot>subtitle</spot>";
var customTags = new List<CssTagStyle> (){
	new CssTagStyle ("spot"){ CSS = "spot{color:#ff0000}" }
};
var subtitleLabel = TextStyle.Create<UILabel> ("h2", htmlText, customTags);
```

**Mapping CSS Rules**
In some cases it may be useful to remap an HTML tag to a new CSS rule. 
For example if we want a ``<span>`` tag to appear with the same properties as an ``<h3>`` tag we create a new ``CssTagStyle`` with the TagID of "span" and the StyleID of "h3"  

```csharp
var htmlText = "This is a <span>subtitle</span>";
var customTags = new List<CssTagStyle> (){
	new CssTagStyle ("span"){ StyleID = "h3" }
};
var subtitleLabel = TextStyle.Create<UITextView> ("body", htmlText, customTags);
```

**Custom Mapping CSS Rules**
In addition to mapping CSS rules we can specify additional properties on a ``CssTagStyle``  instance. Any custom CSS passed to the ``CssTagStyle``will only be applied to the text display and does not alter the stored CSS declaration.

```csharp
var customSpanTag = new CssTagStyle ("span"){ 
	StyleID = "h3", 
	CSS = "h3{color:#ff0000}"
};
```

**StyleManager**
The ``StyleManager`` is used to manage multiple text displays contained on a view. If ``TextStyle`` loads a new CSS file, the ``StyleManager`` is notified and will update all of its registered text displays.

```csharp
var styleManager = new StyleManager ();
styleManager.Add (labelOne, "h1");
styleManager.Add (labelTwo, "h2");
styleManager.Add (body, "body", "Lorem ipsum facto");
```

**Updating displays using the StyleManager**
To update the value of a text display using the ``StyleManager`` simple call the ``UpdateText`` method passing a reference to the field and the new string. The ``StyleManager`` will update the display using its stored settings. 

```csharp
styleManager.UpdateText (labelOne, "Hello world this is the new text to use.");
```

**HTML NSAttributedString**
TextStyle can be used to format HTML into NSAttributed strings. 
Simply pass the CreateHtmlString method the HTML you want formatted along with a list of ``CssTagStyle`` instances that define the styles.

```csharp
var customTags = new List<CssTagStyle> (){
	new CssTagStyle ("body"){ StyleID = "body" },
	new CssTagStyle ("b"){ StyleID = "b" },
	new CssTagStyle ("i"){ CSS = @"i{font-family: 'Avenir-Heavy';}" }
};

var html = "<b>hello world</b><br/>This is a <i>test</i> of the emergency broadcast system.";

var attributedString = TextStyle.CreateHtmlString (html, customTags);
```

**Standard NSAttributedString**
Standard text can also be styled using the ``textStyle.CreateStyledString()`` method. Passing the selector name and text to style will return an ``NSAttributedString`` value.
```csharp
var message = "Hello world, this is a test"

var attributedString = TextStyle.CreateStyledString ("h1", message);
```

**Special CSS properties**
TextStyle CSS declarations can also include the custom CSS property  ``lines`` which will set the number of lines on a text display. Using a value of 0 will make the text display multi-line.
```csharp
var parameters = new TextStyleParameters(){
	Lines = 0
};
```

**Hex UIColor extension**
The ``TextStyle`` library includes an extension for converting any ``UIColor`` to a CSS hex string.
This can be used to dynamically change color values of a CSS style. 

```csharp
// Returns #ff0000
var redHexString = Colors.Red.ToHex ();

var htmlText = "This is a <span>subtitle</span>";
var customTags = new List<CssTagStyle> () {
	new CssTagStyle ("span"){ CSS = "span{color:" + redHexString + "}" }
};
var subtitleLabel = TextStyle.Create<UILabel> ("h2", htmlText, customTags);

```