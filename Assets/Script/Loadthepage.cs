using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
public class Loadthepage : MonoBehaviour {
	public AudioSource music;
	// Use this for initialization
	public void sss()
	{
		music.Play ();

		Application.LoadLevelAsync ("Load");

	}
	

}
