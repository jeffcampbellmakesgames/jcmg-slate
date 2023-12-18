using System;
using UnityEngine;
using UnityEngine.UIElements;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#elif USE_NAUGHTY_ATTR
using NaughtyAttributes;
#endif

namespace JCMG.Slate
{
	/// <summary>
	/// A variant of a UIScreen using the Unity UI Toolkit.
	/// </summary>
	[RequireComponent(typeof(UIDocument))]
	public abstract class UIScreenDocumentBase : MonoBehaviour, IUIScreen
	{
		/// <inheritdoc />
		public event Action Shown;

		/// <inheritdoc />
		public event Action Hidden;

		/// <inheritdoc />
		public UILayer Layer => _layer;

		/// <inheritdoc />
		public bool IsVisible => _isVisible;

		/// <summary>
		/// The selector class to use when showing this UI screen.
		/// </summary>
		protected string VisibleClass => _visibleClass;

		/// <summary>
		/// The selector class to use when hiding this UI screen.
		/// </summary>
		protected string HiddenClass => _hiddenClass;

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.UI_REFS)]
		[Required]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(RuntimeConstants.UI_REFS)]
		[Required]
		#endif
		[SerializeField]
		protected UIDocument _uiDocument;

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.SETTINGS)]
		[Required]
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
		[TitleGroup(RuntimeConstants.SETTINGS)]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(RuntimeConstants.SETTINGS)]
		#endif
		[SerializeField]
		private string _visibleClassOverride;

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.SETTINGS)]
		#elif USE_NAUGHTY_ATTR
		[BoxGroup(RuntimeConstants.SETTINGS)]
		#endif
		[SerializeField]
		private string _hiddenClassOverride;

		#if ODIN_INSPECTOR
		[TitleGroup(RuntimeConstants.DEBUG)]
		[ReadOnly]
		#elif USE_NAUGHTY_ATTR
		[Space]
		[BoxGroup(RuntimeConstants.DEBUG)]
		[ReadOnly]
		#endif
		[SerializeField]
		protected bool _isVisible;

		private string _visibleClass;
		private string _hiddenClass;
		protected VisualElement _rootElement;
		protected StyleEnum<DisplayStyle> _originalDisplayStyle;

		// The default selector classes used if overrides are not provided.
		private const string VISIBLE_CLASS = "jcmg-slate-screen-visible";
		private const string HIDDEN_CLASS = "jcmg-slate-screen-hidden";

		protected virtual void Awake()
		{
			// Set default state
			_isVisible = true;
			_visibleClass =!string.IsNullOrEmpty(_visibleClassOverride)
				? _visibleClassOverride
				: VISIBLE_CLASS;
			_hiddenClass = !string.IsNullOrEmpty(_hiddenClassOverride)
				? _hiddenClassOverride
				: HIDDEN_CLASS;

			_rootElement = _uiDocument.rootVisualElement;
			_originalDisplayStyle = _rootElement.style.display;

			SetSortingOrder(_layer.SortingLayer);
			ToggleInteractivity(_isInteractable);

			if (_startupMode == StartupMode.HideOnAwake)
			{
				Hide(immediate:true);
			}
			else if (_startupMode == StartupMode.ShowOnAwake)
			{
				Hide(immediate:true);
				Show(immediate:true);
			}

			UIScreenControl.RegisterUIScreen(this);
		}

		protected virtual void Start()
		{
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
		public void Show(bool immediate = false)
		{
			// If already visible, do nothing.
			if (IsVisible)
			{
				return;
			}

			_rootElement.style.display = _originalDisplayStyle;

			if (immediate)
			{
				_rootElement.BringToFront();
				_rootElement.RemoveFromClassList(HiddenClass);
			}
			else
			{
				_rootElement.AddToClassList(VisibleClass);
				_rootElement.BringToFront();
				_rootElement.RemoveFromClassList(HiddenClass);
			}

			_isVisible = true;

			Shown?.Invoke();
		}

		/// <inheritdoc />
		public void Hide(bool immediate = false)
		{
			// If already hidden, do nothing.
			if (!IsVisible)
			{
				return;
			}

			if (immediate)
			{
				_rootElement.AddToClassList(HiddenClass);
				_rootElement.RemoveFromClassList(VisibleClass);
				_rootElement.style.display = DisplayStyle.None;
			}
			else
			{
				_rootElement.AddToClassList(HiddenClass);
				_rootElement.RemoveFromClassList(VisibleClass);
			}

			_isVisible = false;

			Hidden?.Invoke();
		}

		/// <inheritdoc />
		public void ToggleInteractivity(bool isInteractive)
		{
			// Enable interactivity by setting pickingMode to Position or to Ignore to disable it.
			_rootElement.pickingMode = isInteractive
				? PickingMode.Position
				: PickingMode.Ignore;
		}

		/// <inheritdoc />
		public void SetSortingOrder(int sortingLayer)
		{
			_uiDocument.sortingOrder = sortingLayer;
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
