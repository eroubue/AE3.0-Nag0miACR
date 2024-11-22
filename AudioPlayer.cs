using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Triggernometry;
using Triggernometry.PluginBridges;
using static Triggernometry.Action.ActionTypeEnum;
using static Triggernometry.Entity.RoleType;
using static Triggernometry.Interpreter.StaticHelpers;
using static Triggernometry.RealPlugin;
using Action = Triggernometry.Action;

plug.UnregisterNamedCallback("LatihasTUW");
plug.RegisterNamedCallback("LatihasTUW", new Action<object, string>(LatihasTUW.Callback), null);

public static class LatihasTUW {
 private static string MyJob, MyName, hs, ttsafe;
 private static bool Ubroadcast, Uthreebucket;
 private static Vector2 Zhuzi2;
 private static Ciyu ciyu;
 private static List<DHP43> p43dhDist = new();
 private static int p43dhCount;
 private static readonly Player[] Players = new Player[8];
 private static readonly List<string> P43PlayerName = new();
 private static readonly List<Player> Threebuckets = new();
 private static readonly Trigger _tri = new();
 private static readonly List<Zhuzi> Zhuzis = new();
 private static readonly string[] JobOrder = { "MT", "ST", "H1", "H2", "D1", "D2", "D3", "D4" },
  TBJobOrder = { "MT", "ST", "D1", "D2", "D3", "D4", "H1", "H2" },
  Server = {
   "红玉海", "神意之地", "拉诺西亚", "幻影群岛", "萌芽池", "宇宙和音", "沃仙曦染", "晨曦王座", "白银乡", "白金幻象", "神拳痕", "潮风亭", "旅人栈桥", "拂晓之间",
   "龙巢神殿", "梦羽宝境", "紫水栈桥", "延夏", "静语庄园", "摩杜纳", "海猫茶屋", "柔风海湾", "琥珀原", "水晶塔", "银泪湖", "伊修加德", "太阳海岸", "红茶川", "Anima",
   "Belias", "Chocobo", "Hades", "Ixion", "Mandragora", "Masamune", "Pandaemonium", "Shinryu", "Titan", "Asura"
  };

 private static void Place(string expr) {
  try {
   plug.InvokeNamedCallback("AdvWm", expr);
  }
  catch (Exception ex) {
   Log($"Place Error({expr}):{ex.StackTrace}");
  }
 }

