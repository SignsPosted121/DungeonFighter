using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManagerScript : MonoBehaviour
{

	public PlayerStats PStats;
	public AttackScript Attack;
	public int SP;

	void ParseFilling (Transform Upgrade)
	{
		for (int i = 0; i < Upgrade.childCount; i++)
		{
			int Max = 0;
			if (Upgrade.parent.name == "MaxHealth")
			{
				Max = PStats.MaxHealth - 10;
				Upgrade.parent.Find("Text").GetComponent<Text>().text = "Max Health : " + PStats.MaxHealth;
			}
			if (Upgrade.parent.name == "MaxShield")
			{
				Max = PStats.MaxShield - 2;
				Upgrade.parent.Find("Text").GetComponent<Text>().text = "Max Shield : " + PStats.MaxShield;
			}
			if (Upgrade.parent.name == "MaxWillpower")
			{
				Max = (PStats.MaxWillpower - 20) / 3;
				Upgrade.parent.Find("Text").GetComponent<Text>().text = "Max Willpower : " + PStats.MaxWillpower;
			}
			if (Upgrade.parent.name == "QuiverSize")
			{
				Max = (Attack.MaxBolts - 10) / 2;
				Upgrade.parent.Find("Text").GetComponent<Text>().text = "Quiver Size : " + Attack.MaxBolts;
			}
			if (i + 1 <= Max + SP)
			{
				Upgrade.GetChild(i).Find("Fill").GetComponent<Image>().color = new Color32(0, 255, 0, 255);
			} else Upgrade.GetChild(i).Find("Fill").GetComponent<Image>().color = new Color32(80, 80, 80, 255);
			if (i + 1 <= Max)
			{
				Upgrade.GetChild(i).Find("Fill").GetComponent<Image>().color = new Color32(251, 193, 39, 255);
			}
		}
	}

	void Upgrade(Transform Upgrade)
	{
		if (Upgrade.parent.name == "MaxHealth")
		{
			if (SP >= 1 && PStats.MaxHealth < 20)
			{
				SP -= 1;
				PStats.MaxHealth += 1;
				SaveScript.SaveGame();
			}
		}
		if (Upgrade.parent.name == "MaxShield")
		{
			if (SP >= 1 && PStats.MaxShield < 6)
			{
				SP -= 1;
				PStats.MaxShield += 1;
				SaveScript.SaveGame();
			}
		}
		if (Upgrade.parent.name == "MaxWillpower")
		{
			if (SP >= 1 && PStats.MaxWillpower < 50)
			{
				SP -= 1;
				PStats.MaxWillpower += 3;
				SaveScript.SaveGame();
			}
		}
		if (Upgrade.parent.name == "QuiverSize")
		{
			if (SP >= 1 && Attack.MaxBolts < 20)
			{
				SP -= 1;
				Attack.MaxBolts += 2;
				SaveScript.SaveGame();
			}
		}
	}

	private void Start()
	{
		transform.Find("Upgrade").Find("MaxHealth").Find("Level").GetComponent<Button>().onClick.AddListener(() => Upgrade(transform.Find("Upgrade").Find("MaxHealth").Find("Level")));
		transform.Find("Upgrade").Find("MaxShield").Find("Level").GetComponent<Button>().onClick.AddListener(() => Upgrade(transform.Find("Upgrade").Find("MaxShield").Find("Level")));
		transform.Find("Upgrade").Find("MaxWillpower").Find("Level").GetComponent<Button>().onClick.AddListener(() => Upgrade(transform.Find("Upgrade").Find("MaxWillpower").Find("Level")));
		transform.Find("Upgrade").Find("QuiverSize").Find("Level").GetComponent<Button>().onClick.AddListener(() => Upgrade(transform.Find("Upgrade").Find("QuiverSize").Find("Level")));
	}

	void Update()
    {
		transform.Find("ExperienceBar").Find("Fill").GetComponent<RectTransform>().localScale = new Vector3(PStats.Experience / ((PStats.Level - 1) * 20 + 50), 1f, 1f);
		transform.Find("Level").GetComponent<Text>().text = "Level : " + PStats.Level;
		transform.Find("Upgrade").Find("StatPoints").GetComponent<Text>().text = "Stat Points : " + SP;
		transform.Find("Resistances").Find("WR").Find("Percent").GetComponent<Text>().text = PStats.WindResistance + "%";
		transform.Find("Resistances").Find("WR").Find("Percent").GetComponent<Text>().color = new Color(2 - (float)PStats.WindResistance / 100, (float)PStats.WindResistance / 100, 1 - (2 - (float)PStats.WindResistance / 100) + (float)PStats.WindResistance / 100 * (2 - (float)PStats.WindResistance / 100));
		transform.Find("Resistances").Find("FR").Find("Percent").GetComponent<Text>().text = PStats.FireResistance + "%";
		transform.Find("Resistances").Find("FR").Find("Percent").GetComponent<Text>().color = new Color(2 - (float)PStats.FireResistance / 100, (float)PStats.FireResistance / 100, 1 - (2 - (float)PStats.FireResistance / 100) + (float)PStats.FireResistance / 100 * (2 - (float)PStats.FireResistance / 100));
		transform.Find("Resistances").Find("WaR").Find("Percent").GetComponent<Text>().text = PStats.WaterResistance + "%";
		transform.Find("Resistances").Find("WaR").Find("Percent").GetComponent<Text>().color = new Color(2 - (float)PStats.WaterResistance / 100, (float)PStats.WaterResistance / 100, 1 - (2 - (float)PStats.WaterResistance / 100) + (float)PStats.WaterResistance / 100 * (2 - (float)PStats.WaterResistance / 100));
		transform.Find("Resistances").Find("PR").Find("Percent").GetComponent<Text>().text = PStats.PoisonResistance + "%";
		transform.Find("Resistances").Find("PR").Find("Percent").GetComponent<Text>().color = new Color(2 - (float)PStats.PoisonResistance / 100, (float)PStats.PoisonResistance / 100, 1 - (2 - (float)PStats.PoisonResistance / 100) + (float)PStats.PoisonResistance / 100 * (2 - (float)PStats.PoisonResistance / 100));
		ParseFilling(transform.Find("Upgrade").Find("MaxHealth").Find("Level"));
		ParseFilling(transform.Find("Upgrade").Find("MaxShield").Find("Level"));
		ParseFilling(transform.Find("Upgrade").Find("MaxWillpower").Find("Level"));
		ParseFilling(transform.Find("Upgrade").Find("QuiverSize").Find("Level"));
	}
}
