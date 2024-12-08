using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	public Transform Main;
	public Transform Crossbows;
	public Transform CardInventory;
	public Transform ItemInventory;
	public Transform Stats;
	public PlayerStats PStats;

	void SelectCrossbow (Transform Crossbow) {
		PlayerPrefs.SetInt(PStats.Crossbow, 1);
		PlayerPrefs.SetInt (Crossbow.name, 2);
		PStats.Crossbow = Crossbow.name;
		if (Main.Find("CrossbowCard").Find("Bow"))
		{
			Destroy(Main.Find("CrossbowCard").Find("Bow").gameObject);
		}
		GameObject NewImage = (GameObject)Instantiate(Crossbow.Find("Bow").gameObject);
		NewImage.name = "Bow";
		NewImage.transform.SetParent(Main.Find("CrossbowCard"));
		NewImage.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
		NewImage.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
		Destroy(NewImage.transform.Find("Name").gameObject);
		Destroy(NewImage.transform.Find("Description").gameObject);
		if (PStats.transform.Find("HUD").Find("Crossbow").Find("Bow"))
		{
			Destroy(PStats.transform.Find("HUD").Find("Crossbow").Find("Bow").gameObject);
		}
		GameObject NewImage2 = (GameObject)Instantiate(Crossbow.Find("Bow").gameObject);
		NewImage2.name = "Bow";
		NewImage2.transform.SetParent(PStats.transform.Find ("HUD").Find ("Crossbow"));
		NewImage2.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
		NewImage2.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
		Destroy(NewImage2.transform.Find("Name").gameObject);
		Destroy(NewImage2.transform.Find("Description").gameObject);
		Crossbows.gameObject.SetActive (false);
		Main.gameObject.SetActive (true);
	}

	void UseBasicItem (Transform Item) {
		string ID = Item.Find ("ItemName").GetComponent <Text> ().text;
		if (ID == "Health Potion") {
			if (Item.GetComponent <BasicItemScript> ().Amount > 0)
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Health < GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().MaxHealth)
                {
                    Item.GetComponent<BasicItemScript>().Amount -= 1;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Health += 5;
                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Health > GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().MaxHealth)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().MaxHealth;
                    }
                }
			}
		}
		if (ID == "Will Potion") {
			if (Item.GetComponent <BasicItemScript> ().Amount > 0) {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Willpower < GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().MaxWillpower)
                {
                    Item.GetComponent<BasicItemScript>().Amount -= 1;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Willpower += 5;
                    if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Willpower > GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().MaxWillpower)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Willpower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().MaxWillpower;
                    }
                }
			}
		}
		if (ID == "Neutralizer") {
			if (Item.GetComponent <BasicItemScript> ().Amount > 0) {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().OnFire > 0 || GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Poisoned > 0)
                {
                    Item.GetComponent<BasicItemScript>().Amount -= 1;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().OnFire = 0;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Poisoned = 0;
                }
			}
        }
        if (ID == "Leather Straps") {
            if (Item.GetComponent<BasicItemScript>().Amount > 0)
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Shield < GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().MaxShield)
                {
                    Item.GetComponent<BasicItemScript>().Amount -= 1;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Shield += 1;
                }
            }
        }
    }

	void CloseInventory () {
		gameObject.SetActive (false);
	}

	void OpenItemInventory()
	{
		ItemInventory.gameObject.SetActive(true);
		Main.gameObject.SetActive(false);
	}

	void CloseItemInventory()
	{
		ItemInventory.gameObject.SetActive(false);
		Main.gameObject.SetActive(true);
	}

	void OpenCardInventory()
	{
		CardInventory.gameObject.SetActive(true);
		Main.gameObject.SetActive(false);
	}

	void CloseCardInventory()
	{
		CardInventory.gameObject.SetActive(false);
		Main.gameObject.SetActive(true);
	}

	void OpenCrossbows () {
		Crossbows.gameObject.SetActive (true);
		Main.gameObject.SetActive (false);
	}

	void CloseCrossbows () {
		Crossbows.gameObject.SetActive (false);
		Main.gameObject.SetActive (true);
	}

	void OpenStats()
	{
		Stats.gameObject.SetActive(true);
		Main.gameObject.SetActive(false);
	}

	void CloseStats()
	{
		Stats.gameObject.SetActive(false);
		Main.gameObject.SetActive(true);
	}

	void Start() {
        if (PlayerPrefs.HasKey("ALPHA_SaveData") == false) {
            PlayerPrefs.SetInt("BasicCrossbow", 2);
            PlayerPrefs.SetInt("StrongCrossbow", 0);
            PlayerPrefs.SetInt("DoubleCrossbow", 0);
            PlayerPrefs.SetInt("SmallCrossbow", 0);
            PlayerPrefs.SetInt("FireCrossbow", 0);
            PlayerPrefs.SetInt("PoisonousCrossbow", 0);
        }
        if (PlayerPrefs.HasKey("ALPHA_SaveData") == true)
        {
            Crossbows.Find("BasicCrossbow").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, PlayerPrefs.GetInt("BasicCrossbow"));
            Crossbows.Find("BasicCrossbow").Find("Bow").GetComponent<Image>().color = new Color(1, 1, 1, PlayerPrefs.GetInt("BasicCrossbow"));
            if (PlayerPrefs.GetInt("BasicCrossbow") == 2)
            {
				SelectCrossbow(Crossbows.Find ("BasicCrossbow"));
			}
            Crossbows.Find("StrongCrossbow").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, PlayerPrefs.GetInt("StrongCrossbow"));
            Crossbows.Find("StrongCrossbow").Find("Bow").GetComponent<Image>().color = new Color(1, 1, 1, PlayerPrefs.GetInt("StrongCrossbow"));
            if (PlayerPrefs.GetInt("StrongCrossbow") == 2)
			{
				SelectCrossbow(Crossbows.Find("StrongCrossbow"));
			}
            Crossbows.Find("DoubleCrossbow").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, PlayerPrefs.GetInt("DoubleCrossbow"));
            Crossbows.Find("DoubleCrossbow").Find("Bow").GetComponent<Image>().color = new Color(1, 1, 1, PlayerPrefs.GetInt("DoubleCrossbow"));
            if (PlayerPrefs.GetInt("DoubleCrossbow") == 2)
			{
				SelectCrossbow(Crossbows.Find("DoubleCrossbow"));
			}
            Crossbows.Find("SmallCrossbow").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, PlayerPrefs.GetInt("SmallCrossbow"));
            Crossbows.Find("SmallCrossbow").Find("Bow").GetComponent<Image>().color = new Color(1, 1, 1, PlayerPrefs.GetInt("SmallCrossbow"));
            if (PlayerPrefs.GetInt("SmallCrossbow") == 2)
			{
				SelectCrossbow(Crossbows.Find("SmallCrossbow"));
			}
            Crossbows.Find("FireCrossbow").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, PlayerPrefs.GetInt("FireCrossbow"));
            Crossbows.Find("FireCrossbow").Find("Bow").GetComponent<Image>().color = new Color(1, 1, 1, PlayerPrefs.GetInt("FireCrossbow"));
            if (PlayerPrefs.GetInt("FireCrossbow") == 2)
			{
				SelectCrossbow(Crossbows.Find("FireCrossbow"));
			}
            Crossbows.Find("PoisonousCrossbow").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, PlayerPrefs.GetInt("PoisonousCrossbow"));
            Crossbows.Find("PoisonousCrossbow").Find("Bow").GetComponent<Image>().color = new Color(1, 1, 1, PlayerPrefs.GetInt("PoisonousCrossbow"));
            if (PlayerPrefs.GetInt("PoisonousCrossbow") == 2)
			{
				SelectCrossbow(Crossbows.Find("PoisonousCrossbow"));
			}
        }
        Main.Find("CrossbowCard").GetComponent<Button>().onClick.AddListener(OpenCrossbows);
        Main.Find("CardInventoryCard").GetComponent<Button>().onClick.AddListener(OpenCardInventory);
		Main.Find("ItemInventoryCard").GetComponent<Button>().onClick.AddListener(OpenItemInventory);
		Main.Find("StatsCard").GetComponent<Button>().onClick.AddListener(OpenStats);
		Main.Find("Exit").GetComponent<Button>().onClick.AddListener(CloseInventory);
        Crossbows.Find("Exit").GetComponent<Button>().onClick.AddListener(CloseCrossbows);
        CardInventory.Find("Exit").GetComponent<Button>().onClick.AddListener(CloseCardInventory);
		ItemInventory.Find("Exit").GetComponent<Button>().onClick.AddListener(CloseItemInventory);
		Stats.Find("Exit").GetComponent<Button>().onClick.AddListener(CloseStats);
		for (int i = 0; i < ItemInventory.childCount; i++)
        {
            if (ItemInventory.GetChild(i).Find("Item") != null)
            {
                Transform Item = ItemInventory.GetChild(i).Find("Item");
                ItemInventory.GetChild(i).GetComponent<Button>().onClick.AddListener(() => UseBasicItem(Item));
            }
        }
        for (int i = 0; i < Crossbows.childCount; i++)
        {
            Transform Cross = Crossbows.GetChild(i);
            if (Cross.name != "Exit")
            {
                Crossbows.GetChild(i).GetComponent<Button>().onClick.AddListener(() => SelectCrossbow(Cross));
            }
        }
        gameObject.SetActive(false);
    }

    void CheckCrossbows () {
		if (PlayerPrefs.HasKey ("ALPHA_SaveData") == true) {
			Crossbows.Find ("BasicCrossbow").GetComponent <Image> ().color = new Color (0.5f, 0.5f, 0.5f, PlayerPrefs.GetInt ("BasicCrossbow"));
			Crossbows.Find ("StrongCrossbow").GetComponent <Image> ().color = new Color (0.5f, 0.5f, 0.5f, PlayerPrefs.GetInt ("StrongCrossbow"));
			Crossbows.Find ("DoubleCrossbow").GetComponent <Image> ().color = new Color (0.5f, 0.5f, 0.5f, PlayerPrefs.GetInt ("DoubleCrossbow"));
			Crossbows.Find ("SmallCrossbow").GetComponent <Image> ().color = new Color (0.5f, 0.5f, 0.5f, PlayerPrefs.GetInt ("SmallCrossbow"));
			Crossbows.Find ("FireCrossbow").GetComponent <Image> ().color = new Color (0.5f, 0.5f, 0.5f, PlayerPrefs.GetInt ("FireCrossbow"));
			Crossbows.Find ("PoisonousCrossbow").GetComponent <Image> ().color = new Color (0.5f, 0.5f, 0.5f, PlayerPrefs.GetInt ("PoisonousCrossbow"));
		}
	}

	void Update () {
        CheckCrossbows();
		float x1 = 0.05f;
		float y1 = 0.8f;
		float x2 = 0.05f;
		float y2 = 0.8f;
		transform.Find ("CoinCount").GetComponent <Text> ().text = PStats.gameObject.GetComponent <FightManager> ().Shop.GetComponent <ShopManager> ().Coins + " Coins";
		for (int i = 0; i < ItemInventory.childCount; i++) {
			if (ItemInventory.GetChild (i).name != "Exit") {
				ItemInventory.GetChild (i).GetComponent <RectTransform> ().anchorMin = new Vector2 (x2, y2);
				ItemInventory.GetChild (i).GetComponent <RectTransform> ().anchorMax = new Vector2 (x2 + 0.225f, y2 + 0.15f);
				ItemInventory.GetChild (i).GetComponent <RectTransform> ().offsetMin = new Vector2 (0, 0);
				ItemInventory.GetChild (i).GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
				x2 += 0.225f;
				if (x2 > 0.725f) {
					x2 = 0.05f;
					y2 -= 0.15f;
				}
			}
		}
		for (int i = 0; i < Crossbows.childCount; i++) {
			if (Crossbows.GetChild (i).name != "Exit" && Crossbows.GetChild (i).GetComponent <Image> ().color.a <= 0) {
				Crossbows.GetChild (i).GetComponent <RectTransform> ().anchorMin = new Vector2 (2, 2);
				Crossbows.GetChild (i).GetComponent <RectTransform> ().anchorMax = new Vector2 (3, 3);
				Crossbows.GetChild (i).GetComponent <RectTransform> ().offsetMin = new Vector2 (0, 0);
				Crossbows.GetChild (i).GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
			}
			if (Crossbows.GetChild (i).name != "Exit" && Crossbows.GetChild (i).GetComponent <Image> ().color.a > 0) {
				Crossbows.GetChild (i).GetComponent <RectTransform> ().anchorMin = new Vector2 (x1, y1);
				Crossbows.GetChild (i).GetComponent <RectTransform> ().anchorMax = new Vector2 (x1 + 0.225f, y1 + 0.15f);
				Crossbows.GetChild (i).GetComponent <RectTransform> ().offsetMin = new Vector2 (0, 0);
				Crossbows.GetChild (i).GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
				x1 += 0.225f;
				if (x1 > 0.725f) {
					x1 = 0.05f;
					y1 -= 0.15f;
				}
			}
		}
	}
}
