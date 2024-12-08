using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class FightManager : MonoBehaviour {

	public Transform Music;
	public Transform Map;
	public Transform HUD;
	public Transform Enemies;
	public Transform Shop;
	public PlayerStats PStats;
	public Text HighscoreText;
    public string CurrentTurn;

    public float FloorDifficulty = 1;

    public bool TurnInPlay = false;
	public bool Spawning = false;
	private float Strength = 1;

	private bool ShownAd = false;

	private List <GameObject> Choices = new List <GameObject> ();

	void Save () {
		if (gameObject.GetComponent <PlayerStats> ().FloorCount > PlayerPrefs.GetInt ("BETA_Highscore")) {
			PlayerPrefs.SetInt ("BETA_Highscore", gameObject.GetComponent <PlayerStats> ().FloorCount);
		}
	}

	void Load () {
		HighscoreText.text = "Highscore: " + PlayerPrefs.GetInt ("BETA_Highscore");
    }

	IEnumerator AIActivate (Transform AI) {
		EnemyStats AIStats = AI.GetComponent <EnemyStats> ();
		if (AIStats.HasTurn == true) {
			if (AI.transform.Find ("CardName").GetComponent <Text> ().text == "Slertch") {
				int Chosen = (int)Random.Range (1, 17);
				if (Chosen >= 1 && Chosen <= 8) {
					int Damage = (int)(Random.Range (1, 5) * Strength);
					if (Damage < 0) {
						Damage = 0;
					}
					if (Damage > 0) {
						if (PStats.Shield <= 0) {
							PStats.Health -= Damage;
							StartCoroutine (PlayerStats.Action ("Slertch has struck you for " + Damage + " damage!", new Color (1, 0.25f, 0.25f)));
							StartCoroutine (PlayerStats.DamagePlayer ());
						} else {
							PStats.Shield -= Damage;
							if (PStats.Shield < 0) {
								Damage += PStats.Shield;
								PStats.Shield = 0;
							}
							StartCoroutine (PlayerStats.Action ("Slertch breaks through " + Damage + " armor!", new Color (1, 0.5f, 0)));
						}
					} else StartCoroutine (PlayerStats.Action ("Slertch tries to strike you, but his puffy arm bounces off of you!", new Color (1, 1, 1)));
				}
				if (Chosen >= 9 && Chosen <= 14) {
					int Damage = (int) ((Random.Range (0, 4 * (float)PStats.PoisonResistance / 100)) * Strength);
					if (Damage < 0) {
						Damage = 0;
					}
					if (Damage > 0) {
						PStats.Poisoned += Damage;
						StartCoroutine (PlayerStats.Action ("Fumes spread around you, poisoning you for " + Damage + " turns!", new Color (1, 0.25f, 0.25f)));
					} else StartCoroutine (PlayerStats.Action ("Fumes spread around you, but to no affect!", new Color (1, 1, 1)));
				}
				if (Chosen >= 15 && Chosen <= 16) {
					int Heal = (int)(Random.Range (-1, 3 * Strength));
					if (Heal < 0) {
						Heal = 0;
					}
					if (Heal > 0) {
						if (AIStats.OnFire <= 0) {
							AIStats.Health += Heal;
							StartCoroutine (PlayerStats.Action ("Slertch sinks roots into the ground for nutrients, gaining him " + Heal + " HP!", new Color (1, 1, 1)));
						}
						if (AIStats.OnFire > 0) {
							AIStats.Health -= Heal;
							StartCoroutine (PlayerStats.Action ("Slertch sinks roots into the ground for nutrients, but the fire instead deals " + Heal + " damage!", new Color (1, 0, 1)));
						}
					} else StartCoroutine (PlayerStats.Action ("Slertch stabs at the ground with roots, but to no avail!", new Color (1, 1, 1)));
				}
				if (AIStats.Poisoned > 0) {
					yield return new WaitForSeconds (1.5f);
					int Heal = (int)(AIStats.Poisoned * 2 * Strength);
					AIStats.Health += Heal;
					AIStats.Poisoned = 0;
					StartCoroutine (PlayerStats.Action ("Slertch uses the poison inflicted on him and converts it into " + Heal + " HP!", new Color (1, 1, 1)));
				}
			}




			//Non bosses down below




			if (AI.transform.Find ("CardName").GetComponent <Text> ().text == "Assassin") {
				int Chosen = (int)Random.Range (1, 15);
				if (Chosen <= 6) {
					if (PStats.Shield <= 0)
					{
						PStats.Health -= 1;
						StartCoroutine (PlayerStats.Action ("The Assassin shanks you for 1 damage!", new Color (1, 0, 0)));
						StartCoroutine (PlayerStats.DamagePlayer ());
					} else
					{
						StartCoroutine (PlayerStats.Action ("The assassin's blade crumples upon your armor!", new Color (1, 0.5f, 0)));
					}
				}
				if (Chosen >= 7 && Chosen <= 8) {
					int Heal = (int)(Random.Range (1, 3) * Strength);
					if (Enemies.childCount != 1) {
						for (int i = 0; i < Enemies.childCount; i++) {
							if (Enemies.GetChild (i) != AI) {
								Enemies.GetChild (i).GetComponent <EnemyStats> ().Health += Heal;
								Enemies.GetChild (i).GetComponent <EnemyStats> ().GrowThis (-1);
								if (Enemies.GetChild (i).GetComponent <EnemyStats> ().MaxHealth != 0 && Enemies.GetChild (i).GetComponent <EnemyStats> ().Health > Enemies.GetChild (i).GetComponent <EnemyStats> ().MaxHealth) {
									Enemies.GetChild (i).GetComponent <EnemyStats> ().Health = Enemies.GetChild (i).GetComponent <EnemyStats> ().MaxHealth;
								}
							}
						}
						StartCoroutine (PlayerStats.Action ("The Assassin heals everyone else for around " + Heal + " HP!", new Color (1, 1, 1)));
					} else Chosen = 11;
				}
				if (Chosen >= 9 && Chosen <= 11) {
					if (PStats.Shield <= 0)
					{
						PStats.Health -= 2;
						StartCoroutine (PlayerStats.Action ("The Assassin shanks you for 2 damage!", new Color (1, 0, 0)));
						StartCoroutine (PlayerStats.DamagePlayer ());
					} else
					{
						PStats.Health -= 1;
						StartCoroutine (PlayerStats.Action ("The Assassin stabs through your shield for 1 damage!", new Color (1, 0, 0)));
						StartCoroutine (PlayerStats.DamagePlayer ());
					}
				}
				if (Chosen >= 12 && Chosen <= 12) {
					if (PStats.Shield <= 0)
					{
						PStats.Health -= (int)(3 * Strength);
						StartCoroutine (PlayerStats.Action ("The Assassin shanks you for " + (int)3 * Strength + " damage!", new Color (1, 0, 0)));
						StartCoroutine (PlayerStats.DamagePlayer ());
					} else
					{
						PStats.Health -= 1;
                        PStats.Shield -= 1;
                        StartCoroutine (PlayerStats.Action ("The Assassin stabs through your shield for 1 HP and 1 armor damage!", new Color (1, 0, 0)));
						StartCoroutine (PlayerStats.DamagePlayer ());
					}
				}
				if (Chosen >= 13)
				{
					int Damage = (int) ((Random.Range (1, 4) / ((float) PStats.PoisonResistance / 100)) * Strength);
					PStats.Poisoned += Damage;
					StartCoroutine (PlayerStats.Action ("The Assassin poisons you for " + Damage + " turns!", new Color (1, 0, 0)));
				}
			}



			if (AI.transform.Find ("CardName").GetComponent <Text> ().text == "Fireling") {
				int Chosen = (int)Random.Range (1, 11);
				if (Chosen <= 3) {
					int Damage = (int)(Random.Range (0, 3) * Strength);
					if (Damage > 0) {
						PStats.Willpower -= Damage;
						StartCoroutine (PlayerStats.Action ("The Fireling laughs maliciously, scaring you of " + Damage + " Will!", new Color (1, 0, 0)));
					} else StartCoroutine (PlayerStats.Action ("The Fireling laughs maliciously, but to no affect!", new Color (1, 0, 0)));
				}
				if (Chosen >= 4 && Chosen <= 6) {
					int Damage = (int) ((Random.Range (1, 4) / ((float) PStats.FireResistance / 100)) * Strength);
					PStats.OnFire += Damage;
					StartCoroutine (PlayerStats.Action ("The Fireling spouts out flames, setting you on fire for " + Damage + " turns!", new Color (1, 0, 0)));
				}
				if (Chosen >= 7 && Chosen <= 10) {
					int Dodge = (int)Random.Range (1, 5);
					if (Dodge < 2) {
						int Damage = (int)((Random.Range (0, 4) / ((float)PStats.FireResistance / 100)) * Strength);
						int Damage2 = (int)(Random.Range (0, 3) * Strength);
						if (PStats.Shield <= 0) {
							PStats.OnFire += Damage;
							PStats.Health -= Damage2;
							StartCoroutine (PlayerStats.Action ("The Fireling fires a firebolt, dealing " + Damage2 + " damage and sets you on fire for " + Damage + " turns!", new Color (1, 0, 0)));
						} else {
							PStats.OnFire += Damage;
							PStats.Shield -= Damage2;
							StartCoroutine (PlayerStats.Action ("The Fireling fires a firebolt, breaking " + Damage2 + " shield" + ", and sets you on fire for " + Damage + " turns!", new Color (1, 0, 0)));
						}
					} else StartCoroutine (PlayerStats.Action ("The Fireling fires a firebolt, but misses!", new Color (1, 1, 1)));
				}
			}



			if (AI.transform.Find ("CardName").GetComponent <Text> ().text == "Ghost") {
				int Chosen = (int)Random.Range (1, 22);
				if (Chosen >= 1 && Chosen <= 13 && !AIStats.Ultimate) {
					int Damage = (int)(Random.Range (0, 4) * Strength);
					if (Damage > 0) {
						PStats.Willpower -= Damage;
						StartCoroutine (PlayerStats.Action ("The Ghost lets out a shillring cry, draining you of " + Damage + " Will!", new Color (1, 0, 0)));
					} else StartCoroutine (PlayerStats.Action ("The Ghost moans to itself in agony.", new Color (1, 1, 1)));
				}
				if (Chosen >= 14 && Chosen <= 20 && !AIStats.Ultimate) {
					int Damage = (int)((Random.Range (1, 3) / ((float)PStats.WindResistance / 100)) * Strength);
					PStats.Health -= Damage;
					StartCoroutine (PlayerStats.Action ("The Ghost attacks your spirit for " + Damage + " damage!", new Color (1, 0, 0)));
				}
				if (AIStats.Ultimate)
				{
					int Damage = (int)(AIStats.Health * 2 * Strength);
					AIStats.Health = 0;
					if (PStats.Shield <= 0)
					{
						PStats.Health -= Damage;
						StartCoroutine(PlayerStats.Action("The Ghost vaporizes itself in an explosion, damaging you for " + Damage + " HP!", new Color(1, 0, 0)));
					}
					else
					{
						PStats.Shield -= Damage;
						StartCoroutine(PlayerStats.Action("The Ghost vaporizes itself in an explosion, damaging your armor " + Damage + " damage!", new Color(1, 0, 0)));
					}
				}
				if (Chosen >= 21 && Chosen <= 21 && !AIStats.Ultimate)
				{
					AIStats.Ultimate = true;
					StartCoroutine(PlayerStats.Action("The ghost is about to blow apart it's soul and kill you!", new Color(1, 0.5f, 0)));
				}
			}



			if (AI.transform.Find ("CardName").GetComponent <Text> ().text == "Skeleton") {
				int Chosen = (int)Random.Range (1, 17);
				if (Chosen <= 11) {
					int Chance = (int)Random.Range (1, 11);
					if (Chance <= 8) {
						if (PStats.Shield <= 0) {
							PStats.Health -= 1;
							StartCoroutine (PlayerStats.Action ("The Skeleton shoots an arrow for 1 damage!", new Color (1, 0, 0)));
							StartCoroutine (PlayerStats.DamagePlayer ());
						} else {
							StartCoroutine (PlayerStats.Action ("The Skeleton's arrow bounces off of your armor!", new Color (1, 0.5f, 0)));
						}
					}
					if (Chance >= 9 && Chance <= 9) {
						int Damage = (int) ((Random.Range (1, 3) / ((float) PStats.FireResistance / 100)) * Strength);
						PStats.Health -= 1;
						PStats.OnFire += Damage;
						StartCoroutine (PlayerStats.Action ("The Skeleton shoots a fire arrow, damaging you for 1 HP and setting you on fire for " + Damage + " turns!", new Color (1, 0, 0)));
						StartCoroutine(PlayerStats.DamagePlayer());
					}
					if (Chance >= 10) {
						int Damage = (int) ((Random.Range (1, 3) / ((float) PStats.PoisonResistance / 100)) * Strength);
						PStats.Health -= 1;
						PStats.Poisoned += Damage;
						StartCoroutine (PlayerStats.Action ("The Skeleton shoots a poison arrow, damaging you for 1 HP and poisoning you for " + Damage + " turns!", new Color (1, 0, 0)));
						StartCoroutine(PlayerStats.DamagePlayer());
					}
				}
				if (Chosen >= 12 && Chosen <= 16) {
					StartCoroutine (PlayerStats.Action ("The Skeleton's arrow misses!", new Color (1, 1, 1)));
				}
			}



			if (AI.transform.Find ("CardName").GetComponent <Text> ().text == "Slime") {
				int Chosen = (int)Random.Range (1, 18);
				if (AIStats.Health >= 3 && Chosen > 12 && Chosen <= 16) {
					Chosen = (int)Random.Range(16, 18);
				}
				if (AIStats.Health < 2 && (Chosen <= 12 || Chosen > 16)) {
					Chosen = (int)Random.Range (5, 17);
				}
				if (Strength < 1)
				{
					Chosen = (int)Random.Range(1, 13);
				}
				if (Chosen <= 12) {
					if (PStats.Shield <= 0) {
						PStats.Health -= 1;
						StartCoroutine (PlayerStats.Action ("The Slime attacks for 1 damage!", new Color (1, 0, 0)));
						StartCoroutine (PlayerStats.DamagePlayer ());
					} else {
						PStats.Shield -= 1;
						if (PStats.Shield <= 0) {
							PStats.Shield = 0;
							StartCoroutine(PlayerStats.Action("The Slime breaks your shield!", new Color(1, 0.5f, 0)));
                        } else
                            StartCoroutine(PlayerStats.Action("The Slime damages your shield for 1 damage!", new Color(1, 0.5f, 0)));
                    }
				}
				if (Chosen > 12 && Chosen <= 16) {
					AIStats.Health += 1;
					StartCoroutine (AIStats.GetComponent <EnemyStats> ().GrowThis (-1));
					if (AIStats.Health > AIStats.MaxHealth) {
						AIStats.Health = AIStats.MaxHealth;
					}
					StartCoroutine (PlayerStats.Action ("The Slime heals for 1 health point!", new Color (1, 1, 1)));
				}
				if (Chosen >= 17) {
					if (PStats.Shield <= 0) {
						PStats.Health -= 2;
						StartCoroutine (PlayerStats.Action ("The Slime attacks for 2 damage!", new Color (1, 0, 0)));
						StartCoroutine (PlayerStats.DamagePlayer ());
					} else {
						PStats.Shield -= 1;
						if (PStats.Shield < 0) {
							StartCoroutine (PlayerStats.Action ("The Slime damages your shield for 1 damage!", new Color (1, 0.5f, 0)));
						} else
							StartCoroutine (PlayerStats.Action ("The Slime breaks your shield!", new Color (1, 0.5f, 0)));
					}
				}
			}



			if (AI.transform.Find ("CardName").GetComponent <Text> ().text == "Sucker") {
				int Chosen = (int)Random.Range (1, 8);
				if (Chosen <= 3) {
					int Damage = (int)(Random.Range (1, 5) * Strength);
					PStats.Willpower -= Damage;
					if (PStats.Willpower < 0) {
						Damage += PStats.Willpower;
						PStats.Willpower = 0;
					}
					StartCoroutine (PlayerStats.Action ("The Sucker has sucked " + Damage + " will power from you!", new Color (1, 0, 0)));
				}
				if (Chosen >= 4 && Chosen <= 6) {
					int Damage = (int)(Random.Range (1, 3) * Strength);
					PStats.Health -= Damage;
					AIStats.Health += Damage;
					StartCoroutine (AIStats.GetComponent <EnemyStats> ().GrowThis (-1));
					StartCoroutine (PlayerStats.Action ("The Sucker has sucked " + Damage + " HP from you!", new Color (1, 0, 0)));
					StartCoroutine (PlayerStats.DamagePlayer ());
				}
				if (Chosen >= 7) {
					if (PStats.Shield <= 0) {
						int Damage1 = (int)(Random.Range (1, 3) * Strength);
						int Damage2 = (int)(Random.Range (1, 5) * Strength);
						PStats.Health -= Damage1;
						PStats.Willpower -= Damage2;
						if (PStats.Willpower < 0) {
							Damage2 += PStats.Willpower;
							PStats.Willpower = 0;
						}
						StartCoroutine (PlayerStats.Action ("The Sucker has sucked " + Damage1 + " HP and " + Damage2 + " will power from you!", new Color (1, 0, 0)));
						StartCoroutine (PlayerStats.DamagePlayer ());
					} else {
						StartCoroutine (PlayerStats.Action ("The Sucker tries to suck your essence, but is deterred!", new Color (1, 0.5f, 0)));
					}
				}
			}



			if (AI.transform.Find ("CardName").GetComponent <Text> ().text == "Troll") {
				int Chosen = (int)Random.Range (1, 11);
				if (Chosen <= 2) {
					int Damage1 = (int)(Random.Range (1, 4) * Strength);
					int Damage2 = (int)(Random.Range (1, 7) * Strength);
					PStats.Health -= Damage1;
					PStats.Willpower -= Damage2;
					if (PStats.Willpower < 0) {
						Damage2 += PStats.Willpower;
						PStats.Willpower = 0;
					}
					StartCoroutine (PlayerStats.Action ("The Troll bashes the ground for " + Damage1 + " damage and knocks the wind out of you for " + Damage2 + " will power!", new Color (1, 0, 0)));
					StartCoroutine (PlayerStats.DamagePlayer ());
				}
				if (Chosen >= 3 && Chosen <= 4) {
					int Damage = (int)(Random.Range (1, 4) * Strength);
					PStats.Willpower -= Damage;
					if (PStats.Willpower < 0) {
						Damage += PStats.Willpower;
						PStats.Willpower = 0;
					}
					StartCoroutine (PlayerStats.Action ("The Troll screams profanity at you draining you of " + Damage + " will power!", new Color (1, 0, 0)));
				}
				if (Chosen >= 5 && Chosen <= 9) {
					StartCoroutine (PlayerStats.Action ("The Troll screams profanity at you!", new Color (1, 1, 1)));
				}
				if (Chosen >= 10) {
					if (PStats.Shield <= 0) {
						int Damage = (int)(Random.Range (1, 5) * Strength);
						PStats.Health -= Damage;
						StartCoroutine (PlayerStats.Action ("The Troll jabs your stomach with his club for " + Damage + " damage!", new Color (1, 0, 0)));
						StartCoroutine (PlayerStats.DamagePlayer ());
					} else {
						PStats.Shield -= 2;
						if (PStats.Shield < 0) {
							PStats.Shield = 0;
							StartCoroutine (PlayerStats.Action ("The Troll damages your shield for 2 damage!", new Color (1, 0.5f, 0)));
						} else
							StartCoroutine (PlayerStats.Action ("The Troll breaks your shield!", new Color (1, 0.5f, 0)));
						int Damage = (int)(Random.Range (1, 3) * Strength);
						PStats.Health -= Damage;
						StartCoroutine (PlayerStats.DamagePlayer ());
					}
				}
			}



			if (AI.transform.Find ("CardName").GetComponent <Text> ().text == "Zombie") {
				int Chosen = (int)Random.Range (1, 13);
				if (Chosen >= 1 && Chosen <= 4) {
					int Damage = (int)(Random.Range (0, 3) * Strength);
					PStats.Willpower -= Damage;
					if (PStats.Willpower < 0) {
						Damage += PStats.Willpower;
						PStats.Willpower = 0;
					}
					if (Damage > 0) {
						StartCoroutine (PlayerStats.Action ("The Zombie's moans fill your body with dread, draining you of " + Damage + " Will!", new Color (1, 0, 0)));
					} else StartCoroutine (PlayerStats.Action ("The Zombie's moans send chills down your spine!", new Color (1, 0, 0)));
				}
				if (Chosen >= 5 && Chosen <= 6) {
					int Damage = (int)(Random.Range (1, 3) * Strength);
					int Damage2 = (int)Random.Range (-1, 2);
					if (Damage2 < 0) {
						Damage2 = 0;
					}
					PStats.Willpower -= Damage2;
					if (PStats.Willpower < 0) {
						Damage2 += PStats.Willpower;
						PStats.Willpower = 0;
					}
					if (PStats.Shield <= 0) {
						PStats.Health -= Damage;
						StartCoroutine (PlayerStats.Action ("The Zombie strikes you with a heavy swing from his arm, dealing " + Damage + " damage and grossing you out for " + Damage2 + " Will!", new Color (1, 0, 0)));
					} else {
						PStats.Shield -= 1;
						StartCoroutine (PlayerStats.Action ("The Zombie strikes you with a heavy swing from his arm, breaking your shield and grossing you out for " + Damage2 + " Will!", new Color (1, 0, 0)));
					}
				}
				if (Chosen >= 7 && Chosen <= 8) {
					int Damage = (int) ((Random.Range (-1, 3) / ((float) PStats.PoisonResistance / 100)) * Strength);
					if (Damage < 0) {
						Damage = 0;
					}
					PStats.Poisoned += Damage;
					StartCoroutine (PlayerStats.Action ("The Zombie scratches your arm, spreading his diseases for " + Damage + " turns!", new Color (1, 0, 0)));
				}
				if (Chosen >= 9 && Chosen <= 12) {
					int Damage = (int) ((Random.Range (-1, 3) / ((float) PStats.PoisonResistance / 100)) * Strength);
					int Damage2 = (int)(Random.Range (0, 3) * Strength);
					if (Damage < 0) {
						Damage = 0;
					}
					if (PStats.Shield <= 0) {
						PStats.Poisoned += Damage;
						PStats.Health -= Damage2;
						StartCoroutine (PlayerStats.Action ("The Zombie bites you, dealing " + Damage2 + " damage and spreads his diseases for " + Damage + " turns!", new Color (1, 0, 0)));
					} else {
						PStats.Shield -= Damage2;
						if (PStats.Shield < 0) {
							Damage2 += PStats.Shield;
							PStats.Shield = 0;
						}
						StartCoroutine (PlayerStats.Action ("The Zombie bites into your shield for " + Damage2 + " shield damage!", new Color (1, 0.5f, 0)));
					}
				}
			}
			yield return new WaitForSeconds (1.5f);
		}



		AIStats.HasTurn = true;
		if (AIStats.OnFire > 0) {
            if (Random.Range(0, AIStats.FireResistance) <= 100)
            {
                AIStats.Health -= 1;
                if (AIStats.Health > 0)
                {
                    StartCoroutine(PlayerStats.Action(AI.Find("CardName").GetComponent<Text>().text + " burns for 1 damage!", new Color(1, 0, 1)));
                }
                else StartCoroutine(PlayerStats.Action(AI.Find("CardName").GetComponent<Text>().text + " burnt to a crisp!", new Color(1, 0, 1)));
                AIStats.OnFire -= 1;
                if (AIStats.OnFire > 5)
                {
                    AIStats.OnFire = 4;
                }
            }
            yield return new WaitForSeconds (1.5f);
		}
		if (AIStats.Poisoned > 0) {
			AIStats.Health -= AIStats.Poisoned;
			if (AIStats.Health > 0) {
				StartCoroutine (PlayerStats.Action (AI.Find ("CardName").GetComponent <Text> ().text + " is weakened by " + AIStats.Poisoned + " HP!", new Color (1, 0, 1)));
			} else StartCoroutine (PlayerStats.Action (AI.Find ("CardName").GetComponent <Text> ().text + " withered away!", new Color (1, 0, 1)));
			AIStats.Poisoned -= 1;
			if (AIStats.Poisoned > 5) {
				AIStats.Poisoned = 4;
			}
			yield return new WaitForSeconds (1.5f);
        }
        SaveScript.SaveGame();
        if (CurrentTurn == "Enemy1" && TurnInPlay == true) {
			CurrentTurn = "Enemy2";
			TurnInPlay = false;
		}
		if (CurrentTurn == "Enemy2" && TurnInPlay == true) {
			CurrentTurn = "Enemy3";
			TurnInPlay = false;
		}
		if (CurrentTurn == "Enemy3" && TurnInPlay == true) {
			CurrentTurn = "Enemy4";
			TurnInPlay = false;
		}
		if (CurrentTurn == "Enemy4" && TurnInPlay == true) {
			CurrentTurn = "Player";
			TurnInPlay = false;
		}
		yield return null;
	}

	void StartAI (Transform AI) {
		StartCoroutine (AIActivate (AI));
	}

	public IEnumerator SpawnEnemies () {
		gameObject.GetComponent <PlayerStats> ().FloorCount += 1;
		PlayerPrefs.SetInt ("BETA_TotalCrossbowHighscore", PlayerPrefs.GetInt ("BETA_TotalCrossbowHighscore") + 1);
		if (PlayerPrefs.GetInt ("BETA_TotalCrossbowHighscore") >= 25) {
			StartCoroutine(ShowAdWhenReady("StartAdvertisement"));
			Choices.Clear ();
			PlayerPrefs.SetInt ("BETA_TotalCrossbowHighscore", PlayerPrefs.GetInt ("BETA_TotalCrossbowHighscore") - 25);
			Transform Inventory = gameObject.GetComponent <PlayerStats> ().Inventory.GetComponent <InventoryManager> ().Crossbows;
			for (int i = 0; i < Inventory.childCount; i++) {
				if (Inventory.GetChild (i).name != "Exit" && PlayerPrefs.GetInt (Inventory.GetChild (i).name) == 0)
                {
                    Choices.Add (Inventory.GetChild (i).gameObject);
				}
			}
            if (Choices.Count > 1)
            {
                GameObject CrossbowChosen = Choices[Random.Range(1, Choices.Count)];
                PlayerPrefs.SetInt(CrossbowChosen.name, 1);
                HUD.Find("OpenInventory").GetComponent<CardMouseOver>().Shine(1);
				StartCoroutine(PlayerStats.Loot("New Crossbow Awarded!", "Important"));
            }
            if (Choices.Count == 1)
            {
                GameObject CrossbowChosen = Choices [1];
                PlayerPrefs.SetInt(CrossbowChosen.name, 1);
                HUD.Find("OpenInventory").GetComponent<CardMouseOver>().Shine(1);
            }
        }
		Save ();
		yield return new WaitForSeconds (0.1f);
		Load ();
		for (int i = transform.Find ("Enemies").childCount - 1; i > -1; i--) {
			Destroy (transform.Find ("Enemies").GetChild (i).gameObject);
		}
		yield return new WaitForSeconds (0.9f);
		gameObject.GetComponent <PlayerStats> ().Willpower += 10;
		gameObject.GetComponent <PlayerStats> ().OnFire = 0;
		gameObject.GetComponent <PlayerStats> ().Poisoned = 0;
		if (gameObject.GetComponent <PlayerStats> ().Willpower > gameObject.GetComponent <PlayerStats> ().MaxWillpower) {
			gameObject.GetComponent <PlayerStats> ().Willpower = gameObject.GetComponent <PlayerStats> ().MaxWillpower;
        }
        SaveScript.SaveGame();
        if ((gameObject.GetComponent <PlayerStats> ().FloorCount) % 10 == 0 && gameObject.GetComponent<PlayerStats>().FloorCount != 0) {
			StartCoroutine (PlayerStats.Action ("You find yourself in a well lit area with a shopkeeper!", new Color (1, 1, 1)));
			yield return new WaitForSeconds (2);
			Shop.gameObject.SetActive (true);
			SaveScript.SaveGame();
		}
        yield return new WaitWhile (() => Shop.gameObject.activeSelf);
		if (gameObject.GetComponent <PlayerStats> ().FloorCount == 0 || (gameObject.GetComponent <PlayerStats> ().FloorCount + 1) % 20 != 0) {
			StartCoroutine (PlayerStats.Action ("Spawning new dungeon...", new Color (1, 1, 1)));
			yield return new WaitForSeconds (1);
			CurrentTurn = "Player";
			int SpawnCount = Random.Range (1, 4);
			if (gameObject.GetComponent <PlayerStats> ().FloorCount >= 40) {
				SpawnCount = Random.Range (2, 5);
			}
			for (int i = 0; i < SpawnCount; i++) {
				int Chosen = (int)Random.Range (1, 30);
				GameObject EnemyCard = null;
				if (Chosen >= 1 && Chosen <= 8) {
					EnemyCard = (GameObject)Instantiate (Resources.Load ("Cards/Enemies/Slime"));
				}
				if (Chosen >= 9 && Chosen <= 12) {
					EnemyCard = (GameObject)Instantiate (Resources.Load ("Cards/Enemies/Skeleton"));
				}
				if (Chosen >= 13 && Chosen <= 14) {
					EnemyCard = (GameObject)Instantiate (Resources.Load ("Cards/Enemies/Sucker"));
				}
				if (Chosen >= 15 && Chosen <= 18) {
					EnemyCard = (GameObject)Instantiate (Resources.Load ("Cards/Enemies/Assassin"));
				}
				if (Chosen >= 19 && Chosen <= 19) {
					EnemyCard = (GameObject)Instantiate (Resources.Load ("Cards/Enemies/Troll"));
				}
				if (Chosen >= 20 && Chosen <= 23) {
					EnemyCard = (GameObject)Instantiate (Resources.Load ("Cards/Enemies/Fireling"));
				}
				if (Chosen >= 24 && Chosen <= 25) {
					EnemyCard = (GameObject)Instantiate (Resources.Load ("Cards/Enemies/Zombie"));
				}
				if (Chosen >= 26 && Chosen <= 28) {
					EnemyCard = (GameObject)Instantiate (Resources.Load ("Cards/Enemies/Ghost"));
				}
				if (Chosen >= 29 && Chosen <= 29) {
					EnemyCard = (GameObject)Instantiate (Resources.Load ("Cards/Enemies/HugeRat"));
				}
				EnemyCard.transform.SetParent (transform.Find ("Enemies"));
				if (SpawnCount == 2) {
					if (i == 0) {
						EnemyCard.GetComponent <RectTransform> ().anchorMin = new Vector2 (0.25f, 0.4f);
						EnemyCard.GetComponent <RectTransform> ().anchorMax = new Vector2 (0.45f, 0.6f);
					}
					if (i == 1) {
						EnemyCard.GetComponent <RectTransform> ().anchorMin = new Vector2 (0.55f, 0.4f);
						EnemyCard.GetComponent <RectTransform> ().anchorMax = new Vector2 (0.75f, 0.6f);
					}
				}
				if (SpawnCount == 1 || SpawnCount == 3) {
					if (i == 0) {
						EnemyCard.GetComponent <RectTransform> ().anchorMin = new Vector2 (0.4f, 0.4f);
						EnemyCard.GetComponent <RectTransform> ().anchorMax = new Vector2 (0.6f, 0.6f);
					}
					if (i == 1) {
						EnemyCard.GetComponent <RectTransform> ().anchorMin = new Vector2 (0.1f, 0.4f);
						EnemyCard.GetComponent <RectTransform> ().anchorMax = new Vector2 (0.3f, 0.6f);
					}
					if (i == 2) {
						EnemyCard.GetComponent <RectTransform> ().anchorMin = new Vector2 (0.7f, 0.4f);
						EnemyCard.GetComponent <RectTransform> ().anchorMax = new Vector2 (0.9f, 0.6f);
					}
				}
				if (SpawnCount == 4) {
					if (i == 0) {
						EnemyCard.GetComponent <RectTransform> ().anchorMin = new Vector2 (0.025f, 0.4f);
						EnemyCard.GetComponent <RectTransform> ().anchorMax = new Vector2 (0.225f, 0.6f);
					}
					if (i == 1) {
						EnemyCard.GetComponent <RectTransform> ().anchorMin = new Vector2 (0.275f, 0.4f);
						EnemyCard.GetComponent <RectTransform> ().anchorMax = new Vector2 (0.475f, 0.6f);
					}
					if (i == 2) {
						EnemyCard.GetComponent <RectTransform> ().anchorMin = new Vector2 (0.525f, 0.4f);
						EnemyCard.GetComponent <RectTransform> ().anchorMax = new Vector2 (0.725f, 0.6f);
					}
					if (i == 3) {
						EnemyCard.GetComponent <RectTransform> ().anchorMin = new Vector2 (0.775f, 0.4f);
						EnemyCard.GetComponent <RectTransform> ().anchorMax = new Vector2 (0.975f, 0.6f);
					}
				}
				EnemyCard.GetComponent <RectTransform> ().offsetMin = new Vector2 (0, 0);
				EnemyCard.GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
			}
		}
		if ((gameObject.GetComponent <PlayerStats> ().FloorCount + 1) % 20 == 0) {
			StartCoroutine (PlayerStats.Action ("Something is coming..!", new Color (1, 0, 0)));
			yield return new WaitForSeconds (1);
			CurrentTurn = "Player";
			GameObject BossCard = null;
			int Chosen = (int)Random.Range (1, 2);
			if (Chosen == 0) {
				BossCard = (GameObject)Instantiate (Resources.Load ("Cards/Bosses/Garpy"));
			}
			if (Chosen == 1) {
				BossCard = (GameObject)Instantiate (Resources.Load ("Cards/Bosses/Slertch"));
			}
			BossCard.transform.SetParent (transform.Find ("Enemies"));
			BossCard.GetComponent <RectTransform> ().anchorMin = new Vector2 (0.35f, 0.4f);
			BossCard.GetComponent <RectTransform> ().anchorMax = new Vector2 (0.65f, 0.7f);
			BossCard.GetComponent <RectTransform> ().offsetMin = new Vector2 (0, 0);
			BossCard.GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
        }
        SaveScript.SaveGame();
        TurnInPlay = false;
		Spawning = false;
		ShownAd = false;
	}

	private IEnumerator ShowAdWhenReady(string Advert)
	{
		while (!Advertisement.IsReady(Advert))
		{
			yield return new WaitForSeconds(0.25f);
		}

		Advertisement.Show(Advert);
		ShownAd = false;
	}

	void Start ()
	{
		Advertisement.Initialize("3019218", false);
		if (PlayerPrefs.HasKey("BETA_SaveData"))
        {
            StartCoroutine(SaveScript.LoadGame());
            gameObject.GetComponent<AttackScript>().NewCard(PlayerPrefs.GetString("Card4"), transform.Find("HUD").Find("ActionCards").GetChild(4));
            HUD.Find("ActionCards").GetChild(4).GetComponent<UseActionCard>().TurnsHeld = PlayerPrefs.GetInt("Card4Floors");
            if (HUD.Find("ActionCards").GetChild(4).GetComponent<UseActionCard>().TurnsHeld >= 7)
            {
                HUD.Find("ActionCards").GetChild(4).GetComponent<UseActionCard>().SuperCharge();
            }
            gameObject.GetComponent<AttackScript>().NewCard(PlayerPrefs.GetString("Card3"), transform.Find("HUD").Find("ActionCards").GetChild(3));
            HUD.Find("ActionCards").GetChild(3).GetComponent<UseActionCard>().TurnsHeld = PlayerPrefs.GetInt("Card3Floors");
            if (HUD.Find("ActionCards").GetChild(3).GetComponent<UseActionCard>().TurnsHeld >= 7)
            {
                HUD.Find("ActionCards").GetChild(3).GetComponent<UseActionCard>().SuperCharge();
            }
            gameObject.GetComponent<AttackScript>().NewCard(PlayerPrefs.GetString("Card2"), transform.Find("HUD").Find("ActionCards").GetChild(2));
            HUD.Find("ActionCards").GetChild(2).GetComponent<UseActionCard>().TurnsHeld = PlayerPrefs.GetInt("Card2Floors");
            if (HUD.Find("ActionCards").GetChild(2).GetComponent<UseActionCard>().TurnsHeld >= 7)
            {
                HUD.Find("ActionCards").GetChild(2).GetComponent<UseActionCard>().SuperCharge();
            }
            gameObject.GetComponent<AttackScript>().NewCard(PlayerPrefs.GetString("Card1"), transform.Find("HUD").Find("ActionCards").GetChild(1));
            HUD.Find("ActionCards").GetChild(1).GetComponent<UseActionCard>().TurnsHeld = PlayerPrefs.GetInt("Card1Floors");
            if (HUD.Find("ActionCards").GetChild(1).GetComponent<UseActionCard>().TurnsHeld >= 7)
            {
                HUD.Find("ActionCards").GetChild(1).GetComponent<UseActionCard>().SuperCharge();
            }
            gameObject.GetComponent<AttackScript>().NewCard(PlayerPrefs.GetString("Card0"), transform.Find("HUD").Find("ActionCards").GetChild(0));
            HUD.Find("ActionCards").GetChild(0).GetComponent<UseActionCard>().TurnsHeld = PlayerPrefs.GetInt("Card0Floors");
            if (HUD.Find("ActionCards").GetChild(0).GetComponent<UseActionCard>().TurnsHeld >= 7)
            {
                HUD.Find("ActionCards").GetChild(0).GetComponent<UseActionCard>().SuperCharge();
            }
        }
        else
        {
			PlayerPrefs.SetFloat("BETA_BabyMode", 1);
			gameObject.GetComponent<AttackScript>().NewCard("None", transform.Find("HUD").Find("ActionCards").GetChild(4));
            gameObject.GetComponent<AttackScript>().NewCard("None", transform.Find("HUD").Find("ActionCards").GetChild(3));
            gameObject.GetComponent<AttackScript>().NewCard("None", transform.Find("HUD").Find("ActionCards").GetChild(2));
            gameObject.GetComponent<AttackScript>().NewCard("None", transform.Find("HUD").Find("ActionCards").GetChild(1));
            gameObject.GetComponent<AttackScript>().NewCard("None", transform.Find("HUD").Find("ActionCards").GetChild(0));
        }
	}

	IEnumerator CheckPlayerDamage ()
	{
		for (int i = 0; i < HUD.Find("ActionCards").childCount; i++)
		{
			HUD.Find("ActionCards").GetChild(i).GetComponent<UseActionCard>().TurnsHeld += 1;
			if (HUD.Find("ActionCards").GetChild(i).GetComponent<UseActionCard>().TurnsHeld >= 7)
			{
				HUD.Find("ActionCards").GetChild(i).GetComponent<UseActionCard>().SuperCharge();
			}
		}
		if (gameObject.GetComponent <PlayerStats> ().Poisoned > 0) {
			int Damage = gameObject.GetComponent<PlayerStats> ().Poisoned;
			Damage /= gameObject.GetComponent<PlayerStats>().PoisonResistance / 100;
			gameObject.GetComponent<PlayerStats>().Health -= Damage;
			if (gameObject.GetComponent<PlayerStats>().Health > 0)
			{
				StartCoroutine(PlayerStats.Action("You've been weakened by " + Damage + " HP!", new Color(1, 0, 1)));
			} else StartCoroutine(PlayerStats.Action("You withered away!", new Color(1, 0, 1)));
			gameObject.GetComponent<PlayerStats>().Poisoned -= 1;
			if (gameObject.GetComponent<PlayerStats>().Poisoned > 3)
			{
				gameObject.GetComponent<PlayerStats>().Poisoned = 2;
			}
			yield return new WaitForSeconds (1.5f);
		}
		if (gameObject.GetComponent <PlayerStats> ().OnFire > 0) {
			int Damage = 0;
            if (Random.Range (0, gameObject.GetComponent <PlayerStats> ().FireResistance) <= 100)
            {
                Damage = 1;
                if (gameObject.GetComponent<PlayerStats>().Shield > 0)
                {
                    gameObject.GetComponent<PlayerStats>().Shield -= 1;
                    StartCoroutine(PlayerStats.Action("Your armor burnt for 1 damage!", new Color(1, 0.5f, 0)));
                }
                else if (gameObject.GetComponent<PlayerStats>().Health > 1)
                {
                    gameObject.GetComponent<PlayerStats>().Health -= Damage;
                    StartCoroutine(PlayerStats.Action("You burnt for 1 damage!", new Color(1, 0, 1)));
                    StartCoroutine(PlayerStats.DamagePlayer());
                }
                else
                {
                    gameObject.GetComponent<PlayerStats>().Health -= Damage;
                    StartCoroutine(PlayerStats.Action("You burnt to a crisp!", new Color(1, 0, 1)));
                    StartCoroutine(PlayerStats.DamagePlayer());
                }
                gameObject.GetComponent<PlayerStats>().OnFire -= 1;
                if (gameObject.GetComponent<PlayerStats>().OnFire > 5)
                {
                    gameObject.GetComponent<PlayerStats>().OnFire = 4;
                }
            }
			yield return new WaitForSeconds (1.5f);
		}
		CurrentTurn = "Enemy1";
        SaveScript.SaveGame();
		yield return null;
	}

	void Update () {
		Strength = PlayerPrefs.GetFloat("BETA_BabyMode") * FloorDifficulty;
		HUD.Find ("Crossbow").Find ("Ammo").GetComponent <Text> ().text = gameObject.GetComponent <AttackScript> ().Bolts + "/" + gameObject.GetComponent <AttackScript> ().MaxBolts + " Bolts";
		if (CurrentTurn == "None") {
			gameObject.GetComponent <AttackScript> ().BoltsShoot = 1;
			if (gameObject.GetComponent <PlayerStats> ().Crossbow == "SmallCrossbow") {
				gameObject.GetComponent <AttackScript> ().BoltsShoot += 1;
			}
			CurrentTurn = "Player";
			if (Spawning == false) {
				Spawning = true;
				StartCoroutine (SpawnEnemies ());
			}
		}
		if (CurrentTurn == "PlayerDone") {
			if (Enemies.childCount > 0) {
				CurrentTurn = "CheckingDamage";
				StartCoroutine (CheckPlayerDamage ());
			}
		}
        for (int i = 0; i < transform.Find ("Notifications").childCount; i++)
        {
            RectTransform Notif = transform.Find("Notifications").GetChild(i).GetComponent<RectTransform>();
            Notif.anchorMin = new Vector2(0.01f, 0.65f + 0.05f * i);
            Notif.anchorMax = new Vector2(0.31f, 0.7f + 0.05f * i);
            Notif.offsetMin = new Vector2(0, 0);
            Notif.offsetMax = new Vector2(0, 0);
        }

		// DEATH IS HERE -----------------//

		if (gameObject.GetComponent <PlayerStats> ().Health <= 0) {
			gameObject.GetComponent <PlayerStats> ().FloorCount = 0;
			gameObject.GetComponent <PlayerStats> ().Health = 10;
			gameObject.GetComponent <PlayerStats> ().MaxHealth = 10;
            gameObject.GetComponent<PlayerStats>().Willpower = 0;
			gameObject.GetComponent<PlayerStats>().MaxWillpower = 20;
			gameObject.GetComponent<PlayerStats>().Experience = 0;
			gameObject.GetComponent<PlayerStats>().Level = 1;
			gameObject.GetComponent<PlayerStats>().Shield = 2;
            gameObject.GetComponent<PlayerStats>().MaxShield = 2;
            gameObject.GetComponent <PlayerStats> ().Poisoned = 0;
			gameObject.GetComponent <PlayerStats> ().OnFire = 0;
            gameObject.GetComponent<AttackScript> ().Target = null;
            gameObject.GetComponent <AttackScript> ().ActiveCard = null;
            gameObject.GetComponent<AttackScript>().Bolts = 10;
            gameObject.GetComponent<AttackScript>().MaxBolts = 10;
            gameObject.GetComponent<AttackScript>().BoltsShoot = 1;
            Transform ItemInventory = gameObject.GetComponent<PlayerStats>().Inventory.GetComponent<InventoryManager>().ItemInventory;
			ItemInventory.parent.Find("Stats").GetComponent<StatsManagerScript>().SP = 0;
            Shop.GetComponent<ShopManager>().Coins = 10;
            for (int i = 1; i < ItemInventory.childCount; i++)
            {
                if (ItemInventory.GetChild(i).Find("Item"))
                {
                    ItemInventory.GetChild(i).Find("Item").GetComponent<BasicItemScript>().Amount = ItemInventory.GetChild(i).Find("Item").GetComponent<BasicItemScript>().MaxAmount;
                }
            }

            CurrentTurn = "Player";
			if (Spawning == false) {
				Spawning = true;
				gameObject.GetComponent <AttackScript> ().NewCard ("None", transform.Find ("HUD").Find ("ActionCards").GetChild (4));
				gameObject.GetComponent <AttackScript> ().NewCard ("None", transform.Find ("HUD").Find ("ActionCards").GetChild (3));
				gameObject.GetComponent <AttackScript> ().NewCard ("None", transform.Find ("HUD").Find ("ActionCards").GetChild (2));
				gameObject.GetComponent <AttackScript> ().NewCard ("None", transform.Find ("HUD").Find ("ActionCards").GetChild (1));
				gameObject.GetComponent <AttackScript> ().NewCard ("None", transform.Find ("HUD").Find ("ActionCards").GetChild (0));
				StartCoroutine (SpawnEnemies ());
			}
			if (!ShownAd)
			{
				StartCoroutine(ShowAdWhenReady("OnDeathAdvertisement"));
				ShownAd = true;
			}
			SaveScript.SaveGame();
		}



		if (Enemies.childCount >= 1 && CurrentTurn == "Enemy1" && TurnInPlay == false) {
			TurnInPlay = true;
			StartAI (Enemies.GetChild (0).transform);
		}
		if (Enemies.childCount >= 2 && CurrentTurn == "Enemy2" && TurnInPlay == false) {
			TurnInPlay = true;
			StartAI (Enemies.GetChild (1).transform);		
		}
		if (Enemies.childCount >= 3 && CurrentTurn == "Enemy3" && TurnInPlay == false) {
			TurnInPlay = true;
			StartAI (Enemies.GetChild (2).transform);
		}
		if (Enemies.childCount >= 4 && CurrentTurn == "Enemy4" && TurnInPlay == false) {
			TurnInPlay = true;
			StartAI (Enemies.GetChild (3).transform);
		}
		if ((CurrentTurn == "Enemy1" || CurrentTurn == "Enemy2" || CurrentTurn == "Enemy3" || CurrentTurn == "Enemy4") && TurnInPlay == false) {
			CurrentTurn = "Player";
			gameObject.GetComponent <AttackScript> ().BoltsShoot = 1;
			if (gameObject.GetComponent <PlayerStats> ().Crossbow == "SmallCrossbow") {
				gameObject.GetComponent <AttackScript> ().BoltsShoot += 1;
			}
		}
		if (Enemies.childCount <= 0 && CurrentTurn != "Player" && CurrentTurn != "PlayerWorking" && CurrentTurn != "ChoosingMap" && CurrentTurn != "None") {
            PlayerPrefs.SetString("Enemy0", "None");
			CurrentTurn = "ChoosingMap";
			if (((float)gameObject.GetComponent<PlayerStats>().FloorCount + 2) % 20 != 0)
			{
				StartCoroutine(Map.GetComponent<MapManagerScript>().SpawnDoors(0));
			} else if (((float)gameObject.GetComponent<PlayerStats>().FloorCount + 2) % 20 == 0)
			{
				StartCoroutine(Map.GetComponent<MapManagerScript>().SpawnDoors(1));
			}
			Map.gameObject.SetActive (true);
		}
	}
}
