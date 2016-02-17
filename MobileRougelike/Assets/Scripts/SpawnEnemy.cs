using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

    Rigidbody2D enemy;

	// Use this for initialization
	void Start () {
        InvokeRepeating("EnemySpawn", 3f, 3f);
	}
	

    void EnemySpawn()
    {
        Rigidbody2D enemyInstance;

        enemyInstance = Instantiate(Resources.Load("Prefabs/Dwarf"), new Vector3(Random.Range(-8, 8), Random.Range(-5, 5), 0), Quaternion.identity)as Rigidbody2D;
    }
}
