using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
public class GenerateCube : MonoBehaviour
{
	public GameObject _GenerateCubeRes;
	GameObject[] _GeneratePrefs;
	Vector3[] modelPositon;
	// Use this for initialization
	void Start ()
	{
//		_GenerateCubeRes = Resources.Load ("GenerateCube") as GameObject;
		Debug.Log (_GenerateCubeRes);


	}

	  
	private void createOrWriteConfigFile(string path,string name,string info)
	{
		StreamWriter streamWriter;
		FileInfo fileInfo=new FileInfo(path+"//"+name);
		if(!fileInfo.Exists){
			streamWriter=fileInfo.CreateText();
		}else{
//			streamWriter=fileInfo.AppendText();
			streamWriter=fileInfo.CreateText();
		}
		streamWriter.WriteLine(info);
		streamWriter.Close();
		streamWriter.Dispose();
	}
	// Update is called once per frame
	void Update ()
	{
//		print (Input.mousePosition);
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				GameObject mycube = null;
				if (hit.point.x >= 0 && hit.point.x <= 10
					&& hit.point.z >= 0 && hit.point.z <= 10) {
					if (mycube == null) {
						Debug.Log ("x:" + Mathf.Floor (hit.point.x) + " z:" + Mathf.Floor (hit.point.z));
						if(_GenerateCubeRes!=null){
						mycube = GameObject.Instantiate (_GenerateCubeRes) as GameObject;
						mycube.transform.position = new Vector3 (Mathf.Floor (hit.point.x) + 0.5f, 0.5f, Mathf.Floor (hit.point.z) + 0.5f);
						}
					}
				}
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				if (hit.point.x >= 0 && hit.point.x <= 10
					&& hit.point.z >= 0 && hit.point.z <= 10) {
					if (hit.transform.position.y > 0.4f && hit.transform.position.y < 0.6f) {
						Debug.Log (hit.transform.position.y);
						Destroy (hit.transform.gameObject);
					}
				}
			}
		}
	}


	public void OnButtonClick(){

		Debug.Log("ButtonClick");
		StringBuilder info=new StringBuilder("");
		_GeneratePrefs=GameObject.FindGameObjectsWithTag("GeneratePref");
		for(int i=0;i<_GeneratePrefs.Length;i++){
			info.Append(_GeneratePrefs[i].transform.position.x+",")
				.Append(_GeneratePrefs[i].transform.position.y+",")
				.Append(_GeneratePrefs[i].transform.position.z+";");
		}
		createOrWriteConfigFile(Application.dataPath,"Generate.txt",info.ToString());

	}

	
}
