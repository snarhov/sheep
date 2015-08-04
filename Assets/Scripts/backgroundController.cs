using UnityEngine;
using System.Collections;

public class backgroundController : MonoBehaviour {


	public Camera cam;
	public Transform[] backgrounds;
	private float intersectionEps = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float screenAspect = (float)Screen.width / (float)Screen.height;
		float cameraHeight = cam.orthographicSize * 2;
		float cameraWidth = cameraHeight * screenAspect;

		float cameraLeftBoundX = cam.transform.position.x - cameraWidth / 2;
		float cameraRightBoundX = cam.transform.position.x + cameraWidth / 2;
		
		int nextBG;
		int prevBG;

		for (int i =0; i<backgrounds.Length; i++) {
			if (backgrounds.Length == i + 1){
				nextBG = 0;
				prevBG = i-1;
			} else if (0 == i){
				nextBG = i+1;
				prevBG = backgrounds.Length - 1;
			} else {
				nextBG = i+1;
				prevBG = i-1;
			}

			float bgRightBoundX = backgrounds[i].GetComponent<Transform>().position.x+backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.x/2;
			float nextBgWidth = backgrounds[nextBG].GetComponent<SpriteRenderer>().bounds.size.x;

			if (bgRightBoundX+nextBgWidth < cameraLeftBoundX) {
				backgrounds[i].transform.position = new Vector3(backgrounds[i].transform.position.x + nextBgWidth*3 - intersectionEps,
				                                                backgrounds[i].transform.position.y,
				                                                backgrounds[i].transform.position.z); 
			}

			float bgLeftBoundX = backgrounds[i].GetComponent<Transform>().position.x-backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.x/2;
			float prevBgWidth = backgrounds[prevBG].GetComponent<SpriteRenderer>().bounds.size.x;
			
			if (bgLeftBoundX-prevBgWidth > cameraRightBoundX) {
				backgrounds[i].transform.position = new Vector3(backgrounds[i].transform.position.x - prevBgWidth*3 + intersectionEps,
				                                                backgrounds[i].transform.position.y,
				                                                backgrounds[i].transform.position.z); 
			}




		}



	}
}
