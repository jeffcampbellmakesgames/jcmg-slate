/*
Slate
Copyright (C) Jeff Campbell - All Rights Reserved
Unauthorized copying of this file, via any medium is strictly prohibited
Proprietary and confidential
Written by Jeff Campbell <mirraraenn@gmail.com>, 2022
*/

using NaughtyAttributes;
using UnityEngine;

namespace JCMG.Slate
{
	/// <summary>
	/// A component that handles any UX reactions to a panel being shown or hidden.
	/// </summary>
	[RequireComponent(typeof(UIScreen))]
	[AddComponentMenu("JCMG/Slate/UIScreenTransitionController")]
	public sealed class UIScreenTransitionController : MonoBehaviour
	{
		/// <summary>
		/// The type of transition object that should be assigned.
		/// </summary>
		private enum TransitionSourceType
		{
			MonoBehaviour = 0,
			ScriptableObject = 1
		}

		/// <summary>
		/// Returns true if using a scriptable transition for show, otherwise false.
		/// </summary>
		internal bool IsUsingShowScriptableTransition =>
			_showTransitionSourceType == TransitionSourceType.ScriptableObject;

		/// <summary>
		/// Returns true if using a scriptable transition for hide, otherwise false.
		/// </summary>
		internal bool IsUsingHideScriptableTransition =>
			_showTransitionSourceType == TransitionSourceType.ScriptableObject;

		[BoxGroup(RuntimeConstants.UI_REFS)]
		[SerializeField, Required]
		private UIScreen _screen;

		[Space(5)]
		[BoxGroup("Show Configuration")]
		[SerializeField]
		private TransitionSourceType _showTransitionSourceType;

		[BoxGroup("Show Configuration")]
		[ShowIf("IsUsingShowScriptableTransition")]
		[SerializeField]
		private ScreenTransitionBase showScreenTransition;

		[BoxGroup("Show Configuration")]
		[HideIf("IsUsingShowScriptableTransition")]
		[SerializeField]
		private BehaviourScreenTransitionBase showBehaviourScreenTransition;

		[BoxGroup("Hide Configuration")]
		[SerializeField]
		private TransitionSourceType _hideTransitionSourceType;

		[BoxGroup("Hide Configuration")]
		[ShowIf("IsUsingHideScriptableTransition")]
		[SerializeField]
		private ScreenTransitionBase hideScreenTransition;

		[BoxGroup("Hide Configuration")]
		[HideIf("IsUsingHideScriptableTransition")]
		[SerializeField]
		private BehaviourScreenTransitionBase hideBehaviourScreenTransition;

		/// <summary>
		/// Plays a transition to show the attached <see cref="UIScreen"/> instance; upon completion,
		/// <paramref name="onShowComplete"/> will be invoked.
		/// </summary>
		public void AnimateShowReaction(ScreenTransitionCallback onShowComplete)
		{
			var showScreenReaction = GetShowScreenTransition();
			if (showScreenReaction != null)
			{
				showScreenReaction.PlayTransition(onShowComplete);
			}
			else
			{
				onShowComplete?.Invoke();
			}
		}

		/// <summary>
		/// Plays a transition to hide the attached <see cref="UIScreen"/> instance; upon completion,
		/// <paramref name="onHideComplete"/> will be invoked.
		/// </summary>
		public void AnimateHideReaction(ScreenTransitionCallback onHideComplete)
		{
			var hideScreenReaction = GetHideScreenTransition();
			if (hideScreenReaction != null)
			{
				hideScreenReaction.PlayTransition(onHideComplete);
			}
			else
			{
				onHideComplete?.Invoke();
			}
		}

		/// <summary>
		/// Returns the <see cref="IScreenTransition"/> in-use for this instance for showing the attached
		/// <see cref="UIScreen"/>.
		/// </summary>
		private IScreenTransition GetShowScreenTransition()
		{
			return _showTransitionSourceType == TransitionSourceType.ScriptableObject
				? showScreenTransition
				: showBehaviourScreenTransition;
		}

		/// <summary>
		/// Returns the <see cref="IScreenTransition"/> in-use for this instance for hiding the attached
		/// <see cref="UIScreen"/>.
		/// </summary>
		private IScreenTransition GetHideScreenTransition()
		{
			return _hideTransitionSourceType == TransitionSourceType.ScriptableObject
				? hideScreenTransition
				: hideBehaviourScreenTransition;
		}
	}
}
