using UnityEngine;
using System.Collections;

public class movingPlatform : MonoBehaviour {

	public Transform platform;
	public Transform startTransform;
	public Transform endTranform;

	public float platformSpeed;
	public bool isStatic;
	public float startFallingAfter;
	public bool stopAtFinishPosition;


	public bool fallingTrigger  { get ; set; }
	public float timeToStartFalling { get; set;}
	public bool isFallAvaliable { get;  set; }


	Transform destination;
	Vector3 direction;

	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube (startTransform.position, platform.localScale);

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (endTranform.position, platform.localScale);
	}	

	void Start(){

		SetDestination (startTransform);
		if (startFallingAfter > 0)
			isFallAvaliable = true; 
	}

	void FixedUpdate(){
		if (!isStatic) {
			platform.GetComponent<Rigidbody2D> ().MovePosition (platform.position +
				direction * platformSpeed * Time.fixedDeltaTime);

			if (Vector3.Distance (platform.position, destination.position) < platformSpeed * Time.fixedDeltaTime) {

				if (stopAtFinishPosition) {
					if (Vector3.Distance (platform.position, endTranform.position) < platformSpeed * Time.fixedDeltaTime)
					{
						isStatic = true;
					}
				}

				SetDestination (destination == startTransform ? endTranform : startTransform);
			}
		}

		if (fallingTrigger) {
			timeToStartFalling -= Time.fixedDeltaTime;
			if (timeToStartFalling < 0) {
				fallingTrigger = false;
				isStatic = false;
			}
		}


	}

	void SetDestination(Transform dist){
		destination = dist;
		direction = (destination.position - platform.position).normalized;
	}

}
