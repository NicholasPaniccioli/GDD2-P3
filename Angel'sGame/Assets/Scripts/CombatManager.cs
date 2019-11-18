using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<MonoBehaviour> {
    [SerializeField]
    private GameObject player;
    private List<GameObject> interactibles;

    // Start is called before the first frame update
    void Start() {
        interactibles = new List<GameObject>();
        foreach(Health h in GameObject.FindObjectsOfType<Health>()) {
            if (h.gameObject == player)
                continue;

        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void AddEnemy(GameObject newObj) {

    }

    public void AddInteractible(GameObject newObj) {

    }
}
