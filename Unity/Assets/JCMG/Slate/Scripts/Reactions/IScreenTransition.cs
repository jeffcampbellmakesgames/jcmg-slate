using UnityEngine;

namespace JCMG.Slate
{
	/// <summary>
	/// Represents a callback related to a <see cref="IScreenTransition"/>.
	/// </summary>
	public delegate void ScreenTransitionCallback();

	/// <summary>
	/// Represents a transition for a <see cref="UIScreen"/>.
	/// </summary>
	public interface IScreenTransition
	{
		/// <summary>
		/// Plays a screen transition; upon completion, <paramref name="onComplete"/> will be invoked.
		/// </summary>
		void PlayTransition(ScreenTransitionCallback onComplete);
	}
}
