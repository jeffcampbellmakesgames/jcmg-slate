/*
Slate
Copyright (C) Jeff Campbell - All Rights Reserved
Unauthorized copying of this file, via any medium is strictly prohibited
Proprietary and confidential
Written by Jeff Campbell <mirraraenn@gmail.com>, 2020
*/

using UnityEngine;

namespace JCMG.Slate
{
	/// <summary>
	/// Represents a distinct layers the UI might exist on.
	/// </summary>
	[CreateAssetMenu(
		menuName = "JCMG/Slate/UI Layer",
		fileName = "NewUILayer")]
	public class UILayer : ScriptableObject
	{
		/// <summary>
		/// The sorting layer the UI should be on.
		/// </summary>
		public int SortingLayer => _sortingLayer;

		[SerializeField]
		private int _sortingLayer;
	}
}
