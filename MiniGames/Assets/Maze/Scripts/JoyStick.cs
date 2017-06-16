using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour {

	public bool touched = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float x = transform.localPosition.x;
		float y = transform.localPosition.y;
		float z = -2f;
		transform.localPosition = new Vector3 (x, y, z);

	}

	void OnMouseDown(){
		Debug.Log ("toucheddown");
		touched = true;
	}

	void OnMouseDrag(){
		if (touched == true) {
			Vector3 finger = getMousePosition ();
			Vector3 relativefinger = finger - transform.position;
			transform.position = new Vector3 (finger.x, finger.y, -2f);
			//transform.position = finger;
			float x = transform.localPosition.x;
			float y = transform.localPosition.y;
			float z = transform.localPosition.z;
			if (x > .4f) {
				x = .4f;
			}
			if (y > .4f) {
				y = .4f;
			}
			if (y < -.4f) {
				y = -.4f;
			}
			if (y < -.4f) {
				y = -.4f;
			}
			
			transform.localPosition = new Vector3 (x, y, -2f);
			float locx = x * .1f;
			float locy = y * .1f;
			float locz = 0;
			GameObject.Find ("Sphere(Clone)").GetComponent<Sphere> ().Velocity = new Vector3 (locx, locy, locz);

		}

	}

	void OnMouseUp(){
		touched = false;
		Debug.Log ("untouched");
		transform.localPosition = new Vector3 (0, 0, -2f);
		GameObject.Find ("Sphere(Clone)").GetComponent<Sphere> ().Velocity = new Vector3 (0, 0, 0);
	}

		public Vector3 getMousePosition(){
			Vector3 WorldPos = Input.mousePosition;
			WorldPos.z = 10;
			Vector3 ScreenMousePos = Camera.main.ScreenToWorldPoint (WorldPos);
			return ScreenMousePos;
		}

}
