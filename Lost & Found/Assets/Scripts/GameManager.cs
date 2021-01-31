using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    public float transitionTime = 1f;
    public Transform breakablesContainer;
    public float breakablesCount = 0;
    public float percentIncrease;
    public float completionPercent = 0f;
    public Text percentText;
    public GameObject enemyToSpawn;
    public GameObject[] spawnLocations;

    public float timeToSpawnEnemy = 10.0f;
    public TextMeshProUGUI countdownText;
    bool bSpawningEnemy = false;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Update()
    {
        if (bSpawningEnemy)
        {
            Countdown();
        }
    }

    private void Start()
    {
        CalculateScore();
        SpawnEnemy();
    }

    void MoveCamera()
    {

    }

    void LoadLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    void CalculateScore()
    {
        foreach (Transform child in breakablesContainer)
        {
            breakablesCount++;
        }

        percentIncrease = 100 / breakablesCount;
    }
    void Countdown()
    {
        timeToSpawnEnemy -= 1 * Time.deltaTime;
        countdownText.text = timeToSpawnEnemy.ToString("0");

        if (timeToSpawnEnemy <= 0 && countdownText)
        {
            countdownText.text = "";
            bSpawningEnemy = false;
            SpawnEnemy();
        }

    }

    public void OnBreakObject()
    {
        completionPercent += percentIncrease;
        if (completionPercent > 99) { Debug.Log("LEVEL COMPLETE"); completionPercent = 100; }
        
        percentText.text = "SMASHED: " +(int)completionPercent+ "%";
    }

    public void SpawnEnemy()
    {
        if (spawnLocations.Length != 0)
        {
            Instantiate(enemyToSpawn, spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Quaternion.identity);
        }
        //yield return new WaitForSeconds(2.0f);
        // StartCoroutine(SpawnEnemy());
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
