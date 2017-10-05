using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

	[SerializeField]private float delay;

	// Use this for initialization
	void Start () {
		Destroy (gameObject,delay);
	}

}
