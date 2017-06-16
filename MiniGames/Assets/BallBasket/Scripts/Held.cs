using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Held : MonoBehaviour {

	public int color = -1;
	public Sprite Red;
	public Sprite Blue;
	public Sprite Green;
	public Sprite Yellow;
	public Sprite Purple;
	public Sprite Orange;
	public Sprite Black;
	private GameObject BallBasketO;
	private BallBasket ballbasket;


	// Use this for initialization
	void Start () {
		BallBasketO = GameObject.Find ("BallBasket");
		ballbasket = BallBasketO.GetComponent<BallBasket> ();
		ChangeColor ();
	}

	// Update is called once per frame
	void Update () {
		if (color != ballbasket.Held) {
			ChangeColor ();
		}
	}

	public void ChangeColor(){

		color = ballbasket.Held;

		SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer> ();
		switch (color) {
		case 0:
			rend.sprite = Red;
			break;
		case 1:
			rend.sprite = Blue;
			break;
		case 2:
			rend.sprite = Green;
			break;
		case 3:
			rend.sprite = Purple;
			break;
		case 4:
			rend.sprite = Yellow;
			break;
		case 5:
			rend.sprite = Orange;
			break;
		default:
			rend.sprite = Black;
			break;
		}
	}
}
