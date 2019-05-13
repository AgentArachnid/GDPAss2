using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	public GameObject use; // reference to the object that an instance is linked to
	public bool dropObject; // Decides if the button just activates gravity on the object
	public bool ActivatePendulum; // Specific to the pendulum, decides if the button activates the pendulum
	public bool activateObject; // Decides if the button activates a certain function on the object, which is defined by their components
	// For example, decides if the button activates the portal and allows objects to pass between them
	public float mass; // Specific to the Rigidbody, I can change the mass of the object, giving it more force at different times.
	public bool destroyObject;


	//References to the potential components the linked object could have
	private Rigidbody RG;
	private TriggerPendulum trggrP;
	private Portal portal;


	// Use this for initialization
	void Start () {

		// Checks if the object is valid and then if it has the relevant components and sets them respectfully

		if (use != null)
		{
			

			if (use.GetComponent<Rigidbody> () != null) 
			{
				RG = use.GetComponent<Rigidbody> ();
			}

			if (use.GetComponent<TriggerPendulum> () != null) 
			{
				trggrP = use.GetComponent<TriggerPendulum> ();
			}

			if (use.GetComponent<Portal> () != null) 
			{
				portal = use.GetComponent<Portal> ();
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		// Checks if the colliding object can trigger the button

		if (other.CompareTag("CanTrigger"))
		{
		// Checks the main 2 bools to see if we are dropping, activating or both, and calls the co-responding function
			if (dropObject) {
				Drop ();
			}

			if (activateObject) {
				Activate ();
			}

			if (destroyObject) {
				Destroy (use.gameObject);
			}
		}
	}


	public void Activate()
	{
		// Checks if the object has the components and does 
		if (use.GetComponent<TriggerPendulum> () != null) 
		{
			trggrP.canBeUsed = true;
			if (ActivatePendulum | trggrP.triggered) 
			{
				trggrP.Use (use);
			}
		}
		if (use.GetComponent<Portal> () != null) 
		{
			portal.CanTP = true;
		}

	}

	public void RigidBodyActivate(Rigidbody RG)
	{
		
	}

	public void Drop()
	{
		RG.useGravity = true;
	}

}
