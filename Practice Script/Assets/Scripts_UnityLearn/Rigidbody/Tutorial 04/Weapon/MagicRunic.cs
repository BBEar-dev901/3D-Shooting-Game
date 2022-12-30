using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class MagicRunic : MonoBehaviour
    {
        public void Shooting()
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if( Physics.Raycast(ray, out hitInfo, 100f, 1 << LayerMask.NameToLayer("Ground")) )
            {
                if (hitInfo.transform)
                {
                    GameObject _magicRunicSkill = SkillPooler.Instance.GetCannonSkill(CannonSkillType.CANNON_SKILL_MAGIC_RUNIC);

                    if(_magicRunicSkill)
                    {
                        _magicRunicSkill.transform.position = hitInfo.point;
                        _magicRunicSkill.SetActive(true);
                    }

                    GameObject _magicRunicFx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_MAGICRUNIC);

                    if (_magicRunicFx != null)
                    {
                        _magicRunicFx.transform.position = hitInfo.point + new Vector3(0f, 0.1f, 0f);
                        _magicRunicFx.SetActive(true);
                    }
                }
            }
        }

    }
}