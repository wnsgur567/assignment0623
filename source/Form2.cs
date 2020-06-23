using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        Size BoxIndexSize; //잘라내기 시 text 박스에서 읽어 온 값으로 설정하기
        static readonly Point imagePoint = _Constant.Constants.imagePoint;        

        public Form2()
        {
            InitializeComponent();

            pictureBox1.Image = Properties.Resources.Sample;
        }

        //파일 디렉토리를 열고 이미지를 dialog로 불러옴
        void LoadImageDialog()
        {
            string image_file = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"C:\";
            if (dialog.ShowDialog() == DialogResult.OK)
                image_file = dialog.FileName;
            else if (dialog.ShowDialog() == DialogResult.Cancel)                
                return;

            pictureBox1.Image = Bitmap.FromFile(dialog.FileName);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        //위치에 맞는 조각낸 이미지 return
        Image ResizeImage(Image image, int indexX, int indexY)
        {
            int tmpWidht = image.Width / BoxIndexSize.Width;
            int tmpHeight = image.Height / BoxIndexSize.Height;
            if (image != null)
            {
                Bitmap croppedBitmap = new Bitmap(image);
                croppedBitmap = croppedBitmap.Clone(
                    new Rectangle(indexX * tmpWidht, indexY * tmpHeight,
                    tmpWidht, tmpHeight),
                    System.Drawing.Imaging.PixelFormat.DontCare);
                return croppedBitmap;
            }
            else
                return null;
        }

        //image를 잘라서 파일로 출력       
        void FilePrintImage(Image image)
        {
            int _height = BoxIndexSize.Height;
            int _width = BoxIndexSize.Width;

            //Debug폴더 안에 ResizedImage 폴더 생성 후 이미지 저장
            string save_route = @"..\..\Resources";
            if (!System.IO.Directory.Exists(save_route))
            {
                System.IO.Directory.CreateDirectory(save_route);
            }

            int num = 0;
            for (int height = 0; height < _height; height++)
            {
                for (int width = 0; width < _width; width++)
                {
                    ResizeImage(image, width, height).Save(save_route + "\\Resized_" + num.ToString("00") + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    ++num;
                }
            }
        }       

        private void button1_Click(object sender, EventArgs e)
        {
            LoadImageDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {                
                Close();
                return;
            }
            int tmp = int.Parse(textBox1.Text);
            BoxIndexSize = new Size(tmp, tmp);
            Form1.Value = textBox1.Text;
            FilePrintImage(pictureBox1.Image);
            Close();
        }
    }
}
