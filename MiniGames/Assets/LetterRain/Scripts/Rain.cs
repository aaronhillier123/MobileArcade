using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rain : MonoBehaviour {

	public GameObject Letter;
	public GameObject LinePiece;
	public GameObject Line;
	public GameObject Endpanel;
	public bool Drawing = false;
	public Vector3 LastPos = new Vector3();
	private bool LastPosMatters = false;
	public GameObject PreviousLine;
	public string current;
	public string last;
	public Material Green;
	public Material Red;
	public List<Letter> LastLetters = new List<Letter>();
	public int score = 0;
	public List<string> AllWords = new List<string>();
	public int time = 65;
	public bool started = false;
	public bool GameOver = false;
	// Use this for initialization
	void Start () {
		
	}

	void AStart(){
		InvokeRepeating ("Raindrop", .5f, .5f);
		InvokeRepeating ("Timer", 1f, 1f);
	}
	// Update is called once per frame
	void FixedUpdate () {

		if (started == false) {
			if (Input.GetMouseButtonDown (0)) {
				AStart ();
				Destroy (GameObject.Find ("StartText"));
				started = true;
			}
		}
		if (started == true) {
			if (Input.GetMouseButton (0) && GameOver == false) {
			
				if (Drawing == false) {
					GameObject line = Instantiate (Line) as GameObject;
				}
				Drawing = true;

				if (GetMousePosition () != LastPos) {
					GameObject CurrentLine = Instantiate (LinePiece) as GameObject;
					LinePiece lp = CurrentLine.GetComponent<LinePiece> ();
					lp.inuse = true;
					//if its not the first line segment

					if (LastPosMatters == true) {
						if (CurrentLine != null && PreviousLine != false) {
							CurrentLine.transform.position = ((GetMousePosition () + LastPos) / 2);
							CurrentLine.transform.localScale = new Vector3 (Vector3.Distance (CurrentLine.transform.position, LastPos) + (PreviousLine.transform.localScale.x / 2), .15f, .1f);
						}
					}
				//if its the first line segment
				else if (LastPosMatters == false) {
						CurrentLine.transform.position = (GetMousePosition ());
						CurrentLine.transform.localScale = new Vector3 (.15f, .1f, .1f);
					}

			
					if (LastPosMatters == true) {
						CurrentLine.transform.Rotate (GetRot (LastPos, CurrentLine.transform.position));
					}
					LastPos = CurrentLine.transform.position;
					try {
						CurrentLine.transform.SetParent (GameObject.Find ("Line(Clone)").transform);
					} catch {
					}
					PreviousLine = CurrentLine;
					lp.inuse = false;
				}
				LastPosMatters = true;

			} else if (!Input.GetMouseButtonDown (0)) {
				if (Drawing == true) {
					LastPosMatters = false;
					Drawing = false;
					last = current;

					//compare string
					GameObject dicO = GameObject.Find ("Dictionary");
					Dictionary dic = dicO.GetComponent<Dictionary> ();

					//if it is real
					if (dic.RealWord (current)) {
						score = score + GetScore (current);
						AllWords.Add (current);
						GameObject.Find ("ScoreText").GetComponent<Text> ().text = "Score: " + score;
						foreach (Letter l in LastLetters) {
							l.GetComponent<Renderer> ().material = Green;
							l.finish (true);
						}
					}
				//if not real word
				else {
						foreach (Letter l in LastLetters) {
							l.GetComponent<Renderer> ().material = Red;
							l.finish (false);
						}
					}
					current = "";
					LastLetters.Clear ();
					Destroy (GameObject.Find ("Line(Clone)"));
				}
			}
			GameObject.Find ("CurrentString").GetComponent<Text> ().text = current;

			if (time <= 0 && GameOver == false) {
				EndGame ();
			}
		}
	}

	public void Raindrop(){
		GameObject NewLetter = Instantiate (Letter) as GameObject;
		float x = Random.Range (-2.5f, 2.5f);
		NewLetter.transform.position = new Vector3 (x, 5f, -1f);
	}

	public Vector3 GetMousePosition(){
		Vector3 WorldPos = Input.mousePosition;
		WorldPos.z = 10;
		Vector3 ScreenMousePos = Camera.main.ScreenToWorldPoint (WorldPos);
		return ScreenMousePos;
	}

	Vector3 GetRot(Vector3 Last, Vector3 Current){
		Vector3 dir = Current - Last;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		return(new Vector3(0f, 0f, angle));
	}

	float DistanceBetween(Vector3 last, Vector3 now){
		float val = Mathf.Sqrt (Mathf.Abs (last.x - now.x) + Mathf.Abs (last.y - now.y));
		return val;
	}

	public void AddToCurrent(char a){
		current = current + a;
	}

	public int GetScore(string a){
		int points = 0;
		foreach (char b in a) {
			switch (b) {
			case 'A':
				points = points + 1;
				break;
			case 'B':
				points = points + 3;
				break;
			case 'C':
				points = points + 3;
				break;
			case 'D':
				points = points + 2;
				break;
			case 'E':
				points = points + 1;
				break;
			case 'F':
				points = points + 4;
				break;
			case 'G':
				points = points + 2;
				break;
			case 'H':
				points = points + 4;
				break;
			case 'I':
				points = points + 1;
				break;
			case 'J':
				points = points + 8;
				break;
			case 'K':
				points = points + 5;
				break;
			case 'L':
				points = points + 1;
				break;
			case 'M':
				points = points + 3;
				break;
			case 'N':
				points = points + 1;
				break;
			case 'O':
				points = points + 1;
				break;
			case 'P':
				points = points + 3;
				break;
			case 'Q':
				points = points + 10;
				break;
			case 'R':
				points = points + 1;
				break;
			case 'S':
				points = points + 1;
				break;
			case 'T':
				points = points + 1;
				break;
			case 'U':
				points = points + 1;
				break;
			case 'V':
				points = points + 4;
				break;
			case 'W':
				points = points + 4;
				break;
			case 'X':
				points = points + 12;
				break;
			case 'Y':
				points = points + 4;
				break;
			case 'Z':
				points = points + 10;
				break;
			default:
				break;
			}

		}
		int multiplyer = 1;
		int count = a.Length;
		multiplyer = (count / 3) + 1;
		return points * multiplyer;
	}

	public void Timer(){
		time--;
		GameObject.Find ("TimeText").GetComponent<Text> ().text = "Time: " + time;
	}

	public void EndGame(){
		GameOver = true;
		int high = PlayerPrefs.GetInt ("RainHighScore");
		if (score > high) {
			PlayerPrefs.SetInt ("RainHighScore", score);
		}
		CancelInvoke ();
		GameObject endpanel = Instantiate (Endpanel) as GameObject;
		endpanel.transform.SetParent (GameObject.Find ("Canvas").transform, false);
		string finalwords = "";
		foreach (string s in AllWords) {
			int points = GetScore (s);
			finalwords = finalwords + " " + s + "(" + points.ToString() + "), ";
		}
		GameObject.Find ("FinalWords").GetComponent<Text> ().text = finalwords;
	}

}
