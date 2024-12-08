using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EybyAnimate : MonoBehaviour {

	private bool Animating = false;

	IEnumerator NormalAnimation () {
		Animating = true;
		transform.Find ("EybyTalking2").gameObject.SetActive (false);
		transform.Find ("Eyby3").gameObject.SetActive (false);
		transform.Find ("Eyby1").gameObject.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		transform.Find ("Eyby1").gameObject.SetActive (false);
		transform.Find ("Eyby2").gameObject.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		transform.Find ("Eyby2").gameObject.SetActive (false);
		transform.Find ("Eyby3").gameObject.SetActive (true);
		yield return new WaitForSeconds (0.5f);
		Animating = false;
	}

	public IEnumerator TalkingAnimation () {
		Animating = true;
		transform.Find ("Eyby4").gameObject.SetActive (false);
		transform.Find ("EybyTalking2").gameObject.SetActive (false);
		transform.Find ("EybyTalking1").gameObject.SetActive (true);
		yield return new WaitForSeconds (0.2f);
		transform.Find ("EybyTalking1").gameObject.SetActive (false);
		transform.Find ("EybyTalking2").gameObject.SetActive (true);
		yield return new WaitForSeconds (0.2f);
		transform.Find ("EybyTalking2").gameObject.SetActive (false);
		transform.Find ("EybyTalking1").gameObject.SetActive (true);
		yield return new WaitForSeconds (0.2f);
		transform.Find ("EybyTalking1").gameObject.SetActive (false);
		transform.Find ("EybyTalking2").gameObject.SetActive (true);
		yield return new WaitForSeconds (0.2f);
	}

	IEnumerator Talk () {
		int ChosenDialog = (int)Random.Range (1, 11);
		if (ChosenDialog == 1) {
			transform.Find ("Saying").GetComponent <Text> ().text = "Please buy my wares I'm poor.";
		}
		if (ChosenDialog == 2) {
			transform.Find ("Saying").GetComponent <Text> ().text = "There's lots of monsters, buy potions.";
		}
		if (ChosenDialog == 3) {
			transform.Find ("Saying").GetComponent <Text> ().text = "Need some coin? Too bad.";
		}
		if (ChosenDialog == 4) {
			transform.Find ("Saying").GetComponent <Text> ().text = "I hope you can find something you like!";
		}
		if (ChosenDialog == 5) {
			transform.Find ("Saying").GetComponent <Text> ().text = "My name's Eyby by the way.";
		}
		if (ChosenDialog == 6) {
			transform.Find ("Saying").GetComponent <Text> ().text = "Everything has a use.";
		}
		if (ChosenDialog == 7) {
			transform.Find ("Saying").GetComponent <Text> ().text = "Perhaps my speech is a distraction?";
		}
		if (ChosenDialog == 8) {
			transform.Find ("Saying").GetComponent <Text> ().text = "Did you know there are 6 different crossbows?";
		}
		if (ChosenDialog == 9) {
			transform.Find ("Saying").GetComponent <Text> ().text = "Sell to the needy, take from their body!";
		}
		if (ChosenDialog == 10) {
			transform.Find ("Saying").GetComponent <Text> ().text = "I love my job so much!";
		}
		StartCoroutine (TalkingAnimation ());
		yield return new WaitForSeconds (0.8f);
		StartCoroutine (TalkingAnimation ());
		yield return new WaitForSeconds (0.8f);
		StartCoroutine (TalkingAnimation ());
		yield return new WaitForSeconds (0.8f);
		StartCoroutine (TalkingAnimation ());
		yield return new WaitForSeconds (0.8f);
		StartCoroutine (TalkingAnimation ());
		yield return new WaitForSeconds (0.8f);
		transform.Find ("Saying").GetComponent <Text> ().text = "";
		Animating = false;
	}

	void Update () {
		int Normal = (int)Random.Range (1, 6);
		if (Normal < 5 && Animating == false) {
			Animating = true;
			StartCoroutine (NormalAnimation ());
		}
		if (Normal >= 5 && Animating == false) {
			Animating = true;
			StartCoroutine (Talk ());
		}
	}
}
