using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AnnoDesigner
{
    public static class DataIO
    {
        public static void SaveToFile<T>(T obj, string filename)
        {
            var stream = File.Open(filename, FileMode.Create);
            var serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(stream, obj);
            stream.Close();
        }

        public static T LoadFromFile<T>(string filename)
        {
            T obj;
            LoadFromFile(out obj, filename);
            return obj;
        }

        public static void LoadFromFile<T>(out T obj, string filename)
        {
            var stream = File.Open(filename, FileMode.Open);
            var serializer = new DataContractJsonSerializer(typeof(T));
            obj = (T)serializer.ReadObject(stream);
            stream.Close();
        }

        public static void RenderToFile(FrameworkElement controlToRender, string filename)
        {
            // render control
            const int dpi = 96;
            var rtb = new RenderTargetBitmap((int)controlToRender.ActualWidth, (int)controlToRender.ActualHeight, dpi, dpi, PixelFormats.Default);
            rtb.Render(controlToRender);
            // encode to png
            var png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            // save file
            Stream file = File.Open(filename, FileMode.Create);
            png.Save(file);
            file.Close();
        }
    }
}