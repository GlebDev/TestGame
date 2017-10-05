using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuFunction : MonoBehaviour {

	[SerializeField] private string nextSceneName,loadingScreenScene;
	[SerializeField] private  Image volmeIcon;
	[SerializeField] private  Sprite volumeEnabled, volumeDiisabled;
	[SerializeField] private  Slider volumeSlider;

	public void ChangeVolume(){
		if (volumeSlider.value <= 0) {
			volmeIcon.sprite = volumeDiisabled;
		} else {
			volmeIcon.sprite = volumeEnabled;
		}
		AudioListener.volume = Mathf.Clamp(volumeSlider.value, 0f, 1f);
	} 

	public void Exit(){
		Application.Quit ();
	}

	public void StartGame(){
		LoadLevel ();
	}

	public void LoadLevel(){
		SceneManagementData.LoadingSceneName = nextSceneName;
		SceneManager.LoadScene(loadingScreenScene);

	} 

}
