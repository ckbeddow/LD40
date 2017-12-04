using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour {

	public float fillRate = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider other){
		if (other.tag == "Player") {
			other.GetComponentInChildren<WaterJet> ().RefillTank (fillRate);
		}
	}
}
