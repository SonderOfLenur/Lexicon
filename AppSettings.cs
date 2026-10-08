using System;
using System.Drawing;
using System.Xml.Serialization;

namespace Lexicon
{
    public class AppSettings
    {
        public bool IsDarkTheme { get; set; } = true;
        public bool IsAutoSaveEnabled { get; set; } = false;
        public string FontFamily { get; set; } = "Consolas";
        public float FontSize { get; set; } = 12f;

        public FontStyle FontStyle { get; set; } = FontStyle.Regular;
        public bool WordWrap { get; set; } = true;
        public int WindowWidth { get; set; } = 1200;
        public int WindowHeight { get; set; } = 800;
        public string LastOpenedFile { get; set; } = string.Empty;
    }
}