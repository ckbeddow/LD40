using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	Camera viewCamera;
	PlayerMovement mover;
	public WaterJet waterSpray;
	GameManager manager;

	public float speed = 2f;


	// Use this for initialization
	void Start () {
		viewCamera = Camera.main;
		mover = GetComponent<PlayerMovement> ();
		manager = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		float xMove = Input.GetAxisRaw ("Horizontal");
		float yMove = Input.GetAxisRaw ("Vertical");

		Vector3 moveDirection = new Vector3 (xMove, 0f, yMove);
		if (manager.gameRunning) {
			mover.Move (moveDirection.normalized * speed);
		}
		//this.transform.Translate (moveDirection.normalized * speed * Time.deltaTime);

		Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
		float rayDistance;

		if(groundPlane.Raycast(ray, out rayDistance)){
			Vector3 point = ray.GetPoint(rayDistance);
			Debug.DrawLine(ray.origin, point, Color.red);
			waterSpray.LootAt (point);
		}

		if (Input.GetMouseButtonDown (0)) {
			if (manager.gameRunning) {
				Debug.Log ("spray on");
				waterSpray.TurnOn ();
			}

		}

		if (Input.GetMouseButtonUp (0)) {
			Debug.Log ("spray off");
			waterSpray.TurnOff ();
		}

		if(Input.GetKeyDown(KeyCode.Escape)){
			manager.PauseGame ();
		}
	}


}
