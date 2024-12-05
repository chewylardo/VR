using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public float m_speed = 2000.0f;
    public Transform m_transform = null;

    private Rigidbody m_Rigidbody = null;
    private bool m_IsStopped = true;
    private Vector3 m_LastPosition = Vector3.zero;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (m_IsStopped)
            return;

        m_Rigidbody.MoveRotation(Quaternion.LookRotation(m_Rigidbody.velocity, transform.up));

        if (Physics.Linecast(m_LastPosition, m_transform.position))
        {
            Stop();
        }

        m_LastPosition = m_transform.position;
    }

    private void Stop()
    {
        m_IsStopped = true;
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.useGravity = false;
    }
    public void Fire(float pullValue)
    {
        m_IsStopped = false;
        transform.parent = null;

        m_Rigidbody.isKinematic = false;
        m_Rigidbody.useGravity = true;
        m_Rigidbody.AddForce(transform.forward * (pullValue * m_speed));

        Destroy(gameObject, 5.0f);
    }
}
