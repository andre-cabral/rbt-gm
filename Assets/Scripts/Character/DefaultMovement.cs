using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HashAnimatorDefaultMovement))]

public class DefaultMovement : MonoBehaviour {
	public float movementSpeed;
	public float turnSpeed = 20f;

	private float inputTotal = 0f;
	private Animator animator;
	private HashAnimatorDefaultMovement hash;

	private GameObject classContainer;
	private ChangeClass changeClassScript;
	private bool changingClass = false;
	private int goToClass;

	private bool stoppedOnAnimation = false;

	void Awake(){
		animator = GetComponent<Animator>();
		hash = GetComponent<HashAnimatorDefaultMovement>();

		classContainer = GameObject.FindGameObjectWithTag(Tags.characterClassesContainer);
		changeClassScript = classContainer.GetComponent<ChangeClass>();
	}

	void Update () {
		Debug.Log(!animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationsNames.changeClassStealth));
		if(changingClass && !animator.IsInTransition(0) 
		   && !animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationsNames.changeClassStealth)
		   && !animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationsNames.changeClassPower)){
			finishClassChange();
		}

		if(!changeClassScript.GetChangingClass() && !stoppedOnAnimation && !changingClass){

			if( Input.GetButtonDown(Buttons.class0) && !changeClassScript.class0.Equals(changeClassScript.GetActiveClass()) ){
				startClassChange(0);
			}
			
			if( Input.GetButtonDown(Buttons.class1) && !changeClassScript.class1.Equals(changeClassScript.GetActiveClass()) ){
				startClassChange(1);
			}
			
			if( Input.GetButtonDown(Buttons.class2) && !changeClassScript.class0.Equals(changeClassScript.GetActiveClass()) ){
				startClassChange(2);
			}

		}

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


	//########MOVEMENT START
	//###########################################
	void Walking(){
		float horizontal = Input.GetAxis(Buttons.horizontal);
		float vertical = Input.GetAxis(Buttons.vertical);
		bool isIdle = true;

		if(horizontal != 0 || vertical != 0){
			isIdle = false;
		}

		if (horizontal == 0 || vertical == 0){
			inputTotal = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
		}else{
			inputTotal = (Mathf.Abs(horizontal) + Mathf.Abs(vertical))/2;
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

	private void walkingAnimation(float movementAngle, float sign, bool isIdle){
		float valueX = 0;
		float valueZ = 0;
		
		if(!isIdle){
			//0
			if(movementAngle == 0){
				valueX = 0;
				valueZ = 1;
			}
			//1_45
			if(sign>0 && movementAngle > 0 && movementAngle <= 45){
				valueX = Mathf.Tan(toRadians(movementAngle));
				valueZ = 1;
			}
			//46_89
			if(sign>0 && movementAngle > 45 && movementAngle < 90){
				valueX = 1;
				valueZ = Mathf.Tan(toRadians(90-movementAngle));
			}
			//90
			if(sign>0 && movementAngle == 90){
				valueX = 1;
				valueZ = 0;
			}
			//91_135
			if(sign>0 && movementAngle > 90 && movementAngle <= 135){
				valueX = 1;
				valueZ = - Mathf.Tan(toRadians(movementAngle-90));
			}
			//136_179
			if(sign>0 && movementAngle > 135 && movementAngle < 180){
				valueX = Mathf.Tan(toRadians(90-(movementAngle-90)));
				valueZ = -1;
			}
			//180
			if(movementAngle == 180){
				valueX = 0;
				valueZ = -1;
			}
			

			//-1_-45
			if(sign<0 && movementAngle > 0 && movementAngle <= 45){
				valueX = - Mathf.Tan(toRadians(movementAngle));
				valueZ = 1;
			}
			//-46_-89
			if(sign<0 && movementAngle > 45 && movementAngle < 90){
				valueX = -1;
				valueZ = Mathf.Tan(toRadians(90-movementAngle));
			}
			//-90
			if(sign<0 && movementAngle == 90){
				valueX = -1;
				valueZ = 0;
			}
			//-91_-135
			if(sign<0 && movementAngle > 90 && movementAngle <= 135){
				valueX = -1;
				valueZ = - Mathf.Tan(toRadians(movementAngle-90));
			}
			//-136_-179
			if(sign<0 && movementAngle > 135 && movementAngle < 180){
				valueX = - Mathf.Tan(toRadians(90-(movementAngle-90)));
				valueZ = -1;
			}
			//-180
			if(movementAngle == -180){
				valueX = 0;
				valueZ = -1;
			}
		}else{
			valueX = 0;
			valueZ = 0;
		}

		valueX *= inputTotal;
		valueZ *= inputTotal;
		
		//valueX = Mathf.Round(valueX * 100f)/100;
		//valueZ = Mathf.Round(valueZ * 100f)/100;

		animator.SetFloat(hash.valueX, valueX);
		animator.SetFloat(hash.valueZ, valueZ);
	}
	//########MOVEMENT END
	//###########################################


	//########CHANGE CLASS START
	//###########################################
	void startClassChange(int goToClass){
		stoppedOnAnimation = true;
		changingClass = true;
		animator.SetBool(hash.changeClass, true);
		this.goToClass = goToClass;

		if(goToClass == 0){
			animator.SetInteger(hash.classToGo, ClassAnimationNumber(changeClassScript.class0));
		}
		if(goToClass == 1){
			animator.SetInteger(hash.classToGo, ClassAnimationNumber(changeClassScript.class1));
		}
		if(goToClass == 2){
			animator.SetInteger(hash.classToGo, ClassAnimationNumber(changeClassScript.class2));
		}
	}

	void finishClassChange(){
		stoppedOnAnimation = false;
		changingClass = false;
		animator.SetBool(hash.changeClass, false);

		if(goToClass == 0){
			changeClassScript.ActivateClass(changeClassScript.class0);
			animator.SetInteger(hash.classToGo, 99);
		}
		if(goToClass == 1){
			changeClassScript.ActivateClass(changeClassScript.class1);
			animator.SetInteger(hash.classToGo, 99);		
		}
		if(goToClass == 2){
			changeClassScript.ActivateClass(changeClassScript.class2);
			animator.SetInteger(hash.classToGo, 99);
		}
	}

	int ClassAnimationNumber(GameObject classToGo){
		if(classToGo.name == ClassesObjectsNames.stealth){
			return 0;
		}
		if(classToGo.name == ClassesObjectsNames.power){
			return 1;
		}
		/*
		if(classToGo.name == ClassesObjectsNames.ranged){
			return 2;
		}
		*/
		return 99;
	}
	//########CHANGE CLASS END
	//###########################################


	//########UTILITIES START
	//###########################################
	public float toRadians(float degrees){
		return (degrees * Mathf.PI)/180;
	}
	
	public HashAnimatorDefaultMovement getHash(){
		return hash;
	}
	
	public bool getStoppedOnAnimation(){
		return stoppedOnAnimation;
	}
	
	public void setStoppedOnAnimation(bool stopped){
		stoppedOnAnimation = stopped;
	}
	//########UTILITIES END
	//###########################################
}
