using AEAssist;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.MemoryApi;
using FFXIVClientStructs.FFXIV.Common.Math;
using ImGuiNET;
using System.Runtime.CompilerServices;
using AEAssist.Extension;
using AEAssist.Helper;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Plugin.Services;
using ECommons.DalamudServices;
using Nagomi.Utils;

namespace Nagomi.Shared;

/// <summary>
/// 共享悬浮窗模块，包含所有重复的 ImGui 功能
/// </summary>
public static class 共享悬浮窗模块
{
    // 静态变量，每个职业独立管理
    private static readonly Dictionary<string, bool> showHiddenImguiDict = new();
    private static readonly Dictionary<string, bool> showHiddenImguiProDict = new();
    private static readonly Dictionary<string, int> decorativeSliderValueDict = new();
    private static readonly Dictionary<string, int> decorativeSliderValue1Dict = new();
    private static readonly Dictionary<string, int> decorativeSliderValue2Dict = new();
    private static readonly Dictionary<string, int> decorativeSliderValue3Dict = new();
    private static readonly Dictionary<string, int> decorativeSliderValue4Dict = new();
    private static readonly Dictionary<string, string> passwordDict = new();
    
    private const string correctPassword = "22/7";
    private const string correctPasswordPro = "我是西条和的狗";
    
    // DEBUG 模式下的自定义消息相关变量
    private static string customName = "";
    private static string customWorld = "";
    private static string customMessage = "";
    private static XivChatType customType = XivChatType.Say;

    /// <summary>
    /// 获取职业特定的变量
    /// </summary>
    private static string GetJobKey(string jobName)
    {
        string jobKey = jobName.ToLower();
        
        // 初始化所有字典（如果不存在）
        if (!showHiddenImguiDict.ContainsKey(jobKey))
            showHiddenImguiDict[jobKey] = false;
        if (!showHiddenImguiProDict.ContainsKey(jobKey))
            showHiddenImguiProDict[jobKey] = false;
        if (!decorativeSliderValueDict.ContainsKey(jobKey))
            decorativeSliderValueDict[jobKey] = 0;
        if (!decorativeSliderValue1Dict.ContainsKey(jobKey))
            decorativeSliderValue1Dict[jobKey] = 0;
        if (!decorativeSliderValue2Dict.ContainsKey(jobKey))
            decorativeSliderValue2Dict[jobKey] = 0;
        if (!decorativeSliderValue3Dict.ContainsKey(jobKey))
            decorativeSliderValue3Dict[jobKey] = 0;
        if (!decorativeSliderValue4Dict.ContainsKey(jobKey))
            decorativeSliderValue4Dict[jobKey] = 0;
        if (!passwordDict.ContainsKey(jobKey))
            passwordDict[jobKey] = "";
            
        return jobKey;
    }

    /// <summary>
    /// 绘制通用的倒计时信息
    /// </summary>
    public static void DrawCountdownInfo()
    {
        if (ImGui.CollapsingHeader("倒计时"))
        {
            DefaultInterpolatedStringHandler interpolatedStringHandler;
            interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
            interpolatedStringHandler.AppendLiteral("状态: ");
            interpolatedStringHandler.AppendFormatted<bool>(Core.Resolve<MemApiCountdown>().IsActive());
            ImGui.TextUnformatted(interpolatedStringHandler.ToStringAndClear());
            interpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
            interpolatedStringHandler.AppendLiteral("倒计时剩余: ");
            interpolatedStringHandler.AppendFormatted<float>(Core.Resolve<MemApiCountdown>().TimeRemaining());
            ImGui.TextUnformatted(interpolatedStringHandler.ToStringAndClear());
        }
    }

    /// <summary>
    /// 绘制通用的战斗信息
    /// </summary>
    public static void DrawCombatInfo()
    {
        if (ImGui.CollapsingHeader("通用"))
        {
            ImGui.TextUnformatted($"自身是否在移动: {Helper.自身是否在移动()}");
            ImGui.TextUnformatted($"自身是否在读条: {Helper.自身是否在读条()}");
            ImGui.TextUnformatted($"GCD剩余时间: {Helper.GCD剩余时间()}");
            ImGui.TextUnformatted($"GCD可用状态: {Helper.GCD可用状态()}");
            ImGui.TextUnformatted($"高优先级gcd队列中技能数量: {Helper.高优先级gcd队列中技能数量()}");
            ImGui.TextUnformatted($"上一个gcd技能: {Helper.上一个gcd技能()}");
            ImGui.TextUnformatted($"上一个能力技能: {Helper.上一个能力技能()}");
            ImGui.TextUnformatted($"上一个连击技能: {Helper.上一个连击技能()}");
        }
    }

