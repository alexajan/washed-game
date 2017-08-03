using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInteraction : MonoBehaviour {

	public BoxCollider col;
	public GameObject parent;
	private Bounds bounds;
	private Transform child;
	bool hasBounds = false;

	void Start() {
		col = parent.GetComponent<BoxCollider>();
	}

	void Update() {
		
		FitCollider(col);
			
	}

	public void FitCollider(BoxCollider coll) {
		
		for (int i = 0; i < parent.transform.childCount; i++) {
			if (parent.transform.GetChild(i).CompareTag("Bacteria")) {
				Renderer childRenderer = parent.transform.GetChild(i).GetComponent<Renderer>();
				if (childRenderer != null) {
					if (hasBounds) {
						bounds.Encapsulate(childRenderer.bounds);

					} else {
						bounds = childRenderer.bounds;
						hasBounds = true;
					}
					coll.center = bounds.center - parent.transform.GetChild(i).transform.position;
					coll.size = bounds.size;
				}
			}

		}
	}
	
}

