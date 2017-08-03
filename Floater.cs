using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour {

	public bool Float = true;
	public Vector3 MoveVector = Vector3.up;
	public float MoveRange = 2.0f;
	public float MoveSpeed = 2.0f;

	private Floater floatObject;

	Vector3 startPosition;

	void Start () {
		floatObject = this;
		startPosition = floatObject.transform.position;
	}
	
	void Update () {
		if (Float) {
			floatObject.transform.position = startPosition + MoveVector * (MoveRange * Mathf.Sin(Time.timeSinceLevelLoad * MoveSpeed));
		}
	}
}
