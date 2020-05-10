using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour {

	[SerializeField]float speed;
	[SerializeField]GameObject muzzlePrefab;
	[SerializeField]GameObject hitPrefab;
	[SerializeField]AudioClip shotSFX;
	[SerializeField]AudioClip hitSFX;
	[SerializeField]List<GameObject> trails;
    [SerializeField]GameObject target;
	bool collided;

    [SerializeField]float damage;
    [SerializeField]float actualDamage;
    void Start () 
    {
		if (muzzlePrefab != null) {
			var muzzleVFX = Instantiate (muzzlePrefab, transform.position, Quaternion.identity);
			muzzleVFX.transform.forward = gameObject.transform.forward;
			var ps = muzzleVFX.GetComponent<ParticleSystem>();
			if (ps != null)
				Destroy (muzzleVFX, ps.main.duration);
			else {
				var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
				Destroy (muzzleVFX, psChild.main.duration);
			}
		}

		if (shotSFX != null && GetComponent<AudioSource>()) {
			GetComponent<AudioSource>().PlayOneShot (shotSFX);
		}
	}

	void Update () {
        if (speed != 0)
        {
            var pos = new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, pos, 10f * Time.deltaTime);
            if (Vector3.Distance(transform.position, target.transform.position) <= 1)
            {
                CollisionDetected();
            }
        }
        
    }

    void CollisionDetected() 
    {
        if (!collided) {
			collided = true;
            target.GetComponent<EnemyHealthController>().DoDamage(actualDamage);

			if (shotSFX != null && GetComponent<AudioSource>()) {
				GetComponent<AudioSource> ().PlayOneShot (hitSFX);
			}

			if (trails.Count > 0) {
				for (int i = 0; i < trails.Count; i++) {
					trails [i].transform.parent = null;
					var ps = trails [i].GetComponent<ParticleSystem> ();
					if (ps != null) {
						ps.Stop ();
						Destroy (ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
					}
				}
			}
		
			speed = 0;
			GetComponent<Rigidbody> ().isKinematic = true;

			if (hitPrefab != null) {
				var hitVFX = Instantiate (hitPrefab, target.transform.position, target.transform.rotation);
				var ps = hitVFX.GetComponent<ParticleSystem> ();
				if (ps == null) {
					var psChild = hitVFX.transform.GetChild (0).GetComponent<ParticleSystem> ();
					Destroy (hitVFX, psChild.main.duration);
				} else
					Destroy (hitVFX, ps.main.duration);
			}

			StartCoroutine (IEDestroyParticle (0f));
		}
	}

	IEnumerator IEDestroyParticle (float waitTime) {

		if (transform.childCount > 0 && waitTime != 0) {
			List<Transform> tList = new List<Transform> ();

			foreach (Transform t in transform.GetChild(0).transform) {
				tList.Add (t);
			}		

			while (transform.GetChild(0).localScale.x > 0) {
				yield return new WaitForSeconds (0.01f);
				transform.GetChild(0).localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				for (int i = 0; i < tList.Count; i++) {
					tList[i].localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				}
			}
		}
		
		yield return new WaitForSeconds (waitTime);
		Destroy (gameObject);
	}

    public void SetDamage(float damage)
    {
        this.actualDamage = damage;
    }

    public float GetDamage()
    {
        return this.damage;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}
