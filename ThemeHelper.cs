using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lexicon.Helpers
{
    public static class ThemeHelper
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        /// <summary>
        /// Возвращает базовую цветовое палитру приложения (Фон формы, Фон контролов, Цвет текста)
        /// </summary>
        public static (Color BgColor, Color ControlBgColor, Color TextColor) GetPalette(bool isDark)
        {
            Color bgColor = isDark ? Color.FromArgb(32, 32, 32) : Color.FromArgb(245, 245, 245);
            Color controlBgColor = isDark ? Color.FromArgb(50, 50, 50) : Color.FromArgb(225, 225, 225);
            Color textColor = isDark ? Color.FromArgb(240, 240, 240) : Color.FromArgb(20, 20, 20);

            return (bgColor, controlBgColor, textColor);
        }

        /// <summary>
        /// Скругляет углы любого элемента управления
        /// </summary>
        public static void MakeControlRounded(Control ctrl, int radius)
        {
            if (ctrl == null || ctrl.Width <= 0 || ctrl.Height <= 0) return;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.StartFigure();
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(ctrl.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(ctrl.Width - radius, ctrl.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, ctrl.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();

                Region oldRegion = ctrl.Region;
                ctrl.Region = new Region(path);
                oldRegion?.Dispose();
            }
        }

        /// <summary>
        /// Применяет единый цвет и радиус скругления для группы кнопок
        /// </summary>
        public static void StyleButtons(IEnumerable<Button> buttons, Color bg, Color text, int radius = 10)
        {
            foreach (Button btn in buttons)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = bg;
                btn.ForeColor = text;
                MakeControlRounded(btn, radius);
            }
        }

        public static void ApplyTitleBarTheme(Form form, bool isDark)
        {
            if (form == null || form.IsDisposed) return;

            if (!form.IsHandleCreated)
            {
                EventHandler handleCreatedHandler = null!;
                handleCreatedHandler = (s, e) =>
                {
                    form.HandleCreated -= handleCreatedHandler;
                    ApplyTitleBarTheme(form, isDark);
                };
                form.HandleCreated += handleCreatedHandler;
                return;
            }

            int useDarkMode = isDark ? 1 : 0;
            DwmSetWindowAttribute(form.Handle, 20, ref useDarkMode, sizeof(int));
        }

        public static void ApplyThemeToForm(Form form, bool isDark)
        {
            ApplyTitleBarTheme(form, isDark);

            var (bgColor, controlBg, textColor) = GetPalette(isDark);
            Color borderColor = isDark ? Color.FromArgb(60, 60, 60) : Color.FromArgb(200, 200, 200);

            form.BackColor = bgColor;
            form.ForeColor = textColor;

            ApplyThemeToControls(form.Controls, isDark, bgColor, controlBg, textColor, borderColor);
        }

        private static void ApplyThemeToControls(Control.ControlCollection controls, bool isDark, Color bgColor, Color controlBg, Color textColor, Color borderColor)
        {
            foreach (Control ctrl in controls)
            {
                ctrl.ForeColor = textColor;

                if (ctrl is Panel || ctrl is GroupBox || ctrl is FlowLayoutPanel || ctrl is TableLayoutPanel)
                {
                    ctrl.BackColor = bgColor;
                }
                else if (ctrl is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.BorderColor = borderColor;
                    btn.BackColor = controlBg;
                }
                else if (ctrl is CheckBox || ctrl is RadioButton || ctrl is Label)
                {
                    ctrl.BackColor = Color.Transparent;
                }
                else if (ctrl is TextBox || ctrl is NumericUpDown)
                {
                    ctrl.BackColor = controlBg;
                }

                if (ctrl.HasChildren)
                {
                    ApplyThemeToControls(ctrl.Controls, isDark, bgColor, controlBg, textColor, borderColor);
                }
            }
        }
    }
}