using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Typer : MonoBehaviour
{
    public WordBank wordBank = null; // Ini nge-link sama script WordBank buat ambil kata-kata.
    public TMP_Text wordOutput = null; // Output teks yang bakal ditampilkan di layar.

    private string remainingWord = string.Empty; // Nyimpen kata yang masih harus diketik.
    private string currentWord = string.Empty; // Kata yang lagi aktif untuk diketik.

    // Pas game mulai, langsung set kata pertama buat diketik.
    private void Start()
    {
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        // Ambil kata baru dari WordBank, terus atur supaya siap ditampilkan.
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        // Update kata yang harus diketik di layar.
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }

    // Di-update tiap frame buat ngecek input dari pemain.
    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        // Cek kalau ada tombol yang ditekan. Kalau iya, ambil karakter yang diketik
        // dan pastikan cuma satu huruf aja yang diproses.
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;
            if (keysPressed.Length == 1)
                EnterLetter(keysPressed);
        }
    }

    private void EnterLetter(string typedLetter)
    {
        // Kalau huruf yang diketik bener, hapus huruf itu dari kata yang harus diketik.
        // Kalau kata udah habis, langsung ambil kata baru.
        if (IsCorrectLetter(typedLetter))
        {
            RemoveLetter();
            if (IsWordComplete())
                SetCurrentWord();
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        // Ngecek apakah huruf yang diketik sesuai sama huruf pertama di kata yang tersisa.
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        // Kalau huruf bener, huruf pertama dihapus dan kata yang tersisa di-update.
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        // Ngecek apakah semua huruf di kata udah selesai diketik.
        return remainingWord.Length == 0;
    }
}
