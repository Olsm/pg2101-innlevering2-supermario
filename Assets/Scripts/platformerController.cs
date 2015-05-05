using UnityEngine;
using System.Collections;

	public class platformerController : MonoBehaviour
	{
	private GameObject mario;
	private GameObject powerCube;
	private GameObject brick;
	public float paddleSpeed = 0.1f; //Definer fart for paddlen
		private Animator animator;
		private bool isWalking;
		private bool movingRight;
		private bool movingLeft;
	public float yPos = 0.32f;
	public float jumpPower = 0.065f;
	public float jumpStartY = 0.32f;
	public bool touchdown = true;
	public bool falling = false;
		
		
		private Vector2 playerPos =  new Vector2(0.15f, 0.32f); //Utgangsposisjon

		void Start() {
		powerCube = GameObject.Find ("SporsmalsCube");
		mario = GameObject.Find ("SuperMarioSmall");
		animator = mario.GetComponent<Animator>();
		}
		
		void startWalk(){
		animator.SetBool ("Walk", true);
		}
		void stopWalk(){
		animator.SetBool ("Walk", false);
		}
	void faceLeft(){
		Vector3 theScale = transform.localScale;
		theScale.x = -1;
		transform.localScale = theScale;
	}
	void faceRight(){
		Vector3 theScale = transform.localScale;
		theScale.x = 1;
		transform.localScale = theScale;
	}

	void flowerPopup(){
		animator.speed = 1;
	}


	void OnCollisionEnter2D (Collision2D col)
	
	{
		Debug.Log ("Treff");
		falling = true;
		jumpPower = -jumpPower;
			flowerPopup();
	}
		void Update ()
		{

		float xPos = transform.position.x + (Input.GetAxis ("Horizontal") * paddleSpeed);
		playerPos = new Vector2 (xPos, yPos);
		transform.position = playerPos;

		movingLeft = false;
		movingRight = false;
		stopWalk ();

		if (Input.GetKey ("right") && movingLeft == false) {
			startWalk ();
			faceRight ();
			movingRight = true;
		}


		if (Input.GetKey ("left") && movingRight == false) {
			startWalk ();
			faceLeft ();
			movingLeft = true;
		}

		if (yPos <= jumpStartY) {
			touchdown = true;
			falling = false;

		}

		if (Input.GetKeyDown ("up") || Input.GetKeyDown ("space")) {
			if (touchdown) {
				jumpPower = 0.065f;
				touchdown = false;
				falling = false;
				jumpStartY = transform.position.y;
			}
		}

		if (Input.GetKeyUp ("up") || yPos < jumpStartY)

			falling = true;


		if (falling) {
			jumpPower = jumpPower - (0.003f);
			yPos = Mathf.Clamp (transform.position.y + jumpPower, jumpStartY, jumpStartY + 1f);
		}
		if (Input.GetKey ("up") || Input.GetKey ("space")) {

			if (!falling && !touchdown) {
				jumpPower = jumpPower - (0.003f);
				yPos = transform.position.y + jumpPower;
			} 
		}else if(falling){
			jumpPower = jumpPower - (0.003f);
			yPos = Mathf.Clamp (transform.position.y + jumpPower, jumpStartY, jumpStartY + 1f);
		}
		
		
		

		

	}
	}

