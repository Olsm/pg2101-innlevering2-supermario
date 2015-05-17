using UnityEngine;
using System.Collections;

public class platformerController : MonoBehaviour
{
	private GameObject mario;
	private GameObject powerCube;
	private GameObject brick;
	public float marioSpeed = 0.025f; //Definer fart for paddlen
	private Animator animator;
	private bool isWalking;
	private bool movingRight;
	private bool movingLeft;
	private float yPos = 0.32f;
	private float touchdownPos = 0.338f;
	public float jumpPower = 2f;
	public float jumpHeight = 0.55f;
	public float maxHeight = 2.3f;
	public bool touchdown = false;
	private Rigidbody2D rb;
	private bool jump = false;
	public bool jumping = false;
	public bool fallDown = false;
	
	private PolygonCollider2D bodyCollider;
	public BoxCollider2D headCollider;
	public BoxCollider2D feetCollider;
	
	private Vector2 playerPos =  new Vector2(0.15f, 0.32f); //Utgangsposisjon
	
	void Start() {
		rb = GetComponent <Rigidbody2D>();
		powerCube = GameObject.Find ("QuestionBlock");
		mario = GameObject.Find ("SuperMarioSmall");
		bodyCollider = mario.GetComponent <PolygonCollider2D>();
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
		jumping = false;
		Collider2D otherCollider = col.contacts [0].otherCollider;
		Collider2D thisCollider = col.contacts [0].collider;
		Debug.Log ("Enter: " + otherCollider);
		if (otherCollider == bodyCollider) {
			if (thisCollider.gameObject.name == "QuestionBlock" || thisCollider.gameObject.name == "Brick" || transform.position.y > touchdownPos + 0.01f) {
				rb.velocity = Vector2.zero;
				fallDown = true;
			}
		} else if (otherCollider == feetCollider) {
			touchdownPos = transform.position.y;
			fallDown = false;
			animator.SetBool ("Jump", false);
		} else if (otherCollider == headCollider && !fallDown) {
			if (col.gameObject.name == "QuestionBlock") {
				foreach (Transform child in col.gameObject.transform) {
					Animator qBlockItemAnimator = child.gameObject.GetComponent <Animator> ();
					AudioSource audio = child.GetComponent <AudioSource> ();
					if (qBlockItemAnimator)
						qBlockItemAnimator.SetTrigger ("Start");
					if (audio) 
						audio.Play ();
					if (col.gameObject.transform.Find ("Coin") && child.name == "PointsCanvas") {
						Animator pointsAnimator = child.gameObject.transform.Find ("PointsText").GetComponent <Animator> ();
						if (pointsAnimator)
							pointsAnimator.SetTrigger ("Start");
					} 
				}
			} else if (col.gameObject.name == "Brick" && !fallDown) {
				Destroy (col.gameObject);
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

	void OnCollisionStay2D (Collision2D col) {
		touchdown = true;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.name == "FlagPole") {
			Debug.Log("You won! - Y pos = " + transform.position.y);
			Debug.Break();
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.name == "GameOverCollider") {
			GetComponent<AudioSource>().Stop();
			other.GetComponent<AudioSource>().Play();
			StartCoroutine(LoadLevelWait(4,"GameOver"));
		}
	}

	IEnumerator LoadLevelWait(int sleep, string scene)
	{
		yield return new WaitForSeconds(sleep);
		Application.LoadLevel(scene);
	}
	
	void FixedUpdate() {
		if (!fallDown && (Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0)) {
			float xPos = Input.GetAxis ("Horizontal") * marioSpeed;
			yPos = rb.position.y;
			transform.position = new Vector2 (rb.position.x + xPos, yPos);
			if (transform.position.y > maxHeight) {
				transform.position = new Vector2 (rb.position.x, maxHeight);
				jumping = false;
			} else if (jump) {
				if (rb.velocity.y >= 0) 
					rb.velocity = new Vector2 (rb.velocity.x, jumpPower);
				jump = false;
			}
		} else if (fallDown) 
			transform.position = new Vector2 (transform.position.x, transform.position.y - 0.02f);
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
		if ((Input.GetKeyDown ("up") || Input.GetKeyDown ("space")) && !jump && touchdown) {
			jumping = true;
			animator.SetBool ("Jump", true);
		}

		if (Input.GetKey("up")) {
			if (jumping && transform.position.y < (touchdownPos + jumpHeight))
				jump = true;
			else 
				jumping = false;
		}
		
		
		
	}
}
