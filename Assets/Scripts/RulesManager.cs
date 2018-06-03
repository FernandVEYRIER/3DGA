using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.Linq;
using JetBrains.Annotations;

public class RulesManager : MonoBehaviour
{
	public string duration;
	
	public int nbAI;
	public int maxScore;
	public int nbAIMax;

	private float _timeMinute;
	private float _timeSeconds;

	private UIManagerMiest _ui;

	public Transform spawneur1;
	public Transform spawneur2;

	public GameObject AI;

	private bool _spawnAI;
	private bool coreGame;	
	
	void Start ()
	{
		if (convertiseurTime() == 1)
			returnError("Bro... more than 60 seconds... ironic");

		_ui = GameObject.Find("GameManager").GetComponent<UIManagerMiest>();

		_spawnAI = true;
		coreGame = true;

		StartCoroutine("LostTime");
	}
	void Update ()
	{
		if (coreGame)
			{	
				_ui._score.text = maxScore.ToString();
				_ui.enemyLeft.text = nbAI.ToString();

				//_timeMinute = (int) (Time.deltaTime / 60f);
				//_timeSeconds = (int) (Time.deltaTime % 60f);
				_ui._time.text = _timeMinute.ToString("00") + ":" + _timeSeconds.ToString("00");

				//StartCoroutine("LostTime");

				if (_spawnAI)
				{
					float timeUntilNewSpawnAI = Random.Range(2, 5);

					StartCoroutine(waitToSpawnAI(timeUntilNewSpawnAI));
				}

				if (_timeMinute == 0f && _timeSeconds == 0f ||  nbAIMax == 0)
					coreGame = false;	
			}
	}

	public void ennemyHit()
	{
		maxScore += 20;
	}
	
	public int convertiseurTime()
	{
		string[] time = duration.Split(':');

		_timeMinute = float.Parse(time[0], CultureInfo.InvariantCulture.NumberFormat);
		_timeSeconds = float.Parse(time[1], CultureInfo.InvariantCulture.NumberFormat);

		if (_timeSeconds > 60)
			return 1;
		return 0;
	}

	public string returnError(string errorMessages)
	{
		using (WWW www = new WWW(errorMessages))
		{
			return www.error;
		}
	}

	public void spawnOneAI(Transform _pos)
	{
		Instantiate(AI, _pos.position, Quaternion.identity);
	}

	IEnumerator waitToSpawnAI(float time)
	{
		_spawnAI = false;
		yield return new WaitForSeconds(time);
		int randPos = Random.Range(1, 3);
		if (randPos == 1)
			spawnOneAI(spawneur1);
		else 
			spawnOneAI(spawneur2);
		_spawnAI = true;
		nbAI++;
	}

	IEnumerator LostTime()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			_timeSeconds--;
			if (_timeSeconds == 0 && _timeMinute > 0)
				_timeSeconds = 59;
			
			if (_timeSeconds == 59)
				_timeMinute--;
			
		}
	}
}
