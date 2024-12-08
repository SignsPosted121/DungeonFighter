using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackScript : MonoBehaviour {

	public Transform ActiveCard;
	public Transform Target;
	public Transform CardInv;
	public int Bolts = 10;
	public int MaxBolts = 20;
	public int BoltsShoot = 1;

	private List <GameObject> Choices = new List <GameObject> ();

	void SelectCrossbow () {
		if (BoltsShoot > 0 && Bolts > 0 && (ActiveCard == null || ActiveCard.name != "Crossbow")) {
			ActiveCard = transform.Find ("HUD").Find ("Crossbow"); return;
		}
		if (ActiveCard != null && ActiveCard.name == "Crossbow") {
			ActiveCard = null; return;
		}
	}

	public void NewCard (string Card, Transform Replacement) {
		GameObject ActionCard = new GameObject ();
		Destroy (ActionCard);
		if (Card == "None") {
			Choices.Clear ();
			for (int i = 0; i < CardInv.childCount; i++) {
				if (CardInv.GetChild (i).GetComponent <SelectedCard> () != null && CardInv.GetChild (i).GetComponent <SelectedCard> ().Selected != null) {
					GameObject ChosenCard = CardInv.GetChild (i).GetComponent <SelectedCard> ().Selected;
					for (int x = 0; x < ChosenCard.GetComponent <UseActionCard> ().Chance; x++) {
						Choices.Add (ChosenCard);
					}
				}
			}
			GameObject FinalCard = Choices [Random.Range (1, Choices.Count)];
			ActionCard = (GameObject)Instantiate (Resources.Load ("Cards/Actions/" + FinalCard.name));
		} else ActionCard = (GameObject)Instantiate (Resources.Load ("Cards/Actions/" + Card));

		int Indexing = 0;
		if ((int)Replacement.transform.rotation.eulerAngles.z == 5) {
			Indexing = 1;
		}
		if ((int)Replacement.transform.rotation.eulerAngles.z == 0) {
			Indexing = 2;
		}
		if ((int)Replacement.transform.rotation.eulerAngles.z == 355) {
			Indexing = 3;
		}
		if ((int)Replacement.transform.rotation.eulerAngles.z == 350) {
			Indexing = 4;
		}
		ActionCard.transform.SetParent (Replacement.parent);
		ActionCard.transform.rotation = Replacement.rotation;
		ActionCard.GetComponent <RectTransform> ().anchorMin = Replacement.GetComponent <RectTransform> ().anchorMin;
		ActionCard.GetComponent <RectTransform> ().anchorMax = Replacement.GetComponent <RectTransform> ().anchorMax;
		ActionCard.GetComponent <RectTransform> ().offsetMin = new Vector2 (0, 0);
		ActionCard.GetComponent <RectTransform> ().offsetMax = new Vector2 (0, 0);
		ActionCard.GetComponent <RectTransform> ().localScale = new Vector3 (1, 1, 1);
		Destroy (Replacement.gameObject);
		Target = null;
		ActionCard.transform.SetSiblingIndex (Indexing);
	}

	IEnumerator CheckThrough () {
		if (gameObject.GetComponent <FightManager> ().CurrentTurn == "Player" && ActiveCard != null) {
			if (ActiveCard.GetComponent <UseActionCard> () != null && gameObject.GetComponent <PlayerStats> ().Willpower < ActiveCard.GetComponent <UseActionCard> ().Willpower) {
				transform.Find ("HUD").Find ("Willpower").Find ("Card").GetComponent <CardMouseOver> ().Bulge (0);
				ActiveCard = null;
				Target = null;
			}

			if (ActiveCard != null && ActiveCard.name == "Crossbow") {
				PlayerStats PStats = gameObject.GetComponent <PlayerStats> ();
				if (BoltsShoot > 0 && Target != null) {
					if (PStats.Crossbow == "BasicCrossbow" || PStats.Crossbow == "SmallCrossbow") {
						BoltsShoot -= 1;
						Bolts -= 1;
						Target.GetComponent<EnemyStats>().TakeDamage(1);
						Target.Find("Health").GetComponent<AudioSource>().Play();
						StartCoroutine (PlayerStats.Action ("Your crossbow deals 1 damage to " + Target.Find ("CardName").GetComponent <Text> ().text + "!", new Color (1, 1, 1)));
					}
					if (PStats.Crossbow == "StrongCrossbow") {
						BoltsShoot -= 1;
						Bolts -= 1;
						int Damage = (int) Random.Range (1, 3);
						Target.GetComponent<EnemyStats>().TakeDamage(Damage);
						StartCoroutine (PlayerStats.Action ("Your crossbow deals " + Damage + " damage to " + Target.Find ("CardName").GetComponent <Text> ().text + "!", new Color (1, 1, 1)));
					}
					if (PStats.Crossbow == "DoubleCrossbow") {
						Transform Enemies = transform.Find ("Enemies");
						BoltsShoot -= 1;
						Bolts -= 1;
						Transform Target2 = Enemies.GetChild ((int) Random.Range (0, Enemies.childCount - 1));
						Target.GetComponent<EnemyStats>().TakeDamage(1);
						Target2.GetComponent<EnemyStats>().TakeDamage(1);
                        StartCoroutine (PlayerStats.Action ("Your crossbow deals 1 damage to " + Target.Find ("CardName").GetComponent <Text> ().text + " and " + Target2.Find ("CardName").GetComponent <Text> ().text + "!", new Color (1, 1, 1)));
					}
					if (PStats.Crossbow == "FireCrossbow") {
						BoltsShoot -= 1;
						Bolts -= 1;
						int Damage = (int) (Random.Range (0, 3 / ((float) Target.GetComponent<EnemyStats>().FireResistance / 100)));
						Target.GetComponent<EnemyStats>().TakeDamage(1);
						Target.GetComponent <EnemyStats> ().OnFire += Damage;
						StartCoroutine (PlayerStats.Action ("Your crossbow deals 1 damage to " + Target.Find ("CardName").GetComponent <Text> ().text + ", and sets them on fire for " + Damage + " turns!", new Color (1, 1, 1)));
					}
					if (PStats.Crossbow == "PoisonousCrossbow") {
						BoltsShoot -= 1;
						Bolts -= 1;
						int Damage = (int) (Random.Range (0, 3 / ((float)Target.GetComponent<EnemyStats>().FireResistance / 100)));
						Target.GetComponent<EnemyStats>().TakeDamage(1);
						Target.GetComponent <EnemyStats> ().Poisoned += Damage;
						StartCoroutine (PlayerStats.Action ("Your crossbow deals 1 damage to " + Target.Find ("CardName").GetComponent <Text> ().text + ", and poisons them for " + Damage + " turns!", new Color (1, 1, 1)));
					}
					ActiveCard = null;
					Target = null;
				}
			}

			if (ActiveCard != null && ActiveCard.GetComponent <UseActionCard> () != null) {
				if (ActiveCard.Find ("CardName").GetComponent <Text> ().text == "Defend" && gameObject.GetComponent <PlayerStats> ().Willpower >= ActiveCard.GetComponent <UseActionCard> ().Willpower) {
                    if (gameObject.GetComponent<PlayerStats>().Shield < gameObject.GetComponent<PlayerStats>().MaxShield)
                    {
                        if (ActiveCard.GetComponent<UseActionCard>().SuperCharged == false)
                        {
                            StartCoroutine(PlayerStats.Action("Shielded from one incoming attack!", new Color(1, 1, 1)));
                            gameObject.GetComponent<PlayerStats>().Shield += 1;
                            if (gameObject.GetComponent<PlayerStats>().Shield > gameObject.GetComponent<PlayerStats>().MaxShield)
                            {
                                gameObject.GetComponent<PlayerStats>().Shield = gameObject.GetComponent<PlayerStats>().MaxShield;
                            }
                        } else
                        {
                            StartCoroutine(PlayerStats.Action("Shielded from 2 incoming attacks!", new Color(1, 1, 1)));
                            gameObject.GetComponent<PlayerStats>().Shield += 2;
                            if (gameObject.GetComponent<PlayerStats>().Shield > gameObject.GetComponent<PlayerStats>().MaxShield)
                            {
                                gameObject.GetComponent<PlayerStats>().Shield = gameObject.GetComponent<PlayerStats>().MaxShield;
                            }
                        }
                        gameObject.GetComponent<PlayerStats>().Willpower -= ActiveCard.GetComponent<UseActionCard>().Willpower;
                        gameObject.GetComponent<FightManager>().CurrentTurn = "PlayerWorking";
                        NewCard("None", ActiveCard);
                        yield return new WaitForSeconds(1.5f);
                        gameObject.GetComponent<FightManager>().CurrentTurn = "PlayerDone";
                    }
				}
				if (ActiveCard != null && ActiveCard.Find ("CardName").GetComponent <Text> ().text == "Whirlwind" && gameObject.GetComponent <PlayerStats> ().Willpower >= ActiveCard.GetComponent <UseActionCard> ().Willpower)
                {
                    int Damage = 2;
                    if (ActiveCard.GetComponent<UseActionCard>().SuperCharged == false)
                    {
                        Damage = (int)Random.Range(1, 4);
                    } else
                    {
                        Damage = (int)Random.Range(2, 6);
                    }
					StartCoroutine (PlayerStats.Action ("Whirlwind damages all enemies for around " + Damage + " HP!", new Color (1, 1, 1)));
					for (int i = 0; i < transform.Find ("Enemies").childCount; i++)
					{
						transform.Find("Enemies").GetChild(i).GetComponent<EnemyStats>().TakeDamage((int)((float)Damage / ((float)transform.Find("Enemies").GetChild(i).GetComponent<EnemyStats>().WindResistance / 100)));
						StartCoroutine (transform.Find ("Enemies").GetChild (i).GetComponent <EnemyStats> ().GrowThis (1));
					}
					gameObject.GetComponent <PlayerStats> ().Willpower -= ActiveCard.GetComponent <UseActionCard> ().Willpower;
					gameObject.GetComponent <FightManager> ().CurrentTurn = "PlayerWorking";
					NewCard ("None", ActiveCard);
					yield return new WaitForSeconds (1.5f);
					gameObject.GetComponent <FightManager> ().CurrentTurn = "PlayerDone";
				}
				if (ActiveCard != null && ActiveCard.Find ("CardName").GetComponent <Text> ().text == "Heal" && gameObject.GetComponent <PlayerStats> ().Willpower >= ActiveCard.GetComponent <UseActionCard> ().Willpower)
                {
                    if (gameObject.GetComponent<PlayerStats>().Health < gameObject.GetComponent<PlayerStats>().MaxHealth)
                    {
                        int Damage = 3;
                        if (ActiveCard.GetComponent<UseActionCard>().SuperCharged == false)
                        {
                            Damage = (int)Random.Range(1, 6);
                        } else
                        {
                            Damage = (int)Random.Range(3, 8);
                        }
                        gameObject.GetComponent<PlayerStats>().Health += Damage;
                        if (gameObject.GetComponent<PlayerStats>().Health > gameObject.GetComponent<PlayerStats>().MaxHealth)
                        {
                            gameObject.GetComponent<PlayerStats>().Health = gameObject.GetComponent<PlayerStats>().MaxHealth;
                        }
                        StartCoroutine(PlayerStats.Action("Healed " + Damage + " HP!", new Color(1, 1, 1)));
                        gameObject.GetComponent<PlayerStats>().Willpower -= ActiveCard.GetComponent<UseActionCard>().Willpower;
                        gameObject.GetComponent<FightManager>().CurrentTurn = "PlayerWorking";
                        NewCard("None", ActiveCard);
                        yield return new WaitForSeconds(1.5f);
                        gameObject.GetComponent<FightManager>().CurrentTurn = "PlayerDone";
                    }
				}
				if (Target != null) {
					if (ActiveCard != null && ActiveCard.Find ("CardName").GetComponent <Text> ().text == "Bash" && gameObject.GetComponent <PlayerStats> ().Willpower >= ActiveCard.GetComponent <UseActionCard> ().Willpower)
                    {
                        int Damage = 2;
                        if (ActiveCard.GetComponent<UseActionCard>().SuperCharged == false)
                        {
                            Damage = (int)Random.Range(1, 3);
                        } else
                        {
                            Damage = (int)Random.Range(1, 6);
                        }
						StartCoroutine (PlayerStats.Action ("Bash damages " + Target.Find ("CardName").GetComponent <Text> ().text + " for " + Damage + " HP!", new Color (1, 1, 1)));
						Target.GetComponent<EnemyStats>().TakeDamage(Damage);
						StartCoroutine (Target.GetComponent <EnemyStats> ().GrowThis (1));
						gameObject.GetComponent <PlayerStats> ().Willpower -= ActiveCard.GetComponent <UseActionCard> ().Willpower;
						gameObject.GetComponent <FightManager> ().CurrentTurn = "PlayerWorking";
						NewCard ("None", ActiveCard);
						yield return new WaitForSeconds (1.5f);
						gameObject.GetComponent <FightManager> ().CurrentTurn = "PlayerDone";
					}
					if (ActiveCard != null && ActiveCard.Find ("CardName").GetComponent <Text> ().text == "Slash" && gameObject.GetComponent <PlayerStats> ().Willpower >= ActiveCard.GetComponent <UseActionCard> ().Willpower) {
                        int Damage = 2;
                        if (ActiveCard.GetComponent<UseActionCard>().SuperCharged == false)
                        {
                            Damage = (int)Random.Range(1, 4);
                        } else
                        {
                            Damage = (int)Random.Range(2, 5);
                        }
                        StartCoroutine (PlayerStats.Action ("Slash damages " + Target.Find ("CardName").GetComponent <Text> ().text + " for " + Damage + " HP!", new Color (1, 1, 1)));
						Target.GetComponent<EnemyStats>().TakeDamage(Damage);
						StartCoroutine (Target.GetComponent <EnemyStats> ().GrowThis (1));
						gameObject.GetComponent <PlayerStats> ().Willpower -= ActiveCard.GetComponent <UseActionCard> ().Willpower;
						gameObject.GetComponent <FightManager> ().CurrentTurn = "PlayerWorking";
						NewCard ("None", ActiveCard);
						yield return new WaitForSeconds (1.5f);
						gameObject.GetComponent <FightManager> ().CurrentTurn = "PlayerDone";
					}
					if (ActiveCard != null && ActiveCard.Find ("CardName").GetComponent <Text> ().text == "Fireball" && gameObject.GetComponent <PlayerStats> ().Willpower >= ActiveCard.GetComponent <UseActionCard> ().Willpower)
                    {
                        int Damage = 1;
                        int Damage2 = 1;
                        if (ActiveCard.GetComponent<UseActionCard>().SuperCharged == false)
                        {
                            Damage = (int)Random.Range(0, 3);
                            Damage2 = (int)Random.Range(1, 3);
                        } else
                        {
                            Damage = (int)Random.Range(1, 4);
                            Damage2 = (int)Random.Range(2, 5);
                        }
						StartCoroutine (PlayerStats.Action ("Fireball damages " + Target.Find ("CardName").GetComponent <Text> ().text + " for " + Damage + " HP, and sets them on fire for " + Damage2 + " turns!", new Color (1, 1, 1)));
						Target.GetComponent<EnemyStats>().TakeDamage(Damage);
						Target.GetComponent <EnemyStats> ().OnFire += Damage2;
						StartCoroutine (Target.GetComponent <EnemyStats> ().GrowThis (1));
						gameObject.GetComponent <PlayerStats> ().Willpower -= ActiveCard.GetComponent <UseActionCard> ().Willpower;
						gameObject.GetComponent <FightManager> ().CurrentTurn = "PlayerWorking";
						NewCard ("None", ActiveCard);
						yield return new WaitForSeconds (1.5f);
						gameObject.GetComponent <FightManager> ().CurrentTurn = "PlayerDone";
					}
					if (ActiveCard != null && ActiveCard.Find ("CardName").GetComponent <Text> ().text == "Poison Stab" && gameObject.GetComponent <PlayerStats> ().Willpower >= ActiveCard.GetComponent <UseActionCard> ().Willpower)
                    {
                        int Damage = 1;
                        int Damage2 = 1;
                        if (ActiveCard.GetComponent<UseActionCard>().SuperCharged == false)
                        {
                            Damage = (int)Random.Range(0, 3);
                            Damage2 = (int)Random.Range(1, 3);
                        }
                        else
                        {
                            Damage = (int)Random.Range(1, 4);
                            Damage2 = (int)Random.Range(2, 5);
                        }
                        StartCoroutine (PlayerStats.Action ("Poison Stab damages " + Target.Find ("CardName").GetComponent <Text> ().text + " for " + Damage + " HP, and poisons them for " + Damage2 + " turns!", new Color (1, 1, 1)));
						Target.GetComponent<EnemyStats>().TakeDamage(Damage);
						Target.GetComponent <EnemyStats> ().Poisoned += Damage2;
						StartCoroutine (Target.GetComponent <EnemyStats> ().GrowThis (1));
						gameObject.GetComponent <PlayerStats> ().Willpower -= ActiveCard.GetComponent <UseActionCard> ().Willpower;
						gameObject.GetComponent <FightManager> ().CurrentTurn = "PlayerWorking";
						NewCard ("None", ActiveCard);
						yield return new WaitForSeconds (1.5f);
						gameObject.GetComponent <FightManager> ().CurrentTurn = "PlayerDone";
					}
				}
			}
		}
		yield return new WaitForSeconds (0);
	}

	void Start () {
		transform.Find ("HUD").Find ("Crossbow").GetComponent <Button> ().onClick.AddListener (SelectCrossbow);
	}

	void Update () {
		StartCoroutine (CheckThrough ());
	}
}
