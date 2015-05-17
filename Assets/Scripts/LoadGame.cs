using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(LoadLevelWait(3,"WorldLoadingScreen"));
	}
	
	IEnumerator LoadLevelWait(int sleep, string scene)
	{
		yield return new WaitForSeconds(sleep);
		Application.LoadLevel(scene);
	}
}