    /// <summary>
    /// 绘制LB信息
    /// </summary>
    public static void DrawLBInfo()
    {
        if (ImGui.CollapsingHeader("LB"))
        {
            ImGui.Text("LB充能槽数量: " + Core.Me.LimitBreakBarCount().ToString());
            ImGui.Text("每条充能槽填满所需: " + Core.Me.LimitBreakBarValue().ToString());
            ImGui.Text("当前LB充能数值: " + Core.Me.LimitBreakCurrentValue().ToString());
        }
    }

    /// <summary>
    /// 绘制获取链接按钮
    /// </summary>
    public static void DrawGetLinksButtons()
    {
        if (ImGui.Button("获取Nag0mi的触发器链接"))
        {
            Core.Resolve<MemApiChatMessage>().Toast2("感谢使用\nヾ(￣▽￣)已为您输出至默语频道", 1, 2000);
            Core.Resolve<MemApiSendMessage>().SendMessage("/e https://github.com/eroubue/TRN");
        }
        if (ImGui.Button("获取Nag0mi的logs可视化修改器链接"))
        {
            Core.Resolve<MemApiChatMessage>().Toast2("感谢使用\nヾ(￣▽￣)已为您输出至默语频道", 1, 2000);
            Core.Resolve<MemApiSendMessage>().SendMessage("/e https://github.com/eroubue/FFXIV_Logs_GUI_Editor/releases");
        }

        if (ImGui.CollapsingHeader("FA调试"))
        {
            if (ImGui.Button("获取当前目标点"))
            {
                Core.Resolve<MemApiSendMessage>().SendMessage("/e 获取当前目标点");
            }
            
            if (ImGui.Button("获取完整状态"))
            {
                Core.Resolve<MemApiSendMessage>().SendMessage("/e 获取完整状态");
            }
            if (ImGui.Button("获取移动状态"))
            {
                Core.Resolve<MemApiSendMessage>().SendMessage("/e 获取移动状态");
            }
            if (ImGui.Button("获取延迟状态"))
            {
                Core.Resolve<MemApiSendMessage>().SendMessage("/e 获取延迟状态");
            }
            if (ImGui.Button("获取队列状态"))
            {
                Core.Resolve<MemApiSendMessage>().SendMessage("/e 获取队列状态");
            }
            if (ImGui.Button("获取队列长度"))
            {
                Core.Resolve<MemApiSendMessage>().SendMessage("/e 获取队列长度");
            }
            if (ImGui.Button("获取当前位置方向"))
            {
                Core.Resolve<MemApiSendMessage>().SendMessage("/e 获取当前位置方向");
            }
            if (ImGui.Button("安全地获取当前地图基础名称"))
            {
                string mapName = MapUtils.GetMapBaseNameSafely();
                if (!string.IsNullOrEmpty(mapName))
                {
                    LogHelper.Print($"当前地图: {mapName}");
                }
                else
                {
                    LogHelper.Print("无法获取地图信息");
                }
            }
            if (ImGui.Button("GetCurrentTerritoryIdSafely"))
            {
                uint territoryId = MapUtils.GetCurrentTerritoryIdSafely();
                if (territoryId > 0)
                {
                    LogHelper.Print($"当前区域ID: {territoryId}");
                }
                else
                {
                    LogHelper.Print("无法获取区域ID");
                }
            }
            if (ImGui.Button("GetCurrentWeatherIdSafely"))
            {
                byte weatherId = MapUtils.GetCurrentWeatherIdSafely();
                if (weatherId > 0)
                {
                    LogHelper.Print($"当前天气ID: {weatherId}");
                }
                else
                {
                    LogHelper.Print("无法获取天气ID");
                }
            }
            
        }
    }
        

