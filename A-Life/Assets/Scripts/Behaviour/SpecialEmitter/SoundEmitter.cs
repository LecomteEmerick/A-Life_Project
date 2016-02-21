using UnityEngine;
using System.Collections;

public class SoundEmitter : MonoBehaviour {

    [SerializeField]
    private SphereCollider SoundCollider;

    private HearInfosClass Sound;
    private EmitterClass<HearInfosClass> Emitter;

    void Start()
    {
        this.Sound = new HearInfosClass();

        this.Sound.LowFrequencyValue = 0.5f;
        this.Sound.MediumFrequencyValue = 0.5f;
        this.Sound.HightFrequencyValue = 0.5f;

        this.Sound.Volume = 0.0f;

        this.Emitter = new EmitterClass<HearInfosClass>(this.Sound);

        this.InvokeRepeating("ChangeSound", 2.0f, 5.0f);
        this.InvokeRepeating("SendSound", 0.0f, 1.0f);
    }

    void ChangeSound()
    {
        this.Sound.Volume = this.Sound.Volume == 0.0f ? 10.0f : 0.0f;

        this.SoundCollider.radius = 0.5f + this.Sound.Volume; 
    }

    void SendSound()
    {
        this.Emitter.Emit();
    }

    void OnTriggerEnter(Collider other)
    {
        CreatureClass creatureClass;
        if (GameData.CreatureManagerInstance.CreatureList.TryGetValue(other, out creatureClass))
        {
            this.Emitter.RegisterReceptor(creatureClass.SoundReceptor.Receptor);
            creatureClass.SoundReceptor.Receptor.Reception(this.Sound);
        }
        else
        {
            Debug.LogError("Collider cannot be found.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        CreatureClass creatureClass;
        if (GameData.CreatureManagerInstance.CreatureList.TryGetValue(other, out creatureClass))
        {
            this.Emitter.UnRegisterReceptor(creatureClass.SoundReceptor.Receptor);
        }
        else
        {
            Debug.LogError("Collider cannot be found.");
        }
    }
}
