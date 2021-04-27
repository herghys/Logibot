using UnityEngine;

public class EndBarrierField : ForceField
{
    public GameManager manager;

    public void Shield()
    {
        bool end = manager.endUnlocked;
        gameObject.SetActive(!end);
        /*if (end)
        {
*//*            if (gameObject.activeSelf == false)
            {
                gameObject.SetActive(true);
            }*//*
        }
        else
        {
            gameObject.SetActive(!end)
        }*/
    }

}