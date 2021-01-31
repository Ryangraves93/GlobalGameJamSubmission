using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public void OnBreakObject()
    {
        completionPercent += percentIncrease;
        if (completionPercent > 99) { Debug.Log("LEVEL COMPLETE"); completionPercent = 100; }
        
        percentText.text = "SMASHED: " +(int)completionPercent+ "%";
    }

    public void SpawnEnemy()
    {
        //Instantiate(enemyToSpawn, spawnLocations[Random.Range(0,spawnLocations.Length)].transform.position, Quaternion.identity);
        //yield return new WaitForSeconds(2.0f);
       // StartCoroutine(SpawnEnemy());
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