 public static void Callback(object _, string str) {
  try {
   var ss = str.Split(':');
   switch (ss[0].ToLower()) {
    case "initplace": //v
     Place(StaticPlace.initPlace);
     break;
    case "broadcast": //str
     Broadcast(ss[1]);
     break;
    case "tts": //str
     PostTTS(ss[1]);
     break;
    case "ydsx":
     ydsx();
     break;
    case "p0init": //str:job
     if (str.Contains(":")) {
      var tmp = ss[1].Split(',')[0].ToUpper();
      if (JobOrder.All(j => j != tmp)) {
       Log(tmp);
       if (tmp == Tank.ToString().ToUpper()) MyJob = "MT";
       if (tmp == PureHealer.ToString().ToUpper()) MyJob = "H1";
       if (tmp == BarrierHealer.ToString().ToUpper()) MyJob = "H2";
       if (tmp == StrengthMelee.ToString().ToUpper()) MyJob = "D1";
       if (tmp == DexterityMelee.ToString().ToUpper()) MyJob = "D2";
       if (tmp == PhysicalRanged.ToString().ToUpper()) MyJob = "D3";
       if (tmp == MagicalRanged.ToString().ToUpper()) MyJob = "D4";
      }
      else MyJob = tmp;
      GetPartyOrderInit(ss[1]);
     }
     else if (MyJob != null) {
      InitParams();
      PostTTS($"已使用{MyJob}进行初始化");
      Log(string.Join("|", Players));
     }
     break;
    case "p1place": //void
     Place(StaticPlace.initPlace);
     Place(StaticPlace.clear2);
     Place(StaticPlace.clear3);
     Place(StaticPlace.clear4);
     if (MyJob == null) PostTTS("未设置职业，请检查设置。");
     break;
    case "p1ciyudmg": //str
     lock (ciyu) { ciyu.dmg.Add(ss[1]); }
     break;
    case "p1ciyucs": //void
     lock (ciyu) { ciyu.cs++; }
     break;
    case "p1ciyudeath": //void
     lock (ciyu) {
      if (ciyu.cs != 2) Broadcast($"羽炸:{string.Join("|", ciyu.dmg)}");
     }
     break;
    case "p1fs1": //void
     var es = GetXYFromBnpcid("8723");
     if (es.Count == 2) {
      var fs1 = GetDir(es[0]);
      var fs2 = GetDir(es[1]);
      PostTTS($"{fs1},{fs2}");
      FsPlace(fs1, fs2);
     }
     break;
    case "p1fs2": //void
     const string fs3 = "左西";
     const string fs4 = "右东";
     PostTTS($"{fs3},{fs4}");
     FsPlace(fs3, fs4);
     break;
    case "p2hs": //id,x,y
     p2hs(ss[1]);
     break;
    case "p2safe": //x,y
     p2safe(ss[1]);
     break;
    case "p2zhuzi": //id,x,y
     p2zhuzi(ss[1]);
     break;
    case "p2zhuzidmg": //str
     p2zhuzidmg(ss[1]);
     break;
    case "p2zhuzijx": //id
     p2zhuzijx(ss[1]);
     break;
    case "p2zhuzideath": //id
     p2zhuzideath(ss[1]);
     break;
    case "p2zhuzi23": //vois
     p2zhuzi23();
     break;
    case "p3threebucket": //name
     ThreeBucket(ss[1]);
     break;
    case "p3fly": //id
     p3fly(ss[1]);
     break;
    case "p3ygjd": //x,y
     p3ygjd(ss[1]);
     break;
    case "p41": //void
     p41();
     break;
    case "p41taitan": //x
     p41taitan(ss[1]);
     break;
    case "p42hs": //void
     p42hs();
     break;
    case "p42place2": //void
     Place(StaticPlace.p42place2);
     break;
    case "p42place2rf": //void
     Place(StaticPlace.p42place2rf);
     break;
    case "p43place": //void
     Place(StaticPlace.startp43);
     break;
    case "p43dh": //xy
     p43dh(ss[1]);
     break;
    case "p43fq": //name
     p43dh_fq(ss[1], "风枪");
     break;
    case "jzha": //void
     Place(StaticPlace.jzhA);
     Broadcast("A点");
     break;
    case "jzh2": //void
     Place(StaticPlace.jzh2);
     Broadcast("2点");
     break;
    case "jzh3": //void
     Place(StaticPlace.jzh3);
     Broadcast("3点");
     break;
   }
  }
  catch (Exception e) {
   Log($"Error: {str}");
   Log(e.StackTrace);
  }
 }

 private static void ydsx() {
  bool StDead = false, MtDead = false;
  foreach (var p in Players) {
   if (BridgeFFXIV.GetNamedPartyMember(p.name).GetValue("currenthp").ToString() == "0" && p.job == "ST")
    StDead = true;
   if (BridgeFFXIV.GetNamedPartyMember(p.name).GetValue("currenthp").ToString() == "0" && p.job == "MT")
    MtDead = true;
  }
  var op = "二仇炮。";
  if (MtDead || StDead) {
   op += "T死亡，二仇注意出人群";
   Broadcast(op);
  }
  PostTTS(op);
 }

 private static void p42hs() {
  var yflt = GetXYFromBnpcid("8730", 1)[0];
  var dir = GetDir(yflt);
  if (dir is "左上西北" or "右下东南") Place("action:place\n4:100,100 polar 18,45\u00b0");
  if (dir is "左下西南" or "右上东北") Place("action:place\n4:100,100 polar 18,-45\u00b0");
 }

