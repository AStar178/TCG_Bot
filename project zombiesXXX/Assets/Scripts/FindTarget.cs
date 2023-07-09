using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class FindTarget : PlayerComponetSystem
    {
        public StarterAssetsInputs _input;
        public LayerMask EnemyLayer;
        public GameObject closest;

        void Update()
        {
            if (_input.findTarget == true)
            {
                // change the color of the old target if possible
                if (closest != null && closest.GetComponent<Outliner>() != null)
                    closest.GetComponent<Outliner>().OutlineColor = Color.white;

                Collider[] objects = Physics.OverlapSphere(transform.position, GetPlayer().PlayerState.ResultValue.AttackRange, EnemyLayer);
                //GameObject[] objects = GameObject.FindGameObjectsWithTag("Objetivo");
                float dot = -2;

                print(objects.Length);

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
                        closest = obj.gameObject;

                        print(obj);
                    }
                }

                // set the target to null if no target found
                if (objects.Length <= 0)
                    closest = null;

                // change the color of the new target if possible
                if (closest != null && closest.GetComponent<Outliner>() != null)
                    closest.GetComponent<Outliner>().OutlineColor = Color.red;
            }

            _input.findTarget = false;
        }
    }
}
