using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {


	public bool motion = false;
	public int animal = 0;

	public SpriteRenderer rend = new SpriteRenderer();
	public Sprite Cat;
	public Sprite Dog;
	public Sprite Pig;
	public Sprite Bear;


	private float OriginalX;
	private float OriginalY;
	private float diffx;
	private float diffy;
	private Vector2 velocity;
	private float speed = .1f;
	private AnimalSlingshot game;
	// Use this for initialization
	void Start () {
		game = FindObjectOfType (typeof(AnimalSlingshot)) as AnimalSlingshot;
		OriginalX = transform.position.x;
		OriginalY = transform.position.y;
		SetSprite ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (motion == true) {
			move ();
		}
	}

	public void SetSprite(){
		if (rend != null) {
			if (animal == 0) {
				rend.sprite = Cat;
			} else if (animal == 1) {
				rend.sprite = Dog;
			} else if (animal == 2) {
				rend.sprite = Pig;
			} else if (animal == 3) {
				rend.sprite = Bear;
			}
		}
	}

	void OnMouseDown(){
	}

	void OnMouseDrag(){
		if (game.GameOver == false) {
			Vector3 CurrentPosition = getMousePosition ();
			transform.position = CurrentPosition;
			diffx = transform.position.x - OriginalX;
			diffy = transform.position.y - OriginalY;
			if (transform.position.y > 0) {
				transform.position = new Vector3 (transform.position.x, 0);
			}
		}
	}

	void OnMouseUp(){
		//this is when the bullet is released from the slingshot
		//here we destroy the big rectangular hit box and add a tight polygonal one
		Destroy(gameObject.GetComponent<BoxCollider2D>());
		gameObject.AddComponent<PolygonCollider2D> ();
		gameObject.GetComponent<PolygonCollider2D> ().isTrigger = true;

		//this routine causes the elastic to have a natual motion
		StartCoroutine ("Follow");

		//this sets the velocity for the new bullet based on how far it was pulled back
		velocity = new Vector2 (-1.5f * diffx, -1.5f * diffy);
		motion = true;
	}

	void move(){
		velocity.y = velocity.y - .05f;
		this.transform.Translate(velocity* speed);
		if (transform.position.y < -5) {
			Destroy (gameObject);
		}
	}


	Vector3 getMousePosition(){
		Vector3 WorldPos = Input.mousePosition;
		WorldPos.z = 10;
		Vector3 ScreenMousePos = Camera.main.ScreenToWorldPoint (WorldPos);
		return ScreenMousePos;
	}

	IEnumerator Follow(){
		yield return new WaitForSeconds (.1f);
		Elastic left = GameObject.Find ("ElasticRight").GetComponent<Elastic>();
		Elastic right = GameObject.Find ("ElasticLeft").GetComponent<Elastic>();
		left.Reset ();
		right.Reset ();
		game.SetLoaded (null);
	}
}
