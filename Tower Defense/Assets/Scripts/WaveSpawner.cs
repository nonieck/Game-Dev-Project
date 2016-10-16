using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class WaveSpawner : MonoBehaviour {

    public Transform enemyPrefab;

    public Transform spawnPoint; //lokasi spawn si enemy nya

    public float timeBetweenWaves = 5f;
    private float countdown = 2f; //waktu pertama kali start

    public Text waveCountdownText;

    private int waveIndex = 0;

    void Update()
    {
        if ( countdown <= 0f )
        {
            StartCoroutine(SpawnWave()); // memanggil spawnwave. untuk memulai startcoroutine karena ada IEnumerator
            countdown = timeBetweenWaves; //setelah countdown pertama habis maka akan diubah menjadi 5f ( timebetwenwave )
        }

        countdown -= Time.deltaTime; //untuk si countdown berkurang perdetik
        //ori sesi 3 :
        //waveCountdownText.text = Mathf.Round(countdown).ToString();

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        //Debug.Log("Spawn Incoming!"); // memberikan log spawn incoming *hanya permulaan untuk menunjukan apakah ini berhasil
        // banyak cara untuk menspawn enemy  seperti
        // 1. numOfEnemies = waves[ waveNumber].count;
        // 2. numOfEnemies = waveNumber * waveNumber + 1;
        // 3. numOfEnemies = waveNumber;

        //untuk sekarang kita memakai :
        //waveNumber++;
        waveIndex++;
        for (int i = 0; i < waveIndex; i++ )
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f); //spawn enemy 1 dan setelahny
        }

        
    }

    void SpawnEnemy() //allow to pause of this piece of code
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); //untuk menginstansiasi enemy nya dan posisi dispawning nya
    }

}
