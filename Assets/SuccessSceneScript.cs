using UnityEngine;
using System.Collections;

public class SuccessSceneScript : MonoBehaviour {

	GameObject scoreLabel;
	UILabel UiLabel;

	GameObject ScoreUI;
	void Awake() {
		ScoreUI=GameObject.Find("ScoreUI");

	}
	// Use this for initialization
	void Start () {
		scoreLabel=GameObject.Find("SuccessSceneLabel");
		UiLabel=scoreLabel.GetComponent<UILabel>();

		
	}
	
	// Update is called once per frame
	void Update () {
		UiLabel.text="YourScore:"+Score.crashScore;
	}

	public void GameRestart(){
		Score.crashScore=0;
		Destroy(ScoreUI);
		Application.LoadLevel("Load");

	}
	public void GameExit(){
		Application.Quit();
	}
}
