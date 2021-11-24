using System.Collections.Generic;
using Flurl.Http;
using Mapbox.Unity.Location;
using UnityEngine;
using UnityEngine.Assertions;
using Mapbox.Unity.Map;
using System.Threading.Tasks;
using Mapbox.CheapRulerCs;
using System.Linq;

public class BeaconSpawner : MonoBehaviour
{

    [SerializeField]
    public GameObject Beacon;

    [SerializeField]
    public GameObject BeaconWrapper;

    [SerializeField]
    public AbstractMap Map;

    private Mapbox.Utils.Vector2d lastKnownLocation { get; set; }

    private int threshold = 200;

    async Task FetchBeacons(Mapbox.Utils.Vector2d location)
    {
        var pointOfInterests = await "http://localhost:3000/api/pois"
            .PostJsonAsync(new {
                userLocation = new {
                    lat = location.x,
                    lon = location.y
                },
                radius = 500,
            }).ReceiveJson<List<PointOfInterestDTO>>();

        if (BeaconWrapper.transform.childCount > 0)
        {
            var uuids = pointOfInterests.Select(x => x.Uuid).ToArray();

            foreach (Transform child in BeaconWrapper.transform)
            {
                var childProps = child.gameObject.GetComponent<FeatureSetter>();
                var childUuid = childProps.Props[MorbsConstants.UuidKey].ToString();

                if (!uuids.Contains(childUuid))
                {
                    GameObject.Destroy(child.gameObject);
                } else 
                {
                    var index = pointOfInterests.FindIndex(x => x.Uuid == childUuid);
                    pointOfInterests.RemoveAt(index);
                }
            }
        }

        pointOfInterests.ForEach(poi => {
            var coords = new Mapbox.Utils.Vector2d(poi.Latitude, poi.Longitude);
            var obj = Instantiate(Beacon, Map.GeoToWorldPosition(coords, false), Quaternion.identity, BeaconWrapper.gameObject.transform);
            obj.name = poi.Uuid;
            obj.gameObject.GetComponent<FeatureSetter>().Set(new Dictionary<string, object>() {
                [MorbsConstants.UuidKey] = poi.Uuid
            });
        });
    }

    void Start()
    {
        Assert.IsNotNull(BeaconWrapper);
        Assert.IsNotNull(Beacon);

        var lp = LocationProviderFactory.Instance.DefaultLocationProvider;

        lp.OnLocationUpdated += (Location location) => {
            var coords = location.LatitudeLongitude;

            var ruler = new CheapRuler(coords.x, CheapRulerUnits.Meters);
            var dist = ruler.Distance(new double[] {lastKnownLocation.y, lastKnownLocation.x}, new double[] {coords.y, coords.x});

            if (dist > threshold)
            {
                StartCoroutine("FetchBeacons", coords);
                lastKnownLocation = coords;
            }
        };
    }
}
