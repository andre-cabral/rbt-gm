using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(HashAnimatorDefaultMovement))]
[RequireComponent(typeof(FlickerWhenDamaged))]

public class DefaultMovement : MonoBehaviour {
	public float movementSpeed = 0.1f;
	private float startSpeed;
	public float turnSpeed = 20f;
	public float lookAtHeight = 2f;

	private float inputTotal = 0f;
	private Animator animator;
	private HashAnimatorDefaultMovement hash;

	private GameObject classContainer;
	private ChangeClass changeClassScript;
	private bool changingClass = false;
	private int goToClass;
	ClassMarkerUI classMarkerUIScript;

	//#####JUMP REMOVED
	//the grounded started as false with the jump
	public bool grounded = true;
	private bool jumpStart = false;
	public float jumpHeight = 300f;
	public GameObject groundedPositionObject;
	public float groundedObjectRadius = 0.05f;
	public LayerMask layerFloor;

	public GameObject hitIconPrefab;

	private bool canWalk = true;
	private bool canJump = true;
	private bool stoppedOnAnimation = false;

	public bool isDead = false;
	private bool deathRagDollActive = false;
	private List<Rigidbody> ragDollRigidbodies = new List<Rigidbody>();
	private Collider playerCollider;
	private Rigidbody playerRigidbody;
	public static GameObject[] gameOverObjects;

	public static bool isPaused = false;
	public static GameObject[] pauseObjects;

	private GameObject gameController;
	private LifeManager lifeManager;
	private FlickerWhenDamaged flickerWhenDamaged;

	void Awake(){
		InitializeStartSpeed();

		animator = GetComponent<Animator>();
		hash = GetComponent<HashAnimatorDefaultMovement>();

		classContainer = GameObject.FindGameObjectWithTag(Tags.characterClassesContainer);
		changeClassScript = classContainer.GetComponent<ChangeClass>();
		classMarkerUIScript = GameObject.FindGameObjectWithTag(Tags.classMarkerUI).GetComponent<ClassMarkerUI>();

		playerCollider = GetComponent<Collider>();
		playerRigidbody = GetComponent<Rigidbody>();

		gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
		lifeManager = gameController.GetComponent<LifeManager>();
		flickerWhenDamaged = GetComponent<FlickerWhenDamaged>();

		List<GameObject> ragDollObjects = GetObjectsInLayer( gameObject, LayerMask.NameToLayer(Layers.collidersRagdoll) );
		ragDollObjects = GetObjectsWithRigidbody(ragDollObjects);
		ragDollRigidbodies = GetRigidbodiesFromObjects(ragDollObjects);

		DefaultMovement.pauseObjects = GameObject.FindGameObjectsWithTag(Tags.pauseObject);
		DefaultMovement.setPause(false);

		DefaultMovement.gameOverObjects = GameObject.FindGameObjectsWithTag(Tags.gameOverObject);
		DefaultMovement.setGameOverObjects(false);
	}


	void Update () {
		if(!isDead){
			if(Input.GetKeyDown(KeyCode.Escape)){
				TogglePause();
			}
		}

		if(!isDead && !DefaultMovement.isPaused){

			//#####JUMP REMOVED
			/*
			if(Input.GetButtonDown(Buttons.jump) && !jumpStart && !stoppedOnAnimation && grounded && canJump){
				jump();
			}
			jumpGroundCheck();
			*/
			if(!grounded){
				grounded = true;
			}

			if(!isDead && !stoppedOnAnimation){
				classChangeCheck();
			}else if(!isDead && changingClass){
				classChangeCheck();
			}
		}
	}
	
