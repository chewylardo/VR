using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRArco : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float grabThreshold = 0.15f;
    public Transform start;
    public Transform end;
    public Transform socket;

    public InputActionProperty gatillo; // Acción asignada al botón Trigger

    private Transform pullingHand = null;
    private Flecha currentArrow = null;
    //private Animator animator = null;

    private float pullValue = 0.0f;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(CreateArrow(0.0f));
    }

    private void Update()
    {
        // Verificar si la cuerda está siendo tirada
        if (pullingHand != null && currentArrow != null)
        {
            pullValue = CalculatePull(pullingHand);
            pullValue = Mathf.Clamp(pullValue, 0.0f, 1.0f);
            //animator.SetFloat("Blend", pullValue);
        }

        // Verificar si se presiona el botón Trigger para disparar
        if (gatillo.action != null && gatillo.action.IsPressed())
        {
            Release();
        }
    }

    private float CalculatePull(Transform pullHand)
    {
        Vector3 direction = end.position - start.position;
        float magnitude = direction.magnitude;

        direction.Normalize();
        Vector3 difference = pullHand.position - start.position;

        return Vector3.Dot(difference, direction) / magnitude;
    }

    private IEnumerator CreateArrow(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        GameObject arrowObject = Instantiate(arrowPrefab, socket);
        arrowObject.transform.localPosition = new Vector3(0, 0, 0.425f);
        arrowObject.transform.localEulerAngles = Vector3.zero;

        currentArrow = arrowObject.GetComponent<Flecha>();
    }

    public void Pull(XRBaseInteractor hand)
    {
        Transform handTransform = hand.transform;
        float distance = Vector3.Distance(handTransform.position, start.position);

        if (distance > grabThreshold)
        {
            return;
        }

        pullingHand = handTransform;
    }

    public void Release()
    {
        if (pullValue > 0.25f)
        {
            FireArrow();
        }

        pullValue = 0.0f;
        //animator.SetFloat("Blend", 0);

        if (currentArrow == null)
        {
            StartCoroutine(CreateArrow(0.25f));
        }
    }

    private void FireArrow()
    {
        currentArrow.Fire(pullValue);
        currentArrow = null;
    }
}
