using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PlayerLife : MonoBehaviour {

    public float maxHP;
    public float damage;
    public float timeRegen;
    public float maxFocalLength;
    public PostProcessingProfile PostProcessingProfile;
    private DepthOfFieldModel.Settings dof;
    private float currentHP;
    private float timer;
    private float percentHP;

	// Use this for initialization
	void Start () {
        currentHP = maxHP;
        dof = PostProcessingProfile.depthOfField.settings;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "EmptyBottle")
        {
            currentHP -= damage;
        }
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (timer >= timeRegen)
        {
            currentHP += 1;
            if (currentHP > maxHP)
            {
                currentHP = maxHP;
            }
            timer = 0;
        }
        percentHP = currentHP / maxHP * 100;
        if (percentHP < 0)
        {
            percentHP = 0;
        }
        dof.focalLength = (maxFocalLength - 50) / 100 * (100 - percentHP) + 50;
        PostProcessingProfile.depthOfField.settings = dof;
    }
}
