namespace Xarbrough.CommentComponent.Editor.Tests
{
	using NUnit.Framework;
	using UnityEngine;
	using UnityEngine.TestTools;

	[Category(nameof(CommentComponent))]
	internal class RemovalReportTests
	{
		[Test]
		[UnityPlatform(include = new[]
		{
			RuntimePlatform.WindowsEditor,
			RuntimePlatform.OSXEditor,
			RuntimePlatform.LinuxEditor,
		})]
		public void Report_ContainsGameObjectAndSceneName()
		{
			var report = new RemovalReport();

			var go = new GameObject("SpecialTestGameObject");
			report.Record(go);

			bool hasContent = report.CreateMessage(out string message);

			Assert.IsTrue(hasContent);

			StringAssert.Contains(
				go.name, message, "Should include recorded GameObject name.");

			StringAssert.Contains(
				go.scene.name, message, "Should include recorded scene name.");
		}
	}
}
