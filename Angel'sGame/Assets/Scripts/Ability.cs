using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    //Eventually this should have methods for determining if the ability is on cooldown, increasing and decreasing control of the character, and destroying the object whenever the spell runs out
    //however that's going to involve rewriting stuff that I'm not doing tonight, so rn this is literally an entire script to hold a float.
    //Tl;DR  Don't kill me Jacob.  ~Steven
    [SerializeField]
    protected float damage;
    [SerializeField]

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

   void Update()
    {

    }
}
