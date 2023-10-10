using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 pos;

    public static CameraShake instance;
    Cinemachine.CinemachineTargetGroup targetGroup;
    [SerializeField] Cinemachine.CinemachineVirtualCamera cm;
    IEnumerator c;

    CinemachineBasicMultiChannelPerlin cmf;
    public void addTargetGroup(Transform d)
    {
        if(targetGroup.m_Targets.Length>1)
        {
        targetGroup.RemoveMember(targetGroup.m_Targets[targetGroup.m_Targets.Length - 1].target);

        }
        targetGroup.AddMember(d,1,1);
    }
    private void Awake()
    {
        instance = this;
    }

    [Range(0.0F, 10.0F)]
    public float strenght;

    [Range(0.0F, 10.0F)]
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        targetGroup=GetComponent<Cinemachine.CinemachineTargetGroup>();
        cmf = cm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        pos = transform.position;
    
       
    }
    private IEnumerator _ProcessShake(float shakeIntensity , float shakeTiming )
    {
        Noise(shakeIntensity, shakeIntensity);
        yield return new WaitForSeconds(shakeTiming);
        Noise(0, 0);
    }

    public void Noise(float amplitudeGain, float frequencyGain)
    {
        cmf.m_AmplitudeGain= amplitudeGain;
        cmf.m_FrequencyGain = amplitudeGain;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Shake();
        }
      
    }
    public void Shake()
    {
        c = _ProcessShake(strenght, timer);
        StartCoroutine(c);
    }
}
