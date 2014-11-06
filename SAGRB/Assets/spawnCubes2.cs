using UnityEngine;
using System.Collections;

public class spawnCubes2 : MonoBehaviour {
	public GameObject cube;
	// Use this for initialization
	void Start () {
		InvokeRepeating("spawnem", 1f,.5f);
	}
	void spawnem(){
		Instantiate(cube,new Vector3(Random.Range(1f,7f),2f,Random.Range (1f,2f)),Quaternion.identity);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
