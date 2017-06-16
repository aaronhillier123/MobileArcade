using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePiece : MonoBehaviour {

	public int count = 0;
	public Material old;
	public bool inuse = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		++count;
		if (count == 2) {
			SetVoid ();
		}
		if (count >= 60 && this!= GameObject.Find("Rain").GetComponent<Rain>().PreviousLine && inuse == false) {
			Destroy (gameObject);
		}
	}

	public void SetVoid(){
		Destroy (GetComponent<BoxCollider2D> ());
		GetComponent<Renderer> ().material = old;
	}
}
