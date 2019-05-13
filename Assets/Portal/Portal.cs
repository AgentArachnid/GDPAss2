using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	//Idea is to make the portals dynamics, able to be used in 3 dimensions and at different rotations, so that the RG Machine can
	// use them to translate objects around the scene, while keeping their physical features

	// I made a portal script as it is a concept that was relatively easy to implement and useful in the context of transporting objects,
	// which is a major part of a Rube Goldberg Machine.


	// Public variables
	public Portal portal; // Linked Portal
	public bool CanTP = true; // Will Determine if the portal can be used
	public bool singleUSe = true;
	public bool inverseExitDirection; // Decides which side of the portal an object will be ejected from..
	public Vector3 rayDirection; // Origin of the raycast
	public float ToggleCanTPDelay = 0.2f; // The value for the delay in resetting the CanTP variable

	// Private variables
	private Vector3 portalnormal; // value of the normal of the portal, gained by the raycast
	private Ray Exit; // The 2 vector3s that hold the data for the dynamic raycast
	private Rigidbody RG; // Reference to the RigidBody of the Object passing through Portal

	//Protected variables
	protected Vector3 EntryVelocity; // Value of the Objects velocity as it collides with first portal
	protected Vector3 ExitVelocity; // Value of the Objects velocity as it is ejected from the linked portal

	void Start(){
		CalculateNormal ();
	}

	public void CalculateNormal() {

		// Checks the face of the box collider that the object will be ejected from
		RaycastHit hit;
		Exit = new Ray(portal.rayDirection, (portal.gameObject.transform.position - portal.rayDirection));
	
		// Controls the Ray cast origin and the direction vector
		//The direction vector is derived dynamically by the use of basic vector maths 
		//AB = b-a, b meaning the target position vector or the portal object 
		//and a meaning the position of which the ray will be casted
		// Together I can calculate the vector needed tp get from the the origin of the ray and the portal object
		// I did it like this as I can change the position of the origin from the inspector to get a different result
		// Thanks to the exitDirection variable.


		if (Physics.Raycast (Exit, out hit)) 
		{
			// This checks if the raycast has hit a portal by checking it's tag
			// I did this so that the normal variable is accurate and is from the right object.
			if (hit.collider.tag == "Portal")
			{
				// I have set CanTeleport here as if it wasn't a portal object the ray hit, then I don't know the portal normal
				// therefore, al further calculations will be useless and in vain
				CanTP = true;
				// The portal normal is important later for making the object eject out of the linked portal at the right veloctity,
				// but at the right direction.
				// Without it, the object would just fall straight down, which isn't what I want.
				portalnormal = hit.normal;
			}
		}
	}



	void OnTriggerEnter (Collider other)
	{	

		if (CanTP) {
			// This checks that the portal can be used as it can be only be used at certain times
			// 
			//Gets the RigidBody of the object colliding with the box, which I need to access the velocity
			RG = other.gameObject.GetComponent<Rigidbody> ();
			// Gets the Velocity of the object when it collides with the portal
			EntryVelocity = (RG.velocity);
			portal.CanTP = false; 
			// Without this, there would be an infinite loop with a flickering effect of the object swapping 
			//between portals every tick


			other.gameObject.transform.SetPositionAndRotation(portal.gameObject.transform.position, portal.gameObject.transform.rotation);
			// This sets the position and rotation for the colliding object to the position and rotation of the linked portal

			PhysicsTeleport (RG);

			if(!singleUSe)
			{
				ResetTP ();
			}

			if (singleUSe) {
				Destroy (gameObject);
				Destroy (portal.gameObject);
			}
			}
		}

	void PhysicsTeleport(Rigidbody other)
	{
		if (inverseExitDirection == true) {
			ExitVelocity = -portalnormal * EntryVelocity.magnitude;
		}
		if(inverseExitDirection == false){
			ExitVelocity = portalnormal * EntryVelocity.magnitude;
		}
		//Gets the correct exit velocity based off of the portal normal,
		//the length of the entry velocity and the exit direction
		other.velocity = ExitVelocity;

		// Sets the objects Rigidbody to the exit velocity that was just calculated
	
	}

	IEnumerator ResetTP(){
		yield return new WaitForSeconds (ToggleCanTPDelay);
		CanTP = true;
	}

}
