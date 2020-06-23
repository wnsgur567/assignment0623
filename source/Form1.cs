using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        /*-------------------------------------------------------------------*/
        //이미지 불러와서 조각 낼 dialog
        Form2 form2 = new Form2();
        //자식 dialog에서 Text Box 값 가져오기
        static public string Value { get; set; }
        //조각낸 이미지들을 차례로 담음 / 생성자 초기화
        List<Image> imageList;

        /*-------------------------------------------------------------------*/
        //정사각형으로 가정 , 아닐경우 x,y 분할 설정할 것
        static int pictureBoxImgLength;
        static Size BoxIndexSize;
        static readonly Point imagePoint = _Constant.Constants.imagePoint;

        //picturebox 판정 확인용 박스    
        static int[,] m_boxData = null;
        /*-------------------------------------------------------------------*/
        //화면 출력용 imagebox
        static PictureBox[,] pictureBoxARRAY = null;
        /*-------------------------------------------------------------------*/
        //1.게임 시작시 shuffle       
        //2.경과 시간 표시
        static Timer timer = new Timer();
        //Tick 불린 횟수
        static int tickCount = 0;
        //경과시간 체크, game start시 now할당
        static DateTime startTime;

        /*-------------------------------------------------------------------*/
        //button을 누를 수 있는 상태(첫 셔플 끝난 이후 true
        static bool gameStart = false;
        //image를 움직인 횟수
        static int moveCount = 0;
        /*-------------------------------------------------------------------*/
        const int capacity = _Constant.Constants.undoListCapacity;
        //가장 최근에 undo list에 들어간 Pos의 index
        int UndoIndex = 0;
        int UndoSize = 0;
        //Move 이전의 zeroPos를 List 에저장
        Point[] UndoList = new Point[capacity];

        public Form1()
        {
            //file열어서 가져오고 16등분해서 저장
            form2.ShowDialog();

            KeyPreview = true;              //key 사용을 위한 설정

            //form2에서 가져온 값으로 초기화
            int tmpSize = int.Parse(Value);
            BoxIndexSize = new Size(tmpSize, tmpSize);
            pictureBoxImgLength = _Constant.Constants.pictureMaxLength / BoxIndexSize.Height;
            imageList = new List<Image>(BoxIndexSize.Width * BoxIndexSize.Height);


            //16등분한 이미지 담기
            InitImageList();
            InitializeComponent();
            InitDataBoxes();
            InitPictureBoxes();

            UpdatePictureBox();

            timer.Interval = _Constant.Constants.ShuffleSpeed / BoxIndexSize.Height;
            timer.Tick += TimerTick_Shuffle;
            timer.Start();
        }


        /*-------------------------------------------------------------------*/
        //timer
        //public delegate void EventHandler(object sender, EventArgs e);
        void TimerTick_Shuffle(object sender, EventArgs e)
        {
            Suffle();
            tickCount++;

            if (tickCount + 1 > _Constant.Constants.ShuffleCount * BoxIndexSize.Height * (BoxIndexSize.Width - 2))
            {
                gameStart = true;
                startTime = DateTime.Now;
                timer.Stop();
                timer.Tick -= TimerTick_Shuffle;
                InitStartTimer();
            }
        }

        //시간 경과 화면에 출력
        void InitStartTimer()
        {
            timer.Interval = 100; // 1 sec
            timer.Tick += TimerTick_Elapsed;
            timer.Start();
        }

        void TimerTick_Elapsed(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            ElapsedTime_DataText.Text = String.Format("{0:00}:{1:00}", elapsed.Minutes, elapsed.Seconds);
        }

        void Suffle()
        {
            Random random = new Random();

            do //움직일 수 있는 방향이 나올 때까지 랜덤함수 돌림
            {
                Action.MoveClass.m = (Action.Movement)random.Next(4);
            } while (!Action.MoveClass.CanMove(GetZeroPos(), BoxIndexSize));
            Swap();
        }
        /*-------------------------------------------------------------------*/
        /*------------------------Init---------------------------------------*/
        Image LoadImage(int h, int w)
        {
            Image retImg = null;
            string firstName = @"..\..\Resources\Resized_";
            int num = BoxIndexSize.Width * h + w;

            retImg = Bitmap.FromFile(firstName + num.ToString("00") + ".jpg");
            return retImg;
        }

        //잘라 논 이미지들을 리스트에 탑재
        void InitImageList()
        {
            int _height = BoxIndexSize.Height;
            int _width = BoxIndexSize.Width;

            for (int height = 0; height < _height; height++)
            {
                for (int width = 0; width < _width; width++)
                {
                    imageList.Add(LoadImage(height, width));
                }
            }
        }
        //판정 박스 초기화
        void InitDataBoxes()
        {
            int _height = BoxIndexSize.Height;
            int _width = BoxIndexSize.Width;

            m_boxData = new int[_height, _width];

            int num = 0;
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    m_boxData[i, j] = num++;
                }
            }
        }
        //PictureBox초기화 ,image 할당 안함, image는 update에서
        void InitPictureBoxes()
        {
            pictureBoxARRAY = new PictureBox[BoxIndexSize.Height, BoxIndexSize.Width];

            int _height = BoxIndexSize.Height;
            int _width = BoxIndexSize.Width;

            for (int height = 0; height < _height; height++)
            {
                for (int width = 0; width < _width; width++)
                {
                    pictureBoxARRAY[height, width] = new System.Windows.Forms.PictureBox();
                    ((System.ComponentModel.ISupportInitialize)(pictureBoxARRAY[height, width])).BeginInit();
                    this.SuspendLayout();

                    pictureBoxARRAY[height, width].Location = new System.Drawing.
                                                Point(imagePoint.X + pictureBoxImgLength * width, imagePoint.Y + pictureBoxImgLength * height);
                    pictureBoxARRAY[height, width].Name = "pictureBoxARR" + "[" + height.ToString() + "," + width.ToString() + "]";
                    pictureBoxARRAY[height, width].Size = new System.Drawing.Size(pictureBoxImgLength, pictureBoxImgLength);
                    pictureBoxARRAY[height, width].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    pictureBoxARRAY[height, width].TabIndex = height * 10 + width + 1;
                    pictureBoxARRAY[height, width].TabStop = false;
                    pictureBoxARRAY[height, width].Padding = new Padding(1);
                    pictureBoxARRAY[height, width].BackColor = Color.Gray;

                    Controls.Add(pictureBoxARRAY[height, width]);

                    ((System.ComponentModel.ISupportInitialize)(pictureBoxARRAY[height, width])).EndInit();
                    this.ResumeLayout(false);
                }
            }
        }


        //해당 위치 pictureBox에 image 설정, overload
        void SetImageByPos(int _imageIndex, int posX, int posY)
        {
            if (_imageIndex == 0)
                pictureBoxARRAY[posY, posX].Image = null;
            else
                pictureBoxARRAY[posY, posX].Image = imageList[_imageIndex];
        }
        void SetImageByPos(int _x, int _y)
        {
            pictureBoxARRAY[_y, _x].Image = imageList[BoxIndexSize.Width * _y + _x];
        }


        //판정 박스의 0위치 반환
        Point GetZeroPos()
        {
            Point retPos;
            int _height = BoxIndexSize.Height;
            int _width = BoxIndexSize.Width;

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (m_boxData[y, x] == 0)
                    {
                        retPos = new Point(x, y);
                        return retPos;
                    }
                }
            }

            return retPos = new Point(-1, -1);
        }

        /*-------------------------------------------------------------------*/
        /*-------------------------키보드 및 버튼 공용 move--------------------*/

        private void MoveNode()
        {
            if (gameStart)
            {
                if (Action.MoveClass.CanMove(GetZeroPos(), BoxIndexSize))
                {
                    //undo list에다 넣기
                    UndoList[UndoIndex] = GetZeroPos();
                    UndoIndex = (UndoIndex + 1) % capacity;
                    UndoSize++;
                    if (UndoSize > capacity) UndoSize = capacity;
                    //

                    Swap();
                    StringPrintMoveCount();
                }
            }
        }

        /*-------------------------------------------------------------------*/
        /*--------------------------button-----------------------------------*/

        //값 교환 후 pciture box update까지
        void Swap()
        {
            if (m_boxData == null) return;

            //zero와 바꿀 위치의 인덱스 차
            int[,] offset = new int[,]
            {
            /*  { i, j}  // j = 0 은 x좌표  , j = 1은 y좌표   */     
                { 0, 1}, //UP
                { 0,-1}, //DOWN
                { 1, 0}, //LEFT
                {-1, 0}  //RIGHT
            };

            Point zeroPos = GetZeroPos();
            int x = zeroPos.X;
            int y = zeroPos.Y;
            int index = (int)Action.MoveClass.m;
            int offset_X = offset[index, 0];
            int offset_Y = offset[index, 1];

            int tmp;
            tmp = m_boxData[y + offset_Y, x + offset_X];
            m_boxData[y + offset_Y, x + offset_X] = m_boxData[y, x];
            m_boxData[y, x] = tmp;

            UpdatePictureBox();
        }
        private void upButton_Click(object sender, EventArgs e)
        {
            Action.MoveClass.updateMovementUp();
            MoveNode();
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            Action.MoveClass.updateMovementDown();
            MoveNode();
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            Action.MoveClass.updateMovementLeft();
            MoveNode();
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            Action.MoveClass.updateMovementRight();
            MoveNode();
        }

        void StringPrintMoveCount()
        {
            moveCount++;
            MoveCount_DataText.Text = string.Format("{0}", moveCount);
        }


        /*-------------------------------------------------------------------*/

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            e.Handled = false;
            if (gameStart)
            {
                switch (e.KeyCode)
                {
                    case Keys.Q:
                        Undo();
                        break;
                    case Keys.W:
                        //case Keys.Up:
                        Action.MoveClass.updateMovementUp();
                        MoveNode();
                        break;
                    case Keys.S:
                        //case Keys.Down:
                        Action.MoveClass.updateMovementDown();
                        MoveNode();
                        break;
                    case Keys.A:
                        //case Keys.Left:
                        Action.MoveClass.updateMovementLeft();
                        MoveNode();
                        break;
                    case Keys.D:
                        //case Keys.Right:
                        Action.MoveClass.updateMovementRight();
                        MoveNode();
                        break;
                }
            }
        }


        /*-------------------------------------------------------------------*/

        //boxData 값으로 모든 picture box이미지 업데이트 + 승리 검사
        void UpdatePictureBox()
        {
            int _height = BoxIndexSize.Height;
            int _width = BoxIndexSize.Width;

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    SetImageByPos(m_boxData[i, j], j, i);
                }
            }

            if (isClear())
            {
                timer.Stop();
                MessageBox.Show("Clear");
            }
        }
        /*-------------------------------------------------------------------*/

        //모든 조각을 맞췄는지 판단
        bool isClear()
        {
            if (gameStart)
            {
                int num = 0;
                foreach (var item in m_boxData)
                {
                    if (item != num++)
                        return false;
                }
                return true;
            }
            return false;
        }

        //undo
        void Undo()
        {
            if (UndoSize <= 0) return;

            UndoIndex = (UndoIndex + capacity - 1) % capacity;

            Point tmpPos = UndoList[UndoIndex];
            int dx = GetZeroPos().X - tmpPos.X;
            int dy = GetZeroPos().Y - tmpPos.Y;

            //총 4가지 경우
            if (dx == 1)
                Action.MoveClass.m = Action.Movement.RIGHT;
            else if (dx == -1)
                Action.MoveClass.m = Action.Movement.LEFT;
            else if (dy == 1)
                Action.MoveClass.m = Action.Movement.DOWN;
            else if (dy == -1)
                Action.MoveClass.m = Action.Movement.UP;


            Swap();

            UndoSize--;
            moveCount--;
            MoveCount_DataText.Text = string.Format("{0}", moveCount);
        }

        //aplication filter
        private void upButton_Enter(object sender, EventArgs e)
        {            
            this.OnKeyDown(new KeyEventArgs(Keys.Up));
            label_ElapsedTime.Focus();                      
        }

        private void rightButton_Enter(object sender, EventArgs e)
        {
            this.OnKeyDown(new KeyEventArgs(Keys.Right));
            label_ElapsedTime.Focus();            
        }

        private void leftButton_Enter(object sender, EventArgs e)
        {
            this.OnKeyDown(new KeyEventArgs(Keys.Left));
            label_ElapsedTime.Focus();            
        }

        private void downButton_Enter(object sender, EventArgs e)
        {
            this.OnKeyDown(new KeyEventArgs(Keys.Down));
            label_ElapsedTime.Focus();            
        }
    }
}
