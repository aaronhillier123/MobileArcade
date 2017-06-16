using UnityEngine;
using System.Collections;

public class Foreground : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move(){
		transform.Translate (-.02f, 0f, 0f);
		if (transform.position.x < -16f) {
			transform.position = new Vector3 (32f, transform.position.y, transform.position.z);
		}
	}
}
