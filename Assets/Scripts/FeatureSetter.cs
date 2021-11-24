using System.Collections.Generic;
using Mapbox.Unity.MeshGeneration.Interfaces;
using UnityEngine;

public class FeatureSetter : MonoBehaviour, IFeaturePropertySettable
{
    public Dictionary<string, object> Props { get; private set; }

    public void Set(Dictionary<string, object> props)
    {
        this.Props = props;
    }
}
