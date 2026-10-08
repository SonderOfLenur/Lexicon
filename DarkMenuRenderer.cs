using System.Drawing;
using System.Windows.Forms;

namespace Lexicon.Views.Helpers
{
    public class DarkColorTable : ProfessionalColorTable
    {
        private readonly Color _bgColor = Color.FromArgb(32, 32, 32);
        private readonly Color _hoverColor = Color.FromArgb(60, 60, 60);
        private readonly Color _borderColor = Color.FromArgb(50, 50, 50);

        // Фон выпадающего списка
        public override Color ToolStripDropDownBackground => _bgColor;

        // Убираем светлую боковую полосу слева (зону иконок)
        public override Color ImageMarginGradientBegin => _bgColor;
        public override Color ImageMarginGradientMiddle => _bgColor;
        public override Color ImageMarginGradientEnd => _bgColor;

        // Границы выпадающего меню и пунктов
        public override Color MenuBorder => _borderColor;
        public override Color MenuItemBorder => Color.Transparent;

        // Цвета подстветки при наведении
        public override Color MenuItemSelected => _hoverColor;
        public override Color MenuItemSelectedGradientBegin => _hoverColor;
        public override Color MenuItemSelectedGradientEnd => _hoverColor;
        public override Color MenuItemPressedGradientBegin => _hoverColor;
        public override Color MenuItemPressedGradientEnd => _hoverColor;
    }

    public class DarkMenuRenderer : ToolStripProfessionalRenderer
    {
        public DarkMenuRenderer() : base(new DarkColorTable()) { }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            // Убираем системную белую рамку вокруг выпадающего списка
            if (e.ToolStrip is ToolStripDropDown)
            {
                using (Pen pen = new Pen(Color.FromArgb(50, 50, 50)))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
                }
            }
            else
            {
                base.OnRenderToolStripBorder(e);
            }
        }
    }
}