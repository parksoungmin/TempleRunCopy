using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Editor���� ������Ʈ�� ���� �� �ִ� ��Ʈ����Ʈ
[ExecuteInEditMode]
[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizationText : MonoBehaviour
{
    public string stringId;

//#if UNITY_EDITOR
//    public Languages editorLanguage;
//#endif

    public Languages editorLanguage;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if(Application.isPlaying)
            OnChangeLanguage(Varibalbes.currentLanguage);
        else
        {
            OnChangeLanguage(editorLanguage);
        }
    }

    public void OnChangeLanguage(Languages languages)
    {
        var stringTableId = DataTableIds.String[(int)languages];
        var stringTable = DataTableManager.Get<StringTable>(stringTableId);

        text.text = stringTable.Get(stringId);
    }
}
