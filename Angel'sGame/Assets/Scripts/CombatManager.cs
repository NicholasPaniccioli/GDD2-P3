using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<MonoBehaviour> {
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float iFrameDuration = 1;
    
    private List<Enemy> enemies;
    private List<GameObject> allyDamageSources;
    // Start is called before the first frame update
    void Start() {
        enemies = new List<Enemy>();
        allyDamageSources = new List<GameObject>(); //  Will be entity

        //  Will be enemies
        foreach(Enemy e in GameObject.FindObjectsOfType<Enemy>()) {
            if (e.gameObject == player)
                continue;
            enemies.Add(e);
        }
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        foreach(Enemy e in enemies) {
            if (e.gameObject.GetComponent<BoxCollider>().bounds.Intersects(player.GetComponent<BoxCollider>().bounds)) {
                if (player.GetComponent<Health>().iFrameTimeStamp <= Time.time) {
                    //  Player is being hit by the demon, take damage
                    player.GetComponent<Health>().TakeDamage(10);
                    player.GetComponent<Health>().iFrameTimeStamp = Time.time + iFrameDuration;
                    e.Intersecting = true;
                }
            }
            else if (e.Intersecting)
                 e.Intersecting = false;

            foreach(GameObject d in allyDamageSources) {
                if(d.GetComponent<SphereCollider>().bounds.Intersects(e.GetComponent<BoxCollider>().bounds) || d.GetComponent<SphereCollider>().bounds.Contains(e.GetComponent<BoxCollider>().bounds.center)) {
                    //  Enemy Colliding with damage source, damage will be handled differently later
                    if (e.GetComponent<Health>().iFrameTimeStamp <= Time.time) {
                        e.gameObject.GetComponent<Health>().TakeDamage(d.GetComponent<Ability>().Damage);
                        e.GetComponent<Health>().iFrameTimeStamp = Time.time + iFrameDuration;
                    }
                }
            }
        }
    }

    public void AddEnemy(Enemy e) {
        enemies.Add(e);
    }

    public void removeEnemy(Enemy e) {
        enemies.Remove(e);
    }

    public void addAllyDamageSource(GameObject newObj) {
        allyDamageSources.Add(newObj);
    }
    public void removeAllyDamageSource(GameObject newObj) {
        allyDamageSources.Remove(newObj);
    }
}
