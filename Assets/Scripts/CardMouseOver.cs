using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardMouseOver : MonoBehaviour {

	public bool AttackScriptDependent = true;

	IEnumerator StartBulge (int Negative) {
		if (transform.name == "Crossbow") {
			for (int i = 0; i < 5; i++) {
				gameObject.GetComponent <RectTransform> ().localScale += new Vector3 (0.02f * Negative, 0.02f * Negative, 0);
				yield return new WaitForSeconds (0.01f);
			}
		}
		if (transform.parent.name == "ActionCards") {
			yield return new WaitForSeconds (0.01f);
			if ((int) Negative == -1) {
				if ((int)transform.rotation.eulerAngles.z == 10) {
					transform.SetSiblingIndex (0);
				}
				if ((int)transform.rotation.eulerAngles.z == 5) {
					transform.SetSiblingIndex (1);
				}
				if ((int)transform.rotation.eulerAngles.z == 0) {
					transform.SetSiblingIndex (2);
				}
				if ((int)transform.rotation.eulerAngles.z == 355) {
					transform.SetSiblingIndex (3);
				}
				if ((int)transform.rotation.eulerAngles.z == 350) {
					transform.SetSiblingIndex (4);
				}
			}
			if ((int) Negative == 1) {
				transform.SetAsLastSibling ();
			}
		}
		if ((int) Negative != 0) {
			for (int i = 0; i < 5; i++) {
				gameObject.GetComponent <RectTransform> ().localScale += new Vector3 (0.04f * Negative, 0.04f * Negative, 0);
				yield return new WaitForSeconds (0.01f);
			}
		}
		if ((int) Negative == 0) {
			for (int i = 0; i < 5; i++) {
				gameObject.GetComponent <RectTransform> ().localScale += new Vector3 (0.04f, 0.04f, 0);
				transform.Find ("WillText").GetComponent <Text> ().color = new Color (1, 0, 0);
				yield return new WaitForSeconds (0.01f);
			}
			yield return new WaitForSeconds (0.2f);
			for (int i = 0; i < 5; i++) {
				gameObject.GetComponent <RectTransform> ().localScale -= new Vector3 (0.04f, 0.04f, 0);
				transform.Find ("WillText").GetComponent <Text> ().color = new Color (0, 0.392f, 1);
				yield return new WaitForSeconds (0.01f);
			}
		}
	}

	public void Bulge (int Negative) {
		StartCoroutine (StartBulge (Negative));
	}

	IEnumerator ShineStart (int Negative) {
		StartCoroutine (StartBulge (Negative));
		yield return new WaitForSeconds (0.1f);
		StartCoroutine (StartBulge (-Negative));
	}

	public void Shine (int Negative) {
		StartCoroutine (ShineStart (Negative));
	}

	void Update () {
		if (AttackScriptDependent == true) {
			if (transform.parent.parent.GetComponent <AttackScript> () != null) {
				if (transform.parent.parent.GetComponent <AttackScript> ().ActiveCard == transform) {
					gameObject.GetComponent <Image> ().color = new Color (0.9f, 0.9f, 1f);
				} else gameObject.GetComponent <Image> ().color = new Color (1, 1, 1);
			} else {
				if (transform.parent.parent.parent.GetComponent <AttackScript> () != null && transform.parent.parent.parent.GetComponent <AttackScript> ().ActiveCard == transform) {
					gameObject.GetComponent <Image> ().color = new Color (0.9f, 0.9f, 1f);
				} else gameObject.GetComponent <Image> ().color = new Color (1, 1, 1);
			}
		}
	}

}