 /// <summary>
 /// 执行特定的场景逻辑，根据方位和条件计算最优行动路径。
 /// </summary>
 private static void p41() {
     // 获取特定ID的XY坐标和方向信息
     var jll = GetXYFromBnpcid("8722", 1)[0];
     var tt = GetXYFromBnpcid("8727", 1)[0];
     var yflt = GetXYFromBnpcid("8730", 1)[0];
     var yflt_dir = GetDir(yflt);
     var jjsb = GetXYFromBnpcid("8734")[0];
     var jjsb_dir = GetDir(jjsb);
     var tt_dir = GetDir(tt);
     var result = new List<P41Info>();
     
     // 遍历四个可能的方向，根据条件判断是否符合条件
     foreach (var s in new[] { "上北", "下南", "左西", "右东" }) {
         if (jll.X > 100 && s == "右东" || jll.X < 100 && s == "左西" ||
             jll.Y > 100 && s == "下南" || jll.Y < 100 && s == "上北" ||
             s == tt_dir) continue;
         
         // 根据方向添加可能的结果
         switch (s) {
             case "上北":
                 if (jjsb_dir != "左上西北")
                     result.Add(new P41Info(s, "上北然后逆时针", new Vector2(91, 83),
                         yflt_dir != "左上西北" && yflt_dir != "右下东南"));
                 if (jjsb_dir != "右上东北")
                     result.Add(new P41Info(s, "上北然后顺时针", new Vector2(109, 83),
                         yflt_dir != "右上东北" && yflt_dir != "左下西南"));
                 break;
             case "下南":
                 if (jjsb_dir != "左下西南")
                     result.Add(new P41Info(s, "下南然后顺时针", new Vector2(91, 117),
                         yflt_dir != "右上东北" && yflt_dir != "左下西南"));
                 if (jjsb_dir != "右下东南")
                     result.Add(new P41Info(s, "下南然后逆时针", new Vector2(109, 117),
                         yflt_dir != "左上西北" && yflt_dir != "右下东南"));
                 break;
             case "左西":
                 if (jjsb_dir != "左上西北")
                     result.Add(new P41Info(s, "左西然后顺时针", new Vector2(83, 91),
                         yflt_dir != "左上西北" && yflt_dir != "右下东南"));
                 if (jjsb_dir != "左下西南")
                     result.Add(new P41Info(s, "左西然后逆时针", new Vector2(83, 109),
                         yflt_dir != "右上东北" && yflt_dir != "左下西南"));
                 break;
             case "右东":
                 if (jjsb_dir != "右上东北")
                     result.Add(new P41Info(s, "右东然后逆时针", new Vector2(117, 91),
                         yflt_dir != "右上东北" && yflt_dir != "左下西南"));
                 if (jjsb_dir != "右下东南")
                     result.Add(new P41Info(s, "右东然后顺时针", new Vector2(117, 109),
                         yflt_dir != "左上西北" && yflt_dir != "右下东南"));
                 break;
         }
     }
     
     // 根据是否能执行特殊动作对结果进行排序
     var esresult = result.Where(t => t.canES).ToList();
     esresult.AddRange(result.Where(t => !t.canES));
     
     // 选择最优的结果并执行相应的动作
     var recommand = esresult[0];
     PostTip(recommand.desc);
     PostTTS(recommand.desc);
     Place(recommand.first switch {
         "上北" => StaticPlace.safeA,
         "下南" => StaticPlace.safeC,
         "左西" => StaticPlace.safeD,
         "右东" => StaticPlace.safeB
     });
     
     // 记录日志并延迟执行后续动作
     Log($"可能安全点:{string.Join("|", esresult)}。神兵:{jjsb_dir},土神:{tt_dir},火神:{yflt_dir}。");
     Task.Run(async delegate {
         await Task.Delay(recommand.canES ? 1000 : 6000);
         Place($"action:place\n4:{recommand.after.X},{recommand.after.Y}");
     });
 }

 private static void p43dh_fq(string s, string desc) {
  if (s == MyName) {
   PostTTS(desc);
   PostTip(desc);
  }
  lock (P43PlayerName) {
   P43PlayerName.Add(s);
   Broadcast($"{desc} {s}");
   if (P43PlayerName.Count == 5) p43qdp();
  }
 }

