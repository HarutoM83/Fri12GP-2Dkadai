using UnityEngine;
using UnityEngine.UI;
using TMPro;
using R3;
using R3.Triggers;


public class LifeGauge : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image gaugeImage;
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] TextMeshProUGUI maxLifeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ライフの最大値
        maxLifeText.text = player.MaxLife.ToString();
        player.life.Subscribe(
            life => lifeText.text = life.ToString());
        player.life.Select(life => life / player.MaxLife)
            .Subscribe(x => gaugeImage.fillAmount = x);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
