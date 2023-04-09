using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue {
    public class AIConversant : MonoBehaviour
    {
        [SerializeField] string Name;
        [SerializeField] Dialogue Dialogue;

        public void OnIntract(PlayerConversant playerConversant)
        {
            playerConversant.StartDialogue(this ,Dialogue);
        }

        public string GetName()
        {
            return Name;
        }

        public void Destoy()
        {
            Destroy(this);
        }
    } 
}
