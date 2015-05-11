using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadRescourse : MonoBehaviour
{
	public Texture2D picturebg;
	public Texture2D blood_red;
	public Texture2D blood_black;
	float percentageLoaded;
	public float HP = 0f;

	void Update ()
	{

		if (Application.GetStreamProgressForLevel (2) == 1) {
			if (HP == 285) {
				Application.LoadLevelAsync ("PlaneWar");
			}
		} else {
			percentageLoaded = Application.GetStreamProgressForLevel (2) * 100;
//			HP=percentageLoaded;
		}
		HP++;
	}
	//这里主要是进度条显示
	void OnGUI ()
	{
		GUI.DrawTexture (new Rect (Screen.width / 2 - 180, Screen.height / 2 + 165, HP, 21), blood_red);
	}
}

