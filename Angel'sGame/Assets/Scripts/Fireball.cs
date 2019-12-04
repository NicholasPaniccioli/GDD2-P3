using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability
{
    [SerializeField]
    private float duration, maxSpeed;//Time until the projectile is deleted
    public GameObject staff ;
    private float initializationTime;
    CombatManager combatManager;
    Vector3 crosshairPosition;

    // Start is called before the first frame update
    void Start()
    {
        crosshairPosition = staff.transform.right;
        initializationTime = Time.timeSinceLevelLoad;
        combatManager = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
        combatManager.addAllyDamageSource(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += crosshairPosition * maxSpeed * Time.deltaTime;
        if (initializationTime + duration <= Time.timeSinceLevelLoad)
        {
            combatManager.removeAllyDamageSource(gameObject);
            Destroy(gameObject);
        }
    }
}
