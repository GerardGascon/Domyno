using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerObject : MonoBehaviour{

	bool canPlace = true;
	public bool CanPlace { get { return canPlace; } }

	[SerializeField] Color canPlaceColor, cannotPlaceColor;

	Material material;

	void Start(){
		material = GetComponent<MeshRenderer>().material;
	}

	void OnTriggerStay(Collider other){
		if (other.CompareTag("Piece") || other.CompareTag("Button") || other.CompareTag("Obstacle")){
			canPlace = false;
			material.color = cannotPlaceColor;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag("Piece") || other.CompareTag("Button") || other.CompareTag("Obstacle")){
			canPlace = true;
			material.color = canPlaceColor;
		}
	}
}
