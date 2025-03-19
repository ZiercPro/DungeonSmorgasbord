using RMC.Mini;
using RMC.Mini.View;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZiercCode._DungeonGame.UI.Settings
{
    public class SettingsView : MonoBehaviour, IView
    {
        //子页面切换器
        [SerializeField] private Toggle volumeViewToggle;
        [SerializeField] private Toggle otherViewToggle;
        [SerializeField] private Toggle languageViewToggle;

        //子页面
        [SerializeField] private CanvasGroupUser volumeViewGroupUser;
        [SerializeField] private CanvasGroupUser otherViewGroupUser;
        [SerializeField] private CanvasGroupUser languageViewGroupUser;

        private Dictionary<Toggle, CanvasGroupUser>
            _toggleViewBind = new Dictionary<Toggle, CanvasGroupUser>(); //toggle和子页面的绑定关系

        [field: SerializeField] public CanvasGroupUser SettingsViewGroupUser { get; private set; }
        [field: SerializeField] public Button BackButton { get; private set; }
        [field: SerializeField] public Button ApplyButton { get; private set; }
        [field: SerializeField] public Slider MasterVolume { get; private set; }
        [field: SerializeField] public Slider MusicVolume { get; private set; }
        [field: SerializeField] public Slider SfxVolume { get; private set; }
        [field: SerializeField] public Slider EnvironmentVolume { get; private set; }
        [field: SerializeField] public Toggle Fps { get; private set; }
        [field: SerializeField] public TMP_Dropdown LanguageDropdown { get; private set; }

        public bool IsInitialized => _isInitialize;
        public IContext Context => _context;

        private bool _isInitialize;
        private IContext _context;

        public void RequireIsInitialized()
        {
        }

        public void Initialize(IContext context)
        {
            if (!_isInitialize)
            {
                _context = context;

                _isInitialize = true;

                //绑定toggle和子页面
                _toggleViewBind.Add(volumeViewToggle, volumeViewGroupUser);
                _toggleViewBind.Add(otherViewToggle, otherViewGroupUser);
                _toggleViewBind.Add(languageViewToggle, languageViewGroupUser);

                //初始化toggle页面切换事件
                foreach (var bind in _toggleViewBind)
                {
                    bind.Key.onValueChanged.AddListener(v =>
                    {
                        if (v)
                            bind.Value.Enable();
                        else bind.Value.Disable();
                    });
                }
            }
        }

        //初始化子页面 只需要激活需要激活的页面 其他页面自动关闭
        public void InitSubView()
        {
            volumeViewToggle.isOn = true;
            volumeViewGroupUser.Enable();
            otherViewGroupUser.Disable();
            languageViewGroupUser.Disable();
        }

        public void Dispose()
        {
        }
    }
}