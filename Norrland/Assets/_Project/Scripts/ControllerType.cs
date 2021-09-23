using System;
using System.Globalization;
using Object = System.Object;

namespace _Project.Scripts.EnumFlags
{
    [Flags]
    public enum ControllerType
    {
        LaserPointers = 1 << 0,
        Teleporting = 1 << 1,
        MotionMovement = 1 << 2,
        Climbing = 1 << 3
    }

    public static class ControllerTypeExtensions
    {
        public static T SetFlag<T>(ref this T flags, T flag, bool value) where T : struct, IComparable, IFormattable, IConvertible
        {
            int flagsInt = flags.ToInt32(NumberFormatInfo.CurrentInfo);
            int flagInt = flag.ToInt32(NumberFormatInfo.CurrentInfo);
            
            if (value)
                flagsInt |= flagInt;
            else
                flagsInt &= ~flagInt;

            return (T)(Object)flagsInt;
        }
    }
}
