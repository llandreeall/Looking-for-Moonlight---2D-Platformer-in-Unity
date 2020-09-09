using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    CameraShake camShake;


    [SerializeField]
    private int maxLives = 3;

    private static int remainingLives = 3;
    public static int RemainingLives {
        get { return remainingLives; }
        set { remainingLives = value; }
    }

    private int startingMoney = 80;
    public static int Money;

    public static int money {
        get { return Money; }
        set { Money = value; }
    }


    private void Awake() {
        if (gm == null) {
            gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        }
    }


    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public GameObject spawnPrefab;
    public string spawnSoundName;
    public string afterSpawnSoundName;
    public string gameOverSoundName = "GameOver";

    public CameraShake cameraShake;

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject upgradeMenu;

    [SerializeField]
    private WaveSpawner waveSpawner;

    public delegate void UpgradeMenuCallback(bool active);
    public UpgradeMenuCallback onToggleUpgradeMenu;

    //cache
    private AudioManager audioManager;

    private void Start() {
        if (cameraShake == null) {
            Debug.LogError("No camera shake referenced in GameMaster");
        }
        remainingLives = maxLives;

        Money = startingMoney;

        //caching
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in the scene!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            ToggleUpgradeMenu();
        }
    }

    private void ToggleUpgradeMenu()
    {
        upgradeMenu.SetActive( !upgradeMenu.activeSelf );
        waveSpawner.enabled = !upgradeMenu.activeSelf;
        onToggleUpgradeMenu.Invoke(upgradeMenu.activeSelf);
    }

    public void EndGame() {

        audioManager.PlaySound(gameOverSoundName);

        Debug.Log("Game over");
        gameOverUI.SetActive(true);
    }

    public IEnumerator _RespawnPlayer() {
        audioManager.PlaySound(spawnSoundName);
        yield return new WaitForSeconds(spawnDelay);

        Debug.Log(spawnPoint.position.x + " " + spawnPoint.position.y + " " + spawnPoint.position.z);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        
        audioManager.PlaySound(afterSpawnSoundName);
        PlayerStats.instance.curHealth = PlayerStats.instance.maxHealth;

        CamShake(0.2f, 0.2f);
        GameObject clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
        //Debug.Log(clone.transform.position.x + " " + clone.transform.position.y + " " + clone.transform.position.z);
        Destroy(clone, 3f);
    }

    public static void KillPlayer (Player player) {
        Destroy(player.gameObject);
        remainingLives--;
        if(remainingLives <= 0) {
            gm.EndGame();
        } else {
            gm.StartCoroutine(gm._RespawnPlayer());
        }
    }


    public static void KillEnemy(Enemy enemy) {
       
        CamShake(0.2f, 0.2f);
        gm._KillEnemy(enemy);
        //gm.StartCoroutine(gm.RespawnPlayer());
    }

    public void _KillEnemy(Enemy _enemy) {

        //play some sounds
        audioManager.PlaySound(_enemy.deathSoundName);

        //Gain some money
        Money += _enemy.moneyDrop;
        audioManager.PlaySound("Money");

        //add particles
        GameObject _clone = Instantiate<GameObject>(_enemy.deathParticles.gameObject, _enemy.transform.position, Quaternion.identity) as GameObject;
        cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        Destroy(_clone, 5f);
        //cameraShake.Shake(_enemy.shakeAmt, _enemy.shakeLength);
        Destroy(_enemy.gameObject);
    }

    public static void CamShake(float sm, float l) {
        CameraShake camShake;
        camShake = gm.GetComponent<CameraShake>();
        if (camShake == null)
            Debug.LogError("No CamerShake script found on gm object");

        camShake.Shake(sm, l);
    }


}
