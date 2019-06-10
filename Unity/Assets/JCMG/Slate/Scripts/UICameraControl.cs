/*
Slate
Copyright (C) Jeff Campbell - All Rights Reserved
Unauthorized copying of this file, via any medium is strictly prohibited
Proprietary and confidential
Written by Jeff Campbell <mirraraenn@gmail.com>, 2022
*/
using JCMG.Utility;
using NaughtyAttributes;
using UnityEngine;

namespace JCMG.Slate
{
	/// <summary>
	/// A global singleton to provide reference to the camera rendering the screen-space UI.
	/// </summary>
	[AddComponentMenu("JCMG/Slate/Singletons/UICameraControl")]
	public sealed class UICameraControl : Singleton<UICameraControl>
	{
		/// <summary>
		/// The camera rendering the UI.
		/// </summary>
		public Camera Camera => _camera;

		[SerializeField, Required]
		private Camera _camera;
	}
}
