using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour {

	Animator anim;
	bool isFading = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
    /// <summary>
    /// When day end, screen trigger, time freeze and has fading effect
    /// </summary>
    /// <returns></returns>
	public IEnumerator DayEnd(){
		Time.timeScale = 0;
		isFading = true;
		anim.SetTrigger ("DayEnd");

		while (isFading)
			yield return null;
	}

	void AnimationComplete(){
		Time.timeScale = 1;
		isFading = false;
	}
}
