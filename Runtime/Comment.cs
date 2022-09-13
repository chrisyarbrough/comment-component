namespace Xarbrough.CommentComponent
{
	using JetBrains.Annotations;
	using UnityEngine;

	/// <summary>
	/// A component which adds a text for display in the inspector.
	/// </summary>
	public sealed class Comment : MonoBehaviour
	{
		[Tooltip("The text content to display.")]
		[UsedImplicitly]
		[SerializeField]
		private string text = string.Empty;

		[Tooltip("The icon to show next to the text.")]
		[UsedImplicitly]
		[SerializeField]
		private IconType iconType = IconType.None;

		internal enum IconType
		{
			None,
			Info,
			Warning
		}

		internal static string TextPropertyName => nameof(text);
		internal static string IconTypePropertyName => nameof(iconType);
	}
}
