using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPendulum : MonoBehaviour {

	public GameObject ball; // Linked ball of the pendulum
	public bool canBeUsed; // Decides if the trigger itself can be used
	public bool triggered; // Checks if the trigger has been used to trigger the pendulum

	private Rigidbody RG; //The Rigidbody that will allow me to switch the gravity



	void OnTriggerEnter (Collider other)
	{

		if (other.CompareTag ("CanTrigger")) // Checks if the object colliding
			//with the trigger has the right tag to trigger the pendulum
		{
			triggered = true; // Makes sure the trigger can't be used twice
			if (canBeUsed) // Makes sure the trigger can be used
			{
				Use (ball);
			}
		}
	}

	public void Use(GameObject ball)
	{	
		RG = ball.GetComponent<Rigidbody> (); //Gets the rigidbody of the ball
		if (RG != null) { // Checks that the ball has a rigidbody component to
			// prevent errors
			RG.useGravity = true; // Starts a chain reaction that will react in the ball swinging
		}

	}

	void Update(){
		if(triggered){
			
			Use(ball);
		}
	}
}
