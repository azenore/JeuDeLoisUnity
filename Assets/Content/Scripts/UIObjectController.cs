using UnityEngine;

public class UIObjectController : MonoBehaviour
{
    [SerializeField] private GameObject _objectGavel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _objectGavel.SetActive(false);
    }

    public void HasGavel()
    {
        _objectGavel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
