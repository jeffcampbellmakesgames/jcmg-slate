/*
Slate
Copyright (C) Jeff Campbell - All Rights Reserved
Unauthorized copying of this file, via any medium is strictly prohibited
Proprietary and confidential
Written by Jeff Campbell <mirraraenn@gmail.com>, 2022
*/

using System;
using System.Collections.Generic;

namespace JCMG.Slate
{
	/// <summary>
	/// The runtime UI manager containing all <see cref="UIScreen"/> instances.
	/// </summary>
	public static class UIScreenControl
	{
		/// <summary>
		/// The runtime collection of all registered <see cref="UIScreen"/> instances.
		/// </summary>
		private static readonly List<UIScreen> REGISTERED_UI_PANELS;

		private const string COULD_NOT_FIND_SCREEN_ERROR_FORMAT = "Could not find UIPanel of type '{0}' in UIManager...";

		static UIScreenControl()
		{
			REGISTERED_UI_PANELS = new List<UIScreen>();
		}

		/// <summary>
		/// Registers the <see cref="UIScreen"/> instance, if not already present.
		/// </summary>
		public static void RegisterUIScreen(UIScreen screen)
		{
			if (REGISTERED_UI_PANELS.Contains(screen))
			{
				return;
			}

			// Set the Sorting order of the UIPanel's Canvas based on its index in the UILayerOrder.
			screen.SetSortingOrder((int)screen.Layer);

			REGISTERED_UI_PANELS.Add(screen);
		}

		/// <summary>
		/// Registers the <see cref="UIScreen"/> instance if present.
		/// </summary>
		public static void UnregisterUIScreen(UIScreen screen)
		{
			if (!REGISTERED_UI_PANELS.Contains(screen))
			{
				return;
			}

			REGISTERED_UI_PANELS.Remove(screen);
		}

		/// <summary>
		/// Returns the registered <see cref="UIScreen"/> instance of type <typeparamref name="T"/> if present. If none
		/// is found, an exception is thrown.
		/// </summary>
		public static T GetPanel<T>() where T : UIScreen
		{
			for (var i = 0; i < REGISTERED_UI_PANELS.Count; i++)
			{
				if (REGISTERED_UI_PANELS[i].GetType() == typeof(T))
				{
					return (T)REGISTERED_UI_PANELS[i];
				}
			}

			throw new Exception(string.Format(COULD_NOT_FIND_SCREEN_ERROR_FORMAT, typeof(T)));
		}

		/// <summary>
		/// Returns the registered <see cref="UIScreen"/> instance of <see cref="Type"/> <paramref name="type"/> if
		/// present. If none is found, an exception is thrown.
		/// </summary>
		public static UIScreen GetPanel(Type type)
		{
			for (var i = 0; i < REGISTERED_UI_PANELS.Count; i++)
			{
				if (REGISTERED_UI_PANELS[i].GetType() == type)
				{
					return REGISTERED_UI_PANELS[i];
				}
			}

			throw new Exception(string.Format(COULD_NOT_FIND_SCREEN_ERROR_FORMAT, type));
		}

		/// <summary>
		/// Returns true if the registered <see cref="UIScreen"/> instance of type <typeparamref name="T"/> is present,
		/// otherwise returns false. If true, <paramref name="screen"/> will be initialized.
		/// </summary>
		public static bool TryGetPanel<T>(out T screen) where T : UIScreen
		{
			screen = null;
			for (var i = 0; i < REGISTERED_UI_PANELS.Count; i++)
			{
				if (REGISTERED_UI_PANELS[i].GetType() == typeof(T))
				{
					screen = (T)REGISTERED_UI_PANELS[i];
				}
			}

			return screen != null;
		}
	}
}
