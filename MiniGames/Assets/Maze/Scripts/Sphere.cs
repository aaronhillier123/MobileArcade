using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Sphere : MonoBehaviour {

	public Vector2 Velocity;
	public bool movable;
	public bool movingdown = false;
	public bool movingup = false;
	public bool movingright = false;
	public bool movingleft = false;
	public Room MyRoom;
	private Button FB;
	private Button BB;
	// Use this for initialization
	void Start () {
		movable = true;
		Velocity = new Vector2 (0f, 0f);
		MyRoom = GameObject.Find ("Generator").GetComponent<Generation> ().AllRooms [0];



	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Move ();


		if (MyRoom != null) {
			if (GameObject.Find ("Generator").GetComponent<Generation> ().started == true) {
				Button FB = GameObject.Find ("FowardButton").GetComponent<Button> ();
				Button BB = GameObject.Find ("BackwardButton").GetComponent<Button> ();
				if (MyRoom.BackwardWall == null) {
					BB.interactable = true;
				} else {
					BB.interactable = false;
				}

				if (MyRoom.FowardWall == null) {
					FB.interactable = true;
				} else {
					FB.interactable = false;
				}
			}
		}

		}
		

	public void Move(){
		transform.Translate (Velocity);
	}

	public void OnTriggerEnter2D(Collider2D coll){
		GameObject roomO = coll.gameObject;
		Room MyNewRoom = roomO.GetComponent<Room> ();
		if(MyNewRoom!=null){
			MyRoom = MyNewRoom;
		}
	}


}
