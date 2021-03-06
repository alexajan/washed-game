using UnityEngine;
using System.Collections;

public class LaserPointer : MonoBehaviour {
	
	public Transform cameraRigTransform;
	public GameObject teleportReticlePrefab;
	public Transform teleportReticleTransform;
	public Transform headTransform;
	public Vector3 teleportReticleOffset;
	public LayerMask teleportMask;
	public GameObject laserPrefab;
	
	private float y;
	private bool shouldTeleport;
	private GameObject reticle;
	private GameObject laser;
	private Transform laserTransform;
	private Vector3 hitPoint;

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		laser = Instantiate(laserPrefab);
		laserTransform = laser.transform;
		reticle = Instantiate(teleportReticlePrefab);
		teleportReticleTransform = reticle.transform;
		y = GameObject.FindWithTag("GameController").GetComponent<ControllerGrabObject>().pos.y;
	}
	  
	void Update () {
		if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
			RaycastHit hit;

			if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask)) {
				hitPoint = hit.point;
				ShowLaser(hit);
				reticle.SetActive(true);
				teleportReticleTransform.position = hitPoint + teleportReticleOffset;
				shouldTeleport = true;
			}
		} else {
			laser.SetActive(false);
			reticle.SetActive(false);
		}
		
		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport) {
			Teleport();
		}
	}
	private void ShowLaser(RaycastHit hit) {
		
		laser.SetActive(true);

		laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);

		laserTransform.LookAt(hitPoint); 

		laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
			hit.distance);
	}
	
	private void Teleport() {
		shouldTeleport = false;
		reticle.SetActive (false);

		Vector3 difference = cameraRigTransform.position - headTransform.position;

		difference.y = 0 - y;

		cameraRigTransform.position = hitPoint + difference;
	}
}
