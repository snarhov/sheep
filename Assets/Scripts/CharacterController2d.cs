using UnityEngine;
using System.Collections;

public class CharacterController2d : MonoBehaviour, IKnockBackAffectable {


	public int hp;
	public int maxhp;

	public float maxSpeed = 10f;
	public float moveForce = 20f;
	public bool isFixedJumpForce;
	public bool jumpWhenKeyDown;

	public float fixedJumpForce;
	public float minJumpForce;
	public float maxJumpForce; 
	float jumpForce;

	public bool isDoubleJumpAvaliable; 
	bool jumpInThisFrame = false;
	float jumpCharge;
	float jumpStartTime;
	public float maxJumpChargeSeconds = 1f;
	
	bool jumpExecution;
	float addingJumpForce;
	float jumpFinishTime;
	bool jumpAvaliable;

	bool doubleJump = false; // true if character already use doubleJump ability, reset this to false when grounded

	bool facingRight = true;
	float move;
	float movePrev = 0;
	bool jump;
	
	bool grounded = false;
	float groundRadius = 0.3f;
	public Transform  groundCheck;
	public LayerMask whatIsGround;	


	bool fire;
	bool firePrev = false;

	Animator anim;
	Rigidbody2D rb;
	public GameObject bullet;
	GameObject createdBullet;
	public MobileButtons mobileButtons;
	
	public float nextKnockBack { get ;  set;}
	public Vector2 KnockbackForce { get; set;}

	public bool inWater { get; set; }
	public float waterResistance { get; set;}
	public float waterFlowForce{ get; set;}

	public GameObject objectToPull { get; set;}
	bool isPulling = false;
	public float pullDistance;
	Vector3 distance ;

	
	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();

		distance = Vector3.zero;
	}
	
	void FixedUpdate () {

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);


		//if (Mathf.Abs(rb.velocity.y) > 0.001f) {
		//	grounded = false;
		//}

		if (inWater) {

			rb.AddForce(new Vector2(waterFlowForce, 0));
		}

		#region JumpLogic
	
		if (isFixedJumpForce) {
			jumpForce = fixedJumpForce;
			jumpInThisFrame = jump;
		}

		if (!isFixedJumpForce && jumpWhenKeyDown) {
			jumpForce = minJumpForce;
			addingJumpForce = (maxJumpForce-minJumpForce)*(Time.fixedDeltaTime/maxJumpChargeSeconds);
		}

		if (!isDoubleJumpAvaliable) {
			doubleJump = true;
		} else if (grounded) {
			doubleJump = false;
		}

		if ((grounded || !doubleJump) && jumpInThisFrame) {


			anim.SetBool("Ground", false);
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(new Vector2(0, jumpForce));

			if (!isFixedJumpForce){
				jumpCharge = 0;
			}
			if (jumpWhenKeyDown){
				jumpExecution = true;
			}
			jumpInThisFrame = false;

			if (!doubleJump && !grounded){
				doubleJump = true;
			}
		}

		if (jumpExecution) {
			rb.AddForce(new Vector2(0, addingJumpForce));
		}

		#endregion

		anim.SetBool ("Ground", grounded);
		anim.SetFloat ("vSpeed", rb.velocity.y);
		anim.SetFloat ("Speed", Mathf.Abs (move));




	//	rb.velocity = new Vector2 (move * maxSpeed, rb.velocity.y);

		if (Mathf.Abs(rb.velocity.x) < maxSpeed) {
			rb.AddForce (new Vector2 (move * moveForce, 0));
		}

		if ((movePrev!= 0) && (move == 0)) {
			rb.velocity = new Vector2(rb.velocity.x*0.5f, rb.velocity.y);
		}



		if (isPulling && fire && !firePrev) {
			
			objectToPull.GetComponent<DistanceJoint2D>().enabled = false;
			isPulling = false;
			objectToPull = null;
		}

		if (objectToPull != null && fire && !firePrev && !isPulling) {
			
			distance =  objectToPull.transform.position - transform.position;

			if (distance.magnitude < pullDistance){
				
				objectToPull.GetComponent<DistanceJoint2D>().enabled = true;
				isPulling = true;		
			} 
		}





		if (move >0 && !facingRight){
			Flip ();
		} else if (move <0 && facingRight) {
			Flip ();
		}

		movePrev = move;
		firePrev = fire;
	} 

	// Update is called once per frame
	void Update(){

		if (Application.platform == RuntimePlatform.Android) {
			move = mobileButtons.move;
			fire = mobileButtons.fire;
			jump = mobileButtons.jump;
		}

		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
			move = Input.GetAxis ("Horizontal");
			jump = Input.GetKey(KeyCode.Space); 
			fire = Input.GetKeyDown (KeyCode.F);
		}

		#region JumpLogic
		if (!isFixedJumpForce && jumpWhenKeyDown) {
		
			if (!jumpExecution && jump && jumpAvaliable){
				jumpInThisFrame = true;
				jumpFinishTime = Time.time+maxJumpChargeSeconds;
			} 


			if (jumpExecution && jumpFinishTime<Time.time) {
				jumpExecution = false;
				jumpAvaliable = false;
			}

			if (!jumpAvaliable && !jump){
				jumpAvaliable = true;
			}

			if (jumpExecution && !jump) {
				jumpExecution = false;
			}
		
		}

		if (!isFixedJumpForce && !jumpInThisFrame && !jumpWhenKeyDown) {


			if (jump && (jumpStartTime<Mathf.Epsilon)) {
				jumpStartTime = Time.time;
			}
		
			if (jumpStartTime > Mathf.Epsilon){
			jumpCharge = Time.time - jumpStartTime;
			}

			if (jumpCharge > maxJumpChargeSeconds) {
				jumpInThisFrame = true;
				jumpStartTime = 0f;
				CalculateJumpForce();
				jumpCharge = 0f;

			}

			if (!jump && (jumpCharge > Mathf.Epsilon)){
				jumpInThisFrame = true;
				jumpStartTime = 0f;
				CalculateJumpForce();
				jumpCharge = 0f;
			
			}
		}

		#endregion



	}



	
	// Change sprite facing direction
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void CalculateJumpForce(){

		if (jumpCharge >= maxJumpChargeSeconds) {
			jumpForce = maxJumpForce; 
		} else {
			jumpForce = minJumpForce + (maxJumpForce-minJumpForce)*(jumpCharge/(maxJumpChargeSeconds));
		}
	}

	public void KnockBackAffected(){
		rb.AddForce (KnockbackForce);
	}
	
}
