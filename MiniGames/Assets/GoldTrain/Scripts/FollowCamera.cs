using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject train = GameObject.Find ("Lead");
		float x = train.transform.position.x;
		if (x <= 0f) {
			x = 0f;
		}
		if (x >= 12f) {
			x = 12f;
		}
		float y = train.transform.position.y;
		if (y <= -1f) {
			y = -1f;
		}
		if (y >= 6f) {
			y = 6f;
		}
		float z = -10f;
		transform.position = new Vector3 (x, y, z);
	}
}
