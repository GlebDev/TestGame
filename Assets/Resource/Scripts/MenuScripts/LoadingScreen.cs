using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {


	[SerializeField] private RPB progressBar;

	private AsyncOperation async = null;

	private void Start()
	{
		StartCoroutine(LoadLevel(SceneManagementData.LoadingSceneName));
		progressBar.SetProgress (0);
	}

	private void FixedUpdate()
	{
		progressBar.SetProgress (async.progress);
	}


	IEnumerator LoadLevel(string scene) {
		async = SceneManager.LoadSceneAsync(scene);
		if (async != null)
		{
			progressBar.SetProgress (1-async.progress);
		}
		yield return async;
	}

}
