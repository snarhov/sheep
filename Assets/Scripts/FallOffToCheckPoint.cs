using UnityEngine;
using System.Collections;

public class FallOffToCheckPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
		
			coll.gameObject.transform.position = new Vector3(5f, 5f, 0);
			coll.gameObject.GetComponent<CharacterController2d>().hp--;
		}
	}
}
