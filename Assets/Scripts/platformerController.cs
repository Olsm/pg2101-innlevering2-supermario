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
	private float jumpPower = 4f;
	private bool touchdown = false;
	private Rigidbody2D rb;
	private bool jumping = false;

	private BoxCollider2D bodyCollider;
	public BoxCollider2D headCollider;
	public BoxCollider2D feetCollider;
		
		private Vector2 playerPos =  new Vector2(0.15f, 0.32f); //Utgangsposisjon

		void Start() {
			rb = GetComponent <Rigidbody2D>();
			powerCube = GameObject.Find ("QuestionBlock");
			mario = GameObject.Find ("SuperMarioSmall");
			bodyCollider = mario.GetComponent <BoxCollider2D>();
			animator = mario.GetComponent <Animator>();
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

	void OnCollisionEnter2D (Collision2D col)
	{
		Collider2D otherCollider = col.contacts [0].otherCollider;
		Collider2D thisCollider = col.contacts [0].collider;
		Debug.Log ("Enter: " + otherCollider);
		if (thisCollider == feetCollider || otherCollider == feetCollider) {
			touchdown = true;
			jumping = false;
			animator.SetBool ("Jump", false);
		} else if (otherCollider == headCollider) {
			touchdown = false;
			if (col.gameObject.name == "QuestionBlock") {
				foreach (Transform child in col.gameObject.transform)
				{
					Animator qBlockItemAnimator = child.gameObject.GetComponent <Animator>();
					AudioSource audio = child.GetComponent <AudioSource>();
					if (qBlockItemAnimator)
						qBlockItemAnimator.SetTrigger("Start");
					if (audio) 
						audio.Play();
				}
			}
		}
	}
	
	void OnCollisionExit2D (Collision2D col) {
		Collider2D otherCollider = col.contacts [0].otherCollider;
		Collider2D thisCollider = col.contacts [0].collider;
		if (thisCollider == feetCollider || otherCollider == feetCollider) {
			touchdown = false;
		}
	}

	void FixedUpdate() {
		if (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) {
			float xPos = Input.GetAxis ("Horizontal") * marioSpeed;
			yPos = rb.velocity.y;
			if (!jumping)
				rb.velocity = new Vector2 (xPos, yPos);
			else {
				rb.velocity = new Vector2 (rb.velocity.x, jumpPower);
				jumping = false;
			}
		}
	}

		void Update ()
		{

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

		// Jump
		if ((Input.GetKeyDown ("up") || Input.GetKeyDown ("space")) && !jumping && touchdown) {
			jumping = true;
			animator.SetBool ("Jump", true);
		}

		

	}
	}

