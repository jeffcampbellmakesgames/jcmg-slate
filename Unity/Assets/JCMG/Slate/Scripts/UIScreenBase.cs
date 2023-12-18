/*
Slate
Copyright (C) Jeff Campbell - All Rights Reserved
Unauthorized copying of this file, via any medium is strictly prohibited
Proprietary and confidential
Written by Jeff Campbell <mirraraenn@gmail.com>, 2022
*/

using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#elif USE_NAUGHTY_ATTR
using NaughtyAttributes;
#endif

namespace JCMG.Slate
{
	/// <summary>
	/// An abstract base class used to represent a single UI screen instance.
	/// </summary>
	[RequireComponent(typeof(Canvas))]
	public abstract class UIScreenBase : MonoBehaviour, IUIScreen
	{
		/// <inheritdoc />
		public event Action Shown;

		/// <inheritdoc />
		public event Action Hidden;

		/// <inheritdoc />
		public UILayer Layer => _layer;

		/// <inheritdoc />
		public bool IsVisible => _isVisible;

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.UI_REFS)]
		[Required]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(RuntimeConstants.UI_REFS)]
		[Required]
		#endif
		[SerializeField]
		protected Canvas _canvas;

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.UI_REFS)]
		[Required]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(RuntimeConstants.UI_REFS)]
		[Required]
		#endif
		[SerializeField]
		protected CanvasGroup _canvasGroup;

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.UI_REFS)]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(RuntimeConstants.UI_REFS)]
		#endif
		[SerializeField]
		protected UIScreenTransitionController _uiTransitionController;

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.SETTINGS)]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(RuntimeConstants.SETTINGS)]
		#endif
		[SerializeField]
		protected bool _isInteractable;

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.SETTINGS)]
		[Required]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(RuntimeConstants.SETTINGS)]
		[Required]
		#endif
		[SerializeField]
		protected UILayer _layer;

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.SETTINGS)]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(RuntimeConstants.SETTINGS)]
		#endif
		[SerializeField]
		protected StartupMode _startupMode;

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.DEBUG)]
		[ReadOnly, ShowInInspector]
		#elif USE_NAUGHTY_ATTR
		[Space]
		[BoxGroup(RuntimeConstants.DEBUG)]
		[ReadOnly, ShowNonSerializedField]
		#endif
		protected bool _isVisible;

		/// <summary>
		/// Returns true if this instance is currently in the process of showing, otherwise false.
		/// </summary>
		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.DEBUG)]
		[ReadOnly, ShowInInspector]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(RuntimeConstants.DEBUG)]
		[ReadOnly, ShowNonSerializedField]
		#endif
		protected bool _isShowing;

		/// <summary>
		/// Returns true if this instance is currently in the process of hiding, otherwise false.
		/// </summary>
		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.DEBUG)]
		[ReadOnly, ShowInInspector]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(RuntimeConstants.DEBUG)]
		[ReadOnly, ShowNonSerializedField]
		#endif
		protected bool _isHiding;

		protected virtual void Awake()
		{
			// Set default state
			_isVisible = true;

			SetSortingOrder(_layer.SortingLayer);
			ToggleInteractivity(_isInteractable);

			UIScreenControl.RegisterUIScreen(this);

			if (_startupMode == StartupMode.HideOnAwake)
			{
				Hide(immediate:true);
			}
			else if (_startupMode == StartupMode.ShowOnAwake)
			{
				Hide(immediate:true);
				Show(immediate:true);
			}
		}

		protected virtual void Start()
		{
			if (_canvas.renderMode == RenderMode.ScreenSpaceCamera && _canvas.worldCamera == null)
			{
				_canvas.worldCamera = UICameraControl.Instance.Camera;
			}

			if (_startupMode == StartupMode.HideOnStart)
			{
				Hide(immediate: true);
			}
		}

		protected virtual void OnDestroy()
		{
			UIScreenControl.UnregisterUIScreen(this);
		}

		/// <inheritdoc />
		public void SetSortingOrder(int sortingLayer)
		{
			_canvas.sortingOrder = sortingLayer;
		}

		/// <inheritdoc />
		public virtual void Show(bool immediate = false)
		{
			// If already visible or animating to being shown, return
			if (IsVisible || _isShowing)
			{
				return;
			}

			_isShowing = true;
			_canvas.enabled = true;

			if (immediate || _uiTransitionController == null)
			{
				FinalizeShow();
			}
			else
			{
				_uiTransitionController.AnimateShowReaction(FinalizeShow);
			}
		}

		/// <summary>
		/// Finalizes the logic for <see cref="Show"/> by setting up interactivity appropriately and invoking any events.
		/// </summary>
		private void FinalizeShow()
		{
			ToggleInteractivity(_isInteractable);

			_isShowing = false;

			Shown?.Invoke();
		}

		/// <inheritdoc />
		public virtual void Hide(bool immediate = false)
		{
			// If not visible or animating to being hidden, return
			if (!IsVisible || _isHiding)
			{
				return;
			}

			_isHiding = true;

			if (immediate || _uiTransitionController == null)
			{
				FinalizeHide();
			}
			else
			{
				_uiTransitionController.AnimateHideReaction(FinalizeHide);
			}
		}

		/// <summary>
		/// Finalizes the logic for <see cref="Hide"/> by setting up interactivity appropriately and invoking any events.
		/// </summary>
		private void FinalizeHide()
		{
			_canvas.enabled = false;
			_isHiding = false;

			ToggleInteractivity(false);

			Hidden?.Invoke();
		}

		/// <inheritdoc />
		public void ToggleInteractivity(bool isInteractive)
		{
			if (!isInteractive)
			{
				_canvasGroup.interactable = false;
				_canvasGroup.blocksRaycasts = false;
			}
			else if (IsVisible && _isInteractable)
			{
				_canvasGroup.interactable = true;
				_canvasGroup.blocksRaycasts = true;
			}
		}

		/// <inheritdoc />
		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.DEBUG)]
		[Button]
		[DisableInEditorMode]
		#elif USE_NAUGHTY_ATTR
		[Button(enabledMode:EButtonEnableMode.Playmode)]
		#endif
		public void DisableInteractivity()
		{
			ToggleInteractivity(false);
		}

		/// <inheritdoc />
		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.DEBUG)]
		[Button]
		[DisableInEditorMode]
		#elif USE_NAUGHTY_ATTR
		[Button(enabledMode:EButtonEnableMode.Playmode)]
		#endif
		public void EnableInteractivity()
		{
			ToggleInteractivity(true);
		}

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.DEBUG)]
		[Button]
		[DisableInEditorMode]
		#elif USE_NAUGHTY_ATTR
		[Button(enabledMode:EButtonEnableMode.Playmode)]
		#endif
		private void ShowImmediately()
		{
			Show(immediate:true);
		}

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.DEBUG)]
		[Button]
		[DisableInEditorMode]
		#elif USE_NAUGHTY_ATTR
		[Button(enabledMode:EButtonEnableMode.Playmode)]
		#endif
		private void ShowOverTime()
		{
			Show();
		}

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.DEBUG)]
		[Button]
		[DisableInEditorMode]
		#elif USE_NAUGHTY_ATTR
		[Button(enabledMode:EButtonEnableMode.Playmode)]
		#endif
		private void HideImmediately()
		{
			Hide(immediate:true);
		}

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.DEBUG)]
		[Button]
		[DisableInEditorMode]
		#elif USE_NAUGHTY_ATTR
		[Button(enabledMode:EButtonEnableMode.Playmode)]
		#endif
		private void HideOverTime()
		{
			Hide();
		}
	}
}
