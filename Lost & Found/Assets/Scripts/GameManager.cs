using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.AI;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    //public NavMeshSurface surface;

    public float transitionTime = 1f;
    public Transform breakablesContainer;
    public float breakablesCount = 0;
    public float percentIncrease;
    public float completionPercent = 0f;
    public Text percentText;
    public GameObject enemyToSpawn;
    public GameObject[] spawnLocations;

    public Image progressBar;
    public GameObject GameOverPanel;
    public float timeToSpawnEnemy = 10.0f;
    float enemyTimer;
    public TextMeshProUGUI countdownText;

    public TextMeshProUGUI startText;
    bool bSpawningEnemy = false;

    AudioSource m_audioSource;


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
        //progressBar.fillAmount = 0f;
        //m_audioSource = GetComponent<AudioSource>();
        enemyTimer = timeToSpawnEnemy;
        CalculateScore();
        //SpawnEnemy();
        //StartCoroutine(DisplayStartText());
        GameManager.Instance.bSpawningEnemy = true;
    }

    void MoveCamera()
    {

    }

    IEnumerator DisplayStartText()
    {
        startText.text = "Search objects";
        yield return new WaitForSeconds(4.0f);
        startText.text = "";
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void BeginLevelLoad()
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
        enemyTimer -= 1 * Time.deltaTime;
        countdownText.text = enemyTimer.ToString("0");

        if (enemyTimer <= 0 && countdownText)
        {
            countdownText.text = "";
            bSpawningEnemy = false;
            SpawnEnemy();
            enemyTimer = timeToSpawnEnemy;
        }

    }

    public void PlayAudioFromBreaking(Breakable brokenObject)
    {
        m_audioSource.clip = brokenObject.sound;
        if (m_audioSource.clip)
        {
            m_audioSource.Play();
        }
        
    }


    public void OnBreakObject(Breakable NewlyBrokenObject)
    {
       // PlayAudioFromBreaking(NewlyBrokenObject);
        completionPercent += percentIncrease;
        if(completionPercent > 90)
        {
            // Activate Halos on remaining breakables
            foreach (Transform child in breakablesContainer)
            {
                if (child.childCount > 0)
                {
                    child.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogError("This 'breakable' (in breakablesContainer) has no child. Tried to activate Halo, but failed. " + child);
                }
            }
        }
        if (completionPercent >= 100) 
        { 
            BeginLevelLoad(); 
            //completionPercent = 100; 
        }
        
        percentText.text = (int)completionPercent+ "%";
        //progressBar.fillAmount += (percentIncrease/100);

        // Rebuild Navmesh
        //NavMeshBuilder.BuildNavMesh();
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
       bSpawningEnemy = true;
    }
}
