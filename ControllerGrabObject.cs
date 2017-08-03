using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControllerGrabObject: MonoBehaviour {


	private Text instruction;
//	private Transform child;
	private GameObject collidingObject; 

	private GameObject objectInHand;
	private JellyScore scoreScript;
	public GameObject camera;
	public Vector3 pos;

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		scoreScript = GameObject.Find ("dish-rack-full").GetComponent<JellyScore>();
//		player = GameObject.Find ("Player");
	}

	private void SetCollidingObject(Collider col) {
		
		if (collidingObject || !col.GetComponent<Rigidbody>()) {
			return;
		}

		collidingObject = col.gameObject;
	}

	public void OnTriggerEnter(Collider other) {
		SetCollidingObject(other);
	}

	// 2
	public void OnTriggerStay(Collider other) {
		SetCollidingObject(other);
	}


	public void OnTriggerExit(Collider other) {
		if (!collidingObject)
		{
			return;
		}

		collidingObject = null;
	}

	private void GrabObject() {
		objectInHand = collidingObject;
		collidingObject = null;

		var joint = AddFixedJoint();
		joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
	}


	private FixedJoint AddFixedJoint() {
		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	private void ReleaseObject() {
		
		if (GetComponent<FixedJoint>()) {
			
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());

//			objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
//			objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
		}

		objectInHand = null;
	}

	// Update is called once per frame
	void Update () {
		
		if (Controller.GetHairTriggerDown()) {
			if (collidingObject) {
				if (collidingObject.GetComponent<Floater>()) {
					collidingObject.GetComponent<Floater>().enabled = false;
					collidingObject.GetComponent<SphereCollider>().enabled = false;
				}
				GrabObject();
			}

		}
			
		if (Controller.GetHairTriggerUp()) {
			if (objectInHand) {
				objectInHand.transform.parent = trackedObj.transform;
				objectInHand.GetComponent<Rigidbody>().useGravity = false;
				pos = camera.transform.position;
				pos.y = pos.y + 0.3f;
				camera.transform.position = pos;
//				Physics.IgnoreCollision(collidingObject.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>());
//				collidingObject.GetComponent<Collider>().isTrigger = true;
				ReleaseObject();
				scoreScript.IncrementScore();
			}
		}
	}

}
