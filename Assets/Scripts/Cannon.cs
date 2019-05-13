using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

	public float forceAmount;
	public Vector3 forceOrigin;
	public float forceRadius;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		
		if (other.CompareTag ("CanTrigger")) {
			if (other.GetComponent<Rigidbody> () != null) {
				other.GetComponent<Rigidbody> ().AddExplosionForce (forceAmount, forceOrigin, forceRadius);

			
			}
	
		}
	}
}
