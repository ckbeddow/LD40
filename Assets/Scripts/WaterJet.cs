using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterJet : MonoBehaviour {


	public GameObject sprayGFX;
	public float waterTankCapacity = 100f;
	float waterTankLevel;
	public float sprayRate = 5f;
	public float strength = .3f;
	public Image waterFill;
	GameManager manager;
	AudioSource audio;

	BoxCollider boxC;

	bool spraying = false;
	// Use this for initialization
	void Start () {
		manager = FindObjectOfType<GameManager> ();
		waterTankLevel = waterTankCapacity;
		boxC = GetComponent<BoxCollider> ();
		boxC.enabled = false;
		audio = GetComponent<AudioSource> ();
	}

	void Update(){
		if (spraying) {
			DepleteTank (sprayRate);
		}
		waterFill.fillAmount = waterTankLevel / waterTankCapacity;

		if (waterTankLevel < 0.01) {
			TurnOff ();
		}
	}
	
	public void TurnOn(){
		if (waterTankLevel > 0.01) {
			sprayGFX.SetActive (true);
			spraying = true;
			boxC.enabled = true;
			audio.Play ();
		}
	}

	public void TurnOff(){
		sprayGFX.SetActive (false);
		spraying = false;
		boxC.enabled = false;
		audio.Pause ();
	}


	public void LootAt(Vector3 lookPoint){
		Vector3 heightCorrectedPoint = new Vector3 (lookPoint.x, transform.position.y, lookPoint.z);
		transform.LookAt (heightCorrectedPoint);
	}

	public void DepleteTank(float rate){
		if (waterTankLevel > 0) {
			float waterloss = rate * Time.deltaTime;
			manager.waterUsed += waterloss;
			waterTankLevel -= waterloss;
		}
	}

	public void RefillTank(float rate){
		if (waterTankLevel < waterTankCapacity && manager.gameRunning) {
			waterTankLevel += rate * Time.deltaTime;
		}
	}

}
