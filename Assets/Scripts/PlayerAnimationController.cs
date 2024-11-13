using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;          // Velocidad de movimiento
    public float rotationSpeed = 700.0f; // Velocidad de rotación
    public Animator animator;            // Referencia al Animator

    void Update()
    {
        // Obtener la entrada de movimiento
        float moveForward = Input.GetAxis("Vertical") * speed * Time.deltaTime; // W/S para adelante/atrás
        float moveSide = Input.GetAxis("Horizontal") * speed * Time.deltaTime; // A/D para izquierda/derecha

        // Determinar si se está moviendo (adelante o atrás)
        bool isMoving = moveForward != 0; // Si se está moviendo hacia adelante o atrás

        // Aplicar el movimiento
        transform.Translate(moveSide, 0, moveForward, Space.World);

        // Rotar el personaje solo si hay movimiento
        if (moveSide != 0 || moveForward != 0)
        {
            // Calcular el ángulo de rotación basado en la entrada lateral
            float targetAngle = Mathf.Atan2(moveSide, moveForward) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Actualizar el parámetro Speed en el Animator
        float totalMovement = Mathf.Abs(moveForward) + Mathf.Abs(moveSide);
        animator.SetFloat("Speed", totalMovement);

        // Actualizar el parámetro Running en el Animator
        animator.SetBool("Running", isMoving); // Establecer Running si se mueve hacia adelante o hacia atrás

        // Ataque
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }

        // Saltar
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("Jump");
        }
    }
}

