using UnityEngine;
using UnityEngine.Assertions;

public class Beacon : MonoBehaviour
{
    public bool IsActive { get; set; }

    [SerializeField]
    public GameObject Beam;

    public void Start()
    {
        Assert.IsNotNull(Beam);
    }

    public void ActivateBeacon()
    {
        if (!IsActive) {
            IsActive = true;
            Beam.SetActive(true);
        }
    }
}
