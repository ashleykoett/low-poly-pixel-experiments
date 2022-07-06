using UnityEngine;
using System.Collections;
public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float rotSpeed = 1.5f;
    public float offsetY = 5f;
    private float _rotY;
    private Vector3 _offset;

    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;
    }

    void LateUpdate()
    {
        _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        Vector3 lookPos = target.position - (rotation * _offset);
        transform.position = lookPos;

        transform.LookAt(target);

        lookPos.y += offsetY;
        transform.position = lookPos;
    }
}
