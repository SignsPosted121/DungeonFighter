using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour {

	public int Health = 0;
	public int MaxHealth = 0;
	public bool Boss = false;
	public bool HasTurn = true;

	public int FireResistance = 100;
	public int WaterResistance = 100;
	public int PoisonResistance = 100;
	public int WindResistance = 100;

	public int Poisoned = 0;
	public int OnFire = 0;
	public int BaseXP = 10;

	public bool Ultimate = false;

	public void TakeDamage (int Amount)
	{
		Health -= Amount;
		if (Health > 0)
		{
			transform.GetComponent<AudioSource>().volume = 0.1f + Amount / 10;
			transform.GetComponent<AudioSource>().pitch = (float)Random.Range(75, 125) / 100;
			transform.GetComponent<AudioSource>().Play();
		}
	}

	void TargetThis () {
		if (transform.parent.parent.GetComponent <AttackScript> ().Target == transform) {
			transform.parent.parent.GetComponent <AttackScript> ().Target = null; return;
		}
		if (transform.parent.parent.GetComponent <FightManager> ().CurrentTurn == "Player") {
			if (transform.parent.parent.GetComponent <AttackScript> ().Target != transform) {
				transform.parent.parent.GetComponent <AttackScript> ().Target = transform;
				return;
			}
		}
	}

	public IEnumerator GrowThis (int Negative) {
		yield return new WaitForSeconds (0.01f);
		for (int i = 0; i < 5; i++) {
			if (this != null) {
				gameObject.GetComponent <RectTransform> ().localScale -= new Vector3 (0.04f * Negative, 0.04f * Negative, 0);
				yield return new WaitForSeconds (0.01f);
			}
		}
		for (int i = 0; i < 5; i++) {
			if (this != null) {
				gameObject.GetComponent <RectTransform> ().localScale += new Vector3 (0.04f * Negative, 0.04f * Negative, 0);
				yield return new WaitForSeconds (0.01f);
			}
		}
	}

	private bool BubbleReady = true;

	public IEnumerator CreateBubble()
	{
		if (BubbleReady)
		{
			BubbleReady = false;
			GameObject BubbleFab = (GameObject)Instantiate(Resources.Load("PoisonBubble"));
			RectTransform Bubble = BubbleFab.GetComponent<RectTransform>();
			Bubble.SetParent(transform.Find("Poison"));
			Bubble.offsetMin = new Vector2(0, 0);
			Bubble.offsetMax = new Vector2(0, 0);
			Bubble.anchorMin = new Vector2(Random.Range(0, 90) / 100f, Random.Range(60, 90) / 100f);
			Bubble.anchorMax = new Vector2(Bubble.anchorMin.x + 0.1f, Bubble.anchorMin.y + 0.1f);
			yield return new WaitForSeconds(0.25f);
			BubbleReady = true;
		}
	}

		void Start () {
		gameObject.GetComponent <Button> ().onClick.AddListener (() => TargetThis ());
	}

	void Update () {
		transform.Find ("Health").GetComponent <Text> ().text = Health + "/" + MaxHealth + " HP";
		if (Health <= 0) {
			transform.parent.parent.Find("Music").Find("EnemyDied").GetComponent<AudioSource>().pitch = (float)Random.Range(75, 125) / 100;
			transform.parent.parent.Find("Music").Find("EnemyDied").GetComponent<AudioSource>().Play();
			PlayerStats.Action (gameObject.name + " has died!", new Color (0, 1, 0, 1));
            if ((int)Random.Range(1, 3) == 2) {
                int Chances = (int)Random.Range(1, 20);
                if (Chances <= 10)
                {
                    int Coins = (int) (Random.Range(1, 3) * GameObject.FindGameObjectWithTag("Player").GetComponent<FightManager>().FloorDifficulty);
                    if (Coins > 0)
                    {
                        transform.parent.parent.GetComponent<FightManager>().Shop.gameObject.GetComponent<ShopManager>().Coins += Coins;
						StartCoroutine(PlayerStats.Loot("Item Found:           " + Coins + " Coins", "Coins"));
                        transform.parent.parent.Find("HUD").Find("OpenInventory").GetComponent<CardMouseOver>().Shine(1);
                    }
                }
                if (Chances >= 11 && Chances <= 12)
                {
                    int Bolts = (int)(Random.Range(1, 4) * GameObject.FindGameObjectWithTag("Player").GetComponent<FightManager>().FloorDifficulty);
                    transform.parent.parent.GetComponent<AttackScript>().Bolts += Bolts;
                    if (transform.parent.parent.GetComponent<AttackScript>().Bolts > transform.parent.parent.GetComponent<AttackScript>().MaxBolts)
                    {
                        transform.parent.parent.GetComponent<AttackScript>().Bolts = transform.parent.parent.GetComponent<AttackScript>().MaxBolts;
                    }
					StartCoroutine(PlayerStats.Loot("Item Found:           " + Bolts + " Bolts", "Bolts"));
                    transform.parent.parent.Find("HUD").Find("OpenInventory").GetComponent<CardMouseOver>().Shine(1);
                }
                if (Chances >= 13 && Chances <= 14)
                {
                    BasicItemScript Stats = transform.parent.parent.GetComponent<PlayerStats>().Inventory.GetComponent<InventoryManager>().ItemInventory.GetChild(1).Find("Item").GetComponent<BasicItemScript>();
                    Stats.Amount += 1;
                    if (Stats.Amount > Stats.MaxAmount)
                    {
                        Stats.Amount = Stats.MaxAmount;
                    }
					StartCoroutine(PlayerStats.Loot("Item Found:           Health Potion", "HealthPotion"));
                    transform.parent.parent.Find("HUD").Find("OpenInventory").GetComponent<CardMouseOver>().Shine(1);
                }
                if (Chances >= 15 && Chances <= 16)
                {
                    BasicItemScript Stats = transform.parent.parent.GetComponent<PlayerStats>().Inventory.GetComponent<InventoryManager>().ItemInventory.GetChild(2).Find("Item").GetComponent<BasicItemScript>();
                    Stats.Amount += 1;
                    if (Stats.Amount > Stats.MaxAmount)
                    {
                        Stats.Amount = Stats.MaxAmount;
                    }
					StartCoroutine(PlayerStats.Loot("Item Found:           Will Potion", "WillPotion"));
                    transform.parent.parent.Find("HUD").Find("OpenInventory").GetComponent<CardMouseOver>().Shine(1);
                }
                if (Chances >= 17 && Chances <= 18)
                {
                    BasicItemScript Stats = transform.parent.parent.GetComponent<PlayerStats>().Inventory.GetComponent<InventoryManager>().ItemInventory.GetChild(3).Find("Item").GetComponent<BasicItemScript>();
                    Stats.Amount += 1;
                    if (Stats.Amount > Stats.MaxAmount)
                    {
                        Stats.Amount = Stats.MaxAmount;
                    }
					StartCoroutine(PlayerStats.Loot("Item Found:           Neutralizer", "Neutralizer"));
                    transform.parent.parent.Find("HUD").Find("OpenInventory").GetComponent<CardMouseOver>().Shine(1);
                }
                if (Chances >= 19 && Chances <= 19)
                {
                    BasicItemScript Stats = transform.parent.parent.GetComponent<PlayerStats>().Inventory.GetComponent<InventoryManager>().ItemInventory.GetChild(4).Find("Item").GetComponent<BasicItemScript>();
                    Stats.Amount += 1;
                    if (Stats.Amount > Stats.MaxAmount)
                    {
                        Stats.Amount = Stats.MaxAmount;
					}
					StartCoroutine(PlayerStats.Loot("Item Found:           Leather Straps", "LeatherStraps"));
                    transform.parent.parent.Find("HUD").Find("OpenInventory").GetComponent<CardMouseOver>().Shine(1);
                }
            }
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Experience += (float) BaseXP * GameObject.FindGameObjectWithTag("Player").GetComponent<FightManager>().FloorDifficulty;
			if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Experience >= (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Level - 1) * 20 + 50)
			{
				StartCoroutine(PlayerStats.LevelUp());
				GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Experience -= (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Level - 1) * 20 + 50;
				GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Level += 1;
				GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Inventory.GetComponent<InventoryManager>().Stats.GetComponent<StatsManagerScript>().SP += 1;
			}
			Destroy (gameObject);
		}
		transform.Find ("Fire").GetComponent <Image> ().color = new Color (1, 1, 1, (float) OnFire / 5);
		for (int i = 0; i < Poisoned * 2; i++)
		{
			if (transform.Find ("Poison").childCount < Poisoned * 2)
			{
				StartCoroutine(CreateBubble());
			}
			RectTransform Bubble = transform.Find("Poison").GetChild(i).GetComponent<RectTransform>();
			Bubble.offsetMin += new Vector2(0, 0.2f);
			Bubble.offsetMax += new Vector2(0, 0.2f);
			if (Bubble.offsetMin.y < 10)
			{
				Bubble.GetChild(0).localScale += new Vector3(0.02f, 0.02f, 0);
			}
			if (Bubble.offsetMin.y >= 14 && Bubble.offsetMin.y <= 20)
			{
				Bubble.GetChild((int)Bubble.offsetMin.y - 14).GetComponent<Image>().color = new Color(1, 1, 1, 0);
				Bubble.GetChild((int)Bubble.offsetMin.y - 13).GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
			}
			if (Bubble.offsetMin.y >= 21)
			{
				Destroy(Bubble.gameObject);
			}
		}
		if (MaxHealth == 0) {
			transform.Find ("Health").GetComponent <Text> ().text = Health + " HP";
		}
		if (transform.parent.parent.GetComponent <AttackScript> ().Target == transform) {
			transform.parent.parent.localScale = new Vector3 (1.25f, 1.25f, 1.25f);
		} else transform.parent.parent.localScale = new Vector3 (1f, 1f, 1f);
		if (transform.parent.parent.GetComponent <AttackScript> ().Target == transform) {
			gameObject.GetComponent <Image> ().color = new Color (1, 0.9f, 0.9f);
		} else gameObject.GetComponent <Image> ().color = new Color (1, 1, 1);
		if (Boss) {
			gameObject.GetComponent <Image> ().color = new Color (1, 0.4f, 0.85f);
		}
	}
}
