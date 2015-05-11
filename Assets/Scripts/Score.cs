using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	string ScoreTest;
	public static int crashScore=0;
	GameObject scoreLabel;
	UILabel UiLabel;
	// Use this for initialization
	void Start () {
		scoreLabel=GameObject.Find("ScoreLable");
		UiLabel=scoreLabel.GetComponent<UILabel>();


	}
	
	// Update is called once per frame
	void Update () {
		ScoreTest="Score:"+crashScore;
		UiLabel.text=ScoreTest;
	}
}
