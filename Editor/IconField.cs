namespace Xarbrough.CommentComponent.Editor
{
	using UnityEngine.UIElements;

	internal class IconField : BindableElement, INotifyValueChanged<int>
	{
		public int value
		{
			get => _value;
			set
			{
				if (value == _value)
					return;

				int previousValue = _value;
				_value = value;

				using (var evt = ChangeEvent<int>.GetPooled(previousValue, value))
				{
					evt.target = this;
					this.SendEvent(evt);
				}
				RefreshIcon();
			}
		}

		private int _value;

		public IconField()
		{
			style.width = 34;
			style.height = 32;
			style.flexShrink = 0;
			RefreshIcon();
		}

		public void SetValueWithoutNotify(int newValue)
		{
			_value = newValue;
			RefreshIcon();
		}

		private void RefreshIcon()
		{
			base.ClearClassList();
			style.display = DisplayStyle.Flex;

			switch ((Comment.IconType)_value)
			{
				case Comment.IconType.None:
					style.display = DisplayStyle.None;
					break;
				case Comment.IconType.Info:
					AddToClassList(HelpBox.iconInfoUssClassName);
					break;
				case Comment.IconType.Warning:
					AddToClassList(HelpBox.iconwarningUssClassName);
					break;
			}
		}
	}
}
