using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace Action
{
    public enum Movement { UP, DOWN, LEFT, RIGHT };
    public static class MoveClass
    {
        public static Movement m = new Movement();
        static Size s;

        static Movement GetMovement() { return m; }

        public static bool CanMove(Point zeroPos,Size size)
        {
            s = size;
            switch (m)
            {
                //0이 맨 아랫줄에 있으면 이동 불가
                case Movement.UP:
                    if (zeroPos.Y == s.Height-1)
                    {
                        return false;
                    }
                    break;
                //0이 맨 윗줄에 있으면 이동 불가
                case Movement.DOWN:
                    if (zeroPos.Y == 0)
                    {
                        return false;
                    }
                    break;
                //0이 가장 오른쪽 줄에 있으면 이동 불가
                case Movement.LEFT:
                    if (zeroPos.X == s.Width-1)
                    {
                        return false;
                    }
                    break;
                //0이 가장 왼쪽 줄에 있으면 이동 불가
                case Movement.RIGHT:
                    if (zeroPos.X == 0)
                    {
                        return false;
                    }
                    break;
            }
            return true;
        }
        public static void updateMovementLeft()
        {
            m = Movement.LEFT;
        }
        public static void updateMovementRight()
        {
            m = Movement.RIGHT;
        }
        public static void updateMovementUp()
        {
            m = Movement.UP;
        }
        public static void updateMovementDown()
        {
            m = Movement.DOWN;
        }
    }
}
