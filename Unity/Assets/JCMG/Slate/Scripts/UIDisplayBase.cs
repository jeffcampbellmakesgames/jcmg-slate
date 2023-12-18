/*
Slate
Copyright (C) Jeff Campbell - All Rights Reserved
Unauthorized copying of this file, via any medium is strictly prohibited
Proprietary and confidential
Written by Jeff Campbell <mirraraenn@gmail.com>, 2022
*/

using UnityEngine;

namespace JCMG.Slate
{
	/// <summary>
	/// A display responsible for rendering a single <typeparamref name="T"/> instance in the UI.
	/// </summary>
	public abstract class UIDisplayBase<T> : MonoBehaviour
	{
		protected T _value;

		/// <summary>
		/// Sets a <typeparamref name="T"/> instance for this UI.
		/// </summary>
		public virtual void Set(T value)
		{
			_value = value;
		}

		/// <summary>
		/// Refreshes this UI display.
		/// </summary>
		public abstract void UpdateDisplay();

		/// <summary>
		/// Shows the UI display.
		/// </summary>
		public virtual void Show()
		{
			// No-op
		}

		/// <summary>
		/// Hides the UI display.
		/// </summary>
		public virtual void Hide()
		{
			// No-op
		}
	}
}
