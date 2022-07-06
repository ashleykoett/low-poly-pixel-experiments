using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraRaycast : MonoBehaviour
{
    public Transform target;

    private float _rayLength;
    private List<GameObject> _hitGOs = new List<GameObject>();

    // Update is called once per frame
    void FixedUpdate()
    {
        int layerMask = 1 << 7;
        RaycastHit[] hits;

        Vector3 direction = (target.position - transform.position).normalized;
        _rayLength = Vector3.Distance(transform.position, target.position);

        hits = Physics.RaycastAll(transform.position, direction, _rayLength, layerMask);
        List<GameObject> currentGameObjects = new List<GameObject>();
        
        for(int i=0;i<hits.Length;i++)
        {
            RaycastHit hit = hits[i];
            GameObject hitGO = hit.transform.gameObject;
            Renderer rend = hit.transform.GetComponent<Renderer>();
            currentGameObjects.Add(hitGO);

            if (!_hitGOs.Contains(hitGO) && rend)
            {
                rend.material.shader = Shader.Find("Transparent/Diffuse");
                Color tempColor = rend.material.color;
                tempColor.a = 0.3f;
                rend.material.color = tempColor;
                _hitGOs.Add(hitGO);
            }
        }

        foreach(GameObject go in _hitGOs)
        {
            if(!currentGameObjects.Contains(go))
            {
                Renderer rend = go.GetComponent<Renderer>();
                rend.material.shader = Shader.Find("Standard (Specular setup)");
                Color tempColor = rend.material.color;
                tempColor.a = 1.0f;
                rend.material.color = tempColor;
                _hitGOs.Remove(go);
            }
        }
    }
}
