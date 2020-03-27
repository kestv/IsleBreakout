using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnProjectilesScript : MonoBehaviour {

	public bool cameraShake;
	public Text effectName;
    public GameObject spell;
	public RotateToMouseScript rotateToMouse;
	public GameObject firePoint;
	public GameObject cameras;
	public List<GameObject> VFXs = new List<GameObject> ();

	//private int count = 0;
	private float timeToFire = 0f;
	private GameObject effectToSpawn;
	private List<Camera> camerasList = new List<Camera> ();

	void Start () {
        rotateToMouse = GetComponent<RotateToMouseScript>();
		for (int i = 0; i < cameras.transform.childCount; i++) {
			camerasList.Add (cameras.transform.GetChild(i).gameObject.GetComponent<Camera>());
		}

		Application.targetFrameRate = 60;
		effectToSpawn = VFXs[0];
		//if (effectName != null) effectName.text = effectToSpawn.name;

		//rotateToMouse.SetCamera (camerasList [2]);
		//rotateToMouse.StartUpdateRay ();
	}

	void Update () {
		if (Input.GetKey (KeyCode.Q) && Time.time >= timeToFire || Input.GetMouseButton (0) && Time.time >= timeToFire) {
			timeToFire = Time.time + 1f / effectToSpawn.GetComponent<ProjectileMoveScript>().fireRate;
			SpawnVFX ();	
		}
        if(Input.GetKey(KeyCode.Q))
        {
            SpawnVFX();
        }
	}

	public void SpawnVFX () {
		GameObject vfx;

		if (cameraShake && cameras != null)
			cameras.GetComponent<CameraShakeSimpleScript> ().ShakeCamera ();

		if (firePoint != null) {
			vfx = Instantiate (spell, firePoint.transform.position, Quaternion.identity);
			if(rotateToMouse != null){
				vfx.transform.localRotation = rotateToMouse.GetRotation ();
			} 
			else Debug.Log ("No RotateToMouseScript found on firePoint.");
		}
		else
			vfx = Instantiate (effectToSpawn);

		var ps = vfx.GetComponent<ParticleSystem> ();

		if (vfx.transform.childCount > 0) {
			ps = vfx.transform.GetChild (0).GetComponent<ParticleSystem> ();
		}
	}

	public void CameraShake () {
		cameraShake = !cameraShake;
	}
}
