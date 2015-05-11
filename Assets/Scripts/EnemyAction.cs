using UnityEngine;
using System.Collections;

public class EnemyAction : MonoBehaviour
{
	GameObject _myPlane;
	float _zDistance;
	float _Distance;
	float _enemySpeed = 20f;
	GameObject BulletPoint;
	GameObject bulletPoint1;
	GameObject bulletPoint2;
	GameObject bulletPoint3;
	GameObject enemyOneBulletRes;
	GameObject enemyTwoBulletRes;
	GameObject enemyBossBulletRes;
	GameObject bloodBarRes;
	public static GameObject bloodBar;
	GameObject[] BossBulletPoint;
	float oneBulletTimeCount=0f;
	float twoBulletTimeCount=0f;
	float bossBulletTimeCount=0f;
	bool isBossBeginMove=false;
	bool isInitBloodBar=true;
	float bulletTimeInterval=2.4f;
	// Use this for initialization
	void Start ()
	{
		_myPlane = GameObject.Find ("omega_fighter");
		enemyOneBulletRes = Resources.Load ("enemyBullet") as GameObject;
		enemyTwoBulletRes=Resources.Load("enemyTwoBullet") as GameObject;
		enemyBossBulletRes=Resources.Load("enemyBossBullet") as GameObject;
		bloodBarRes=Resources.Load("BossDes") as GameObject;
	}
	
	// Update is called once per frame
	void Update ()
	{
		_zDistance = Mathf.Abs (_myPlane.transform.position.z - transform.position.z);
		if((_myPlane.transform.position.z - transform.position.z)>80){
			if(transform.gameObject.name.Contains ("Boss")){

			}else{
				Destroy(transform.gameObject);
			}
		}


		if(isBossBeginMove==true){
			transform.Translate (new Vector3 (0, 0, Time.deltaTime * _enemySpeed/2));
//			transform.Rotate (new Vector3 (0, 0, 1) * 200 * Time.deltaTime);
			bossBulletTimeCount+=Time.deltaTime;
		}

		if (_zDistance < 200f) {

			if (transform.gameObject.name.Contains ("DroidFighter")) {
				twoBulletTimeCount+=Time.deltaTime;
				transform.Translate (new Vector3 (0, 0, Time.deltaTime * _enemySpeed));
				transform.Rotate (new Vector3 (0, 0, 1) * 200 * Time.deltaTime);

			} else if (transform.gameObject.name.Contains ("Boss")) {
				isBossBeginMove=true;
				if(isInitBloodBar){
				bloodBar=GameObject.Instantiate(bloodBarRes);
				isInitBloodBar=false;
				}



//				transform.Translate (new Vector3 (0, 0, Time.deltaTime * _enemySpeed));
//				transform.Rotate (new Vector3 (0, 0, 1) * 200 * Time.deltaTime);
//				bossBulletTimeCount+=Time.deltaTime;


			} else {
				oneBulletTimeCount+=Time.deltaTime;
				transform.Translate (new Vector3 (Time.deltaTime * _enemySpeed, 0, 0));
				transform.Rotate (new Vector3 (1, 0, 0) * 200 * Time.deltaTime);
			}

		}


		_Distance = Vector3.Distance (_myPlane.transform.position, transform.position);
		if (_Distance < 500f) {
			if (transform.gameObject != null) {
				if (transform.gameObject.name.Contains ("DroidFighter")) {
					if(twoBulletTimeCount>=bulletTimeInterval/6*5){
						bulletPoint1=transform.Find("bulletPoint1").gameObject;
						bulletPoint2=transform.Find("bulletPoint2").gameObject;
						bulletPoint3=transform.Find("bulletPoint3").gameObject;
						GameObject point1Bullet =GameObject.Instantiate (enemyTwoBulletRes);
						GameObject point2Bullet =GameObject.Instantiate (enemyTwoBulletRes);
						GameObject point3Bullet =GameObject.Instantiate (enemyTwoBulletRes);

						point1Bullet.transform.position=bulletPoint1.transform.position;
						point2Bullet.transform.position=bulletPoint2.transform.position;
						point3Bullet.transform.position=bulletPoint3.transform.position;

						point1Bullet.AddComponent<EnemyBulletTranslate> ();
						point2Bullet.AddComponent<EnemyBulletTranslate> ();
						point3Bullet.AddComponent<EnemyBulletTranslate> ();

						twoBulletTimeCount=0.0f;
					}
				} else if (transform.gameObject.name.Contains ("Boss")) {
					if(bossBulletTimeCount>=bulletTimeInterval/4*3){
						bossBulletTimeCount=0.0f;
						BossBulletPoint=GameObject.FindGameObjectsWithTag("BossBulletPoint");
						for(int i=0;i<BossBulletPoint.Length;i++){
							GameObject bossBullet=GameObject.Instantiate (enemyBossBulletRes);
							bossBullet.transform.position=BossBulletPoint[i].transform.position;
							bossBullet.AddComponent<EnemyBulletTranslate> ();
						}
					}

				} else {
					if(oneBulletTimeCount>=bulletTimeInterval){
					BulletPoint = transform.Find("BulletPoint").gameObject;
					GameObject enemyBullet = GameObject.Instantiate (enemyOneBulletRes);
					enemyBullet.transform.position = BulletPoint.transform.position;
					enemyBullet.AddComponent<EnemyBulletTranslate> ();
					oneBulletTimeCount=0.0f;
					}
				}
			}

			
		}

	}
}
