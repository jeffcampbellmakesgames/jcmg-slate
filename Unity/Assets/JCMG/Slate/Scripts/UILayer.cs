/*
Slate
Copyright (C) Jeff Campbell - All Rights Reserved
Unauthorized copying of this file, via any medium is strictly prohibited
Proprietary and confidential
Written by Jeff Campbell <mirraraenn@gmail.com>, 2020
*/

namespace JCMG.Slate
{
	/// <summary>
	/// Represents a set of distinct layers the UI might exist on.
	/// </summary>
	public enum UILayer
	{
		Main = 0,
		Hud = 10,
		Popup = 20,
		Loading = 100,
		Error = 200
	}
}
