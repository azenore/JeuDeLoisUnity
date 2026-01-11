using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDatas", menuName = "Scriptable Objects/PlayerDatas")]
public class PlayerDatas : ScriptableObject
{
    [SerializeField] public int _cellNumber;
    [SerializeField] public int _money;
}
