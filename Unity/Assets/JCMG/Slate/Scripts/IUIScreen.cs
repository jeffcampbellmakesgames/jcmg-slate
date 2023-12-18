using System;

namespace JCMG.Slate
{
	/// <summary>
	/// Represents a discrete UI Screen that can be shown or hidden.
	/// </summary>
	public interface IUIScreen
	{
		/// <summary>
		/// Invoked when this instance is shown.
		/// </summary>
		event Action Shown;

		/// <summary>
		/// Invoked when this instance is hidden.
		/// </summary>
		event Action Hidden;

		/// <summary>
		/// Returns the <see cref="UILayer"/> this UI is on.
		/// </summary>
		UILayer Layer { get; }

		/// <summary>
		/// Returns true if the UI is visible, otherwise false.
		/// </summary>
		bool IsVisible { get; }

		/// <summary>
		/// Shows this UI panel instance and if applicable sets it to be interactable.
		/// </summary>
		void Show(bool immediate = false);

		/// <summary>
		/// Hides this UI panel instance from view and prevents it from being interactable.
		/// </summary>
		void Hide(bool immediate = false);

		/// <summary>
		/// Sets the state of this UI screen to be non-interactive.
		/// </summary>
		void DisableInteractivity();

		/// <summary>
		/// Sets the state of this UI screen to be interactive.
		/// </summary>
		void EnableInteractivity();

		/// <summary>
		/// Toggles the state of this UI instance to be interactive or not.
		/// </summary>
		void ToggleInteractivity(bool isInteractive);

		/// <summary>
		/// Sets the sorting order of this UI instance's <see cref="Canvas"/>.
		/// </summary>
		void SetSortingOrder(int sortingLayer);
	}
}
