using AEAssist.Helper;
using Nagomi.PCT.Settings;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Nagomi.PCT;
using System.IO;
using System.Net.Http;

#nullable enable
namespace NagomiAudioPlayer
{
  public class AudioPlayer
{
     private 
  #nullable disable
  IWavePlayer waveOutDevice;
  private WaveStream audioStream;
  private readonly object _lockObject = new object();
  private static readonly Dictionary<string, string[]> audioFiles = new Dictionary<string, string[]>()
  {
    {
      "usagi",
      new string[7]
      {
        "usagi_bru.mp3",
        "usagi_ha.mp3",
        "usagi_ha^.mp3",
        "usagi_haa.mp3",
        "usagi_ura.mp3",
        "usagi_yaha.mp3",
        "usagi_yahha.mp3"
      }
    },
    {
      "behind",
      new string[2]{ "behind1.mp3", "behind2.mp3" }
    },
    {
      "flanking",
      new string[2]{ "flanking1.mp3", "flanking2.mp3" }
    },
    {
      "heartsteel",
      new string[1]{ "heartsteel.mp3" }
    }
  };

  public static float Volume
  {
    get => PCTSettings.Instance.Volume;
    set
    {
      PCTSettings.Instance.Volume = (double) value >= 0.0 && (double) value <= 1.0 ? value : throw new ArgumentOutOfRangeException(nameof (value), "音量必须在 0.0 和 1.0 之间");
    }
  }

  private string getResName(string key)
  {
    string[] strArray;
    if (AudioPlayer.audioFiles.TryGetValue(key, out strArray))
    {
      Random random = new Random();
      return "Ken.res." + strArray[random.Next(strArray.Length)];
    }
    LogHelper.Info("检查字典------ '" + key + "'");
    return (string) null;
  }

  public async Task PlayMp3Async(string key, float volume)
  {
    lock (this._lockObject)
    {
      this.DisposeResources();
      string resourcePath = this.getResName(key);
      if (resourcePath == null)
        return;
      Assembly assembly = Assembly.GetExecutingAssembly();
      try
      {
        using (Stream resourceStream = assembly.GetManifestResourceStream(resourcePath))
        {
          if (resourceStream != null)
          {
            using (this.audioStream = (WaveStream) new Mp3FileReader(resourceStream))
            {
              this.waveOutDevice = (IWavePlayer) new WaveOutEvent();
              this.waveOutDevice.Init((IWaveProvider) this.audioStream);
              this.waveOutDevice.Play();
              this.WaitForPlaybackToEnd().Wait();
            }
          }
          else
            LogHelper.Info("没找到 '" + resourcePath + "'");
        }
      }
      catch (Exception ex)
      {
        LogHelper.Info("播放中错误: " + ex.Message);
      }
      finally
      {
        this.DisposeResources();
      }
      resourcePath = (string) null;
      assembly = (Assembly) null;
    }
  }

  private Task WaitForPlaybackToEnd()
  {
    TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
    this.waveOutDevice.PlaybackStopped += (EventHandler<StoppedEventArgs>) ((sender, args) => tcs.SetResult(true));
    return (Task) tcs.Task;
  }

  private void DisposeResources()
  {
    ((Stream) this.audioStream)?.Dispose();
    ((IDisposable) this.waveOutDevice)?.Dispose();
    this.waveOutDevice = (IWavePlayer) null;
    this.audioStream = (WaveStream) null;
  }
}
}
