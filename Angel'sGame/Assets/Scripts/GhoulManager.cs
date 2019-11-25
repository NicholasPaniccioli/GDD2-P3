using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulManager : MonoBehaviour
{
    private GameObject[] pathList;
    private GameObject currentNode;
    private CombatManager combatManager;
    [SerializeField]
    private GameObject dresden, ghoulPrefab;
    // Start is called before the first frame update
    void Start()
    {
        pathList = GameObject.FindGameObjectsWithTag("Path");
        combatManager = gameObject.GetComponent<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (dresden.transform.position.y > -36.95f)

    }
}
