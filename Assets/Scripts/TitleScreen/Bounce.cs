using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{

	public float Speed = 1;
	public float MaxRotation = 10;

	private float CurrentDirection = 1;
	private float Turn = 1;

	void Update()
	{
		transform.eulerAngles += new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Speed * CurrentDirection * Time.deltaTime);
		if (transform.eulerAngles.z > MaxRotation && Turn == 1)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, MaxRotation);
			CurrentDirection = -1f;
			Turn = 2;
		}
		if (transform.eulerAngles.z > MaxRotation && Turn == 2)
		{
			Turn = 3;
		}
		if (transform.eulerAngles.z < 360 - MaxRotation && Turn == 3)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -MaxRotation);
			CurrentDirection = 1f;
			Turn = 4;
		}
		if (transform.eulerAngles.z >= 0 && transform.eulerAngles.z < 360 - MaxRotation && Turn == 4)
		{
			Turn = 1;
		}
	}
}
