using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathwayScript : MonoBehaviour
{

	public float Difficulty = 1;
	public string CL = "Easy";

	void Start()
    {
		if (GameObject.FindGameObjectWithTag ("Player"))
		{
			if (transform.parent.childCount > 2)
			{
				PlayerStats PStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
				int Base = ((PStats.Level - 1) / 5) * 100;
				Difficulty = Random.Range(75f + (Base / 3f), 100f + Base);
				Difficulty = Mathf.Floor(Difficulty);
				Difficulty /= 100;
				if (Difficulty > 3)
				{
					Difficulty = 3;
				}
				if (Difficulty < 1f)
				{
					CL = "Easy";
				}
				if (Difficulty >= 1f && Difficulty < 1.25f)
				{
					CL = "Normal";
				}
				if (Difficulty >= 1.25f && Difficulty < 1.5f)
				{
					CL = "Hard";
				}
				if (Difficulty >= 1.5f && Difficulty < 1.75f)
				{
					CL = "Very Hard";
				}
				if (Difficulty >= 1.75f && Difficulty < 2f)
				{
					CL = "Extreme";
				}
				if (Difficulty >= 2f && Difficulty < 2.5f)
				{
					CL = "Insane";
				}
				if (Difficulty >= 2.5f && Difficulty < 3f)
				{
					CL = "Suicide";
				}
				if (Difficulty >= 3f)
				{
					CL = "Ungodly";
				}
				transform.Find("Difficulty").GetComponent<Text>().text = "Difficulty: " + CL;
			} else
			{
				Difficulty = 1;
				transform.Find("Difficulty").GetComponent<Text>().text = "Boss Door";
			}
		}
    }
}
