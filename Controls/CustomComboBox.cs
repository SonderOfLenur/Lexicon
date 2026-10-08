using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Lexicon.Views.Controls
{
    public class CustomComboBox : ComboBox
    {
        public Color ArrowColor { get; set; } = Color.White;
        public Color BorderColor { get; set; } = Color.FromArgb(50, 50, 50);

        public CustomComboBox()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.ItemHeight = 18;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            object item = this.Items[e.Index];
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            Color bg = isSelected ? Color.FromArgb(70, 70, 70) : this.BackColor;

            using (SolidBrush bgBrush = new SolidBrush(bg))
            using (SolidBrush textBrush = new SolidBrush(this.ForeColor))
            {
                // Закрашиваем ТОЛЬКО выделенную область e.Bounds (которая передана без зоны стрелки)
                e.Graphics.FillRectangle(bgBrush, e.Bounds);

                using (StringFormat sf = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Near,
                    FormatFlags = StringFormatFlags.NoWrap
                })
                {
                    if (item is Color colorItem)
                    {
                        string colorName = colorItem.IsKnownColor || colorItem.IsNamedColor
                            ? colorItem.Name
                            : $"#{colorItem.R:X2}{colorItem.G:X2}{colorItem.B:X2}";

                        int boxSize = 10;
                        int boxY = e.Bounds.Y + (e.Bounds.Height - boxSize) / 2;

                        using (SolidBrush colorBoxBrush = new SolidBrush(colorItem))
                        using (Pen boxPen = new Pen(Color.FromArgb(120, 120, 120)))
                        {
                            e.Graphics.FillRectangle(colorBoxBrush, e.Bounds.X + 3, boxY, boxSize, boxSize);
                            e.Graphics.DrawRectangle(boxPen, e.Bounds.X + 3, boxY, boxSize, boxSize);
                        }

                        Rectangle colorTextRect = new Rectangle(
                            e.Bounds.X + boxSize + 6,
                            e.Bounds.Y,
                            e.Bounds.Width - (boxSize + 8),
                            e.Bounds.Height
                        );
                        e.Graphics.DrawString(colorName, e.Font ?? this.Font, textBrush, colorTextRect, sf);
                    }
                    else
                    {
                        string text = this.GetItemText(item);
                        Rectangle textRect = new Rectangle(
                            e.Bounds.X + 3,
                            e.Bounds.Y,
                            e.Bounds.Width - 4,
                            e.Bounds.Height
                        );
                        e.Graphics.DrawString(text, e.Font ?? this.Font, textBrush, textRect, sf);
                    }
                }
            }

            base.OnDrawItem(e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0F) // WM_PAINT
            {
                using (Graphics g = this.CreateGraphics())
                {
                    int arrowAreaWidth = 14;
                    int contentWidth = this.Width - arrowAreaWidth;

                    // 1. Закрашиваем весь элемент нейтральным базовым фоном
                    using (Brush bgBrush = new SolidBrush(this.BackColor))
                    {
                        g.FillRectangle(bgBrush, 0, 0, this.Width, this.Height);
                    }

                    // 2. Отрисовываем выбранный контент СТРОГО до разделителя (не заходя на стрелку)
                    if (this.SelectedIndex >= 0)
                    {
                        DrawItemEventArgs args = new DrawItemEventArgs(
                            g,
                            this.Font,
                            new Rectangle(0, 0, contentWidth, this.Height),
                            this.SelectedIndex,
                            DrawItemState.None,
                            this.ForeColor,
                            this.BackColor
                        );
                        OnDrawItem(args);
                    }

                    // 3. Рисуем вертикальную линию-разделитель между контентом и кнопкой стрелки
                    using (Pen sepPen = new Pen(this.BorderColor, 1))
                    {
                        g.DrawLine(sepPen, contentWidth, 1, contentWidth, this.Height - 2);
                    }

                    // 4. Отрисовываем внешнюю контурную рамку
                    using (Pen innerPen = new Pen(this.BorderColor, 1))
                    {
                        g.DrawRectangle(innerPen, 0, 0, this.Width - 1, this.Height - 1);
                    }

                    // 5. Рисуем стрелку точно по центру правой зоны
                    int centerX = contentWidth + (arrowAreaWidth / 2);
                    int centerY = this.Height / 2;

                    Point[] arrowPoints = new Point[]
                    {
                        new Point(centerX - 3, centerY - 1),
                        new Point(centerX + 3, centerY - 1),
                        new Point(centerX, centerY + 2)
                    };

                    using (Brush arrowBrush = new SolidBrush(this.ArrowColor))
                    {
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.FillPolygon(arrowBrush, arrowPoints);
                    }
                }
            }
        }
    }
}