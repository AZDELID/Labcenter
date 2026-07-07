using System.Drawing;
using System.Windows.Forms;

namespace labcenter.Views
{
    public static class MovableWindowBehavior
    {
        public static void Attach(Form form, Control dragSurface)
        {
            bool dragging = false;
            Point dragCursorPoint = Point.Empty;
            Point dragFormPoint = Point.Empty;

            dragSurface.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    dragging = true;
                    dragCursorPoint = Cursor.Position;
                    dragFormPoint = form.Location;
                }
            };

            dragSurface.MouseMove += (s, e) =>
            {
                if (dragging)
                {
                    Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                    form.Location = Point.Add(dragFormPoint, new Size(dif));
                }
            };

            dragSurface.MouseUp += (s, e) => { dragging = false; };
        }
    }
}
