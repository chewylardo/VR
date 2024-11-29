using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class MostrarBienvenida: MonoBehaviour
{
    public GameObject canvasBienvenida; 
    public Transform camaraJugador;
    public InputActionProperty gatillo; 
    public float distanciaCanvas = 2f;
    public float alturaCanvas = 0f; 

    private bool visible = true;

    void Start()
    {
        if (canvasBienvenida == null)
        {
            Debug.LogError("Canvas de bienvenida no asignado.");
            enabled = false;
        }

        if (camaraJugador == null)
        {
            Debug.LogError("Cámara del jugador no asignada.");
            enabled = false;
        }
    }

    void Update()
    {
        if (canvasBienvenida != null && camaraJugador != null && visible)
        {
            // Posiciona el Canvas frente a la cámara
            Vector3 posicionCanvas = camaraJugador.position + camaraJugador.forward * distanciaCanvas;
            posicionCanvas.y += alturaCanvas; // Ajusta la altura del Canvas
            canvasBienvenida.transform.position = posicionCanvas;

            // Asegura que el Canvas esté orientado hacia la cámara
            canvasBienvenida.transform.LookAt(camaraJugador);
            canvasBienvenida.transform.Rotate(0, 180, 0); // Corrige la orientación (opcional)

            // Verifica si el usuario presiona el gatillo
            if (gatillo.action != null && gatillo.action.WasPressedThisFrame())
            {
                canvasBienvenida.SetActive(false); // Oculta el Canvas
                visible = false; 
            }
        }
    }
}
