using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadTimeTrial(){
		SceneManager.LoadScene ("TimeTrial");
	}

	public void LoadSurvival(){
		SceneManager.LoadScene ("Survival");
	}

	public void LoadMenu(){
		SceneManager.LoadScene ("GameBasic");
	}
}
