using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallBasket : MonoBehaviour {

	public GameObject Ball;
	public int NextColor;
	public int Held = -1;
	public int nextid=0;
	private bool ready;
	private int count;
	public bool GameOver = false;
	public GameObject EndPanel;
	public int score = 0;
	public int time = 60;
	public bool started = false;
	// Use this for initialization
	void Start () {
		
	}

	void AStart(){
		NextColor = Random.Range (0, 6);
		InvokeRepeating ("Timer", 1f, 1f);
		GameObject HighScoreTextO = GameObject.Find ("HighScoreText");
		Text HighScoreText = HighScoreTextO.GetComponent<Text>();
		HighScoreText.text = "High \nScore: \n" + PlayerPrefs.GetInt ("BasketHighScore").ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		if (started == false) {
			if (Input.GetMouseButtonDown (0)) {
				Destroy (GameObject.Find ("StartText"));
				AStart ();
				started = true;
			}
		}

		if (started == true) {
			if (Input.GetMouseButtonDown (0) && ready == true && GameOver == false) {
				ready = false;
				count = 0;
				++nextid;
				Vector3 TouchPoint = getMousePosition ();
				if (TouchPoint.x > -2 && TouchPoint.x < 2.6) {
					GameObject NewBall = Instantiate (Ball) as GameObject;
					NewBall.GetComponent<BasketBall> ().color = NextColor;
					NewBall.transform.position = new Vector3 (TouchPoint.x, 4.5f, -1f);
				}
			}
			if (time == 0) {
				EndGame ();
			}
			GameObject ScoreO = GameObject.Find ("ScoreText");
			Text scoretext = ScoreO.GetComponent<Text> ();
			scoretext.text = "Score: \n" + score.ToString ();
			++count;
			if (count >= 12) {
				count = 12;
				ready = true;
			}
		}
	}

	public void reset(){
		NextColor = Random.Range (0, 6);
	}

		Vector3 getMousePosition(){
			Vector3 WorldPos = Input.mousePosition;
			WorldPos.z = 10;
			Vector3 ScreenMousePos = Camera.main.ScreenToWorldPoint (WorldPos);
			return ScreenMousePos;
		}

	public void EndGame(){

		if (GameOver == false) {
			CancelInvoke ();
			int highscore = PlayerPrefs.GetInt ("BasketHighScore");
			if (score > highscore) {
				PlayerPrefs.SetInt ("BasketHighScore", score);
			}
			GameObject endpanel = Instantiate (EndPanel) as GameObject;
			GameObject cano = GameObject.Find ("Canvas");
			Canvas can = cano.GetComponent<Canvas> ();
			endpanel.transform.SetParent (can.transform, false);
		}
		GameOver = true;
	}

	public void Timer(){
		time--;
		GameObject timeO = GameObject.Find ("TimeText");
		Text timetext = timeO.GetComponent<Text> ();
		timetext.text = "Time: \n" + time.ToString ();
	}

	public void Hold(){
		GameObject heldO = GameObject.Find ("Held");
		Held held = heldO.GetComponent<Held> ();
		if (held.color == -1) {
			Held = NextColor;
			NextColor = Random.Range (0, 6);
		} else if (held.color != -1) {
			int temp = held.color;
			Held = NextColor;
			NextColor = temp;
		}
	}
}
