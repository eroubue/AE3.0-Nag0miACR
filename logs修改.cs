using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AEAssist;
using AEAssist.CombatRoutine.View.JobView;
using ImGuiNET;
using System.Windows.Forms;

namespace Nagomi
{
    public class LogModifier
    {
        // 生成查找表，用于加密
        private static readonly int[] Lookup = GenerateLookup();
        // 存储用户选择的文件夹路径
        private static string Dirpath = "";
        private static string _sourceName = "";
        private static string _targetNameInput = "";
        private static string _skillName = ""; // 新增：技能名称
        private static int _selectedIndex = 0;
        private static double _multiplier = 1.05;
        private static List<int> _damageValues = new List<int>();
        private static int _selectedDamageType = 0; // 0: 普通, 1: 直击, 2: 暴击, 3: 直爆

      
        

     public static void DrawLogModifierTab(JobViewWindow jobViewWindow)
        {
            // 选择文件夹路径按钮
            if (ImGui.Button("选择文件夹路径"))
            {
                using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Dirpath = dialog.SelectedPath;
                    }
                }
            }

            // 显示当前选择的文件夹路径
            ImGui.Text($"当前文件夹路径: {Dirpath}");

            // 获取日志文件夹中的所有 .log 文件
            if (Directory.Exists(Dirpath))
            {
                List<string> filenames = Directory.GetFiles(Dirpath, "*.log").ToList();
                if (filenames.Count == 0)
                {
                    ImGui.Text("请把logs文件放入logscheat文件夹后重新运行！");
                    return;
                }

               

                // 列出所有日志文件供用户选择
                for (int idx = 0; idx < filenames.Count; idx++)
                {
                    if (ImGui.Selectable($"[{idx}] {Path.GetFileName(filenames[idx])}", _selectedIndex == idx))
                    {
                        _selectedIndex = idx;
                    }
                }

                // 显示用户选择的文件路径
                ImGui.Text($"选择的文件: {filenames[_selectedIndex]}");
                // 提示用户输入角色名
                ImGui.InputText("角色名", ref _sourceName, 256);

                // 提示用户输入BOSS名
                ImGui.InputText("BOSS名", ref _targetNameInput, 256);

                // 提示用户输入需要修改的技能名称
                ImGui.InputText("技能名字(中文)", ref _skillName, 256);

                // 输入伤害倍率
                ImGui.InputDouble("伤害倍率", ref _multiplier);

                // 选择技能伤害类型
                string[] damageTypes = new string[] { "普通", "直击", "暴击", "直爆" };
                string damageTypesStr = string.Join("\0", damageTypes) + "\0"; // 以零字符分隔的字符串
                ImGui.Combo("技能伤害类型", ref _selectedDamageType, damageTypesStr);

                // 修改文件按钮
                if (ImGui.Button("修改文件"))
                {
                    string path = filenames[_selectedIndex];
                    List<string> targetNames = new List<string> { _targetNameInput };
                    ModifyFile(path, _sourceName, _multiplier, targetNames, _selectedDamageType, _skillName);
                }
                // 读取并显示指定技能的伤害值
                if (ImGui.Button("读取指定技能的伤害"))
                {
                    string path = filenames[_selectedIndex];
                    ReadAndDisplaySkillDamage(path, _skillName);
                }

                // 显示指定技能的伤害值
                ImGui.Text("指定技能的伤害值:");
                foreach (var damage in _damageValues)
                {
                    ImGui.Text($"{damage}");
                }
            }
            else
            {
                ImGui.Text("请选择有效的文件夹路径！");
            }
        }
        public static void ReadAndDisplaySkillDamage(string path, string skillName)
        {
            _damageValues.Clear(); // 清空之前的伤害值

            if (File.Exists(path))
            {
                try
                {
                    
                    string[] lines = File.ReadAllLines(path, Encoding.UTF8);
                    foreach (string line in lines)
                    {
                        string[] rowCols = line.Split('|');
                        string damageStr = rowCols[9];
                        int damage;
                        if (damageStr.EndsWith("0000"))
                        {
                            damage = int.Parse(damageStr.Substring(0, damageStr.Length - 4),
                                System.Globalization.NumberStyles.HexNumber);
                        }
                        else if (damageStr.EndsWith("4001"))
                        {
                            damage = 65535 + int.Parse(damageStr.Substring(0, damageStr.Length - 4),
                                System.Globalization.NumberStyles.HexNumber);
                        }
                        else if (damageStr == "0")
                        {
                            
                            continue;
                        }
                        else
                        {
                            
                            continue;
                        }
                        if (rowCols.Length > 5 && rowCols[5] == skillName)
                        {
                            
                            if (int.TryParse(rowCols[9], out damage))
                            {
                                _damageValues.Add(damage);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ImGui.Text($"读取文件时发生错误: {ex.Message}");
                }
            }
            else
            {
                ImGui.Text($"文件 {path} 不存在");
            }
        }

        public static void ModifyFile(string path, string sourceName, double multiplier, List<string> targetNames,
            int damageType, string skillName)
        {
            // 读取日志文件的所有行
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            string[] splitPath = Path.GetFileNameWithoutExtension(path).Split('.');
            string tempTargetPath = $"{splitPath[0]}_temp.{splitPath[1]}";
            string finalTargetPath = $"{splitPath[0]}_modified.{splitPath[1]}";

            // 写入修改后的内容到临时文件
            using (StreamWriter f = new StreamWriter(tempTargetPath, false, Encoding.UTF8))
            {
                int idx = 0;
                foreach (string row in lines)
                {
                    string[] rowCols = row.Split('|');
                    idx++;
                    if (rowCols[0] == "01")
                    {
                        idx = 1;
                    }

                    // 检查是否为战斗日志行
                    if (rowCols[0] != "21" || (!targetNames.Contains(rowCols[6]) && !targetNames.Contains(rowCols[7])))
                    {
                        f.WriteLine(row);
                        continue;
                    }

                    // 检查是否为指定角色的攻击
                    if (rowCols[3] != sourceName || rowCols[3] == rowCols[7])
                    {
                        f.WriteLine(row);
                        continue;
                    }
                    
                    if (!targetNames.Contains(rowCols[6]) && !targetNames.Contains(rowCols[7]))
                    {
                        f.WriteLine(row);
                        continue;
                    }

                    // 检查是否为指定技能
                    if (rowCols[5] != skillName)
                    {
                        f.WriteLine(row);
                        continue;
                    }

                    // 解析伤害值
                    string damageStr = rowCols[9];
                    int damage;
                    if (damageStr.EndsWith("0000"))
                    {
                        damage = int.Parse(damageStr.Substring(0, damageStr.Length - 4),
                            System.Globalization.NumberStyles.HexNumber);
                    }
                    else if (damageStr.EndsWith("4001"))
                    {
                        damage = 65535 + int.Parse(damageStr.Substring(0, damageStr.Length - 4),
                            System.Globalization.NumberStyles.HexNumber);
                    }
                    else if (damageStr == "0")
                    {
                        Console.WriteLine("bugged row?");
                        Console.WriteLine(row);
                        f.WriteLine(row);
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("bugged row?");
                        Console.WriteLine(row);
                        f.WriteLine(row);
                        continue;
                    }

                    // 计算新的伤害值
                    damage = (int)(damage * multiplier);

                    // 格式化新的伤害值
                    string damageIncreased;
                    if (damage >= 65536)
                    {
                        damageIncreased = $"{damage - 65535:X}4001";
                    }
                    else if (0 < damage && damage <= 65535)
                    {
                        damageIncreased = $"{damage:X}0000";
                    }
                    else if (damage == 0)
                    {
                        Console.WriteLine("bugged row?");
                        Console.WriteLine(row);
                        f.WriteLine(row);
                        continue;
                    }
                    else
                    {
                        throw new ArgumentException("damage increased error");
                    }

                    // 修改技能伤害类型
                    switch (damageType)
                    {
                        case 1: // 直击
                            rowCols[8] = "712003";
                            break;
                        case 2: // 暴击
                            rowCols[8] = "714003";
                            break;
                        case 3: // 直爆
                            rowCols[8] = "716003";
                            break;
                        default: // 普通
                            rowCols[8] = "710003";
                            break;
                    }

                    // 更新日志行中的伤害值
                    rowCols[9] = damageIncreased;
                    string text = string.Join("|", rowCols.Take(rowCols.Length - 1));
                    string encCode = Encrypt(text, idx.ToString());
                    rowCols[rowCols.Length - 1] = encCode + "\n";
                    string newRow = string.Join("|", rowCols);
                    Console.WriteLine($"{idx} {row}");
                    Console.WriteLine($"{idx} {newRow}");
                    f.WriteLine(newRow);
                }
            }

            // 将临时文件重命名为最终文件
            File.Move(tempTargetPath, finalTargetPath, true);
        }

        // 加密方法
        public static string Encrypt(string text, string lineNum)
        {
            string testStr = (text + '|' + lineNum);
            byte[] testBytes = Encoding.UTF8.GetBytes(testStr);
            byte[] hashBytes = ComputeSha256Hash(testBytes);
            return U49152(hashBytes);
        }

        // 计算SHA-256哈希值
        private static byte[] ComputeSha256Hash(byte[] input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(input);
            }
        }

        // 将哈希值转换为特定格式的字符串
        private static string U49152(byte[] byteStr)
        {
            char[] res = new char[16];
            for (int i = 0; i < 8; i++)
            {
                int num = Lookup[byteStr[i]];
                res[2 * i] = (char)(num % 128);
                res[2 * i + 1] = (char)((num >> 16) % 128);
            }

            return new string(res);
        }

        // 生成查找表
        private static int[] GenerateLookup()
        {
            int[] numArray = new int[256];
            for (int i = 0; i < 256; i++)
            {
                string hexString = i.ToString("X2");
                numArray[i] = hexString[0] + (hexString[1] << 16);
            }

            return numArray;

        }
    }
}
