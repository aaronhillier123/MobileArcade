using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AnimalSlingshot : MonoBehaviour {

	public GameObject Current;
	public GameObject Loaded;
	public GameObject Target;
	public GameObject EndPanel;
	public Button MainButton;

	public GameObject Bullet;

	public bool started = false;
	public bool GameOver = false;
	private Vector3 TranVector;
	public Text GoodScoreText;
	public Text BadScoreText;
	public int CShape = 1;
	public float GoodScore = 0f;
	public float BadScore = 0f;
	public bool able = true;
	public string mode;
	private float rate;
	public bool ended = false;
	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		GameObject HighScoreO = GameObject.Find ("HighScoreText");
		Text HighScore = HighScoreO.GetComponent<Text> ();
		HighScore.text = "High Score: " + PlayerPrefs.GetFloat ("AnimalHighScore").ToString();

	}

	public void AStart(){
		started = true;
		GameObject StartText = GameObject.Find ("StartText");
		Destroy (StartText);
		//rate = PlayerPrefs.GetFloat ("Rate");
		Debug.Log (Screen.width + " and " + Screen.height);
		//Testing purposes
		rate = 1.5f;
		Current = Bullet;
		Current.GetComponent<Bullet> ().animal = 0;
		LoadSling ();

		mode = SceneManager.GetActiveScene().name;
		GoodScoreText = Instantiate (GoodScoreText) as Text;
		BadScoreText = Instantiate (BadScoreText) as Text;
		Canvas can = FindObjectOfType (typeof(Canvas)) as Canvas;
		GoodScoreText.color = Color.green;
		BadScoreText.color = Color.red;
		BadScoreText.transform.position = new Vector3 (2*Screen.width/3, (1* Screen.height)/5, 0);
		GoodScoreText.transform.position = new Vector3 (2*Screen.width/9, (1* Screen.height)/5, 0);
		GoodScoreText.transform.SetParent (can.transform);
		BadScoreText.transform.SetParent (can.transform);
		if (mode != "GameBasic") {
			InvokeRepeating ("CreateTarget", rate, rate);
		}
		InvokeRepeating ("Clean", .5f, .5f);
	}
	
	// Update is called once per frame
		void FixedUpdate () {
		
		if (Input.GetMouseButtonDown (0) && started==false) {
			Survival sur = FindObjectOfType (typeof (Survival)) as Survival;
			sur.AStart ();
			AStart ();
		}
		if (started == true) {
			if (Loaded == null) {
				LoadSling ();
			}
		}
	}
		

	private void Clean(){
		Bullet[] bullets = FindObjectsOfType (typeof(Bullet)) as Bullet[];
		foreach (Bullet b in bullets) {
			if (b.motion == false && b.gameObject != Loaded) {
				b.motion = true;
			}
		}
	}

	public void UpdateScore(){
		GoodScoreText.text = GoodScore.ToString ();
		BadScoreText.text = BadScore.ToString ();
	}
		

	private void LoadSling(){
		GameObject shot = GameObject.Find ("Shot");
		if (shot != null) {
			if (Current != null) {
				shot.transform.localPosition = new Vector3 (0f, -.3f, 1f);
				GameObject currentobj = Instantiate (Current) as GameObject;
				//currentobj.transform.localPosition = shot.transform.position;
				float x = shot.transform.position.x;
				float y = shot.transform.position.y;
				float z = shot.transform.position.z -3;
				currentobj.transform.localPosition = new Vector3 (x, y, z);
				currentobj.transform.localScale = new Vector2 (.4f, .4f);
				//shot.GetComponent<Shot> ().SetSprite (CShape);
				currentobj.GetComponent<Bullet> ().animal= CShape;
				SetLoaded (currentobj);
			}
		}
	}


	public void SetCurrent(GameObject a){
		Current = a;
	}

	public GameObject GetCurrent(){
		return Current;
	}

	public GameObject GetLoaded(){
		return Loaded;
	}

	public void SetLoaded(GameObject a){
		Loaded = a;
	}

	public void CreateTarget(){
		//int index = 0;
		int level = UnityEngine.Random.Range (0, 4);
		int dir = UnityEngine.Random.Range (0, 2);
		int shape = UnityEngine.Random.Range (0, 4);
		GameObject TarOb;
		TarOb = Instantiate (Target) as GameObject;
		Target MyTarget = TarOb.GetComponent<Target> ();
		if (MyTarget != null) {
			MyTarget.SetDirection (dir, level);
			MyTarget.SetShape (shape);
		}
	}
		

	public void GoodHit(int a, int b){
			GoodScore = GoodScore + 2f;
			GoodScoreText.text = GoodScore.ToString ();
	}

	public void BadHit(int a){
			GoodScore = GoodScore - 1f;
			GoodScoreText.text = GoodScore.ToString ();
	}

	public void end(){
		Debug.Log("Game Over");
		GameOver = true;
		if (ended == false) {
			if (GoodScore > PlayerPrefs.GetFloat ("AnimalHighScore")) {
				PlayerPrefs.SetFloat ("AnimalHighScore", GoodScore);
			}
			CancelInvoke ();
			Survival sur = FindObjectOfType (typeof(Survival)) as Survival;
			sur.CancelInvoke ();
			GameObject b = Instantiate (EndPanel) as GameObject;
			Canvas can = FindObjectOfType (typeof(Canvas)) as Canvas;
			b.transform.SetParent (can.transform);
			b.transform.localScale = new Vector3(1, 1, 1);
			b.transform.localPosition = new Vector3 (0, 0, 0);
			ended = true;
		}

	}


		
			
}
