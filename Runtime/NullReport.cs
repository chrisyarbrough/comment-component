namespace Xarbrough.CommentComponent.Editor
{
	using UnityEngine;

	/// <summary>
	/// Used when the report option is disabled.
	/// </summary>
	/// <remarks>
	/// Because <see cref="Record"/> might be called many times in larger projects
	/// this null object pattern implementation can save some performance over
	/// checking against the option in an if statement.
	/// </remarks>
	internal sealed class NullReport : IRemovalReport
	{
		public void Record(GameObject _)
		{
			/* Do nothing. */
		}

		public bool CreateMessage(out string message)
		{
			message = string.Empty;
			return false;
		}
	}
}
