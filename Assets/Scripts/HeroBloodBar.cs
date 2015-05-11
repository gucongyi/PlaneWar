using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HeroBloodBar : MonoBehaviour {

	Texture sourceImage;
	public Texture blood_red;
	private float HP;
	private float hpInterval;
	public float fina;
	public bool isDesOne=false;
	public bool isDesFive=false;
	public bool isDesTwenty=false;

	// Use this for initialization
	void Start () {
		sourceImage=transform.GetComponent<Image>().mainTexture;
		HP= sourceImage.width*4.4f/12*5.3f;
		fina=sourceImage.width*4.4f/12*5.3f;
		hpInterval=HP/1000;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition=new Vector3(0,-(Screen.height/2-sourceImage.height/2),0);
		
		if(isDesOne){
			fina=HP-hpInterval*2;
			HP=HP-hpInterval*2;
			isDesOne=false;
			
		}

		if(isDesFive){
			fina=HP-hpInterval*5;
			HP=HP-hpInterval*5;
			isDesFive=false;
			
		}
		if(isDesTwenty){
			fina=HP-hpInterval*20;
			HP=HP-hpInterval*20;
			isDesTwenty=false;
			
		}
		
	}
	void OnGUI()
	{
		GUI.DrawTexture(new Rect((Screen.width/2-sourceImage.width),Screen.height-sourceImage.height/4*2.6f,fina,sourceImage.height/4), blood_red);
	}
}
