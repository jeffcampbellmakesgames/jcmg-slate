/*
Slate
Copyright (C) Jeff Campbell - All Rights Reserved
Unauthorized copying of this file, via any medium is strictly prohibited
Proprietary and confidential
Written by Jeff Campbell <mirraraenn@gmail.com>, 2022
*/

using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#elif USE_NAUGHTY_ATTR
using NaughtyAttributes;
#endif

namespace JCMG.Slate
{
	/// <summary>
	/// A component that handles any UX reactions to a panel being shown or hidden.
	/// </summary>
	[RequireComponent(typeof(UIScreenBase))]
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

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.UI_REFS)]
		[Required]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(RuntimeConstants.UI_REFS)]
		[Required]
		#endif
		[SerializeField]
		private UIScreenBase _screen;

		#if ODIN_INSPECTOR
		[TitleGroup(SHOW_TG)]
		#elif USE_NAUGHTY_ATTR
		[Space(5)]
		[BoxGroup(SHOW_TG)]
		#endif
		[SerializeField]
		private TransitionSourceType _showTransitionSourceType;

		#if ODIN_INSPECTOR
		[TitleGroup(SHOW_TG)]
		[ShowIf("IsUsingShowScriptableTransition")]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(SHOW_TG)]
		[ShowIf("IsUsingShowScriptableTransition")]
		#endif
		[SerializeField]
		private ScreenTransitionBase showScreenTransition;

		#if ODIN_INSPECTOR
		[TitleGroup(SHOW_TG)]
		[HideIf("IsUsingShowScriptableTransition")]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(SHOW_TG)]
		[HideIf("IsUsingShowScriptableTransition")]
		#endif
		[SerializeField]
		private BehaviourScreenTransitionBase showBehaviourScreenTransition;

		#if ODIN_INSPECTOR
		[TitleGroup(HIDE_TG)]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(HIDE_TG)]
		#endif
		[SerializeField]
		private TransitionSourceType _hideTransitionSourceType;

		#if ODIN_INSPECTOR
		[TitleGroup(HIDE_TG)]
		[ShowIf("IsUsingHideScriptableTransition")]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(HIDE_TG)]
		[ShowIf("IsUsingHideScriptableTransition")]
		#endif
		[SerializeField]
		private ScreenTransitionBase hideScreenTransition;

		#if ODIN_INSPECTOR
		[TitleGroup(HIDE_TG)]
		[HideIf("IsUsingHideScriptableTransition")]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup("Hide Configuration")]
		[HideIf("IsUsingHideScriptableTransition")]
		#endif
		[SerializeField]
		private BehaviourScreenTransitionBase hideBehaviourScreenTransition;

		private const string SHOW_TG = "Show Configuration";
		private const string HIDE_TG = "Hide Configuration";

		/// <summary>
		/// Plays a transition to show the attached <see cref="UIScreenBase"/> instance; upon completion,
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
		/// Plays a transition to hide the attached <see cref="UIScreenBase"/> instance; upon completion,
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
		/// <see cref="UIScreenBase"/>.
		/// </summary>
		private IScreenTransition GetShowScreenTransition()
		{
			return _showTransitionSourceType == TransitionSourceType.ScriptableObject
				? showScreenTransition
				: showBehaviourScreenTransition;
		}

		/// <summary>
		/// Returns the <see cref="IScreenTransition"/> in-use for this instance for hiding the attached
		/// <see cref="UIScreenBase"/>.
		/// </summary>
		private IScreenTransition GetHideScreenTransition()
		{
			return _hideTransitionSourceType == TransitionSourceType.ScriptableObject
				? hideScreenTransition
				: hideBehaviourScreenTransition;
		}
	}
}
