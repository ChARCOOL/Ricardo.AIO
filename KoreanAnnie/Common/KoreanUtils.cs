using EnsoulSharp.Common;
using System;

namespace KoreanAnnie.Common
{
    internal static class KoreanUtils
    {
        #region Static Fields

        public static readonly Func<CommonMenu, string, string> ParamName = (cm, s) =>
            string.Format("{0}.{1}", cm.MainMenu.Name, s);

        public static readonly Func<CommonMenu, string, MenuItem> GetParam = (cm, s) =>
            cm.MainMenu.Item(ParamName(cm, s));

        public static readonly Func<CommonMenu, string, bool> GetParamBool = (cm, s) =>
            cm.MainMenu.Item(ParamName(cm, s)).GetValue<bool>();

        public static readonly Func<CommonMenu, string, int> GetParamInt = (cm, s) =>
            cm.MainMenu.Item(ParamName(cm, s)).GetValue<int>();

        public static readonly Func<CommonMenu, string, bool> GetParamKeyBind = (cm, s) =>
            cm.MainMenu.Item(ParamName(cm, s)).GetValue<KeyBind>().Active;

        public static readonly Func<CommonMenu, string, int> GetParamSlider = (cm, s) =>
            cm.MainMenu.Item(ParamName(cm, s)).GetValue<Slider>().Value;

        public static readonly Func<CommonMenu, string, int> GetParamStringList = (cm, s) =>
            cm.MainMenu.Item(ParamName(cm, s)).GetValue<StringList>().SelectedIndex;

        public static readonly Action<CommonMenu, string, bool> SetValueBool = (cm, s, b) =>
            cm.MainMenu.Item(ParamName(cm, s)).SetValue(b);

        public static readonly Action<CommonMenu, string, int> SetValueInt = (cm, s, i) =>
            cm.MainMenu.Item(ParamName(cm, s)).SetValue(i);

        public static readonly Action<CommonMenu, string, Slider> SetValueSlider = (cm, s, slider) =>
            cm.MainMenu.Item(ParamName(cm, s)).SetValue(slider);

        #endregion Static Fields
    }
}