using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintQuaternion : MonoBehaviour {
	public Transform base1; 
	// Use this for initialization
	void Start () {
		Quaternion rotationDelta = Quaternion.FromToRotation(transform.forward, base1.forward);
		Debug.Log("AAAAA");
		Debug.Log(transform.rotation);
		Debug.Log(rotationDelta);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
