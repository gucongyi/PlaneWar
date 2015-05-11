using UnityEngine;
using System.Collections;

public class BulletTranslate : MonoBehaviour
{

	float BulletSpeed = 150;
	float localX;
	float localZ;
	GameObject _smokeParticleRes;
	GameObject _exploreParticleRes;
	bool isSmoke = true;
	GameObject smoke;
	GameObject explore;
	float timeCount = 0.0f;
	GameObject ScoreUI;
	AudioSource ExplodeAudioEffect;
	void Awake ()
	{
		ScoreUI = GameObject.Find ("ScoreUI");
		DontDestroyOnLoad (ScoreUI);
	}

	void LoadExpodeAndSmokeRes ()
	{
		_smokeParticleRes = Resources.Load ("SmokeParticle") as GameObject;
		_exploreParticleRes = Resources.Load ("Explosion") as GameObject;
	}

	void Start ()
	{
		LoadExpodeAndSmokeRes ();
		ExplodeAudioEffect=GameObject.Find("omega_fighter").GetComponent<PlaneControll>().ExplodeAudioEffect;
	}

	void HeroBulletTranslate ()
	{
		transform.Translate (new Vector3 (0, 0, Time.deltaTime * BulletSpeed));
	}

	void DestroyHeroBulletInSomeTime ()
	{
		if (gameObject != null) {
			Destroy (gameObject, 0.4f);
			//			Destroy (gameObject, 3f);
		}
	}

	void HeroBulletEventHandle ()
	{
		RaycastHit hit;
		Physics.Raycast (transform.position, Vector3.forward, out hit, 5f);
		//		print (hit.transform);
		if (hit.transform != null) {
			localX = hit.transform.localRotation.x;
			localZ = hit.transform.localRotation.z;
			if (hit.transform.gameObject.name.Contains ("omega_fighter")) {
				//				do nothing
			} else {
				if (isSmoke) {
					if (hit.transform.gameObject.name.Contains ("DroidFighter")) {
						ExplodeAudioEffect.Play();
						hit.transform.localEulerAngles = new Vector3 (90, 180, localZ);
						Destroy (hit.transform.gameObject, 1.5f);
						hit.transform.gameObject.GetComponent<Rigidbody> ().useGravity = true;
						Score.crashScore += 200;
					} else
						if (hit.transform.gameObject.name.Contains ("Boss")) {
						ExplodeAudioEffect.Play();
						Score.crashScore += 500;
						if (EnemyAction.bloodBar != null) {
							Transform imageTrans = EnemyAction.bloodBar.transform.Find ("Image");
							BgPosition BloodBar = imageTrans.GetComponent<BgPosition> () as BgPosition;
							print (BloodBar);
							BloodBar.isDesOne = true;
							print ("BloodBar.isDesOne:" + BloodBar.isDesOne);
							if (BloodBar.fina < 10f) {
								Destroy (EnemyAction.bloodBar);
								hit.transform.localEulerAngles = new Vector3 (90, 180, localZ);
								hit.transform.gameObject.GetComponent<Rigidbody> ().useGravity = true;
								smoke = GameObject.Instantiate (_smokeParticleRes);
								smoke.transform.SetParent (hit.transform);
								smoke.transform.localPosition = Vector3.zero;
								smoke.transform.localEulerAngles = new Vector3 (270, 0, 0);
								//isSmoke = false;
								Destroy (smoke, 5f);
								Exploresion (hit);
								Destroy (hit.transform.gameObject, 5f);
//								LoadSuccessSceneInFive ();
								GameObject.Find("omega_fighter").AddComponent<DestroyInFiveSeconds>();
//								Invoke("LoadSuccessSceneInFive",0f);
							}
						}
						//						
					} else {
						ExplodeAudioEffect.Play();
						Score.crashScore += 100;
						hit.transform.localEulerAngles = new Vector3 (localX, 90, 270);
						Destroy (hit.transform.gameObject, 1.5f);
						hit.transform.gameObject.GetComponent<Rigidbody> ().useGravity = true;
					}
					smoke = GameObject.Instantiate (_smokeParticleRes);
					smoke.transform.SetParent (hit.transform);
					smoke.transform.localPosition = Vector3.zero;
					smoke.transform.localEulerAngles = new Vector3 (270, 0, 0);
					isSmoke = false;
					Destroy (smoke, 3f);
					Exploresion (hit);
				}
			}
			Destroy (gameObject);
		}
	}

	void TimeCount ()
	{
		timeCount += Time.deltaTime;
	}

	void Update ()
	{
		HeroBulletTranslate ();
		DestroyHeroBulletInSomeTime ();
		HeroBulletEventHandle ();
		TimeCount ();

	}

	void LoadSuccessSceneInFive ()
	{
		print ("success");
		Application.LoadLevel ("GameOverScene");
	}

	void Exploresion (RaycastHit hit)
	{
		explore = GameObject.Instantiate (_exploreParticleRes);
//		Debug.Log (explore);
		explore.transform.SetParent (hit.transform);
		explore.transform.localPosition = Vector3.zero;
		explore.transform.localEulerAngles = Vector3.zero;
		
	}


}
