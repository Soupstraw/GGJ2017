using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizScript : MonoBehaviour {

	//AKA COMBAT SCRIPT

	//AKA SHITCODE SCRIPT

	//by Daniel Kütt 21.01.2017

	public List<Question> questions;

	public Question currentQuestion;

	public GameObject combatCanvas;
	public Button buttonA, buttonB, buttonC, buttonD;
	public Text questionText, enemyText, playerText;

	public AICombatTrigger aiTrigger;

	private int enemyHealth;
	private int playerHealth;
	private int lastQuestion;
	private ColorBlock normalColors;
	private Color32 pinkColor;
	private Color32 greenColor;
	private List<Question> theQuestions;

	public float timer;
	public float timerMax = 10f;

	public bool timerRunning = false;

	public RectTransform timerBar;

	// Use this for initialization
	void Start () {
		normalColors = buttonA.colors;
		pinkColor = new Color32 (227, 42, 106, 255);
		greenColor = new Color32 (122, 176, 67, 255);
		setButtonsClickable (true);
		questions = new List<Question> ();
		questions.Add (new Question ("Mis värvi on armastus?",
			"Sinine","Kes seda teab...","#FF0000","Kartulivärvi", 1));
		questions.Add (new Question ("USA president on...",
			"Brobama","Cave Johnson","Clinton","Drumpf", 3));
		questions.Add (new Question ("Kui 5 küünalt põlevad 5 minutit, siis 10 küünalt põlevad mitu minutit?",
			"5 minutit","7.5 minutit","10 minutit","12.5 minutit", 0));
		questions.Add (new Question ("Küsimus",
			"Õigem vastus","Valem vastus","NullPointerException","Vastus", 3));
		questions.Add (new Question ("How much wood would a woodchuck if a woodchuck could chuck wood",
			"Palun EI","2 tihu","7 tihu","Ma ei tea", 3));
		questions.Add (new Question ("Desoksüribonukleeinhappe paarid",
			"A-G ja C-T","A-C ja G-T","A-T ja C-G","A-G ja T-C", 2));
		questions.Add (new Question ("Palju maksab 1 eurone leib",
			"1 euro + taara","1 euro - taara","1 euro","taara", 2));	
	}

	void OnEnable(){
		ItemScript.OnItemUse += DoItemEffect;
	}

	void OnDisable(){
		ItemScript.OnItemUse -= DoItemEffect;
	}
	
	// Update is called once per frame
	void Update () {
		if (timerRunning) {
			timer -= Time.deltaTime;
			timerBar.localScale = new Vector3 (timer / timerMax, 1, 1);
		}
	}

	public void CombatStart(){
		Debug.Log (aiTrigger.GetComponentInChildren<MonsterAttributes> ().health);
		lastQuestion = -1;
		playerHealth = GameObject.FindWithTag ("Player").GetComponent<PlayerAttributes> ().health;; 
		enemyHealth = aiTrigger.GetComponentInChildren<MonsterAttributes> ().health;
		theQuestions = generateQuestionArray ();
		combatCanvas.transform.FindChild ("EnemyHP").GetComponent<Text> ();
		newQuestion ();
		GameObject.Find ("GameLogic").GetComponent<InventoryScript> ().ResetItems ();
	}
		
	public List<Question> generateQuestionArray(){
		List<Question> questionsCopy = new List<Question>();
		for (int i = 0; i < questions.Count; i++){
			questionsCopy.Add (questions [i]);
			//Debug.Log (questions [i].question);
		}
		//Debug.Log ("questionscopy filled " + questionsCopy.Count);
		List<Question> questionsList = new List<Question> ();

		//TODO replace 2*enemyHealth with actual condition that wouldnt break.	
		for (int i = 0; i < (playerHealth + enemyHealth); i++) { 
			int randomIndex = Random.Range (0, questionsCopy.Count);
			//Debug.Log ("rnd "+randomIndex);
			//Debug.Log ("cnt "+questionsCopy.Count);
			Question thatWillBeAdded = questionsCopy [randomIndex];
			questionsList.Add (thatWillBeAdded);
			questionsCopy.Remove (thatWillBeAdded);
		}
		return questionsList;
	}

	public void DoItemEffect(ItemScript item){
		switch (item.itemType) {
		case ItemType.APPLE:
			timer = Mathf.Clamp(timer + 5f, 0, 10f);
			break;
		case ItemType.PEN:
			int ans;
			do {
				ans = Random.Range (0, 3);
			} while(ans == currentQuestion.correctAnswer);

			Button button = numberToButton (ans);
			ColorBlock cb = button.colors;
			cb.disabledColor = Color.black;
			button.colors = cb;
			button.interactable = false;
			break;
		case ItemType.BOOK:
			newQuestion ();
			break;
		}
	}

	public void FleeCombat(){
		aiTrigger.FleeCombat ();
	}

	public void newQuestion(){
		timer = timerMax;
		timerRunning = true;
		lastQuestion++;
		//Debug.Log("HEALTH: "+enemyHealth);	
		//Text t = combatCanvas.transform.FindChild ("EnemyHP").GetComponent<Text> ();
		//t.text = health.ToString();
		enemyText.text = enemyHealth.ToString();
		playerText.text = playerHealth.ToString ();

		//Debug.Log ("lastQ " + lastQuestion);
		//Debug.Log ("curQCount " + theQuestions.Count);

		currentQuestion = theQuestions [lastQuestion];
		//Debug.Log (currentQuestion);
		buttonA.GetComponentInChildren<Text> ().text = currentQuestion.answerA;
		buttonB.GetComponentInChildren<Text> ().text = currentQuestion.answerB;
		buttonC.GetComponentInChildren<Text> ().text = currentQuestion.answerC;
		buttonD.GetComponentInChildren<Text> ().text = currentQuestion.answerD;
		questionText.text = currentQuestion.question;
	}

	public void SelectAnswer(int answer){
		timerRunning = false;
		StartCoroutine (CO_DisableButtons(currentQuestion.correctAnswer == answer && enemyHealth == 1, currentQuestion.correctAnswer != answer && playerHealth == 1));
		if (currentQuestion.correctAnswer == answer) {
			Debug.Log ("Correct answer (" + answer + ")!");
			colorButton (currentQuestion.correctAnswer, greenColor);
			enemyHealth--;	
			if (enemyHealth == 0) {
				GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Animator> ().Play ("Win");
				aiTrigger.GetComponentInChildren<Animator> ().SetTrigger ("Lose");
			} else {
				GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Animator> ().Play ("Attack");
				GameObject.FindGameObjectWithTag ("Player").GetComponent<ParticleSystem> ().Play ();
				StartCoroutine (aiTrigger.CO_ShakeAndGlow ());
			}
		} else {
			colorButton (currentQuestion.correctAnswer, greenColor);
			colorButton (answer, pinkColor);
			Debug.Log ("False answer (" + answer + ")! Correct answer was "+currentQuestion.correctAnswer+".");
			playerHealth--;
			GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Animator> ().Play ("Lose");
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


	IEnumerator CO_DisableButtons(bool lose, bool win){
		setButtonsClickable (false);
		float i = 0.0f;
		while(i < 2.4f)
		{
			i += Time.deltaTime;
			yield return null;
		}
		setButtonsClickable (true);
		if (lose) {
			aiTrigger.FleeCombat ();
		} else if (win) {
			aiTrigger.FleeCombat ();
		} else {
			newQuestion ();
		}
	}
}
