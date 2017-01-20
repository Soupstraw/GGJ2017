using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizScript : MonoBehaviour {

	public List<Question> questions;

	public Question currentQuestion;

	public GameObject combatCanvas;
	public Button buttonA, buttonB, buttonC, buttonD;
	public Text questionText;

	// Use this for initialization
	void Start () {
		questions = new List<Question> ();
		questions.Add (new Question ("Mis värvi on armastus?",
			"Sinine","Kes seda teab...","#FF0000","Kartulivärvi", 1));
		questions.Add (new Question ("USA president on...",
			"Brobama","Cave Johnson","Clinton","Drumpf", 3));
		questions.Add (new Question ("Kui 5 küünalt põlevad 5 minutit, siis 10 küünalt põlevad mitu minutit?",
			"5 minutit","7.5 minutit","10 minutit","12.5 minutit", 0));
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CombatStart(){
		currentQuestion = questions [Random.Range (0, questions.Count)];
		Debug.Log (currentQuestion);
		buttonA.GetComponentInChildren<Text> ().text = currentQuestion.answerA;
		buttonB.GetComponentInChildren<Text> ().text = currentQuestion.answerB;
		buttonC.GetComponentInChildren<Text> ().text = currentQuestion.answerC;
		buttonD.GetComponentInChildren<Text> ().text = currentQuestion.answerD;
		questionText.text = currentQuestion.question;
	}

	public void SelectAnswer(int answer){
		if (currentQuestion.correctAnswer == answer) {
			Debug.Log ("Correct answer (" + answer + ")!");
		} else {
			Debug.Log ("False answer (" + answer + ")! Correct answer was "+currentQuestion.correctAnswer+".");
		}
	}
}
