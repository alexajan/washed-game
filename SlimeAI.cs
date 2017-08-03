 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour {
	private Animator animator;
	private Transform child;
	private GameObject collidingObject;
	private GameObject bacteria;
	private int attacks = 0;
	private int death = 1;
	private int MaxDist = 20;
	private float MoveSpeed = 0.5f;
	private JellyScore scoreScript;

	public GameObject RightController;
	public GameObject LeftController;

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		animator = GetComponent<Animator>();
		scoreScript = GameObject.Find ("dish-rack-full").GetComponent<JellyScore>();
	}
	
	void Update () {

		if(Vector3.Distance(transform.position, RightController.transform.position) < MaxDist) {
			transform.LookAt(RightController.transform);
			transform.position += transform.forward * Mathf.Sin(MoveSpeed * Time.deltaTime);
		
		} else {
			bacteria = GameObject.FindWithTag("Bacteria");
			transform.LookAt(bacteria.transform);
			transform.position += transform.forward * Mathf.Sin(MoveSpeed * Time.deltaTime);
		}
			

		if (attacks > death) {
			death = death + 1;
			scoreScript.SlimeScore();
			Destroy(this.gameObject);
		}
	}

	public void OnCollisionEnter(Collision col) {
		if (col.collider.CompareTag("Bacteria")) {
			Destroy(col.collider.gameObject);
		}
	}

	public void OnTriggerEnter(Collider other) {
		if (other.transform.childCount > 0) {
			
			for (int index = 0; index < other.transform.childCount; index++) {
				child = other.transform.GetChild(index);
				if(child.CompareTag("Bacteria")) {
					Destroy(child.gameObject);
					scoreScript.DecreaseScore();
					attacks = attacks + 1;
					return;
				} 
			}
		}
	}
}
