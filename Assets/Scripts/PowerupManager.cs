using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    public GameObject[] powerUps;
    public float spawnTime;
    public float removeTime;
    private float time;

    public GameObject[] shield;

    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        
        if(GameManager.instance.toggleController.GetComponent<Toggle>().isOn == true)
        {
            SpawnPowerup();
        }
    }

    public void SpawnPowerup()
    {
        if(time >= spawnTime)
        {
            int powerupIndex = Random.Range(0, 3);
            GameObject powerupSpawned = Instantiate(powerUps[powerupIndex], new Vector2(Random.Range(-6f, 6f), Random.Range(-4f, 4f)), Quaternion.identity);
            time = 0;
            Destroy(powerupSpawned, removeTime);
        }
    }

    public IEnumerator MultiplyScore()
    {
        GameManager.instance.scoreManager.addScore = 2;
        yield return new WaitForSeconds(5);
        GameManager.instance.scoreManager.addScore = 1;
    }

    public IEnumerator MinusScore()
    {
        GameManager.instance.scoreManager.addScore *= -1;
        yield return new WaitForSeconds(5);
        GameManager.instance.scoreManager.addScore *= -1;
    }

    public IEnumerator DoShield(int id)
    {
        int index = id - 1;
        shield[index].gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        shield[index].gameObject.SetActive(false);
    }
}
