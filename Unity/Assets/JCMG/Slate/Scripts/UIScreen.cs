/*
Slate
Copyright (C) Jeff Campbell - All Rights Reserved
Unauthorized copying of this file, via any medium is strictly prohibited
Proprietary and confidential
Written by Jeff Campbell <mirraraenn@gmail.com>, 2022
*/

using System;
using NaughtyAttributes;
using UnityEngine;

namespace JCMG.Slate
{
	/// <summary>
	/// An abstract base class used to represent a single UI screen instance.
	/// </summary>
	[RequireComponent(typeof(Canvas))]
	[AddComponentMenu("JCMG/Slate/UIScreen")]
	public abstract class UIScreen : MonoBehaviour
	{
		/// <summary>
		/// Invoked when this instance is shown.
		/// </summary>
		public event Action Shown;

		/// <summary>
		/// Invoked when this instance is hidden.
		/// </summary>
		public event Action Hidden;

		/// <summary>
		/// Returns the <see cref="UILayer"/> this UI is on.
		/// </summary>
		public UILayer Layer => _layer;

		/// <summary>
		/// Returns true if the UI is visible, otherwise false.
		/// </summary>
		public bool IsVisible => _canvas != null && _canvas.enabled;

		[BoxGroup(RuntimeConstants.UI_REFS)]
		[SerializeField, Required]
		protected Canvas _canvas;

		[BoxGroup(RuntimeConstants.UI_REFS)]
		[SerializeField, Required]
		protected CanvasGroup _canvasGroup;

		[BoxGroup(RuntimeConstants.UI_REFS)]
		[SerializeField]
		protected UIScreenTransitionController _uiTransitionController;

		[BoxGroup(RuntimeConstants.SETTINGS)]
		[SerializeField]
		protected bool _isInteractable;

		[BoxGroup(RuntimeConstants.SETTINGS)]
		[SerializeField]
		protected UILayer _layer;

		[BoxGroup(RuntimeConstants.SETTINGS)]
		[SerializeField]
		protected bool _showOnStart;

		/// <summary>
		/// Returns true if this instance is currently in the process of showing, otherwise false.
		/// </summary>
		[BoxGroup(RuntimeConstants.DEBUG)]
		[SerializeField, ReadOnly]
		protected bool _isShowing;

		/// <summary>
		/// Returns true if this instance is currently in the process of hiding, otherwise false.
		/// </summary>
		[BoxGroup(RuntimeConstants.DEBUG)]
		[SerializeField, ReadOnly]
		protected bool _isHiding;

		protected virtual void Awake()
		{
			UIScreenControl.RegisterUIScreen(this);
		}

		protected virtual void Start()
		{
			if (_canvas.renderMode == RenderMode.ScreenSpaceCamera && _canvas.worldCamera == null)
			{
				_canvas.worldCamera = UICameraControl.Instance.Camera;
			}

			if (_showOnStart)
			{
				Show(immediate:true);
			}
			else
			{
				Hide(immediate:true);
			}
		}

		protected virtual void OnDestroy()
		{
			UIScreenControl.UnregisterUIScreen(this);
		}

		/// <summary>
		/// Sets the sorting order of this UI instance's <see cref="Canvas"/>.
		/// </summary>
		internal void SetSortingOrder(int sortingLayer)
		{
			_canvas.sortingOrder = sortingLayer;
		}

		/// <summary>
		/// Shows this UI panel instance and if applicable sets it to be interactable.
		/// </summary>
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

		/// <summary>
		/// Hides this UI panel instance from view and prevents it from being interactable.
		/// </summary>
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

		/// <summary>
		/// Toggles the state of this UI instance to be interactive or not.
		/// </summary>
		protected void ToggleInteractivity(bool isInteractive)
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

		#if UNITY_EDITOR

		/// <summary>
		/// Wrapper bool for <see cref="Application.isPlaying"/> for inspector UI.
		/// </summary>
		private bool IsApplicationPlaying => Application.isPlaying;

		/// <summary>
		/// Wrapper bool for <see cref="Application.isPlaying"/> for inspector UI.
		/// </summary>
		private bool IsEditorMode => !Application.isPlaying;

		/// <summary>
		/// Should only be used by the inspector to trigger a panel to show immediately for testing animations
		/// </summary>
		[HideIf("IsEditorMode")]
		[DisableIf("IsVisible")]
		[Button("Show Panel")]
		private void ShowPanelFromInspector()
		{
			if (!Application.isPlaying)
			{
				return;
			}

			Show();
		}

		/// <summary>
		/// Should only be used by the inspector to trigger a panel to hide immediately for testing animations
		/// </summary>
		[HideIf("IsEditorMode")]
		[EnableIf("IsVisible")]
		[Button("Hide Panel")]
		private void HidePanelFromInspector()
		{
			if (!Application.isPlaying)
			{
				return;
			}

			Hide();
		}

		#endif
	}
}
