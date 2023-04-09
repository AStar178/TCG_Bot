using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUpdater : MonoBehaviour
{
    public bool onDeath;
    public bool onDestroy;
    public float onRangeRange;
    public bool onRange;
    public bool onIntract;
    public bool DestroyAfterComplete = true;

    public Quest UpdateThis;
    private void OnDestroy()
    {
        if (onDestroy)
        {
            UpdateThis.UpdateQuest();
        }
    }

    private void FixedUpdate()
    {

        bool gg = Physics.CheckSphere(gameObject.transform.position, onRangeRange, Static.PlayerLayer);
        if (gg)
        {
            UpdateThis.UpdateQuest();
            Destroy(this);
            TryGetComponent(out Marker marker);
            if (marker != null)
            Destroy(marker);
        }
    }
}
