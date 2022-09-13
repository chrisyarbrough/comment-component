namespace Xarbrough.CommentComponent.Editor
{
	using UnityEngine;

	internal interface IRemovalReport
	{
		void Record(GameObject gameObject);
		bool CreateMessage(out string message);
	}
}
