using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	void Start () {
		if (!PlayerPrefs.HasKey ("SpacedAttackHand")) {
			PlayerPrefs.SetInt ("SpacedAttackHand", 1);
		}
		if (PlayerPrefs.GetInt("SpacedAttackHand") == 1)
		{
			transform.Find("Options").Find("TiledCards").GetComponent<Toggle>().isOn = true;
			Transform Cards = GameObject.FindGameObjectWithTag("Player").transform.Find("HUD").Find("ActionCards");
			Transform Backpack = GameObject.FindGameObjectWithTag("Player").transform.Find("HUD").Find("OpenInventory");
			Transform Crossbow = GameObject.FindGameObjectWithTag("Player").transform.Find("HUD").Find("Crossbow");
			if (Backpack != null)
			{
				Backpack.GetComponent<RectTransform>().anchorMin = new Vector2(0.3f, 0.3f);
				Backpack.GetComponent<RectTransform>().anchorMax = new Vector2(0.475f, 1.5f);
				Backpack.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
				Backpack.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
			}
			if (Crossbow != null)
			{
				Crossbow.GetComponent<RectTransform>().anchorMin = new Vector2(0.525f, 0.3f);
				Crossbow.GetComponent<RectTransform>().anchorMax = new Vector2(0.7f, 1.5f);
				Crossbow.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
				Crossbow.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
			}
			for (int i = 0; i < Cards.childCount; i++)
			{
				Cards.GetChild(i).GetComponent<RectTransform>().anchorMin = new Vector2(i * 0.2f - 0.5f, 5.5f);
				Cards.GetChild(i).GetComponent<RectTransform>().anchorMax = new Vector2(i * 0.2f - 0.1f, 13f);
				Cards.GetChild(i).GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
				Cards.GetChild(i).GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
				Cards.GetChild(i).eulerAngles = new Vector3(0, 0, 0);
			}
		}
		if (PlayerPrefs.GetFloat("BETA_BabyMode") < 1)
		{
			transform.Find("Options").Find("EasyMode").GetComponent<Toggle>().isOn = true;
		}
		else PlayerPrefs.SetFloat("BETA_BabyMode", 1);
		transform.Find("Options").Find("TiledCards").GetComponent<Toggle>().onValueChanged.AddListener(ReArrangeCards);
		transform.Find("Options").Find("EasyMode").GetComponent<Toggle>().onValueChanged.AddListener(BabyMode);
		transform.Find ("Options").Find ("Close").GetComponent <Button> ().onClick.AddListener (Close);
		gameObject.SetActive (false);
	}

	void Close () {
		gameObject.SetActive (false);
	}

	void ReArrangeCards(bool On) {
		Transform Cards = GameObject.FindGameObjectWithTag ("Player").transform.Find ("HUD").Find ("ActionCards");
		Transform Backpack = GameObject.FindGameObjectWithTag ("Player").transform.Find ("HUD").Find ("OpenInventory");
		Transform Crossbow = GameObject.FindGameObjectWithTag ("Player").transform.Find ("HUD").Find ("Crossbow");
		if (!On) {
			PlayerPrefs.SetInt ("SpacedAttackHand", 0);
			if (Backpack != null) {
				Backpack.GetComponent <RectTransform> ().anchorMin = new Vector2 (0.06f, 2);
				Backpack.GetComponent <RectTransform> ().anchorMax = new Vector2 (0.2f, 3);
				Backpack.GetComponent <RectTransform> ().offsetMin = new Vector2 (0, 0);
				Backpack.GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
			}
			if (Crossbow != null) {
				Crossbow.GetComponent <RectTransform> ().anchorMin = new Vector2 (0.8f, 2);
				Crossbow.GetComponent <RectTransform> ().anchorMax = new Vector2 (0.94f, 3);
				Crossbow.GetComponent <RectTransform> ().offsetMin = new Vector2 (0, 0);
				Crossbow.GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
			}
			for (int i = 0; i < Cards.childCount; i++) {
				Cards.GetChild (i).GetComponent <RectTransform> ().anchorMin = new Vector2 (i * 0.15f, 0.5f);
				Cards.GetChild (i).GetComponent <RectTransform> ().anchorMax = new Vector2 (i * 0.15f + 0.4f, 8f);
				Cards.GetChild (i).GetComponent <RectTransform> ().offsetMin = new Vector2 (0, 0);
				Cards.GetChild (i).GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
				Cards.GetChild (i).eulerAngles = new Vector3 (0, 0, 10 - i * 5);
			}
		}
		if (On) {
			PlayerPrefs.SetInt ("SpacedAttackHand", 1);
			if (Backpack != null) {
				Backpack.GetComponent <RectTransform> ().anchorMin = new Vector2 (0.3f, 0.3f);
				Backpack.GetComponent <RectTransform> ().anchorMax = new Vector2 (0.475f, 1.5f);
				Backpack.GetComponent <RectTransform> ().offsetMin = new Vector2 (0, 0);
				Backpack.GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
			}
			if (Crossbow != null) {
				Crossbow.GetComponent <RectTransform> ().anchorMin = new Vector2 (0.525f, 0.3f);
				Crossbow.GetComponent <RectTransform> ().anchorMax = new Vector2 (0.7f, 1.5f);
				Crossbow.GetComponent <RectTransform> ().offsetMin = new Vector2 (0, 0);
				Crossbow.GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
			}
			for (int i = 0; i < Cards.childCount; i++) {
				Cards.GetChild (i).GetComponent <RectTransform> ().anchorMin = new Vector2 (i * 0.4f - 0.5f, 5.5f);
				Cards.GetChild (i).GetComponent <RectTransform> ().anchorMax = new Vector2 (i * 0.4f - 0.1f, 13f);
				Cards.GetChild (i).GetComponent <RectTransform> ().offsetMin = new Vector2 (0, 0);
				Cards.GetChild (i).GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
				Cards.GetChild (i).eulerAngles = new Vector3 (0, 0, 0);
			}
		}
	}

	void BabyMode(bool On)
	{
		if (On)
		{
			PlayerPrefs.SetFloat("BETA_BabyMode", 0.75f);
		}
		if (!On)
		{
			PlayerPrefs.SetFloat("BETA_BabyMode", 1);
		}
	}
}
