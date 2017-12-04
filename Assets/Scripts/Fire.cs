using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	public float growthRate = .1f;
	public float maxSize = 10f;
	bool growing = true;

	public float expandDistance = 12f;
	public float expandTimer = 5f;
	float expandCountDown;

	public GameObject fire;

	GameManager manager;
	// Use this for initialization
	void Start () {
		manager = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (growing && manager.gameRunning) {
			Grow ();

		}
	
		//Debug.Log (this.transform.localScale.x);

		if (AtMaxSize() && manager.gameRunning) {
			Debug.Log ("max size");
			expandCountDown += Time.deltaTime;
			if (expandCountDown > expandTimer) {
				Expand ();
				expandCountDown -= expandTimer;
			}
		}

	}

	bool AtMaxSize(){
		if (this.transform.localScale.x < maxSize) {
			return false;
		} else {
			return true;
		}
	}

	void Grow(){
		float growth = (growthRate * Time.deltaTime);
		Vector3 newSize = new Vector3 (this.transform.localScale.x + growth, 1f, this.transform.localScale.z + growth);
		if (this.transform.localScale.x < maxSize) {
			this.transform.localScale = newSize;
		}
	}

	void PutOut(float shrinkRate){
		float shrinkage = (shrinkRate * Time.deltaTime);
		Vector3 newSize = new Vector3 (this.transform.localScale.x - shrinkage, 1f, this.transform.localScale.z - shrinkage);

		this.transform.localScale = newSize;

		if (this.transform.localScale.x <= .3) {
			Debug.Log ("destroyed");
			manager.FireFought ();
			Destroy (this.gameObject);
		}
		growing = true;
	}

	void Expand(){
		Vector3 randomSpawnPoint = RandomSpawnPoint ();

		manager.SpawnFire (randomSpawnPoint);

	}

	bool isOutOfBounds(Vector3 point){
		if (point.x < -25f || point.x > 25f || point.z < -25f || point.z > 25f) {
			return true;
		} else
			return false;
	}

	Vector3 RandomSpawnPoint(){
		Vector3 randomSpawnPoint = new Vector3 (30f, 0, 0);
		do {
			Vector3 randomDirection = Random.onUnitSphere * expandDistance;
			randomSpawnPoint = new Vector3 (randomDirection.x + this.transform.position.x, 0f, randomDirection.z + this.transform.position.z);
		} while(isOutOfBounds (randomSpawnPoint));
		return randomSpawnPoint;
	}

	void OnTriggerEnter(Collider colInfo){
		Debug.Log ("hit");

	}

	void OnTriggerStay(Collider col){
		if (col.tag == "Water") {
			growing = false;
			PutOut (col.GetComponent<WaterJet>().strength);
		}
	}

}
