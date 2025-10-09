using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using PrimeTween;

public class lightninEffect : MonoBehaviour
{
    [SerializeField] private Volume postProssesningVolume;

    private Vector4 flashGamma = new Vector4(2f, 2f, 2f, 2f);
    private Vector4 normalGama = new Vector4(1f, 1f, 1f, 1f);

    [SerializeField] private float flashDuration;
    [SerializeField] private AudioClip thunderSound;

    private LiftGammaGain _liftGammaGain;
    

    private void Start()
    {
        StartCoroutine(RandomLightning(thunderSound));
    }


    private IEnumerator RandomLightning(AudioClip audioClip)
    {
        while (true)
        {
                        Debug.Log(audioClip);
                        
                        Flash();
                        
                        yield return new WaitForSeconds(3);
        }

    }

    private void Flash()
    {
        if (postProssesningVolume.profile.TryGet<LiftGammaGain>(out _liftGammaGain))
        {
            Debug.Log(postProssesningVolume.profile);
            // Example values: You can tweak the Vector4 values
            Vector4 easedGamma = EaseOutVector4(normalGama, flashGamma, flashDuration);
            // liftGammaGain.gamma.value = easedGamma;
            
            // _liftGammaGain.lift.value = easedGamma;   // Red tint lift
            _liftGammaGain.gamma.value = easedGamma;     // Normal gamma
            // _liftGammaGain.gain.value = easedGamma; // Slight gain boost

        }
        
        
    }
    
    Vector4 EaseOutReversed(Vector4 start, Vector4 end, float t)
    {
        t = Mathf.Clamp01(t);
        float easedT = EaseOutCubic(1f - t);  // Reverse the direction
        Debug.Log(easedT);
        return Vector4.Lerp(start, end, easedT);
    }
    
    Vector4 EaseOutVector4(Vector4 start, Vector4 end, float t)
    {
        t = Mathf.Clamp01(t);
        float easedT = EaseOutCubic(t); // or EaseOut(t), EaseOutSine(t), etc.
        return Vector4.Lerp(start, end, easedT);
    }
    
    float EaseOutCubic(float t)
    {
        t = Mathf.Clamp01(t);
        return 1f - Mathf.Pow(1f - t, 3);
    }
    

}
