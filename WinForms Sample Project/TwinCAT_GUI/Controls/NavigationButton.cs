using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace TwinCAT_GUI.Controls
{
    public class NavigationButton : TcForms.TcButtonBase
    {
        private bool isActive;
        private Color activeColor;
        private Color defaultBackColor;
        private Color defaultMouseOverBackColor;
        private Color defaultMouseDownBackColor;

        [Category("Appearance")]
        public Color ActiveColor
        {
            get { return activeColor; }
            set
            {
                activeColor = value;
                Invalidate();
            }
        }

        //Constructor
        public NavigationButton()
        {
            ActiveColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            //Color lineColor = stState.bStatus ? onColor : offColor;
            Color lineColor = Color.Blue;
            //int lineY = this.Height - 6;
            //int xOffset = this.CornerRadius > 0 ? this.CornerRadius + 2 : 5;
            if (isActive)
            {
                using (Pen pen = new Pen(lineColor, 4))
                {
                    pevent.Graphics.DrawLine(pen, 2, 0, 2, this.Height);
                }
            }
        }

        public void Activate()
        {
            defaultBackColor = BackColor;
            defaultMouseOverBackColor = FlatAppearance.MouseOverBackColor;
            defaultMouseDownBackColor = FlatAppearance.MouseDownBackColor;
            BackColor = ActiveColor;
            FlatAppearance.MouseOverBackColor = ActiveColor;
            FlatAppearance.MouseDownBackColor = ActiveColor;
            isActive = true;
            Invalidate();
        }

        public void Deactivate()
        {
            BackColor = defaultBackColor;
            FlatAppearance.MouseOverBackColor = defaultMouseOverBackColor;
            FlatAppearance.MouseDownBackColor = defaultMouseDownBackColor;
            isActive = false;
            Invalidate();
        }
    }
}
