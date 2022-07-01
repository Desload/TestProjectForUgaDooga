using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Diagnostics;

public class LettersGenerator : MonoBehaviour
{
    [SerializeField] private int maxCountOnSide;

    [SerializeField] private LetterController prefabLetter;
    [SerializeField] private CellInfo cellPrefab;

    private RectTransform lettersParent;
    private GridLayout gridLayout;
    private InputReader inputReader;
    private LettersHolder lettersHolder;

    private char[] alphabet = Enumerable.Range('A', 'Z' - 'A' + 1).Select(c => (char)c).ToArray();

    private void Awake()
    {
        lettersParent = GetComponent<RectTransform>();
        inputReader = GetComponent<InputReader>();
        gridLayout = GetComponent<GridLayout>();
        lettersHolder = GetComponent<LettersHolder>();
    }

    public void GenerateCombination()
    {
        lettersHolder.ClearAll();

        float xSize;
        float ySize;

        float endValueForLatters;

        if (inputReader.GetValueFromFields(out int widthCount, out int heightCount))
        {
            if (widthCount == 0 || heightCount == 0)
            {
                ErrorDisplay.ShowError?.Invoke("бш ббекх гмювемхе пюбмне мскч");
                return;
            }

            xSize = lettersParent.sizeDelta.x / widthCount;
            ySize = lettersParent.sizeDelta.y / heightCount;

            endValueForLatters = xSize > ySize ?  ySize : xSize;
        }
        else
        {
            ErrorDisplay.ShowError?.Invoke("мейнпейрмн ббедемш гмювемхъ");
            return;
        }

        Vector2 center = new Vector2(lettersParent.sizeDelta.x / 2, lettersParent.sizeDelta.y / 2);
        Vector2 topLeftPoint = new Vector2(((widthCount - 1) * -endValueForLatters) / 2, ((heightCount - 1) * endValueForLatters) / 2);

        for (int w = 0; w < widthCount; w++)
        {
            for(int h = 0; h < heightCount; h++)
            {
                CellInfo tempCell = Instantiate(cellPrefab, transform);
                tempCell.CellTransform.localPosition = topLeftPoint + new Vector2(endValueForLatters * w, -endValueForLatters * h);
                lettersHolder.Cells.Add(tempCell);

                LetterController tempLatter;
                tempLatter = Instantiate(prefabLetter, transform);
                lettersHolder.Letters.Add(tempLatter);
            }
        }

        System.Random rng = new System.Random();
        List<CellInfo> tempRandomCellList = lettersHolder.Cells.OrderBy(a => rng.Next()).ToList();

        for (int counter = 0; counter < widthCount * heightCount; counter++)
        {
            lettersHolder.Letters[counter].SetLetter(alphabet[Random.Range(0, alphabet.Length)].ToString());

            lettersHolder.Letters[counter].LetterTransform.localPosition = tempRandomCellList[counter].CellTransform.localPosition;
            tempRandomCellList[counter].isEmpty = false;

            lettersHolder.Letters[counter].LetterTransform.sizeDelta = new Vector2(endValueForLatters, endValueForLatters);
        }
    }
}
