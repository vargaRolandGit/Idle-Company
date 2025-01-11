using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    [SerializeField] TextMeshProUGUI headerField;
    [SerializeField] TextMeshProUGUI contentField;
    [SerializeField] int characterWrapLimit;
    [SerializeField] LayoutElement tooltipLayoutElement => GetComponent<LayoutElement>();
    [SerializeField] RectTransform rectTransform => GetComponent<RectTransform>();
    
    public void SetText(string content, string header = "") {
        headerField.text = header;
        headerField.gameObject.SetActive(!string.IsNullOrEmpty(header));
        contentField.text = content;
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        tooltipLayoutElement.enabled = (
            headerLength > characterWrapLimit
            || contentLength > characterWrapLimit
        );
    }

    private void Update() {
        Vector2 mousePos = Input.mousePosition;
        transform.position = mousePos;
        float pivotY = mousePos.y / Screen.height;
        float pivotX = 0; //mousePos.x / Screen.width;

        float length = contentField.text.Length < characterWrapLimit
            ? (contentField.text.Length / 10) 
            : characterWrapLimit;
        if (mousePos.x < Screen.width / 2) {
            pivotX -= 0.1f * length;
        } else {        
            pivotX += 0.5f * length;
        }

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = mousePos;
    }
}
