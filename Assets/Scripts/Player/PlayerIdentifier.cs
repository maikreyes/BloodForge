using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdentifier : MonoBehaviour
{
    public int playerID;

    void OnEnable()
    {
        // Usar el playerIndex que asigna Unity
        playerID = GetComponent<PlayerInput>().playerIndex;
    }
}
