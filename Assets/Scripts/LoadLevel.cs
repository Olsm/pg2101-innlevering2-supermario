using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(LoadLevelWait(1,"Level-1-1"));
	}

	IEnumerator LoadLevelWait(int sleep, string scene)
	{
		yield return new WaitForSeconds(sleep);
		Application.LoadLevel(scene);
	}
}
