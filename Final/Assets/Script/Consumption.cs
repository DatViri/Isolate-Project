using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumption : MonoBehaviour {
	public Inventory inventory;
	public PlayerVitals player;
    /// <summary>
    /// Food consumption
    /// </summary>
	void ConsumeFood() {
		if (inventory.GetAmountOfItem (0) >= 1) {
			player.IncreaseHunger (5);
			inventory.DecreaseItem (0);
		}
	}
    /// <summary>
    /// Water consumption
    /// </summary>
	void ConsumeWater() {
		if (inventory.GetAmountOfItem (1) >= 1) {
			player.IncreaseThirst (10);
			inventory.DecreaseItem (1);
		}
	}
    /// <summary>
    /// Put keys to consume resources
    /// </summary>
	void Update() {
		if(Input.GetKeyDown("c")) {
			ConsumeFood ();
		}
		if(Input.GetKeyDown("x")) {
			ConsumeWater ();
		}	
	}
}
