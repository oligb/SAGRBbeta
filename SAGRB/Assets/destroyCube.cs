using UnityEngine;
using System.Collections;

public class destroyCube : MonoBehaviour {
	public float startTime;
	public float lifeTime=1000f;

	// Use this for initialization
	void Start () {
		startTime=Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	if(Time.time-startTime>lifeTime)
			Destroy(gameObject);
	}
}
