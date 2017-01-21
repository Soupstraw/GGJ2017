using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizScript : MonoBehaviour {

	public List<Question> questions;

	public Question currentQuestion;

	public GameObject combatCanvas;
	public Button buttonA, buttonB, buttonC, buttonD;
	public Text questionText, enemyText;

	public AICombatTrigger aiTrigger;

	private int health;
	private int lastQuestion;
	private ColorBlock normalColors;
	private Color32 pinkColor;
	private Color32 greenColor;


	// Use this for initialization
	void Start () {
		normalColors = buttonA.colors;
		pinkColor = new Color32 (227, 42, 106, 255);
		greenColor = new Color32 (122, 176, 67, 255);
		setButtonsClickable (true);
		lastQuestion = -1;
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
		health = 3;
		combatCanvas.transform.FindChild ("EnemyHP").GetComponent<Text> ();
		newQuestion ();
	}

	public void newQuestion(){
		Debug.Log("HEALTH: "+health);
		//Text t = combatCanvas.transform.FindChild ("EnemyHP").GetComponent<Text> ();
		//t.text = health.ToString();
		enemyText.text = health.ToString();

		int counter = 0; //just in case we have extremely bad/good luck
		while (true) {
			counter++;
			int randomInt = Random.Range (0, questions.Count);
			if (lastQuestion == -1) {
				lastQuestion = randomInt;
				break;
			} else if (randomInt != lastQuestion) {
				lastQuestion = randomInt;
				break;
			} else if (questions.Capacity < 2) {
				break;
			} else if (counter > 20) {
				break;
			}	
		}
		Debug.Log (counter);
		currentQuestion = questions [lastQuestion];
		//Debug.Log (currentQuestion);
		buttonA.GetComponentInChildren<Text> ().text = currentQuestion.answerA;
		buttonB.GetComponentInChildren<Text> ().text = currentQuestion.answerB;
		buttonC.GetComponentInChildren<Text> ().text = currentQuestion.answerC;
		buttonD.GetComponentInChildren<Text> ().text = currentQuestion.answerD;
		questionText.text = currentQuestion.question;
	}

	public void SelectAnswer(int answer){
		StartCoroutine (CO_DisableButtons(currentQuestion.correctAnswer == answer && health == 1));
		if (currentQuestion.correctAnswer == answer) {
			Debug.Log ("Correct answer (" + answer + ")!");
			colorButton (currentQuestion.correctAnswer, greenColor);
			health--;	
			if (health == 0) {
				Debug.Log ("U win the fight, do somit here plox");
			}
		} else {
			colorButton (currentQuestion.correctAnswer, greenColor);
			colorButton (answer, pinkColor);
			Debug.Log ("False answer (" + answer + ")! Correct answer was "+currentQuestion.correctAnswer+".");
		}	
	}

	public void colorButton(int buttonNumber, Color32 color){
		Button button = numberToButton (buttonNumber);
		ColorBlock cb = button.colors;
		cb.disabledColor = color;
		button.colors = cb;
	}

	public void setButtonsClickable(bool input){
		buttonA.colors = normalColors;
		buttonB.colors = normalColors;
		buttonC.colors = normalColors;
		buttonD.colors = normalColors;

		buttonA.interactable = input;
		buttonB.interactable = input;
		buttonC.interactable = input;
		buttonD.interactable = input;
	}

	public Button numberToButton(int number){
		switch (number) {
		case 0:
			return buttonA;
		case 1:
			return buttonB;
		case 2:
			return buttonC;
		case 3:
			return buttonD;
		default:
			Debug.Log ("invalid number, couldnt get button");
			return null;
		}
	}


	IEnumerator CO_DisableButtons(bool input){
		setButtonsClickable (false);
		float i = 0.0f;
		while(i < 2.4f)
		{
			i += Time.deltaTime;
			yield return null;
		}
		setButtonsClickable (true);
		if (input) {
			aiTrigger.FleeCombat ();
		} else {
			newQuestion ();
		}
	}
}