	void FixedUpdate(){
		if(isDead && !deathRagDollActive){
			StartDeathRagDoll();
		}

		if(!isDead && !stoppedOnAnimation && canWalk){
			Walking();
		}

		if(!isDead && !stoppedOnAnimation){
			RotateToMouse();
		}
		
		if(Input.GetKey("e")){
			playerRigidbody.AddForce(0f,150f,0f);
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


		float x = horizontal;
		float y = 0f;
		float z = vertical;
		
		float movementAngle = 0f;
		
		Vector3 targetTranslation = new Vector3( x ,y, z).normalized * movementSpeed;
		
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
		Plane intersectPlane = new Plane(Vector3.up, new Vector3(transform.position.x,transform.position.y + lookAtHeight,transform.position.z));
		
		//ray that comes from the camera
		Ray rayFromTheCamera = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		float distanceRayCameraToPlane;
		
		//this if changes the distanceRayCameraToPlane to the correct value. 
		//the out keyword makes a parameter that is not returned to be changed
		if(intersectPlane.Raycast(rayFromTheCamera, out distanceRayCameraToPlane)){
			Vector3 rayFromTheCameraPoint = rayFromTheCamera.GetPoint(distanceRayCameraToPlane);
			Vector3 positionTolook = new Vector3(rayFromTheCameraPoint.x, transform.position.y, rayFromTheCameraPoint.z);

			Quaternion rotationToLookAt = Quaternion.LookRotation(positionTolook - transform.position);

			//transform.rotation = rotationToLookAt;
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


//########Collisions START
//###########################################
	void OnTriggerStay(Collider collider) {
		GameObject collidedObject = collider.gameObject;

		if( !flickerWhenDamaged.getFlicker() && collidedObject.tag == Tags.enemyAttackCollider){
			TakeDamage(collidedObject, collider.transform.position);
		}

	}
//########Collisions END
//###########################################


//########Damage START
//###########################################
	void TakeDamage(GameObject collidedObject, Vector3 colliderPosition){
		EnemyAttackCollider enemyAttackColliderScript = collidedObject.GetComponent<EnemyAttackCollider>();
		if(enemyAttackColliderScript.DamageDealt() > 0){
			Instantiate(hitIconPrefab, colliderPosition, hitIconPrefab.transform.rotation);
			LifeLoss(enemyAttackColliderScript.DamageDealt());
		}
	}
//########Damage END
//###########################################

//########Speed change START
//###########################################
	public void SpeedChange(float newSpeed){
		InitializeStartSpeed();

		movementSpeed = newSpeed;
	}

	public void SpeedAdd(float speedToAdd){
		InitializeStartSpeed();

		movementSpeed += speedToAdd;
	}

	public void SpeedReduce(float speedToReduce){
		InitializeStartSpeed();

		if(movementSpeed > speedToReduce){
			movementSpeed -= speedToReduce;
		}else{
			movementSpeed = 0f;
		}
	}

	public void SpeedReduceOnce(float speedToReduce){
		InitializeStartSpeed();

		if(movementSpeed > startSpeed-speedToReduce){
			if(startSpeed > speedToReduce){
				movementSpeed = startSpeed-speedToReduce;
			}else{
				movementSpeed = 0f;
			}
		}
	}

	void InitializeStartSpeed(){
		if(startSpeed == 0f){
			startSpeed = movementSpeed;
		}
	}

	public void SpeedReset(){
		movementSpeed = startSpeed;
	}
//########Speed change END
//###########################################


//########CHANGE CLASS START
//###########################################
	void classChangeCheck(){
		if(changingClass && !animator.IsInTransition(1) 
		   && !animator.GetCurrentAnimatorStateInfo(1).IsName(AnimationsNames.changeClassStealth)
		   && !animator.GetCurrentAnimatorStateInfo(1).IsName(AnimationsNames.changeClassPower)
		   && !animator.GetCurrentAnimatorStateInfo(1).IsName(AnimationsNames.changeClassRanged)){
			finishClassChange();
		}
		
		if(!changeClassScript.GetChangingClass() && !stoppedOnAnimation && !changingClass && grounded){
			
			if( Input.GetButtonDown(Buttons.class0) && !changeClassScript.class0.Equals(changeClassScript.GetActiveClass()) ){
				startClassChange(0);
			}
			
			if( Input.GetButtonDown(Buttons.class1) && !changeClassScript.class1.Equals(changeClassScript.GetActiveClass()) ){
				startClassChange(1);
			}
			
			if( Input.GetButtonDown(Buttons.class2) && !changeClassScript.class2.Equals(changeClassScript.GetActiveClass()) ){
				startClassChange(2);
			}
			
		}
	}

	void startClassChange(int goToClass){	
		//stoppedOnAnimation = true;
		classMarkerUIScript.changingClassStart(goToClass);
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
		//stoppedOnAnimation = false;
		//classMarkerUIScript.changingClassEnd();
		changingClass = false;
		animator.SetBool(hash.changeClass, false);

		if(goToClass == 0){
			animator.SetInteger(hash.classToGo, 99);
			changeClassScript.ActivateClass(changeClassScript.class0);
		}
		if(goToClass == 1){
			animator.SetInteger(hash.classToGo, 99);
			changeClassScript.ActivateClass(changeClassScript.class1);

		}
		if(goToClass == 2){
			animator.SetInteger(hash.classToGo, 99);
			changeClassScript.ActivateClass(changeClassScript.class2);
		}
	}

	int ClassAnimationNumber(GameObject classToGo){
		if(classToGo.name == ClassesObjectsNames.stealth){
			return 0;
		}
		if(classToGo.name == ClassesObjectsNames.power){
			return 1;
		}

		if(classToGo.name == ClassesObjectsNames.ranged){
			return 2;
		}

		return 99;
	}
//########CHANGE CLASS END
//###########################################

//#####JUMP REMOVED
//########JUMP START
//###########################################
/*
	void jump(){
		jumpStart = true;
		stoppedOnAnimation = true;
		animator.SetBool(hash.jumpStart, jumpStart);
	}

	void JumpStartFinishedAnimationEvent(){
		jumpStart = false;
		animator.SetBool(hash.jumpStart, jumpStart);
		jumpAddForce();
	}

	void jumpAddForce(){
		stoppedOnAnimation = false;
		playerRigidbody.AddForce(Vector3.up * jumpHeight);
	}

	void jumpGroundCheck(){
		grounded = Physics.OverlapSphere(groundedPositionObject.transform.position,groundedObjectRadius, layerFloor).Length > 0;
		//grounded = Physics.OverlapSphere(groundedPositionObject.transform.position,groundedObjectRadius).Length > 0;
		//grounded = Mathf.Round(rigidbody.velocity.y*1000f)/1000f == 0;
		//Debug.Log(Mathf.Round(rigidbody.velocity.y*1000f)/1000f);
		animator.SetBool(hash.grounded, grounded);
		
		animator.SetFloat(hash.verticalSpeed, Mathf.Round(playerRigidbody.velocity.y*100)/100);
	}
*/
//########JUMP END
//###########################################

//########Life Manager START
//###########################################
	public void LifeGain(int gainedLife){
		lifeManager.LifeGain(gainedLife);
	}

	public void LifeLoss (int lifeToLose){
		//only flicker if the damage dont kill the player
		if( lifeToLose < lifeManager.GetLife() ){
			flickerWhenDamaged.startFlickering();
		}

		lifeManager.LifeLoss(lifeToLose);

		if(lifeManager.GetLife() <= 0){
			setIsDead(true);
			DefaultMovement.setGameOverObjects(true);
		}
	}

//########Life Manager END
//###########################################


//########DEATH START
//###########################################
	void StartDeathRagDoll(){
		animator.enabled = false;
		playerCollider.enabled = false;
		playerRigidbody.isKinematic = true;

		foreach(Rigidbody ragDollRigidbody in ragDollRigidbodies){
			ragDollRigidbody.isKinematic = false;
			ragDollRigidbody.useGravity = true;
		}
		
		deathRagDollActive = true;
	}

	public static void setGameOverObjects(bool setObjectTo){
		if(DefaultMovement.gameOverObjects != null){
			foreach(GameObject gameOverObject in DefaultMovement.gameOverObjects){
				if(gameOverObject != null){
					gameOverObject.SetActive(setObjectTo);
				}
			}
		}
	}
//########DEATH END
//###########################################


//########PAUSE START
//###########################################
	public static void setPause(bool pause){
		if(pause){
			isPaused = true;
			Time.timeScale = 0f;
			setPauseObjects(true);
		}else{
			isPaused = false;
			Time.timeScale = 1f;
			setPauseObjects(false);
		}
	}

	public static void setPauseObjects(bool setObjectTo){
		if(DefaultMovement.pauseObjects != null){
			foreach(GameObject pauseObject in DefaultMovement.pauseObjects){
				if(pauseObject != null){
					pauseObject.SetActive(setObjectTo);
				}
			}
		}
	}

	public void TogglePause(){
		DefaultMovement.setPause(!DefaultMovement.isPaused);
	}
//########PAUSE END
//###########################################


//########UTILITIES START
//###########################################
	public float toRadians(float degrees){
		return (degrees * Mathf.PI)/180;
	}
	
	public HashAnimatorDefaultMovement getHash(){
		return hash;
	}

	public void setMovementSpeed(float movementSpeed){
		this.movementSpeed = movementSpeed;
	}

	public float getMovementSpeed(){
		return movementSpeed;
	}

	public bool getGrounded(){
		return grounded;
	}

	public bool getStoppedOnAnimation(){
		return stoppedOnAnimation;
	}
	
	public void setStoppedOnAnimation(bool stoppedOnAnimation){
		this.stoppedOnAnimation = stoppedOnAnimation;
	}

	public bool getCanWalk(){
		return canWalk;
	}
	
	public void setCanWalk(bool canWalk){
		this.canWalk = canWalk;
	}

	public bool getCanJump(){
		return canJump;
	}
	
	public void setCanJump(bool canJump){
		this.canJump = canJump;
	}

	public bool getIsChangingClass(){
		return changingClass;
	}

	public bool getIsDead(){
		return isDead;
	}

	public void setIsDead(bool isDead){
		this.isDead = isDead;
	}

	List<GameObject> GetObjectsInLayer(GameObject root, int layer){
		List<GameObject> ret = new List<GameObject>();
		foreach (Transform t in root.transform.GetComponentsInChildren(typeof(Transform), true))
		{
			if (t.gameObject.layer == layer)
			{
				ret.Add (t.gameObject);
			}
		}
		
		return ret;        
	}
	
	List<GameObject> GetObjectsWithRigidbody(List<GameObject> objects){
		List<GameObject> ret = new List<GameObject>();
		foreach (GameObject t in objects)
		{
			if (t.GetComponents(typeof(Rigidbody)).Length != 0 )
			{
				ret.Add (t.gameObject);
			}
		}
		return ret;
	}
	
	List<Rigidbody> GetRigidbodiesFromObjects(List<GameObject> objects){
		List<Rigidbody> ret = new List<Rigidbody>();
		foreach (GameObject t in objects)
		{
			if (t.GetComponents(typeof(Rigidbody)).Length != 0 )
			{
				ret.Add (t.gameObject.GetComponent<Rigidbody>());
			}
		}
		return ret;
	}
//########UTILITIES END
//###########################################
}
