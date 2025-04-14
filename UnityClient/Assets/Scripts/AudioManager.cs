using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSource;

    public void playSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
