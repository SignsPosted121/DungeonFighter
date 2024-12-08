using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManagerScript : MonoBehaviour
{
	public FightManager FM;

	IEnumerator GoThroughDoor (Transform Pathway)
	{
		FM.FloorDifficulty = Pathway.GetComponent<PathwayScript>().Difficulty;
		FM.CurrentTurn = "None";
		for (int i = 0; i < transform.childCount; i++)
		{
			if (transform.GetChild (i).name != "Background")
			{
				Destroy(transform.GetChild (i).gameObject);
			}
		}
		gameObject.SetActive(false);
		yield return true;
	}

	public IEnumerator SpawnDoors (int Amount)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			if (transform.GetChild(i).name != "Background")
			{
				Destroy(transform.GetChild(i));
			}
		}
		int Doors = Amount;
		if (Doors == 0)
		{
			Doors = Random.Range(2, 7);
		}
		int X = 0;
		int Y = 0;
		for (int i = 0; i < Doors; i++)
		{
			GameObject Pathway = null;
			int Chosen = Random.Range(1, 5);
			if (Chosen <= 4)
			{
				Pathway = (GameObject)Instantiate(Resources.Load("Pathways/Path" + Random.Range(1, 3)));
			}
			Pathway.transform.SetParent(transform);
			if (Doors == 6)
			{
				Pathway.GetComponent<RectTransform>().anchorMin = new Vector2(0.1f + (0.4f * X), 0.65f - (0.25f * Y));
				Pathway.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f + (0.4f * X), 0.85f - (0.25f * Y));
				Pathway.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
				Pathway.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
			}
			if (Doors == 5)
			{
				if (i != 4)
				{
					Pathway.GetComponent<RectTransform>().anchorMin = new Vector2(0.1f + (0.4f * X), 0.65f - (0.25f * Y));
					Pathway.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f + (0.4f * X), 0.85f - (0.25f * Y));
					Pathway.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
					Pathway.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
				}
				if (i == 4)
				{
					Pathway.GetComponent<RectTransform>().anchorMin = new Vector2(0.3f, 0.65f - (0.25f * Y));
					Pathway.GetComponent<RectTransform>().anchorMax = new Vector2(0.7f, 0.85f - (0.25f * Y));
					Pathway.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
					Pathway.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
				}
			}
			if (Doors == 4)
			{
				Pathway.GetComponent<RectTransform>().anchorMin = new Vector2(0.1f + (0.4f * X), 0.55f - (0.3f * Y));
				Pathway.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f + (0.4f * X), 0.75f - (0.3f * Y));
				Pathway.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
				Pathway.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
			}
			if (Doors == 3)
			{
				if (i != 2)
				{
					Pathway.GetComponent<RectTransform>().anchorMin = new Vector2(0.1f + (0.4f * X), 0.55f - (0.3f * Y));
					Pathway.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f + (0.4f * X), 0.75f - (0.3f * Y));
					Pathway.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
					Pathway.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
				}
				if (i == 2)
				{
					Pathway.GetComponent<RectTransform>().anchorMin = new Vector2(0.3f, 0.55f - (0.3f * Y));
					Pathway.GetComponent<RectTransform>().anchorMax = new Vector2(0.7f, 0.75f - (0.3f * Y));
					Pathway.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
					Pathway.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
				}
			}
			if (Doors == 2)
			{
				Pathway.GetComponent<RectTransform>().anchorMin = new Vector2(0.1f + (0.4f * X), 0.4f);
				Pathway.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f + (0.4f * X), 0.6f);
				Pathway.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
				Pathway.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
			}
			if (Doors == 1)
			{
				Pathway.GetComponent<RectTransform>().anchorMin = new Vector2(0.3f, 0.4f);
				Pathway.GetComponent<RectTransform>().anchorMax = new Vector2(0.7f, 0.6f);
				Pathway.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
				Pathway.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
			}
			X += 1;
			if (X > 1)
			{
				X = 0;
				Y += 1;
			}
			Pathway.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(GoThroughDoor(Pathway.transform)));
		}
		SaveScript.SaveGame();
		yield return true;
	}
    void Start()
    {
		gameObject.SetActive(false);
    }
}
