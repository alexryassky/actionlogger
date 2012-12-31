using System;
using System.Collections.Generic;

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;   
using System.Drawing.Drawing2D;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DocGen
{   

    /// <summary>
    /// cGradientButton - кнопка,закрашиваемая градиентом c использованием функций GDI+;
    /// Компонент написан Jet 5 Декабря 2011. c 
    /// TODO: Разработать Designer
    /// Лицензия Lesser GNU
    /// </summary>
  
    public partial class cGradientButton : Button
    {
        const string AERO_IDENTIFIER = "CAero.msstyle";

        // Флаг состояния кнопки (нажата/отжата).
        private bool Pushed = false;
        // Флаг темы - Aero или нет
        private bool ThemeAero = true;
        // роль кнопки -- true: обычная кнопка; false : типа "Далее".
        private bool _role = true;
        // Объект, хранящий картинку
        private System.Drawing.Image objImage; 
        private Color _backColor = System.Drawing.Color.CornflowerBlue;

        public Color BackColorGrad {
            get { return _backColor; }
            set { _backColor = value; } 
        }


        public bool Role { get { return _role; } set { _role = value; } }
        // --

        //  возможно,  в будущем края кнопок будут закруглены...        
        private int RoundCorners = 15;

        public cGradientButton()
        {
         
           
                // Вся обработка перерисовки ложится на пользовательский обработчик WM_PAINT
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

                // Кнопка перерисовывается без использования WM_ERASEBKGND
                this.SetStyle(ControlStyles.UserPaint, true);

                // При изменении размера кнопки вызывается перерисовка
                this.SetStyle(ControlStyles.ResizeRedraw, true);

                // Включена буферизация
                this.SetStyle(ControlStyles.DoubleBuffer, true);

                // Можно использовать альфаканал в кисти
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            

                // Обработчики по умолчанию для этих двух событий изменяют флаг Pushed и вызывают перерисовку
                this.MouseDown += new MouseEventHandler(GradientButtonMouseDown);
                this.MouseUp += new MouseEventHandler(GradientButtonMouseUp);
           
          

        }
      
   
        protected override void  OnPaint(PaintEventArgs pevent)
        {
            if (ThemeAero) // Если тема Windows Aero, то применяем кастомное оформление
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                //Контур вокруг кнопки
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

                // Параметры градиента фона
                float[] Factors = new float[] { 0.0f, 0.25f, 0.7f, 1.0f };
                float[] Positions = new float[] { 0.0f, 0.5f, 0.6f, 1.0f };

                System.Drawing.Drawing2D.LinearGradientBrush PassiveGradientBrush = null;
                System.Drawing.Drawing2D.LinearGradientBrush BorderGradientBrush = null;

                //Градиент для закраски неактивной кнопки        
                PassiveGradientBrush = new LinearGradientBrush(

                    new Rectangle(0, 0, this.Width, this.Height),
                    Color.FromArgb(255, 255, 255, 255),
                    BackColorGrad,
                    LinearGradientMode.Vertical
                    );


                //Градиент для закраски контура кнопки        
                BorderGradientBrush = new LinearGradientBrush(
                    new Rectangle(0, 0, this.Width, this.Height),
                    Color.LightGray,
                    Color.DarkGray,
                    LinearGradientMode.BackwardDiagonal
                    );

                // Инициализация бленды (структуры для управления рисованием градиента фона кнопки).
                System.Drawing.Drawing2D.Blend mBlend = new System.Drawing.Drawing2D.Blend();
                mBlend.Factors = Factors;
                mBlend.Positions = Positions;
                PassiveGradientBrush.Blend = mBlend;

                // Кисть для закраски подложки кнопки (используется вместо WM_ERASEBKGND)
                SolidBrush solbBGBrush = new SolidBrush(Color.White);

                // Инициализируем контур
                path.AddLine(new Point(0, 0),
                             new Point(this.Width, 0)
                             );
                path.AddLine(new Point(Width, 0),
                             new Point(this.Width, this.Height - 1)
                             );
                path.AddLine(new Point(this.Width, this.Height - 1),
                             new Point(0, this.Height - 1)
                             );
                path.AddLine(new Point(0, this.Height - 1),
                             new Point(0, 0)
                             );
                path.CloseFigure();
                Pen pen = new Pen(BorderGradientBrush, 1);

                // Инструменты для рисования текста
                StringFormat stringFormat = new StringFormat();
                SolidBrush solbTextBrush = new SolidBrush(Color.Black);
                stringFormat.Alignment = StringAlignment.Center;
                // Закрашиваем контур градиентом
                pevent.Graphics.FillPath(BorderGradientBrush, path);


                if (Enabled)
                {
                    if (Pushed)
                    {
                        
                        
                    }
                    else
                    {
                        Rectangle rect = ClientRectangle;
                        // Уменьшаем площадь закраски на 1 пкс для того, чтобы поместился контур
                        rect.Inflate(new System.Drawing.Size(-1, -1)
                                    );
                        //Рисуем градиент
                        pevent.Graphics.FillRectangle(PassiveGradientBrush, rect);
                    }
                }
                else
                {
                    
                }
                //Рисуем текст
                pevent.Graphics.DrawString(this.Text, this.Font, solbTextBrush, new PointF(Width / 2, this.Height / 4), stringFormat);

                //Рисуем контур
                pevent.Graphics.DrawPath(pen, path);

                //Во избежание утечек памяти 

                if (PassiveGradientBrush != null)

                    PassiveGradientBrush.Dispose();

                if (solbBGBrush != null)

                    solbBGBrush.Dispose();

                if (solbTextBrush != null)

                    solbTextBrush.Dispose();

                if (BorderGradientBrush != null)

                    BorderGradientBrush.Dispose();

                if (path != null)

                    path.Dispose();

                if (pen != null)

                    pen.Dispose();
            }
            else
                base.OnPaint(pevent);
        }

        private void GradientButtonMouseDown(object sender, MouseEventArgs e)
        {
            this.Pushed = true;
            // Вызываем Invalidate и Update, что и есть, по-сути, метод Refresh
            Refresh();
        }

        private void GradientButtonMouseUp(object sender, MouseEventArgs e)
        {
            this.Pushed = false;
            Refresh(); 
        }
    }
}
