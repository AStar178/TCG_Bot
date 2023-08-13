using System.Collections.Generic;
using UnityEngine;

public class LOF : MonoBehaviour {
    
    [SerializeField] List<MonoBehaviour> monoBehaviours;
    [SerializeField] List<GameObject> objects;
    [SerializeField] List<Animation> animations;
    [HideInInspector]
    public bool anibale;
    public void DISABLE()
    {
        anibale = false;
        for (int i = 0; i < monoBehaviours.Count; i++)
        {
            if (monoBehaviours[i] != null)
                monoBehaviours[i].enabled = false;
        }
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] != null)
                objects[i].SetActive(false);
        }
        for (int i = 0; i < animations.Count; i++)
        {
            if (animations[i] != null)
               animations[i].enabled = false;
        }

    }

    private void Update() {
        
        if (anibale == false)
            return;
        var s = Vector3.Distance( Player.Current.PlayerEffect.transform.position , transform.position );
        if (s > Player.Current.PlayerEffect.LOR || Vector3.Dot( Player.Current.CameraControler.transform.forward , (Player.Current.CameraControler.transform.position - transform.position).normalized ) > 0)
        {
            DISABLE();
        }
            

    }

    public void Enable()
    {
        anibale = true;
        for (int i = 0; i < monoBehaviours.Count; i++)
        {
            if (monoBehaviours[i] != null)
                monoBehaviours[i].enabled = true;
        }
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] != null)
                objects[i].SetActive(true);
        }
        for (int i = 0; i < animations.Count; i++)
        {
            if (animations[i] != null)
                animations[i].enabled = true;
        }

    }


}