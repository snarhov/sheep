using UnityEngine;
using System.Collections;

interface IKnockBackAffectable {


	float nextKnockBack {get ; set;}
	Vector2 KnockbackForce { get; set;}
	void KnockBackAffected();

}

