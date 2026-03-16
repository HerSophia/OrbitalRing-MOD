using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace ProjectOrbitalRing.Utils
{
    internal static partial class IconDescUtils
    {
        internal static readonly Dictionary<int, ModIconDesc> IconDescs = new Dictionary<int, ModIconDesc>
        {
            { 7019, new FluidIconDesc(new Color32(129, 199, 241, 255)) }, // O2
            { 6220, new FluidIconDesc(new Color32(137, 242, 178, 255)) }, // N2
            { 6234, new FluidIconDesc(new Color32(244, 255, 183, 255)) }, // He4
            { 6235, new FluidIconDesc(new Color32(210, 222, 142, 255)) }, // He3
            { 6205, new FluidIconDesc(new Color32(90, 248, 244, 255)) },  // 甲烷
            { 6208, new FluidIconDesc(new Color32(0, 243, 255, 255)) },    // 激素生长菌群
            { 6212, new FluidIconDesc(new Color32(255, 0, 0, 255)) },  // 共生菌核
            { 6202, new FluidIconDesc(new Color32(255, 86, 0, 255)) }, // 深层熔岩

            { 1114, new NoIconFluidIconDesc(new Color32(138, 83, 43, 255)) },   // Coal Oil
            { 6251, new NoIconFluidIconDesc(new Color32(255, 165, 0, 255)) }, // 岩浆
            { 7018, new NoIconFluidIconDesc(new Color32(97, 132, 186, 255)) },  // Ocean
            { 7009, new NoIconFluidIconDesc(new Color32(188, 182, 5, 255)) },   // JP10
            //{ 7014, new NoIconFluidIconDesc(new Color32(104, 187, 154, 255)) }, // HCl
            { 6225, new NoIconFluidIconDesc(new Color32(42, 97, 32, 255)) },    // U238
            { 7014, new NoIconFluidIconDesc(new Color32(195, 198, 234, 255)) }, // 导电银浆
            //{ 7017, new NoIconFluidIconDesc(new Color32(157, 56, 157, 255)) },  // HNO3
            { 6226, new NoIconFluidIconDesc(new Color32(167, 255, 39, 255)) },  // U235

            { 6207, new OreIconDesc(new Color32(230, 239, 137, 255)) }, // S
            { 6222, new OreIconDesc(new Color32(106, 175, 78, 255)) },  // Rads
            //{ 6225, new OreIconDesc(new Color32(130, 235, 139, 255)) }, // U
            { 6201, new OreIconDesc(new Color32(30, 29, 30, 255)) },    // Tungsten
            { 7706, new OreIconDesc(new Color32(230, 230, 230, 255)) }, // 辉银矿

            { 7803, new ComponentIconDesc(new Color32(228, 153, 255, 255)) }, // 光学芯片
            { 7804, new ComponentIconDesc(new Color32(228, 153, 255, 255)) }, // 光学纤维
            { 7805, new ComponentIconDesc(new Color32(93, 191, 255, 255)) },  // 量子主机
            { 7806, new ComponentIconDesc(new Color32(228, 153, 255, 255)) }, // 光学主机
            { 6263, new ComponentIconDesc(new Color32(150, 173, 240, 255)) }, // 氦闪约束器
            { 6267, new ComponentIconDesc(new Color32(147, 244, 241, 255)) }, // 大气采集站
            { 6221, new ComponentIconDesc(new Color32(122, 227, 130, 255)) }, // 同位素发电机
            { 6261, new ComponentIconDesc(new Color32(41, 221, 255, 255)) },  // 人造恒星MK2
            { 6229, new ComponentIconDesc(new Color32(164, 218, 255, 255)) }, // 量子缸
            { 6231, new ComponentIconDesc(new Color32(213, 82, 255, 255)) },  // 量子箱
            { 6230, new ComponentIconDesc(new Color32(196, 255, 106, 255)) }, // 粉碎机
            { 7617, new ComponentIconDesc(new Color32(77, 182, 241, 255)) },  // 机枪塔MK2
            { 7618, new ComponentIconDesc(new Color32(140, 64, 219, 255)) },  // 激光塔MK2
            { 6501, new ComponentIconDesc(new Color32(234, 163, 87, 255)) },  // 基础组件
            { 6502, new ComponentIconDesc(new Color32(87, 255, 191, 255)) },  // 先进组件
            { 6503, new ComponentIconDesc(new Color32(53, 206, 255, 255)) },  // 尖端组件
            { 7501, new ComponentIconDesc(new Color32(210, 157, 118, 255)) }, // 塑料基板
            { 7504, new ComponentIconDesc(new Color32(109, 196, 255, 255)) }, // 光学基板
            { 6257, new ComponentIconDesc(new Color32(241, 158, 60, 255)) },  // 大组装
            { 6258, new ComponentIconDesc(new Color32(71, 132, 253, 255)) },  // 大熔炉
            { 6259, new ComponentIconDesc(new Color32(249, 255, 89, 255)) },  // 大化反
            { 6260, new ComponentIconDesc(new Color32(201, 50, 65, 255)) },   // 大精密
            { 6264, new ComponentIconDesc(new Color32(71, 188, 84, 255)) },   // 大回收
            { 6265, new ComponentIconDesc(new Color32(106, 61, 172, 255)) },  // 大对撞

            { 7612, new FullIconDesc(new Color32(173, 207, 172, 255)) }, // 核子炮弹
            { 7613, new FullIconDesc(new Color32(187, 172, 252, 255)) }, // 反物质炮弹
            { 7615, new FullIconDesc(new Color32(187, 172, 252, 255)) }, // 反物质导弹
            { 6204, new FullIconDesc(new Color32(161, 157, 152, 255)) }, // 铝合金
            { 7707, new FullIconDesc(new Color32(51, 51, 57, 255)) },    // 钨钢
            { 6271, new FullIconDesc(new Color32(221, 218, 255, 255)) }, // 三元合金
            { 7608, new FullIconDesc(new Color32(81, 83, 90, 255)) },    // 钨子弹
            { 7609, new FullIconDesc(new Color32(221, 218, 255, 255)) }, // 三元子弹
            { 7616, new FullIconDesc(new Color32(187, 172, 252, 255)) }, // 反物质子弹
            { 6252, new FullIconDesc(new Color32(160, 216, 255, 255)) }, // 铁粉
            { 6253, new FullIconDesc(new Color32(237, 176, 150, 255)) }, // 铜粉
            { 6273, new FullIconDesc(new Color32(218, 212, 195, 255)) }, // 铝粉
            { 6254, new FullIconDesc(new Color32(153, 236, 182, 255)) }, // 硅粉
            { 6255, new FullIconDesc(new Color32(232, 236, 255, 255)) }, // 钛粉
            { 6281, new FullIconDesc(new Color32(32, 33, 36, 255)) },    // 钨粉
            { 6256, new FullIconDesc(new Color32(56, 57, 62, 255)) },    // 煤粉
            { 6277, new FullIconDesc(new Color32(255, 255, 142, 255)) }, // 硫粉
            { 7002, new FullIconDesc(new Color32(96, 255, 114, 255)) }, // 增生组织
            { 7017, new FullIconDesc(new Color32(251, 251, 251, 255)) },  // 工业糖
            { 6227, new FullIconDesc(new Color32(255, 255, 255, 255)) },   // 铀块

            { 6203, new NoIconMetalIconDesc(new Color32(186, 176, 144, 255)) }, // 铝块
            { 7705, new NoIconMetalIconDesc(new Color32(43, 44, 48, 255)) },    // 钨块
            //{ 6227, new NoIconMetalIconDesc(new Color32(66, 222, 87, 255)) },   // 铀块
            { 6228, new NoIconMetalIconDesc(new Color32(239, 83, 90, 255)) },   // 钚块
            { 7006, new NoIconMetalIconDesc(new Color32(255, 177, 0, 255)) },   // 润滑油
            { 7015, new NoIconMetalIconDesc(new Color32(0, 255, 157, 255)) },    // 工业醇

            { 6217, new RodIconDesc(new Color32(163, 145, 85, 255)) },  // 煤油棒
            { 6216, new RodIconDesc(new Color32(198, 207, 111, 255)) }, // JP10棒
            { 6242, new RodIconDesc(new Color32(33, 170, 87, 255)) },   // 铀棒
            { 6241, new RodIconDesc(new Color32(204, 74, 78, 255)) },   // 钚棒
            { 6243, new RodIconDesc(new Color32(153, 157, 169, 255)) }, // MOX棒
            { 6244, new RodIconDesc(new Color32(245, 250, 105, 255)) }, // He3棒
            { 6245, new RodIconDesc(new Color32(147, 77, 255, 255)) },  // 混合棒
            { 6206, new RodIconDesc(new Color32(96, 255, 255, 255)) }, // 激素营养液

            { 6278, new MartixIconDesc(new Color(1f, 0.4117f, 0.3137f, 0.1961f), new Color(1f, 0.2706f, 0f, 0f)) },
            { 6279, new MartixIconDesc(new Color(1f, 0.7530f, 0.7961f, 0.1961f), new Color(0.7804f, 0.0824f, 0.5216f, 0f)) },
            { 6280, new MartixIconDesc(new Color(0.4020f, 0.4020f, 0.4020f, 0.1961f), new Color(0.3f, 0.3f, 0.3f, 0f)) },
            { 7610, new WhiteIconDesc() }, // 核子单元
            { 7611, new WhiteIconDesc() }, // 反物质单元

            //{ 7706, new GlassIconDesc(new Color32(91, 91, 91, 255)) }, // 钨玻璃
        };

        private static readonly IconToolNew.IconDesc Default = new IconToolNew.IconDesc
        {
            faceColor = Color.white,
            sideColor = new Color(0.4667F, 0.5333F, 0.6F, 1f),
            faceEmission = Color.black,
            sideEmission = Color.black,
            iconEmission = new Color(0.2f, 0.2f, 0.2f, 1f),
            metallic = 0.8f,
            smoothness = 0.5f,
            solidAlpha = 1f,
            iconAlpha = 1f,
        };

        internal static IconToolNew.IconDesc GetIconDesc(int itemid) =>
            IconDescs.TryGetValue(itemid, out ModIconDesc value) ? value.ToIconDesc() : Default;

        internal static IconToolNew.IconDesc ExportIconDesc(int itemId)
        {
            IconSet iconSet = GameMain.iconSet;

            var iconDesc = new IconToolNew.IconDesc();

            uint num1 = iconSet.itemIconIndex[itemId];

            if (num1 <= 0) return iconDesc;

            FieldInfo[] fields = typeof(IconToolNew.IconDesc).GetFields(BindingFlags.Instance | BindingFlags.Public);

            uint index = 0;

            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.FieldType == typeof(float))
                {
                    fieldInfo.SetValue(iconDesc, iconSet.itemDescArr[(int)num1 * 40 + (int)index++]);
                }
                else if (fieldInfo.FieldType == typeof(Color))
                {
                    float r = iconSet.itemDescArr[(int)num1 * 40 + (int)index++];
                    float g = iconSet.itemDescArr[(int)num1 * 40 + (int)index++];
                    float b = iconSet.itemDescArr[(int)num1 * 40 + (int)index++];
                    float a = iconSet.itemDescArr[(int)num1 * 40 + (int)index++];

                    fieldInfo.SetValue(iconDesc, new Color(r, g, b, a));
                }
            }

            return iconDesc;
        }
    }

    internal static partial class IconDescUtils
    {
        internal abstract class ModIconDesc
        {
            internal Color Color;

            protected ModIconDesc(Color color) => Color = color;

            internal abstract IconToolNew.IconDesc ToIconDesc();
        }

        internal class FluidIconDesc : ModIconDesc
        {
            public FluidIconDesc(Color color) : base(color) {}

            internal override IconToolNew.IconDesc ToIconDesc() =>
                new IconToolNew.IconDesc
                {
                    faceColor = Color,
                    sideColor = Color,
                    reserved0 = Color,
                    faceEmission = Color.black,
                    sideEmission = Color.black,
                    iconEmission = new Color(0.2f, 0.2f, 0.2f, 1f),
                    metallic = 1f,
                    smoothness = 0.302f,
                    liquidity = 1f,
                    solidAlpha = 0f,
                    iconAlpha = 1f,
                };
        }

        internal class NoIconFluidIconDesc : ModIconDesc
        {
            internal NoIconFluidIconDesc(Color color) : base(color) {}

            internal override IconToolNew.IconDesc ToIconDesc() =>
                new IconToolNew.IconDesc
                {
                    faceColor = Color,
                    sideColor = Color,
                    reserved0 = Color,
                    faceEmission = Color.black,
                    sideEmission = Color.black,
                    iconEmission = Color.clear,
                    metallic = 1f,
                    liquidity = 1f,
                    smoothness = 0.302f,
                    solidAlpha = 0f,
                    iconAlpha = 0f,
                };
        }

        internal class OreIconDesc : ModIconDesc
        {
            public OreIconDesc(Color color) : base(color) {}

            internal override IconToolNew.IconDesc ToIconDesc() =>
                new IconToolNew.IconDesc
                {
                    faceColor = Color,
                    sideColor = Color,
                    reserved0 = Color,
                    faceEmission = Color.black,
                    sideEmission = Color.black,
                    iconEmission = Color.clear,
                    metallic = 0.8f,
                    smoothness = 0.5f,
                    solidAlpha = 1f,
                    iconAlpha = 1f,
                };
        }

        internal class NoIconMetalIconDesc : ModIconDesc
        {
            public NoIconMetalIconDesc(Color color) : base(color) {}

            internal override IconToolNew.IconDesc ToIconDesc() =>
                new IconToolNew.IconDesc
                {
                    faceColor = Color,
                    sideColor = Color,
                    faceEmission = Color.black,
                    sideEmission = Color.black,
                    iconEmission = Color.clear,
                    metallic = 1f,
                    smoothness = 0.6f,
                    solidAlpha = 1f,
                    iconAlpha = 0f,
                };
        }

        internal class FullIconDesc : ModIconDesc
        {
            public FullIconDesc(Color color) : base(color) {}

            internal override IconToolNew.IconDesc ToIconDesc() =>
                new IconToolNew.IconDesc
                {
                    faceColor = Color,
                    sideColor = Color,
                    faceEmission = Color.black,
                    sideEmission = Color.black,
                    iconEmission = Color.clear,
                    metallic = 0f,
                    smoothness = 0.5f,
                    solidAlpha = 1f,
                    iconAlpha = 1f,
                };
        }

        internal class ComponentIconDesc : ModIconDesc
        {
            public ComponentIconDesc(Color color) : base(color) {}

            internal override IconToolNew.IconDesc ToIconDesc() =>
                new IconToolNew.IconDesc
                {
                    faceColor = Color.white,
                    sideColor = Color,
                    faceEmission = Color.black,
                    sideEmission = Color.black,
                    iconEmission = Color.clear,
                    metallic = 0.8f,
                    smoothness = 0.5f,
                    solidAlpha = 1f,
                    iconAlpha = 1f,
                };
        }

        internal class GlassIconDesc : ModIconDesc
        {
            public GlassIconDesc(Color color) : base(color) {}

            internal override IconToolNew.IconDesc ToIconDesc() =>
                new IconToolNew.IconDesc
                {
                    faceColor = Color,
                    sideColor = Color,
                    reserved0 = Color,
                    faceEmission = Color.black,
                    sideEmission = Color.black,
                    iconEmission = Color.clear,
                    metallic = 1f,
                    smoothness = 0.5f,
                    solidAlpha = 0.8f,
                    iconAlpha = 0f,
                };
        }

        internal class RodIconDesc : ModIconDesc
        {
            public RodIconDesc(Color color) : base(color) {}

            internal override IconToolNew.IconDesc ToIconDesc() =>
                new IconToolNew.IconDesc
                {
                    faceColor = Color,
                    sideColor = Color,
                    reserved0 = Color,
                    faceEmission = Color.black,
                    sideEmission = Color.black,
                    iconEmission = Color.clear,
                    metallic = 1f,
                    smoothness = 0.5f,
                    solidAlpha = 0.6f,
                    iconAlpha = 1f,
                };
        }

        internal class WhiteIconDesc : ModIconDesc
        {
            public WhiteIconDesc() : base(Color.white) {}

            internal override IconToolNew.IconDesc ToIconDesc() =>
                new IconToolNew.IconDesc
                {
                    faceColor = Color,
                    sideColor = Color,
                    faceEmission = Color.black,
                    sideEmission = Color.black,
                    iconEmission = new Color(0.2f, 0.2f, 0.2f, 1f),
                    metallic = 0.8f,
                    smoothness = 0.5f,
                    solidAlpha = 1f,
                    iconAlpha = 1f,
                };
        }

        internal class MartixIconDesc : ModIconDesc
        {
            private readonly Color _emission;

            public MartixIconDesc(Color color, Color emission) : base(color) => _emission = emission;

            internal override IconToolNew.IconDesc ToIconDesc() =>
                new IconToolNew.IconDesc
                {
                    faceColor = Color,
                    sideColor = Color,
                    faceEmission = _emission,
                    sideEmission = _emission,
                    iconEmission = Color.clear,
                    metallic = 0f,
                    smoothness = 0f,
                    solidAlpha = 0.5f,
                    iconAlpha = 0.0f,
                };
        }

        internal class DefaultIconDesc : ModIconDesc
        {
            private readonly Color _emission;

            public DefaultIconDesc(Color color, Color emission) : base(color) => _emission = emission;

            internal override IconToolNew.IconDesc ToIconDesc() =>
                new IconToolNew.IconDesc
                {
                    faceColor = Color,
                    sideColor = Color,
                    faceEmission = _emission,
                    sideEmission = _emission,
                    iconEmission = new Color(0.2f, 0.2f, 0.2f, 1f),
                    metallic = 0.8f,
                    smoothness = 0.5f,
                    solidAlpha = 1f,
                    iconAlpha = 1f,
                };
        }
    }
}
