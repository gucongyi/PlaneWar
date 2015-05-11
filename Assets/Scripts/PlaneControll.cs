using UnityEngine;
using System.Collections;

public class PlaneControll : MonoBehaviour
{
	public float speed = 20;
	private float initSpeed = 0.2f;
	float initHSpeed = 0.0f;
	float localX;
	float localY;
	float localZ;
	float plocalX1;
	float plocalY1;
	float plocalZ1;
	float plocalX2;
	float plocalY2;
	float plocalZ2;
	float rotateY;
	Vector3 _point1;
	Vector3 _point2;
	Vector3 _point3;
	Vector3 _point4;
	GameObject _bulletRes;
	GameObject _bullet1;
	GameObject _bullet2;
	GameObject _bullet3;
	GameObject _bullet4;
	GameObject _particle1;
	GameObject _particle2;
	bool isBulletOriginPosition = true;
	float timeCount = 0.0f;
	GameObject[] _allEnemy;
	bool isSmoke = true;
	GameObject smoke;
	GameObject explore;
	GameObject _smokeParticleRes;
	GameObject _exploreParticleRes;
	GameObject heroBloodBar;
	GameObject heroBloodBarImage;
	HeroBloodBar scriptHeroBloodBar;
	AudioSource audioSource;
	GameObject ScoreUI;
	public AudioSource HeroCastBulletMusic;
	public AudioSource ExplodeAudioEffect;
	void Awake() {
		ScoreUI=GameObject.Find("ScoreUI");
		DontDestroyOnLoad(ScoreUI);
		audioSource=gameObject.GetComponent<AudioSource>();

	}

	void Start ()
	{
		InitAndGetMessage ();
	}

	void InitAndGetMessage ()
	{
		BackGroundMusicPaly ();
		GetTransform ();
		LoadBulletRes ();
		AllEnemyPlaneAddActionScript ();
		JetTwoParticleFindAndSetScale ();
		FindExplodeAndSmokeParticle ();
	}

	void AllEnemyPlaneAddActionScript ()
	{
		_allEnemy = GameObject.FindGameObjectsWithTag ("Enemy");
		Debug.Log (_allEnemy.Length);
		for (int i = 0; i < _allEnemy.Length; i++) {
			_allEnemy [i].AddComponent<EnemyAction> ();
		}
	}

	void LoadBulletRes ()
	{
		_bulletRes = Resources.Load ("bullet") as GameObject;
	}

	void GetTransform ()
	{
		localX = transform.localScale.x;
		localY = transform.localScale.y;
		localZ = transform.localScale.z;
		rotateY = transform.localRotation.y;
	}

	void BackGroundMusicPaly ()
	{
		audioSource.Play ();
	}

	void FindExplodeAndSmokeParticle ()
	{
		_smokeParticleRes = Resources.Load ("SmokeParticle") as GameObject;
		_exploreParticleRes = Resources.Load ("Explosion") as GameObject;
	}

	void JetTwoParticleFindAndSetScale ()
	{
		_particle1 = GameObject.Find ("Particle") as GameObject;
		_particle2 = GameObject.Find ("Particle 1") as GameObject;
		plocalX1 = _particle1.transform.localScale.x;
		plocalY1 = _particle1.transform.localScale.y;
		plocalZ1 = _particle1.transform.localScale.z;
		plocalX2 = _particle2.transform.localScale.x;
		plocalY2 = _particle2.transform.localScale.y;
		plocalZ2 = _particle2.transform.localScale.z;
	}

	void FindHeroBloodBar ()
	{
		heroBloodBar = GameObject.FindWithTag ("heroBloodBar");
	}

	void GameOverCondition ()
	{
		if (heroBloodBar != null) {
			heroBloodBarImage = heroBloodBar.transform.Find ("Image").gameObject;
			scriptHeroBloodBar = heroBloodBarImage.GetComponent<HeroBloodBar> () as HeroBloodBar;
			if (scriptHeroBloodBar.fina < 5f) {
				Destroy (heroBloodBar);
				Application.LoadLevel ("GameOverScene");
			}
		}
	}

