using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class lightninEffect : MonoBehaviour
{
    [SerializeField] private bool alwaysOn = true;
    
    [SerializeField] private Volume postProssesningVolume;

    private Vector4 flashGamma = new Vector4(1.5f, 1.5f, 1.5f, 1.5f);
    private Vector4 normalGama = new Vector4(0f, 0f, 0f, 0f);

    [SerializeField] private float flashDuration;
    [SerializeField] private AudioClip[] thunderSound;
    private AudioSource audioSource;
    private LiftGammaGain _liftGammaGain;

    private void Awake()
    {
        Debug.Log("Awake");

        if (SceneManager.GetActiveScene().buildIndex != 0)
            GameManager.Instance.onEnemyLevelUp.AddListener(() => StartCoroutine(RandomLightning()));
        
        
        audioSource  = GetComponent<AudioSource>();
        
        StartCoroutine(RandomLightningTime());
    }
    
    private IEnumerator RandomLightningTime()
    {
        while (alwaysOn)
        {
            yield return new WaitForSeconds(Random.Range(3,9));
            if (!audioSource.isPlaying)
            {

                StartCoroutine(RandomLightning());
            }
            
        }
    }
    
 
    private IEnumerator RandomLightning()
    {
        audioSource.clip = thunderSound[Random.Range(0, thunderSound.Length)];

        if (postProssesningVolume.profile.TryGet<LiftGammaGain>(out _liftGammaGain))
        {
            Debug.Log("<Color=yellow><b>LiftGammaGain</b></b></Color>");
            
            _liftGammaGain.gamma.value = flashGamma;  
            
            
            yield return new WaitForSeconds(flashDuration);
            audioSource.PlayDelayed(Random.Range(0.1f,1f));
            
            _liftGammaGain.gamma.value = normalGama;    // Normal gamma
   
        }
    }
    
}
