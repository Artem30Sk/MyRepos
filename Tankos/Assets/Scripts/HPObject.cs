using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPObject : MonoBehaviour
{
	[SerializeField] private float _HP = 100;
	[SerializeField] private bool autoDestroy;

	public float currentHP
	{
		get { return _HP; }
	}

	public void Adjust(float value)
	{
		_HP += value;

		if (autoDestroy && _HP <= 0)
		{
			Destroy(gameObject);
		}
	}
}
