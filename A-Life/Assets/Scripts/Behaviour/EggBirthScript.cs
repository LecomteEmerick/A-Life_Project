using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EggBirthScript : MonoBehaviour {

    public GameObject Instance;
    public PoolledObjectClass InstanceInfos;

    public Renderer EggMatTop;
    public Renderer EggMatBottom;

    public AudioSource Source;
    public List<AudioClip> EggCrackingClip;

    public float step = 0.1f;

    //void Start()
    //{
    //    Debug.Log("Enlever moi pour la realese");
    //    StartCoroutine(Birth(1.0f));
    //}

    public void StartCountdownBirth()
    {
        float delay = Random.Range(15.0f, 60.0f);
        StartCoroutine(Birth(delay));
    }

    public void StartCountdownBirth(float delay)
    {
        StartCoroutine(Birth(delay));
    }

    private IEnumerator Birth(float second)
    {
        int numberTurn = (int)(1 / step);
        float alpha = 1.0f;
        yield return new WaitForSeconds(second);

        PoolledObjectClass obj = GameData.EnvironnementManagerInstance.GetObjectFromPool(GameData.PoolledObjectType.Creature);
        InstanceInfos.PoolledObjectRigibody.useGravity = false;
        InstanceInfos.PoolledObjectCollider.enabled = false;

        if (obj != null)
        {
            obj.PoolledObectInstance.transform.position = Instance.transform.position;
            obj.PoolledObectInstance.SetActive(true);
            //obj.PoolledObjectCollider.enabled = false;
        }

        int clipIndex = 0;
        while (numberTurn > 0)
        {
            if(numberTurn % 2 == 0)
            {
                Source.clip = EggCrackingClip[clipIndex];
                Source.Play();
                clipIndex = (clipIndex + 1) % EggCrackingClip.Count;
            }

            EggMatTop.material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, alpha));
            EggMatBottom.material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, alpha));
            alpha -= step;
            numberTurn--;
            yield return new WaitForSeconds(1.0f);
        }
        EggMatTop.material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.0f));
        EggMatBottom.material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 0.0f));

        obj.PoolledObjectCollider.enabled = true;
        obj.StartEvent.Invoke();

        InstanceInfos.Deactivate();
    }
}
