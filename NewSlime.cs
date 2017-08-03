using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSlime : MonoBehaviour {
	public GameObject objectToPlace;

	private int numberObjects = 5;
	private int currentObjects = 1;
	private float randomX;
	private float randomZ;
	private Renderer render;
	private LayerMask teleportLayer;
	private int death = 1;

	void Start () {
		render = this.GetComponent<Renderer>();
		teleportLayer = LayerMask.GetMask("CanTeleport");
	}
	
	void Update () {
		RaycastHit hit;
		if (currentObjects <= numberObjects) {
			randomX = Random.Range(render.bounds.min.x, render.bounds.max.x);
			randomZ = Random.Range(render.bounds.min.z, render.bounds.max.z);
			if (Physics.Raycast(new Vector3(randomX, render.bounds.max.y, randomZ), -Vector3.up, out hit, teleportLayer)) {
				if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("CanTeleport")) {
					Instantiate(objectToPlace, hit.point, Quaternion.identity);
					currentObjects ++;
				}
			}
		}
	}
}
