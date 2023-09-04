using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float cloudSpeed = 1.0f;
    public float cloudSpawnDistance = 10.0f;

    public Vector3 spawnPoint;

    private List<Transform> cloudList = new List<Transform>();

    private void Start()
    {
        // Busca todos los hijos (nubes) y guárdalos en la lista.
        foreach (Transform child in transform)
        {
            cloudList.Add(child);
        }
    }

    private void Update()
    {
        // Mueve cada nube hacia la izquierda.
        foreach (Transform cloud in cloudList)
        {
            cloud.Translate(Vector3.left * cloudSpeed * Time.deltaTime);

            // Si una nube está lo suficientemente lejos, vuelve a colocarla en el lado derecho de la pantalla.
            if (cloud.position.x < -cloudSpawnDistance)
            {
                RepositionCloud(cloud);
            }
        }
    }

    // Función para reposicionar una nube en el lado derecho de la pantalla.
    private void RepositionCloud(Transform cloud)
    {
        float maxX = 0.0f;

        // Encuentra la posición X máxima entre todas las nubes para evitar solapamientos.
        foreach (Transform c in cloudList)
        {
            maxX = Mathf.Max(maxX, c.position.x);
        }

        // Reposiciona la nube justo después de la nube más a la derecha.
        Vector3 newPosition = spawnPoint;
        newPosition.y = cloud.position.y; // Mantener la coordenada Y original de la nube.
        cloud.position = newPosition;
    }
}
