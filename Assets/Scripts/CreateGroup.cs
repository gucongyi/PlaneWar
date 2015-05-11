using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
public class CreateGroup : MonoBehaviour {
	GameObject[] _GeneratePrefs;
	Vector3[] modelPositon;
	public GameObject Pref;
	GameObject[] _instantiate;
	public TextAsset asset;
	void Start ()
	{
		string fileStringInfo=GetFileStringInfo ();
		ParseStringToVector3Finish (fileStringInfo);
		_instantiate=new GameObject[modelPositon.Length];
		for(int i=0;i<_instantiate.Length;i++){
			_instantiate[i]=Instantiate(Pref);
			_instantiate[i].transform.parent=transform;
			_instantiate[i].transform.localPosition=modelPositon[i];
		}
	}
	void ParseStringToVector3Finish (string fileStringInfo)
	{
		string[] stringArray = fileStringInfo.Split (';');
		modelPositon = new Vector3[stringArray.Length - 1];
		for (int i = 0; i < stringArray.Length - 1; i++) {
			float positionX = 0f;
			float positionY = 0f;
			float positionZ = 0f;
			string[] iPosition = stringArray [i].Split (',');
			for (int j = 0; j < iPosition.Length; j++) {
				positionX = float.Parse (iPosition [0]);
				positionY = float.Parse (iPosition [1]);
				positionZ = float.Parse (iPosition [2]);
			}
			modelPositon [i] = new Vector3 (positionX, positionY, positionZ);
			Debug.Log (modelPositon [i]);
		}
	}

	string GetFileStringInfo ()
	{
		string fileGetPosition;
		StringBuilder line = LoadFile (Application.dataPath, "Generate.txt");
		fileGetPosition = line.ToString ();
		print (fileGetPosition);
		return fileGetPosition;
	}

	private StringBuilder LoadFile(string path,string name)    
	{     

		StringBuilder myStringBuilder=new StringBuilder("");
		FileInfo fileInfo=new FileInfo(path+"//"+name);
		if(!fileInfo.Exists){
			return myStringBuilder.Append("error");
		}
		StreamReader streamReader=null;
		streamReader=File.OpenText(path+"//"+name);
		string line="";
		while((line=streamReader.ReadLine())!=null){
			myStringBuilder.Append(line);
		}
		streamReader.Close();
		streamReader.Dispose();
		return myStringBuilder;
//		return new StringBuilder(asset.text);
	}

}
