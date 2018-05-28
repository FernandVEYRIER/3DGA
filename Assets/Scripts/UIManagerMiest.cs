using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerMiest : MonoBehaviour
{

	public Text _time;
	public Text _score;
	public Text enemyLeft;

	public Text _gameOver;
	public Text _displayScoreEnd;
	public Text _scoreEndText;

	private bool endGame;
	
	// Use this for initialization
	void Start ()
	{
		endGame = false;

		_gameOver.enabled = false;
		_displayScoreEnd.enabled = false;
		_scoreEndText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (endGame)
		{
			_time.enabled = false;
			_score.enabled = false;
			enemyLeft.enabled = false;

			_gameOver.enabled = true;
			_displayScoreEnd.enabled = true;
			_scoreEndText.enabled = true;
		}
	}
}
