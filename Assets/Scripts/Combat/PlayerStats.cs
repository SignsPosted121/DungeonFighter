using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerStats : MonoBehaviour {

	public int Health = 10;
	public int MaxHealth = 10;
	public int Shield = 1;
	public int MaxShield = 4;
	public int Willpower = 10;
	public int MaxWillpower = 50;
	public int FloorCount = -1;

	public float Experience = 0;
	public int Level = 1;

	public string Crossbow = "BasicCrossbow";

	public Text HealthText;
	public Text WillText;
	public Transform Settings;
	public Transform Inventory;

	public int FireResistance = 100;
	public int WaterResistance = 100;
	public int PoisonResistance = 100;
	public int WindResistance = 100;

	public int Poisoned = 0;
    public int OnFire = 0;
    public bool Dodging = false;
    public bool Counter = false;

    public static IEnumerator Action(string Message, Color TextColor)
    {
        GameObject TextBox = Instantiate((GameObject)Resources.Load("ActionText"));
        TextBox.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
        TextBox.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        TextBox.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        TextBox.GetComponent<Text>().text = Message;
        TextBox.GetComponent<Text>().color = TextColor;
        TextBox.GetComponent<Text>().CrossFadeAlpha(0, 2f, false);
        for (int i = 0; i < 50; i++)
        {
            TextBox.GetComponent<RectTransform>().anchorMax += new Vector2(0f, 0.001f);
            TextBox.GetComponent<RectTransform>().anchorMin += new Vector2(0f, 0.001f);
            TextBox.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            TextBox.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(TextBox.gameObject);
    }

    public static IEnumerator Loot(string Message, string Item)
    {
        GameObject TextBox = Instantiate((GameObject)Resources.Load("GainedText"));
        TextBox.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform.Find("Notifications"));
        TextBox.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        TextBox.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        TextBox.transform.Find("Text").GetComponent<Text>().text = Message;
        TextBox.transform.Find("Picture").Find(Item).localScale = new Vector3(1, 1, 1);
        TextBox.GetComponent<Image>().CrossFadeAlpha(0, 2f, false);
        TextBox.transform.Find("Text").GetComponent<Text>().CrossFadeAlpha(0, 2f, false);
		if (Item != "Important")
		{
			TextBox.transform.Find("Picture").Find(Item).GetComponent<Image>().CrossFadeAlpha(0, 2f, false);
		} else TextBox.transform.Find("Picture").Find(Item).GetComponent<Text>().CrossFadeAlpha(0, 2f, false);
		Destroy(TextBox.gameObject, 2f);
		yield return true;
	}

	public static IEnumerator LevelUp()
	{
		GameObject TextBox = Instantiate((GameObject)Resources.Load("LevelUpText"));
		TextBox.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
		TextBox.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
		TextBox.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
		TextBox.GetComponent<Image>().CrossFadeAlpha(0, 1f, false);
		TextBox.transform.Find ("Text").GetComponent<Text>().CrossFadeAlpha(0, 1f, false);
		Destroy(TextBox.gameObject, 1f);
		yield return true;
	}

	public static IEnumerator DamagePlayer () {
		GameObject.FindGameObjectWithTag ("Player").transform.Find ("Cover").GetComponent <Image> ().color = new Color (1, 0, 0, 0.25f);
		for (int i = 0; i < 50; i++) {
			GameObject.FindGameObjectWithTag ("Player").transform.Find ("Cover").GetComponent <Image> ().color -= new Color (0, 0, 0, 0.005f);
			yield return new WaitForSeconds (0.01f);
		}
	}

	IEnumerator WaitTurn () {
		FightManager FM = GameObject.FindGameObjectWithTag ("Player").GetComponent <FightManager> ();
		if (FM.CurrentTurn == "Player") {
			int Recover = (int)Random.Range (2, 6);
			gameObject.GetComponent <AttackScript> ().ActiveCard = null;
			gameObject.GetComponent <AttackScript> ().Target = null;
			Willpower += Recover;
			StartCoroutine (Action ("You've waited and regained " + Recover + " will power!", new Color (0, 0.4f, 1)));
			gameObject.GetComponent <FightManager> ().CurrentTurn = "PlayerWorking";
			yield return new WaitForSeconds (1.5f);
			FM.CurrentTurn = "PlayerDone";
		}
		yield return new WaitForSeconds (0);
	}

	void OpenSettings () {
		Settings.gameObject.SetActive (true);
	}

	void OpenInventory () {
		Inventory.gameObject.SetActive (true);
	}

	void Start () {
		WillText.transform.parent.GetComponent <Button> ().onClick.AddListener (() => StartCoroutine (WaitTurn ()));
		transform.Find ("OpenSettings").GetComponent <Button> ().onClick.AddListener (OpenSettings);
		transform.Find ("HUD").Find ("OpenInventory").GetComponent <Button> ().onClick.AddListener (OpenInventory);
	}

	void Update () {
		HealthText.text = Health + "/" + MaxHealth + "          HP";
		HealthText.transform.parent.Find ("Filled").GetComponent <Image> ().fillAmount = (float) Health / MaxHealth;
		HealthText.transform.parent.Find ("Fire").GetComponent <Image> ().color = new Color (1, 1, 1, (float) OnFire / 5);
		HealthText.transform.parent.Find ("Poison").GetComponent <Image> ().color = new Color (1, 1, 1, (float) Poisoned / 3);
		if (OnFire > 0 || Poisoned > 0) {
			HealthText.color = new Color (1, 0, 1);
		} else if (Shield > 0) {
			HealthText.color = new Color (1, 0.5F, 0);
		} else HealthText.color = new Color (1, 0, 0);
		WillText.text = Willpower + "/" + MaxWillpower + "          Will";
		WillText.transform.parent.Find ("Filled").GetComponent <Image> ().fillAmount = (float) Willpower / MaxWillpower;
		transform.Find ("FloorCount").GetComponent <Text> ().text = "Floors Cleared : " + FloorCount;
		for (int i = 0; i < 6; i++) {
			Transform ShieldPoint = HealthText.transform.parent.Find ("ShieldPoints").GetChild (i);
            if (i < MaxShield)
            {
                if (Shield >= i + 1)
                {
                    ShieldPoint.GetComponent<Image>().color = new Color(1, 0.5f, 0, 1);
                }
                else ShieldPoint.GetComponent<Image>().color = new Color(0.785f, 0.785f, 0.785f, 1);
            }
            else ShieldPoint.GetComponent<Image>().color = new Color(0.785f, 0.785f, 0.785f, 0);
        }
	}
}
