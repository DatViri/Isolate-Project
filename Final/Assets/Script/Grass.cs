using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grass : MonoBehaviour {

	public GameObject GrassTrigger;
	public Button buttonChallenge;
	public Button buttonRun;
	public Inventory inventory;
	public PlayerVitals player;
	public GameObject PlayerVitals;
	public Text animalName;

    /// <summary>
    /// Initialization
    /// </summary>
	void Start () {
		GrassTrigger = GetComponent<Grass> ().GrassTrigger;
		inventory = GetComponent<Grass> ().inventory;
		player = GetComponent<Grass> ().player;
		buttonChallenge = GetComponent<Grass>().buttonChallenge;
		buttonRun = GetComponent<Grass>().buttonRun;
		animalName = GetComponent<Grass> ().animalName;
		GrassTrigger.SetActive (false);
	}
		
    /// <summary>
    /// Meet the enemy
    /// </summary>
    /// <param name="col"></param>
	void OnTriggerEnter2D(Collider2D col){
		if (col.GetComponent<Movement> ()) {
			float p = Random.Range (0, 30);
			if (p < 6) {
				Time.timeScale = 0;
				PlayerVitals.GetComponent<Movement> ().isAllowedToMove = false;
				GrassTrigger.SetActive (true);
				SetText (animalName, p);
                buttonChallenge.onClick.AddListener (() => Challenges ());
                buttonRun.onClick.AddListener (() => Noob());
                }
            }
		}
    /// <summary>
    /// Challenging the animal
    /// </summary>
	public void Challenges() {
		float random = Random.Range (0, 10); 
		if (random < 4) {
			int randomItem = Random.Range (0, 5);
			inventory.AddItem (0, randomItem);
            randomItem = 0;
            Debug.Log  (randomItem);
		} 
		else {
			int healthLoss = Random.Range (0, 5);
            player.DecreaseHealth(healthLoss);
            healthLoss = 0;
            Debug.Log(healthLoss);
        }
        buttonChallenge.onClick.RemoveAllListeners();
        GrassTrigger.SetActive (false);
		Time.timeScale = 1;
		PlayerVitals.GetComponent<Movement> ().isAllowedToMove = true;
	}
    /// <summary>
    /// Or runaway from the animal
    /// </summary>
	public void Noob() {
		int hungerLoss = Random.Range (0, 20);
		int thirstLoss = Random.Range (0, 20);
		player.DropHunger(hungerLoss);
        player.DropThirst(thirstLoss);
        hungerLoss = 0;
        thirstLoss = 0;
		GrassTrigger.SetActive (false);
		Time.timeScale = 1;
		PlayerVitals.GetComponent<Movement> ().isAllowedToMove = true;
        buttonRun.onClick.RemoveAllListeners();
	}
    /// <summary>
    /// Set the animal name to the screen
    /// </summary>
    /// <param name="text"></param>
    /// <param name="a"></param>
	public void SetText(Text text, float a) {
		if (a == 0) {
			text.text = "Tiger";
		} else if (a == 1) {
			text.text = "Bull";
		} else if (a == 2) {
			text.text = "Monkey";
		} else if (a == 3) {
			text.text = "Bunny";
		} else if (a == 4) {
			text.text = "Buffalo";
		} else if(a == 5) {
			text.text = "Pig";
		}
	}
}
