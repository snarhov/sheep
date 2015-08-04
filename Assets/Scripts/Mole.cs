using UnityEngine;
using System.Collections;

public class Mole : MonoBehaviour {

	Animator anim;
	BoxCollider2D boxCollider;

	bool inGround = true;

	public float inGroundTime;
	public float outOfGroundTime;
	
	float timeFromLastStateChange;

	float damageToPlayerCd = 0.2f;
	float timeFromLastDamageToPlayer;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		boxCollider = GetComponent<BoxCollider2D>();
		timeFromLastStateChange = 0;
	}
	
	// Update is called once per frame
	void Update () {

		timeFromLastDamageToPlayer += Time.deltaTime;

		timeFromLastStateChange += Time.deltaTime;
		if (inGround && timeFromLastStateChange > inGroundTime) {
			timeFromLastStateChange = 0;
			inGround = !inGround;
		} else if (!inGround && timeFromLastStateChange > outOfGroundTime) {
			timeFromLastStateChange = 0;
			inGround = !inGround;
		}


		boxCollider.enabled = !inGround;
		anim.SetBool("inGround", inGround);
	}


	void OnTriggerEnter2D(Collider2D coll) {

		if (coll.gameObject.tag == "Player" && timeFromLastDamageToPlayer > damageToPlayerCd) {
			coll.GetComponent<CharacterController2d>().hp--;
			timeFromLastDamageToPlayer = 0;
		}
	}
	
}
