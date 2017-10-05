using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Character : MonoBehaviour {

	public enum CharacterRoleType{Player,Enemy};
	public enum StateOfMovement{Walk,Stand};
	public bool IsDie{
		get{
			return isDie;
		}
		set{
			isDie = value;
		}
	}
	public CharacterRoleType RoleType{
		get{ 
			return roleType;
		}
		set{
			roleType = value;
			switch (value) {
			case  CharacterRoleType.Player:
				meshRenderer.material = playerMaterial;
				agent.speed = GC.GetPlayerSpeed();
				transform.tag = "Player";
				break;
			case  CharacterRoleType.Enemy:
				meshRenderer.material = enemyMaterials[Random.Range(0,enemyMaterials.Length)];
				transform.tag = "Enemy";
				break;
			default:
				break;
			}
		}
	}

	public StateOfMovement CharacterStateOfMovement{
		get{ 
			return characterStateOfMovement;
		}
		set{
			characterStateOfMovement = value;
			switch (value) {
			case StateOfMovement.Walk:
				_animator.SetBool ("IsWalk",true);
				break;
			case StateOfMovement.Stand:
				_animator.SetBool ("IsWalk",false);
				break;
			default:
				break;
			}
		}
	}

	[SerializeField]private  CharacterRoleType roleType;
	[SerializeField]private Animator _animator;
	[SerializeField]private NavMeshAgent agent;
	[SerializeField]private int walkAreaWidth, walkAreaLength;
	[SerializeField]private float walkSpeed;
	[SerializeField]private MeshRenderer meshRenderer;
	[SerializeField]private Material[] enemyMaterials;
	[SerializeField]private Material playerMaterial;
	[SerializeField]private float interactRadius;
	[SerializeField]private float destroyDelay;
	[SerializeField]private AudioClip eatMushroomAudio, collisionAudio;
		
	private StateOfMovement characterStateOfMovement;
	private Vector3 target = new Vector3(0,0);
	private Transform targetObject;
	private bool walkEnabled;
	private GameController GC;
	private bool isDie;




	// Use this for initialization
	void Start () {
		GC = GameObject.Find ("Controller").GetComponent<GameController>();
		switch (RoleType) {
		case CharacterRoleType.Player:
			StopWalk ();
			agent.speed = GC.GetPlayerSpeed ();
			break;
		case CharacterRoleType.Enemy:
			WalkTo (GetRandomPositionOnArea (walkAreaWidth, walkAreaLength));
			agent.speed = walkSpeed;
			break;
		default:
			break;
		}

	}

	void Update(){

		/*---- Processing of collision with near objects ----*/
		if(RoleType == CharacterRoleType.Player){
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactRadius);
			foreach(Collider col in hitColliders){
				if (col.CompareTag ("Enemy") && col.transform.parent.GetComponent<AI_Character>() != null) {
					InteractWithEnemy (col.transform.parent.GetComponent<AI_Character>());
				}
				if (col.CompareTag ("Mushroom")) {
					transform.GetComponent<AudioSource> ().PlayOneShot(eatMushroomAudio);
					Destroy(col.gameObject);
					GC.Score++;
					GC.SetScoreText(GC.Score.ToString ());
				}
			}
		}
		/*------------------------------------*/

		if (agent.remainingDistance <= 0.1f) {  //if distance to target <= 0.1
			StopWalk ();
			if (RoleType == CharacterRoleType.Enemy) {
				WalkToRandomPosition();
			}

		} else if(CharacterStateOfMovement == StateOfMovement.Walk){
			if (targetObject != null) {
				target = targetObject.position;
			}
			agent.SetDestination (target);
		}
	
	}
		
	public void SetTarget (Vector3 NewPosition){
		target = NewPosition;
		agent.SetDestination (target);
	}

	public void WalkToTarget(){
		CharacterStateOfMovement = StateOfMovement.Walk;
	}

	public void WalkTo(Vector3 NewPosition){
		SetTarget (NewPosition);
		WalkToTarget ();
		targetObject = null;
	}

	public void WalkTo(Transform NewTarget){
		targetObject = NewTarget;
		WalkToTarget ();
	}

	public void WalkToRandomPosition(){
		WalkTo (GetRandomPositionOnArea (walkAreaWidth, walkAreaLength));
	}

	public void StopWalk(){
		agent.ResetPath ();
		CharacterStateOfMovement = StateOfMovement.Stand;
	}


	private Vector3 GetRandomPositionOnArea(int x, int z){
		Vector3 newPosition = new Vector3 (Random.Range(-x/2f,x/2f),0,Random.Range(-z/2f,z/2f));
		return newPosition;
	}

	private void InteractWithEnemy(AI_Character EnemyCharacter){
		if(!EnemyCharacter.IsDie){
			transform.GetComponent<AudioSource> ().PlayOneShot(collisionAudio);
			EnemyCharacter.IsDie = true;
			GC.deleteOneCharacter();
			EnemyCharacter.StopWalk ();
			EnemyCharacter.Invoke("CreateMushroomOnBodyPosition",destroyDelay);
			Destroy (EnemyCharacter.gameObject,destroyDelay);
		}

	}

	private void CreateMushroomOnBodyPosition(){
		GC._Model.SpawnMushroom(transform.position);
	}

}
