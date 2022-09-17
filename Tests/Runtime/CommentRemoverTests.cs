namespace Xarbrough.CommentComponent.Tests
{
	using NUnit.Framework;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.SceneManagement;
	using UnityEngine.TestTools;

	[Category(nameof(CommentComponent))]
	internal class CommentRemoverTests
	{
		private static bool IsCommentRemoverDisabled()
		{
#if DISABLE_COMMENTREMOVER
			return true;
#else
			return false;
#endif
		}

		[UnityTest]
		[UnityPlatform(exclude = new[]
		{
			RuntimePlatform.WindowsEditor,
			RuntimePlatform.OSXEditor,
			RuntimePlatform.LinuxEditor,
		})]
		public IEnumerator NoCommentComponentsAreFoundInPlayer()
		{
			if (IsCommentRemoverDisabled())
			{
				Assert.Ignore("This test does not need to run because the " +
				              "scripting define 'DISABLE_COMMENTREMOVER' is set.");
			}

			// ReSharper disable once Unity.LoadSceneUnknownSceneName
			// Added during build process by CommentRemoverTestsBuildModifier in editor assembly.
			const string sceneName = "CommentRemoverTestsScene";

			int buildIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);

			if (buildIndex == -1)
				Assert.Inconclusive("Failed to load test scene: " + sceneName);

			yield return SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);

			var scene = SceneManager.GetSceneByBuildIndex(buildIndex);
			var sceneRoots = new List<GameObject>();
			scene.GetRootGameObjects(sceneRoots);

			foreach (GameObject root in sceneRoots)
			{
				Assert.IsNull(root.GetComponentInChildren<Comment>(),
					$"A comment component was found in scene {scene.name}, but there should be " +
					"no comments left in the player after removing them during the build.");
			}
		}
	}
}
