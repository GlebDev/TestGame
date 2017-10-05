using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clicker : MonoBehaviour {

	[SerializeField] private GameController GC;

	private Vector2 touchOrigin;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Check if we are running either in the Unity editor or in a standalone build.
		#if UNITY_STANDALONE || UNITY_WEBPLAYER
		if (Input.GetMouseButtonDown (1)) {
			RaycastHit hit;
			//Create ray from camera to Cursor click point
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit)) {
				GC._Model.CurPlayerCharacter.WalkTo(hit.point);
				if (hit.transform.CompareTag ("Enemy")) {
					GC._Model.CurPlayerCharacter.WalkTo (hit.transform);
				}
			}
		}
		//Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		if (Input.touchCount > 0){
			//Store the first touch detected.
			Touch myTouch = Input.touches[0];
			//Check if the phase of that touch equals Began
			if (Input.touches[0].phase == TouchPhase.Began)
			{
			RaycastHit hit;
			//Create ray from camera to touch point
			Ray ray = Camera.main.ScreenPointToRay (myTouch.position); 
				if (Physics.Raycast(ray, out hit)) {
					GC._Model.CurPlayerCharacter.WalkTo(hit.point);
					if (hit.transform.CompareTag ("Enemy")) {
						GC._Model.CurPlayerCharacter.WalkTo (hit.transform);
					}
				}
			}
		}
		#endif //End of mobile platform dependendent compilation section started above with #elif
	}
		
}
