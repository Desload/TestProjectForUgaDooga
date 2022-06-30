using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettersHolder : MonoBehaviour
{
    private List<LetterController> letters = new List<LetterController>();
    private List<CellInfo> cells = new List<CellInfo>();

    public List<LetterController> Letters => letters;
    public List<CellInfo> Cells => cells;

    public void ClearAll()
    {
        letters.Clear();
        cells.Clear();

        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
