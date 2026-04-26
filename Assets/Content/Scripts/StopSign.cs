using UnityEngine;

/// <summary>A stop sign that registers a click and notifies the MiniGame2Manager.</summary>
[RequireComponent(typeof(Collider))]
public class StopSign : MonoBehaviour
{
    private bool _isClicked = false;

    /// <summary>Called by the camera raycast when the player clicks this sign.</summary>
    public void OnClicked()
    {
        if (_isClicked)
            return;

        _isClicked = true;
        MiniGame2Manager.Instance.RegisterSignClicked();
        Destroy(gameObject);
    }
}
