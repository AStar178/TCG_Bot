using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticalClide : MonoBehaviour
{   [SerializeField] public UnityEvent<EnemyHp> unityEvent;
    private void OnParticleCollision(GameObject other) {
        
        if (other.TryGetComponent<EnemyHp>(out var s))
            unityEvent?.Invoke(s);

    }
}
