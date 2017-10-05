using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Model _Model;

	[SerializeField] private int maxEnemiesCount;
	[SerializeField] private float spawnEnemyDelay,SpawnMushroomDelay;
	[SerializeField] private int spawnAreaRange;
	[SerializeField] private CameraFollowing cameraFollowing;
	[SerializeField] private int playerSpeed;
	[SerializeField] private Text scoreText;
	[SerializeField] private RectTransform pauseMenu;
	[SerializeField] private string menuSceneName,loadingScreenScene;

	private int score;
	private int CurEnemiesCount;

	public int Score{
		get{ 
			return score;
		}
		set{
			score = value;
		}
	}
		
	// Use this for initialization
	void Start () {
		CurEnemiesCount = 0;
		StartSpawnEnemies ();
		StartSpawnMushrooms ();
		_Model.CurPlayerID = 0;
		_Model.CurPlayerCharacter = _Model.PlayerCharacters[_Model.CurPlayerID];
		cameraFollowing.SetTarget (_Model.CurPlayerCharacter.transform);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			TooglePauseMenu ();
		}
	}

	public float GetPlayerSpeed(){
		return playerSpeed;
	}

	void StartSpawnEnemies(){
		InvokeRepeating ("createEnemyOnRandomPosition",0f,spawnEnemyDelay);
	}

	void createEnemyOnRandomPosition (){
		if (CurEnemiesCount < maxEnemiesCount) {
			_Model.SpawnRandomCharacter (new Vector3 (Random.Range (-spawnAreaRange, spawnAreaRange), 0, Random.Range (-spawnAreaRange, spawnAreaRange)), AI_Character.CharacterRoleType.Enemy);
			CurEnemiesCount++;
		} else {
			CancelInvoke ("createEnemyOnRandomPosition");
		}
	}
		
	void StartSpawnMushrooms(){
		InvokeRepeating ("createMushroomOnRandomPosition",0f,SpawnMushroomDelay);
	}

	void createMushroomOnRandomPosition (){
		_Model.SpawnMushroom( new Vector3 (Random.Range (-spawnAreaRange, spawnAreaRange), 0, Random.Range (-spawnAreaRange, spawnAreaRange)));
	}

	public void NextCharacter(){
		_Model.CurPlayerCharacter.StopWalk ();
		int NewID = _Model.CurPlayerID;
		NewID++;
		if (NewID < 0)
			NewID = _Model.PlayerCharacters.Count - 1;
		if (NewID > _Model.PlayerCharacters.Count - 1)
			NewID = 0;
		_Model.SwitchPlayerCharacter(NewID);
		cameraFollowing.SetTarget (_Model.CurPlayerCharacter.transform);
	}

	public void PrevCharacter(){
		_Model.CurPlayerCharacter.StopWalk ();
		int NewID = _Model.CurPlayerID;
		NewID--;
		if (NewID < 0)
			NewID = _Model.PlayerCharacters.Count - 1;
		if (NewID > _Model.PlayerCharacters.Count - 1)
			NewID = 0;
		_Model.SwitchPlayerCharacter(NewID);
		cameraFollowing.SetTarget (_Model.CurPlayerCharacter.transform);
	}

	public void SetScoreText(string str){
		scoreText.text = "SCORE: "+str;
	}

	public void deleteOneCharacter(){
		CurEnemiesCount--;
		StartSpawnEnemies ();
	}

	public void TooglePauseMenu(){
		pauseMenu.gameObject.SetActive (!pauseMenu.gameObject.activeSelf);
	}

	public void BackToMenu(){
		SceneManagementData.LoadingSceneName = menuSceneName;
		SceneManager.LoadScene(loadingScreenScene);

	} 

}
