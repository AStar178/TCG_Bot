using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClassSelector : MonoBehaviour
{
    public Player player;
    public GameObject Canves;
    public Image Sprite;
    public TMP_Text Name;
    public TMP_Text Counter;
    public TMP_Text Description;

    public List<GameObject> Classes;
    [SerializeField] int SelectedClass;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        SetClassOnMyFace();
    }

    public void Next()
    {
        SelectedClass++;
        if (SelectedClass > Classes.Count - 1)
            SelectedClass = 0;
        SetClassOnMyFace();
    }

    public void Back()
    {
        SelectedClass--;
        if (SelectedClass < 0)
            SelectedClass = Classes.Count - 1;
        SetClassOnMyFace();
    }

    public void SetClass()
    {
        Canves.SetActive(false);
        var b = Instantiate(Classes[SelectedClass]);
        b.transform.SetParent(player.Body);
        b.transform.localPosition = Vector3.zero;
        player.PlayerWeaponManger.CurrentWeapons = b.GetComponent<AbilityWeapons>();
        if (player.PlayerWeaponManger.CurrentWeapons == null) { return; }

        player.PlayerWeaponManger.CurrentWeapons.StartAbilityWp(Player.Singleton);
        player.WeaponCheck();
    }

    public void SetClassRandom()
    {
        Canves.SetActive(false);
        var b = Instantiate(Classes[Random.Range(1,Classes.Count)]);
        b.transform.SetParent(player.Body);
        b.transform.localPosition = Vector3.zero;
        player.PlayerWeaponManger.CurrentWeapons = b.GetComponent<AbilityWeapons>();
        if (player.PlayerWeaponManger.CurrentWeapons == null) { return; }

        player.PlayerWeaponManger.CurrentWeapons.StartAbilityWp(Player.Singleton);
        player.WeaponCheck();
    }

    private void SetClassOnMyFace()
    {
        Classes[SelectedClass].GetComponent<AbilityWeapons>().GetSprite();
        Sprite.sprite = Classes[SelectedClass].GetComponent<AbilityWeapons>().image;
        Name.text = Classes[SelectedClass].GetComponent<AbilityWeapons>().WeaponName;
        Counter.text = (SelectedClass + 1) + "/" + Classes.Count;
        Description.text = Classes[SelectedClass].GetComponent<AbilityWeapons>().WeaponDescription;
    }
}
