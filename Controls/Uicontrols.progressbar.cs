using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CSharpDesctop.Controls
{
    
    
    
    public class RoundedProgressBar : Control
    {
        private int _value = 0;
        private int _maximum = 100;
        private int _cornerRadius = 8;
        private Color _trackColor = Color.FromArgb(235, 238, 242);
        private Color _fillColorStart = Color.FromArgb(45, 125, 210);
        private Color _fillColorEnd = Color.FromArgb(86, 180, 233);
        private bool _showPercentText = true;

        
        private float _animatedValue = 0f;
        private readonly System.Windows.Forms.Timer _animTimer;
        private const float AnimSpeed = 0.06f; 

        
        private float _shimmerPos = -0.3f;
        private readonly System.Windows.Forms.Timer _shimmerTimer;

        [System.ComponentModel.Category("Behavior")]
        public int Maximum
        {
            get => _maximum;
            set { _maximum = Math.Max(1, value); Invalidate(); }
        }

        [System.ComponentModel.Category("Behavior")]
        public int Value
        {
            get => _value;
            set
            {
                _value = Math.Max(0, Math.Min(_maximum, value));
                
                if (!_animTimer.Enabled) _animTimer.Start();
            }
        }

        [System.ComponentModel.Category("Appearance")]
        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; Invalidate(); }
        }

        [System.ComponentModel.Category("Appearance")]
        public Color TrackColor
        {
            get => _trackColor;
            set { _trackColor = value; Invalidate(); }
        }

        [System.ComponentModel.Category("Appearance")]
        public Color FillColorStart
        {
            get => _fillColorStart;
            set { _fillColorStart = value; Invalidate(); }
        }

        [System.ComponentModel.Category("Appearance")]
        public Color FillColorEnd
        {
            get => _fillColorEnd;
            set { _fillColorEnd = value; Invalidate(); }
        }

        [System.ComponentModel.Category("Appearance")]
        public bool ShowPercentText
        {
            get => _showPercentText;
            set { _showPercentText = value; Invalidate(); }
        }

        public RoundedProgressBar()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer, true);
            Height = 24;
            Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);

            
            _animTimer = new System.Windows.Forms.Timer { Interval = 16 };
            _animTimer.Tick += (s, e) =>
            {
                float target = (float)_value / _maximum;
                float current = _animatedValue;
                float diff = target - current;

                if (Math.Abs(diff) < 0.001f)
                {
                    _animatedValue = target;
                    _animTimer.Stop();
                }
                else
                {
                    
                    _animatedValue += diff * 0.08f;
                }
                Invalidate();
            };

            
            _shimmerTimer = new System.Windows.Forms.Timer { Interval = 30 };
            _shimmerTimer.Tick += (s, e) =>
            {
                _shimmerPos += 0.012f;
                if (_shimmerPos > 1.3f) _shimmerPos = -0.3f;
                if (_animatedValue > 0.02f) Invalidate();
            };
            _shimmerTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            
            using (var trackPath = GetRoundedRect(rect, _cornerRadius))
            using (var trackBrush = new SolidBrush(_trackColor))
                g.FillPath(trackBrush, trackPath);

            
            using (var innerShadowPath = GetRoundedRect(new Rectangle(0, 0, Width - 1, Height / 2), _cornerRadius))
            using (var innerBrush = new LinearGradientBrush(
                new Rectangle(0, 0, Width, Height / 2),
                Color.FromArgb(20, 0, 0, 0),
                Color.Transparent,
                LinearGradientMode.Vertical))
                g.FillPath(innerBrush, innerShadowPath);

            
            int fillWidth = (int)(Width * _animatedValue);
            if (fillWidth > 1)
            {
                Rectangle fillRect = new Rectangle(0, 0, fillWidth - 1, Height - 1);
                using (var fillPath = GetRoundedRect(fillRect, _cornerRadius))
                using (var fillBrush = new LinearGradientBrush(fillRect, _fillColorStart, _fillColorEnd, 0f))
                    g.FillPath(fillBrush, fillPath);

                
                if (fillWidth > 20)
                {
                    int shimCenter = (int)(_shimmerPos * fillWidth);
                    int shimWidth = Math.Max(20, fillWidth / 5);
                    Rectangle shimRect = new Rectangle(
                        Math.Max(0, shimCenter - shimWidth / 2),
                        1,
                        shimWidth,
                        Height - 2);
                    shimRect.X = Math.Min(shimRect.X, fillWidth - 1);
                    shimRect.Width = Math.Min(shimRect.Width, fillWidth - shimRect.X);

                    if (shimRect.Width > 0)
                    {
                        using (var shimBrush = new LinearGradientBrush(
                            shimRect,
                            Color.Transparent,
                            Color.FromArgb(55, 255, 255, 255),
                            LinearGradientMode.Horizontal))
                        {
                            shimBrush.SetBlendTriangularShape(0.5f);
                            
                            var state = g.Save();
                            g.SetClip(new Rectangle(0, 0, fillWidth, Height));
                            g.FillRectangle(shimBrush, shimRect);
                            g.Restore(state);
                        }
                    }
                }

                
                Rectangle topHighlight = new Rectangle(1, 1, fillWidth - 2, (Height - 2) / 2);
                if (topHighlight.Width > 0)
                {
                    using (var highlightBrush = new LinearGradientBrush(
                        new Rectangle(topHighlight.X, topHighlight.Y, topHighlight.Width, topHighlight.Height + 1),
                        Color.FromArgb(50, 255, 255, 255),
                        Color.Transparent,
                        LinearGradientMode.Vertical))
                    {
                        var state = g.Save();
                        g.SetClip(new Rectangle(0, 0, fillWidth, Height));
                        g.FillRectangle(highlightBrush, topHighlight);
                        g.Restore(state);
                    }
                }
            }

            
            if (_showPercentText)
            {
                string text = $"{(int)(_animatedValue * 100)}%";
                TextRenderer.DrawText(g, text, Font, rect,
                    Color.FromArgb(60, 65, 75),
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private static GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();

            if (d == 0 || bounds.Width <= d || bounds.Height <= d)
            {
                path.AddRectangle(bounds);
                return path;
            }

            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animTimer?.Dispose();
                _shimmerTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    
    
    
    public static class AvatarHelper
    {
        public static void MakeCircular(PictureBox pic)
        {
            if (pic.Width <= 0 || pic.Height <= 0) return;
            var path = new GraphicsPath();
            path.AddEllipse(0, 0, pic.Width, pic.Height);
            pic.Region = new Region(path);
        }
    }
}