using UnityEngine;
using System.Collections;

public class Elastic : MonoBehaviour {

	private float OriginalX;
	private float OriginalY;
	private GameObject CurrentOb;
	private AnimalSlingshot game;

	// Use this for initialization
	void Start () {
		game = FindObjectOfType (typeof(AnimalSlingshot)) as AnimalSlingshot;
		OriginalX = transform.position.x;
		OriginalY = transform.position.y;
		if (game.GetLoaded() != null) {
			CurrentOb = game.GetLoaded ();
		}
		Reset ();
	}
	
	// Update is called once per frame
	void Update () {
		if(game.GetLoaded()!=false){
			CurrentOb = game.GetLoaded ();
			stretch (CurrentOb.transform.position.x, CurrentOb.transform.position.y);
		}
	}

	public void SetCurrentOb(GameObject a){
		CurrentOb = a;
	}

	public void stretch(float a, float b){

		//distance between the side of slingshot and the current ball position
		float OriginToBulletX = OriginalX - a;
		float OriginToBulletY = OriginalY - b;

		//Angle between side of slingshot and current ball position 
		float NewAngle = Mathf.Atan2 (OriginToBulletY, OriginToBulletX);
		NewAngle = (NewAngle * Mathf.Rad2Deg) + 90f;

		//this section deals with the size of the elastic
		float diffx = absolute(OriginToBulletX);
		float diffy = absolute(OriginToBulletY);
		float distance = Mathf.Sqrt ((diffx * diffx) + (diffy + diffy));
		float MaxDist = distance;
		//if(MaxDist>2f){MaxDist = 2f;}
		this.transform.localScale = new Vector3 (.1f, MaxDist, 0);

		//the elastic moves in order to appear to be growing from a side point instead of the middle out
		float XDirection = (MaxDist/2) * (Mathf.Cos(NewAngle * Mathf.Deg2Rad *-1));
		float YDirection = (MaxDist/2) * (Mathf.Sin(NewAngle * Mathf.Deg2Rad *-1));
		this.transform.position = new Vector3 (OriginalX + YDirection, OriginalY + XDirection, 0f);

		//this section deals with the rotation of the elastic
		this.transform.localRotation = Quaternion.Euler (0f, 0f, NewAngle);
	}

	float absolute(float a){
		if (a<0){
			a = a * -1;
		}
		return a;
	}

	public float avg(float a, float b){
		float sum = a + b;
		float mean = sum / 2;
		return mean;
	}

	public void Reset(){
		Transform Sling = transform.parent;
		stretch (Sling.position.x, Sling.position.y-.3f);
		GameObject shot = GameObject.Find ("Shot");
		shot.transform.position = new Vector2 (Sling.position.x, Sling.position.y - .3f);
	}




}
