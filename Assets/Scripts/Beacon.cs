using UnityEngine;
using UnityEngine.Assertions;

public class Beacon : MonoBehaviour
{
    public bool IsActive { get; set; }

    [SerializeField]
    public GameObject Beam;

    public void Awake()
    {
        Assert.IsNotNull(Beam);
    }

    public void Start()
    {
        Debug.Log("Start");
    }

    public void ActivateBeacon()
    {
        if (!IsActive)
        {
            IsActive = true;
            Beam.SetActive(true);

            GameManager.Instance.PlaySound(GameManager.SoundEffectTypes.BEACON);
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            ActivateBeacon();
        }
    }
}
