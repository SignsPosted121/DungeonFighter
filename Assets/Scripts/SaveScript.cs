using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    public static IEnumerator LoadGame ()
    {
        Transform Player = GameObject.FindGameObjectWithTag("Player").transform;
        Transform Enemies = GameObject.FindGameObjectWithTag("Player").transform.Find("Enemies");
        Transform Inventory = Player.GetComponent<PlayerStats>().Inventory.GetComponent<InventoryManager>().ItemInventory;
		Transform Stats = Inventory.parent.GetComponent<InventoryManager>().Stats;
		Transform Shop = Player.GetComponent<FightManager>().Shop;

		Player.GetComponent<FightManager>().enabled = false;

		Player.GetComponent<PlayerStats>().FloorCount = PlayerPrefs.GetInt("LastFloorOn");
		Player.GetComponent<FightManager>().FloorDifficulty = PlayerPrefs.GetFloat("FloorDifficulty");
		Shop.GetComponent<ShopManager>().Coins = PlayerPrefs.GetInt("Coins");
		Stats.GetComponent<StatsManagerScript>().SP = PlayerPrefs.GetInt("PlayerStatPoints");
		Player.Find ("Highscore").GetComponent <Text> ().text = "Highscore: " + PlayerPrefs.GetInt("BETA_Highscore");

		Player.GetComponent<PlayerStats>().Health = PlayerPrefs.GetInt("PlayerHealth");
        Player.GetComponent<PlayerStats>().MaxHealth = PlayerPrefs.GetInt("PlayerMaxHealth");
        Player.GetComponent<PlayerStats>().Shield = PlayerPrefs.GetInt("PlayerShield");
        Player.GetComponent<PlayerStats>().MaxShield = PlayerPrefs.GetInt("PlayerMaxShield");
        Player.GetComponent<PlayerStats>().Willpower = PlayerPrefs.GetInt("PlayerWillpower");
		Player.GetComponent<PlayerStats>().MaxWillpower = PlayerPrefs.GetInt("PlayerMaxWillpower");
		Player.GetComponent<PlayerStats>().Experience = PlayerPrefs.GetFloat("PlayerExperience");
		Player.GetComponent<PlayerStats>().Level = PlayerPrefs.GetInt("PlayerLevel");
		Player.GetComponent<PlayerStats>().FireResistance = PlayerPrefs.GetInt("PlayerFireResistance");
        Player.GetComponent<PlayerStats>().WaterResistance = PlayerPrefs.GetInt("PlayerWaterResistance");
        Player.GetComponent<PlayerStats>().PoisonResistance = PlayerPrefs.GetInt("PlayerPoisonResistance");
        Player.GetComponent<PlayerStats>().WindResistance = PlayerPrefs.GetInt("PlayerWindResistance");
        Player.GetComponent<PlayerStats>().Poisoned = PlayerPrefs.GetInt("PlayerPoisoned");
        Player.GetComponent<PlayerStats>().OnFire = PlayerPrefs.GetInt("PlayerOnFire");
        Player.GetComponent<AttackScript>().Bolts = PlayerPrefs.GetInt("PlayerBolts");
        Player.GetComponent<AttackScript>().MaxBolts = PlayerPrefs.GetInt("PlayerMaxBolts");
        Player.GetComponent<AttackScript>().BoltsShoot = PlayerPrefs.GetInt("PlayerBoltsShoot");


		for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetString("Enemy" + i) != "None")
            {
                GameObject EnemyCard = null;
                if (Resources.Load("Cards/Enemies/" + PlayerPrefs.GetString("Enemy" + i)) != null)
                {
                    EnemyCard = (GameObject)Instantiate(Resources.Load("Cards/Enemies/" + PlayerPrefs.GetString("Enemy" + i)));
                } else if (Resources.Load("Cards/Bosses/" + PlayerPrefs.GetString("Enemy" + i)) != null)
                {
                    EnemyCard = (GameObject)Instantiate(Resources.Load("Cards/Bosses/" + PlayerPrefs.GetString("Enemy" + i)));
                }
                EnemyCard.GetComponent <EnemyStats> ().Health = PlayerPrefs.GetInt("Enemy" + i + "_Health");
                EnemyCard.GetComponent<EnemyStats>().Poisoned = PlayerPrefs.GetInt("Enemy" + i + "_Poison");
                EnemyCard.GetComponent<EnemyStats>().OnFire = PlayerPrefs.GetInt("Enemy" + i + "_Fire");
                EnemyCard.transform.SetParent(Enemies);
                if (PlayerPrefs.GetInt("EnemyCount") == 2)
                {
                    if (i == 0)
                    {
                        EnemyCard.GetComponent<RectTransform>().anchorMin = new Vector2(0.25f, 0.4f);
                        EnemyCard.GetComponent<RectTransform>().anchorMax = new Vector2(0.45f, 0.6f);
                    }
                    if (i == 1)
                    {
                        EnemyCard.GetComponent<RectTransform>().anchorMin = new Vector2(0.55f, 0.4f);
                        EnemyCard.GetComponent<RectTransform>().anchorMax = new Vector2(0.75f, 0.6f);
                    }
                }
                if (PlayerPrefs.GetInt("EnemyCount") == 1 || PlayerPrefs.GetInt("EnemyCount") == 3)
                {
                    if (i == 0)
                    {
                        EnemyCard.GetComponent<RectTransform>().anchorMin = new Vector2(0.4f, 0.4f);
                        EnemyCard.GetComponent<RectTransform>().anchorMax = new Vector2(0.6f, 0.6f);
                    }
                    if (i == 1)
                    {
                        EnemyCard.GetComponent<RectTransform>().anchorMin = new Vector2(0.1f, 0.4f);
                        EnemyCard.GetComponent<RectTransform>().anchorMax = new Vector2(0.3f, 0.6f);
                    }
                    if (i == 2)
                    {
                        EnemyCard.GetComponent<RectTransform>().anchorMin = new Vector2(0.7f, 0.4f);
                        EnemyCard.GetComponent<RectTransform>().anchorMax = new Vector2(0.9f, 0.6f);
                    }
                }
                if (PlayerPrefs.GetInt("EnemyCount") == 4)
                {
                    if (i == 0)
                    {
                        EnemyCard.GetComponent<RectTransform>().anchorMin = new Vector2(0.025f, 0.4f);
                        EnemyCard.GetComponent<RectTransform>().anchorMax = new Vector2(0.225f, 0.6f);
                    }
                    if (i == 1)
                    {
                        EnemyCard.GetComponent<RectTransform>().anchorMin = new Vector2(0.275f, 0.4f);
                        EnemyCard.GetComponent<RectTransform>().anchorMax = new Vector2(0.475f, 0.6f);
                    }
                    if (i == 2)
                    {
                        EnemyCard.GetComponent<RectTransform>().anchorMin = new Vector2(0.525f, 0.4f);
                        EnemyCard.GetComponent<RectTransform>().anchorMax = new Vector2(0.725f, 0.6f);
                    }
                    if (i == 3)
                    {
                        EnemyCard.GetComponent<RectTransform>().anchorMin = new Vector2(0.775f, 0.4f);
                        EnemyCard.GetComponent<RectTransform>().anchorMax = new Vector2(0.975f, 0.6f);
                    }
                }
                EnemyCard.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
                EnemyCard.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            }
        }

        for (int i = 0; i < Inventory.childCount; i++)
        {
            if (Inventory.GetChild(i).name != "Exit")
            {
                Inventory.GetChild(i).Find("Item").GetComponent<BasicItemScript>().Amount = (float) PlayerPrefs.GetInt("Item" + i);
            }
        }

        Player.GetComponent<FightManager>().CurrentTurn = "Player";
        Player.GetComponent<FightManager>().TurnInPlay = true;
        Player.GetComponent<FightManager>().enabled = true;
        Player.GetComponent<FightManager>().TurnInPlay = false;

        yield return new WaitForSeconds(1f);

        if ((Player.GetComponent<PlayerStats>().FloorCount) % 10 == 0 && (Player.GetComponent<PlayerStats>().FloorCount) != 0)
        {
            Shop.gameObject.SetActive(true);
        }
    }

    public static void SaveGame ()
    {
        Transform Player = GameObject.FindGameObjectWithTag("Player").transform;
        Transform Enemies = GameObject.FindGameObjectWithTag("Player").transform.Find("Enemies");
        Transform HUD = GameObject.FindGameObjectWithTag("Player").transform.Find("HUD");
        Transform Inventory = Player.GetComponent<PlayerStats>().Inventory.GetComponent<InventoryManager>().ItemInventory;
		Transform Stats = Inventory.parent.GetComponent<InventoryManager>().Stats;
		Transform Shop = Player.GetComponent<FightManager>().Shop;
        Transform Cards = HUD.Find("ActionCards");

        PlayerPrefs.SetInt("BETA_SaveData", 1);

        PlayerPrefs.SetString("Enemy1", "None");
        PlayerPrefs.SetString("Enemy2", "None");
        PlayerPrefs.SetString("Enemy3", "None");
        PlayerPrefs.SetString("Enemy4", "None");
        PlayerPrefs.SetInt("EnemyCount", Enemies.childCount);

		PlayerPrefs.SetInt("LastFloorOn", Player.GetComponent<PlayerStats>().FloorCount);
		PlayerPrefs.SetFloat("FloorDifficulty", Player.GetComponent<FightManager>().FloorDifficulty);
		PlayerPrefs.SetInt("Coins", Shop.GetComponent <ShopManager> ().Coins);
		PlayerPrefs.SetInt("PlayerStatPoints", Stats.GetComponent<StatsManagerScript>().SP);

		PlayerPrefs.SetInt("PlayerHealth", Player.GetComponent<PlayerStats>().Health);
        PlayerPrefs.SetInt("PlayerMaxHealth", Player.GetComponent<PlayerStats>().MaxHealth);
        PlayerPrefs.SetInt("PlayerShield", Player.GetComponent<PlayerStats>().Shield);
        PlayerPrefs.SetInt("PlayerMaxShield", Player.GetComponent<PlayerStats>().MaxShield);
        PlayerPrefs.SetInt("PlayerWillpower", Player.GetComponent<PlayerStats>().Willpower);
		PlayerPrefs.SetInt("PlayerMaxWillpower", Player.GetComponent<PlayerStats>().MaxWillpower);
		PlayerPrefs.SetFloat("PlayerExperience", Player.GetComponent<PlayerStats>().Experience);
		PlayerPrefs.SetInt("PlayerLevel", Player.GetComponent<PlayerStats>().Level);
		PlayerPrefs.SetInt("PlayerFireResistance", Player.GetComponent<PlayerStats>().FireResistance);
        PlayerPrefs.SetInt("PlayerWaterResistance", Player.GetComponent<PlayerStats>().WaterResistance);
        PlayerPrefs.SetInt("PlayerPoisonResistance", Player.GetComponent<PlayerStats>().PoisonResistance);
        PlayerPrefs.SetInt("PlayerWindResistance", Player.GetComponent<PlayerStats>().WindResistance);
        PlayerPrefs.SetInt("PlayerPoisoned", Player.GetComponent<PlayerStats>().Poisoned);
        PlayerPrefs.SetInt("PlayerOnFire", Player.GetComponent<PlayerStats>().OnFire);
        PlayerPrefs.SetInt("PlayerBolts", Player.GetComponent<AttackScript>().Bolts);
        PlayerPrefs.SetInt("PlayerMaxBolts", Player.GetComponent<AttackScript>().MaxBolts);
        PlayerPrefs.SetInt("PlayerBoltsShoot", Player.GetComponent<AttackScript>().BoltsShoot);

        for (int i = 0; i < Enemies.childCount; i++)
        {
            PlayerPrefs.SetString("Enemy" + i, Enemies.GetChild (i).Find ("CardName").GetComponent <Text> ().text);
            PlayerPrefs.SetInt("Enemy" + i + "_Health", Enemies.GetChild(i).GetComponent<EnemyStats>().Health);
            PlayerPrefs.SetInt("Enemy" + i + "_Poison", Enemies.GetChild(i).GetComponent<EnemyStats>().Poisoned);
            PlayerPrefs.SetInt("Enemy" + i + "_Fire", Enemies.GetChild(i).GetComponent<EnemyStats>().OnFire);
        }

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetString("Card" + i, Cards.GetChild(i).Find("RealName").GetComponent<Text>().text);
            PlayerPrefs.SetInt("Card" + i + "Floors", Cards.GetChild(i).GetComponent <UseActionCard> ().TurnsHeld);
        }

        for (int i = 0; i < Inventory.childCount; i++)
        {
            if (Inventory.GetChild(i).name != "Exit")
            {
                PlayerPrefs.SetInt("Item" + i, (int)Inventory.GetChild(i).Find("Item").GetComponent<BasicItemScript>().Amount);
            }
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
