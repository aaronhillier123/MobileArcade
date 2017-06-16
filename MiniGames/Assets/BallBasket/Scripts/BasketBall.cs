using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour {

	public int color;
	public Sprite Red;
	public Sprite Blue;
	public Sprite Green;
	public Sprite Yellow;
	public Sprite Purple;
	public Sprite Orange;



	private GameObject BallBasketO;
	private BallBasket ballbasket;
	public List<GameObject> CollidingBalls = new List<GameObject> ();
	public List<GameObject> SameColor = new List<GameObject> ();
	public int id;
	// Use this for initialization
	void Start () {

		InvokeRepeating ("CheckForThree", .1f, .1f);
		InvokeRepeating ("CheckWin", .1f, .1f);
		BallBasketO = GameObject.Find ("BallBasket");
		ballbasket = BallBasketO.GetComponent<BallBasket> ();
		id = ballbasket.nextid;
		color = ballbasket.NextColor;

		SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer> ();
		switch(color){
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
			rend.sprite = Red;
			break;
		}
		ballbasket.reset ();

	}

	void OnCollisionEnter2D(Collision2D coll){
		//int count = 0;
		bool already = false;
		//bool alreadycolor = false;
		foreach (GameObject g in CollidingBalls) {
			if (g == coll.gameObject) {
				already = true;
			}
		}
		if (already == false) {
			CollidingBalls.Add (coll.gameObject);
			foreach (GameObject g in CollidingBalls) {
				if (g != null) {
					BasketBall ball = g.GetComponent<BasketBall> ();
					if (ball != null) {
						if (ball.color == color) {
							//foreach(GameObject gg in SameColor){
							//	if(gg == ball.gameObject){
							//		alreadycolor = true;
							//	}
							//}
							//if(alreadycolor == false){
							SameColor.Add (ball.gameObject);
						}
					}
				}
			}
			}

		}

	void OnCollisionExit2D(Collision2D col){
		bool same = false;
		foreach (GameObject g in SameColor){
			if (col.gameObject == g) {
				same = true;
			}
		}
		if (same == true) {
			SameColor.Remove (col.gameObject);
		}
		CollidingBalls.Remove (col.gameObject);
	}
	
		
	
	// Update is called once per frame
	void Update () {
			
	}

	public void CheckForThree(){
		if (SameColor.Count > 1) {
			bool del = CheckForRepeats ();
			if (del == true) {
				foreach (GameObject g in SameColor) {
					Destroy (g);
				}
				Destroy (gameObject);
				ballbasket.score = ballbasket.score + 3;
			}
		}
	}

	public bool CheckForRepeats(){
		bool del = false;
		//List<int> ids = new List<int> ();
		if (SameColor.Count < 2) {
			return false;
		}
		try{
		BasketBall ball = SameColor [0].GetComponent<BasketBall> ();
		if (ball != null) {
			int first = ball.id;
		
			//ids.Add (first);
			foreach (GameObject g in SameColor) {
				BasketBall b = g.GetComponent<BasketBall> ();
				if (b.id != first) {
					del = true;
				}
			}
		}
		}
		catch{
		}
		return del;
	}

	public void CheckWin(){
		if (transform.position.y > 4.8f) {
			ballbasket.EndGame ();
		}
	}
			
			
			
			
}
