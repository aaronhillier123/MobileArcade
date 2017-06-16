using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {


		
	}
	
	// Update is called once per frame
	void Update () {
		SpriteRenderer sr = GetComponent<SpriteRenderer>();

		float worldScreenHeight = Camera.main.orthographicSize * 2;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3(
			(worldScreenWidth / sr.sprite.bounds.size.x),
			(worldScreenHeight / sr.sprite.bounds.size.y)+.01f, 1);
	}
}
