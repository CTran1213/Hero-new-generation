using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    // Links to other objects
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Text enemyTouched;
    [SerializeField] Text enemyDestroyed;
    [SerializeField] Text playerHealthText;
    [SerializeField] Text eggCountText;
    [SerializeField] Text eggCoodldown;
    [SerializeField] GameObject waypoints;
    
    // Counter for eggs on the screen
    public int eggCount = 0;

    // Toggle for random movement of enemy
    public bool randomMovement = false;

    // Private state variables
    int maxPlanes = 10;
    int numberOfPlanes = 0;
    int planesDestoyed = 0;
    private int touched = 0;
    private float cooldown = 0;


    // World bounds that need to be accessed by other scripts
    [HideInInspector] public float xMin;
    [HideInInspector] public float xMax;
    [HideInInspector] public float yMin;
    [HideInInspector] public float yMax;

    // Start is called before the first frame update
    void Start()
    {
        // Set up the world bounds
        Camera camera = Camera.main;
        xMin = camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        xMax = camera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
        yMin = camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y;
        yMax = camera.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y;
    }

    // Update is called once per frame
    void Update()
    {
        DetectIfTryToExit();

        if (Input.GetKeyDown(KeyCode.R))
        {
            randomMovement = !randomMovement;
        }

        TrySpawnEnemy();
        //UpdateEnemyTouchedText();
        UpdateEggCount();
        DetectWaypointEnable();
    }

    private void DetectWaypointEnable()
    {
        if(Input.GetKeyDown("h"))
        {
            waypoints.SetActive(!waypoints.activeInHierarchy);
        }
    }

    private void UpdateEggCount()
    {
        eggCountText.text = "Eggs: " + eggCount;
    }

    private void TrySpawnEnemy()
    {
        if (numberOfPlanes < maxPlanes)
        {

            Vector3 pos;
            pos.x = Random.Range(xMin, xMax);
            pos.y = Random.Range(yMin, yMax);
            pos.z = 0f;

            Instantiate(enemyPrefab, pos, Quaternion.identity);

            ++numberOfPlanes;
        }
    }

    private static void DetectIfTryToExit()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
    }

    public void UpdateEnemyTouchedText()
    {
        touched++;
        enemyTouched.text = "Enemies Touched: " + touched;
    }

    public void UpdateTouched()
    {
        touched++;
    }
    public void EnemyDestroyed() {
        --numberOfPlanes;
        enemyDestroyed.text = "Destroyed: " + ++planesDestoyed;
    }

    public void UpdatePlayerHealth(int playerHealth)
    {
        playerHealthText.text = "Health: " + playerHealth;
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateCooldown(float c)
    {
        cooldown = c;
        eggCoodldown.text = "Egg Cooldown: " + c;
    }
}
