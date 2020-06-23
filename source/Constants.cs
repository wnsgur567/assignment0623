using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _Constant
{
    public static class Constants
    {
        /*-------------------------------------------------------------------*/
        //Box ARRAY의 인덱스 사이즈        
        public readonly static Size BoxIndexSize = new Size(4, 4);
        /*-------------------------------------------------------------------*/
        //이미지 시작 위치
        public readonly static Point imagePoint = new Point(10, 10);        
        //box 당 image size
        // 4 * 4 기준 100 으로 최적화      /    총 길이 400 * 400       
        
        //box index size로 divide해서 각 박스별 이미지 길이 관리
        public const int pictureMaxLength = 400;
        /*-------------------------------------------------------------------*/

        public const int ShuffleCount = 30;
        public const int ShuffleSpeed = 10;

        /*-------------------------------------------------------------------*/
        public const int undoListCapacity = 10;
    }
}
