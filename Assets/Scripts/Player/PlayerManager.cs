using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static int currentID = 1;

    public static int GetNewPlayerID()
    {
        return currentID++;
    }
}
