using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private static int sortingOrder;
    private static Transform canvasParent;
    private TMP_Text textMesh;
    private Vector3 moveVector;
    private Color textColor;
    private float disappearTimer;
    
    public static DamagePopup Create(Vector3 position)
    {
        Transform damagePopupTF = Instantiate(GameAssets.i.damagePopupPF, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTF.GetComponent<DamagePopup>();
        // damagePopup.Setup(100);
        return damagePopup;
    }
    public void Setup(int damageAmount)
    {
        transform.SetParent(GameAssets.i.uiCanvas);
        transform.localScale = Vector3.one;

        textMesh = GetComponent<TMP_Text>();
        textMesh.SetText("Kontik");
        disappearTimer = DISAPPEAR_TIMER_MAX;
        moveVector = new Vector3(.7f, 1f) * 30f;
        textColor = textMesh.color;
        sortingOrder = 1;
        

        textMesh.SetText(damageAmount.ToString());
        // textMesh.sortingOrder = sortingOrder;
        sortingOrder++;
    }
    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;
        if(disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            transform.localScale += Vector3.one * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * Time.deltaTime;
        }

        if(disappearTimer < 0)
        {
            float disappearSpeed = 3;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
        disappearTimer -= Time.deltaTime;
    }
}
