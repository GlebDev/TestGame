using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour {

	private Transform target;
	private Vector3 m_OriginalCameraPosition;
	private Camera m_Camera;

	// Use this for initialization
	void Start () {
		m_Camera = Camera.main;
		m_OriginalCameraPosition = m_Camera.transform.position - target.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		m_Camera.transform.position = m_OriginalCameraPosition + target.position;
	}

	public void SetTarget(Transform NewTarget){
		target = NewTarget;
	}
}
