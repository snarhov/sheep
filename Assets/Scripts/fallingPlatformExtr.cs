using UnityEngine;
using System.Collections;

public class fallingPlatformExtr : MonoBehaviour {
	
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player" && GetComponentInParent<movingPlatform>().isFallAvaliable) {
			
			Debug.Log ("Coll");
			GetComponentInParent<movingPlatform>().fallingTrigger = true;
			GetComponentInParent<movingPlatform>().timeToStartFalling = GetComponentInParent<movingPlatform>().startFallingAfter;
			GetComponentInParent<movingPlatform>().isFallAvaliable = false;
			
		}
		
	}
}
