using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lecture.RigidBody.Tutorial04
{
    public class CannonControl : MonoBehaviour
    {
        private static CannonControl instance = null;

        public static CannonControl Instance
        {
            get { return instance; }
        }

        [Header("Bullet Gun")]
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] GameObject bulletGun;
        [SerializeField] Transform muzzleBulletTransform;
        int bulletsCount = 20;
        Queue<Bullet> bulletsQueue = new Queue<Bullet>();

        Laser m_laser;
        [Header("Laser Gun")]
        [SerializeField] GameObject laserPrefab;
        [SerializeField] GameObject lasergun;
        [SerializeField] Transform muzzleLaserTransform;

        [Header("KeyCode")]
        [SerializeField] KeyCode switchKey = KeyCode.LeftAlt;
        [SerializeField] KeyCode shotKey = KeyCode.Space;

        MagicRunic m_magic;
        [Header("Magic Skill")]
        [SerializeField] GameObject magicPrefab;

        delegate void WeaponFire();
        WeaponFire Fire;

        [SerializeField] Image weaponImage;
        [SerializeField] Text weaponText;

        [Serializable][SerializeField] struct WeaponHudInfo
        {
            public Sprite sprite;
            public string text;
            public Color color;
        }
        [SerializeField] WeaponHudInfo[] weaponHudInfos = new WeaponHudInfo[(int)WeaponType.WEAPONTYPE_MAX_SIZE];

        enum WeaponType
        {
            WEAPONTYPE_DEFAULT,
            WEAPONTYPE_LASER = WEAPONTYPE_DEFAULT,
            WEAPONTYPE_BULLET,
            WEAPONTYPE_MAX_SIZE,
        }
        WeaponType m_currentWeaponType = WeaponType.WEAPONTYPE_DEFAULT;

        void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
            // 총구의 위치 받아오기
            muzzleLaserTransform = transform.GetChild(0).GetChild(0).GetChild(0);
            m_laser = Instantiate(laserPrefab).GetComponent<Laser>();
            m_magic = Instantiate(magicPrefab).GetComponent<MagicRunic>();
            Fire = Fire_Laser;
            bulletGun.SetActive(false);

            Bullet oneBullet;

            for(int i = 0; i < bulletsCount; i++)
            {
                oneBullet = CreateNewBullet();
                oneBullet.gameObject.SetActive(false);
                bulletsQueue.Enqueue(oneBullet);
            }

            WeaponHudSwitch();
        }
        void Update()
        {
            if (Input.GetKeyDown(shotKey))
            {
                Fire();
            }
            if (Input.GetKeyDown(switchKey))
            {
                WeaponSwitch();
            }
            if (Input.GetMouseButtonDown(0))
            {
                m_magic.Shooting();
            }
        }
        void Fire_Bullet()
        {
            Bullet bullet;
            if (bulletsQueue.Count > 0)
            {
                bullet = bulletsQueue.Dequeue();
                bullet.gameObject.SetActive(true);
            }
            else
            {
                bullet = CreateNewBullet();
            }
            bullet.SetMuzzleTransform(muzzleBulletTransform);
            bullet.Shooting(true);
        }
        void Fire_Laser()
        {
            m_laser.SetMuzzleTransform(muzzleLaserTransform);
            m_laser.Shooting();
        }
        public Bullet CreateNewBullet()
        {
            Bullet newBullet;
            newBullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
            newBullet.transform.SetParent(transform.GetChild(0).GetChild(1).GetChild(1));
            return newBullet;
        }
        public void BulletIsExpired(Bullet _bullet)
        {
            _bullet.gameObject.SetActive(false);
            bulletsQueue.Enqueue(_bullet);
        }
        void WeaponSwitch()
        {
            m_currentWeaponType++;

            if(m_currentWeaponType == WeaponType.WEAPONTYPE_MAX_SIZE)
            {
                m_currentWeaponType = WeaponType.WEAPONTYPE_DEFAULT;
            }
            switch (m_currentWeaponType)
            {
                case WeaponType.WEAPONTYPE_LASER:
                    Fire = Fire_Laser;
                    lasergun.SetActive(true);
                    bulletGun.SetActive(false);
                    break;
                case WeaponType.WEAPONTYPE_BULLET:
                    Fire = Fire_Bullet;
                    lasergun.SetActive(false);
                    bulletGun.SetActive(true);
                    break;
                default:
                    break;
            }
            WeaponHudSwitch();
        }

        void WeaponHudSwitch()
        {
            weaponImage.sprite = weaponHudInfos[(int)m_currentWeaponType].sprite;
            weaponImage.color = weaponHudInfos[(int)m_currentWeaponType].color;
            weaponText.text = weaponHudInfos[(int)m_currentWeaponType].text;
            weaponText.color = weaponHudInfos[(int)m_currentWeaponType].color;
        }
    }
}