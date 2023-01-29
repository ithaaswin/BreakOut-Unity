using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] allAudio;

    void Awake() {
        instance = this;
        allAudio = GetComponents<AudioSource>();
    }
    
    public void brick(){
        allAudio[0].Play();
    }

    public void powerUp(){
        allAudio[1].Play();
    }
    public void levelUp(){
        allAudio[2].Play();
    }

    public void gameWon(){
        allAudio[3].Play();
    }

    public void powerDown(){
        allAudio[4].Play();
    }
    
    public void stopMainMusic(){
        allAudio[5].Stop();
    }
}
