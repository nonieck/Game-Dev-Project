using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 10f; //kecepatan gerak si enemy nya

    private Transform target; //priv karena hanya dilakukan untuk enemy saja, ini dibuat untuk tujuan si enemy *waypoint
    private int wavepointIndex = 0; //respawn nya enemy di titik 0 *default

    void Start()
    {
        target = Waypoints.points[0]; //target awal setelah spawn enemy akan menuju ke points 0 ( waypoints pertama )
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position; //untuk memberikan direction kepada enemy nya itu
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); //yg atas dan yg ini dipakai untuk si enemy maju ke depan menuju waypoints

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if ( wavepointIndex >= Waypoints.points.Length - 1 )
        {
            Destroy(gameObject);
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
}
