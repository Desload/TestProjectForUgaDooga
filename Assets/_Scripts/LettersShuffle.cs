using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using UnityEngine;

public class LettersShuffle : MonoBehaviour
{
    private LettersHolder lettersHolder;

    private void Awake()
    {
        lettersHolder = GetComponent<LettersHolder>();
    }

    public void ShuffleLatters()
    {
        foreach (CellInfo cell in lettersHolder.Cells)
        {
            cell.isEmpty = true;
        }

        System.Random rng = new System.Random();

        List<CellInfo> tempRandomCellList = lettersHolder.Cells.OrderBy(a => rng.Next()).ToList();

        for (int i = 0; i < tempRandomCellList.Count; i++)
        {
            lettersHolder.Letters[i].MoveToNewPosiotion(tempRandomCellList[i].CellTransform.localPosition);
            tempRandomCellList[i].isEmpty = false;
        }
    }
}
