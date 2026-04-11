using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Converter;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static async Task Main(string[] args)
    {
        var titulo = string.Empty;
        var pasta = Environment.CurrentDirectory + "\\Mp3";

        if (!Directory.Exists(pasta))
        {
            Directory.CreateDirectory(pasta);
        }

        Console.WriteLine("Digite o título do video: ");

        titulo = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrEmpty(titulo))
        {
            titulo = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".mp3";
        }
        else
        {
            titulo += ".mp3";
        }   

        Console.Write("Digite a URL do vídeo: ");
        var url = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(url) || string.IsNullOrEmpty(url))
        {
            Console.WriteLine("Url Invalida!");
            Environment.Exit(0);
            //return new Exception().Message("Url Invalida!");
        }

        var youtube = new YoutubeClient();

        try
        {
            Console.WriteLine("Obtendo informações do vídeo...");

            //var pasta = Environment.CurrentDirectory + "Musicas";

            //var video = await youtube.Videos.GetAsync(url);

            // Remove caracteres inválidos do nome do arquivo
            //var fileName = SanitizeFileName(video.Title) + ".mp3";

            var fileName = titulo;

            Console.WriteLine($"Baixando áudio: {fileName}");

            await youtube.Videos.DownloadAsync(
                url,
                fileName,
                builder => builder.SetPreset(ConversionPreset.UltraFast)
            );

            Console.WriteLine("Download concluído!");

            if (!Directory.Exists(Environment.CurrentDirectory ))
            {
                Directory.CreateDirectory(pasta);            }

            //Process.Start("explorer.exe", fileName);

            Process.Start("explorer.exe", pasta);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
        }
    }

    static string SanitizeFileName(string name)
    {
        foreach (var c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}