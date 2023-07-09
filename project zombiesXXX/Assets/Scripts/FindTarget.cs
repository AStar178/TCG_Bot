using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class FindTarget : PlayerComponetSystem
    {
        public StarterAssetsInputs _input;
        public LayerMask EnemyLayer;
        public GameObject Target;

        void Update()
        {
            if (_input.findTarget == true)
            {
                // change the color of the old target if possible
                if (Target != null && Target.GetComponent<Outliner>() != null)
                {   
                    if (Target.TryGetComponent<Outliner>( out var outliner ))
                    {
                        outliner.enabled = false;
                    }
                }
                    

                Collider[] objects = Physics.OverlapSphere(transform.position, GetPlayer().PlayerState.ResultValue.AttackRange, EnemyLayer);
                //GameObject[] objects = GameObject.FindGameObjectsWithTag("Objetivo");
                float dot = -2;

                foreach (Collider obj in objects)
                {
                    // store the Dot compared to the camera's forward position (or where the object is locally in the camera's space)
                    // Very important that the point is normalized.

                    Vector3 localPoint = Camera.main.transform.InverseTransformPoint(obj.transform.position).normalized;
                    Vector3 forward = Vector3.forward;
                    float test = Vector3.Dot(localPoint, forward);
                    if (test > dot)
                    {
                        dot = test;
                        Target = obj.gameObject;
                    }
                }

                // set the target to null if no target found
                if (objects.Length <= 0)
                    Target = null;

                // change the color of the new target if possible
                if (Target != null)
                    Target.GetComponent<Outliner>().enabled = true;
            }

            _input.findTarget = false;
        }
    }
}
