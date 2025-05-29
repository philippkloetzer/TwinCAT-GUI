using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace TwinCAT_GUI.Controls
{
    public class MenuButton : TcForms.TcButtonBase
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.None;

            int xOffset = this.CornerRadius > 0 ? this.CornerRadius + 2 : 5;
            int lineY = this.Height / 2 + this.BorderSize;
            int dist = 12;

            using (Pen pen = new Pen(Color.Gray, 3))
            {
                e.Graphics.DrawLine(pen, xOffset, lineY - dist, this.Width - xOffset, lineY - dist);
                e.Graphics.DrawLine(pen, xOffset, lineY, this.Width - xOffset, lineY);
                e.Graphics.DrawLine(pen, xOffset, lineY + dist, this.Width - xOffset, lineY + dist);
            }


        }
    }
}