 private static void p43qdp() {
  var nm = "";
  foreach (var player in Players) {
   var pn = player.name;
   if (player.job is "MT" or "ST" || P43PlayerName.Contains(pn)) continue;
   if (nm == "") nm = pn;
   else return;
  }
  if (nm == MyName) {
   PostTTS("潜地炮");
   PostTip("潜地炮");
  }
  Broadcast($"潜地炮 {nm}");
 }

 private static void p43dh(string xy) {
  var pos = GetXY(xy);
  lock (p43dhDist) {
   var iter = 0;
   foreach (var player in Players) {
    if (player.job is "ST" or "MT") continue;
    var pn = player.name;
    var pl = BridgeFFXIV.GetNamedPartyMember(pn);
    var dist = Vector2.DistanceSquared(pos,
     new Vector2(float.Parse(pl.GetValue("x").ToString()),
      float.Parse(pl.GetValue("y").ToString())));
    if (p43dhCount == 0) p43dhDist.Add(new DHP43(pn, dist));
    else {
     p43dhDist[iter].dist = Math.Min(p43dhDist[iter].dist, dist);
     iter++;
    }
   }
   if (++p43dhCount != 3) return;
   p43dhDist.Sort((a, b) => a.dist.CompareTo(b.dist));
   p43dh_fq(p43dhDist[0].name, "地火");
   p43dh_fq(p43dhDist[1].name, "地火");
   p43dh_fq(p43dhDist[2].name, "地火");
  }
 }

 private static void p41taitan(string x) {
  var dir = x switch {
   "113.70" => "右右右",
   "86.30" => "左左左"
  };
  PostTip(dir);
  PostTTS(dir);
 }

 private static void p2zhuzideath(string s) {
  foreach (var z in Zhuzis.Where(z => z.zid == s && z.cs != 2)) {
   Broadcast($"柱炸:{string.Join("|", z.dmg)}");
   break;
  }
 }

 private static void p2zhuzijx(string s) {
  lock (Zhuzis) {
   foreach (var z in Zhuzis.Where(z => z.zid == s)) {
    z.cs++;
    break;
   }
  }
 }


 private static void p2zhuzidmg(string st) {
  var ss = st.Split(',');
  var s = ss[0];
  var t = ss[1];
  lock (Zhuzis) {
   foreach (var z in Zhuzis.Where(z => z.zid == t)) {
    z.dmg.Add(s);
    break;
   }
  }
 }

 private static void p2zhuzi23() {
  if (Zhuzi2.X is 84 or 93) Zhuzi2.X = 88;
  if (Zhuzi2.X is 107 or 116) Zhuzi2.X = 112;
  if (Zhuzi2.Y is 84 or 93) Zhuzi2.Y = 88;
  if (Zhuzi2.Y is 107 or 116) Zhuzi2.Y = 112;
  Place($"action:place\n2:{Zhuzi2.X},{Zhuzi2.Y}\n3:{200 - Zhuzi2.X},{200 - Zhuzi2.Y}");
 }

