using UnityEngine;
using System.Collections;

	public class platformerController : MonoBehaviour
	{
	private GameObject mario;
	private GameObject powerCube;
	private GameObject brick;
	private float marioSpeed = 1f; //Definer fart for paddlen
		private Animator animator;
		private bool isWalking;
		private bool movingRight;
		private bool movingLeft;
	private float yPos = 0.32f;
	private float jumpPower = 0.07f;
	private float jumpStartY = 0.32f;
	private bool touchdown = true;
	private bool falling = false;
	private Rigidbody rb;
	private bool collision = false;
	private bool jumping = false;
		
		private Vector2 playerPos =  new Vector2(0.15f, 0.32f); //Utgangsposisjon

		void Start() {
			powerCube = GameObject.Find ("SporsmalsCube");
			mario = GameObject.Find ("SuperMarioSmall");
			animator = mario.GetComponent <Animator>();
			rb = GetComponent <Rigidbody>();
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

	void flowerPopup() {
		animator.speed = 1;
	}

	void OnCollisionEnter (Collision col)
	{
		string parentName = "none";
		if (col.gameObject.transform.parent)
			parentName = col.gameObject.transform.parent.name;
		if (parentName != "Bakke") {
			collision = true;
			Debug.Log ("Treff");
			falling = true;
			jumpPower = -jumpPower;
			flowerPopup ();

			if (col.gameObject.name == "prop_powerCube") {
				Destroy (col.gameObject);
			}
		}
	}
	
	void OnCollisionExit (Collision col) {
		collision = false;
	}

		void Update ()
		{

		if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) {
			float xPos = transform.position.x + (Input.GetAxis ("Horizontal") * Time.deltaTime * marioSpeed);
			if (!jumping || collision) 
				yPos = transform.position.y;
			playerPos = new Vector3 (xPos, yPos, -0.01f);
			transform.position = playerPos;
		}

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

		if (yPos <= jumpStartY && touchdown == false) {
			touchdown = true;
			falling = false;
			animator.SetBool ("Jump", false);
			jumping = false;
		}

		// Jump
		if ((Input.GetKeyDown ("up") || Input.GetKeyDown ("space")) && !jumping && touchdown) {
			jumping = true;
			animator.SetBool ("Jump", true);
			if (touchdown) {
				jumpPower = 0.065f;
				touchdown = false;
				falling = false;
				jumpStartY = transform.position.y;
				falling = true;
			}
		}

		if(falling){
			jumpPower = jumpPower - (0.003f);
			yPos = Mathf.Clamp (transform.position.y + jumpPower, jumpStartY, jumpStartY + 1f);
		}
		else if (Input.GetKey ("up") || Input.GetKey ("space")) {
			if (!falling && !touchdown) {
				jumpPower = jumpPower - (0.003f);
				yPos = transform.position.y + jumpPower;
				if (Input.GetKeyUp ("up") || jumpPower < 0)
					falling = true;
			}
		}

		

	}
	}

