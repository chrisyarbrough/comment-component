namespace Xarbrough.CommentComponent.Editor
{
	using System.Collections.Generic;
	using System.Text;
	using UnityEngine;

	/// <summary>
	/// Collects and logs information about removed <see cref="Comment"/> components.
	/// </summary>
	internal class RemovalReport : IRemovalReport
	{
		/// <summary>
		/// Maps from scene identifier to hierarchy paths of removed comment components.
		/// </summary>
		private readonly Dictionary<string, List<string>> logInfo =
			new Dictionary<string, List<string>>();

		public void Record(GameObject gameObject)
		{
			string sceneName = gameObject.scene.name;

			if (!logInfo.TryGetValue(sceneName, out List<string> paths))
			{
				paths = new List<string>();
				logInfo.Add(sceneName, paths);
			}

			string path = CreateHierarchyPath(gameObject.transform);
			paths.Add(path);
		}

		private static string CreateHierarchyPath(Transform transform)
		{
			string path = transform.name;
			while (transform.parent != null)
			{
				transform = transform.parent;
				path = transform.name + "/" + path;
			}
			return path;
		}

		public bool CreateMessage(out string message)
		{
			if (logInfo.Count == 0)
			{
				message = string.Empty;
				return false;
			}

			var builder = new StringBuilder();
			builder.AppendLine("Comment components were removed.");
			builder.AppendLine();

			foreach (var kvp in logInfo)
			{
				string sceneName = kvp.Key;
				List<string> removedCommentPaths = kvp.Value;

				builder.AppendLine(sceneName);
				foreach (string path in removedCommentPaths)
				{
					builder.Append("└ ").AppendLine(path);
				}
				builder.AppendLine();
			}

			message = builder.ToString();
			return message.Length > 0;
		}
	}
}
