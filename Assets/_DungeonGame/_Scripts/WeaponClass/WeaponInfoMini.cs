using RMC.Mini;
using UnityEngine;
using ZiercCode._DungeonGame.UI.WeaponInfo;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    public class WeaponInfoMini : MonoBehaviour
    {
        //武器信息显示
        [SerializeField]
        private GameObject weaponInfoPrefab;

        private GameObject _weaponInfoObject;

        private WeaponInfoView _weaponInfoView;
        private WeaponInfoController _weaponInfoController;
        private WeaponInfoService _weaponInfoService;
        private WeaponInfoModel _weaponInfoModel;

        private IContext _context;

        private BaseWeapon _myWeapon;

        private void Awake()
        {
            _weaponInfoObject = Instantiate(weaponInfoPrefab, transform);
            _weaponInfoObject.transform.localPosition = Vector3.zero;
            _weaponInfoView = _weaponInfoObject.GetComponent<WeaponInfoView>();

            _context = new Context();
            _myWeapon = GetComponent<BaseWeapon>();
            _weaponInfoModel = new WeaponInfoModel();
            _weaponInfoService = new WeaponInfoService(_myWeapon);
            _weaponInfoController = new WeaponInfoController(_weaponInfoModel, _weaponInfoView, _weaponInfoService);
        }

        private void Start()
        {
            _weaponInfoModel.Initialize(_context);
            _weaponInfoView.Initialize(_context);
            _weaponInfoService.Initialize(_context);
            _weaponInfoController.Initialize(_context);
        }
    }
}