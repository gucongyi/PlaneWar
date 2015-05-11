using UnityEngine;
using System.Collections;

public class InitHeroBloodBar : MonoBehaviour {

	// Use this for initialization
	void Start () {

		GameObject.Instantiate(Resources.Load("HeroBloodBar") as GameObject);

	}

}
