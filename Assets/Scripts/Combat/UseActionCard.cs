using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseActionCard : MonoBehaviour {

	public int Willpower = 0;
    public int Chance = 1;
    public int TurnsHeld = 0;
    public bool SuperCharged = false;
    public string Type = "Misc";

    public void SuperCharge ()
    {
        ColorBlock Colors = gameObject.GetComponent<Button>().colors;
        Colors.normalColor = new Color32(251, 205, 083, 255);
        Colors.highlightedColor = new Color32(251, 205, 083, 255);
        Colors.pressedColor = new Color32(222, 181, 074, 255);
        gameObject.GetComponent<Button>().colors = Colors;
        transform.GetChild(2).GetComponent<RectTransform>().localScale = new Vector3(0, 0, 1);
        transform.GetChild(3).GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        transform.GetChild(3).GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        transform.GetChild(3).GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        transform.GetChild(3).GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);
        transform.GetChild(3).GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);
        SuperCharged = true;
    }

	void Action () {
		if (transform.parent.parent.parent.GetComponent <FightManager> ().CurrentTurn == "Player") {
			if (transform.parent.parent.parent.GetComponent <AttackScript> ().ActiveCard == transform) {
				transform.parent.parent.parent.GetComponent <AttackScript> ().ActiveCard = null; return;
			}
			if (transform.parent.parent.parent.GetComponent <AttackScript> ().ActiveCard != transform) {
				transform.parent.parent.parent.GetComponent <AttackScript> ().ActiveCard = transform; return;
			}
		}
	}
	
	void Start () {
		gameObject.GetComponent <Button> ().onClick.AddListener (Action);
	}

}
