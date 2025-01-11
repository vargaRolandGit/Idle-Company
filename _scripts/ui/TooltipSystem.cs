using UnityEngine;

public class TooltipSystem : Singleton<TooltipSystem> {

    public Tooltip tooltip;
    public static void Show(string content, string header = "") { 
        Instance.tooltip.gameObject.SetActive(true);
        Instance.tooltip.SetText(content, header);
    }

    public static void Hide() { 
        Instance.tooltip.gameObject.SetActive(false);
    }
}
