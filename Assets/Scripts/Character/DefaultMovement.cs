using UnityEngine;
using System.Collections;

public class DefaultMovement : MonoBehaviour {
	public float movementSpeed;
	public float turnSpeed = 20f;
	public GameObject legs;
	
	private Animator animator;
	private HashAnimatorDefaultMovement hash;
	private bool stoppedOnAnimation = false;
	
	void Awake(){
		animator = GetComponent<Animator>();
		hash = GetComponent<HashAnimatorDefaultMovement>();
	}
	
	void FixedUpdate(){
		if(!stoppedOnAnimation){
			Walking();
			RotateToMouse();
		}
		
		if(Input.GetKey("e")){
			rigidbody.AddForce(0f,150f,0f);
		}
		
	}
	
	void Walking(){
		float horizontal = Input.GetAxis(Buttons.horizontal);
		float vertical = Input.GetAxis(Buttons.vertical);
		bool isIdle = true;
		if(horizontal != 0 || vertical != 0){
			isIdle = false;
		}

		float x = horizontal * movementSpeed;
		float y = 0f;
		float z = vertical * movementSpeed;
		
		float movementAngle = 0f;
		
		Vector3 targetTranslation = new Vector3( x ,y, z);
		
		transform.Translate(targetTranslation, Space.World);
		
		
		movementAngle = Vector3.Angle(targetTranslation, transform.forward);
		
		//the cross returns the perpendicular angle(90 degrees) to the right(positive)
		Vector3 referenceRight = Vector3.Cross(Vector3.up, transform.forward);
		
		//the dot return positive if its on the right and negative on the left
		float sign = Mathf.Sign(Vector3.Dot(targetTranslation, referenceRight));
	
		walkingAnimation(movementAngle, sign, isIdle);
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
	
	public HashAnimatorDefaultMovement getHash(){
		return hash;
	}

	private void walkingAnimation(float movementAngle, float sign, bool isIdle){
		float valueX = 0;
		float valueZ = 0;
		
		if(!isIdle){
			if(movementAngle == 0){
				valueX = 0;
				valueZ = 1;
				Debug.Log("0");
			}
			
			if(sign>0 && movementAngle > 0 && movementAngle < 90){
				valueX = Mathf.Sin(movementAngle);
				valueZ = Mathf.Cos(movementAngle);
				Debug.Log("1-89");
			}
			
			if(sign>0 && movementAngle == 90){
				valueX = 1;
				valueZ = 0;
				Debug.Log("90");
			}
			
			if(sign>0 && movementAngle > 90 && movementAngle < 180){
				valueX = Mathf.Cos(movementAngle-90);
				valueZ = - Mathf.Sin(movementAngle-90);
				Debug.Log("91-179");
			}
			
			if(movementAngle == 180 || movementAngle == -180){
				valueX = 0;
				valueZ = -1;
				Debug.Log("180/-180");
			}
			
			if(sign<0 && movementAngle > 90){
				valueX = - Mathf.Cos(movementAngle-90);
				valueZ = - Mathf.Sin(movementAngle-90);
				Debug.Log("-91 -179");
			}
			
			if(sign<0 && movementAngle == 90){
				valueX = -1;
				valueZ = 0;
				Debug.Log("-90");
			}
			
			if(sign<0 && movementAngle < 90){
				valueX = - Mathf.Sin(movementAngle);
				valueZ = Mathf.Cos(movementAngle);
				Debug.Log("-1 -89");
			}
		}else{
			valueX = 0;
			valueZ = 0;
		}
		
		
		Debug.Log("x="+valueX+"\nz="+valueZ);
		//Debug.Log("sign="+sign);
		//Debug.Log("movementAngle="+movementAngle);
		
		animator.SetFloat(hash.valueX, valueX);
		animator.SetFloat(hash.valueZ, valueZ);
	}
	
	public bool getStoppedOnAnimation(){
		return stoppedOnAnimation;
	}
	
	public void setStoppedOnAnimation(bool stopped){
		stoppedOnAnimation = stopped;
	}
	
}
