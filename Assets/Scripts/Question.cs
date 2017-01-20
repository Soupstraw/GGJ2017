using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question {

	public string question, answerA, answerB, answerC, answerD;
	public int correctAnswer;

	public Question (string question, string answerA, string answerB, string answerC, string answerD, int correctAnswer)
	{
		this.question = question;
		this.answerA = answerA;
		this.answerB = answerB;
		this.answerC = answerC;
		this.answerD = answerD;
		this.correctAnswer = correctAnswer;
	}
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
