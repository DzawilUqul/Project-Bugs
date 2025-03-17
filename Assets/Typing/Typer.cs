using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Typer : MonoBehaviour
{
    public WordBank wordBank = null;
    public TMP_Text[] wordOutputs = null;

    private string[] remainingWords;
    private string[] currentWords;
    private int activeOutputIndex = 0;

    private void Start()
    {
        if (wordBank == null || wordOutputs == null || wordOutputs.Length == 0)
        {
            Debug.LogError("WordBank or WordOutputs are not assigned!");
            return;
        }

        remainingWords = new string[wordOutputs.Length];
        currentWords = new string[wordOutputs.Length];
        SetNewWords();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchOutput();
        }
        else
        {
            CheckInput();
        }
    }

    private void SetNewWords()
    {
        for (int i = 0; i < wordOutputs.Length; i++)
        {
            SetNewWord(i);
        }
    }

    private void SetNewWord(int index)
    {
        string newWord = wordBank?.GetWord() ?? "default";
        if (!string.IsNullOrEmpty(newWord))
        {
            currentWords[index] = newWord;
            remainingWords[index] = newWord;
            UpdateWordOutput(index);
        }
        else
        {
            Debug.LogError($"Failed to get new word for index {index}");
        }
    }

    private void SwitchOutput()
    {
        // Reset progress dari output sebelumnya
        ResetProgress(activeOutputIndex);

        // Pindah ke output berikutnya (looping)
        activeOutputIndex = (activeOutputIndex + 1) % wordOutputs.Length;
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;
            if (keysPressed.Length == 1)
            {
                HandleMultipleOutputs(keysPressed);
            }
        }
    }

    private void HandleMultipleOutputs(string typedLetter)
    {
        bool anyMatch = false;
        for (int i = 0; i < remainingWords.Length; i++)
        {
            if (IsCorrectLetter(i, typedLetter))
            {
                anyMatch = true;
                RemoveLetter(i);

                // Kalau kata selesai, reset hanya yang cocok
                if (IsWordComplete(i))
                {
                    SetNewWord(i);
                }
            }
            else if (currentWords[i].StartsWith(currentWords[i].Substring(0, currentWords[i].Length - remainingWords[i].Length) + typedLetter))
            {
                // Kalau salah, abaikan input tanpa reset progress
            }
            else
            {
                // Kalau progres sudah jalan, reset kata lain yang tidak cocok
                ResetProgress(i);
            }
        }

        if (!anyMatch)
        {
            // Kalau nggak ada yang cocok, abaikan input (tanpa reset)
        }
    }

    private bool IsCorrectLetter(int index, string letter)
    {
        return remainingWords[index].StartsWith(letter);
    }

    private void RemoveLetter(int index)
    {
        remainingWords[index] = remainingWords[index].Substring(1);
        UpdateWordOutput(index);
    }

    private bool IsWordComplete(int index)
    {
        return remainingWords[index].Length == 0;
    }

    private void ResetProgress(int index)
    {
        // Reset progres hanya jika kata masih ada progresnya
        if (remainingWords[index] != currentWords[index])
        {
            remainingWords[index] = currentWords[index];
            UpdateWordOutput(index);
        }
    }

    private void UpdateWordOutput(int index)
    {
        if (wordOutputs[index] != null)
        {
            int typedLength = currentWords[index].Length - remainingWords[index].Length;
            wordOutputs[index].text = $"<color=green>{currentWords[index].Substring(0, typedLength)}</color>{remainingWords[index]}";
        }
    }
} 