using UnityEngine;
using System.Collections;

public class DestroyInFiveSeconds : MonoBehaviour {

	void Update () {
		Invoke("InvokeInFiveSeconds",5f);
	}

	void InvokeInFiveSeconds ()
	{
		print ("success");
		Application.LoadLevel ("GameOverScene");
	}
}
