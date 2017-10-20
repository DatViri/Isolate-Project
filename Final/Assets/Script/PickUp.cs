using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PickUp : MonoBehaviour
{	
	public Inventory inventory;
	public PlayerVitals player;
    /// <summary>
    /// Picking up resources
    /// </summary>
    /// <param name="other"></param>
	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.CompareTag ("bamboo") && Input.GetKeyDown ("b")) {
			other.GetComponent<PickUp>().inventory.AddItem (1,12);
			Destroy (other.gameObject);
		}
		else if (other.gameObject.CompareTag ("berry") && Input.GetKeyDown ("b")) {
			other.GetComponent<PickUp>().inventory.AddItem (0,7);
			Destroy (other.gameObject);
		}
		else if (other.gameObject.CompareTag ("carrot") && Input.GetKeyDown ("b")) {
			other.GetComponent<PickUp>().inventory.AddItem (0,5);
			Destroy (other.gameObject);
		}
		else if (other.gameObject.CompareTag ("coconut") && Input.GetKeyDown ("b")) {
			other.GetComponent<PickUp>().inventory.AddItem (1,5);
			Destroy (other.gameObject);
		}
		else if (other.gameObject.CompareTag ("fish") && Input.GetKeyDown ("b")) {
			other.GetComponent<PickUp>().inventory.AddItem (0,8);
			Destroy (other.gameObject);
		}
		else if (other.gameObject.CompareTag ("fruit") && Input.GetKeyDown ("b")) {
			other.GetComponent<PickUp>().inventory.AddItem (0,10);
			Destroy (other.gameObject);
		}
		else if (other.gameObject.CompareTag ("rock") && Input.GetKeyDown ("b")) {
			if (other.GetComponent<PickUp> ().player.DropHunger (15) && other.GetComponent<PickUp> ().player.DropThirst (15)) {
				other.GetComponent<PickUp> ().inventory.AddItem (3, 10);
				Destroy (other.gameObject);
			}
		}
		else if (other.gameObject.CompareTag ("tree") && Input.GetKeyDown ("b")) {
			if(other.GetComponent<PickUp> ().player.DropHunger(10) && other.GetComponent<PickUp> ().player.DropThirst(10)) {
				other.GetComponent<PickUp>().inventory.AddItem (4,10);
				Destroy (other.gameObject);
			}
		}
		else if (other.gameObject.CompareTag ("water_pond") && Input.GetKeyDown ("b")) {
			other.GetComponent<PickUp>().inventory.AddItem (1,7);
			Destroy (other.gameObject);
		}
		else if (other.gameObject.CompareTag ("metal") && Input.GetKeyDown ("b")) {
			if (other.GetComponent<PickUp> ().player.DropHunger (15) && other.GetComponent<PickUp> ().player.DropThirst (15)) {
				other.GetComponent<PickUp> ().inventory.AddItem (2, 10);
				Destroy (other.gameObject);
			}
		}
	}
}


