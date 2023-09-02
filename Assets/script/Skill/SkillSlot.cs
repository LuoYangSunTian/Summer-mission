using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameobject.Skill
{
    public class SkillSlot : MonoBehaviour
    {

        public SkillDetails skillDetails;
        [SerializeField] public Image skillSprite;

        public void UpdataUI(SkillDetails details)
        {
            skillDetails = details;
            skillSprite.sprite = skillDetails.SkillIcon;
            switch (skillDetails.skillType)
            {
                case SkillType.Holy:
                    skillSprite.color = new Color(255, 255, 0);
                    break;

                case SkillType.Thunder:
                    skillSprite.color = new Color(136, 52, 255);
                    break;
            }
        }


    }
}
