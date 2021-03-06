﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizScript : MonoBehaviour {

	//AKA COMBAT SCRIPT

	//AKA SHITCODE SCRIPT

	//by Daniel Kütt 21.01.2017

	public List<Question> questions;

	public Question currentQuestion;

	public GameObject combatCanvas;
	public Button buttonA, buttonB, buttonC, buttonD;
	public Text questionText;

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
	public RectTransform enemyHPBar;
	public RectTransform playerHPBar;

	private int enemyMaxHealth;
	private int playerMaxHealth;

	private DigitalRuby.SoundManagerNamespace.SoundsManager soundsManager;

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
		questions.Add (new Question ("Anatidaephobia is the fear that somewhere in the world a ____ is watching you", "Wolf", "Duck", "Cow", "Pigeon", 1));
		questions.Add (new Question ("The inventor of Dynamite.", "Alfred Nobel", "Dmitri Mendeleev", "John Dalton", "Marie Curie", 0));
		questions.Add (new Question ("The Largest country on Earth", "Estonia", "USA", "Africa", "Russia", 3));
		questions.Add (new Question ("55/100 is", "Blasphemy", "Natural Number", "Rational Number", "Integer", 2));
		questions.Add (new Question ("What is this?", "This is blasphemy!", "This is madness!", "Madness?", "This is SPARTA", 3));
		questions.Add (new Question ("What is Earth’s natural satellite?", "There are none", "Another Earth", "Sun", "Moon", 3));
		questions.Add (new Question ("What shape does the Earth have?", "Plate", "Spheroid", "Cube", "Flat", 1));
		questions.Add (new Question ("Modern Human’s (Homo sapiens sapiens) ancestors, according to the theory of evolution, are:", "Chimps", "Owls", "Racoons", "Batmen", 0));
		questions.Add (new Question ("Water has this many states", "1", "2", "3", "4", 2));
		questions.Add (new Question ("First item ever listed on eBay", "Pencil", "Shotgun", "Broken laser pointer", "Sandvich", 2));
		questions.Add (new Question ("Average human body temperature(degrees Celsius) is: ", "30.3", "36.6", "35.1", "40", 2));
		questions.Add (new Question ("In 1980 Saddam Hussein was named an honorary citizen of this city", "Detroit", "Michigan", "Luxembourg", "Baghdad", 0));
		questions.Add (new Question ("The 2. largest planet in our solar system is..","Mars","Jupiter","Saturn","The Sun",2));
		questions.Add (new Question ("What does happen with the alien sheep in this game?","It goes away","It explodes","It gets sick","It flies away",1));
		questions.Add (new Question ("Where was this game made?","In Latvia","In Finland","In Lithuania","In Estonia",3));

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
			if (timer < 0) {
				if (currentQuestion.correctAnswer == 0) {
					SelectAnswer (1);
				} else {
					SelectAnswer (0);
				}
			}
		}
	}

	public void CombatStart(){
		MonsterAttributes monster = aiTrigger.GetComponentInChildren<MonsterAttributes> ();
		soundsManager = GameObject.FindWithTag ("Soundscontainer").GetComponent<DigitalRuby.SoundManagerNamespace.SoundsManager> ();

		lastQuestion = -1;
		playerHealth = GameObject.FindWithTag ("Player").GetComponent<PlayerAttributes> ().health;; 
		enemyHealth = monster.health;

		playerMaxHealth = playerHealth;
		enemyMaxHealth = enemyHealth;

		Debug.Log (playerHealth + "/" + playerMaxHealth + ";" + enemyHealth + "/" + enemyMaxHealth);

		enemyHPBar.localScale = new Vector3 (1, 1, 1);
		playerHPBar.localScale = new Vector3 (1, 1, 1);

		if (enemyHealth > 3) { //if boss
			soundsManager.PlayMusic(2);
		} else { //else normal battle music
			soundsManager.PlayMusic(0);
		}
				

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
		soundsManager.PlaySound (0);
		switch (item.itemType) {
		case ItemType.CLOCK:
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
		case ItemType.APPLE:
			
			break;
		}
	}

	public void EndCombat(){
	}

	public void FleeCombat(){
		timerRunning = false;
		soundsManager.PlaySound (0);
		soundsManager.PlaySound (4);
		playPreviousMusic ();
		aiTrigger.FleeCombat ();
	}

	public void newQuestion(){
		timer = timerMax;
		timerRunning = true;
		lastQuestion++;
		//Debug.Log("HEALTH: "+enemyHealth);	
		//Text t = combatCanvas.transform.FindChild ("EnemyHP").GetComponent<Text> ();
		//t.text = health.ToString();
		//enemyText.text = enemyHealth.ToString();
		//playerText.text = playerHealth.ToString ();

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
		soundsManager.PlaySound (0);
		timerRunning = false;
		StartCoroutine (CO_DisableButtons(currentQuestion.correctAnswer == answer && enemyHealth == 1, currentQuestion.correctAnswer != answer && playerHealth == 1));
		if (currentQuestion.correctAnswer == answer) {
			Debug.Log ("Correct answer (" + answer + ")!");
			colorButton (currentQuestion.correctAnswer, greenColor);
			enemyHealth--;	
			if (enemyHealth == 0) {
				GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Animator> ().Play ("Win");
				aiTrigger.GetComponentInChildren<Animator> ().SetTrigger ("Lose");
				soundsManager.PlaySound (2);
				if (aiTrigger.finalBoss) {
					StartCoroutine (EndGame ());
				}
			} else {
				GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Animator> ().Play ("Attack");
				GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<ParticleSystem> ().Play ();
				StartCoroutine (aiTrigger.CO_ShakeAndGlow ());
				soundsManager.PlaySound (1);
			}
		} else {
			colorButton (currentQuestion.correctAnswer, greenColor);
			colorButton (answer, pinkColor);
			Debug.Log ("False answer (" + answer + ")! Correct answer was "+currentQuestion.correctAnswer+".");
			playerHealth--;
			GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Animator> ().Play ("Lose");
			soundsManager.PlaySound (3);
			StartCoroutine(aiTrigger.CO_HitPlayer ());
		}	

		enemyHPBar.localScale = new Vector3 ((float)((enemyHealth*1.0)/(enemyMaxHealth*1.0))	, 1, 1);
		playerHPBar.localScale = new Vector3 ((float)((playerHealth*1.0)/(playerMaxHealth*1.0)), 1, 1);
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


	IEnumerator CO_DisableButtons(bool win, bool lose){
		setButtonsClickable (false);
		float i = 0.0f;
		while(i < 2.4f)
		{
			i += Time.deltaTime;
			yield return null;
		}
		setButtonsClickable (true);
		if (win) {
			playPreviousMusic ();
			aiTrigger.EndCombat ();
		} else if (lose) {
			playPreviousMusic ();
			soundsManager.PlaySound (4);
			aiTrigger.FleeCombat ();
		} else { //fight still going on
			newQuestion ();
		}
	}

	public void playPreviousMusic(){
		int temp = soundsManager.lastMusic;
		soundsManager.PlayMusic (temp);
	}

	IEnumerator EndGame(){
		yield return new WaitForSeconds (0.5f);
		aiTrigger.GetComponentInChildren<ParticleSystem> ().Play ();
		yield return new WaitForSeconds (0.2f);
		aiTrigger.transform.gameObject.SetActive(false);
		yield return new WaitForSeconds (3f);
		SceneManager.LoadScene ("Outro");
	}
}
