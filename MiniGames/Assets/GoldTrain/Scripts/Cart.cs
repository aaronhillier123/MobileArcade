using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour {

	public int direction = 0;
	public int id = 0;


	public SpriteRenderer rend;
	public List<Vector3> PrevPos = new List<Vector3> ();
	public List<Quaternion> PrevRot = new List<Quaternion> ();
	public GameObject InFront;

	// Use this for initialization
	void Start () {

		GameObject FrontObj = GameObject.Find ("Lead");
		Lead lead = FrontObj.GetComponent<Lead> () as Lead;
		id = lead.Carts.Count-1;
		if (id == 0) {
			InFront = GameObject.Find ("Lead");
			float x = lead.transform.position.x;
			float y = lead.transform.position.y;
			transform.position = new Vector3 (x - .7f, y, 0f);
		} else {
			InFront = lead.Carts [id - 1].gameObject;
			float x = InFront.transform.position.x;
			float y = InFront.transform.position.y;
			transform.position = new Vector3 (x - .7f, y, 0f);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		RecordPrevs ();
		Move ();
	}

	public void Move(){
		Lead front = InFront.GetComponent<Lead> ();
		if (id == 0) {
			if (front != null) {
				transform.position = front.PrevPos [0];
				transform.rotation = front.PrevRot [0];
			}
		}
		else{
			Cart cartfront = InFront.GetComponent<Cart> ();
			if (cartfront != null) {
				transform.position = cartfront.PrevPos [0];
				transform.rotation = cartfront.PrevRot [0];
			}
		}
	}

	private void RecordPrevs(){
		if (PrevPos.Count < 10) {
			PrevPos.Add (transform.position);
		} else {
			for (int i = 0; i < 9; ++i) {
				PrevPos [i] = PrevPos [i + 1];
				PrevPos [9] = transform.position;
			}
		}

		if (PrevRot.Count < 10) {
			PrevRot.Add (transform.rotation);
		} else {
			for (int i = 0; i < 9; ++i) {
				PrevRot [i] = PrevRot [i + 1];
				PrevRot [9] = transform.rotation;
			}
		}
	}
}
