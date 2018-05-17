using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSortingLayer : MonoBehaviour {

    public string sortingLayer;
    public int orderInLayer;

    void Awake() {
        SetSortingLayer();
    }

    [ContextMenu("Update sorting layer settings")]
    void UpdateSortingLayerSettings() {
        SetSortingLayer();
    }

    private void SetSortingLayer() {
        Renderer rend = GetComponent<Renderer>();
        rend.sortingLayerName = sortingLayer;
        rend.sortingOrder = orderInLayer;
    }
}