 private static void p2zhuzi(string dxy) {
  var ss = dxy.Split(',');
  var d = ss[0];
  var x = ss[1];
  var y = ss[2];
  lock (Zhuzis) {
   if (x == "107.00" && y == "107.00") Zhuzis.Add(new Zhuzi(d, 1 << 7));
   else if (x == "93.00" && y is "107.00" or "106.95" or "107") Zhuzis.Add(new Zhuzi(d, 1 << 6));
   else if (x == "90.00" && y == "100.00") Zhuzis.Add(new Zhuzi(d, 1 << 5));
   else if (x == "100.00" && y == "90.00") Zhuzis.Add(new Zhuzi(d, 1 << 4));
   else if (x == "93.00" && y == "93.00") Zhuzis.Add(new Zhuzi(d, 1 << 3));
   else if (x == "110.00" && y == "100.00") Zhuzis.Add(new Zhuzi(d, 1 << 2));
   else if (x == "100.00" && y == "110.00") Zhuzis.Add(new Zhuzi(d, 1 << 1));
   else if (x == "107.00" && y == "93.00") Zhuzis.Add(new Zhuzi(d, 1));
   else Log($"Error: Zhuzi {dxy}.");
   if (Zhuzis.Count != 4) return;
   Zhuzi2 = Zhuzis.Aggregate(0, (current, z) => current | z.pos) switch {
    0b11110000 => new Vector2(116, 93),
    0b01001110 => new Vector2(107, 84),
    0b00101011 => new Vector2(116, 107),
    0b01011100 => new Vector2(107, 116),
    0b10110001 => new Vector2(93, 116),
    0b00001111 => new Vector2(84, 107),
    0b11010100 => new Vector2(84, 93),
    0b10100011 => new Vector2(93, 84)
   };
   Place($"action:place\n2:{Zhuzi2.X},{Zhuzi2.Y}");
  }
 }

 private static void p3ygjd(string xy) {
  var ss = xy.Split(',');
  var x = ss[0];
  var y = ss[1];
  var ygjddir = "";
  if (x == "95.00" && y is "111.00" or "112.00" ||
   x == "88.00" && y == "95.00" ||
   x == "105.00" && y == "88.00" ||
   x == "112.00" && y == "105.00") ygjddir = "右右右";
  if (x == "105.00" && y == "112.00" ||
   x == "88.00" && y == "105.00" ||
   x == "95.00" && y == "88.00" ||
   x is "111.00" or "112.00" && y == "95.00") ygjddir = "左左左";
  if (ygjddir == "") return;
  if (x == "95.00" && y is "111.00" or "112.00" || x is "111.00" or "112.00" && y == "95.00")
   Place("action:place\n2:105,105");
  if (x == "105.00" && y == "112.00" || x == "88.00" && y == "95.00")
   Place("action:place\n2:95,105");
  if (x == "105.00" && y == "88.00" || x == "88.00" && y == "105.00")
   Place("action:place\n2:95,95");
  if (x == "95.00" && y == "88.00" || x == "112.00" && y == "105.00")
   Place("action:place\n2:105,95");
  var index = -1;
  switch (ttsafe) {
   case "AAAA":
    Place("action:place\n3:100,105");
    index = 0;
    break;
   case "BBBB":
    Place("action:place\n3:95,100");
    index = 1;
    break;
   case "CCCC":
    Place("action:place\n3:100,95");
    index = 2;
    break;
   case "DDDD":
    Place("action:place\n3:105,100");
    index = 3;
    break;
  }
  Place(StaticPlace.ygjdPlace4[index, ygjddir == "左左左" ? 0 : 1]);
  PostTip(ygjddir);
  PostTTS(ygjddir);
 }

 private static void Broadcast(string s) {
  plug.InvokeNamedCallback("command", !Ubroadcast ? $"/e {s}" : $"/p {s} <se.1>");
 }

 private static string GetScale(string name) {
  return GetScalarVariable(false, "ptuw_" + name);
 }

 private static void p3fly(string id) {
  var pos = GetXY(id);
  if (Math.Abs(pos.X - 100) <= 2 && Math.Abs(pos.Y - 114) <= 2) {
   ttsafe = "AAAA";
   Place(StaticPlace.safeA);
  }
  if (Math.Abs(pos.X - 86) <= 2 && Math.Abs(pos.Y - 100) <= 2) {
   ttsafe = "BBBB";
   Place(StaticPlace.safeB);
  }
  if (Math.Abs(pos.X - 100) <= 2 && Math.Abs(pos.Y - 86) <= 2) {
   ttsafe = "CCCC";
   Place(StaticPlace.safeC);
  }
  if (Math.Abs(pos.X - 114) <= 2 && Math.Abs(pos.Y - 100) <= 2) {
   ttsafe = "DDDD";
   Place(StaticPlace.safeD);
  }
  Broadcast(ttsafe);
  PostTTS(ttsafe);
 }

