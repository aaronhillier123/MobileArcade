using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lead : MonoBehaviour {

	public int direction = 0;

	public bool TurningRight = false;
	public bool TurningLeft = false;
	public int TurnPowerCount = 0;
	public float TurnPower = 2f;
	public float Speed = .07f;
	public SpriteRenderer rend;
	public bool Started = false;
	public GameObject Cart;
	public List<Cart> Carts = new List<Cart>();
	public List<Vector3> PrevPos = new List<Vector3> ();
	public List<Quaternion> PrevRot = new List<Quaternion>();
	public int score = 0;
	public float power = 200;
	private GameObject MainTrainObj;
	private Train maintrain;
	private PowerBar bar;


	// Use this for initialization
	void Start () {
		MainTrainObj = GameObject.Find ("Train");
		maintrain = MainTrainObj.GetComponent<Train> ();
		GameObject pbo = GameObject.Find ("PowerBar");
		bar = pbo.GetComponent<PowerBar> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (Started == true) {
			RecordPrevs ();
			Move ();
			power = power - .1f;
			//bar.Decrease ();
			if (power <= 0) {
				maintrain.EndGame ();
			}
		} else {
			if (Input.GetMouseButtonDown (0)) {
				Started = true;
				Destroy(GameObject.Find("StartText"));
				}
			}
		}


	public void Move(){
		if (TurningRight == true) {
			transform.Rotate (0f, 0f, -1 * TurnPower);
		} else if (TurningLeft == true) {
			transform.Rotate (0f, 0f, TurnPower);
		}
		transform.Translate (Speed, 0f, 0f);
	}

	public void TurnRight(){
		TurningRight = true;
	}
	public void NoRight(){
		TurningRight = false;
	}
	public void TurnLeft(){
		TurningLeft = true;
	}
	public void NoLeft(){
		TurningLeft = false;
	}


	public void NewCart(){
		IncScore ();
		TurnPower = TurnPower + .1f;
		GameObject Cartobj = Instantiate (Cart) as GameObject;
		Cart cart = Cartobj.GetComponent<Cart> ();
		Cartobj.transform.SetParent (transform.parent);
		cart.id = Carts.Count;
		if (Carts.Count == 0) {
			float x = gameObject.transform.position.x - .8f;
			float y = gameObject.transform.position.y;
			cart.gameObject.transform.position = new Vector3 (x, y, 0f);
		} else {
			float x = Carts[Carts.Count - 1].transform.position.x -.8f;
			float y = Carts[Carts.Count - 1].transform.position.y;
			cart.gameObject.transform.position = new Vector3 (x, y, 0f);
		}
		cart.direction = direction;

		Carts.Add (cart);
		}

	public void NewCoal(){
		power = power + 40f;
		if (power > 500) {
			power = 500;
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


	void OnTriggerEnter2D(Collider2D coll){
		if(coll!=null){
			Cart carto = coll.gameObject.GetComponent<Cart>();
			if (carto != null) {
				if (carto.id != 0) {
					GameObject trainObj = GameObject.Find ("Train");
					Train train = trainObj.GetComponent<Train> ();
					train.EndGame ();
				}
			}
		}
	}

	public void IncScore(){
		score++;
		GameObject ScoreO = GameObject.Find ("ScoreText");
		Text Score = ScoreO.GetComponent<Text> ();
		Score.text = " x " + score.ToString();
	}

}

