using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour {

	public Sprite A;
	public Sprite B;
	public Sprite C;
	public Sprite D;
	public Sprite E;
	public Sprite F;
	public Sprite G;
	public Sprite H;
	public Sprite I;
	public Sprite J;
	public Sprite K;
	public Sprite L;
	public Sprite M;
	public Sprite N;
	public Sprite O;
	public Sprite P;
	public Sprite Q;
	public Sprite R;
	public Sprite S;
	public Sprite T;
	public Sprite U;
	public Sprite V;
	public Sprite W;
	public Sprite X;
	public Sprite Y;
	public Sprite Z;

	public Material Yellow;
	public int id;
	public char value;
	public bool IsVowel = false;
	private int[] vowels = new int[]{0, 4, 8, 14, 20, 24};
	// Use this for initialization
	void Start () {
		id = Random.Range (0, 26);
		int v = Random.Range (0, 100);
		if (v > 70) {
			int wv = Random.Range (0, 6);
			id = vowels [wv];
		}
		SetSpriteAndValue ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move ();
		if (transform.position.y < -15f) {
			if (InCurrent() == false) {
				Destroy (gameObject);
			}
		}
	}

	public void Move(){
		transform.Translate (0f, -.02f, 0f);
	}
		
	public void SetSpriteAndValue(){
		SpriteRenderer rend = GetComponent<SpriteRenderer> ();
		switch (id) {
		case 0:
			rend.sprite = A;
			value = 'A';
			IsVowel = true;
			break;
		case 1:
			rend.sprite = B;
			value = 'B';
			break;
		case 2:
			rend.sprite = C;
			value = 'C';
			break;
		case 3:
			rend.sprite = D;
			value = 'D';
			break;
		case 4:
			rend.sprite = E;
			value = 'E';
			IsVowel = true;
			break;
		case 5:
			rend.sprite = F;
			value = 'F';
			break;
		case 6:
			rend.sprite = G;
			value = 'G';
			break;
		case 7:
			rend.sprite = H;
			value = 'H';
			break;
		case 8:
			rend.sprite = I;
			value = 'I';
			IsVowel = true;
			break;
		case 9:
			rend.sprite = J;
			value = 'J';
			break;
		case 10:
			rend.sprite = K;
			value = 'K';
			break;
		case 11:
			rend.sprite = L;
			value = 'L';
			break;
		case 12:
			rend.sprite = M;
			value = 'M';
			break;
		case 13:
			rend.sprite = N;
			value = 'N';
			break;
		case 14:
			rend.sprite = O;
			value = 'O';
			IsVowel = true;
			break;
		case 15:
			rend.sprite = P;
			value = 'P';
			break;
		case 16:
			rend.sprite = Q;
			value = 'Q';
			break;
		case 17:
			rend.sprite = R;
			value = 'R';
			break;
		case 18:
			rend.sprite = S;
			value = 'S';
			break;
		case 19:
			rend.sprite = T;
			value = 'T';
			break;
		case 20:
			rend.sprite = U;
			value = 'U';
			IsVowel = true;
			break;
		case 21:
			rend.sprite = V;
			value = 'V';
			break;
		case 22:
			rend.sprite = W;
			value = 'W';
			break;
		case 23:
			rend.sprite = X;
			value = 'X';
			break;
		case 24:
			rend.sprite = Y;
			value = 'Y';
			IsVowel = true;
			break;
		case 25:
			rend.sprite = Z;
			value = 'Z';
			break;
		default:
			rend.sprite = A;
			value = 'A';
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		GameObject collO = coll.gameObject;
		LinePiece line = collO.GetComponent<LinePiece> ();
		//Debug.Log ("Collided with line");
		if (line != null) {
			GameObject.Find ("Rain").GetComponent<Rain> ().AddToCurrent (value);
			GameObject.Find ("Rain").GetComponent<Rain> ().LastLetters.Add (this);
			GetComponent<Renderer> ().material = Yellow;
			Destroy (GetComponent<BoxCollider2D> ());
		}

	}

	public void finish(bool real){
		if (real == true) {
			GetComponent<Renderer> ().material = GameObject.Find ("Rain").GetComponent<Rain> ().Green;
		} else {
			GetComponent<Renderer> ().material = GameObject.Find ("Rain").GetComponent<Rain> ().Red;
		}

			StartCoroutine (WaitFinish());
	}

	public IEnumerator WaitFinish(){
		yield return new WaitForSeconds (1);
		Destroy (gameObject);
	}

	public bool InCurrent(){
		bool inn = false;
		List<Letter> last = GameObject.Find ("Rain").GetComponent<Rain> ().LastLetters;
		foreach (Letter l in last) {
			if (l == this) {
				inn = true;
			}
		}
		return inn;
	}
}
