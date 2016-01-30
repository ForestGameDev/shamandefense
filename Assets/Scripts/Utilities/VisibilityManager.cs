using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class VisibilityManager : MonoBehaviour {

    public bool isVisible = true;
    private bool wasVisible = true;

    // Update is called once per frame
    void Update()
    {
        if (isVisible != wasVisible)
        {
            SetVisibility(this.gameObject);
            wasVisible = isVisible;
        }
    }

    private void SetVisibility(GameObject obj)
    {
        foreach (Transform child in obj.GetComponentsInChildren<Transform>())
        {
            Renderer childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null)
                childRenderer.enabled = isVisible;
        }
        Renderer theRenderer = obj.GetComponent<Renderer>();
        if (theRenderer != null)
            theRenderer.enabled = isVisible;
    }
}
