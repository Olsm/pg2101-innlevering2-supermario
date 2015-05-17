using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	public float smooth = 10f;			// The relative speed at which the camera will catch up.
	
	
	private Transform player;			// Reference to the player's transform.
	private float newPos;				// The position the camera is trying to reach.

	
	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.Find("Player").transform;
	}
	
	
	void FixedUpdate ()
	{
		newPos = player.transform.position.x;
		
		// Lerp the camera's position between it's current position and it's new position.
		transform.position = Vector3.Lerp(transform.position, new Vector3(newPos, 1.125f, -0.4f), smooth * Time.deltaTime);
	}
}