 private static void PostTTS(string text) {
  plug.QueueAction(fakectx, _tri, null,
   new Action {
    ActionType = LogMessage.ToString(),
    LogMessageText = $"[Latihas TTS] {text}",
    LogProcess = "true"
   }, DateTime.Now, true);
 }

 private static void PostTip(string text, float x = 0, float y = 0, string id = "main") {
  plug.QueueAction(fakectx, _tri, null,
   new Action {
    ActionType = LogMessage.ToString(),
    LogMessageText = $"[Latihas Tip] {text}:{x}:{y}:{id}",
    LogProcess = "true"
   }, DateTime.Now, true);
 }

 private static void p2safe(string xy) {
  if (hs == "") return;
  var zzpos = GetXY(xy);
  string safe = null;
  if (Math.Abs(zzpos.X - 100) < 1 && zzpos.Y < 85 && hs != "上北" && hs != "下南") safe = "下南";
  if (Math.Abs(zzpos.X - 100) < 1 && zzpos.Y > 115 && hs != "上北" && hs != "下南") safe = "上北";
  if (Math.Abs(zzpos.Y - 100) < 1 && zzpos.X < 85 && hs != "左西" && hs != "右东") safe = "右东";
  if (Math.Abs(zzpos.Y - 100) < 1 && zzpos.X > 115 && hs != "左西" && hs != "右东") safe = "左西";
  if (safe is null) return;
  Place(safe switch {
   "上北" => StaticPlace.safeA,
   "下南" => StaticPlace.safeC,
   "左西" => StaticPlace.safeD,
   "右东" => StaticPlace.safeB
  });
  safe += "安全";
  PostTTS(safe);
  PostTip(safe);
 }

 private static void p2hs(string xy) {
  var ss = xy.Split(',');
  var id = ss[0];
  if (BridgeFFXIV.GetIdEntity(id).GetValue("bnpcid").ToString() != "8730") return;
  hs = GetDir(ss[1], ss[2]);
  if (hs is "上北" or "下南") Place("action:place\n2:90,100\n3:110,100");
  if (hs is "左西" or "右东") Place("action:place\n2:100,90\n3:100,110");
  Place(StaticPlace.clear4);
  var pos = "火神" + hs;
  PostTTS(pos);
  PostTip(pos, 50, 50, "main2");
 }


 private static void FsPlace(string pl1, string pl2) {
  Place(pl1 switch {
   "上北" => StaticPlace.fsN3,
   "下南" => StaticPlace.fsS3,
   "左西" => StaticPlace.fsW3,
   "右东" => StaticPlace.fsE3
  });
  Place(pl2 switch {
   "上北" => StaticPlace.fsN4,
   "下南" => StaticPlace.fsS4,
   "左西" => StaticPlace.fsW4,
   "右东" => StaticPlace.fsE4
  });
 }

 private static string GetDir(Vector2 v) {
  return GetDir(v.X, v.Y);
 }

 private static string GetDir(string x, string y) {
  return GetDir(float.Parse(x), float.Parse(y));
 }

 private static string GetDir(float x, float y) {
  return x switch {
   > 110 when y > 110 => "右下东南",
   > 110 when y < 90 => "右上东北",
   < 90 when y < 90 => "左上西北",
   < 90 when y > 110 => "左下西南",
   > 115 => "右东",
   < 85 => "左西",
   _ => y switch {
    < 85 => "上北",
    > 115 => "下南",
    _ => ""
   }
  };
 }

 private static List<Vector2> GetXYFromBnpcid(string arg, int reqHP = -1) {
  return arg.Any(c => c is (< '0' or > '9') and (< 'A' or > 'F') and (< 'a' or > 'f'))
   ? null
   : (from en in BridgeFFXIV.GetAllEntities().Where(en => en.GetValue("bnpcid").ToString() == arg)
   where reqHP == -1 || int.Parse(en.GetValue("currenthp").ToString()) == reqHP
   select new Vector2(float.Parse(en.GetValue("x").ToString()), float.Parse(en.GetValue("y").ToString()))).ToList();
 }

