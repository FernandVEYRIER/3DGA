using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerMiest : MonoBehaviour
{

	public Text _time;
	public Text _score;
	public Text enemyLeft;

	public Text _gameOver;
	public Text _displayScoreEnd;
	public Text _scoreEndText;

	public Text finalScore;

	public bool endGame;

	public GameObject CanvasGameOver;

	public VRUIInput right_vruiInput;
	public SteamVR_LaserPointer right_laser;
	public SteamVR_TrackedController right_trackedController;
	
	// Use this for initialization
	void Start ()
	{
		endGame = false;

		_gameOver.enabled = false;
		_displayScoreEnd.enabled = false;
		_scoreEndText.enabled = false;

		right_vruiInput.enabled = false;
		right_laser.enabled = false;
		right_trackedController.enabled = false;
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

			CanvasGameOver.SetActive(true);

			right_vruiInput.enabled = true;
			right_laser.enabled = true;
			right_trackedController.enabled = true;

			var tmp = GameObject.Find(CanvasGameOver.name + "/Score");
			tmp.GetComponent<Text>().text = _score.text;
			
			Debug.Log("my score " + tmp.GetComponent<Text>().text);
		}
	}

	public void RestartButton()
	{
		SceneManager.LoadScene("NewBarTry", LoadSceneMode.Single);
		Debug.Log("mdr ça tape !");
	}

	public void QuitButton()
	{
		Application.Quit();
	}
}
