namespace Xarbrough.CommentComponent.Editor
{
	using JetBrains.Annotations;
	using System;
	using UnityEditor;

	/// <summary>
	/// Stores the editing state for a provided target.
	/// </summary>
	/// <remarks>
	/// To store values across domain reload, the target must be persistent.
	/// This means, two session instances pointing to the same target instance
	/// will both return the same <see cref="IsEditing"/> value.
	/// </remarks>
	internal sealed class EditSession
	{
		private readonly string key;
		private readonly bool defaultValue;

		public EditSession([NotNull] UnityEngine.Object target, bool defaultValue = false)
		{
			if (target == null)
				throw new ArgumentNullException(nameof(target));

			this.key = "Xarbrough.CommentComponent.EditSession_" + target.GetInstanceID();
			this.defaultValue = defaultValue;
		}

		public void Clear()
		{
			SessionState.EraseBool(key);
		}

		public bool IsEditing
		{
			get => SessionState.GetBool(key, defaultValue);
			set => SessionState.SetBool(key, value);
		}
	}
}
