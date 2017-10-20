using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DayNight : MonoBehaviour 
{

	public float time;
	private bool done = false;
	public GameObject winning;
	public Text win;
	public GameObject PlayerVitals;
	public TimeSpan currenttime;
	public Text timetext;
	public int days = 1;
	public int speed = 1;
    /// <summary>
    /// Initialization
    /// </summary>
	void Start() {
		winning = GetComponent<DayNight>().winning;
		win = GetComponent<DayNight>().win;
		winning.SetActive (false);
	}
	/// <summary>
    /// Change the status betweeen day and night
    /// </summary>
	void Update () 
	{
		Changetime ();
		if (time >= 50 && done == false) {
			done = true;
			ScreenFader sf = GameObject.FindGameObjectWithTag ("Fader").GetComponent<ScreenFader>();
			Debug.Log ("triggered");
			StartCoroutine (sf.DayEnd());
			Debug.Log ("triggered");
		}

		if (time < 5 && done == true){
			done = false;
		}
		Win ();
	}
	// Mechanice of time changing	
	public void Changetime(){
		time += Time.deltaTime * speed;
		if (time > 50.2) 
		{
			days += 1;
			time = 0;
		}
		currenttime = TimeSpan.FromSeconds (time);
		string[] temptime = currenttime.ToString ().Split (":" [0]);
		timetext.text =" Day: " + days;

	}
    /// <summary>
    /// Winning condition
    /// </summary>
	public void Win() {
		if (days == 10) {
			winning.SetActive (true);
			Time.timeScale = 0;
			PlayerVitals.GetComponent<Movement> ().isAllowedToMove = false;
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Debug.Log ("You win");
                Application.LoadLevel("Menu");
            }
		}
	}
}
