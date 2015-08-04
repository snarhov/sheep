using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform target;
	public float smooth= 5.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 newPosition = new Vector3 (target.position.x, transform.position.y, transform.position.z);
		
		transform.position = Vector3.Lerp (transform.position, newPosition,Time.deltaTime * smooth);

	
	}


}
