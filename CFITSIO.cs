using System;
using System.Runtime.InteropServices;

public static class CFITSIO
{
    // =========================
    // CREATE
    // =========================
    public static IntPtr Create(string path, ref int status)
    {
        IntPtr fptr = new IntPtr();
        CFITSIORaw.ffinit(ref fptr, "!" + path, ref status);
        if (status != 0) throw new Exception($"ffinit failed: {status}");
        return fptr;
    }

    // =========================
    // IMAGE HEADER（已修复）
    // =========================
    public static void CreateImage(IntPtr fptr, int bitpix, int w, int h, ref int status)
    {
        int[] naxes = new int[] { w, h };
        CFITSIORaw.ffcrim(fptr, bitpix, 2, naxes, ref status);
        Check(status, "ffcrim");
    }

    // =========================
    // WRITE U16
    // =========================
    public static void WriteU16(IntPtr fptr, ushort[] data, ref int status)
    {
        var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
        try
        {
            CFITSIORaw.ffppr(
                fptr,
                TUSHORT,
                1,
                data.Length,
                handle.AddrOfPinnedObject(),
                ref status);
            Check(status, "ffppr");
        }
        finally { handle.Free(); }
    }

    // =========================
    // WRITE FLOAT
    // =========================
    public static void WriteF32(IntPtr fptr, float[] data, ref int status)
    {
        var handle = GCHandle.Alloc(data, GCHandleType.Pinned);
        try
        {
            CFITSIORaw.ffppr(
                fptr,
                TFLOAT,
                1,
                data.Length,
                handle.AddrOfPinnedObject(),
                ref status);
            Check(status, "ffppr f32");
        }
        finally { handle.Free(); }
    }

    // =========================
    // OPEN/CLOSE
    // =========================
    public static IntPtr Open(string path, bool write, ref int status)
    {
        IntPtr fptr = new IntPtr();
        CFITSIORaw.ffopen(ref fptr, path, write ? 1 : 0, ref status);
        Check(status, "ffopen");
        return fptr;
    }

    public static void Close(IntPtr fptr, ref int status)
    {
        if (fptr == IntPtr.Zero) return;
        CFITSIORaw.ffclos(fptr, ref status);
    }

    // =========================
    // SIZE（🔥 已修复！）
    // =========================
    public static (int w, int h) GetSize(IntPtr fptr, ref int status)
    {
        // 直接用 int[]，完全不用指针
        int[] naxes = new int[2];

        // 调用 fits 函数
        CFITSIORaw.ffgisz(fptr, 2, naxes, ref status);
        Check(status, "ffgisz");

        // 返回宽、高
        return (naxes[0], naxes[1]);
    }

    // =========================
    // CHECK
    // =========================
    public static void Check(int status, string step)
    {
        if (status != 0)
            throw new Exception($"CFITSIO error {step}: {status}");
    }

    // =========================
    // GONG 太阳 FITS 专用：跳第一个图像 HDU
    // =========================
    public static void MoveToImageHDU(IntPtr fptr, ref int status)
    {
        int hdutype;
        // 🔥 唯一能正确打开 GONG FITS 的函数
        // 2 = 查找下一个 IMAGE_HDU
        CFITSIORaw.ffmnhd(fptr, 2, null, 0, ref status);

        if (status != 0)
            throw new Exception($"GONG FITS 跳转图像HDU失败: {status}");
    }

    /// <summary>
    /// 正确获取 FITS 图像位深 BITPIX
    /// </summary>
    public static int GetImageBitDepth(IntPtr fptr, ref int status)
    {
        status = 0;
        int bitpix = 0;

        // ✅ 正确函数：ffgidt = Get Image Data Type (BITPIX)
        CFITSIORaw.ffgidt(fptr, ref bitpix, ref status);

        Check(status, "ffgidt");
        return bitpix;
    }

    // 对应 BITPIX 含义
    public const int BYTE = 8;
    public const int SHORT = 16;
    public const int LONG = 32;
    public const int LONGLONG = 64;
    public const int FLOAT = -32;
    public const int DOUBLE = -64;

    public const int TUSHORT = 20;
    public const int TFLOAT = 42;
}