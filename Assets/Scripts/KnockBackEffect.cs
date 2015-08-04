using UnityEngine;
using System.Collections;

public class KnockBackEffect : MonoBehaviour {

	public float knockbackForce;
	public Vector2 knockbackScale;
	public float velocityKoefficient;
	public float knockbackCooldown;	
	
	ArrayList KnockBackedObjects = new ArrayList();
	
	void FixedUpdate(){
		
		foreach (GameObject obj in KnockBackedObjects) {
			obj.SendMessage("KnockBackAffected");
		}
		
		KnockBackedObjects.Clear ();
	}
	
	void OnTriggerStay2D(Collider2D coll) {
		
		
		if (coll.gameObject.tag == "Player") {
			GameObject player = coll.gameObject;
			
			if ( player.GetComponent<CharacterController2d>().nextKnockBack < Time.time) {
				player.GetComponent<CharacterController2d>().nextKnockBack =  Time.time + knockbackCooldown;
				Vector2 forceDir = getForceDirection (transform.position, player.transform.position);
				
				
				Vector2 kf = new Vector2();
				Vector2 vk = new Vector2(0, 0);
				
				if (player.GetComponent<Rigidbody2D>() != null) {
					
					
					vk.x = forceDir.x * velocityKoefficient * player.GetComponent<Rigidbody2D> ().velocity.x;
					vk.y = forceDir.y * velocityKoefficient * player.GetComponent<Rigidbody2D> ().velocity.y;
				}
				
				kf.x = forceDir.x * (knockbackForce - vk.x);
				kf.y = forceDir.y * (knockbackForce - vk.y);
				
				player.GetComponent<CharacterController2d>().KnockbackForce = kf;
				
				KnockBackedObjects.Add(coll.gameObject);
			}
		}		
	}
	
	Vector2 getForceDirection(Vector3 fire, Vector3 obj){
		Vector2 q = new Vector2(knockbackScale.x, knockbackScale.y);
		Vector2 r = new Vector2 (fire.x - obj.x, fire.y - obj.y);
		
		if (r.x < 0)
			q.x *= -1;
		if (r.y < 0)
			q.y *= -1;
		return q*-1;
	}
}
