using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


	public GameObject fire;
	public int currentFires = 0;
	int fireFought = 0;
	float maxFireCount = 100f;
	public GameObject[] spawnPoints;
	public Text firecount;
	public Text containmentText;
	public Text endText;
	public float spawnRate = 3f;
	float spawnTimer = 0;
	public bool gameRunning = false;
	public GameObject MenuPanel;
	public GameObject StartMenu;
	public GameObject EndMenu;
	public GameObject PauseMenu;
	public float waterUsed;


	// Use this for initialization
	void Start () {
		MenuPanel.SetActive (true);
		StartMenu.SetActive (true);
		EndMenu.SetActive (false);
		PauseMenu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (currentFires >= maxFireCount) {
			EndGame ();
		}

		if (gameRunning) {
			spawnTimer += Time.deltaTime;
			if (spawnTimer >= spawnRate) {
				spawnTimer -= spawnRate;
				RandomSpawn ();
			}
		}
		float containment = ((maxFireCount - currentFires) / maxFireCount) * 100f;
		if (containment < 0) {
			containment = 0;
		}
		containmentText.text = "Fire Contained: " + containment + "%";
	}

	public void SpawnFire(Vector3 pos){
		Instantiate (fire, pos, this.transform.rotation);
		currentFires++;
	}

	void RandomSpawn(){
		int rand = (int)Random.Range (0, spawnPoints.Length - 1);
		SpawnFire (spawnPoints [rand].transform.position);
	}

	void RandomSpawn(int count){
		for (int i = 0; i < count; i++) {
			RandomSpawn ();
		}
	}

	public void FireFought(){
		fireFought++;
		currentFires--;
		firecount.text = "Fires Fought: " + fireFought;

	}

	public void StartGame(){
		RandomSpawn (3);
		gameRunning = true;
	}
	public void PauseGame(){
		gameRunning = false;
		MenuPanel.SetActive (true);
		StartMenu.SetActive (false);
		EndMenu.SetActive (false);
		PauseMenu.SetActive (true);
	}
	public void UnPauseGame(){
		gameRunning = true;
		MenuPanel.SetActive (false);
		StartMenu.SetActive (false);
		EndMenu.SetActive (false);
		PauseMenu.SetActive (false);
	}
	public void EndGame(){
		gameRunning = false;
		MenuPanel.SetActive (true);
		StartMenu.SetActive (false);
		PauseMenu.SetActive (false);
		EndMenu.SetActive (true);
		endText.text = "The fires overtook you\nYou fought " + fireFought + " fires\nYou used " + (int)waterUsed + " liters of water";
	}

	public void RestartScene(){
		SceneManager.LoadScene (0);
	}

	public void Quit(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif

	}
}
