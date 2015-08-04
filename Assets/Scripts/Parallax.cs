using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {


	public Transform[] backgrounds;
	private float[] parallaxScales; // Load this values from backgrounds[i].position.z 
	public float smoothing = 1f;

	private Transform cam;
	private Vector3 previousCameraPosition;

	void Awake(){
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {

		previousCameraPosition = cam.position;
		parallaxScales =  new float[backgrounds.Length];
	
		for (int i =0; i<backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds[i].position.z;
		}
	}
	
	// Update is called once per frame
	void Update () {

		for (int i=0; i<backgrounds.Length; i++){
			float parallaxEffect = (previousCameraPosition.x - cam.position.x) * parallaxScales[i];
			float backgroundTargetPositionX = backgrounds [i].position.x + parallaxEffect;

			Vector3 backgroundTargetPosition = new Vector3(backgroundTargetPositionX, 
		                                               backgrounds[i].position.y,
		                                               backgrounds[i].position.z);

			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position,
			                                       backgroundTargetPosition,
			                                       smoothing * Time.deltaTime);
			
		}

		previousCameraPosition = cam.position;

	}
}
