namespace Xarbrough.CommentComponent.Editor
{
	using UnityEditor;
	using UnityEngine.UIElements;

	internal static class CommentComponentSettings
	{
		public static bool LogRemovedComponents
		{
			get => EditorPrefs.GetBool(logRemovedComponentsKey, true);
			private set => EditorPrefs.SetBool(logRemovedComponentsKey, value);
		}

		private const string logRemovedComponentsKey = "CommentComponent.LogRemovedComments";

		[SettingsProvider]
		public static SettingsProvider CreateProvider()
		{
			return new SettingsProvider("Preferences/Comment Component", SettingsScope.User)
			{
				activateHandler = CreateUI
			};
		}

		private static void CreateUI(string searchContext, VisualElement root)
		{
			var toggle = new Toggle("Log Removed Comments");
			toggle.SetValueWithoutNotify(LogRemovedComponents);
			toggle.RegisterValueChangedCallback(evt => LogRemovedComponents = evt.newValue);
			root.Add(toggle);
		}
	}
}
