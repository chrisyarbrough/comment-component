namespace Xarbrough.CommentComponent.Editor
{
	using System;
	using UnityEditor;
	using UnityEditor.UIElements;
	using UnityEngine;
	using UnityEngine.UIElements;

	/// <summary>
	/// Draws an inspector for the <see cref="Comment"/> component with two modes.
	/// In readonly mode the text is shown together with an icon.
	/// In edit mode a textfield and dropdown allows modifications.
	/// </summary>
	[CustomEditor(typeof(Comment), editorForChildClasses: true, isFallback = true)]
	[CanEditMultipleObjects]
	public class CommentEditor : Editor
	{
		[SerializeField]
		private StyleSheet styleSheet;

		private EditSession editSession;
		private SerializedProperty textProp;
		private VisualElement contentContainer;

		public override VisualElement CreateInspectorGUI()
		{
			var root = CreateStyledRoot();

			textProp = serializedObject.FindProperty(Comment.TextPropertyName);

			contentContainer = new VisualElement();
			root.Add(contentContainer);

			editSession = new EditSession(target);

			if (ShouldStartInEditMode())
				BeginEditing();
			else
				EndEditing();

			return root;
		}

		private VisualElement CreateStyledRoot()
		{
			var root = new VisualElement { name = "root" };

			if (styleSheet != null)
				root.styleSheets.Add(styleSheet);
			else
			{
				root.Insert(0, new HelpBox(
					"Style sheet reference is not set on editor script",
					HelpBoxMessageType.Error));
			}
			return root;
		}

		private bool ShouldStartInEditMode()
		{
			// Start editing when the component is first added or has no value set
			// or when the editor is enabled after assembly reload.
			return string.IsNullOrEmpty(textProp.stringValue) || editSession.IsEditing;
		}

		private void BeginEditing()
		{
			editSession.IsEditing = true;
			contentContainer.RegisterCallback<KeyUpEvent>(HandleEndEditingKeyboardInput);
			ShowEditUI();
		}

		private void EndEditing()
		{
			editSession.IsEditing = false;
			contentContainer.UnregisterCallback<KeyUpEvent>(HandleEndEditingKeyboardInput);
			ShowCommentDisplay();
		}

		private void ShowCommentDisplay()
		{
			contentContainer.Clear();

			var container = new VisualElement { name = "container" };

			AddClickable(container, BeginEditing, MouseButton.LeftMouse, clickCount: 2);
			AddClickable(container, OpenContextMenu, MouseButton.RightMouse, clickCount: 1);

			container.tooltip = "Double-click to edit the text.\n" +
			                    "Press CTRL + Enter/Return to end editing.";

			var icon = new IconField();
			icon.bindingPath = Comment.IconTypePropertyName;
			container.Add(icon);

			var label = new Label
			{
				name = "comment-text",
				bindingPath = Comment.TextPropertyName
			};
			container.Add(label);

			contentContainer.Add(container);

			contentContainer.Bind(serializedObject);
		}

		private static void AddClickable(
			VisualElement target, Action action, MouseButton mouseButton, int clickCount)
		{
			var clickable = new Clickable(action);
			clickable.activators.Clear();
			clickable.activators.Add(new ManipulatorActivationFilter
			{
				button = mouseButton,
				clickCount = clickCount
			});
			target.AddManipulator(clickable);
		}

		private void OpenContextMenu()
		{
			var menu = new GenericMenu();
			menu.AddItem(new GUIContent("Edit"), false, BeginEditing);
			menu.ShowAsContext();
		}

		private void ShowEditUI()
		{
			contentContainer.Clear();

			var textArea = new TextField
			{
				bindingPath = Comment.TextPropertyName,
				multiline = true
			};
			contentContainer.Add(textArea);

			var dropdown = new EnumField
			{
				bindingPath = Comment.IconTypePropertyName,
				label = "Icon"
			};
			contentContainer.Add(dropdown);

			var closeButton = new Button(EndEditing) { name = "end-editing-button" };
			closeButton.Add(new Label("End Editing"));
			contentContainer.Add(closeButton);
			contentContainer.Bind(serializedObject);
		}

		private void HandleEndEditingKeyboardInput(KeyUpEvent evt)
		{
			if (evt.ctrlKey == false)
				return;

			if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
			{
				EndEditing();
			}
		}
	}
}
