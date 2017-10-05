using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class RPB : MonoBehaviour {

	public float CurrentAmount{
		get{
			return currentAmount;
		}
		set{
			currentAmount = Mathf.Clamp (value, 0f, 1f);
		}
	}

	private float currentAmount;


	public void SetProgress(float progress){
		CurrentAmount = progress;
		GetComponent<Image> ().fillAmount = currentAmount;
	}

	public void SetTransparent(float alpha){
		if (alpha < 0) {
			alpha = 0f;
		}
		if (alpha > 1) {
			alpha = 1f;
		}
		GetComponent<Image> ().CrossFadeAlpha(alpha,0.2f,false);
	}
}
