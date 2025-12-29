using UnityEngine;
using UnityEngine.UI;

public class ButtonGroupSelector : MonoBehaviour
{
    public Button[] buttons;
    public Color normalColor = Color.white;
    public Color selectedColor = new Color(0.7f, 0.85f, 1f);

    private Button selectedButton;
    public int SelectedValue = -1;

    void Start()
    {
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => OnButtonClicked(btn));
        }
    }

    void OnButtonClicked(Button btn)
    {
        if (selectedButton != null)
        {
            selectedButton.image.color = normalColor;
        }

        selectedButton = btn;
        selectedButton.image.color = selectedColor;

        SelectedValue = int.Parse(
            btn.GetComponentInChildren<Text>().text.Split(' ')[0]
        );
    }
}
