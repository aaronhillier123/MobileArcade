using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject LeftHud = GameObject.Find ("LeftHud");
		LeftHud.transform.position = new Vector3 (-1 * Screen.width / 20, -1 * Screen.height / 20);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
