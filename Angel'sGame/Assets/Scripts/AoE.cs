using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoE : Ability
{
    [SerializeField]
    private float duration, animationFrameTime, animationFrameDuration;//Time until the projectile is deleted
    [SerializeField]
    private Sprite[] spriteList;
    private float initializationTime;
    CombatManager combatManager;
    SpriteRenderer renderer;
    private int currentSprite;
    private CircleCollider2D footCollider;

    // Start is called before the first frame update
    void Start()
    {
        initializationTime = Time.time;
        combatManager = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
        combatManager.addAllyDamageSource(gameObject);
        currentSprite = 0;
        damage = float.MaxValue;
        renderer = GetComponent<SpriteRenderer>();
        footCollider = GameObject.Find("FeetCollider").GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (initializationTime + duration <= Time.time){
            combatManager.removeAllyDamageSource(gameObject);
            Destroy(gameObject);
        }
        if (animationFrameDuration < animationFrameTime) {
            if (currentSprite+1==spriteList.Length)
                currentSprite = 0;
            else
                currentSprite++;
            renderer.sprite = spriteList[currentSprite];
            animationFrameTime = 0;
        }
        animationFrameTime += Time.deltaTime;
        transform.localScale = new Vector3(transform.localScale.x * 1.03f, transform.localScale.y * 1.03f, transform.localScale.z * 1.03f);
    }
}
