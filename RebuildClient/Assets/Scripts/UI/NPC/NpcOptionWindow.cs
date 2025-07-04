using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class NpcOptionWindow : MonoBehaviour
    {
        public GameObject TemplateButton;
        public GameObject ButtonGroup;

        private readonly List<GameObject> activeButtons = new List<GameObject>();

        public void ShowOptionWindow(List<string> options)
        {
            DeleteActiveButtons();

            var id = 0;
            foreach (string option in options)
            {
                if (string.IsNullOrWhiteSpace(option))
                {
                    id++; //empty options aren't valid, but we still increment the id
                    continue;
                }
                var newButton = GameObject.Instantiate(TemplateButton);
                newButton.transform.SetParent(ButtonGroup.transform, false);
                newButton.SetActive(true);
                activeButtons.Add(newButton);

                var button = newButton.GetComponent<NpcOptionButton>();
                button.Id = id;
                button.TextBox.text = option;
                button.Parent = this;
                id++;
            }
            
            var rect = GetComponent<RectTransform>();
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 10 + 40f * activeButtons.Count);

            TemplateButton.SetActive(false);
            gameObject.SetActive(true);
            transform.SetAsLastSibling();
        }

        private void DeleteActiveButtons()
        {
            for (var i = 0; i < activeButtons.Count; i++)
            {
                Destroy(activeButtons[i]);
            }

            activeButtons.Clear();
        }

        // Start is called before the first frame update
        void Awake()
        {
            if (Time.timeSinceLevelLoad < 1f)
            {
                TemplateButton.SetActive(false);
                gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
