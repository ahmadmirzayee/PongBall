using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    [Header("Power-ups Sprites")]
    public GameObject[] powerUps;

    [Header("Times")]
    public float spawnTime;
    public float removeTime;
    private float time;

    [Header("Shields Game Objects")]
    public GameObject[] shield;

    // Update is called once per frame
    private void Update()
    {
        time += Time.deltaTime;
        
        if(GameManager.instance.toggleController.GetComponent<Toggle>().isOn == true)
        {
            SpawnPowerup();
        }
    }

    // This function is used to spawn a power-up at a random position on the screen after a certain amount of time has passed, and destroy it after a certain amount of time has passed
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

    // This function is used to multiply the score by 2 for a certain amount of time, and then reset it back to 1
    public IEnumerator MultiplyScore()
    {
        GameManager.instance.scoreManager.addScore = 2;
        yield return new WaitForSeconds(5);
        GameManager.instance.scoreManager.addScore = 1;
    }

    // This function is used to decrease the score by 1 for a certain amount of time, and then reset it back to 1
    public IEnumerator MinusScore()
    {
        GameManager.instance.scoreManager.addScore *= -1;
        yield return new WaitForSeconds(5);
        GameManager.instance.scoreManager.addScore *= -1;
    }

    // This function is used to activate a shield for a certain player for a certain amount of time, and then deactivate it
    public IEnumerator DoShield(int id)
    {
        int index = id - 1;
        shield[index].gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        shield[index].gameObject.SetActive(false);
    }
}
