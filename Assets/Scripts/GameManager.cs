using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

	[SerializeField, Range(1f, 6f)] int numberOfButtons = 1;
	int remainingButtons;
	[SerializeField] int nextScene;
	[SerializeField, Range(0f, 10f)] float transitionDelay = 2f;

	[SerializeField] Animator transition;
	
	public static GameManager instance;

	void Awake(){
		instance = this;
	}

	// Start is called before the first frame update
	void Start(){
		remainingButtons = numberOfButtons;
	}

	public void ButtonPressed(){
		remainingButtons--;
		if(remainingButtons == 0){
			StartCoroutine(Transition());
		}
	}

	IEnumerator Transition(){
		yield return new WaitForSeconds(transitionDelay);
		transition.SetTrigger("Transition");
		yield return new WaitForSeconds(.5f);
		SceneManager.LoadSceneAsync(nextScene);
	}
}
