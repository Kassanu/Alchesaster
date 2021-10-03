using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player player;
    public List<GameObject> tutorialRooms;
    public GameObject bossRoom;
    public EnemySpawner tutorialEnemySpawner;
    public List<GameObject> rooms;
    public List<EnemySpawner> easySpawners;
    public List<EnemySpawner> mediumSpawners;
    public List<EnemySpawner> hardSpawners;
    public GameObject enemiesContainer;
    public GameObject spellsContainer;
    public GameObject hardmodeContainer;
    Exit exit = null;
    GameObject loadedRoom;
    public bool roomDone = false;
    public int tutorialRoomIndex = 0;
    public bool inTutorial = true;
    public int floorsCleared = 0;
    public bool gameOver = false;
    public bool playerDead = false;
    int spawnerIndex = 0;
    public bool hardMode = false;
    public Boss bossprefab;
    public Boss hardBossprefab;
    public int floorsForBoss = 20;
    public GameObject floorText;

    private void Awake() {
        this.reset();
        DontDestroyOnLoad(this.gameObject);
    }

    private void reset() {
        this.roomDone = false;
        this.tutorialRoomIndex = 0;
        this.inTutorial = false;
        this.floorsCleared = 0;
        this.gameOver = false;
    }

    private void Start() {
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        this.loadTutorialRoom();
    }

    private void Update() {
        if (!this.gameOver) {
            if (this.inTutorial && this.tutorialRoomIndex == 0) {
                if (Input.GetKeyUp(KeyCode.H)) {
                    this.hardMode = !this.hardMode;
                    this.hardmodeContainer.SetActive(this.hardMode);
                }
            }
            if (exit != null) {
                if (!this.roomDone && enemiesContainer.transform.childCount == 0) {
                    this.roomDone = true;
                    exit.isActive = true;
                    if (!this.inTutorial) {
                        this.player.stopBottlesRefreshing();
                    }
                }

                if (exit.isActive && exit.playerIsOn) {
                    if (exit.bossExit || this.floorsCleared > 30) {
                        this.gameOver = true;
                        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                    }
                    this.unloadRoom();
                    if (!this.hardMode && (this.inTutorial && this.tutorialRoomIndex < this.tutorialRooms.Count - 1)) {
                        this.tutorialRoomIndex++;
                        this.loadTutorialRoom();
                    } else if (this.floorsCleared >= this.floorsForBoss) {
                        this.loadBossRoom();
                    } else {
                        this.loadRoom();
                    }
                }
            }

            if (this.player == null) {
                this.gameOver = true;
                this.playerDead = true;
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
        }
    }

    private void unloadRoom()
    {
        DestroyImmediate(this.loadedRoom);
        if (!this.inTutorial) {
            this.floorsCleared++;
        }
        this.exit = null;
        this.loadedRoom = null;
        foreach (Transform spell in this.spellsContainer.transform) {
            DestroyImmediate(spell.gameObject);
        }
        if (this.hardMode || this.floorsCleared > 15) {
            this.hardSpawners[this.spawnerIndex].resetSpawnPoints();
        } else if (this.floorsCleared > 5) {
            this.mediumSpawners[this.spawnerIndex].resetSpawnPoints();
        } else {
            this.easySpawners[this.spawnerIndex].resetSpawnPoints();
        }
    }

    private void loadRoom()
    {
        this.floorText.GetComponent<TMPro.TextMeshProUGUI>().text = "Floor: " + (this.floorsCleared + 1).ToString() + "/" + this.floorsForBoss.ToString();
        this.inTutorial = false;
        this.roomDone = false;
        this.player.enterRoom();
        this.loadedRoom = Instantiate(this.rooms[Random.Range(0, this.rooms.Count)], Vector3.zero, Quaternion.identity);
        GameObject spawnPoint = GameObject.FindWithTag("PlayerSpawn");
        this.exit = GameObject.FindWithTag("PlayerExit").GetComponent<Exit>();
        this.player.transform.position = spawnPoint.transform.position;
        this.spawnEnemies();
    }

    private void spawnEnemies()
    {
        if (this.hardMode || this.floorsCleared > 15) {
            this.spawnerIndex = Random.Range(0, this.hardSpawners.Count);
            this.hardSpawners[this.spawnerIndex].Init(enemiesContainer);
            this.hardSpawners[this.spawnerIndex].Spawn();
        } else if (this.floorsCleared > 5) {
            this.spawnerIndex = Random.Range(0, this.mediumSpawners.Count);
            this.mediumSpawners[this.spawnerIndex].Init(enemiesContainer);
            this.mediumSpawners[this.spawnerIndex].Spawn();
        } else {
            this.spawnerIndex = Random.Range(0, this.easySpawners.Count);
            this.easySpawners[this.spawnerIndex].Init(enemiesContainer);
            this.easySpawners[this.spawnerIndex].Spawn();
        }
    }

    private void loadTutorialRoom()
    {
        this.inTutorial = true;
        this.roomDone = false;
        this.player.enterRoom();
        this.loadedRoom = Instantiate(this.tutorialRooms[this.tutorialRoomIndex], Vector3.zero, Quaternion.identity);
        GameObject spawnPoint = GameObject.FindWithTag("PlayerSpawn");
        this.exit = GameObject.FindWithTag("PlayerExit").GetComponent<Exit>();
        this.player.transform.position = spawnPoint.transform.position;
        if (this.tutorialRoomIndex == 1) {
            this.tutorialEnemySpawner.Init(enemiesContainer);
            this.tutorialEnemySpawner.Spawn();
        }
    }

    private void loadBossRoom()
    {
        this.floorText.GetComponent<TMPro.TextMeshProUGUI>().text = "Boss";
        this.roomDone = false;
        this.player.enterRoom();
        this.loadedRoom = Instantiate(this.bossRoom, Vector3.zero, Quaternion.identity);
        GameObject spawnPoint = GameObject.FindWithTag("PlayerSpawn");
        this.exit = GameObject.FindWithTag("PlayerExit").GetComponent<Exit>();
        this.player.transform.position = spawnPoint.transform.position;
        if (this.hardMode) {
            Boss boss = Instantiate(this.hardBossprefab);
            boss.transform.parent = enemiesContainer.transform;
        } else {
            Boss boss = Instantiate(this.bossprefab);
            boss.transform.parent = enemiesContainer.transform;
        }
    }
}
