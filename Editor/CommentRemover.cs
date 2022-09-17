#if !DISABLE_COMMENTREMOVER
namespace Xarbrough.CommentComponent.Editor
{
	using System.Collections.Generic;
	using UnityEditor.Build;
	using UnityEditor.Build.Reporting;
	using UnityEngine;
	using UnityEngine.SceneManagement;

	/// <summary>
	/// Removes comment components during the build process.
	/// </summary>
	internal class CommentRemover : IProcessSceneWithReport,
	                                IPreprocessBuildWithReport,
	                                IPostprocessBuildWithReport
	{
		private readonly List<GameObject> sceneRoots = new List<GameObject>();

		private IRemovalReport removalReport;

		void IPreprocessBuildWithReport.OnPreprocessBuild(BuildReport _)
		{
			if (CommentComponentSettings.LogRemovedComponents)
				this.removalReport = new RemovalReport();
			else
				this.removalReport = new NullReport();
		}

		void IPostprocessBuildWithReport.OnPostprocessBuild(BuildReport _)
		{
			if (this.removalReport.CreateMessage(out string message))
				Debug.Log(message);
		}

		void IProcessSceneWithReport.OnProcessScene(Scene scene, BuildReport buildReport)
		{
			// The report is only valid during builds, but not during playmode
			// and comments should be preserved while testing in the editor.
			if (buildReport == null)
				return;

			scene.GetRootGameObjects(sceneRoots);

			foreach (GameObject root in sceneRoots)
			{
				foreach (var comment in root.GetComponentsInChildren<Comment>(
					includeInactive: true))
				{
					this.removalReport.Record(comment.gameObject);
					Object.DestroyImmediate(comment);
				}
			}
		}

		public int callbackOrder => 0;
	}
}
#endif
