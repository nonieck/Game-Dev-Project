using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static Transform[] points;

    void Awake ()
    {
        points = new Transform[transform.childCount]; // membuat 13 space didalam array nya
        for (int i = 0; i < points.Length; i++) // loop berdasarkan 13 itu
        {
            points[i] = transform.GetChild(i);  // mengisi space tersebut dengan si isian nya *child / dalam kasus ini dengan waypoints itu
        }
    }
}
