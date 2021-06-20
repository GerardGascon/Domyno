using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectPlacer : MonoBehaviour{

	[SerializeField] LayerMask groundMask;
	[SerializeField] LayerMask obstacleMask;
	[SerializeField] LayerMask pieceMask;
	[SerializeField] Camera cam;

	[SerializeField] GameObject cube;
	PlacerObject placerObject;
	[SerializeField, Range(0f, 10f)] float rotationMultiplier = 5f;
	[SerializeField, Range(0f, 2f)] float pieceHeight = 1.5f;
	float cubeRotation;

	[SerializeField] GameObject piece;

	bool isMouseOverButton;

	[Space]
	[SerializeField] Canvas canvas;
	[SerializeField] TMP_Text remainingText;
	[SerializeField, Range(0f, 20f)] int piecesAvaiable;
	int piecesRemaining;

	public bool playing;
	public static bool canvasHidden;
	
	// Start is called before the first frame update
	void Start(){
		placerObject = cube.GetComponent<PlacerObject>();
		piecesRemaining = piecesAvaiable;
		if (canvasHidden)
			canvas.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update(){
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if(Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask) && !Physics.Raycast(ray, 100f, obstacleMask) && !isMouseOverButton && !playing){
			cube.SetActive(true);
			cube.transform.position = hit.point + Vector3.up * pieceHeight / 2;
		}else{
			cube.SetActive(false);
		}

		cubeRotation += Input.mouseScrollDelta.y * rotationMultiplier;
		cube.transform.rotation = Quaternion.Euler(0f, cubeRotation, 0f);

		if(Input.GetMouseButtonDown(1) && Physics.Raycast(ray, out RaycastHit pieceHit, 100f, pieceMask) && !Physics.Raycast(ray, 100f, obstacleMask) && !isMouseOverButton && !playing){
			Destroy(pieceHit.transform.gameObject);
			piecesRemaining++;
		}
		if (Input.GetMouseButtonDown(0) && piecesRemaining > 0 && placerObject.CanPlace && cube.activeSelf && !isMouseOverButton && !playing){
			piecesRemaining--;
			Instantiate(piece, cube.transform.position, Quaternion.Euler(0f, cubeRotation, 0f));
		}

		if (Input.GetKeyDown(KeyCode.Escape)){
			canvasHidden = !canvasHidden;
			canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
			isMouseOverButton = false;
		}

		remainingText.text = "LEFT " + piecesRemaining;

		if (Input.GetKeyDown(KeyCode.R)){
			Restart();
		}
	}

	public void Restart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void MouseOverUI(bool over){
		isMouseOverButton = over;
	}
}
