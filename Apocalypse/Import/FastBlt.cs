using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Apocalypse.Import
{
    static partial class Drawing
    {
        public static void FastBlt(this Bitmap orig, Bitmap dest, Rectangle source, Point target)
        {
            var origData = orig.LockBits(source, ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);
            var destData = dest.LockBits(new Rectangle(target, source.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppPArgb);
            byte[] data = new byte[origData.Stride];
            for (int row = origData.Height; row > 0; row--)
            {
                IntPtr fromPtr = (IntPtr)(origData.Scan0.ToInt64() + row * origData.Stride);
                IntPtr toPtr = (IntPtr)(destData.Scan0.ToInt64() + row * destData.Stride);
                Marshal.Copy(fromPtr, data, 0, data.Length);
                Marshal.Copy(data, 0, toPtr, data.Length);
            }
            orig.UnlockBits(origData);
            dest.UnlockBits(destData);
        }
    }
}
