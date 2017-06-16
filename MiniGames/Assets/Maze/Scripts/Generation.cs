using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generation : MonoBehaviour {

	public GameObject VWall;
	public GameObject HWall;
	public GameObject IWall;
	public GameObject Room;
	public GameObject Sphere;
	public int height = 10;
	public int width = 10;
	public int depth = 3;
	public int level = 1;
	private int count = 0;
	private int time = 0;
	public GameObject EndPanel;
	public bool GameOver = false;
	public bool started = false;
	public List<List<List<Room>>> Rooms = new List<List<List<Room>>>();
	public List<Room> AllRooms = new List<Room> ();
	//public List<HWall> DeletableHWalls = new List<HWall> ();
	//public List<VWall> DeletableVWalls = new List<VWall> ();
	//public List<IWall> DeletableIWalls = new List<IWall> ();


	public Material Foward;
	public Material Backward;
	public Material Both;
	public Material Black;
	public Material Green;
	// Use this for initialization
	void Start () {
		
		CreateAllRooms ();
		AssignRooms ();
		CreateAllWalls ();
		RecursiveBacktracking ();
		AdjustPositioning ();

		DeleteSomeWalls ();
		CreateSphere ();



		//ImplementDepth ();
	}

	void AStart(){
		started = true;
		ColorThem ();
		Destroy (GameObject.Find ("StartText"));
		InvokeRepeating ("Timer", 1f, 1f);
		Rooms [0] [width - 1] [height - 1].GetComponent<Renderer> ().material = Green;
		Rooms [0] [width - 1] [height - 1].final = true;
	}
	// Update is called once per frame
	void Update () {
		if (started == false) {
			if (Input.GetMouseButtonDown (0)) {
				AStart ();
			}
		}
	}

	public void AdjustPositioning (){
		GameObject.Find ("Maze").transform.position = new Vector3 (-8.4f, 5f, 0f);
		GameObject.Find ("PlaneTwo").transform.localPosition = new Vector3 (20f, 0f, 0f);
		GameObject.Find ("PlaneThree").transform.localPosition = new Vector3 (40f, 0f, 2f);
	}
		

	public void CreateSphere(){
		GameObject sphere = Instantiate (Sphere) as GameObject;
		float x = AllRooms [0].transform.position.x;
		float y = AllRooms [0].transform.position.y;
		sphere.transform.position = new Vector3(x, y, -5);
	}

	public void DeleteSomeWalls(){
		for(int k=0; k<depth; ++k){
			for(int i=1; i<width-1; ++i){
				for(int j=1; j<height-1; ++j){
					bool Del = false;
					Room current = Rooms [k] [i] [j];
					List<GameObject> walls = new List<GameObject> ();
					if (current.RightWall != null) {
						walls.Add (current.RightWall.gameObject);
					}
					if (current.LeftWall != null) {
						walls.Add (current.RightWall.gameObject);
					}
					if (current.UpWall != null) {
						walls.Add (current.RightWall.gameObject);
					}
					if (current.DownWall != null) {
						walls.Add (current.RightWall.gameObject);
					}
					int decide = Random.Range (0, 10);
					if (decide < 8 && decide > 5) {
						Del = true;
					}

					if (Del == true) {
						
							int count = walls.Count;
							if (count != 0) {
								int rand = Random.Range (0, count);
							try{
								Destroy (walls [rand]);
								//Debug.Log("Destroyed Random Wall");
								}
							catch{
							}
						}
					}
				}
			}
		}
	}

	public void GoFoward(){
		Sphere sphere = FindObjectOfType (typeof(Sphere)) as Sphere;
		sphere.movable = false;
		if(level == 1){
			GameObject.Find ("PlaneOne").transform.localPosition = new Vector3 (-20f, 0f, 0f);
			GameObject.Find ("PlaneTwo").transform.localPosition = new Vector3 (0f, 0f, 0f);
			GameObject.Find ("PlaneThree").transform.localPosition = new Vector3 (20f, 0f, 0f);
			level = 2;

			}
		else if(level == 2){
			GameObject.Find ("PlaneOne").transform.localPosition = new Vector3 (-40f, 0f, 0f);
			GameObject.Find ("PlaneTwo").transform.localPosition = new Vector3 (-20f, 0f, 0f);
			GameObject.Find ("PlaneThree").transform.localPosition = new Vector3 (0f, 0f, 0f);
			level = 3;
		}
		GameObject.Find ("Stage").GetComponent<Text>().text = "Level: \n" + level.ToString ();
		sphere.movable = true;
	}

	public void GoBackward(){
		Sphere sphere = FindObjectOfType (typeof(Sphere)) as Sphere;
		sphere.movable = false;
		if(level == 2){
			GameObject.Find ("PlaneOne").transform.localPosition = new Vector3 (0f, 0f, 0f);
			GameObject.Find ("PlaneTwo").transform.localPosition = new Vector3 (20f, 0f, 0f);
			GameObject.Find ("PlaneThree").transform.localPosition = new Vector3 (40f, 0f, 0f);
			level = 1;
		}
		else if(level == 3){
			GameObject.Find ("PlaneOne").transform.localPosition = new Vector3 (-20f, 0f, 0f);
			GameObject.Find ("PlaneTwo").transform.localPosition = new Vector3 (0f, 0f, 0f);
			GameObject.Find ("PlaneThree").transform.localPosition = new Vector3 (20f, 0f, 0f);
			level = 2;
		}
		GameObject.Find ("Stage").GetComponent<Text>().text = "Level: \n" + level.ToString ();
		sphere.movable = true;
	}




	//instantiate a grid of rooms width x height
	public void CreateAllRooms(){
		for(int k = 0; k<depth; ++k){
			List<List<Room>> plane = new List<List<Room>> ();
			for (int i = 0; i < width; ++i) {
				List<Room> row = new List<Room> ();
				for (int j = 0; j < height; ++j) {
					GameObject roomO = Instantiate (Room) as GameObject;
					Room room = roomO.GetComponent<Room> ();
					room.transform.localPosition = new Vector3 (i * .68f, j * -.68f, 3*k);
					if (k == 0) {
						room.transform.SetParent (GameObject.Find ("PlaneOne").transform);
					} else if (k == 1) {
						room.transform.SetParent (GameObject.Find ("PlaneTwo").transform);
					} else {
						room.transform.SetParent (GameObject.Find ("PlaneThree").transform);
					}
						
					row.Add(room);
					AllRooms.Add (room);

				}
				plane.Add (row);
			}
			Rooms.Add (plane);
		}
	}

	//Assign each room its corresponding right, left, bottom, and top rooms
	public void AssignRooms(){
		for(int k=0; k<depth; ++k){
			for (int i=0; i<width; ++i){
				for (int j = 0; j < height; ++j) {

					//Left Edge
					if (i != 0) {
						Rooms [k] [i] [j].LeftRoom = Rooms [k] [i - 1] [j];
						Rooms [k] [i] [j].ConnectedRooms.Add (Rooms [k] [i - 1] [j]);
					}

					//Top Edge
					if (j != 0) {
						Rooms [k] [i] [j].UpRoom = Rooms [k] [i] [j - 1];
						Rooms [k] [i] [j].ConnectedRooms.Add (Rooms [k] [i] [j - 1]);
					}

					//Bottom Edge
					if (j != height - 1) {
						Rooms [k] [i] [j].DownRoom = Rooms [k] [i] [j + 1];
						Rooms [k] [i] [j].ConnectedRooms.Add (Rooms [k] [i] [j + 1]);
					}

					//Right Edge
					if (i != width - 1) {
						Rooms [k] [i] [j].RightRoom = Rooms [k] [i + 1] [j];
						Rooms [k] [i] [j].ConnectedRooms.Add (Rooms [k] [i + 1] [j]);
					}

					//Back Plane
					if (k != 0) {
						Rooms [k] [i] [j].BackwardRoom = Rooms [k - 1] [i] [j];
						Rooms [k] [i] [j].ConnectedRooms.Add(Rooms [k - 1] [i] [j]);
					}

					//FrontPlane
					if (k != depth - 1) {
						Rooms [k] [i] [j].FowardRoom = Rooms [k + 1] [i] [j];
						Rooms [k] [i] [j].ConnectedRooms.Add(Rooms [k + 1] [i] [j]);
					}
				}
			}
		}
	}

	//Create and assign all walls to the rooms that they block
	public void CreateAllWalls(){

		//for loop creates left and top walls for every room
		for (int k = 0; k < depth; ++k) {
			for (int i = 0; i < width; ++i) {
				for (int j = 0; j < height; ++j) {
					Room room = Rooms [k][i] [j];

					//Left Walls
					GameObject leftwall = Instantiate (VWall) as GameObject;
					float xl = room.transform.position.x - .325f;
					float yl = room.transform.position.y;
					leftwall.transform.position = new Vector3 (xl, yl, -2f);
					VWall lvwall = leftwall.GetComponent<VWall> ();
					room.LeftWall = lvwall;

					if (k == 0) {
						leftwall.transform.SetParent (GameObject.Find ("PlaneOne").transform);
					} else if (k == 1) {
						leftwall.transform.SetParent (GameObject.Find ("PlaneTwo").transform);
					} else {
						leftwall.transform.SetParent (GameObject.Find ("PlaneThree").transform);
					}
					//leftwall.transform.SetParent (GameObject.Find ("Maze").transform);

					//Top Walls
					GameObject topwall = Instantiate (HWall) as GameObject;
					float xt = room.transform.position.x;
					float yt = room.transform.position.y + .325f;
					//float zt = room.transform.position.z;
					topwall.transform.position = new Vector3 (xt, yt, -2f);
					HWall thwall = topwall.GetComponent<HWall> ();
					room.UpWall = thwall;

					if (k == 0) {
						topwall.transform.SetParent (GameObject.Find ("PlaneOne").transform);
					} else if (k == 1) {
						topwall.transform.SetParent (GameObject.Find ("PlaneTwo").transform);
					} else {
						topwall.transform.SetParent (GameObject.Find ("PlaneThree").transform);
					}

					//Foward Walls

						GameObject fowardwall = Instantiate (IWall) as GameObject;
						float xf = room.transform.position.x;
						float yf = room.transform.position.y;
						float zf = room.transform.position.z + 1f;
						fowardwall.transform.position = new Vector3 (xf, yf, zf);
						IWall fiwall = fowardwall.GetComponent<IWall> ();
						room.FowardWall = fiwall;

						if (k == 0) {
							fowardwall.transform.SetParent (GameObject.Find ("PlaneOne").transform);
						} else if (k == 1) {
							fowardwall.transform.SetParent (GameObject.Find ("PlaneTwo").transform);
						} else {
							fowardwall.transform.SetParent (GameObject.Find ("PlaneThree").transform);
						}
					

				}
			}
		}

			//for loop creates Right and bottom walls for the edges and assigns right and bottom walls to the others
		for(int k=0; k<depth; ++k){
			for (int i = 0; i < width; ++i) {
				for (int j = 0; j < height; ++j) {
					Room room = Rooms [k][i] [j];
						
					//Right Walls

					//right rooms
					if (room.RightRoom == null) {
						GameObject rightwall = Instantiate (VWall) as GameObject;
						float xr = room.transform.position.x + .325f;
						float yr = room.transform.position.y;
						rightwall.transform.position = new Vector3 (xr, yr, -2f);
						VWall rvwall = rightwall.GetComponent<VWall> ();
						room.RightWall = rvwall;

						if (k == 0) {
							rightwall.transform.SetParent (GameObject.Find ("PlaneOne").transform);
						} else if (k == 1) {
							rightwall.transform.SetParent (GameObject.Find ("PlaneTwo").transform);
						} else {
							rightwall.transform.SetParent (GameObject.Find ("PlaneThree").transform);
						}

						//rightwall.transform.SetParent(GameObject.Find("Maze").transform);
					}

					//other rooms
					if (room.RightRoom != null) {
						room.RightWall = room.RightRoom.LeftWall;
					}

					//Bottom Walls
						
					//bottom rooms
					if (room.DownRoom == null) {
						GameObject downwall = Instantiate (HWall) as GameObject;
						float xb = room.transform.position.x;
						float yb = room.transform.position.y - .325f;
						//float zb = room.transform.position.z;
						downwall.transform.position = new Vector3 (xb, yb, -2f);
						HWall bhwall = downwall.GetComponent<HWall> ();
						room.DownWall = bhwall;

						if (k == 0) {
							downwall.transform.SetParent (GameObject.Find ("PlaneOne").transform);
						} else if (k == 1) {
							downwall.transform.SetParent (GameObject.Find ("PlaneTwo").transform);
						} else {
							downwall.transform.SetParent (GameObject.Find ("PlaneThree").transform);
						}

						//downwall.transform.SetParent(GameObject.Find("Maze").transform);
					}

					//other rooms
					if (room.DownRoom != null) {
						room.DownWall = room.DownRoom.UpWall;
					}

					//Backward Walls
					if (room.BackwardRoom == null) {
						
							GameObject backwall = Instantiate (IWall) as GameObject;
							float xbw = room.transform.position.x;
							float ybw = room.transform.position.y;
							;
							float zbw = room.transform.position.z - 1;
							backwall.transform.position = new Vector3 (xbw, ybw, zbw);
							IWall bhwall = backwall.GetComponent<IWall> ();
							room.BackwardWall = bhwall;

							if (k == 0) {
								backwall.transform.SetParent (GameObject.Find ("PlaneOne").transform);
							} else if (k == 1) {
								backwall.transform.SetParent (GameObject.Find ("PlaneTwo").transform);
							} else {
								backwall.transform.SetParent (GameObject.Find ("PlaneThree").transform);
							}
						

						//downwall.transform.SetParent(GameObject.Find("Maze").transform);
					}

					if (room.BackwardRoom != null) {
						room.BackwardWall = room.BackwardRoom.FowardWall;
					}

				}
			}
		}
	}

	public void RecursiveBacktracking ()
	{
		List<Room> Trail = new List<Room> ();
		int remaining = 0;
		Room CurrentRoom = Rooms[0] [0] [0];
		Trail.Add (CurrentRoom);
		CurrentRoom.visited = true;

		//initial amount of rooms to traverse through
		foreach (Room v in AllRooms) {
			if (v.visited == false) {
				remaining++;
			}
		}

		//while there are still unexplored rooms
		while (remaining > 0) {

			//next room
			Room NextRoom;
			//list of adjacent rooms that have not been visited
			List<Room> CNVR = new List<Room> ();

			//initialize this list
			foreach (Room r in CurrentRoom.ConnectedRooms) {
				if (r.visited == false) {
					CNVR.Add (r);
				}
			}

			//if the list count is greater than 0
			if (CNVR.Count > 0) {
				//pick a random room to enter
				int NextRoomNumber = Random.Range (0, CNVR.Count);
				NextRoom = CNVR [NextRoomNumber];
				
				//add this room to the trail
				Trail.Add (NextRoom);

				//Take care of the wall
				if (NextRoom == CurrentRoom.UpRoom) {
					Destroy (CurrentRoom.UpWall.GetComponent<BoxCollider2D>());
					Destroy (CurrentRoom.UpWall.gameObject);
				} else if (NextRoom == CurrentRoom.DownRoom) {
					Destroy (CurrentRoom.DownWall.GetComponent<BoxCollider2D>());
					Destroy (CurrentRoom.DownWall.gameObject);
				} else if (NextRoom == CurrentRoom.RightRoom) {
					Destroy (CurrentRoom.RightWall.GetComponent<BoxCollider2D>());
					Destroy (CurrentRoom.RightWall.gameObject);
				} else if (NextRoom == CurrentRoom.LeftRoom) {
					Destroy (CurrentRoom.LeftWall.GetComponent<BoxCollider2D>());
					Destroy (CurrentRoom.LeftWall.gameObject);
				} else if (NextRoom == CurrentRoom.FowardRoom) {
						Destroy (CurrentRoom.FowardWall.gameObject);
				} else if (NextRoom == CurrentRoom.BackwardRoom) {
						Destroy (CurrentRoom.BackwardWall.gameObject);
				}
						
				NextRoom.visited = true;
				CurrentRoom = NextRoom;
				remaining--;
			} else if (CNVR.Count == 0) {
				Trail.RemoveAt (Trail.Count - 1);
				CurrentRoom = Trail [Trail.Count - 1];
			}
		}
	}

	public void ColorThem(){
		foreach (Room r in AllRooms) {
			if (r.BackwardWall == null) {
				r.gameObject.GetComponent<Renderer> ().material = Backward;
			}
			if (r.FowardWall == null) {
				r.gameObject.GetComponent<Renderer> ().material = Foward;
			}
			if (r.BackwardWall == null && r.FowardWall == null) {
				r.gameObject.GetComponent<Renderer> ().material = Both;
			}
			if (r.BackwardWall != null && r.FowardWall != null) {
				r.gameObject.GetComponent<Renderer>().material = Black;
			}
		}
	}

	public void EndGame(){
		if (GameOver == false) {
			CancelInvoke ();
			GameObject endpanel = Instantiate (EndPanel) as GameObject;
			endpanel.transform.SetParent (GameObject.Find ("Canvas").transform, false);

			int hightime = PlayerPrefs.GetInt ("MazeHighScore");
			if (hightime == 0) {
				PlayerPrefs.SetInt ("MazeHighScore", 2000);
			}
			hightime = PlayerPrefs.GetInt ("MazeHighScore");
			if (time < hightime) {
				PlayerPrefs.SetInt ("MazeHighScore", time);
			}
			GameOver = true;
		}
	}

	public void MoveRightYes(){
		if (GameOver == false) {
			Sphere sphere = FindObjectOfType (typeof(Sphere)) as Sphere;
			sphere.movingright = true;
		}
	}
	public void MoveLeftYes(){
		if (GameOver == false) {
			Sphere sphere = FindObjectOfType (typeof(Sphere)) as Sphere;
			sphere.movingleft = true;
		}
	}
	public void MoveDownYes(){
		if (GameOver == false) {
			Sphere sphere = FindObjectOfType (typeof(Sphere)) as Sphere;
			sphere.movingdown = true;
		}
	}
	public void MoveUpYes(){
		if (GameOver == false) {
			Sphere sphere = FindObjectOfType (typeof(Sphere)) as Sphere;
			sphere.movingup = true;
		}
	}
	public void MoveRightNo(){
		if (GameOver == false) {
			Sphere sphere = FindObjectOfType (typeof(Sphere)) as Sphere;
			sphere.movingright = false;
		}
	}
	public void MoveLeftNo(){
		if (GameOver == false) {
			Sphere sphere = FindObjectOfType (typeof(Sphere)) as Sphere;
			sphere.movingleft = false;
		}
	}
	public void MoveDownNo(){
		{
			Sphere sphere = FindObjectOfType (typeof(Sphere)) as Sphere;
			sphere.movingdown = false;
		}
	}
	public void MoveUpNo(){
		{
			Sphere sphere = FindObjectOfType (typeof(Sphere)) as Sphere;
			sphere.movingup = false;
		}
	}

	public void Timer(){
		++time;
		GameObject.Find ("Time").GetComponent<Text> ().text = "Time: \n" + time.ToString ();
	}
}
			

