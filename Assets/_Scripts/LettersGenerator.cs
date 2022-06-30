using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

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
                print("Вы ввели значение равное нулю");
                return;
            }
            if (widthCount > maxCountOnSide || heightCount > maxCountOnSide)
            {
                print("Вы ввели значение большее максимума");
                return;
            }

            xSize = lettersParent.sizeDelta.x / widthCount;
            ySize = lettersParent.sizeDelta.y / heightCount;

            endValueForLatters = xSize > ySize ?  ySize : xSize;
        }
        else
        {
            print("Некоректно введены значения");
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
            }
        }

        for (int count = 0; count < widthCount * heightCount; count++)
        {
            LetterController tempLatter;
            tempLatter = Instantiate(prefabLetter, transform);
            tempLatter.SetLetter(alphabet[Random.Range(0, alphabet.Length)].ToString());
            LatterPositionSetter(tempLatter);
            tempLatter.LetterTransform.sizeDelta = new Vector2(endValueForLatters, endValueForLatters);
            lettersHolder.Letters.Add(tempLatter);
        }
    }

    private void LatterPositionSetter(LetterController latterController)
    {
        List<CellInfo> tempCellList = lettersHolder.Cells.Where(x => x.isEmpty).ToList();
        int tempRandomID = Random.Range(0, tempCellList.Count);
        latterController.LetterTransform.localPosition = tempCellList[tempRandomID].CellTransform.localPosition;
        tempCellList[tempRandomID].isEmpty = false;
    }
}
