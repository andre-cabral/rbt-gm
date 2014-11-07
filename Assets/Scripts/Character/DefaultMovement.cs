﻿using UnityEngine;
using System.Collections;

public class DefaultMovement : MonoBehaviour {
	public float movementSpeed;
	public float turnSpeed = 20f;

	void FixedUpdate(){
		Walking();
		RotateToMouse();
		//okay!!!
		if(Input.GetKey("e")){
			rigidbody.AddForce(0f,150f,0f);
		}

	}

	void Walking(){
		float horizontal = Input.GetAxis(Buttons.horizontal);
		float vertical = Input.GetAxis(Buttons.vertical);
		
		float x = horizontal * movementSpeed;
		float y = 0f;
		float z = vertical * movementSpeed;
		
		Vector3 targetTranslation = new Vector3( x ,y, z);
		
		transform.Translate(targetTranslation, Space.World);
	}

	void RotateToMouse(){
		//plane that intersect with the raycast from the camera to find the point which the player should look at
		Plane intersectPlane = new Plane(Vector3.up, transform.position);
		
		//ray that comes from the camera
		Ray rayFromTheCamera = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		float distanceRayCameraToPlane;
		
		//this if changes the distanceRayCameraToPlane to the correct value. 
		//the out keyword makes a parameter that is not returned to be changed
		if(intersectPlane.Raycast(rayFromTheCamera, out distanceRayCameraToPlane)){
			Quaternion rotationToLookAt = Quaternion.LookRotation(rayFromTheCamera.GetPoint(distanceRayCameraToPlane) - transform.position);
			
			transform.rotation = Quaternion.Slerp(transform.rotation, rotationToLookAt, turnSpeed * Time.deltaTime);

			//transform.localPosition = new Vector3(0f,transform.localPosition.y,0f);
		}
	}

}