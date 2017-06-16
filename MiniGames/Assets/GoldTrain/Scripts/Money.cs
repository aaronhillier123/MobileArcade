using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour {

	public GameObject lead;
	// Use this for initialization
	void Start () {
		lead = GameObject.Find ("Lead");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll!=null){
			Lead lead = coll.gameObject.GetComponent<Lead>();
			if (lead != null) {
				lead.NewCart ();
				GameObject trainobj = GameObject.Find ("Train");
				Train train = trainobj.GetComponent<Train> ();
				train.CreateMoney ();
				Destroy (this.gameObject);
			}
		}
	}
}
