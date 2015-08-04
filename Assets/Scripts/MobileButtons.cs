using UnityEngine;
using System.Collections;

public class MobileButtons : MonoBehaviour {

	public float move;
	public bool fire;
	public bool jump;

	public  void leftDown() {
		move = -1;
	}

	public  void leftUp() {
		move = 0;
	}

	public  void rightDown() {
		move = 1;
	}
	
	public  void rightUp() {
		move = 0;
	}

	public  void jumpDown() {
		jump = true;
	}
	
	public  void jumpUp() {
		jump = false;
	}
	
	public  void fireDown() {
		fire = true;
	}
	
	public  void fireUp() {
		fire = false;
	}



	// Use this for initialization
	void Start () {
	 
	}


	// Update is called once per frame
	void Update () {

	}
}
