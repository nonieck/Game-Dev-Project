using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;

    public float speed = 70f;
    public GameObject impactEffect;

    public void Seek(Transform _target) //setup everything we need to setup on the bullet
    {
        target = _target;
    }
	
	void Update () {
	    if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame ) //dir.mag jarak antara bullet dan targetny *jika jarak bulet ke target kurang dari frame maka terkena hit
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World); //dir * dis untuk pergerakan si bulletny

	}

    void HitTarget()
    {
        //Debug.Log("we hit something");
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f); //ngancurin bulletins ny dalam 2 detik

        Destroy(target.gameObject); //ngancrin enemy
        //Destroy(gameObject); //ngancurin si bulletnya

    }
}
