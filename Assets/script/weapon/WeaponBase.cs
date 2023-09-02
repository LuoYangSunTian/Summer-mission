using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameobject.Weapon
{
    public class WeaponBase : MonoBehaviour
    {

        [Header("组件")]
        private SpriteRenderer spriteRenderer;
        private BoxCollider2D coll;
        [SerializeField] public TextMesh describe;

        [Header("参数")]
        public int weaponID;
        public WeaponDetails weaponDetails;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            coll = GetComponent<BoxCollider2D>();
        }
        // Start is called before the first frame update
        void Start()
        {
            if (weaponID != 0)
            {
                Init(weaponID);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Init(int ID)
        {
            weaponID = ID;
            weaponDetails = WeaponManager.Instance.GetWeaponDetails(ID);
            if (weaponDetails != null)
            {
                spriteRenderer.sprite = weaponDetails.weaponImage;
                //修改碰撞体的大小
                Vector2 newSize = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);//获取图片的尺寸
                coll.size = newSize;
                //coll.offset = new Vector2(0, spriteRenderer.sprite.bounds.center.y);//设置碰撞体的偏移量
            }
        }

        public void DisplayWeaponDescribe()
        {
            describe.text = weaponDetails.weaponDescribe;
        }

        public void CancelWeaponDescribe()
        {
            describe.text = string.Empty;
        }
    }
}
