namespace Xarbrough.CommentComponent.Editor.Tests
{
	using NUnit.Framework;
	using System;
	using UnityEngine;

	[Category(nameof(CommentComponent))]
	internal class EditSessionTests
	{
		[Test]
		public void Constructor_Throws_WhenNullTargetIsPassed()
		{
			Assert.Throws<ArgumentNullException>(() => { new EditSession(null); });
		}

		[Test]
		public void IsEditing_ReturnsDefaultValue_WhenOnlyDefaultIsSet(
			[Values(false, true)] bool defaultValue)
		{
			using (var target = ScriptableObject.CreateInstance<TestTarget>())
			{
				var session = new EditSession(target, defaultValue);
				Assert.AreEqual(defaultValue, session.IsEditing);
			}
		}

		[Test]
		public void IsEditing_StoresAssignedValue([Values(false, true)] bool value)
		{
			using (var target = ScriptableObject.CreateInstance<TestTarget>())
			{
				var session = new EditSession(target);
				session.IsEditing = value;
				Assert.AreEqual(value, session.IsEditing);
			}
		}

		[Test]
		public void IsEditing_StoresAssignedValue_DespiteDefault([Values(false, true)] bool value)
		{
			using (var target = ScriptableObject.CreateInstance<TestTarget>())
			{
				var session = new EditSession(target, defaultValue: !value);
				session.IsEditing = value;
				Assert.AreEqual(value, session.IsEditing);
			}
		}

		[Test]
		public void IsEditing_ReturnsTheSameValue_ForMultipleSessionInstances()
		{
			using (var target = ScriptableObject.CreateInstance<TestTarget>())
			{
				var first = new EditSession(target, defaultValue: false);
				first.IsEditing = true;
				var second = new EditSession(target, defaultValue: false);
				Assert.IsTrue(second.IsEditing);
			}
		}

		[Test]
		public void IsEditing_ReturnsIndependentValues_ForMultipleTargets()
		{
			using (var targetA = ScriptableObject.CreateInstance<TestTarget>())
			using (var targetB = ScriptableObject.CreateInstance<TestTarget>())
			{
				var sessionA = new EditSession(targetA, defaultValue: false);
				sessionA.IsEditing = true;
				var sessionB = new EditSession(targetB, defaultValue: false);
				Assert.IsFalse(sessionB.IsEditing);

				sessionA.IsEditing = false;
				sessionB.IsEditing = true;
				Assert.IsFalse(sessionA.IsEditing);
				Assert.IsTrue(sessionB.IsEditing);
			}
		}

		[Test]
		public void Erase_ResetsIsEditingToDefault()
		{
			using (var target = ScriptableObject.CreateInstance<TestTarget>())
			{
				var session = new EditSession(target, defaultValue: false);
				session.IsEditing = true;
				session.Clear();
				Assert.IsFalse(session.IsEditing);
			}
		}

		private class TestTarget : ScriptableObject, IDisposable
		{
			public void Dispose()
			{
				DestroyImmediate(this);
			}
		}
	}
}
