using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioScriptEnemy : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceWalking;
    [SerializeField] private AudioSource audioSourceGrunt;
    
    // [SerializeField] AudioClip audioWalk;
    // [SerializeField] AudioClip audioSFX;

    
    private void Start()
    {
        // _audioSource = GetComponent<AudioSource>();
        
    }


    private IEnumerator GruntSound()
    {

        
        
        float waitForSecond = Random.Range(0.5f, 2f);
        yield return new WaitForSeconds(waitForSecond);
    }

    private IEnumerator WalkSound()
    {
        yield return new WaitForSeconds(0.1f);
    }
    
    
    
}
