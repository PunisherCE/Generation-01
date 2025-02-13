using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource soundSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioClip clip = Resources.Load($"Music/Sueno_de_Amor") as AudioClip;
        soundSource.clip = clip;
        soundSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
