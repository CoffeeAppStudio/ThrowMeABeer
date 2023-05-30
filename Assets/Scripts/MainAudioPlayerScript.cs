using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudioPlayerScript : MonoBehaviour
{
    public AudioClip Ambiance;
    public AudioClip People;
    public AudioClip Bell;
    public AudioClip ThrowSound;
    public AudioClip BrokenGlass;
    public AudioClip GlassCollide;
    

    private AudioSource ambianceSource;
    private AudioSource peopleSource;
    private AudioSource bellSource;
    private AudioSource throwSource;
    private AudioSource brokenGlassSource;
    private AudioSource glassCollideSource;

    public static MainAudioPlayerScript instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        // Create an AudioSource for each clip
        ambianceSource = gameObject.AddComponent<AudioSource>();
        peopleSource = gameObject.AddComponent<AudioSource>();
        bellSource = gameObject.AddComponent<AudioSource>();
        throwSource = gameObject.AddComponent<AudioSource>();
        brokenGlassSource = gameObject.AddComponent<AudioSource>();
        glassCollideSource = gameObject.AddComponent<AudioSource>();

        // Assign the clips to their respective AudioSource
        ambianceSource.clip = Ambiance;
        peopleSource.clip = People;
        bellSource.clip = Bell;
        throwSource.clip = ThrowSound;
        brokenGlassSource.clip = BrokenGlass;
        glassCollideSource.clip = GlassCollide;

        // Set the AudioSource to loop
        ambianceSource.loop = true;
        peopleSource.loop = true;

        // Play the audio
        ambianceSource.Play();
        peopleSource.Play();
        bellSource.Play();
    }

    public void playBell()
    {
        bellSource.Play();
    }
    
    public void playThrowSound()
    {
        throwSource.Play();
    }
    
    public void playBrokenGlass()
    {
        brokenGlassSource.Play();
    }
    
    public void playGlassColliding()
    {
        glassCollideSource.Play();
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
