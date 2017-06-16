using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SpacePod : MonoBehaviour {
	public GameObject PaddlePrefab;
	public GameObject Poutline;
	public GameObject BallPrefab;
	public Poutline CurrentOutline;
	public Paddle CurrentPaddle;
	public GameObject EndPanel;
	public GameObject ObstacleObject;

	//Decalre all obstacles for the current scene
	public GameObject Meteor1;
	public GameObject Meteor2;
	public GameObject Meteor3;
	public GameObject Meteor4;
	public GameObject Rocket;
	public GameObject Alien;
	public GameObject Satellite;
	public GameObject Comet;

	//Declare texts for scores and panels
	public Text ScoreText;
	public Text HighScoreText;
	public Text EndGame;
	public bool Started = false;

	//Declare the list holding the obstacles for randomization
	public List<GameObject> Obstacles = new List<GameObject> ();
	public List<Paddle> ActivePaddles = new List<Paddle>();
	private Vector3 OriginPoint;
	public int PaddlePhase = 0;
	public int score = 0;
	private bool ToDynamic = false;

	// Use this for initialization
	void Start () {
		
		Application.targetFrameRate = 60;

	}

	public void AStart(){
		Started = true;
		GameObject StartText = GameObject.Find ("StartText");
		Destroy (StartText);
		InitObstacles ();
		CreateBall ();
		ScoreText = Instantiate (ScoreText) as Text;
		HighScoreText = Instantiate (HighScoreText) as Text;
		ScoreText.color = Color.red;
		HighScoreText.color = Color.red;

		Canvas can = FindObjectOfType<Canvas> ();
		ScoreText.transform.SetParent (can.transform, false);
		HighScoreText.transform.SetParent (can.transform, false);
		ScoreText.transform.position = new Vector3 (Screen.width / 7, (6* Screen.height)/7, 0);
		HighScoreText.transform.position = new Vector3 (Screen.width / 7, (9* Screen.height)/10, 0);
		int sounds = PlayerPrefs.GetInt ("SoundToggle");
		if (sounds != 0) {
			PlaySound ();
		}
		HighScoreText.text = "High Score: " + PlayerPrefs.GetInt ("SpacePodHighScore").ToString();
		InvokeRepeating ("CountScore", .1f, .1f);
		InvokeRepeating ("CreateObstacle", 2f, 4f);
	}
	// Update is called once per frame
	void FixedUpdate () {

		if (Started == false) {
			PaddlePhase = 2;
			if (Input.GetMouseButton (0)) {
				AStart ();
			}
		}
		if (Started == true) {
			if (score > 750 && ToDynamic == false) {
				ToDynamic = true;
				Debug.Log ("Started dynamics");
				InvokeRepeating ("CreateComet", 1f, 2f); 
			}

			if (Input.GetMouseButton (0)) {
				if (PaddlePhase == 0) {
					OriginPoint = getMousePosition ();
					CreatePaddle (OriginPoint);
				} else if (PaddlePhase == 1) {
					if (CurrentOutline != null) {
						ModifyPaddle (getMousePosition ());
					}
				}
			}
			if ((!Input.GetMouseButton (0) && PaddlePhase != 0) || PaddlePhase==2) {
				if (CurrentOutline != null) {
					FinishPaddle ();
				}
				if (!Input.GetMouseButton(0) && PaddlePhase == 2) {
					PaddlePhase = 0;
				}
			}
				
		}
	}

	void PlaySound(){
		GetComponent<AudioSource> ().Play ();
	}
		
	void CountScore(){
		score = ++score;
		ScoreText.text = "Score: " + score.ToString ();
	}
		
	void InitObstacles(){
		Obstacles.Add (Meteor1);
		Obstacles.Add (Meteor2);
		Obstacles.Add (Meteor3);
		Obstacles.Add (Meteor4);
		Obstacles.Add (Rocket);
		Obstacles.Add (Satellite);
		Obstacles.Add (Alien);
	}

	void CreateBall(){
		GameObject ballobj = Instantiate (BallPrefab) as GameObject;
		//Ball ball = ballobj.GetComponent<Ball> (); 
	} 

	void OnMouseDown(){
		if (Started == false) {
			Debug.Log ("Starting");
			AStart ();
		}

		if (Started == true) {
			CreatePaddle (getMousePosition ());
		}
	}
		
	void CreatePaddle(Vector3 Location){
		//Create a paddle on click and set its location to the position of the mouse when the click occured
		GameObject OutlineObject = Instantiate (Poutline) as GameObject;
		CurrentOutline = OutlineObject.GetComponent<Poutline> ();
		CurrentOutline.transform.position = new Vector3 (Location.x, Location.y, 0f);
		CurrentOutline.transform.localScale = new Vector3 (.2f, .1f, 0.0f);
		PaddlePhase = 1;
	}

	void ModifyPaddle(Vector3 Location){
		//If the mouse is being dragged, the paddle grows and rotates according to the mouses current position
		//distance between the original click and the current mouse position
		float OriginToMouseX = OriginPoint.x - Location.x;
		float OriginToMouseY = OriginPoint.y - Location.y;

		//Angle between original click and current mouse position 
		float NewAngle = Mathf.Atan2 (OriginToMouseY, OriginToMouseX);
		NewAngle = (NewAngle * Mathf.Rad2Deg) + 90f;

		//this section deals with the size of the current paddle
		float diffx = absolute(Location.x - OriginPoint.x);
		float diffy = absolute(Location.y - OriginPoint.y);
		float distance = Mathf.Sqrt ((diffx * diffx) + (diffy + diffy));
		float MaxDist = distance;
		if(MaxDist>2f){MaxDist = 2f;}
		CurrentOutline.transform.localScale = new Vector3 (.2f, MaxDist, 0);

		//the paddle moves in order to appear to be growing from a side point instead of the middle out
		float XDirection = (MaxDist/2) * (Mathf.Cos(NewAngle * Mathf.Deg2Rad *-1));
		float YDirection = (MaxDist/2) * (Mathf.Sin(NewAngle * Mathf.Deg2Rad *-1));
		CurrentOutline.transform.position = new Vector3 (OriginPoint.x + YDirection, OriginPoint.y + XDirection, 0f);

		//this section deals with the rotation of the paddle
		CurrentOutline.transform.localRotation = Quaternion.Euler (0f, 0f, NewAngle);
	}

	//function to find absolute value of a number
	float absolute(float a){
		if (a<0){
			a = a * -1;
		}
		return a;
	}

	//when mouse is released, the paddle is finalized and added to the array of active paddles
	//the paddle can no longer be modified
	public void FinishPaddle(){
		//if(CurrentOutline!=null){

			//deletes the paddle outline if it is already touching the ball
		//if (CurrentOutline.touching == true) {
		//	Destroy (CurrentOutline.gameObject);
		//		CurrentOutline = null;
		//		PaddlePhase = 0;
		//}
		//else{
				
		//This part creates a paddle in the exact position and size of the outline that was there
		GameObject PaddleObject = Instantiate (PaddlePrefab) as GameObject;
		CurrentPaddle = PaddleObject.GetComponent<Paddle> ();
		CurrentPaddle.transform.position = CurrentOutline.transform.position;
		CurrentPaddle.transform.localScale = CurrentOutline.transform.localScale;
		CurrentPaddle.transform.localScale = CurrentPaddle.transform.localScale - new Vector3 (.05f, .05f, 0f);
		CurrentPaddle.transform.rotation = CurrentOutline.transform.rotation;
		ActivePaddles.Add (CurrentPaddle);
		Destroy (CurrentOutline.gameObject);
		if (PaddlePhase == 1) {
			PaddlePhase = 0;
		}

				//Removes the oldest paddle if more than 3 paddles are made
		if (ActivePaddles.Count > 3) {
			Paddle OldPaddle = ActivePaddles [0];
			ActivePaddles.RemoveAt (0);
			Destroy (OldPaddle.gameObject);
		}
	//}
	//}
	}

	Vector3 getMousePosition(){
		Vector3 WorldPos = Input.mousePosition;
		WorldPos.z = 10;
		Vector3 ScreenMousePos = Camera.main.ScreenToWorldPoint (WorldPos);
		return ScreenMousePos;
	}

	void CreateObstacle(){
		int rand = UnityEngine.Random.Range (0, 7);
		Obstacle CurrentOb = Instantiate (Obstacles [rand].GetComponent<Obstacle> ()) as Obstacle;
	}

	public void end(){

		Canvas canv = FindObjectOfType (typeof(Canvas)) as Canvas;

		//deals with sounds when game ends
		AudioSource Player = GetComponent<AudioSource> ();
		Player.mute = true;

		//shows the ending c at game end
		GameObject EndPanelObj;
		EndPanelObj = Instantiate (EndPanel) as GameObject;
		EndPanelObj.transform.SetParent (canv.transform, false);

		//sets the HighScore if this game's score was higher
		int a = Screen.height/2;
		int b = Screen.width/2;
		EndGame = Instantiate (EndGame) as Text;
		int MyScore = PlayerPrefs.GetInt ("SpacePodHighScore");
		if (score > MyScore) {
			PlayerPrefs.SetInt ("SpacePodHighScore", score);
		}

		//output message at the end of the game
		EndGame.transform.SetParent (canv.transform, false);
		EndGame.text = "The space pod has crashed!" + "\n YOUR SCORE WAS " + score.ToString ();
		EndGame.color = Color.red;

		//adjust position and size of end game panel
		EndGame.rectTransform.sizeDelta = new Vector3 (200, 100);
		EndGame.transform.position = new Vector3 (Screen.width / 2, 2*Screen.height / 3, 0);
		EndGame.alignment = TextAnchor.MiddleCenter;
		EndPanelObj.transform.position = new Vector3 (b, a);
	}
		
	public void CreateComet(){
		Debug.Log ("Creating comet");
		int rand = UnityEngine.Random.Range (0, 2);
		if (rand == 1) {
			//Debug.Log ("Creating chicken");
			GameObject CurrentComet = Instantiate (Comet) as GameObject;
			//Comet comet = CurrentComet.GetComponent<Comet> (); 
		}
	}
}
