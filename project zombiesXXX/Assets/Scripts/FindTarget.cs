using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace StarterAssets
{
    public class FindTarget : PlayerComponetSystem
    {
        public StarterAssetsInputs _input;
        public LayerMask EnemyLayer;
        public GameObject Target;
        [SerializeField] Transform Targexxxxx;
        [SerializeField] Rig rig;
        bool TargetSelectetMode;
        void Update()
        {
            if (_input.findTarget == true)
            {
                TargetSelectetMode = TargetSelectetMode == true ? false : true;
            }
            TargetSelecting();
            if (TargetSelectetMode)
            {
                // change the color of the old target if possible
                if (Target != null && Target.GetComponent<Outliner>() != null)
                {   
                    Target.GetComponent<Outliner>().AlwaysRed = true;
                    Target.GetComponent<Outliner>().enabled = false;
                }
                    

                Collider[] objects = Physics.OverlapSphere(transform.position, Player.PlayerState.ResultValue.AttackRange, EnemyLayer);
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
                if (Target != null && Target.TryGetComponent<Outliner>(out var target))
                {
                    target.AlwaysRed = false;
                    target.enabled = true;
                }

            }
            else
            {
                if (Target != null && Target.TryGetComponent<Outliner>(out var target))
                {
                    target.AlwaysRed = true;
                    target.enabled = false;
                    Target = null;
                }
                
            }

            
            
            _input.findTarget = false;
        }

        private void TargetSelecting()
        {
            if (Target != null)
            {
                Targexxxxx.transform.position = Target.transform.position;
                rig.weight = Mathf.Lerp( rig.weight , 1 , 0.1f );
                return;
            }
            rig.weight = Mathf.Lerp( rig.weight , 0 , 0.1f );
        }
    }
}
