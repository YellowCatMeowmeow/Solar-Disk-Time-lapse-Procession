using OpenCvSharp;
using System;
using System.Runtime.InteropServices;

public static class FitsIO
{
    public static void SaveMat(Mat mat, string path, bool normalize = false)
    {
        using Mat gray = new Mat();
        if (mat.Channels() > 1)
            Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);
        else
            mat.CopyTo(gray);

        using Mat img16 = new Mat();
        if (normalize)
            Cv2.Normalize(gray, img16, 0, 65535, NormTypes.MinMax);
        else if (gray.Depth() == MatType.CV_8U)
            gray.ConvertTo(img16, MatType.CV_16U, 256.0);
        else
            gray.ConvertTo(img16, MatType.CV_16U);

        int w = img16.Cols;
        int h = img16.Rows;
        ushort[] data = new ushort[w * h];

        unsafe
        {
            byte* basePtr = img16.DataPointer;
            if (!img16.IsContinuous())
            {
                for (int y = 0; y < h; y++)
                {
                    byte* rowPtr = basePtr + y * img16.Step();
                    ushort* src = (ushort*)rowPtr;
                    fixed (ushort* dstBase = data)
                    {
                        Buffer.MemoryCopy(
                            src,
                            dstBase + y * w,
                            w * sizeof(ushort),
                            w * sizeof(ushort));
                    }
                }
            }
            else
            {
                fixed (ushort* dst = data)
                {
                    ushort* src = (ushort*)basePtr;
                    Buffer.MemoryCopy(src, dst, data.Length * 2, data.Length * 2);
                }
            }
        }

        WriteU16(data, w, h, path);
    }

    public static void WriteU16(ushort[] data, int w, int h, string path)
    {
        int status = 0;
        IntPtr fptr = IntPtr.Zero;
        try
        {
            fptr = CFITSIO.Create(path, ref status);
            CFITSIO.CreateImage(fptr, CFITSIO.TUSHORT, w, h, ref status);
            CFITSIO.WriteU16(fptr, data, ref status);
        }
        finally
        {
            if (fptr != IntPtr.Zero)
                CFITSIO.Close(fptr, ref status);
        }
    }

        public static Mat ReadMat(string path)
    {
        int status = 0;
        IntPtr fptr = IntPtr.Zero;

        try
        {
            fptr = CFITSIO.Open(path, false, ref status);
            //CFITSIO.MoveToImageHDU(fptr, ref status);
            var (w, h) = CFITSIO.GetSize(fptr, ref status);
            int bitpix = CFITSIO.GetImageBitDepth(fptr, ref status);

            // 自动识别类型读取
            Mat mat = new Mat();
            if (bitpix == 16)
            {
                ushort[] data = new ushort[w * h];
                GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                try
                {
                    int anynul = 0;
                    CFITSIORaw.ffgpv(
                        fptr,
                        CFITSIO.TUSHORT,
                        1L,
                        data.Length,
                        IntPtr.Zero,
                        handle.AddrOfPinnedObject(),
                        ref anynul,
                        ref status);
                    CFITSIO.Check(status, "ffgpv");
                }
                finally { handle.Free(); }

                mat = new Mat(h, w, MatType.CV_16U);
                unsafe
                {
                    fixed (ushort* src = data)
                    {
                        Buffer.MemoryCopy(src, mat.DataPointer, (long)data.Length * 2, (long)data.Length * 2);
                    }
                }
            }
            else if (bitpix == 8)
            {
                byte[] data = new byte[w * h];
                GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
                try
                {
                    int anynul = 0;
                    CFITSIORaw.ffgpv(
                        fptr,
                        11, // TBYTE
                        1L,
                        data.Length,
                        IntPtr.Zero,
                        handle.AddrOfPinnedObject(),
                        ref anynul,
                        ref status);
                    CFITSIO.Check(status, "ffgpv");
                }
                finally { handle.Free(); }

                mat = new Mat(h, w, MatType.CV_8U);
                unsafe
                {
                    fixed (byte* src = data)
                    {
                        Buffer.MemoryCopy(src, mat.DataPointer, data.Length, data.Length);
                    }
                }
            }
            else
            {
                throw new NotSupportedException($"不支持的BITPIX: {bitpix}");
            }

            return mat;
        }
        finally
        {
            if (fptr != IntPtr.Zero)
                CFITSIO.Close(fptr, ref status);
        }
    }

    // =========================
    // GONG 太阳图 专用读取（完美读取 BITPIX=16）
    // =========================
    public static Mat ReadGongMat(string path)
    {
        int status = 0;
        IntPtr fptr = IntPtr.Zero;

        try
        {
            // 打开文件
            fptr = CFITSIO.Open(path, false, ref status);

            // 读取图像宽高（GONG 在 HDU0）
            var (w, h) = CFITSIO.GetSize(fptr, ref status);

            // --------------------------
            // GONG 格式：int16（有符号）
            // --------------------------
            short[] data = new short[w * h];
            int anynul = 0;

            CFITSIORaw.ffgpvi(
                fptr,
                0,             // group
                1,             // firstelem
                data.Length,   // nelem
                -32768,        // nulval
                data,          // ✅ 直接传数组！
                ref anynul,
                ref status);

            // 直接生成 OpenCV 16位有符号 Mat
            Mat mat = new Mat(h, w, MatType.CV_16S);
            unsafe
            {
                fixed (short* src = data)
                {
                    Buffer.MemoryCopy(
                        src,
                        mat.DataPointer,
                        (long)data.Length * 2,
                        (long)data.Length * 2);
                }
            }

            return mat;
        }
        finally
        {
            if (fptr != IntPtr.Zero)
                CFITSIO.Close(fptr, ref status);
        }
    }
}