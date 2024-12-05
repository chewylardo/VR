using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CuerdaArcoController : MonoBehaviour
{
    [SerializeField]
    private CuerdaArco cuerdaArcoRenderer;

    private XRGrabInteractable interactable;

    [SerializeField]
    private Transform notchPointGrabObject, notchPointVisualObject, notchPointParent;

    [SerializeField]
    private float cuerdaArcoStretchLength = 0.3f;

    private Transform interactor;

    private void Awake()
    {
        interactable = notchPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        interactable.selectEntered.AddListener(prepareCuerdaArco);
        interactable.selectExited.AddListener(resetCuerdaArco);
    }

    private void resetCuerdaArco(SelectExitEventArgs arg0)
    {
        interactor = null;
        notchPointGrabObject.localPosition = Vector3.zero;
        notchPointVisualObject.localPosition = Vector3.zero;
        cuerdaArcoRenderer.CreateString(null);
    }

    private void prepareCuerdaArco(SelectEnterEventArgs arg0)
    {
        interactor = arg0.interactorObject.transform;
    }

    private void Update()
    {
        if (interactable != null)
        {
            Vector3 notchPointLocalSpace = notchPointParent.InverseTransformPoint(notchPointGrabObject.position);

            float notchPointLocalZAbs = MathF.Abs(notchPointLocalSpace.z);
            HandleStringPushedBackToStart(notchPointLocalSpace);
            HandleStringPulledBackToLimit(notchPointLocalZAbs, notchPointLocalSpace);
            HandlePullingString(notchPointLocalZAbs,notchPointLocalSpace);

            cuerdaArcoRenderer.CreateString(notchPointVisualObject.transform.position);
            
        }
    }

    private void HandlePullingString(float notchPointLocalZAbs, Vector3 notchPointLocalSpace)
    {
        if(notchPointLocalSpace.z <0 && notchPointLocalZAbs < cuerdaArcoStretchLength)
        {
            notchPointVisualObject.localPosition = new Vector3(0, 0, notchPointLocalSpace.z);
        }
    }

    private void HandleStringPulledBackToLimit(float notchPointLocalZAbs, Vector3 notchPointLocalSpace)
    {
        if(notchPointLocalSpace.z < 0 && notchPointLocalZAbs >= cuerdaArcoStretchLength)
        {
            notchPointVisualObject.localPosition = new Vector3(0, 0, -cuerdaArcoStretchLength);
        }
    }

    private void HandleStringPushedBackToStart(Vector3 notchPointLocalSpace)
    {
        if(notchPointLocalSpace.z >= 0)
        {
            notchPointVisualObject.localPosition = Vector3.zero;
        }
    }
}
