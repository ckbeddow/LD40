using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Rigidbody rb;
	Vector3 velocity;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	public void Move(Vector3 vel){
		velocity = vel;
	}

	void FixedUpdate () {
		rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
	}
}