	void CollResHandle (Collider coll)
	{
		if (coll.gameObject.name.Contains ("Boss")) {
			scriptHeroBloodBar.isDesTwenty = true;
			Score.crashScore += 800;
		}
		else {
			coll.gameObject.GetComponent<Rigidbody> ().useGravity = true;
			scriptHeroBloodBar.isDesFive = true;
			Destroy (coll.gameObject, 1.5f);
			Score.crashScore += 500;
		}
		if (isSmoke) {
			if (coll.gameObject.name.Contains ("DroidFighter")) {
				coll.gameObject.transform.localEulerAngles = new Vector3 (90, 180, localZ);
			}
			else
				if (coll.gameObject.name.Contains ("Boss")) {
				}
				else {
					coll.gameObject.transform.localEulerAngles = new Vector3 (localX, 90, 270);
				}
			smoke = GameObject.Instantiate (_smokeParticleRes);
			smoke.transform.SetParent (coll.gameObject.transform);
			smoke.transform.localPosition = Vector3.zero;
			smoke.transform.localEulerAngles = new Vector3 (270, 0, 0);
			isSmoke = false;
			Destroy (smoke, 3f);
			Exploresion (coll);
			isSmoke = true;
		}
	}

	void Exploresion (Collider coll)
	{
		explore = GameObject.Instantiate (_exploreParticleRes);
//		Debug.Log (explore);
		explore.transform.SetParent (coll.gameObject.transform);
		explore.transform.localPosition = Vector3.zero;
		explore.transform.localEulerAngles = Vector3.zero;
		
	}

	void OnTriggerEnter (Collider coll)
	{
		if (coll.gameObject != null) {
//			Debug.Log(coll);

			FindHeroBloodBar ();
			GameOverCondition ();
			CollResHandle (coll);

		}
	}

	void BoundaryHandle ()
	{
		if (transform.position.x < (-840)) {
			transform.position = new Vector3 ((-840), transform.position.y, transform.position.z);
		}
		else
			if (transform.position.x > (-610)) {
				transform.position = new Vector3 ((-610), transform.position.y, transform.position.z);
			}
	}

	void AxisEventHandle ()
	{
		float forwardBack = Input.GetAxis ("Vertical");
		if (forwardBack > 0) {
			localZ = 10;
			plocalZ1 = -0.03f;
			plocalZ2 = -0.03f;
			initSpeed = 0.2f;
		}
		else
			if (forwardBack < 0) {
				//			localZ = -10;
				plocalZ1 = 0.03f;
				plocalZ2 = 0.03f;
				initSpeed = -0.2f;
			}
		float leftRight = Input.GetAxis ("Horizontal");
		//		print (leftRight);
		if (leftRight < 0) {
			initHSpeed = 20f;
		}
		else {
			initHSpeed = 20f;
		}
		transform.Translate (new Vector3 (leftRight * Time.deltaTime * (speed + initHSpeed), 0, forwardBack * Time.deltaTime * speed + initSpeed));
	}

	void TransformSetValue ()
	{
		transform.localScale = new Vector3 (localX, localY, localZ);
		transform.localEulerAngles = new Vector3 (0, rotateY, 0);
	}

	void JetSetLocalScale ()
	{
		_particle1.transform.localScale = new Vector3 (plocalX1, plocalY1, plocalZ1);
		_particle2.transform.localScale = new Vector3 (plocalX2, plocalY2, plocalZ2);
	}

	void FindHeroBulletPosition ()
	{
		//		if (timeCount >= 1.0f) {
		_point1 = transform.Find ("BulletPoint1").position;
		_point2 = transform.Find ("BulletPoint2").position;
		_point3 = transform.Find ("BulletPoint3").position;
		_point4 = transform.Find ("BulletPoint4").position;
	}
	//			isBulletOriginPosition = true;
	//			timeCount = 0.0f;
	//		}

	void TimeCount ()
	{
		timeCount += Time.deltaTime;
	}

	void InitHeroBulletAndAddBulletScript ()
	{
		if (localZ > 0) {
			//			if (isBulletOriginPosition) {
			if (Input.GetMouseButtonDown (0)) {
				HeroCastBulletMusic.Play ();
				_bullet1 = GameObject.Instantiate (_bulletRes);
				_bullet2 = GameObject.Instantiate (_bulletRes);
				_bullet3 = GameObject.Instantiate (_bulletRes);
				_bullet4 = GameObject.Instantiate (_bulletRes);
				_bullet1.transform.position = _point1;
				_bullet2.transform.position = _point2;
				_bullet3.transform.position = _point3;
				_bullet4.transform.position = _point4;
				_bullet1.AddComponent<BulletTranslate> ();
				_bullet2.AddComponent<BulletTranslate> ();
				_bullet3.AddComponent<BulletTranslate> ();
				_bullet4.AddComponent<BulletTranslate> ();
				//					isBulletOriginPosition = false;
			}
			//			}
		}
	}

	void AllHandle ()
	{
		AxisEventHandle ();
		BoundaryHandle ();
		TransformSetValue ();
		JetSetLocalScale ();
		TimeCount ();
		FindHeroBulletPosition ();
		InitHeroBulletAndAddBulletScript ();
	}

	// Update is called once per frame
	void Update ()
	{

		AllHandle ();

	}
}
