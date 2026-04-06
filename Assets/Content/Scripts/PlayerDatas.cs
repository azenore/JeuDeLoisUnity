using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDatas", menuName = "Scriptable Objects/PlayerDatas")]
public class PlayerDatas : ScriptableObject
{
    [SerializeField] public int _cellNumber;
    [SerializeField] public int _money;
    [SerializeField] public bool _hasGavel;
    [SerializeField] public bool _hasGivenGavel;
    private static bool s_isInitialized = false;

    public void ResetToDefaults()
    {
        _cellNumber = 0;
        _money = 100;
        _hasGavel = false;
        _hasGivenGavel = false;
        s_isInitialized = false;
    }

    public void InitializeIfNeeded()
    {
        if (s_isInitialized)
            return;

        _cellNumber = 0;
        _money = 100;
        _hasGavel = false;
        _hasGivenGavel = false;
        s_isInitialized = true;
    }
}
