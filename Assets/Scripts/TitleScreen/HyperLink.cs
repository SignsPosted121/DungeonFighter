using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperLink : MonoBehaviour
{

	public string Link;

	public void HyperLinkUp()
	{
		Application.OpenURL(Link);
	}
}
