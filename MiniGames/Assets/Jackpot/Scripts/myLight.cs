using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myLight : MonoBehaviour {


	public int id;
	public Sprite white;
	public Sprite whiteOff;
	public Sprite red;
	public Sprite redOff;
	public Sprite blue;
	public Sprite blueOff;
	public Sprite green;
	public Sprite greenOff;
	public Sprite yellow;
	public Sprite yellowOff;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void activate(){
		SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer> ();
		if (id == 0) {
			rend.sprite = red;
		} else if (id == 9) {
			rend.sprite = yellow;
		} else if (id == 18) {
			rend.sprite = green;
		} else if (id == 27) {
			rend.sprite = blue;
		} else {
			rend.sprite = white;
		}
	}

	public void deactivate(){
		SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer> ();
		if (id == 0) {
			rend.sprite = redOff;
		} else if (id == 9) {
			rend.sprite = yellowOff;
		} else if (id == 18) {
			rend.sprite = greenOff;
		} else if (id == 27) {
			rend.sprite = blueOff;
		} else {
			rend.sprite = whiteOff;
		}
	}
}
