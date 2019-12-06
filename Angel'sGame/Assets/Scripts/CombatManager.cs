using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<MonoBehaviour> {
    [SerializeField]
    private Player player;
    [SerializeField]
    private float iFrameDuration = 1;
    
    private List<Enemy> enemies;
    private List<GameObject> allyDamageSources;
    // Start is called before the first frame update
    void Start() {
        enemies = new List<Enemy>();
        allyDamageSources = new List<GameObject>(); //  Will be entity

        //  Will be enemies
        foreach(Enemy e in GameObject.FindObjectsOfType<Enemy>())
            enemies.Add(e);
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        for(int i = 0; i < enemies.Count; i++) {
            if (i >= enemies.Count)
                continue;
            if (enemies[i].gameObject.GetComponent<BoxCollider>().bounds.Intersects(player.GetComponent<BoxCollider>().bounds)) {
                if (player.IFrameTimeStamp <= Time.time) {
                    player.TakeDamage(10);
                    enemies[i].Intersecting = true;
                }
            }
            else if (enemies[i].Intersecting)
                 enemies[i].Intersecting = false;

            foreach(GameObject d in allyDamageSources) {
                if(d.GetComponent<SphereCollider>().bounds.Intersects(enemies[i].GetComponent<BoxCollider>().bounds) || d.GetComponent<SphereCollider>().bounds.Contains(enemies[i].GetComponent<BoxCollider>().bounds.center)) {
                    if (enemies[i].IFrameTimeStamp <= Time.time) {
                        enemies[i].TakeDamage(d.GetComponent<Ability>().Damage);
                        
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
