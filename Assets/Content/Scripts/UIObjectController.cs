using UnityEngine;

public class UIObjectController : MonoBehaviour
{
    [SerializeField] private GameObject _objectGavel;

    private void Start()
    {
        _objectGavel.SetActive(false);
    }

    public void HasGavel()
    {
        _objectGavel.SetActive(true);
    }
}
