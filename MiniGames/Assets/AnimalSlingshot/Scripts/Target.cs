using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public int animal;
	public int level;
	public int direction;
	public SpriteRenderer rend;
	public Sprite Cat;
	public Sprite Dog;
	public Sprite Pig;
	public Sprite Bear;
	private Vector3 pos;
	private AnimalSlingshot game;
	private float speed;
	private float rate;
	public int id = 0;

	// Use this for initialization
	void Start () {
		speed = PlayerPrefs.GetFloat ("Speed");
		//Testing purposes
		speed = .03f; 


		rend = gameObject.GetComponent<SpriteRenderer> ();
		gameObject.AddComponent<PolygonCollider2D> ();
		gameObject.GetComponent<PolygonCollider2D> ().isTrigger = true;
		game = FindObjectOfType (typeof(AnimalSlingshot)) as AnimalSlingshot;
	}

	// Update is called once per frame
	void FixedUpdate () {
		move ();
	}

	public void SetShape(int a){
		animal = a;
		rend = gameObject.GetComponent<SpriteRenderer> ();
		if (rend != null) {
			if (animal == 0) {
				rend.sprite = Cat;
			} else if (animal == 1) {
				rend.sprite = Dog;
			} else if (animal == 2) {
				rend.sprite = Pig;
			} else if (animal == 3) {
				rend.sprite = Bear;
			} else {
			}
		} else {
		}
	}

	public void SetLevel(int a){
		

	}

	public void SetDirection(int a, int b){
		direction = a;
		level = b;
		Vector3 pos = transform.position;
		if (level == 0) {
			transform.position = new Vector3(pos.x, 4.5f);
		}
		else if (level == 1) {
			transform.position = new Vector2(pos.x, 3.75f);
		}
		else if (level == 2) {
			transform.position = new Vector2(pos.x, 3f);
		}
		else if (level == 3) {
			transform.position = new Vector2(pos.x, 2.25f);
		}

		pos = transform.position;
		if (direction == 0) {
			gameObject.transform.position = (new Vector3(4.5f, pos.y));
		} else if (direction == 1) {
			gameObject.transform.position = (new Vector3 (-4.5f, pos.y));
		} else {
		}
	}

	void move(){
		Vector3 curpos = transform.position;
		if (direction == 0) {
			transform.position = new Vector3 (curpos.x - speed, curpos.y);
		}
		if (direction == 1) {
			transform.position = new Vector3(curpos.x + speed, curpos.y);
		}
		if (transform.position.x > 10f || transform.position.x < -10f) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		Bullet bull = coll.gameObject.GetComponent<Bullet> () as Bullet;
		Debug.Log ("COLLISION DETECTED");
		if (bull != null) {
			if (bull.animal == animal) {
				Destroy (bull.gameObject);
				Destroy (gameObject);
				game.GoodHit (animal, id);
			} else {
				Destroy (bull.gameObject);
				Destroy (gameObject);
				game.BadHit (animal);
			}
		} else {
		}
	}
}
