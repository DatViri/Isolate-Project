using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerVitals : MonoBehaviour 
{
	public float health;
	public int maxHealth = 100;
	public float healthFallRate;

	public float hunger;
	public int maxHunger = 100;
	public float hungerFallRate;

	public float thirst;
	public int maxThirst = 100;
	public float thirstFallRate;

	public Text showHealth;
	public Text showHunger;
	public Text showThirst;

	public GameObject gameOver;
	public Text over;
	public GameObject player;

	void Start()
	{
		gameOver = GetComponent<PlayerVitals>().gameOver;
		over = GetComponent<PlayerVitals> ().over;
		gameOver.SetActive (false);
		health = maxHealth;
		hunger = maxHunger;
		thirst = maxThirst;

	}
    /// <summary>
    /// Adding the Vital Mechanic
    /// Hunger,Thirst reduce due to time and when hunger or thirst = 0, Health starts to reduce
    /// Making Health, Hunger, Thirst formate number to 2 digits
    /// </summary>
	void Update()
	{

		//HEALTH CONTROLLER
		if (hunger <= 0 && (thirst <= 0)) 
		{
			health -= Time.deltaTime * healthFallRate * 2;
		} 

		else if (hunger <= 0 || thirst <= 0) 
		{
			health -= Time.deltaTime * healthFallRate;
		}

		if (health <= 0) 
		{
			health = 0;
		}

		//HUNGER CONTROLLER
		if (hunger >= 0) 
		{
			hunger -=  Time.deltaTime * hungerFallRate;
		} 

		else if (hunger <= 0) 
		{
			hunger = 0;
		}

		else if (hunger >= maxHunger) 
		{
			hunger = maxHunger;
		}

		//THIRSTBAR CONTROLLER
		if (thirst >= 0) 
		{
			thirst -= Time.deltaTime * thirstFallRate;
		} 

		else if (thirst <= 0) 
		{
			thirst = 0;
		}

		else if (thirst >= maxThirst) 
		{
			thirst = maxThirst;
		}

		showHealth.text = "Health: " + String.Format("{0:0}", health);
		showHunger.text = "Hunger: " + String.Format("{0:0}", hunger);
		showThirst.text = "Thirst: " + String.Format("{0:0}", thirst);
		GameOver ();
	}

    /// <summary>
    /// When health = 0, game put up the GameOver panel that show that player is lose
    /// Player cant move and time freeze
    /// When player push escape, game load the Menu screen
    /// </summary>
	void GameOver()
	{
		if (health == 0) {
			gameOver.SetActive (true);
			Time.timeScale = 0;
			player.GetComponent<Movement> ().isAllowedToMove = false;
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Debug.Log ("You lose");
                Application.LoadLevel("Menu");
			}
		}
	}
	public bool DropHunger(int amount) {
		if (this.hunger > amount) {
			this.hunger -= amount;
			return true;
		}
		return false;
	}
	public bool DropThirst(int amount) {
		if (this.thirst > amount) {
			this.thirst -= amount;
			return true;
		}
		return false;
	}
	public void IncreaseHunger(int amount) {
		if (this.hunger + amount >= 100) {
			this.hunger = 100;
		} else if (this.hunger + amount < 100) {
			this.hunger += amount;
		}
	}
	public void IncreaseThirst(int amount) {
		if (this.thirst + amount >= 100) {
			this.thirst = 100;
		} else if (this.thirst + amount < 100) {
			this.thirst += amount;
		}
	}
    public void DecreaseHealth(int amount) {
        this.health -= amount;
    }
}
