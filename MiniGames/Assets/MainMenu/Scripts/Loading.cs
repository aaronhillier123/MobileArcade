using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

	int count = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		++count;
		if (count > 50) {
			SceneManager.LoadScene (PlayerPrefs.GetString ("Loading"));
		}
	}
}
