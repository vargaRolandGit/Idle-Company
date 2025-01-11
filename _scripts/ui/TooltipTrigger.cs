using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    static LTDescr delay;
    public string header;
    [TextArea(10, 40)] public string content;
    public void OnPointerEnter(PointerEventData eventData) {
        delay = LeanTween.delayedCall(0.5f, () => {
            LeanTween.scale(TooltipSystem.Instance.tooltip.gameObject,
                new Vector3(0, 0, 0), 0.0f);
            TooltipSystem.Show(content, header);
            LeanTween.scale(TooltipSystem.Instance.tooltip.gameObject, 
                new Vector3(1, 1, 1),
                0.2f);
        });
    }

    public void OnPointerExit(PointerEventData eventData) {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }
}
