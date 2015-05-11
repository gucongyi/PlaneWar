using UnityEngine;
using System.Collections;

public class EnemyBulletTranslate : MonoBehaviour
{

	float _enemyBulletSpeed = -50f;
	GameObject heroBloodBar;
	GameObject heroBloodBarImage;
	HeroBloodBar scriptHeroBloodBar;
	// Use this for initialization

	GameObject ScoreUI;
	void Awake() {
		ScoreUI=GameObject.Find("ScoreUI");
		DontDestroyOnLoad(ScoreUI);
	}

	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (new Vector3 (0, 0, Time.deltaTime * _enemyBulletSpeed));
		RaycastHit hit;
		Physics.Raycast (transform.position, Vector3.back, out hit, 2f);
		Debug.DrawRay (transform.position, Vector3.back * 2, Color.green);

		if (hit.transform != null) {
			if (hit.transform.gameObject.name.Contains ("omega_fighter")) {
				heroBloodBar = GameObject.FindWithTag ("heroBloodBar");
				if (heroBloodBar != null) {
					heroBloodBarImage = heroBloodBar.transform.Find ("Image").gameObject;
					scriptHeroBloodBar = heroBloodBarImage.GetComponent<HeroBloodBar> () as HeroBloodBar;
					scriptHeroBloodBar.isDesOne = true;
				
					if (scriptHeroBloodBar.fina < 5f) {
						Destroy (heroBloodBar);
						Application.LoadLevel("GameOverScene");
					}
				}
			}
		}
		if (gameObject != null) {
			Destroy (gameObject, 3f);
		}

	}
}
