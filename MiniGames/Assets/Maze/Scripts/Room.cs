using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour {

	public VWall RightWall;
	public VWall LeftWall;
	public HWall UpWall;
	public HWall DownWall;
	public IWall FowardWall;
	public IWall BackwardWall;

	public Room RightRoom;
	public Room LeftRoom;
	public Room UpRoom;
	public Room DownRoom;
	public Room FowardRoom;
	public Room BackwardRoom;

	public bool final = false;

	public bool visited = false;
	public List<Room> ConnectedRooms = new List<Room> ();
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		GameObject FBO = GameObject.Find ("FowardButton");
		GameObject BBO = GameObject.Find ("BackwardButton");
		Button FB = FBO.GetComponent<Button> ();
		Button BB = BBO.GetComponent<Button> ();

		GameObject collO = coll.gameObject;
		Sphere sphere = collO.GetComponent<Sphere> ();
		//if (sphere != null) {
		//	if (BackwardWall == null) {
		//		BB.interactable = true;
		//	}
		//	if (FowardWall == null) {
		//		FB.interactable = true;
		//	}
			if (final == true) {
				Generation gen = FindObjectOfType (typeof(Generation)) as Generation;
				gen.EndGame ();
			}
		}
	//}

	void OnTriggerExit2D(Collider2D coll){
		GameObject FBO = GameObject.Find ("FowardButton");
		GameObject BBO = GameObject.Find ("BackwardButton");
		Button FB = FBO.GetComponent<Button> ();
		Button BB = BBO.GetComponent<Button> ();
		FB.interactable = false;
		BB.interactable = false;
	}

}
