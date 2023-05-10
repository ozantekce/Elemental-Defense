using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{


    private Text textPrefab;
    private static DamageTextManager _instance;

    public static DamageTextManager Instance { get => _instance; set => _instance = value; }


    private void Awake()
    {
        _instance = this;
        GameObject temp = new GameObject("Text");
        textPrefab = temp.AddComponent<Text>();
        temp.transform.eulerAngles = new Vector3 (85f, -90f, 0f);
        temp.transform.localScale = Vector3.one * 4.6f;
        temp.SetActive(false);
    }


    public void CreateText(string text,Vector3 pos)
    {

        Text textObject = Poolable.GetFromPool(textPrefab);
        textObject.SetText(text,1f);
        textObject.transform.position = pos;

    }




    private class Text : MonoBehaviour,Poolable
    {

        private bool _pooled;
        public string Key { get => "Text"; set => throw new System.NotImplementedException(); }

        public MonoBehaviour MonoBehaviour => this;

        public bool Pooled { get => _pooled; set => _pooled = value; }


        private TextMeshPro _textMeshPro;

        public void SetText(string text,float lifeTime)
        {
            if(_textMeshPro == null)
            {
                _textMeshPro = gameObject.AddComponent<TextMeshPro>();
                _textMeshPro.alignment = TextAlignmentOptions.Center;
                _textMeshPro.fontStyle = FontStyles.Bold;
            }
            _textMeshPro.text = text;
            Invoke("SendToPool", lifeTime);
        }

        private void Update()
        {
            transform.Translate(Vector3.up * Time.deltaTime * 16f);
        }

        private void SendToPool()
        {
            Poolable.AddToPool(this);
        }


    }


}