using CookingPrototype.Controllers;
using System;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
	public static UIController Instance;

	public TMP_Text GoalText;

	void Awake() {
		if ( Instance != null ) {
			Debug.LogError("Another instance of GameplayController already exists");
		}
		Instance = this;
		Debug.Log(Instance.name);
	}

	public event Action GameStarted;

	public void StartGame() {
		GameStarted.Invoke();
	}
}
