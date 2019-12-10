using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField]
    private AudioClip basicAttack, fireballAttack, hurt, hurt2, aoe, heal;

    private AudioSource source;

    // Start is called before the first frame update
    void Start() {
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// <summary>
    /// Plays the regular fire blast attack
    /// </summary>
    public void PlayBasicAttack() {
        source.PlayOneShot(basicAttack, 0.7f);
    }

    public void PlayFireballAttack() {
        source.PlayOneShot(fireballAttack, 0.7f);
    }

    public void PlayHurt() {
        source.PlayOneShot(hurt, 0.7f);
    }

    public void PlayHurt2() {
        source.PlayOneShot(hurt2, 0.7f);
    }

    public void PlayAoe() {
        source.PlayOneShot(aoe, 0.7f);
    }

    public void PlayHeal() {
        source.PlayOneShot(heal, 0.7f);
    }


}
