/*
Slate
Copyright (C) Jeff Campbell - All Rights Reserved
Unauthorized copying of this file, via any medium is strictly prohibited
Proprietary and confidential
Written by Jeff Campbell <mirraraenn@gmail.com>, 2022
*/
using JCMG.Utility;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#elif USE_NAUGHTY_ATTR
using NaughtyAttributes;
#endif

namespace JCMG.Slate
{
	/// <summary>
	/// A simple global control singleton enabling easy access to block all screen input.
	/// </summary>
	[AddComponentMenu("JCMG/Slate/Singletons/UIBlocker")]
	public sealed class UIBlocker : Singleton<UIBlocker>
	{
		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.UI_REFS)]
		[Required]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(RuntimeConstants.UI_REFS)]
		[Required]
		#endif
		[SerializeField]
		private CanvasGroup _canvasGroup;

		/// <summary>
		/// Toggles interaction with the UI on or off based on <paramref name="isOn"/>.
		/// </summary>
		public void ToggleInput(bool isOn)
		{
			_canvasGroup.blocksRaycasts = !isOn;
			_canvasGroup.interactable = !isOn;
		}
	}
}
