using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameobject.Skill
{
    public class SkillManager : Singleton<SkillManager>
    {
        public SkillData_So skillList;
        public List<SkillDetails> havenSkill = new List<SkillDetails>();
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            //EventHandler.AwakeningMagic += Awakening;
        }


        private void OnDisable()
        {
            //EventHandler.AwakeningMagic -= Awakening;
        }


        public SkillDetails SearchSkillDetail(int index)
        {
            return skillList.skillDetailsList.Find(i => i.skillIndex == index);
        }

        public void AddSkill(SkillDetails skillDetails)
        {
            if (!havenSkill.Contains(skillDetails))
            {
                havenSkill.Add(skillDetails);
                EventHandler.CallUpdateSkillUI(skillDetails);
            }
        }

    }
}

