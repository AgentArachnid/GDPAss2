﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBall : MonoBehaviour {

	public float Speed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float xSpeed = Input.GetAxis ("Horizontal");
		float ySpeed = Input.GetAxis ("Vertical");

		Rigidbody body = GetComponent<Rigidbody> ();

		body.AddTorque (new Vector3 (xSpeed, 0, ySpeed));
	}
}