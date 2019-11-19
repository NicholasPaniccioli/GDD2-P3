using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<MonoBehaviour> {
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float iFrameDuration = 1;

    private float iFrameTimeStamp;
    private List<Enemy> enemies;
    private List<GameObject> allyDamageSources;
    // Start is called before the first frame update
    void Start() {
        enemies = new List<Enemy>();
        allyDamageSources = new List<GameObject>(); //  Will be entity
        iFrameTimeStamp = Time.time;

        //  Will be enemies
        foreach(Enemy e in GameObject.FindObjectsOfType<Enemy>()) {
            if (e.gameObject == player)
                continue;
            enemies.Add(e);
        }
        foreach (GameObject proj in GameObject.FindGameObjectsWithTag("allyDamageSource"))
            allyDamageSources.Add(proj);
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        foreach(Enemy e in enemies) {
            if(iFrameTimeStamp <= Time.time &&
                e.gameObject.GetComponent<BoxCollider2D>().IsTouching(player.GetComponent<BoxCollider2D>())) {
                //  Player is being hit by the demon, take damage
                player.GetComponent<Health>().TakeDamage(10);
                iFrameTimeStamp = Time.time + iFrameDuration;
            }

            foreach(GameObject d in allyDamageSources) {
                if(d.GetComponent<CircleCollider2D>().IsTouching(e.gameObject.GetComponent<BoxCollider2D>())) {
                    //  Enemy Colliding with damage source, damage will be handled differently later
                    e.gameObject.GetComponent<Health>().TakeDamage(10);
                }
            }
        }
    }

    public void AddEnemy(Enemy e) {
        enemies.Add(e);
    }

    public void addAllyDamageSource(GameObject newObj) {
        allyDamageSources.Add(newObj);
    }
}
