using UnityEngine;
using System.Collections;

public class selector : MonoBehaviour {

	public GameObject player1Start;
	public GameObject player2Start;
	private bool oneplayer = true;
	// Use this for initialization
	void Start () {

		player1Start = GameObject.Find ("select");
		player2Start = GameObject.Find ("select2");
	
	}
	
	// Update is called once per frame
	void Update () {

				if (Input.GetKeyDown ("up")) {
			player1Start.transform.position = new Vector3(player1Start.transform.position.x,player1Start.transform.position.y,3f);
			player2Start.transform.position = new Vector3(player2Start.transform.position.x,player2Start.transform.position.y,-3f);
			oneplayer = true;
		}

				if (Input.GetKeyDown ("down")) {
			player1Start.transform.position = new Vector3(player1Start.transform.position.x,player1Start.transform.position.y,-3f);
			player2Start.transform.position = new Vector3(player2Start.transform.position.x,player2Start.transform.position.y,3f);
			oneplayer = false;
		}
		if (Input.GetKeyDown ("return")) {
						if (oneplayer) {
							Application.LoadLevel("WorldLoadingScreen");
						}
				}

		}
	
}
