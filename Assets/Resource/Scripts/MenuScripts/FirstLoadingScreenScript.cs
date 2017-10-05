using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLoadingScreenScript : MonoBehaviour {

	[SerializeField] private float loadingDelay;
	[SerializeField] private string nextSceneName;
	[SerializeField] private RPB progressBar;

	private float curLoadingTime;
	// Use this for initialization
	void Start () {
		Invoke ("LoadLevel",loadingDelay);
		curLoadingTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		curLoadingTime += Time.deltaTime;
		progressBar.SetProgress (curLoadingTime / loadingDelay);
	}

	public void LoadLevel(){
		SceneManager.LoadScene(nextSceneName);

	} 
}
