using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

	public Transform Enemy;
	public Transform Screen;
	public Text Health;
	public Text Willpower;

	public int Stage = 0;

	IEnumerator ShowHideScreen (int Negative) {
		if (Negative > 0) {
			Screen.gameObject.SetActive (true);
		}
		for (int i = 0; i < 25; i++) {
			Screen.GetComponent <Image> ().color += new Color (0, 0, 0, 0.028f * Negative);
			Screen.Find ("Text").GetComponent <Text> ().color += new Color (0, 0, 0, 0.04f * Negative);
			yield return new WaitForSeconds (0.01f);
		}
		if (Negative < 0) {
			Screen.gameObject.SetActive (false);
		}
	}

	void StartContinue1 () {
		Destroy (transform.Find ("HUD").Find ("ActionCards").Find ("Slash").gameObject);
		Enemy.Find ("Health").GetComponent <Text> ().text = "1/3 HP";
		Health.text = "2/10          HP";
		Willpower.text = "0/50          Will";
		if (Stage == 5) {
			Screen.Find ("Text").GetComponent <Text> ().text = "Good! Usually with most basic damage cards, you would have to click on an enemy as well in order to attack. We'll turn that off for this tutorial.";
			StartCoroutine (ShowHideScreen (1));
			Stage = 6; return;
		}
	}

	void StartContinue2 () {
		Health.text = "2/10          HP";
		Willpower.text = "3/50          Will";
		if (Stage == 9) {
			Screen.Find ("Text").GetComponent <Text> ().text = "Great, now we have some will! But the enemy is going to attack again!";
			StartCoroutine (ShowHideScreen (1));
			Stage = 10; return;
		}
	}

	void StartContinue3 () {
		Destroy (transform.Find ("HUD").Find ("ActionCards").Find ("Defend").gameObject);
		Health.color = new Color (1, 0.5f, 0);
		Willpower.text = "1/50          Will";
		if (Stage == 12) {
			Screen.Find ("Text").GetComponent <Text> ().text = "Shielding up has granted us the ability to deflect all of the slime's attacks, but it's both wasted a turn and will power. Some enemies can attack through the shield, break the shield, or do both!";
			StartCoroutine (ShowHideScreen (1));
			Stage = 13; return;
		}
	}

	void StartContinue4 () {
		Destroy (transform.Find ("HUD").Find ("ActionCards").Find ("Bash").gameObject);
		Enemy.eulerAngles = new Vector3 (90, 0, 0);
		Willpower.text = "0/50          Will";
		if (Stage == 15) {
			Screen.Find ("Text").GetComponent <Text> ().text = "Hurray! You won!";
			StartCoroutine (ShowHideScreen (1));
			Stage = 16; return;
		}
	}

	void Continue () {
		if (Stage == 0) {
			Screen.Find ("Text").GetComponent <Text> ().text = "Welcome to Dungeon Fighter!";
			Stage = 1; return;
		}
		if (Stage == 1) {
			Screen.Find ("Text").GetComponent <Text> ().text = "In this game, you fight monsters of random difficulty and try to pass through as manny floors as possible!";
			Stage = 2; return;
		}
		if (Stage == 2) {
			Screen.Find ("Text").GetComponent <Text> ().text = "Let's learn how to play with a simple slime!";
			Stage = 3; return;
		}
		if (Stage == 3) {
			Screen.Find ("Text").GetComponent <Text> ().text = "Start off by hitting it with a slash!";
			Stage = 4; return;
		}
		if (Stage == 4) {
			StartCoroutine (ShowHideScreen (-1));
			transform.Find ("HUD").Find ("ActionCards").Find ("Slash").GetComponent <Button> ().onClick.AddListener (StartContinue1);
			Stage = 5; return;
		}
		if (Stage == 6) {
			Screen.Find ("Text").GetComponent <Text> ().text = "Slash was fairly effective against the enemy, but now we have no more will and can't attack!";
			Stage = 7; return;
		}
		if (Stage == 7) {
			Screen.Find ("Text").GetComponent <Text> ().text = "The enemy had their turn and attacked, but we have to wait to gain more will. Do so by clicking on the 'Will Power' card (bottom right).";
			Stage = 8; return;
		}
		if (Stage == 8) {
			StartCoroutine (ShowHideScreen (-1));
			Willpower.transform.parent.GetComponent <Button> ().onClick.AddListener (StartContinue2);
			Stage = 9; return;
		}
		if (Stage == 10) {
			Health.text = "1/10          HP";
			Screen.Find ("Text").GetComponent <Text> ().text = "We're going to perish soon! Quick, let's use the 'Defend' card to shield up! Shielding up will reduce incoming damage, but using it multiple times only increases the durability.";
			Stage = 11; return;
		}
		if (Stage == 11) {
			StartCoroutine (ShowHideScreen (-1));
			transform.Find ("HUD").Find ("ActionCards").Find ("Defend").GetComponent <Button> ().onClick.AddListener (StartContinue3);
			Stage = 12; return;
		}
		if (Stage == 13) {
			Health.color = new Color (1, 0, 0);
			Screen.Find ("Text").GetComponent <Text> ().text = "Let's finish this fight with a 'Bash'!";
			Stage = 14; return;
		}
		if (Stage == 14) {
			StartCoroutine (ShowHideScreen (-1));
			transform.Find ("HUD").Find ("ActionCards").Find ("Bash").GetComponent <Button> ().onClick.AddListener (StartContinue4);
			Stage = 15; return;
		}
		if (Stage == 16) {
			Screen.Find ("Text").GetComponent <Text> ().text = "In the real game, there's a lot more effects, cards, and enemies. It can be a much more challenging experience!";
			Stage = 17; return;
		}
		if (Stage == 17) {
			Screen.Find ("Text").GetComponent <Text> ().text = "For the most part, that's the basics of the game. The most notable differences are you have 5 cards instead of 3, and more shield durability.";
			Stage = 18; return;
		}
		if (Stage == 18) {
			Screen.Find ("Text").GetComponent <Text> ().text = "One last thing, make sure you check what a card CAN do first! Stronger cards have higher risk of low damage output, and they cost more will.";
			Stage = 19; return;
		}
		if (Stage == 19) {
			Screen.Find ("Text").GetComponent <Text> ().text = "Good luck, the best way to learn is to get out there and play!";
			Stage = 20; return;
		}
		if (Stage == 20) {
			SceneManager.LoadScene ("Title", LoadSceneMode.Single);
			Stage = 21; return;
		}
	}
		
	void Start () {
		StartCoroutine (ShowHideScreen (1));
		Screen.GetComponent <Button> ().onClick.AddListener (Continue);
	}
}
