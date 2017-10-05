using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {

	public AI_Character CurPlayerCharacter{
		get{
			return curPlayerCharacter;
		}
		set{
			curPlayerCharacter = value;
		}
	}
	public List<AI_Character> PlayerCharacters{
		get{
			return playerCharacters;
		}
		set{
			playerCharacters = value;
		}
	}
	public int CurPlayerID {
		get {
			return curPlayerID;
		}
		set {
			curPlayerID = value;
		}
	}

	[SerializeField] private AI_Character[] characterPrefabs;
	[SerializeField] private List<AI_Character> playerCharacters;
	[SerializeField] private Transform mushroomPrefab;
	[SerializeField] private Transform characterCursor;

	private AI_Character curPlayerCharacter;
	private int curPlayerID;

	public void SpawnMushroom(Vector3 position){
		Instantiate (mushroomPrefab,position,Quaternion.identity);
	}

	public void SpawnRandomCharacter(Vector3 position, AI_Character.CharacterRoleType role){
		AI_Character curCharacter = Instantiate (characterPrefabs [Random.Range (0, characterPrefabs.Length)], position, Quaternion.identity) as AI_Character;
		curCharacter.RoleType = AI_Character.CharacterRoleType.Enemy;
	}

	public void SwitchPlayerCharacter(int NewCharacterID){
		Vector3 CursorOldPosition = characterCursor.localPosition;
		CurPlayerID = Mathf.Clamp(NewCharacterID, 0, PlayerCharacters.Count);
		CurPlayerCharacter = PlayerCharacters[NewCharacterID];
		characterCursor.SetParent (CurPlayerCharacter.transform);
		characterCursor.localPosition = CursorOldPosition;

	}
}
