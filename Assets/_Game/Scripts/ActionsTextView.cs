using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class ActionsTextView : MonoBehaviour
    {
        [SerializeField] private Text _text;
        
        public void SetMessage(string message)
        {
            _text.DOKill();
            _text.text = message;
            _text.color = Color.white;
            _text.DOFade(0, 2f);
        }
    }
}