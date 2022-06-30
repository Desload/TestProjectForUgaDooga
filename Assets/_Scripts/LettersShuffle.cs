using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        foreach(LetterController letter in lettersHolder.Letters)
        {
            List < CellInfo > tempCellList = lettersHolder.Cells.Where(x => x.isEmpty).ToList();
            int tempRandomID = Random.Range(0, tempCellList.Count);
            letter.MoveToNewPosiotion(tempCellList[tempRandomID].CellTransform.localPosition);
            tempCellList[tempRandomID].isEmpty = false;
        }
    }
}
