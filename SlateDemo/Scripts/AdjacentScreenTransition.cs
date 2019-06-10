using System;
using DG.Tweening;
using JCMG.Slate;
using NaughtyAttributes;
using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Assertions;

namespace Demo
{
	/// <summary>
	/// A scriptable screen transition that animates a <see cref="UIScreen"/> on or offscreen.
	/// </summary>
	[CreateAssetMenu(
		fileName = "NewAdjacentScreenTransition",
		menuName = "JCMG/Slate/AdjacentScreenTransition")]
	public sealed class AdjacentScreenTransition : ScreenTransitionBase
	{
		/// <summary>
		/// Returns true if the screen reaction is triggering from a <see cref="UIScreen"/> being shown,
		/// otherwise false.
		/// </summary>
		private bool IsEntrance => _transitionType == TransitionType.Entrance;

		/// <summary>
		/// Returns true if the screen reaction is triggering from a <see cref="UIScreen"/> being hidden,
		/// otherwise false.
		/// </summary>
		private bool IsExit => _transitionType == TransitionType.Exit;

		/// <summary>
		/// The side at which a reaction should occur.
		/// </summary>
		private enum SideType
		{
			Left,
			Right,
			Top,
			Bottom,
			Back
		}

		/// <summary>
		/// The type of transition that this reaction will be triggered from.
		/// </summary>
		private enum TransitionType
		{
			Exit,
			Entrance
		}

		[Tooltip("The side at which the transition of the UIScreen will occur from or to. If entering, the UIScreen " +
		         "will animate from that side, otherwise if it is an exit it will animate to that side.")]
		[SerializeField]
		private SideType _sideType;

		[Tooltip("Whether this reaction is occuring based on a UIScreen being shown (entering) or being hidden " +
		         "(exiting).")]
		[SerializeField]
		private TransitionType _transitionType;

		[Min(0f)]
		[SerializeField]
		private float _duration;

		[Min(0f)]
		[DisableIf("IsAnimationCurveDefined")]
		[SerializeField]
		private Ease _easeType;

		[SerializeField]
		private AnimationCurveReference _animationCurveReference;

		private RectTransform _targetRectTransform;

		/// <summary>
		/// Sets the target <see cref="RectTransform"/> for this reaction.
		/// </summary>
		public void SetTargetRectTransform(RectTransform targetRectTransform)
		{
			_targetRectTransform = targetRectTransform;
		}

		/// <inheritdoc />
		public override void PlayTransition(ScreenTransitionCallback onComplete)
		{
			Assert.IsNotNull(_targetRectTransform);

			Vector3 start;
			Vector3 end;
			Tween tween;

			switch (_sideType)
			{
				case SideType.Left:
					start = IsEntrance ? new Vector3(-_targetRectTransform.rect.width, 0) : Vector3.zero;
					end = IsEntrance ? Vector3.zero : new Vector3(-_targetRectTransform.rect.width, 0);
					tween = ConfigureMoveTween(start, end);
					break;
				case SideType.Right:
					start = IsEntrance ? new Vector3(_targetRectTransform.rect.width, 0) : Vector3.zero;
					end = IsEntrance ? Vector3.zero : new Vector3(_targetRectTransform.rect.width, 0);
					tween = ConfigureMoveTween(start, end);
					break;
				case SideType.Top:
					start = IsEntrance ? new Vector3(0, _targetRectTransform.rect.height, 0) : Vector3.zero;
					end = IsEntrance ? Vector3.zero : new Vector3(0, _targetRectTransform.rect.height, 0);
					tween = ConfigureMoveTween(start, end);
					break;
				case SideType.Bottom:
					start = IsEntrance ? new Vector3(0, -_targetRectTransform.rect.height, 0) : Vector3.zero;
					end = IsEntrance ? Vector3.zero : new Vector3(0, -_targetRectTransform.rect.height, 0);
					tween = ConfigureMoveTween(start, end);
					break;
				case SideType.Back:
					start = IsEntrance ? Vector3.zero : Vector3.one;
					end = IsEntrance ? Vector3.one : Vector3.zero;
					tween = ConfigureScaleTween(_targetRectTransform, start, end);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			// Set final tween params
			tween.OnComplete(()=> onComplete?.Invoke());
			tween.SetRecyclable();
			tween.Play();
		}

		/// <summary>
		/// Configures a scale tween for <see name="_targetRectTransform"/> from <paramref name="startPosition"/> to
		/// <paramref name="targetPosition"/>.
		/// </summary>
		private Tween ConfigureMoveTween(
			Vector3 startPosition,
			Vector3 targetPosition)
		{
			// Immediately set starting position
			_targetRectTransform.localScale = Vector3.one;
			_targetRectTransform.anchoredPosition3D = startPosition;

			var tween = _targetRectTransform.DOAnchorPos(targetPosition, _duration);
			if (_animationCurveReference.IsValueDefined)
			{
				tween.SetEase(_animationCurveReference.Value);
			}
			else
			{
				tween.SetEase(_easeType);
			}


			return tween;
		}

		/// <summary>
		/// Configures a scale tween for <paramref name="rectTransform"/> from <paramref name="startScale"/> to
		/// <paramref name="endScale"/>.
		/// </summary>
		private Tween ConfigureScaleTween(RectTransform rectTransform, Vector3 startScale, Vector3 endScale)
		{
			// Immediately set starting position
			rectTransform.localScale = startScale;
			rectTransform.anchoredPosition3D = Vector3.zero;

			var tween = rectTransform.DOScale(endScale, _duration);
			if (_animationCurveReference.IsValueDefined)
			{
				tween.SetEase(_animationCurveReference.Value);
			}
			else
			{
				tween.SetEase(_easeType);
			}

			return tween;
		}

		#if UNITY_EDITOR

		/// <summary>
		/// Returns true if an animation curve is defined locally or assigned from a <see cref="AnimationCurveVariable"/>.
		/// </summary>
		private bool IsAnimationCurveDefined()
		{
			return _animationCurveReference.IsValueDefined;
		}

		#endif
	}
}
