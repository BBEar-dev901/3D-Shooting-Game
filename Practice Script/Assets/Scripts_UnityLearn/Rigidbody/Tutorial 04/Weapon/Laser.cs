using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lecture.RigidBody.Tutorial04
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] GameObject m_bloodFx;
        Transform reloadTransform;

        public void Shooting()
        {
            Reload();

            RaycastHit hitInfo;
            //                              레이저가 시작할 위치, 레이저 쏠 방향, 맞는 위치 받아오기, 레이저 길이
            bool isHitted = Physics.Raycast(transform.position, transform.forward, out hitInfo, 150f, 1 << LayerMask.NameToLayer("Attacker") );

            if (isHitted)
            {
                GameObject _laserGunFx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_LASER_GUN);
                if(_laserGunFx != null)
                {
                    _laserGunFx.transform.SetPositionAndRotation(hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    _laserGunFx.SetActive(true);
                }

                GameObject _laserSkill = SkillPooler.Instance.GetCannonSkill(CannonSkillType.CANNON_SKILL_LASER_GUN);
                if (_laserSkill)
                {
                    _laserSkill.transform.position = hitInfo.point;
                    _laserSkill.SetActive(true);
                }

                GameObject _bloodfx = EffectPooler.Instance.GetEffect(EffectType.EFFECTTYPE_BLOODEXPLOSION);
                if (_bloodfx != null)
                {
                    _bloodfx.transform.SetPositionAndRotation(hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    _bloodfx.SetActive(true);
                }

                hitInfo.transform.gameObject.GetComponentInParent<EnemyStat>().AddDamage(5f);
            }
        }
        public void Reload()
        {
            if (reloadTransform)
            {
                transform.position = reloadTransform.position;
                transform.eulerAngles = reloadTransform.eulerAngles;
            }
        }
        public void SetMuzzleTransform(Transform _transform)
        {
            reloadTransform = _transform;
        }
    }
}