using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

	public int Coins = 10;
	public Transform Inventory;
	public Transform ItemInventory;
	public Transform MiniInventory;
	public Transform Game;

	void Leave () {
		gameObject.SetActive (false);
	}

	void PurchaseRestoritive (string Type) {
		if (Type == "Health") {
			if (Coins > 1) {
				if (ItemInventory.Find ("HealthPotion") != null) {
					if (ItemInventory.Find ("HealthPotion").Find ("Item").GetComponent <BasicItemScript> ().Amount < ItemInventory.Find ("HealthPotion").Find ("Item").GetComponent <BasicItemScript> ().MaxAmount) {
						ItemInventory.Find ("HealthPotion").Find ("Item").GetComponent <BasicItemScript> ().Amount += 1;
						Coins -= 2;
					}
				}
			}
		}
		if (Type == "Will") {
			if (Coins > 1) {
				if (ItemInventory.Find ("WillPotion") != null) {
					if (ItemInventory.Find ("WillPotion").Find ("Item").GetComponent <BasicItemScript> ().Amount < ItemInventory.Find ("WillPotion").Find ("Item").GetComponent <BasicItemScript> ().MaxAmount) {
						ItemInventory.Find ("WillPotion").Find ("Item").GetComponent <BasicItemScript> ().Amount += 1;
						Coins -= 2;
					}
				}
			}
		}
		if (Type == "Neutral") {
			if (Coins > 1) {
				if (ItemInventory.Find ("Neutralizer") != null) {
					if (ItemInventory.Find ("Neutralizer").Find ("Item").GetComponent <BasicItemScript> ().Amount < ItemInventory.Find ("Neutralizer").Find ("Item").GetComponent <BasicItemScript> ().MaxAmount) {
						ItemInventory.Find ("Neutralizer").Find ("Item").GetComponent <BasicItemScript> ().Amount += 1;
						Coins -= 2;
					}
				}
			}
		}
		if (Type == "Meal") {
			if (Coins > 0 && Game.GetComponent <PlayerStats> ().Health < Game.GetComponent <PlayerStats> ().MaxHealth) {
				Game.GetComponent <PlayerStats> ().Health = Game.GetComponent <PlayerStats> ().MaxHealth;
				Coins -= 1;
			}
		}
	}

	void PurchaseBolts (int Amount) {
		if (Game.GetComponent <AttackScript> ().MaxBolts - Game.GetComponent <AttackScript> ().Bolts >= Amount) {
			if (Coins >= Amount) {
				Game.GetComponent <AttackScript> ().Bolts += Amount;
				Coins -= Amount;
			}
		}
	}

	void OpenInventory () {
		Inventory.gameObject.SetActive (true);
	}

	void Start () {
		for (int i = 0; i < transform.Find ("Background").Find ("Shop").childCount; i++) {
			Transform Shop = transform.Find ("Background").Find ("Shop");
			if (Shop.GetChild (i).name == "HealthPotion") {
				Shop.GetChild (i).GetComponent <Button> ().onClick.AddListener (() => PurchaseRestoritive ("Health"));
			}
			if (Shop.GetChild (i).name == "WillPotion") {
				Shop.GetChild (i).GetComponent <Button> ().onClick.AddListener (() => PurchaseRestoritive ("Will"));
			}
			if (Shop.GetChild (i).name == "Neutralizer") {
				Shop.GetChild (i).GetComponent <Button> ().onClick.AddListener (() => PurchaseRestoritive ("Neutral"));
			}
			if (Shop.GetChild (i).name == "Bolt") {
				Shop.GetChild (i).GetComponent <Button> ().onClick.AddListener (() => PurchaseBolts (1));
			}
			if (Shop.GetChild (i).name == "BulkBolts") {
				Shop.GetChild (i).GetComponent <Button> ().onClick.AddListener (() => PurchaseBolts (5));
			}
			if (Shop.GetChild (i).name == "Meal") {
				Shop.GetChild (i).GetComponent <Button> ().onClick.AddListener (() => PurchaseRestoritive ("Meal"));
			}
		}
		MiniInventory.Find ("Open").GetComponent <Button> ().onClick.AddListener (OpenInventory);
		MiniInventory.Find ("Leave").GetComponent <Button> ().onClick.AddListener (Leave);
		gameObject.SetActive (false);
	}

	void Update () {
		MiniInventory.Find ("Bolts").Find ("Text").GetComponent <Text> ().text = Game.GetComponent <AttackScript> ().Bolts + "/" + Game.GetComponent <AttackScript> ().MaxBolts + " Bolts";
		MiniInventory.Find ("Coins").Find ("Text").GetComponent <Text> ().text = Coins + " Coins";
	}
}
