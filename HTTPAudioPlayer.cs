using NAudio.Wave;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nagomi.HttpAudioPlayer;

public class HTTPAudioPlayer
{
    static async Task Main()
    {
        string audioUrl = "http://example.com/youraudiofile.mp3"; // 音频文件的URL
        string localFilePath = @"C:\ProgramFiles\Nagomi\res\audiofile.mp3"; // 保存到本地的文件路径

        // 下载音频文件
        await DownloadAudioAsync(audioUrl, localFilePath);

        // 播放音频文件
        PlayAudio(localFilePath);
    }

    static async Task DownloadAudioAsync(string url, string filePath)
    {
        using (var httpClient = new HttpClient())
        {
            try
            {
                // 获取音频流
                using (var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        // 将内容复制到文件
                        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            await stream.CopyToAsync(fileStream);
                        }
                    }
                }
                Console.WriteLine("音频文件下载完成，保存路径：" + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("下载音频文件时出错: " + ex.Message);
            }
        }
    }

    static void PlayAudio(string filePath)
    {
        using (var audioFileReader = new AudioFileReader(filePath))
        using (var waveOut = new WaveOutEvent())
        {
            waveOut.Init(audioFileReader);
            waveOut.Play();

            Console.WriteLine("音频正在播放，按任意键退出...");
            Console.ReadKey();

            waveOut.Stop(); // 停止播放
        }
    }
}