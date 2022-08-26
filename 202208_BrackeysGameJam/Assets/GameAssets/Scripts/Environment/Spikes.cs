using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public GameObject spikes;

    // Start is called before the first frame update
    void Start()
    {
        int LayerSpikes = LayerMask.NameToLayer("Spikes");
        spikes.layer = LayerSpikes;
    }
}
