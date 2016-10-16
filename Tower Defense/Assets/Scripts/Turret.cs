using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    private Transform target;

    [Header("Attributes")] //attribute yang nanti bisa di upgrade atau sejenisny *ini mencakup 3 bawah sebelum header selanjutny

    public float range = 12f;
    public float fireRate = 1f; // 1 bullet / s
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform PartToRotate;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;
        
	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); //0f ( setelah 0s cari target dari awal ) dan diulang mulai dng waktu 0.5s
	}

    void UpdateTarget() //update target yang nantinya akan diarah yaitu yang paling deket dengan turret
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //membuat array enemy yang ditemukan 
        float shortestDistance = Mathf.Infinity; //karena jarak ny berupa infinity 
        GameObject nearestEnemy = null; //

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); //untuk mengukur jarak ke enemy yang disimpan di float
            if (distanceToEnemy < shortestDistance) //untuk membandingkan enemy yang ditemui
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) //kita temukan nearest target disini yang akan dijadikan target
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

	void Update () {
	    if (target == null) // jika tidak ada target maka idle / do nothing
        {
            return;
        }

        /* TARGET LOCK ON */
        Vector3 dir = target.position - transform.position; // *dir untuk direction. if u want to move from point a to point b the key is point b - point a.
        Quaternion lookRotation = Quaternion.LookRotation(dir); //quaternion digunakan untuk rotasi *untuk mencatat direction yg akan dirotasikan
        //Vector3 rotation = lookRotation.eulerAngles; digunakan untnuk rotasi nya *dipakai karena kita hanya rotasi di point Y saja
        //tapi untuk code tersebut update dari 1 enemy ke enemy lain ny dengan sontakan *it just happen* tanpa smoothing atau pergerakan si turretny
        //untuk itu kita gunakan
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //^ dijelaskan dimana rotasi nya tersebut dari current rotasi ke lookrotasi dengan lerp / lembut dimana speed ny dari turnsped * time.delta time
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); //yang dirotasi y nya saja, untuk itu x dan z diposisikan 0 terus
	
        if(fireCountdown <= 0f ) //shoot now, untuk diawal setelahny akan menembak 1 / s
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot() //untuk menembaknya
    {
        //Debug.Log("Shoot");
        GameObject bulletGO =  (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //untuk instansiasi ke bullet.cs
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    void OnDrawGizmosSelected() //untuk menggambarkan lingkaran range si turret
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
