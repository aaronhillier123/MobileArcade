using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderPiece : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		coll.gameObject.GetComponent<Lead>();
		if(coll!=null){
			Debug.Log("Crashed Into Wall");
			GameObject Trainobj = GameObject.Find ("Train");
			Train train = Trainobj.GetComponent<Train> ();
			train.EndGame ();
		}
	}
	
}
