using UnityEngine;

public class MovementDiana : MonoBehaviour
{
    public GameObject objeto; 
    public float distanciaMaxima = 5f;
    public float velocidad = 2f; 
    private int direccion = 1; 
    private Vector3 posicionInicial; 

    void Start()
    {
        if (objeto == null)
        {
            Debug.LogError("No se ha asignado un objeto. Arrastra un objeto al campo 'Objeto' en el inspector.");
            enabled = false;
            return;
        }

  
        posicionInicial = objeto.transform.position;
    }

    void Update()
    {
        if (objeto != null)
        {
           
            objeto.transform.Translate(Vector3.forward * direccion * velocidad * Time.deltaTime);

            float distanciaActual = objeto.transform.position.z - posicionInicial.z;

            if (Mathf.Abs(distanciaActual) >= distanciaMaxima)
            {
                direccion *= -1; 

                float exceso = Mathf.Abs(distanciaActual) - distanciaMaxima;
                objeto.transform.position -= new Vector3(0, 0, direccion * exceso);
            }
        }
    }
}
