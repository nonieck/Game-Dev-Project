using UnityEngine;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Color startColor;
    public Vector3 positionOffset;

    private GameObject turret;

    private Renderer rend; //dibuat ini agar kita tidak selalu mengupdate data setiap kali mouse masuk ke colider, dng ini jadi hanya
    //didata sekali pada saat mulai disetiap mouse masuk collider lalu di store kan ke private

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color; //untuk menyimpan warna awal sebelum mouse masuk colider
    }

    void OnMouseDown()
    {
        if(turret != null) //alr build something here
        {
            Debug.Log("cant build here - TODO : Display On Screen.");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);

    }

    void OnMouseEnter() //saat masuk colider atau si node nya
    {
        rend.material.color = hoverColor; //mengganti warna colider dengan hover
    }

    void OnMouseExit() //saat keluar dari colider
    {
        rend.material.color = startColor; //mengembalikan warna nya ke semula
    }
}
