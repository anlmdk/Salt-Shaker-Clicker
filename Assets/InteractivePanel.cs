using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractivePanel : MonoBehaviour
{
    public Button[] interactiveButton;

    private Coroutine autoClickCoroutine;

    private float autoClickTime;
    
    void Update()
    {
        CheckLevel();
    }

    public void CheckLevel ()
    {
        if (XpBar.Instance.level == 5)
        {
            interactiveButton[0].interactable = true;
        }
        else if (XpBar.Instance.level == 50)
        {
            interactiveButton[1].interactable = true;
        }
        else if (XpBar.Instance.level == 100)
        {
            interactiveButton[2].interactable = true;
        }
        else if (XpBar.Instance.level == 250)
        {
            interactiveButton[3].interactable = true;
        }
        else if (XpBar.Instance.level == 500)
        {
            interactiveButton[4].interactable = true;
        }
        else if (XpBar.Instance.level == 1000)
        {
            interactiveButton[5].interactable = true;
        }
        else if (XpBar.Instance.level == 5000)
        {
            interactiveButton[6].interactable = true;
        }
    }

    public void FriesButton()
    {
        if (autoClickCoroutine == null) // Eðer zaten çalýþmýyorsa
        {
            autoClickCoroutine = StartCoroutine(AutoClick());
        }
        autoClickTime = 1f;
    }

    public void SoupButton()
    {
        Clicker.Instance.SetScoreIncrement(10);
        XpBar.Instance.IncreaseXp(5);
    }

    public void CannedButton()
    {
        Clicker.Instance.IncreaseScore(100);
        XpBar.Instance.IncreaseXp(50);
    }

    public void SausageButton()
    {
        autoClickTime = 0.5f;
        Clicker.Instance.SetScoreIncrement(100);
        XpBar.Instance.IncreaseXp(50);
    }

    public void CheeseButton()
    {
        Clicker.Instance.IncreaseScore(1000);
        XpBar.Instance.IncreaseXp(100);
    }

    public void OliveButton()
    {
        autoClickTime = 0.2f;
        Clicker.Instance.SetScoreIncrement(10000);
        XpBar.Instance.IncreaseXp(250);
    }

    public void PicklesButton()
    {
        Clicker.Instance.IncreaseScore(100000);
        XpBar.Instance.IncreaseXp(1000);
    }

    private IEnumerator AutoClick()
    {
        while (true) // Sonsuz döngü, sürekli çalýþacak
        {
            Clicker.Instance.Score(); // Skoru arttýr
            XpBar.Instance.CurrentXpBar();
            yield return new WaitForSeconds(autoClickTime); // 2 saniye bekle
        }
    }

    public void StopAutoClick()
    {
        if (autoClickCoroutine != null)
        {
            StopCoroutine(autoClickCoroutine);
            autoClickCoroutine = null;
        }
    }
}
