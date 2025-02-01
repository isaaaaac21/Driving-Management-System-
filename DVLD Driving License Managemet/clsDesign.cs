using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Data;

namespace DVLD_Driving_License_Managemet
{
    public class clsDesign
    {
        public static void MakeRoundedBorders(Control control, int cornerRadius)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control), "The control cannot be null.");

            if (cornerRadius < 1)
                throw new ArgumentOutOfRangeException(nameof(cornerRadius), "The corner radius must be greater than 0.");

            // Create a new GraphicsPath object with rounded corners
            GraphicsPath path = new GraphicsPath();
            int width = control.Width;
            int height = control.Height;

            path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90); // Top-left corner
            path.AddArc(width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90); // Top-right corner
            path.AddArc(width - cornerRadius, height - cornerRadius, cornerRadius, cornerRadius, 0, 90); // Bottom-right corner
            path.AddArc(0, height - cornerRadius, cornerRadius, cornerRadius, 90, 90); // Bottom-left corner
            path.CloseFigure();

            // Apply the rounded path to the control's region
            control.Region = new Region(path);
        }


        public static void ApplyRoundedCorners(Form form, int cornerRadius = 30, int borderWidth = 1)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form), "The form cannot be null.");

            if (cornerRadius < 1)
                throw new ArgumentOutOfRangeException(nameof(cornerRadius), "The corner radius must be greater than 0.");

            if (borderWidth < 1)
                throw new ArgumentOutOfRangeException(nameof(borderWidth), "The border width must be greater than 0.");

            // Hook into the form's Paint event to draw custom graphics
            form.Paint += (sender, e) =>
            {
                // Enable anti-aliasing for smooth edges
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Create a GraphicsPath for rounded corners
                GraphicsPath path = new GraphicsPath();
                int width = form.Width;
                int height = form.Height;

                path.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90); // Top-left corner
                path.AddArc(width - cornerRadius - 1, 0, cornerRadius, cornerRadius, 270, 90); // Top-right corner
                path.AddArc(width - cornerRadius - 1, height - cornerRadius - 1, cornerRadius, cornerRadius, 0, 90); // Bottom-right corner
                path.AddArc(0, height - cornerRadius - 1, cornerRadius, cornerRadius, 90, 90); // Bottom-left corner
                path.CloseAllFigures();

                // Apply the rounded path to the form's region
                form.Region = new Region(path);

                // Optional: Fill the background to avoid artifacts
                using (SolidBrush backgroundBrush = new SolidBrush(form.BackColor))
                {
                    e.Graphics.FillPath(backgroundBrush, path);
                }

                // Draw the border
                using (Pen borderPen = new Pen(form.BackColor, borderWidth))
                {
                    e.Graphics.DrawPath(borderPen, path);
                }

                // Add custom drawing (example: horizontal line)
                using (Pen customPen = new Pen(form.BackColor, 3))
                {
                    customPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    customPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    e.Graphics.DrawLine(customPen, 140, 150, 520, 150);
                }
            };

            // Optional: Handle resizing to ensure proper redraw
            form.Resize += (sender, e) => form.Invalidate();

            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.CenterScreen;
        }





        static public void _ShowErrorMessage(string Message)
        {
            MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        static public void _ShowSuccessMessage(string Message)
        {
            MessageBox.Show(Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        static public bool ConfirmMessage(string Message)
        {
            if (MessageBox.Show(Message, "Wait", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                return true; 
            }
            return false; 
        }


        static public void FillComboBox(ref ComboBox cb, DataTable Dt)
        {
            foreach(DataColumn clmn in Dt.Columns)
            {
                cb.Items.Add(clmn.ColumnName); 
            }
            cb.SelectedIndex = 0; 
        }
    }







}
