using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameobject.Skill
{
    public class SkillUI : MonoBehaviour
    {

        [SerializeField] private SkillSlot[] skillSlots;
        private void OnEnable()
        {
            EventHandler.UpdateSkillUI += OnUpdateSkillUI;
        }

        private void OnDisable()
        {
            EventHandler.UpdateSkillUI -= OnUpdateSkillUI;
        }
        private void Update()
        {
            CheckSkillInput();
        }
        private void OnUpdateSkillUI(SkillDetails skillDetails)
        {
            for (int x = 0; x <= skillSlots.Length - 1; x++)
            {

                if (!skillSlots[x].skillSprite.gameObject.activeSelf)
                {
                    skillSlots[x].skillSprite.gameObject.SetActive(true);
                    skillSlots[x].UpdataUI(skillDetails);
                    break;
                }
            }
        }

        public void CheckSkillInput()
        {
            if (Input.GetKeyDown(KeyCode.F) && skillSlots[0].skillSprite.gameObject.activeSelf)
            {
                EventHandler.CallCreateMagic(skillSlots[0].skillDetails.skillIndex);

            }
            if (Input.GetKeyDown(KeyCode.G) && skillSlots[1].skillSprite.gameObject.activeSelf)
            {
                EventHandler.CallCreateMagic(skillSlots[1].skillDetails.skillIndex);
            }
            if (Input.GetKeyDown(KeyCode.C) && skillSlots[2].skillSprite.gameObject.activeSelf)
            {
                EventHandler.CallCreateMagic(skillSlots[2].skillDetails.skillIndex);
            }
            if (Input.GetKeyDown(KeyCode.V) && skillSlots[3].skillSprite.gameObject.activeSelf)
            {
                EventHandler.CallCreateMagic(skillSlots[3].skillDetails.skillIndex);
            }
        }
    }
}

