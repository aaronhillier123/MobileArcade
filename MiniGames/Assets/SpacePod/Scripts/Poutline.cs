using UnityEngine;
using System.Collections;

public class Poutline : MonoBehaviour {

	public bool touching = false;
	public Material grass;
	public Material bad;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	void OnTriggerEnter2D(Collider2D collider){
		Ball ball = collider.gameObject.GetComponent<Ball> ();
		if (ball!=null){
			//change ();
			SpacePod game = FindObjectOfType(typeof(SpacePod)) as SpacePod;
			game.PaddlePhase = 2;
			game.FinishPaddle ();
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.gameObject.GetComponent<Ball>() != null){
			change ();
		}
	}
	
	void change(){
		if (touching == true) {
			touching = false;
		} else if (touching == false) {
			touching = true;
		}

		if (touching == false) {
			this.GetComponent<Renderer> ().material = grass;
		} else if (touching == true) {
			this.GetComponent<Renderer> ().material = bad;
		}
	}
		
}
