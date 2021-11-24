using UnityEngine;
using UnityEngine.Assertions;

public class Beacon : MonoBehaviour
{
    public bool IsActive { get; set; }
    
    private FeatureSetter Features { get; set; }

    [SerializeField]
    public GameObject Beam;

    public void Awake()
    {
        Assert.IsNotNull(Beam);
        Features = FindObjectOfType<FeatureSetter>();
    }

    private void ActivateBeacon(bool playSound)
    {
        Beam.SetActive(true);

        if (playSound) {
            GameManager.Instance.PlaySound(GameManager.SoundEffectTypes.BEACON);
        }
    }

    private void SaveBeaconState()
    {
        var id = Features.Props[MorbsConstants.UuidKey].ToString();
        GameManager.Instance.SaveBeaconToState(id);
    }

    public void OnTriggerEnter(Collider collider)
    {
        var id = Features.Props[MorbsConstants.UuidKey].ToString();

        if (collider.tag == "Player" && !GameManager.Instance.IsBeaconActive(id))
        {
            ActivateBeacon(true);
            SaveBeaconState();
        }
    }

    public void Start()
    {
        var id = Features.Props[MorbsConstants.UuidKey].ToString();
        
        if (id != null && GameManager.Instance.IsBeaconActive(id))
        {
            ActivateBeacon(false);
        }
    }
}
