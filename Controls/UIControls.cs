using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CSharpDesctop.Controls
{
    public class RoundedButton : Button
    {
        private int _cornerRadius = 10;
        private Color _normalColor;
        private Color _hoverColor;
        private Color _pressedColor;

        
        private Color _currentColor;
        private Color _targetColor;
        private readonly System.Windows.Forms.Timer _animTimer;
        private const int AnimSteps = 12;
        private int _animStep = 0;

        
        private bool _showShadow = true;

        [System.ComponentModel.Category("Appearance")]
        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; UpdateRegion(); Invalidate(); }
        }

        [System.ComponentModel.Category("Appearance")]
        public bool ShowShadow
        {
            get => _showShadow;
            set { _showShadow = value; Invalidate(); }
        }

        public RoundedButton()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer, true);

            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            BackColor = Color.FromArgb(45, 125, 210);
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            _normalColor = BackColor;
            RecalculateColors();
            _currentColor = _normalColor;
            _targetColor = _normalColor;

            
            _animTimer = new System.Windows.Forms.Timer { Interval = 16 };
            _animTimer.Tick += AnimTimer_Tick;

            MouseEnter += (s, e) => StartAnim(_hoverColor);
            MouseLeave += (s, e) => StartAnim(_normalColor);
            MouseDown += (s, e) => StartAnim(_pressedColor);
            
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            _animStep++;
            float t = EaseInOut((float)_animStep / AnimSteps);

            _currentColor = Lerp(_currentColor, _targetColor, t);

            if (_animStep >= AnimSteps)
            {
                _currentColor = _targetColor;
                _animTimer.Stop();
            }
            Invalidate();
        }

        private void StartAnim(Color target)
        {
            _targetColor = target;
            _animStep = 0;
            if (!_animTimer.Enabled) _animTimer.Start();
        }

        private static float EaseInOut(float t) => t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;

        private static Color Lerp(Color a, Color b, float t)
        {
            t = Math.Max(0, Math.Min(1, t));
            return Color.FromArgb(
                (int)(a.A + (b.A - a.A) * t),
                (int)(a.R + (b.R - a.R) * t),
                (int)(a.G + (b.G - a.G) * t),
                (int)(a.B + (b.B - a.B) * t));
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            _normalColor = BackColor;
            RecalculateColors();
            _currentColor = _normalColor;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion();
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            Invalidate();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (Parent != null)
                Parent.BackColorChanged += (s, ev) => Invalidate();
        }

        private void RecalculateColors()
        {
            _hoverColor = Lighten(_normalColor, 0.12f);
            _pressedColor = Darken(_normalColor, 0.15f);
        }

        private static Color Lighten(Color c, float amount) =>
            Color.FromArgb(c.A,
                Math.Min(255, (int)(c.R + 255 * amount)),
                Math.Min(255, (int)(c.G + 255 * amount)),
                Math.Min(255, (int)(c.B + 255 * amount)));

        private static Color Darken(Color c, float amount) =>
            Color.FromArgb(c.A,
                Math.Max(0, (int)(c.R - 255 * amount)),
                Math.Max(0, (int)(c.G - 255 * amount)),
                Math.Max(0, (int)(c.B - 255 * amount)));

        private void UpdateRegion()
        {
            if (Width <= 0 || Height <= 0) return;
            using (var path = GetRoundedRect(new Rectangle(0, 0, Width, Height), _cornerRadius))
                Region = new Region(path);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Color parentColor = Parent?.BackColor ?? FindForm()?.BackColor ?? SystemColors.Control;
            pevent.Graphics.Clear(parentColor);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle rect = new Rectangle(0, 0, Width, Height);

            
            if (_showShadow)
            {
                Rectangle shadowRect = new Rectangle(2, 3, Width - 3, Height - 3);
                using (var shadowPath = GetRoundedRect(shadowRect, _cornerRadius))
                using (var shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
                    g.FillPath(shadowBrush, shadowPath);
            }

            using (var path = GetRoundedRect(rect, _cornerRadius))
            using (var brush = new SolidBrush(_currentColor))
                g.FillPath(brush, path);

            
            Rectangle highlightRect = new Rectangle(1, 1, Width - 2, Height / 2);
            using (var highlightPath = GetRoundedRect(highlightRect, _cornerRadius))
            using (var highlightBrush = new LinearGradientBrush(
                highlightRect,
                Color.FromArgb(40, 255, 255, 255),
                Color.FromArgb(0, 255, 255, 255),
                LinearGradientMode.Vertical))
                g.FillPath(highlightBrush, highlightPath);

            TextRenderer.DrawText(g, Text, Font, rect, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        private static GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            Rectangle r = new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
            int d = Math.Min(radius * 2, Math.Min(r.Width, r.Height));
            var path = new GraphicsPath();

            if (d <= 0) { path.AddRectangle(r); return path; }

            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _animTimer?.Dispose();
            base.Dispose(disposing);
        }
    }

    public class RoundedPanel : Panel
    {
        private int _cornerRadius = 14;
        private Color _borderColor = Color.FromArgb(225, 230, 235);
        private int _borderThickness = 1;
        private bool _hoverHighlight = false;

        
        private float _hoverAlpha = 0f;
        private readonly System.Windows.Forms.Timer _hoverTimer;
        private bool _isHovered = false;

        [System.ComponentModel.Category("Appearance")]
        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; Invalidate(); }
        }

        [System.ComponentModel.Category("Appearance")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [System.ComponentModel.Category("Appearance")]
        public int BorderThickness
        {
            get => _borderThickness;
            set { _borderThickness = value; Invalidate(); }
        }

        [System.ComponentModel.Category("Appearance")]
        public bool HoverHighlight
        {
            get => _hoverHighlight;
            set { _hoverHighlight = value; }
        }

        public RoundedPanel()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.White;

            _hoverTimer = new System.Windows.Forms.Timer { Interval = 16 };
            _hoverTimer.Tick += (s, e) =>
            {
                float target = _isHovered ? 1f : 0f;
                _hoverAlpha += (_isHovered ? 0.1f : -0.1f);
                _hoverAlpha = Math.Max(0f, Math.Min(1f, _hoverAlpha));
                if (_hoverAlpha == target) _hoverTimer.Stop();
                Invalidate();
            };

            MouseEnter += (s, e) => { if (_hoverHighlight) { _isHovered = true; _hoverTimer.Start(); } };
            MouseLeave += (s, e) => { if (_hoverHighlight) { _isHovered = false; _hoverTimer.Start(); } };
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            Color parentColor = Parent?.BackColor ?? FindForm()?.BackColor ?? SystemColors.Control;
            pevent.Graphics.Clear(parentColor);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle rect = new Rectangle(0, 0, Width, Height);

            using (var path = GetRoundedRect(rect, _cornerRadius))
            {
                
                using (var brush = new SolidBrush(BackColor))
                    g.FillPath(brush, path);

                
                if (_hoverHighlight && _hoverAlpha > 0)
                {
                    int alpha = (int)(18 * _hoverAlpha);
                    using (var hoverBrush = new SolidBrush(Color.FromArgb(alpha, 45, 125, 210)))
                        g.FillPath(hoverBrush, path);
                }

                
                if (_borderThickness > 0)
                {
                    Color bc = _hoverHighlight && _hoverAlpha > 0
                        ? Color.FromArgb(
                            (int)(_borderColor.R + (45 - _borderColor.R) * _hoverAlpha * 0.5f),
                            (int)(_borderColor.G + (125 - _borderColor.G) * _hoverAlpha * 0.5f),
                            (int)(_borderColor.B + (210 - _borderColor.B) * _hoverAlpha * 0.5f))
                        : _borderColor;
                    using (var pen = new Pen(bc, _borderThickness))
                        g.DrawPath(pen, path);
                }
            }
        }

        private static GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            Rectangle r = new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
            int d = Math.Min(radius * 2, Math.Min(r.Width, r.Height));
            var path = new GraphicsPath();

            if (d <= 0) { path.AddRectangle(r); return path; }

            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _hoverTimer?.Dispose();
            base.Dispose(disposing);
        }
    }

    public static class InputStyler
    {
        public static RoundedPanel WrapTextBox(TextBox textBox, int cornerRadius = 8)
        {
            var wrapper = new RoundedPanel
            {
                CornerRadius = cornerRadius,
                BackColor = Color.White,
                BorderColor = Color.FromArgb(210, 215, 220),
                BorderThickness = 1,
                Size = new Size(textBox.Width, textBox.Height + 8),
                Location = textBox.Location
            };

            textBox.BorderStyle = BorderStyle.None;
            textBox.Location = new Point(8, 4);
            textBox.Width = wrapper.Width - 16;
            wrapper.Controls.Add(textBox);

            
            var focusTimer = new System.Windows.Forms.Timer { Interval = 16 };
            bool focused = false;
            float alpha = 0f;

            focusTimer.Tick += (s, e) =>
            {
                alpha += focused ? 0.1f : -0.1f;
                alpha = Math.Max(0f, Math.Min(1f, alpha));

                int r2 = (int)(210 + (45 - 210) * alpha);
                int g2 = (int)(215 + (125 - 215) * alpha);
                int b2 = (int)(220 + (210 - 220) * alpha);
                wrapper.BorderColor = Color.FromArgb(r2, g2, b2);
                wrapper.Invalidate();

                if (alpha == 0f || alpha == 1f) focusTimer.Stop();
            };

            textBox.Enter += (s, e) => { focused = true; focusTimer.Start(); };
            textBox.Leave += (s, e) => { focused = false; focusTimer.Start(); };

            return wrapper;
        }
    }
}