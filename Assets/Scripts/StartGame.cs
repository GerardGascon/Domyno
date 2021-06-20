using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour{

	[SerializeField] float force = 5f, forceOffset = .5f;
	Rigidbody rb;

	bool started;
	
	// Start is called before the first frame update
	void Awake(){
		rb = GetComponent<Rigidbody>();
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.P))
			Play();
	}

	public void Play(){
		if (started) return;
		rb.AddForceAtPosition(transform.forward * force, transform.position + transform.up * forceOffset, ForceMode.Impulse);
		FindObjectOfType<ObjectPlacer>().playing = true;
		started = true;
	}
}
