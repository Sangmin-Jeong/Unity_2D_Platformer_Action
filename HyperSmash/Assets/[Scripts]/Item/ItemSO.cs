////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: ItemSO.cs
//StudentName: Sangmin Jeong
//StudentID: 101369732
//Last Modified On: 19/11/2023
//Program Description: GAME2014-Mobile
//Revision History: V1.0
////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    HEALTHUP,
    POWERUP,
    SCORE,
}
[CreateAssetMenu(menuName = "ItemSO/item")]
public class ItemSO : ScriptableObject
{
    public ItemType _type;
    public float _degree;

    public void UseItem(GameObject _target)
    {
        switch (_type)
        {
            case ItemType.HEALTHUP:
                _target.GetComponent<Character>().GetHeal(_degree);
                break;
            case ItemType.POWERUP:
                _target.GetComponent<Character>()._knockback.PowerUpForSeconds(_degree);
                break;
            case ItemType.SCORE:
                GameInstance.Instance._score += (int)_degree;
                break;
        }
    }
}
