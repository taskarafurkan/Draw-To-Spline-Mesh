using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    [SerializeField] private Vector3 _axis;
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Rotate(_axis * _speed * Time.deltaTime);
    }
}