 private static Vector2 GetXY(string id_xy) {
  if (id_xy.Contains(",")) {
   var ss = id_xy.Split(',');
   return new Vector2(float.Parse(ss[0]), float.Parse(ss[1]));
  }
  if (id_xy.Any(c => c is (< '0' or > '9') and (< 'A' or > 'F') and (< 'a' or > 'f'))) return new Vector2();
  var e = BridgeFFXIV.GetIdEntity(id_xy);
  return new Vector2(float.Parse(e.GetValue("x").ToString()), float.Parse(e.GetValue("y").ToString()));
 }

 private static void ThreeBucket(string args) {
  foreach (var v in Players) {
   if (args != v.name) continue;
   lock (Threebuckets) {
    switch (Threebuckets.Count) {
     case 0:
      Threebuckets.Add(v);
      break;
     case 1:
      if (Threebuckets[0].storder > v.storder) Threebuckets.Insert(0, v);
      else Threebuckets.Add(v);
      break;
     case 2:
      if (Threebuckets[0].storder > v.storder) Threebuckets.Insert(0, v);
      else if (Threebuckets[1].storder > v.storder) Threebuckets.Insert(1, v);
      else Threebuckets.Add(v);
      if (Uthreebucket) {
       plug.InvokeNamedCallback("command", $"/mk attack1 <{Threebuckets[0].partyorder}>");
       plug.InvokeNamedCallback("command", $"/mk attack2 <{Threebuckets[1].partyorder}>");
       plug.InvokeNamedCallback("command", $"/mk attack3 <{Threebuckets[2].partyorder}>");
      }
      else {
       plug.InvokeNamedCallback("command", $"/e attack1 <{Threebuckets[0].partyorder}>");
       plug.InvokeNamedCallback("command", $"/e attack2 <{Threebuckets[1].partyorder}>");
       plug.InvokeNamedCallback("command", $"/e attack3 <{Threebuckets[2].partyorder}>");
      }
      Threebuckets.Clear();
      break;
    }
   }
   break;
  }
 }

 private static void InitParams() {
  Threebuckets.Clear();
  P43PlayerName.Clear();
  Zhuzis.Clear();
  Zhuzi2 = new Vector2();
  hs = "";
  ttsafe = "";
  ciyu = new Ciyu();
  p43dhCount = 0;
  p43dhDist = new List<DHP43>();
  Ubroadcast = GetScale("Ubroadcast") == "1";
  Uthreebucket = GetScale("Uthreebucket") == "1";
 }

 private static void GetPartyOrderInit(string args) {
  var ss = args.Split(',');
  var find = false;
  for (var i = 0; i < 8; i++) {
   var job = JobOrder[i];
   int num;
   if (MyJob == job) {
    num = 1;
    find = true;
   }
   else num = i + (find ? 1 : 2);
   var name = ss[num];
   foreach (var ser in Server) {
    if (!name.EndsWith(ser)) continue;
    name = name.Substring(0, name.Length - ser.Length);
    break;
   }
   Players[i] = new Player(job, num, name);
  }
  Log(string.Join("|", Players));
  InitParams();
  var sb = new StringBuilder("小队初始化完成。职业").Append(MyJob).Append("。");
  if (Ubroadcast) sb.Append("启用团队播报。");
  if (Uthreebucket) sb.Append("启用三连桶点名。");
  var sbs = sb.ToString();
  Log(sbs);
  PostTTS(sbs);
 }

 private static void Log(string message) {
  plug.InvokeNamedCallback("command", $"/e {message}");
 }

 private record DHP43(string name, float dist) {
  internal readonly string name = name;
  internal float dist = dist;
 }

 private record P41Info {
  internal readonly Vector2 after;
  internal readonly bool canES;
  internal readonly string desc, first;

  internal P41Info(string first, string desc, Vector2 after, bool canES) {
   this.first = first;
   this.canES = canES;
   this.desc = desc;
   if (canES) this.desc += "(提前安全)";
   this.after = after;
  }

