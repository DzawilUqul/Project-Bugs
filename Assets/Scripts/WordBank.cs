using System;
using System.Collections.Generic;
using System.IO; // Buat ngatur file
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WordBank : MonoBehaviour
{
    private List<string> originalWords = new List<string>(); // Buat nyimpen daftar kata asli dari file
    private List<string> workingWords = new List<string>(); // Buat daftar kata yang bakal dipakai
    [SerializeField] private string filePath = "Assets/wordbank.txt"; // Lokasi file kata-kata

    private void Awake()
    {
        // Jadi, pas game mulai, fungsi ini bakal jalan buat load kata-kata dari file teks ke daftar kata asli. 
        // Kata-kata itu nanti bakal dipindahin ke daftar kerjaan (workingWords), terus diacak biar nggak monoton 
        // dan semuanya diubah jadi huruf kecil supaya gampang dicek. Intinya sih, nyiapin semua kata-kata buat game.
        LoadWordsFromFile(filePath);
        workingWords.AddRange(originalWords);
        Shuffle(workingWords);
        ConvertToLower(workingWords);
    }

    private void LoadWordsFromFile(string path)
    {
        // Fungsi ini kerjaannya baca file kata-kata di lokasi yang udah di-set. Kalau file-nya ada, dia bakal baca
        // tiap baris, buang spasi berlebih, terus dimasukin ke daftar kata asli (originalWords). Tapi, kalau file-nya
        // nggak ada atau ada masalah, bakal keluarin error di console biar gampang dicek.
        try
        {
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string word = line.Trim();
                    if (!string.IsNullOrEmpty(word))
                    {
                        originalWords.Add(word);
                    }
                }
                Debug.Log("Kata-kata berhasil dimuat dari " + path);
            }
            else
            {
                Debug.LogError("File nggak ketemu di path: " + path);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error pas baca file: " + ex.Message);
        }
    }

    private void Shuffle(List<string> list)
    {
        // Ini tugasnya buat ngacak-ngacak urutan elemen di list biar nggak gampang ketebak. Caranya, dia
        // tuker-tuker posisi elemen satu sama elemen lain yang diambil secara acak. Simpel tapi efektif.
        for (int i = 0; i < list.Count; i++)
        {
            int random = Random.Range(i, list.Count);
            string temporary = list[i];
            list[i] = list[random];
            list[random] = temporary;
        }
    }

    private void ConvertToLower(List<string> list)
    {
        // Di sini semua elemen dalam list diubah jadi huruf kecil (lowercase) aja biar seragam. Soalnya, 
        // kadang ada masalah kalau huruf gede sama kecil dibandingin. Jadi, ini buat nyelesain itu.
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = list[i].ToLower();
        }
    }

    public string GetWord()
    {
        // Kalau butuh kata baru buat game, fungsi ini bakal kasih kata terakhir dari daftar kerjaan (workingWords), 
        // terus hapus kata itu dari daftar biar nggak kepake lagi. Tapi kalau daftar kosong, ya dia balikin string kosong.
        string newWord = string.Empty;
        if (workingWords.Count != 0)
        {
            newWord = workingWords.Last();
            workingWords.Remove(newWord);
        }
        return newWord;
    }
}

