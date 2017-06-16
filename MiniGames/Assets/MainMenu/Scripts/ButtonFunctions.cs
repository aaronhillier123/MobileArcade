using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour {
	public GameObject Menu;
	// Use this for initialization
	void Start () {

		Application.targetFrameRate = 60;

		GameObject men = Instantiate (Menu) as GameObject;
		men.transform.SetParent (GameObject.Find ("Canvas").transform, false);

		try{
		GameObject SpaceScoreO = GameObject.Find ("SpaceScore");
		Text SpaceScore = SpaceScoreO.GetComponent<Text> ();
		SpaceScore.text = "High Score: " + PlayerPrefs.GetInt ("SpacePodHighScore").ToString();

		GameObject AnimalScoreO = GameObject.Find ("AnimalScore");
		Text AnimalScore = AnimalScoreO.GetComponent<Text> ();
		AnimalScore.text = "High Score: " + PlayerPrefs.GetFloat ("AnimalHighScore").ToString ();


		GameObject TrainScoreO = GameObject.Find ("TrainScore");
		Text TrainScore = TrainScoreO.GetComponent<Text> ();
		TrainScore.text = "High Score: " + PlayerPrefs.GetInt ("TrainHighScore").ToString();

		GameObject BasketScoreO = GameObject.Find ("BasketScore");
		Text BasketScore = BasketScoreO.GetComponent<Text> ();
		BasketScore.text = "High Score: " + PlayerPrefs.GetInt ("BasketHighScore").ToString();

		GameObject MazeScoreO = GameObject.Find ("MazeScore");
		Text MazeScore = MazeScoreO.GetComponent<Text> ();
		MazeScore.text = "High Score: " + PlayerPrefs.GetInt ("MazeHighScore").ToString();

		GameObject RainScoreO = GameObject.Find ("RainScore");
		Text RainScore = RainScoreO.GetComponent<Text> ();
		RainScore.text = "High Score: " + PlayerPrefs.GetInt ("RainHighScore").ToString();
		} 
		catch{
		}

		}


	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadSpacePod(){
		Screen.orientation = ScreenOrientation.Landscape;
		PlayerPrefs.SetString("Loading", "SpacePod");
		SceneManager.LoadScene ("Loading");
	}

	public void LoadMain(){
		Screen.orientation = ScreenOrientation.Landscape;
		PlayerPrefs.SetString("Loading", "MainMenu");
		SceneManager.LoadScene ("Loading");
	}

	public void LoadAnimalSlingshot(){
		Screen.orientation = ScreenOrientation.Portrait;
		PlayerPrefs.SetString("Loading", "AnimalSlingshot");
		SceneManager.LoadScene ("Loading");
	}

	public void LoadGoldTrain(){
		Screen.orientation = ScreenOrientation.Landscape;
		PlayerPrefs.SetString ("Loading", "GoldTrain");
		SceneManager.LoadScene ("Loading");
	}

	public void LoadBallBasket(){
		Screen.orientation = ScreenOrientation.Portrait;
		PlayerPrefs.SetString ("Loading", "BallBasket");
		SceneManager.LoadScene ("Loading");
	}

	public void LoadMaze(){
		Screen.orientation = ScreenOrientation.Landscape;
		PlayerPrefs.SetString ("Loading", "Maze");
		SceneManager.LoadScene ("Loading");
	}

	public void LoadLetterRain(){
		Screen.orientation = ScreenOrientation.Portrait;
		PlayerPrefs.SetString ("Loading", "LetterRain");
		SceneManager.LoadScene ("Loading");
	}
	public void LoadSpacePodT(){
		Screen.orientation = ScreenOrientation.Landscape;
		PlayerPrefs.SetString("Loading", "SpacePodT");
		SceneManager.LoadScene ("Loading");
	}
	public void LoadAnimalSlingshotT(){
		Screen.orientation = ScreenOrientation.Portrait;
		PlayerPrefs.SetString("Loading", "AnimalSlingshotT");
		SceneManager.LoadScene ("Loading");
	}

	public void LoadGoldTrainT(){
		Screen.orientation = ScreenOrientation.Landscape;
		PlayerPrefs.SetString ("Loading", "GoldTrainT");
		SceneManager.LoadScene ("Loading");
	}

	public void LoadBallBasketT(){
		Screen.orientation = ScreenOrientation.Portrait;
		PlayerPrefs.SetString ("Loading", "BallBasketT");
		SceneManager.LoadScene ("Loading");
	}

	public void LoadMazeT(){
		Screen.orientation = ScreenOrientation.Landscape;
		PlayerPrefs.SetString ("Loading", "MazeT");
		SceneManager.LoadScene ("Loading");
	}

	public void LoadLetterRainT(){
		Screen.orientation = ScreenOrientation.Portrait;
		PlayerPrefs.SetString ("Loading", "LetterRainT");
		SceneManager.LoadScene ("Loading");
	}
}
