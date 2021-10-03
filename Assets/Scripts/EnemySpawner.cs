using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemies;
    public List<int> amount;
    public List<Vector2> spawnPositions;
    public bool setSpawn = false;
    Tilemap spawnTilemap;
    Player player;
    GameObject enemiesContainer;

    public void Init(GameObject enemiesContainer) {
        this.enemiesContainer = enemiesContainer;
        this.spawnTilemap = GameObject.FindWithTag("SpawnableTiles").GetComponent<Tilemap>();
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (!this.setSpawn) {
            this.spawnPositions = new List<Vector2>();
            foreach (var position in this.spawnTilemap.cellBounds.allPositionsWithin) {
                //Get the local position of the cell
                Vector3Int relativePos = new Vector3Int(position.x, position.y, position.z);
                //Add it to the List if the local pos exist in the Tile map
                if (this.spawnTilemap.HasTile(relativePos))
                {
                    //Convert to world space
                    Vector2 worldPos = this.spawnTilemap.CellToWorld(relativePos);
                    this.spawnPositions.Add(worldPos);
                }
            }
        }
    }

    public void Spawn()
    {
        int enemyIndex = 0;
        foreach (Enemy enemy in this.enemies) {
            for (int i = 0; i < this.amount[enemyIndex]; i++)
            {
                Enemy newEnemy = Instantiate(enemy, this.getSpawnPosition(), Quaternion.identity);
                newEnemy.transform.parent = enemiesContainer.transform;
            }
            enemyIndex++;
        }
    }

    public Vector2 getSpawnPosition()
    {
        Vector2 spawnPoint = this.spawnPositions[Random.Range(0, this.spawnPositions.Count)];
        while (Vector2.Distance(spawnPoint, this.player.gameObject.transform.position) <= 1) {
            spawnPoint = this.spawnPositions[Random.Range(0, this.spawnPositions.Count)];
        }
        return spawnPoint;
    }

    public void resetSpawnPoints()
    {
        if (!this.setSpawn) {
            this.spawnPositions.Clear();
        }
    }
}