  public override string ToString() {
   return desc;
  }
 }

 private record Ciyu {
  internal readonly List<string> dmg = new();
  internal int cs;
 }

 private record Zhuzi {
  internal readonly List<string> dmg = new();
  internal readonly int pos;
  internal readonly string zid;
  internal int cs;

  internal Zhuzi(string zid, int pos) {
   this.zid = zid;
   this.pos = pos;
  }
 }


 private static class StaticPlace {
  internal const string initPlace =
   "action:place\n" +
   "A:100,100 polar 18,-180\u00b0\n" +
   "B:100,100 polar 18,90\u00b0\n" +
   "C:100,100 polar 18,0\u00b0\n" +
   "D:100,100 polar 18,-90\u00b0\n" +
   "---\n" +
   "action:circle\n" +
   "waymarks:34\n" +
   "r:10\n" +
   "θ:-135\u00b0\n" +
   "center:100,100\n" +
   "---\n" +
   "action:place\n" +
   "1:100,100";
  internal const string clear2 = "action:place\n2:clear";
  internal const string clear3 = "action:place\n3:clear";
  internal const string clear4 = "action:place\n4:clear";
  internal const string fsN3 = "action:place\n3:100,90";
  internal const string fsN4 = "action:place\n4:100,90";
  internal const string fsS3 = "action:place\n3:100,110";
  internal const string fsS4 = "action:place\n4:100,110";
  internal const string fsW3 = "action:place\n3:90,100";
  internal const string fsW4 = "action:place\n4:90,100";
  internal const string fsE3 = "action:place\n3:110,100";
  internal const string fsE4 = "action:place\n4:110,100";
  internal const string safeA =
   "action:circle\nwaymarks:123\nr:2\ncenter:100,82\nθ:0\u00b0\n---\naction:place\n4:100,92.5";
  internal const string safeB =
   "action:circle\nwaymarks:123\nr:2\ncenter:118,100\nθ:-90\u00b0\n---\naction:place\n4:107.5,100";
  internal const string safeC =
   "action:circle\nwaymarks:123\nr:2\ncenter:100,118\n---\naction:place\n4:100,107.5";
  internal const string safeD =
   "action:circle\nwaymarks:123\nr:2\ncenter:82,100\nθ:90\u00b0\n---\naction:place\n4:92.5,100";
  internal const string jzhA = "action:circle\nwaymarks:BCD14\nr:2\ncenter:100,82";
  internal const string jzh2 = "action:circle\nwaymarks:BCD14\nr:2\ncenter:88,88";
  internal const string jzh3 = "action:circle\nwaymarks:BCD14\nr:2\ncenter:93,93";
  internal const string p42place2 = "action:place\n2:100,100 polar 18,-135\u00b0";
  internal const string p42place2rf = "action:place\n2:100,112";
  internal const string startp43 =
   "action:arc\nwaymarks:AB1234\nr:18\ncenter:100,100\nθ:-135\u00b0\ndθ:90\u00b0";
  internal static readonly string[,] ygjdPlace4 = {
   { "action:place\n4:102.1,108.6", "action:place\n4:97.9,108.6" },
   { "action:place\n4:91.4,102.1", "action:place\n4:91.4,97.9" },
   { "action:place\n4:97.9,91.4", "action:place\n4:102.1,91.4" },
   { "action:place\n4:108.6,97.9", "action:place\n4:108.6,102.1" }
  };
 }


 private readonly record struct Player {
  internal readonly string job, name;
  internal readonly int partyorder, storder;

  internal Player(string job, int partyorder, string name) {
   this.job = job;
   if (job == MyJob) MyName = name;
   this.partyorder = partyorder;
   var o = 0;
   for (var i = 0; i < TBJobOrder.Length; i++) {
    if (job != TBJobOrder[i]) continue;
    o = i;
    break;
   }
   storder = o;
   this.name = name;
  }

  public override string ToString() {
   return $"Job:{job}, PartyOrder:{partyorder}, Name:{name}";
  }
 }
}