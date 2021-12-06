using UnityEngine;

public class RotateMuzzle : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 180f;
    [SerializeField] Vector3 targetDistance = new Vector3(0f, 1f, 2f);

    Transform target;

    float angle;

    void Start()
    {
        target = transform.parent.gameObject.transform;
    }

    void Update()
    {
        transform.position = target.position + Quaternion.Euler(0f, angle, 0f) * targetDistance;
        transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(target.position.x, target.position.y, target.position.z), Vector3.up);
        angle += rotateSpeed * Time.deltaTime;
        angle = Mathf.Repeat(angle, 360f);
    }
}
