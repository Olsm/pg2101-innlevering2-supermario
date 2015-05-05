using UnityEngine;
using System.Collections;

public class gameLogic : MonoBehaviour {
	private GameObject mario;
	private GameObject powerCube;
	private GameObject brick;

	// Use this for initialization
	void Start () {
		powerCube = GameObject.Find ("SporsmalsCube");
		mario = GameObject.Find ("SuperMarioSmall");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
