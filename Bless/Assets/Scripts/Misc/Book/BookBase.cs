using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Book", menuName = "Items/Misc/Book")]
public class BookBase : ScriptableObject
{
    [SerializeField] string bookName;
    [SerializeField] string bookDescription;
    [TextArea ,SerializeField] string bookString;
}
