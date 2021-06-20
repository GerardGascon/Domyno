using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour{

	[SerializeField] ParticleSystem winParticles;
	bool won;

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("Piece") && !won){
			winParticles.Play();
			won = true;
			GameManager.instance.ButtonPressed();
		}
	}
}
