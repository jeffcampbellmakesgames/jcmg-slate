using JCMG.Slate;
using UnityEngine;
using UnityEngine.UIElements;

namespace JCMG_Slate_Demo
{
	/// <summary>
	/// A <see cref="UIScreenDocumentBase"/> for a start screen.
	/// </summary>
	public sealed class StartUIScreenDocument : UIScreenDocumentBase
	{
		private Button _playButton;

		protected override void Awake()
		{
			base.Awake();

			_playButton = _uiDocument.rootVisualElement.Q<Button>("PlayButton");
			_playButton.clicked += OnPlayButtonClicked;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			_playButton.clicked -= OnPlayButtonClicked;
		}

		private void OnPlayButtonClicked()
		{
			// No-op currently
		}
	}
}
