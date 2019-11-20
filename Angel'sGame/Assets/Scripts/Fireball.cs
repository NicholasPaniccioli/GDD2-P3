using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability
{
    [SerializeField]
    private float duration, maxSpeed;//Time until the projectile is deleted
    [SerializeField]
    private GameObject staff ;
    private float initializationTime;
    CombatManager combatManager;

    // Start is called before the first frame update
    void Start()
    {
        initializationTime = Time.timeSinceLevelLoad;
        combatManager = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
        combatManager.addAllyDamageSource(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += staff.transform.right * maxSpeed * Time.deltaTime;
        if (initializationTime + duration <= Time.time)
        {
            combatManager.removeAllyDamageSource(gameObject);
            Destroy(gameObject);
        }
    }
}
