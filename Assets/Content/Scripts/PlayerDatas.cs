using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDatas", menuName = "Scriptable Objects/PlayerDatas")]
public class PlayerDatas : ScriptableObject
{
    [SerializeField] public int _cellNumber;
    [SerializeField] public int _money;
    [SerializeField] public bool _hasGavel;
    [SerializeField] public bool _hasGivenGavel;
    [SerializeField] public bool _isInitialized;

    /// <summary>
    /// Resets all values to their defaults and marks the data as uninitialized.
    /// Call this to start a new game.
    /// </summary>
    public void ResetToDefaults()
    {
        _cellNumber = 0;
        _money = 100;
        _hasGavel = false;
        _hasGivenGavel = false;
        _isInitialized = false;
    }

    /// <summary>
    /// Initializes default values only if this is the first time loading.
    /// Subsequent scene loads preserve the existing values.
    /// </summary>
    public void InitializeIfNeeded()
    {
        if (_isInitialized)
            return;

        _cellNumber = 0;
        _money = 100;
        _hasGavel = false;
        _hasGivenGavel = false;
        _isInitialized = true;
    }
}
