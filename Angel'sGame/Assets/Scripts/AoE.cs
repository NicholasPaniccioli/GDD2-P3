using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoE : Ability
{
    [SerializeField]
    private float duration, animationFrameTime, animationFrameDuration;//Time until the projectile is deleted
    private float initializationTime;
    CombatManager combatManager;
    SpriteRenderer renderer;
    public Sprite[] spriteList;
    private int currentSprite;

    // Start is called before the first frame update
    void Start()
    {
        initializationTime = Time.time;
        combatManager = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
        combatManager.addAllyDamageSource(gameObject);
        currentSprite = 0;
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (initializationTime + duration <= Time.time){
            combatManager.removeAllyDamageSource(gameObject);
            Destroy(gameObject);
        }
        if (animationFrameDuration > animationFrameTime) {
            if (currentSprite+1==spriteList.Length)
                currentSprite = 0;
            else
                currentSprite++;
            renderer.sprite = spriteList[currentSprite];
            animationFrameTime = 0;
        }
        animationFrameTime += Time.deltaTime;
    }
}
