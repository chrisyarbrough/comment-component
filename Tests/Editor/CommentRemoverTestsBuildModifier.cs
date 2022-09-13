namespace Xarbrough.CommentComponent.Tests
{
	using JetBrains.Annotations;
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEditor.TestTools;

	[UsedImplicitly]
	internal class CommentRemoverTestsBuildModifier : ITestPlayerBuildModifier
	{
		public BuildPlayerOptions ModifyOptions(BuildPlayerOptions playerOptions)
		{
			var scenePaths = new List<string>(playerOptions.scenes);

			// CommentRemoverTestsScene.unity required by CommentRemoverTests.
			string path = AssetDatabase.GUIDToAssetPath("751855301ddf66f4ebe5d953de4d8ab1");
			scenePaths.Add(path);

			playerOptions.scenes = scenePaths.ToArray();

			return playerOptions;
		}
	}
}
