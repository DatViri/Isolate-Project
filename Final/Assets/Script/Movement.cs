using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

	public float moveSpeed;
	private Animator anim;
	public bool PlayerMoving;
	private Vector2 lastMove;
	private Rigidbody2D myRigibody;
	public bool isAllowedToMove = true;


	void Start () {
		anim = GetComponent<Animator> ();	
		myRigibody = GetComponent<Rigidbody2D> ();
		isAllowedToMove = true;
	}
	
    /// <summary>
    /// Getting the position of player and input the x - y axis to move
    /// Adding the animation of player.
    /// Each last move remember the previous input button and keep that animation
    /// </summary>
	void Update () {
        
        PlayerMoving = false;
        
		if((Input.GetAxisRaw("Horizontal")>0.5f ||Input.GetAxisRaw("Horizontal")<-0.5f) && isAllowedToMove == true){
			transform.Translate (new Vector3 (Input.GetAxisRaw ("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
			PlayerMoving = true;
			lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);
		}

		if((Input.GetAxisRaw("Vertical")>0.5f ||Input.GetAxisRaw("Vertical")<-0.5f) && isAllowedToMove == true){
			transform.Translate (new Vector3 (0f, Input.GetAxisRaw ("Vertical") * moveSpeed * Time.deltaTime, 0f));
			PlayerMoving = true;
			lastMove = new Vector2 (0f, Input.GetAxisRaw ("Vertical"));
		}

		if(Input.GetAxisRaw("Horizontal")<0.5f && Input.GetAxisRaw("Horizontal")>-0.5f){
			myRigibody.velocity = new Vector2 (0f, myRigibody.velocity.y);
		}

		if(Input.GetAxisRaw("Vertical")<0.5f && Input.GetAxisRaw("Vertical")>-0.5f){
			myRigibody.velocity = new Vector2 (myRigibody.velocity.x, 0f);
		}
		anim.SetFloat ("MoveX", Input.GetAxisRaw ("Horizontal"));
		anim.SetFloat ("MoveY", Input.GetAxisRaw ("Vertical"));
		anim.SetBool ("PlayerMoving", PlayerMoving);
		anim.SetFloat ("LastMoveX", lastMove.x);
		anim.SetFloat ("LastMoveY", lastMove.y);
}
}
