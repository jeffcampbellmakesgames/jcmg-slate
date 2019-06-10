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
	/// An abstract base scriptable class representing a screen reaction.
	/// </summary>
	public abstract class ScreenTransitionBase : ScriptableObject, IScreenTransition
	{
		/// <inheritdoc />
		public abstract void PlayTransition(ScreenTransitionCallback onComplete);
	}
}
