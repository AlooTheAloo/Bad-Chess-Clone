using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : MonoBehaviour
{
    private const float maxBoardSize = 150f;
    public int currPosX = 0;
    public int currPosY = 0;
    public void RefreshPos(int destX, int destY)
    {
        currPosX = destX;
        currPosY = destY;
    }
    private void Start()
    {
        FindForBack();
    }

    public void FindForBack()
    {
        FindCurrentPos(transform.localPosition.x, transform.localPosition.y);
    }

    void FindCurrentPos(float destX, float destY)
    {
        List<float> list = new List<float>();
        for (int i = (int)-maxBoardSize + 10; i < maxBoardSize; i += 40)
        {
            list.Add(i);
        }
        currPosX = list.IndexOf(Mathf.Round(destX));
        currPosY = list.IndexOf(Mathf.Round(destY));
    }

    public List<string> FindEndangeredPositions()
    {
        List<string> retval = new List<string>();
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                if (Validate(i, j))
                    retval.Add(i + "|" + j);
        return retval;
    }

    public bool Validate(int destX, int destY)
    {
        if (destY == currPosY && destX == currPosX) return false;

        if (Mathf.Abs(destX - currPosX) == Mathf.Abs(destY - currPosY)) {
            for (int i = 1; i < Mathf.Abs(destX - currPosX); i++)
                if (GameManager.PieceExists(
                    destX > currPosX ?
                    currPosX + i : currPosX - i,
                    destY > currPosY ?
                    currPosY + i : currPosY - i)) return false; 


            if (!GameManager.PieceExists(destX, destY)) { return true; }
            if (GameManager.PieceExists(destX, destY).GetComponent<Movement>().team == GetComponent<Movement>().team) return false;
            else
            {
                return true;
            }
        }
        else return false;


    }
}
