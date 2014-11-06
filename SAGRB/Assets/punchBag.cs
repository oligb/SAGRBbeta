using UnityEngine;
using System.Collections;

public class punchBag : MonoBehaviour {
	public Vector3 forceAdd=Vector3.zero;
	public GameObject robotguy;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.LookAt(robotguy.transform.position);
		float dist= Vector3.Distance(transform.position,robotguy.transform.position);
		rigidbody.AddForce (forceAdd*dist);
	}
}
