using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BgPosition : MonoBehaviour {
	Texture sourceImage;
	public Texture2D blood_red;
	private float HP;
	private float hpInterval;
	public float fina;
	public bool isDesOne=false;
	// Use this for initialization
	void Start () {
		sourceImage=transform.GetComponent<Image>().mainTexture;
		HP= sourceImage.width*4.4f/3;
		fina=sourceImage.width*4.4f/3;
		hpInterval=HP/100;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition=new Vector3(0,Screen.height/2-sourceImage.height/2,0);

			if(isDesOne){
				fina=HP-hpInterval;
				HP=HP-hpInterval;
				isDesOne=false;

		}
		
	}
	void OnGUI()
	{
		GUI.DrawTexture(new Rect((Screen.width/2-sourceImage.width*3/4f),sourceImage.height/4,fina,sourceImage.height/2), blood_red);
	}
}