    /// <summary>
    /// 绘制底裤功能模块
    /// </summary>
    public static void DrawHiddenFeatures(string jobName)
    {
        string jobKey = GetJobKey(jobName);
        
        // 初始化变量
        if (!showHiddenImguiDict.ContainsKey(jobKey))
        {
            showHiddenImguiDict[jobKey] = false;
            showHiddenImguiProDict[jobKey] = false;
            decorativeSliderValueDict[jobKey] = 0;
            decorativeSliderValue1Dict[jobKey] = 0;
            decorativeSliderValue2Dict[jobKey] = 0;
            decorativeSliderValue3Dict[jobKey] = 0;
            decorativeSliderValue4Dict[jobKey] = 0;
            passwordDict[jobKey] = "";
        }

        if (ImGui.CollapsingHeader("底裤功能,轮盘赌通过才可开启"))
        {
            // 获取当前时间
            float time = (float)ImGui.GetTime();

            // 创建脉动效果
            float scale = 1.0f + 0.05f * (float)Math.Sin(time * 2.0f);

            // 设置按钮样式
            ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.1f, 0.7f, 0.4f, 1.0f));
            ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Vector4(0.1f, 0.9f, 0.6f, 1.0f));
            ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Vector4(0.1f, 0.5f, 0.3f, 1.0f));

            if (ImGui.Button("来一枪解锁底裤功能", new Vector2(180, 40)))
            {
                轮盘赌(jobKey);
            }

            if (showHiddenImguiDict[jobKey])
            {
                ImGui.Text("恭喜你，你赢了！成功解锁底裤功能！");
                
                // 使用局部变量来避免 ref 参数问题
                int sliderValue = decorativeSliderValueDict[jobKey];
                int sliderValue1 = decorativeSliderValue1Dict[jobKey];
                int sliderValue2 = decorativeSliderValue2Dict[jobKey];
                int sliderValue3 = decorativeSliderValue3Dict[jobKey];
                int sliderValue4 = decorativeSliderValue4Dict[jobKey];
                
                ImGui.SliderInt("修改额外暴击率", ref sliderValue, 0, 15);
                ImGui.SliderInt("修改额外直击率", ref sliderValue1, 0, 15);
                ImGui.SliderInt("稀有物品爆率提升(坐骑等)", ref sliderValue2, 0, 20);
                ImGui.SliderInt("伤害浮动固定(最高提升5%)", ref sliderValue3, 0, 5);
                ImGui.SliderInt("挖宝下底加成", ref sliderValue4, 0, 5);
                
                // 更新字典中的值
                decorativeSliderValueDict[jobKey] = sliderValue;
                decorativeSliderValue1Dict[jobKey] = sliderValue1;
                decorativeSliderValue2Dict[jobKey] = sliderValue2;
                decorativeSliderValue3Dict[jobKey] = sliderValue3;
                decorativeSliderValue4Dict[jobKey] = sliderValue4;
                
                ImGui.Button("批量生成高级码(lv1以上可用)", new Vector2(300, 40));
                ImGui.Button("一键金100logs", new Vector2(180, 40));
                ImGui.Button("对选中目标批量发送举报加速封号", new Vector2(300, 40));
      
                ImGui.InputText("用户名", ref customName, 100);
                ImGui.InputText("服务器", ref customWorld, 100);
                ImGui.InputText("消息内容", ref customMessage, 300);

                if (ImGui.BeginCombo("消息类型", customType.ToString()))
                {
                    foreach (XivChatType type in Enum.GetValues(typeof(XivChatType)))
                    {
                        bool isSelected = (type == customType);
                        if (ImGui.Selectable(type.ToString(), isSelected))
                        {
                            customType = type;
                        }

                        if (isSelected)
                        {
                            ImGui.SetItemDefaultFocus();
                        }
                    }
                    ImGui.EndCombo();
                }

                if (ImGui.Button("Print"))
                {
                    new FakeMessage().PrintFakeMessage(customName, customWorld, customMessage, customType);
                }

            }

            // 恢复样式
            ImGui.PopStyleColor(3);
            ImGui.Text("输入密码来绕过轮盘赌");
        
            // 使用局部变量来避免 ref 参数问题
            string password = passwordDict[jobKey];
            ImGui.InputText("密码", ref password, 256, ImGuiInputTextFlags.Password);
            passwordDict[jobKey] = password;

            if (ImGui.Button("提交"))
            {
                if (password == correctPassword)
                {
                    showHiddenImguiDict[jobKey] = true;
                }
                if (password == correctPasswordPro)
                {
                    showHiddenImguiDict[jobKey] = true;
                    showHiddenImguiProDict[jobKey] = true;
                }
                else
                {
                    ImGui.TextColored(new System.Numerics.Vector4(1, 0, 0, 1), "错误的密码!");
                }
            }

            if (showHiddenImguiDict[jobKey])
            {
                ImGui.Text("已经解锁!");
            }
        }

        
    }

    /// <summary>
    /// 轮盘赌功能
    /// </summary>
    private static void 轮盘赌(string jobKey)
    {
        if (new Random().Next(1, 3) == 1)
        {
            string name = "丝瓜卡夫卡";
            string server = "拂晓之间";
            string say = "别开挂了听不见吗？";
            new FakeMessage().PrintFakeMessage(name, server, say, XivChatType.TellIncoming);
        }
        else
        {
            Core.Resolve<MemApiChatMessage>().Toast2("解锁！", 1, 2000);
            showHiddenImguiDict[jobKey] = true;
        }
    }


    public class FakeMessage
    {

        public void PrintFakeMessage(string Name, string World, string Message, XivChatType Type)
        {//< icon(88) >
            var builder = new SeStringBuilder().AddText(Name);

            if (!string.IsNullOrEmpty(World))
            {
                builder.AddIcon(BitmapFontIcon.CrossWorld)
                    .AddText(World);
            }

            var Test = builder.Build();
            Svc.Chat.Print(new XivChatEntry()
            {

                Type = Type,
                Name = Test,
                Message = Message
            });
        }


    }
} 