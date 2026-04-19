using System;
using System.Runtime.InteropServices;
using System.Text;

public static class CFITSIORaw
{
    const string dll = "cfitsio.dll";

    // 版本号常量
    public const double CFITSIO_VERSION = 4.63; // CFITSIO库版本号
    public const int CFITSIO_MICRO = 3; // 微版本号
    public const int CFITSIO_MINOR = 6; // 次版本号
    public const int CFITSIO_MAJOR = 4; // 主版本号
    public const int CFITSIO_SONAME = 10; // 共享库兼容版本号

    // 64位整数类型定义（对应C LONGLONG）
    public const long LONGLONG_MAX = 9223372036854775807; // 64位有符号整数最大值
    public const long LONGLONG_MIN = -9223372036854775808; // 64位有符号整数最小值

    // IO缓冲区相关
    public const int NIOBUF = 40; // 创建的IO缓冲区数量（默认40）
    public const int IOBUFLEN = 2880; // 每个IO缓冲区的字节大小，禁止修改

    // 全局字符串长度限制
    public const int FLEN_FILENAME = 1025; // 文件名最大长度
    public const int FLEN_KEYWORD = 75; // 关键字最大长度（层级命名规则）
    public const int FLEN_CARD = 81; // FITS头卡片长度
    public const int FLEN_VALUE = 71; // 关键字值字符串最大长度
    public const int FLEN_COMMENT = 73; // 关键字注释字符串最大长度
    public const int FLEN_ERRMSG = 81; // FITSIO错误信息最大长度
    public const int FLEN_STATUS = 31; // FITSIO状态文本最大长度

    // FITS表格数据类型编码
    public const int TBIT = 1;
    public const int TBYTE = 11;
    public const int TSBYTE = 12;
    public const int TLOGICAL = 14;
    public const int TSTRING = 16;
    public const int TUSHORT = 20;
    public const int TSHORT = 21;
    public const int TUINT = 30;
    public const int TINT = 31;
    public const int TULONG = 40;
    public const int TLONG = 41;
    public const int TINT32BIT = 41; // 返回列数据类型时使用
    public const int TFLOAT = 42;
    public const int TULONGLONG = 80;
    public const int TLONGLONG = 81;
    public const int TDOUBLE = 82;
    public const int TCOMPLEX = 83;
    public const int TDBLCOMPLEX = 163;

    // 关键字类型编码
    public const int TYP_STRUC_KEY = 10;
    public const int TYP_CMPRS_KEY = 20;
    public const int TYP_SCAL_KEY = 30;
    public const int TYP_NULL_KEY = 40;
    public const int TYP_DIM_KEY = 50;
    public const int TYP_RANG_KEY = 60;
    public const int TYP_UNIT_KEY = 70;
    public const int TYP_DISP_KEY = 80;
    public const int TYP_HDUID_KEY = 90;
    public const int TYP_CKSUM_KEY = 100;
    public const int TYP_WCS_KEY = 110;
    public const int TYP_REFSYS_KEY = 120;
    public const int TYP_COMM_KEY = 130;
    public const int TYP_CONT_KEY = 140;
    public const int TYP_USER_KEY = 150;

    // FITS图像BITPIX类型码
    public const int BYTE_IMG = 8;
    public const int SHORT_IMG = 16;
    public const int LONG_IMG = 32;
    public const int LONGLONG_IMG = 64;
    public const int FLOAT_IMG = -32;
    public const int DOUBLE_IMG = -64;

    // 内部使用的无符号整数类型码
    public const int SBYTE_IMG = 10;
    public const int USHORT_IMG = 20;
    public const int ULONG_IMG = 40;
    public const int ULONGLONG_IMG = 80;

    // HDU类型
    public const int IMAGE_HDU = 0; // 图像HDU
    public const int ASCII_TBL = 1; // ASCII表HDU
    public const int BINARY_TBL = 2; // 二进制表HDU
    public const int ANY_HDU = -1; // 匹配任意HDU类型

    // 文件打开模式
    public const int READONLY = 0; // 只读
    public const int READWRITE = 1; // 读写

    // 空值标记
    public const float FLOATNULLVALUE = -9.11912E-36F; // 浮点空值
    public const double DOUBLENULLVALUE = -9.1191291391491E-36; // 双精度空值

    // 压缩算法编码
    public const int NO_DITHER = -1;
    public const int SUBTRACTIVE_DITHER_1 = 1;
    public const int SUBTRACTIVE_DITHER_2 = 2;
    public const int MAX_COMPRESS_DIM = 6;
    public const int RICE_1 = 11;
    public const int GZIP_1 = 21;
    public const int GZIP_2 = 22;
    public const int PLIO_1 = 31;
    public const int HCOMPRESS_1 = 41;
    public const int BZIP2_1 = 51; // 非公开支持，仅测试用
    public const int NOCOMPRESS = -1;

    // 布尔值
    public const int TRUE = 1;
    public const int FALSE = 0;

    // 字符串大小写匹配
    public const int CASESEN = 1; // 区分大小写
    public const int CASEINSEN = 0; // 不区分大小写

    // 层级分组参数
    public const int GT_ID_ALL_URI = 0;
    public const int GT_ID_REF = 1;
    public const int GT_ID_POS = 2;
    public const int GT_ID_ALL = 3;
    public const int GT_ID_REF_URI = 11;
    public const int GT_ID_POS_URI = 12;

    // 删除操作选项
    public const int OPT_RM_GPT = 0;
    public const int OPT_RM_ENTRY = 1;
    public const int OPT_RM_MBR = 2;
    public const int OPT_RM_ALL = 3;

    // 复制操作选项
    public const int OPT_GCP_GPT = 0;
    public const int OPT_GCP_MBR = 1;
    public const int OPT_GCP_ALL = 2;

    // 移动操作选项
    public const int OPT_MCP_ADD = 0;
    public const int OPT_MCP_NADD = 1;
    public const int OPT_MCP_REPL = 2;
    public const int OPT_MCP_MOV = 3;

    // 合并选项
    public const int OPT_MRG_COPY = 0;
    public const int OPT_MRG_MOV = 1;

    // 注释操作选项
    public const int OPT_CMT_MBR = 1;
    public const int OPT_CMT_MBR_DEL = 11;

    // 存储表格列信息的结构体
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct tcolumn
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 70)]
        public string ttype;       // 列名，对应FITS TTYPEn关键字

        public long tbcol;         // 每行中该列首字节偏移量
        public int tdatatype;      // 列数据类型编码
        public long trepeat;       // 列重复次数，即元素个数
        public double tscale;      // FITS TSCALn 线性缩放系数
        public double tzero;       // FITS TZEROn 线性缩放零点
        public long tnull;         // 整数图像/二进制表列的空值

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string strnull;     // ASCII表空值字符串

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string tform;       // FITS tform 关键字值

        public long twidth;        // ASCII表列宽度
    }

    // 结构体验证魔术值
    public const int VALIDSTRUC = 555; // 用于标识结构体是否有效的魔术值

    // 存储FITS文件基础信息的结构体
    [StructLayout(LayoutKind.Sequential)]
    public struct FITSfile
    {
        public int filehandle; // 文件打开函数返回的句柄
        public int driver; // 定义使用的I/O驱动集
        public int open_count; // 使用此结构体的打开文件数
        [MarshalAs(UnmanagedType.LPStr)] string filename; // 文件名
        public int validcode; // 验证结构体有效的魔术值
        public int only_one; // 仅复制指定扩展的标志
        public int noextsyntax; // 忽略扩展语法打开文件的标志
        public long filesize; // 物理磁盘文件当前大小（字节）
        public long logfilesize; // 文件逻辑大小（含未刷新缓冲区）
        public int lasthdu; // 是否为文件最后一个HDU：0=否，其他=是
        public long bytepos; // 文件当前逻辑I/O指针位置
        public long io_pos; // 物理文件当前I/O指针位置
        public int curbuf; // 当前使用的I/O缓冲区编号
        public int curhdu; // 当前HDU编号：0=主阵列
        public int hdutype; // 0=主阵列，1=ASCII表，2=二进制表
        public int writemode; // 0=只读，1=读写
        public int maxhdu; // 文件中已知存在的最大HDU编号
        public int MAXHDU; // headstart数组动态分配维度
        public IntPtr headstart; // 每个HDU起始字节偏移
        public long headend; // 当前HDU头结束字节偏移
        public long ENDpos; // 最后写入END关键字的字节偏移
        public long nextkey; // 下一个关键字起始字节偏移
        public long datastart; // 当前数据单元起始字节偏移
        public int imgdim; // 图像维度（快速访问缓存）
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 99)]
        public long[] imgnaxis; // 各轴长度（快速访问缓存）
        public int tfield; // 表中字段数（主阵列=2）
        public int startcol; // ffgcnn使用的起始列号
        public long origrows; // 原始行数（NAXIS2关键字值）
        public long numrows; // 表中行数（动态更新）
        public long rowlength; // 表行或图像大小（字节）
        public IntPtr tableptr; // 表格结构体指针
        public long heapstart; // 堆起始字节（相对数据单元）
        public long heapsize; // 堆大小（字节）

        // 压缩图像相关参数
        public int request_compress_type; // 请求的图像压缩算法
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public long[] request_tilesize; // 请求的分块大小
        public float request_quantize_level; // 请求的量化级别
        public int request_quantize_method; // 请求的量化方法
        public int request_dither_seed; // 随机抖动起始偏移
        public int request_lossy_int_compress; // 有损压缩整数图像为浮点
        public int request_huge_hdu; // 使用1Q而非1P变长数组
        public float request_hcomp_scale; // 请求的HCOMPRESS缩放因子
        public int request_hcomp_smooth; // 请求的HCOMPRESS平滑参数

        public int compress_type; // 实际使用的压缩算法
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public long[] tilesize; // 压缩分块大小
        public float quantize_level; // 浮点量化级别
        public int quantize_method; // 浮点像素量化算法
        public int dither_seed; // 随机抖动起始偏移

        public int compressimg; // 1=HDU包含压缩图像，0=否
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        public string zcmptype; // 压缩类型字符串
        public int zbitpix; // 图像FITS数据类型(BITPIX)
        public int zndim; // 图像维度
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public long[] znaxis; // 各轴长度
        public long maxtilelen; // 每个图像块最大像素数
        public long maxelem; // 压缩数组最大字节长度

        public int cn_compressed; // COMPRESSED_DATA列号
        public int cn_uncompressed; // UNCOMPRESSED_DATA列号
        public int cn_gzip_data; // GZIP2无损压缩数据列号
        public int cn_zscale; // ZSCALE列号
        public int cn_zzero; // ZZERO列号
        public int cn_zblank; // ZBLANK列号

        public double zscale; // 缩放值（所有分块相同）
        public double zzero; // 零点（所有分块相同）
        public double cn_bscale; // 头文件BSCALE关键字值
        public double cn_bzero; // BZERO关键字值（可重置）
        public double cn_actual_bzero; // BZERO关键字实际值
        public int zblank; // 空像素值（非列时）

        public int rice_blocksize; // Rice压缩参数：像素/块
        public int rice_bytepix; // Rice压缩参数：字节/像素
        public float hcomp_scale; // Hcompress压缩参数1
        public int hcomp_smooth; // Hcompress压缩参数2

        public IntPtr tilerow; // 未压缩分块数据行号
        public IntPtr tiledatasize; // 分块数据长度（字节）
        public IntPtr tiletype; // 分块数据类型
        public IntPtr tiledata; // 未压缩分块数据数组
        public IntPtr tilenullarray; // 空值标记数组
        public IntPtr tileanynull; // 分块是否存在空值

        public IntPtr iobuffer; // FITS文件I/O缓冲区指针
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public long[] bufrecnum; // 每个缓冲区的文件记录号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public int[] dirty; // 缓冲区是否被修改
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
        public int[] ageindex; // 缓冲区相对使用时间
    }

    // 存储HDU基础信息的结构体
    [StructLayout(LayoutKind.Sequential)]
    public struct fitsfile
    {
        public int HDUposition; // HDU在文件中的位置：0=第一个HDU
        public IntPtr Fptr; // 指向FITS文件结构体的指针
    }

    // 迭代器函数列信息结构体
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct iteratorCol
    {
        public IntPtr fptr; // 指向列表所在HDU的指针
        public int colnum; // 表中列号（<1时使用名称）
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 70)]
        public string colname; // 列名（可选）
        public int datatype; // 输出数据类型（按需转换）
        public int iotype; // 列类型：输入/输入输出/输出

        public IntPtr array; // 数据数组指针（含空值）
        public long repeat; // 二进制表向量重复次数
        public long tlmin; // 数据最小值
        public long tlmax; // 数据最大值
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 70)]
        public string tunit; // 物理单位字符串
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 70)]
        public string tdisp; // 建议显示格式
    }

    // 迭代器列类型标志
    public const int InputCol = 0; // 仅输入列
    public const int InputOutputCol = 1; // 输入输出列
    public const int OutputCol = 2; // 仅输出列
    public const int TemporaryCol = 3; // 临时列（内部使用）

    // WCSLIB 坐标相关结构体
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct wtbarr
    {
        public int i; // 图像轴编号
        public int m; // 索引向量的数组轴编号
        public int kind; // 数组类型：'c'(坐标) 或 'i'(索引)
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 72)]
        public string extnam; // 二进制表扩展的EXTNAME
        public int extver; // 二进制表扩展的EXTVER
        public int extlev; // 二进制表扩展的EXTLEV
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 72)]
        public string ttype; // 存储数组的列TTYPEn
        public long row; // 表行号
        public int ndim; // 预期数组维度
        public IntPtr dimlen; // 数组轴长度存储地址
        public IntPtr arrayp; // 数组地址存储地址
    }

    // WCS 读取函数
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_read_wcstab(IntPtr fptr, int nwtb, IntPtr wtb, ref int status); // 读取WCS表数据

    // 错误状态码
    public const int CREATE_DISK_FILE = -106; // 创建磁盘文件，无扩展文件名语法
    public const int OPEN_DISK_FILE = -105; // 打开磁盘文件，无扩展文件名语法
    public const int SKIP_TABLE = -104; // 打开文件时跳转到第一个图像
    public const int SKIP_IMAGE = -103; // 打开文件时跳转到第一个表格
    public const int SKIP_NULL_PRIMARY = -102; // 打开文件时跳过空主阵列
    public const int USE_MEM_BUFF = -101; // 打开文件时使用内存缓冲区
    public const int OVERFLOW_ERR = -11; // 数据类型转换时溢出
    public const int PREPEND_PRIMARY = -9; // 在ffiimg中插入新主阵列
    public const int SAME_FILE = 101; // 输入输出文件相同
    public const int TOO_MANY_FILES = 103; // 尝试打开过多FITS文件
    public const int FILE_NOT_OPENED = 104; // 无法打开指定文件
    public const int FILE_NOT_CREATED = 105; // 无法创建指定文件
    public const int WRITE_ERROR = 106; // 写入FITS文件错误
    public const int END_OF_FILE = 107; // 尝试移动到文件末尾之后
    public const int READ_ERROR = 108; // 读取FITS文件错误
    public const int FILE_NOT_CLOSED = 110; // 无法关闭文件
    public const int ARRAY_TOO_BIG = 111; // 数组维度超出内部限制
    public const int READONLY_FILE = 112; // 无法写入只读文件
    public const int MEMORY_ALLOCATION = 113; // 无法分配内存
    public const int BAD_FILEPTR = 114; // 无效的fitsfile指针
    public const int NULL_INPUT_PTR = 115; // 例程输入指针为空
    public const int SEEK_ERROR = 116; // 文件定位错误
    public const int BAD_NETTIMEOUT = 117; // 文件下载超时设置错误

    public const int BAD_URL_PREFIX = 121; // 文件名URL前缀无效
    public const int TOO_MANY_DRIVERS = 122; // 尝试注册过多IO驱动
    public const int DRIVER_INIT_FAILED = 123; // 驱动初始化失败
    public const int NO_MATCHING_DRIVER = 124; // 未注册匹配驱动
    public const int URL_PARSE_ERROR = 125; // 解析文件URL失败
    public const int RANGE_PARSE_ERROR = 126; // 解析URL范围失败

    public const int SHARED_ERRBASE = 150; // 共享错误基值
    public const int SHARED_BADARG = 151; // 共享参数错误
    public const int SHARED_NULPTR = 152; // 共享空指针
    public const int SHARED_TABFULL = 153; // 共享表已满
    public const int SHARED_NOTINIT = 154; // 共享未初始化
    public const int SHARED_IPCERR = 155; // 共享进程通信错误
    public const int SHARED_NOMEM = 156; // 共享内存不足
    public const int SHARED_AGAIN = 157; // 共享操作重试
    public const int SHARED_NOFILE = 158; // 共享无文件
    public const int SHARED_NORESIZE = 159; // 共享无法调整大小

    public const int HEADER_NOT_EMPTY = 201; // 头文件已有关键字
    public const int KEY_NO_EXIST = 202; // 头文件中未找到关键字
    public const int KEY_OUT_BOUNDS = 203; // 关键字记录号超出范围
    public const int VALUE_UNDEFINED = 204; // 关键字值字段为空
    public const int NO_QUOTE = 205; // 字符串缺少闭合引号
    public const int BAD_INDEX_KEY = 206; // 非法索引关键字名称
    public const int BAD_KEYCHAR = 207; // 关键字名称/卡片含非法字符
    public const int BAD_ORDER = 208; // 必需关键字顺序错误
    public const int NOT_POS_INT = 209; // 关键字值非正整数
    public const int NO_END = 210; // 未找到END关键字
    public const int BAD_BITPIX = 211; // 非法BITPIX关键字值
    public const int BAD_NAXIS = 212; // 非法NAXIS关键字值
    public const int BAD_NAXES = 213; // 非法NAXISn关键字值
    public const int BAD_PCOUNT = 214; // 非法PCOUNT关键字值
    public const int BAD_GCOUNT = 215; // 非法GCOUNT关键字值
    public const int BAD_TFIELDS = 216; // 非法TFIELDS关键字值
    public const int NEG_WIDTH = 217; // 表行大小为负
    public const int NEG_ROWS = 218; // 表行数为负
    public const int COL_NOT_FOUND = 219; // 表中未找到指定名称列
    public const int BAD_SIMPLE = 220; // 非法SIMPLE关键字值
    public const int NO_SIMPLE = 221; // 主阵列未以SIMPLE开头
    public const int NO_BITPIX = 222; // 第二个关键字不是BITPIX
    public const int NO_NAXIS = 223; // 第三个关键字不是NAXIS
    public const int NO_NAXES = 224; // 未找到所有NAXISn关键字
    public const int NO_XTENSION = 225; // HDU未以XTENSION关键字开头
    public const int NOT_ATABLE = 226; // 当前HDU不是ASCII表扩展
    public const int NOT_BTABLE = 227; // 当前HDU不是二进制表扩展
    public const int NO_PCOUNT = 228; // 未找到PCOUNT关键字
    public const int NO_GCOUNT = 229; // 未找到GCOUNT关键字
    public const int NO_TFIELDS = 230; // 未找到TFIELDS关键字
    public const int NO_TBCOL = 231; // 未找到TBCOLn关键字
    public const int NO_TFORM = 232; // 未找到TFORMn关键字
    public const int NOT_IMAGE = 233; // 当前HDU不是图像扩展
    public const int BAD_TBCOL = 234; // TBCOLn值<0或>行长度
    public const int NOT_TABLE = 235; // 当前HDU不是表
    public const int COL_TOO_WIDE = 236; // 列宽超出表容量
    public const int COL_NOT_UNIQUE = 237; // 多列匹配模板
    public const int BAD_ROW_WIDTH = 241; // 列宽总和≠NAXIS1
    public const int UNKNOWN_EXT = 251; // 无法识别的FITS扩展类型
    public const int UNKNOWN_REC = 252; // 无法识别的FITS记录
    public const int END_JUNK = 253; // END关键字非空白
    public const int BAD_HEADER_FILL = 254; // 头填充区非空白
    public const int BAD_DATA_FILL = 255; // 数据填充区非空白/零
    public const int BAD_TFORM = 261; // 非法TFORM格式码
    public const int BAD_TFORM_DTYPE = 262; // 无法识别TFORM数据类型码
    public const int BAD_TDIM = 263; // 非法TDIMn关键字值
    public const int BAD_HEAP_PTR = 264; // 无效二进制表堆地址

    public const int BAD_HDU_NUM = 301; // HDU编号<1或>MAXHDU
    public const int BAD_COL_NUM = 302; // 列号<1或>tfields
    public const int NEG_FILE_POS = 304; // 尝试移动到文件开始之前
    public const int NEG_BYTES = 306; // 尝试读写负字节数
    public const int BAD_ROW_NUM = 307; // 表起始行号非法
    public const int BAD_ELEM_NUM = 308; // 向量起始元素号非法
    public const int NOT_ASCII_COL = 309; // 非ASCII字符串列
    public const int NOT_LOGICAL_COL = 310; // 非逻辑数据类型列
    public const int BAD_ATABLE_FORMAT = 311; // ASCII表列格式错误
    public const int BAD_BTABLE_FORMAT = 312; // 二进制表列格式错误
    public const int NO_NULL = 314; // 未定义空值
    public const int NOT_VARI_LEN = 317; // 非变长列
    public const int BAD_DIMEN = 320; // 数组维度非法
    public const int BAD_PIX_NUM = 321; // 首像素号>末像素号
    public const int ZERO_SCALE = 322; // BSCALE/TSCALn=0非法
    public const int NEG_AXIS = 323; // 轴长度<1非法

    public const int NOT_GROUP_TABLE = 340; // 非组表
    public const int HDU_ALREADY_MEMBER = 341; // HDU已为成员
    public const int MEMBER_NOT_FOUND = 342; // 未找到成员
    public const int GROUP_NOT_FOUND = 343; // 未找到组
    public const int BAD_GROUP_ID = 344; // 组ID错误
    public const int TOO_MANY_HDUS_TRACKED = 345; // 跟踪HDU过多
    public const int HDU_ALREADY_TRACKED = 346; // HDU已跟踪
    public const int BAD_OPTION = 347; // 选项错误
    public const int IDENTICAL_POINTERS = 348; // 指针相同
    public const int BAD_GROUP_ATTACH = 349; // 组附加错误
    public const int BAD_GROUP_DETACH = 350; // 组分离错误

    public const int BAD_I2C = 401; // 整数转格式化字符串错误
    public const int BAD_F2C = 402; // 浮点转格式化字符串错误
    public const int BAD_INTKEY = 403; // 无法解析关键字为整数
    public const int BAD_LOGICALKEY = 404; // 无法解析关键字为逻辑值
    public const int BAD_FLOATKEY = 405; // 无法解析关键字为浮点
    public const int BAD_DOUBLEKEY = 406; // 无法解析关键字为双精度
    public const int BAD_C2I = 407; // 格式化字符串转整数错误
    public const int BAD_C2F = 408; // 格式化字符串转浮点错误
    public const int BAD_C2D = 409; // 格式化字符串转双精度错误
    public const int BAD_DATATYPE = 410; // 关键字数据类型码错误
    public const int BAD_DECIM = 411; // 指定小数位数错误
    public const int NUM_OVERFLOW = 412; // 数据类型转换溢出
    public const int DATA_COMPRESSION_ERR = 413; // 图像压缩错误
    public const int DATA_DECOMPRESSION_ERR = 414; // 图像解压缩错误
    public const int NO_COMPRESSED_TILE = 415; // 压缩分块不存在

    public const int BAD_DATE = 420; // 日期/时间转换错误

    public const int PARSE_SYNTAX_ERR = 431; // 解析器表达式语法错误
    public const int PARSE_BAD_TYPE = 432; // 表达式非目标类型
    public const int PARSE_LRG_VECTOR = 433; // 向量结果过大
    public const int PARSE_NO_OUTPUT = 434; // 数据解析器无输出列
    public const int PARSE_BAD_COL = 435; // 解析列时数据错误
    public const int PARSE_BAD_OUTPUT = 436; // 输出文件类型错误

    public const int ANGLE_TOO_BIG = 501; // 天文角度超出投影范围
    public const int BAD_WCS_VAL = 502; // 天文坐标/像素值错误
    public const int WCS_ERROR = 503; // 天文坐标计算错误
    public const int BAD_WCS_PROJ = 504; // 不支持的天文投影类型
    public const int NO_WCS_KEY = 505; // 未找到天文坐标关键字
    public const int APPROX_WCS_KEY = 506; // 已计算近似WCS关键字

    public const int NO_CLOSE_ERROR = 999; // 内部禁用关闭错误信息

    // grparser.c 错误码
    public const int NGP_ERRBASE = 360; // 错误基值
    public const int NGP_OK = 0; // 无错误
    public const int NGP_NO_MEMORY = 360; // 内存分配失败
    public const int NGP_READ_ERR = 361; // 文件读取错误
    public const int NGP_NUL_PTR = 362; // 空指针参数
    public const int NGP_EMPTY_CURLINE = 363; // 当前行为空
    public const int NGP_UNREAD_QUEUE_FULL = 364; // 回读队列满
    public const int NGP_INC_NESTING = 365; // 包含文件嵌套过深
    public const int NGP_ERR_FOPEN = 366; // 文件打开失败
    public const int NGP_EOF = 367; // 已到文件末尾
    public const int NGP_BAD_ARG = 368; // 参数错误
    public const int NGP_TOKEN_NOT_EXPECT = 369; // 此处不期望标记符

    // FITS 文件 URL 解析相关函数
    // 从字符串中获取解析标记
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fits_get_token(ref IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)] string delimiter, System.Text.StringBuilder token, ref int isanumber);
    // 增强版获取解析标记
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fits_get_token2(ref IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)] string delimiter, ref IntPtr token, ref int isanumber, ref int status);
    // 分割逗号分隔的名称列表
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr fits_split_names(System.Text.StringBuilder list);
    // 解析完整的 FITS URL 字符串
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffiurl([MarshalAs(UnmanagedType.LPStr)] string url, System.Text.StringBuilder urltype, System.Text.StringBuilder infile, System.Text.StringBuilder outfile, System.Text.StringBuilder extspec, System.Text.StringBuilder rowfilter, System.Text.StringBuilder binspec, System.Text.StringBuilder colspec, ref int status);
    // 解析文件 URL 并拆分各组件
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffifile([MarshalAs(UnmanagedType.LPStr)] string url, System.Text.StringBuilder urltype, System.Text.StringBuilder infile, System.Text.StringBuilder outfile, System.Text.StringBuilder extspec, System.Text.StringBuilder rowfilter, System.Text.StringBuilder binspec, System.Text.StringBuilder colspec, System.Text.StringBuilder pixfilter, ref int status);
    // 完整版解析文件 URL
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffifile2([MarshalAs(UnmanagedType.LPStr)] string url, System.Text.StringBuilder urltype, System.Text.StringBuilder infile, System.Text.StringBuilder outfile, System.Text.StringBuilder extspec, System.Text.StringBuilder rowfilter, System.Text.StringBuilder binspec, System.Text.StringBuilder colspec, System.Text.StringBuilder pixfilter, System.Text.StringBuilder compspec, ref int status);
    // 从 URL 获取根文件名
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffrtnm([MarshalAs(UnmanagedType.LPStr)] string url, System.Text.StringBuilder rootname, ref int status);
    // 检查文件是否存在
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffexist([MarshalAs(UnmanagedType.LPStr)] string infile, ref int exists, ref int status);
    // 解析 HDU 扩展规格字符串
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffexts([MarshalAs(UnmanagedType.LPStr)] string extspec, ref int extnum, System.Text.StringBuilder extname, ref int extvers, ref int hdutype, System.Text.StringBuilder colname, System.Text.StringBuilder rowexpress, ref int status);
    // 从 URL 中提取扩展编号
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffextn([MarshalAs(UnmanagedType.LPStr)] string url, ref int extension_num, ref int status);
    // 获取文件的 URL 类型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffurlt(IntPtr fptr, System.Text.StringBuilder urlType, ref int status);
    // 解析分箱规格字符串
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffbins([MarshalAs(UnmanagedType.LPStr)] string binspec, ref int imagetype, ref int haxis, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] System.Text.StringBuilder[] colname, ref double minin, ref double maxin, ref double binsizein, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] System.Text.StringBuilder[] minname, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] System.Text.StringBuilder[] maxname, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] System.Text.StringBuilder[] binname, ref double weight, System.Text.StringBuilder wtname, ref int recip, ref int status);
    // 解析分箱范围参数
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffbinr(ref IntPtr binspec, System.Text.StringBuilder colname, ref double minin, ref double maxin, ref double binsizein, System.Text.StringBuilder minname, System.Text.StringBuilder maxname, System.Text.StringBuilder binname, ref int status);
    // 将表格单元格数据复制到图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fits_copy_cell2image(IntPtr fptr, IntPtr newptr, [MarshalAs(UnmanagedType.LPStr)] string colname, int rownum, ref int status);
    // 将图像数据复制到表格单元格
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fits_copy_image2cell(IntPtr fptr, IntPtr newptr, [MarshalAs(UnmanagedType.LPStr)] string colname, int rownum, int copykeyflag, ref int status);
    // 将像素列表复制到图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fits_copy_pixlist2image(IntPtr infptr, IntPtr outfptr, int firstkey, int naxis, ref int colnum, ref int status);
    // 导入文件内容到内存
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffimport_file([MarshalAs(UnmanagedType.LPStr)] string filename, ref IntPtr contents, ref int status);
    // 解析行范围列表
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffrwrg([MarshalAs(UnmanagedType.LPStr)] string rowlist, long maxrows, int maxranges, ref int numranges, ref int minrow, ref int maxrow, ref int status);
    // 64位版解析行范围列表
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffrwrgll([MarshalAs(UnmanagedType.LPStr)] string rowlist, long maxrows, int maxranges, ref int numranges, ref long minrow, ref long maxrow, ref int status);
    // FITS 文件 I/O 相关函数
    // 初始化 cfitsio 库
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fits_init_cfitsio();
    // 打开内存中的 FITS 文件
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffomem(ref IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string name, int mode, ref IntPtr buffptr, ref UIntPtr buffsize, UIntPtr deltasize, IntPtr mem_realloc, ref int status);
    // 打开 FITS 文件
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffopen(ref IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string filename, int iomode, ref int status);
    // 测试打开 FITS 文件
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffopentest(int soname, ref IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string filename, int iomode, ref int status);
    // 打开磁盘文件
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffdopn(ref IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string filename, int iomode, ref int status);
    // 打开并定位到指定扩展
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffeopn(ref IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string filename, int iomode, [MarshalAs(UnmanagedType.LPStr)] string extlist, ref int hdutype, ref int status);
    // 打开并定位到第一个表格
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fftopn(ref IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string filename, int iomode, ref int status);
    // 打开并定位到第一个图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffiopn(ref IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string filename, int iomode, ref int status);
    // 打开磁盘文件（内核模式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffdkopn(ref IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string filename, int iomode, ref int status);
    // 重新打开已打开的文件
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffreopen(IntPtr openfptr, ref IntPtr newfptr, ref int status);
    // 创建新 FITS 文件
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffinit(ref IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string filename, ref int status);
    // 内核模式创建新文件
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffdkinit(ref IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string filename, ref int status);
    // 创建内存中的 FITS 文件
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffimem(ref IntPtr fptr, ref IntPtr buffptr, ref UIntPtr buffsize, UIntPtr deltasize, IntPtr mem_realloc, ref int status);
    // 创建临时文件
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fftplt(ref IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string filename, System.Text.StringBuilder tempname, ref int status);
    // 刷新文件缓冲区
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffflus(IntPtr fptr, ref int status);
    // 刷新并清空缓冲区
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffflsh(IntPtr fptr, int clearbuf, ref int status);
    // 关闭 FITS 文件
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffclos(IntPtr fptr, ref int status);
    // 删除 FITS 文件
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffdelt(IntPtr fptr, ref int status);
    // 获取文件名
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffflnm(IntPtr fptr, System.Text.StringBuilder filename, ref int status);
    // 获取文件打开模式
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffflmd(IntPtr fptr, ref int filemode, ref int status);
    // 删除 IRAF 格式 FITS 文件
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fits_delete_iraf_file([MarshalAs(UnmanagedType.LPStr)] string filename, ref int status);

    // 通用工具函数
    // 获取 cfitsio 版本号
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern float ffvers(ref float version);
    // 字符串转大写
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern void ffupch(System.Text.StringBuilder str);
    // 获取错误信息文本
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern void ffgerr(int status, System.Text.StringBuilder errtext);
    // 打印错误信息
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern void ffpmsg([MarshalAs(UnmanagedType.LPStr)] string err_message);
    // 打印错误标记
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern void ffpmrk();
    // 获取错误信息
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgmsg(System.Text.StringBuilder err_message);
    // 清空错误信息
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern void ffcmsg();
    // 清空错误标记
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern void ffcmrk();
    // 报告错误信息到流
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern void ffrprt(IntPtr stream, int status);
    // 字符串匹配比较
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern void ffcmps([MarshalAs(UnmanagedType.LPStr)] string templt, [MarshalAs(UnmanagedType.LPStr)] string colname, int casesen, ref int match, ref int exact);
    // 检查关键字格式
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fftkey([MarshalAs(UnmanagedType.LPStr)] string keyword, ref int status);
    // 检查头记录格式
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fftrec(System.Text.StringBuilder card, ref int status);
    // 检查文件结构
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffnchk(IntPtr fptr, ref int status);
    // 构造带数字的关键字名
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffkeyn([MarshalAs(UnmanagedType.LPStr)] string keyroot, int value, System.Text.StringBuilder keyname, ref int status);
    // 构造带数字的关键字名（反向）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffnkey(int value, [MarshalAs(UnmanagedType.LPStr)] string keyroot, System.Text.StringBuilder keyname, ref int status);
    // 获取关键字类别
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkcl(System.Text.StringBuilder card);
    // 获取数据类型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffdtyp([MarshalAs(UnmanagedType.LPStr)] string cval, System.Text.StringBuilder dtype, ref int status);
    // 获取整数类型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffinttyp(System.Text.StringBuilder cval, ref int datatype, ref int negative, ref int status);
    // 解析头记录的值和注释
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpsvc(System.Text.StringBuilder card, System.Text.StringBuilder value, System.Text.StringBuilder comm, ref int status);
    // 从记录获取关键字名
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgknm(System.Text.StringBuilder card, System.Text.StringBuilder name, ref int length, ref int status);
    // 获取 HDU 类型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgthd([MarshalAs(UnmanagedType.LPStr)] string tmplt, System.Text.StringBuilder card, ref int hdtype, ref int status);
    // 创建关键字记录
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkky([MarshalAs(UnmanagedType.LPStr)] string keyname, System.Text.StringBuilder keyval, [MarshalAs(UnmanagedType.LPStr)] string comm, System.Text.StringBuilder card, ref int status);
    // 翻译单个关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fits_translate_keyword(System.Text.StringBuilder inrec, System.Text.StringBuilder outrec, IntPtr patterns, int npat, int n_value, int n_offset, int n_range, ref int pat_num, ref int i, ref int j, ref int m, ref int n, ref int status);
    // 批量翻译关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fits_translate_keywords(IntPtr infptr, IntPtr outfptr, int firstkey, IntPtr patterns, int npat, int n_value, int n_offset, int n_range, ref int status);
    // 解析 ASCII 表格式
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffasfm([MarshalAs(UnmanagedType.LPStr)] string tform, ref int datacode, ref int width, ref int decim, ref int status);
    // 解析二进制表格式
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffbnfm([MarshalAs(UnmanagedType.LPStr)] string tform, ref int datacode, ref int repeat, ref int width, ref int status);
    // 64位版解析二进制表格式
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffbnfmll([MarshalAs(UnmanagedType.LPStr)] string tform, ref int datacode, ref long repeat, ref int width, ref int status);
    // 计算 ASCII 表列偏移
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgabc(int tfields, IntPtr tform, int space, ref int rowlen, ref int tbcol, ref int status);
    // 获取段范围
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fits_get_section_range(ref IntPtr ptr, ref int secmin, ref int secmax, ref int incre, ref int status);
    // 移动文件指针到指定字节位置
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmbyt(IntPtr fptr, long bytpos, int ignore_err, ref int status);

    // 写入单个关键字函数
    // 写入任意类型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpky(IntPtr fptr, int datatype, [MarshalAs(UnmanagedType.LPStr)] string keyname, IntPtr value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入完整头记录
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffprec(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string card, ref int status);
    // 写入注释关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcom(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入单位关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpunt(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string unit, ref int status);
    // 写入历史关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffphis(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string history, ref int status);
    // 写入日期关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpdat(IntPtr fptr, ref int status);
    // 验证日期合法性
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffverifydate(int year, int month, int day, ref int status);
    // 获取时间字符串
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgstm(System.Text.StringBuilder timestr, ref int timeref, ref int status);
    // 获取系统日期
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsdt(ref int day, ref int month, ref int year, ref int status);
    // 日期转字符串
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffdt2s(int year, int month, int day, System.Text.StringBuilder datestr, ref int status);
    // 日期时间转字符串
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fftm2s(int year, int month, int day, int hour, int minute, double second, int decimals, System.Text.StringBuilder datestr, ref int status);
    // 字符串转日期
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffs2dt([MarshalAs(UnmanagedType.LPStr)] string datestr, ref int year, ref int month, ref int day, ref int status);
    // 字符串转日期时间
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffs2tm([MarshalAs(UnmanagedType.LPStr)] string datestr, ref int year, ref int month, ref int day, ref int hour, ref int minute, ref double second, ref int status);
    // 写入关键字（无值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkyu(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入字符串关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkys(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入长字符串关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkls(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入行分隔关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffplsw(IntPtr fptr, ref int status);
    // 写入逻辑关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkyl(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, int value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入64位整数关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkyj(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, long value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入无符号64位整数关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkyuj(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, ulong value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入浮点关键字（固定格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkyf(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入浮点关键字（科学计数法）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkye(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入双精度关键字（通用格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkyg(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入双精度关键字（固定格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkyd(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入单精度复数关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkyc(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, IntPtr value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入双精度复数关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkym(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, IntPtr value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入复数关键字（简化版）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkfc(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, IntPtr value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入双精度复数关键字（简化版）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkfm(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, IntPtr value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入时间格式关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkyt(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, int intval, double frac, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 写入维度关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffptdm(IntPtr fptr, int colnum, int naxis, IntPtr naxes, ref int status);
    // 64位版写入维度关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffptdmll(IntPtr fptr, int colnum, int naxis, IntPtr naxes, ref int status);

    // 批量写入关键字数组
    // 批量写入字符串类型关键字数组
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkns(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyroot, int nstart, int nkey, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] value, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] comm, ref int status);
    // 批量写入逻辑类型关键字数组
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpknl(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyroot, int nstart, int nkey, int[] value, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] comm, ref int status);
    // 批量写入长整型关键字数组
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpknj(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyroot, int nstart, int nkey, int[] value, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] comm, ref int status);
    // 批量写入64位整型关键字数组
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpknjj(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyroot, int nstart, int nkey, long[] value, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] comm, ref int status);
    // 批量写入浮点型关键字数组（固定格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpknf(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyroot, int nstart, int nkey, float[] value, int decim, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] comm, ref int status);
    // 批量写入浮点型关键字数组（科学计数法）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkne(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyroot, int nstart, int nkey, float[] value, int decim, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] comm, ref int status);
    // 批量写入双精度关键字数组（通用格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpkng(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyroot, int nstart, int nkey, double[] value, int decim, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] comm, ref int status);
    // 批量写入双精度关键字数组（固定格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpknd(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyroot, int nstart, int nkey, double[] value, int decim, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] comm, ref int status);
    // 复制列关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffcpky(IntPtr infptr, IntPtr outfptr, int incol, int outcol, [MarshalAs(UnmanagedType.LPStr)] string rootname, ref int status);

    // 写入必需的头关键字
    // 写入图像主头必需关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffphps(IntPtr fptr, int bitpix, int naxis, int[] naxes, ref int status);
    // 写入图像主头必需关键字（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffphpsll(IntPtr fptr, int bitpix, int naxis, long[] naxes, ref int status);
    // 写入主头基础必需关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffphpr(IntPtr fptr, int simple, int bitpix, int naxis, int[] naxes, long pcount, long gcount, int extend, ref int status);
    // 写入主头基础必需关键字（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffphprll(IntPtr fptr, int simple, int bitpix, int naxis, long[] naxes, long pcount, long gcount, int extend, ref int status);
    // 写入ASCII表必需关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffphtb(IntPtr fptr, long naxis1, long naxis2, int tfields, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] ttype, long[] tbcol, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tform, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tunit, [MarshalAs(UnmanagedType.LPStr)] string extname, ref int status);
    // 写入二进制表必需关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffphbn(IntPtr fptr, long naxis2, int tfields, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] ttype, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tform, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tunit, [MarshalAs(UnmanagedType.LPStr)] string extname, long pcount, ref int status);
    // 写入扩展头必需关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffphext(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string xtension, int bitpix, int naxis, int[] naxes, long pcount, long gcount, ref int status);

    // 写入模板关键字
    // 从模板文件写入关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpktp(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string filename, ref int status);

    // 获取头信息
    // 获取头记录总数
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffghsp(IntPtr fptr, ref int nexist, ref int nmore, ref int status);
    // 获取头位置信息
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffghps(IntPtr fptr, ref int nexist, ref int position, ref int status);

    // 移动头指针位置
    // 移动到指定记录位置
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmaky(IntPtr fptr, int nrec, ref int status);
    // 相对移动记录位置
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmrky(IntPtr fptr, int nrec, ref int status);

    // 读取单个关键字
    // 读取下一个匹配关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgnxk(IntPtr fptr, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] inclist, int ninc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] exclist, int nexc, System.Text.StringBuilder card, ref int status);
    // 按记录号读取关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgrec(IntPtr fptr, int nrec, System.Text.StringBuilder card, ref int status);
    // 按关键字名读取记录
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcrd(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, System.Text.StringBuilder card, ref int status);
    // 按字符串搜索读取记录
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgstr(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string str, System.Text.StringBuilder card, ref int status);
    // 读取关键字单位
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgunt(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, System.Text.StringBuilder unit, ref int status);
    // 按序号读取关键字信息
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkyn(IntPtr fptr, int nkey, System.Text.StringBuilder keyname, System.Text.StringBuilder keyval, System.Text.StringBuilder comm, ref int status);
    // 按名称读取关键字值和注释
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkey(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, System.Text.StringBuilder keyval, System.Text.StringBuilder comm, ref int status);
    // 读取任意类型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgky(IntPtr fptr, int datatype, [MarshalAs(UnmanagedType.LPStr)] string keyname, IntPtr value, System.Text.StringBuilder comm, ref int status);
    // 读取字符串关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkys(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, System.Text.StringBuilder value, System.Text.StringBuilder comm, ref int status);
    // 读取关键字长度
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgksl(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, ref int length, ref int status);
    // 读取关键字和注释长度
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkcsl(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, ref int length, ref int comlength, ref int status);
    // 读取长字符串关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkls(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, ref IntPtr value, System.Text.StringBuilder comm, ref int status);
    // 分段读取字符串关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsky(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, int firstchar, int maxchar, System.Text.StringBuilder value, ref int valuelen, System.Text.StringBuilder comm, ref int status);
    // 完整分段读取关键字和注释
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgskyc(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, int firstchar, int maxchar, int maxcomchar, System.Text.StringBuilder value, ref int valuelen, System.Text.StringBuilder comm, ref int comlen, ref int status);
    // 释放内存
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fffree(IntPtr value, ref int status);
    // 读取逻辑关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkyl(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, ref int value, System.Text.StringBuilder comm, ref int status);
    // 读取长整型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkyj(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, ref int value, System.Text.StringBuilder comm, ref int status);
    // 读取64位整型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkyjj(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, ref long value, System.Text.StringBuilder comm, ref int status);
    // 读取无符号64位整型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkyujj(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, ref ulong value, System.Text.StringBuilder comm, ref int status);
    // 读取浮点型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkye(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, ref float value, System.Text.StringBuilder comm, ref int status);
    // 读取双精度关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkyd(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, ref double value, System.Text.StringBuilder comm, ref int status);
    // 读取单精度复数关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkyc(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float[] value, System.Text.StringBuilder comm, ref int status);
    // 读取双精度复数关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkym(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double[] value, System.Text.StringBuilder comm, ref int status);
    // 读取时间格式关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkyt(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, ref int ivalue, ref double dvalue, System.Text.StringBuilder comm, ref int status);
    // 读取维度信息关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtdm(IntPtr fptr, int colnum, int maxdim, ref int naxis, int[] naxes, ref int status);
    // 读取维度信息关键字（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtdmll(IntPtr fptr, int colnum, int maxdim, ref int naxis, long[] naxes, ref int status);
    // 解析维度字符串
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffdtdm(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string tdimstr, int colnum, int maxdim, ref int naxis, int[] naxes, ref int status);
    // 解析维度字符串（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffdtdmll(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string tdimstr, int colnum, int maxdim, ref int naxis, long[] naxes, ref int status);

    // 读取关键字数组
    // 批量读取字符串关键字数组
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkns(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, int nstart, int nmax, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] value, ref int nfound, ref int status);
    // 批量读取逻辑关键字数组
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgknl(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, int nstart, int nmax, int[] value, ref int nfound, ref int status);
    // 批量读取长整型关键字数组
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgknj(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, int nstart, int nmax, int[] value, ref int nfound, ref int status);
    // 批量读取64位整型关键字数组
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgknjj(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, int nstart, int nmax, long[] value, ref int nfound, ref int status);
    // 批量读取浮点型关键字数组
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgkne(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, int nstart, int nmax, float[] value, ref int nfound, ref int status);
    // 批量读取双精度关键字数组
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgknd(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, int nstart, int nmax, double[] value, ref int nfound, ref int status);
    // 将头信息转为字符串
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffh2st(IntPtr fptr, ref IntPtr header, ref int status);
    // 将头信息转为字符串（可排除注释）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffhdr2str(IntPtr fptr, int exclude_comm, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] exclist, int nexc, ref IntPtr header, ref int nkeys, ref int status);
    // 转换头信息为字符串
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffcnvthdr2str(IntPtr fptr, int exclude_comm, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] exclist, int nexc, ref IntPtr header, ref int nkeys, ref int status);

    // 读取必需的头关键字
    // 读取主头必需关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffghpr(IntPtr fptr, int maxdim, ref int simple, ref int bitpix, ref int naxis, int[] naxes, ref int pcount, ref int gcount, ref int extend, ref int status);
    // 读取主头必需关键字（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffghprll(IntPtr fptr, int maxdim, ref int simple, ref int bitpix, ref int naxis, long[] naxes, ref int pcount, ref int gcount, ref int extend, ref int status);
    // 读取ASCII表必需关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffghtb(IntPtr fptr, int maxfield, ref int naxis1, ref int naxis2, ref int tfields, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] ttype, long[] tbcol, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tform, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tunit, System.Text.StringBuilder extname, ref int status);
    // 读取ASCII表必需关键字（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffghtbll(IntPtr fptr, int maxfield, ref long naxis1, ref long naxis2, ref int tfields, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] ttype, long[] tbcol, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tform, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tunit, System.Text.StringBuilder extname, ref int status);
    // 读取二进制表必需关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffghbn(IntPtr fptr, int maxfield, ref int naxis2, ref int tfields, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] ttype, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tform, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tunit, System.Text.StringBuilder extname, ref int pcount, ref int status);
    // 读取二进制表必需关键字（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffghbnll(IntPtr fptr, int maxfield, ref long naxis2, ref int tfields, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] ttype, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tform, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tunit, System.Text.StringBuilder extname, ref long pcount, ref int status);

    /*--------------------- update keywords ---------------*/
    // 通用更新任意类型关键字的值与注释
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffuky(IntPtr fptr, int datatype, [MarshalAs(UnmanagedType.LPStr)] string keyname, IntPtr value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 根据关键字名更新整行关键字记录
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffucrd(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string card, ref int status);
    // 仅更新关键字注释，不修改值
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffukyu(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 更新字符串类型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffukys(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 更新长字符串类型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffukls(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 更新逻辑/布尔类型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffukyl(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, int value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 更新64位整型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffukyj(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, long value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 更新无符号64位整型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffukyuj(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, ulong value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 更新浮点型关键字（固定格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffukyf(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 更新浮点型关键字（科学计数法）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffukye(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 更新双精度关键字（通用格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffukyg(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 更新双精度关键字（固定格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffukyd(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 更新单精度复数关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffukyc(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float[] value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 更新双精度复数关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffukym(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double[] value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 更新单精度复数关键字（简化版）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffukfc(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float[] value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 更新双精度复数关键字（简化版）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffukfm(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double[] value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);

    /*--------------------- modify keywords ---------------*/
    // 修改指定序号的关键字记录
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmrec(IntPtr fptr, int nkey, [MarshalAs(UnmanagedType.LPStr)] string card, ref int status);
    // 根据关键字名修改关键字记录
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmcrd(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string card, ref int status);
    // 修改关键字的名称
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmnam(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string oldname, [MarshalAs(UnmanagedType.LPStr)] string newname, ref int status);
    // 修改关键字的注释信息
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmcom(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 修改关键字注释（仅注释）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkyu(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 修改字符串类型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkys(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 修改长字符串类型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkls(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 修改逻辑/布尔类型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkyl(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, int value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 修改64位整型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkyj(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, long value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 修改无符号64位整型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkyuj(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, ulong value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 修改浮点型关键字（固定格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkyf(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 修改浮点型关键字（科学计数法）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkye(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 修改双精度关键字（通用格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkyg(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 修改双精度关键字（固定格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkyd(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 修改单精度复数关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkyc(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float[] value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 修改双精度复数关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkym(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double[] value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 修改单精度复数关键字（简化版）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkfc(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float[] value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 修改双精度复数关键字（简化版）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkfm(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double[] value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);

    /*--------------------- insert keywords ---------------*/
    // 在指定位置插入关键字记录
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffirec(IntPtr fptr, int nkey, [MarshalAs(UnmanagedType.LPStr)] string card, ref int status);
    // 插入关键字记录（自动位置）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffikey(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string card, ref int status);
    // 插入仅含注释的关键字（无值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffikyu(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 插入字符串类型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffikys(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 插入长字符串类型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffikls(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, [MarshalAs(UnmanagedType.LPStr)] string value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 插入逻辑/布尔类型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffikyl(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, int value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 插入64位整型关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffikyj(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, long value, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 插入浮点型关键字（固定格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffikyf(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 插入浮点型关键字（科学计数法）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffikye(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 插入双精度关键字（通用格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffikyg(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 插入双精度关键字（固定格式）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffikyd(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 插入单精度复数关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffikyc(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float[] value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 插入双精度复数关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffikym(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double[] value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 插入单精度复数关键字（简化版）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffikfc(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, float[] value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);
    // 插入双精度复数关键字（简化版）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffikfm(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, double[] value, int decim, [MarshalAs(UnmanagedType.LPStr)] string comm, ref int status);

    /*--------------------- delete keywords ---------------*/
    // 根据关键字名删除关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffdkey(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string keyname, ref int status);
    // 根据字符串匹配删除关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffdstr(IntPtr fptr, [MarshalAs(UnmanagedType.LPStr)] string str, ref int status);
    // 根据位置删除关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffdrec(IntPtr fptr, int keypos, ref int status);

    /*--------------------- get HDU information -------------*/
    // 获取当前HDU编号
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffghdn(IntPtr fptr, ref int chdunum);
    // 获取HDU扩展类型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffghdt(IntPtr fptr, ref int exttype, ref int status);
    // 获取HDU偏移地址信息（32位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffghad(IntPtr fptr, ref int headstart, ref int datastart, ref int dataend, ref int status);
    // 获取HDU偏移地址信息（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffghadll(IntPtr fptr, ref long headstart, ref long datastart, ref long dataend, ref int status);
    // 获取HDU偏移地址（OFF_T类型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffghof(IntPtr fptr, ref IntPtr headstart, ref IntPtr datastart, ref IntPtr dataend, ref int status);
    // 获取图像参数信息（32位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgipr(IntPtr fptr, int maxaxis, ref int imgtype, ref int naxis, int[] naxes, ref int status);
    // 获取图像参数信息（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgiprll(IntPtr fptr, int maxaxis, ref int imgtype, ref int naxis, long[] naxes, ref int status);
    // 获取图像数据类型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgidt(IntPtr fptr, ref int imgtype, ref int status);
    // 获取图像扩展类型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgiet(IntPtr fptr, ref int imgtype, ref int status);
    // 获取图像维度数量
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgidm(IntPtr fptr, ref int naxis, ref int status);
    // 获取图像尺寸信息（32位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgisz(IntPtr fptr, int nlen, int[] naxes, ref int status);
    // 获取图像尺寸信息（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgiszll(IntPtr fptr, int nlen, long[] naxes, ref int status);

    /*--------------------- HDU operations -------------*/
    // 移动到指定编号的HDU
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmahd(IntPtr fptr, int hdunum, ref int exttype, ref int status);
    // 相对移动指定数量HDU
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmrhd(IntPtr fptr, int hdumov, ref int exttype, ref int status);
    // 按名称和版本移动到指定HDU
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffmnhd(IntPtr fptr, int exttype, [MarshalAs(UnmanagedType.LPStr)] string hduname, int hduvers, ref int status);
    // 获取文件中HDU总数
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffthdu(IntPtr fptr, ref int nhdu, ref int status);
    // 创建新的空HDU
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffcrhd(IntPtr fptr, ref int status);
    // 创建新图像HDU（32位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffcrim(IntPtr fptr, int bitpix, int naxis, int[] naxes, ref int status);
    // 创建新图像HDU（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffcrimll(IntPtr fptr, int bitpix, int naxis, long[] naxes, ref int status);
    // 创建新表格HDU
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffcrtb(IntPtr fptr, int tbltype, long naxis2, int tfields, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] ttype, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tform, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tunit, [MarshalAs(UnmanagedType.LPStr)] string extname, ref int status);
    // 插入图像HDU（32位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffiimg(IntPtr fptr, int bitpix, int naxis, int[] naxes, ref int status);
    // 插入图像HDU（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffiimgll(IntPtr fptr, int bitpix, int naxis, long[] naxes, ref int status);
    // 插入ASCII表格HDU
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffitab(IntPtr fptr, long naxis1, long naxis2, int tfields, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] ttype, long[] tbcol, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tform, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tunit, [MarshalAs(UnmanagedType.LPStr)] string extname, ref int status);
    // 插入二进制表格HDU
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffibin(IntPtr fptr, long naxis2, int tfields, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] ttype, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tform, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] tunit, [MarshalAs(UnmanagedType.LPStr)] string extname, long pcount, ref int status);
    // 调整图像HDU大小（32位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffrsim(IntPtr fptr, int bitpix, int naxis, int[] naxes, ref int status);
    // 调整图像HDU大小（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffrsimll(IntPtr fptr, int bitpix, int naxis, long[] naxes, ref int status);
    // 删除当前HDU
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffdhdu(IntPtr fptr, ref int hdutype, ref int status);
    // 复制整个HDU
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffcopy(IntPtr infptr, IntPtr outfptr, int morekeys, ref int status);
    // 部分复制HDU内容
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffcpfl(IntPtr infptr, IntPtr outfptr, int prev, int cur, int follow, ref int status);
    // 仅复制HDU头
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffcphd(IntPtr infptr, IntPtr outfptr, ref int status);
    // 仅复制HDU数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffcpdt(IntPtr infptr, IntPtr outfptr, ref int status);
    // 更改文件打开模式
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffchfl(IntPtr fptr, ref int status);
    // 关闭文件并删除
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffcdfl(IntPtr fptr, ref int status);
    // 将HDU写入流
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffwrhdu(IntPtr fptr, IntPtr outstream, ref int status);
    // 读取HDU定义
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffrdef(IntPtr fptr, ref int status);
    // 读取下一个HDU
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffrhdu(IntPtr fptr, ref int hdutype, ref int status);
    // 写入HDU定义
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffhdef(IntPtr fptr, int morekeys, ref int status);
    // 设置堆偏移地址
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpthp(IntPtr fptr, int theap, ref int status);
    // 计算校验和
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffcsum(IntPtr fptr, int nrec, ref uint sum, ref int status);
    // 编码校验和为字符串
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern void ffesum(uint sum, int complm, System.Text.StringBuilder ascii);
    // 解码字符串为校验和
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern uint ffdsum([MarshalAs(UnmanagedType.LPStr)] string ascii, int complm, ref uint sum);
    // 写入校验和关键字
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcks(IntPtr fptr, ref int status);
    // 更新校验和
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffupck(IntPtr fptr, ref int status);
    // 校验校验和有效性
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffvcks(IntPtr fptr, ref int datastatus, ref int hdustatus, ref int status);
    // 获取校验和数值
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcks(IntPtr fptr, ref uint datasum, ref uint hdusum, ref int status);

    /*--------------------- define scaling or null values -------------*/
    // 设置图像缩放系数和零点
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpscl(IntPtr fptr, double scale, double zeroval, ref int status);
    // 设置图像空值
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffpnul(IntPtr fptr, long nulvalue, ref int status);
    // 设置表格列缩放系数和零点
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fftscl(IntPtr fptr, int colnum, double scale, double zeroval, ref int status);
    // 设置表格列数值空值
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int fftnul(IntPtr fptr, int colnum, long nulvalue, ref int status);
    // 设置表格列字符串空值
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffsnul(IntPtr fptr, int colnum, [MarshalAs(UnmanagedType.LPStr)] string nulstring, ref int status);

    /*--------------------- get column information -------------*/
    // 根据模板查找列号
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcno(IntPtr fptr, int casesen, [MarshalAs(UnmanagedType.LPStr)] string templt, ref int colnum, ref int status);
    // 根据模板查找列名与列号
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcnn(IntPtr fptr, int casesen, [MarshalAs(UnmanagedType.LPStr)] string templt, [MarshalAs(UnmanagedType.LPStr)] string colname, ref int colnum, ref int status);
    // 获取列数据类型信息（32位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtcl(IntPtr fptr, int colnum, ref int typecode, ref int repeat, ref int width, ref int status);
    // 获取列数据类型信息（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtclll(IntPtr fptr, int colnum, ref int typecode, ref long repeat, ref long width, ref int status);
    // 获取等效数据类型（32位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffeqty(IntPtr fptr, int colnum, ref int typecode, ref int repeat, ref int width, ref int status);
    // 获取等效数据类型（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffeqtyll(IntPtr fptr, int colnum, ref int typecode, ref long repeat, ref long width, ref int status);
    // 获取表格总列数
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgncl(IntPtr fptr, ref int ncols, ref int status);
    // 获取表格行数（32位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgnrw(IntPtr fptr, ref int nrows, ref int status);
    // 获取表格行数（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgnrwll(IntPtr fptr, ref long nrows, ref int status);
    // 获取完整列定义信息
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgacl(IntPtr fptr, int colnum, [MarshalAs(UnmanagedType.LPStr)] string ttype, ref int tbcol, [MarshalAs(UnmanagedType.LPStr)] string tunit, [MarshalAs(UnmanagedType.LPStr)] string tform, ref double tscal, ref double tzero, [MarshalAs(UnmanagedType.LPStr)] string tnull, [MarshalAs(UnmanagedType.LPStr)] string tdisp, ref int status);
    // 获取二进制列详细信息（32位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgbcl(IntPtr fptr, int colnum, [MarshalAs(UnmanagedType.LPStr)] string ttype, [MarshalAs(UnmanagedType.LPStr)] string tunit, [MarshalAs(UnmanagedType.LPStr)] string dtype, ref int repeat, ref double tscal, ref double tzero, ref int tnull, [MarshalAs(UnmanagedType.LPStr)] string tdisp, ref int status);
    // 获取二进制列详细信息（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgbclll(IntPtr fptr, int colnum, [MarshalAs(UnmanagedType.LPStr)] string ttype, [MarshalAs(UnmanagedType.LPStr)] string tunit, [MarshalAs(UnmanagedType.LPStr)] string dtype, ref long repeat, ref double tscal, ref double tzero, ref long tnull, [MarshalAs(UnmanagedType.LPStr)] string tdisp, ref int status);
    // 获取表格行大小
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgrsz(IntPtr fptr, ref int nrows, ref int status);
    // 获取列显示宽度
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcdw(IntPtr fptr, int colnum, ref int width, ref int status);

    /*--------------------- read primary array or image elements -------------*/
    // 读取图像像素值（通用，32位起始）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpxv(IntPtr fptr, int datatype, int[] firstpix, long nelem, IntPtr nulval, IntPtr array, ref int anynul, ref int status);
    // 读取图像像素值（通用，64位起始）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpxvll(IntPtr fptr, int datatype, long[] firstpix, long nelem, IntPtr nulval, IntPtr array, ref int anynul, ref int status);
    // 读取图像像素并标记空值（32位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpxf(IntPtr fptr, int datatype, int[] firstpix, long nelem, IntPtr array, IntPtr nullarray, ref int anynul, ref int status);
    // 读取图像像素并标记空值（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpxfll(IntPtr fptr, int datatype, long[] firstpix, long nelem, IntPtr array, IntPtr nullarray, ref int anynul, ref int status);
    // 读取图像子区域数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsv(IntPtr fptr, int datatype, int[] blc, int[] trc, int[] inc, IntPtr nulval, IntPtr array, ref int anynul, ref int status);
    // 读取连续像素值（通用）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpv(IntPtr fptr, int datatype, long firstelem, long nelem, IntPtr nulval, IntPtr array, ref int anynul, ref int status);
    // 读取连续像素并标记空值
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpf(IntPtr fptr, int datatype, long firstelem, long nelem, IntPtr array, IntPtr nullarray, ref int anynul, ref int status);

    // 读取字节型像素
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpvb(IntPtr fptr, int group, long firstelem, long nelem, byte nulval, byte[] array, ref int anynul, ref int status);
    // 读取有符号字节型像素
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpvsb(IntPtr fptr, int group, long firstelem, long nelem, sbyte nulval, sbyte[] array, ref int anynul, ref int status);
    // 读取无符号短整型像素
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpvui(IntPtr fptr, int group, long firstelem, long nelem, ushort nulval, ushort[] array, ref int anynul, ref int status);
    // 读取短整型像素
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpvi(IntPtr fptr, int group, long firstelem, long nelem, short nulval, short[] array, ref int anynul, ref int status);
    // 读取无符号长整型像素
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpvuj(IntPtr fptr, int group, long firstelem, long nelem, uint nulval, uint[] array, ref int anynul, ref int status);
    // 读取长整型像素
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpvj(IntPtr fptr, int group, long firstelem, long nelem, int nulval, int[] array, ref int anynul, ref int status);
    // 读取无符号64位整型像素
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpvujj(IntPtr fptr, int group, long firstelem, long nelem, ulong nulval, ulong[] array, ref int anynul, ref int status);
    // 读取64位整型像素
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpvjj(IntPtr fptr, int group, long firstelem, long nelem, long nulval, long[] array, ref int anynul, ref int status);
    // 读取无符号整型像素
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpvuk(IntPtr fptr, int group, long firstelem, long nelem, uint nulval, uint[] array, ref int anynul, ref int status);
    // 读取整型像素
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpvk(IntPtr fptr, int group, long firstelem, long nelem, int nulval, int[] array, ref int anynul, ref int status);
    // 读取浮点型像素
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpve(IntPtr fptr, int group, long firstelem, long nelem, float nulval, float[] array, ref int anynul, ref int status);
    // 读取双精度像素
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpvd(IntPtr fptr, int group, long firstelem, long nelem, double nulval, double[] array, ref int anynul, ref int status);

    // 读取字节型像素（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpfb(IntPtr fptr, int group, long firstelem, long nelem, byte[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取有符号字节型像素（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpfsb(IntPtr fptr, int group, long firstelem, long nelem, sbyte[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取无符号短整型像素（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpfui(IntPtr fptr, int group, long firstelem, long nelem, ushort[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取短整型像素（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpfi(IntPtr fptr, int group, long firstelem, long nelem, short[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取无符号长整型像素（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpfuj(IntPtr fptr, int group, long firstelem, long nelem, uint[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取长整型像素（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpfj(IntPtr fptr, int group, long firstelem, long nelem, int[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取无符号64位整型像素（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpfujj(IntPtr fptr, int group, long firstelem, long nelem, ulong[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取64位整型像素（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpfjj(IntPtr fptr, int group, long firstelem, long nelem, long[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取无符号整型像素（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpfuk(IntPtr fptr, int group, long firstelem, long nelem, uint[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取整型像素（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpfk(IntPtr fptr, int group, long firstelem, long nelem, int[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取浮点型像素（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpfe(IntPtr fptr, int group, long firstelem, long nelem, float[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取双精度像素（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgpfd(IntPtr fptr, int group, long firstelem, long nelem, double[] array, IntPtr nularray, ref int anynul, ref int status);

    // 读取2D字节图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg2db(IntPtr fptr, int group, byte nulval, long ncols, long naxis1, long naxis2, byte[] array, ref int anynul, ref int status);
    // 读取2D有符号字节图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg2dsb(IntPtr fptr, int group, sbyte nulval, long ncols, long naxis1, long naxis2, sbyte[] array, ref int anynul, ref int status);
    // 读取2D无符号短整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg2dui(IntPtr fptr, int group, ushort nulval, long ncols, long naxis1, long naxis2, ushort[] array, ref int anynul, ref int status);
    // 读取2D短整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg2di(IntPtr fptr, int group, short nulval, long ncols, long naxis1, long naxis2, short[] array, ref int anynul, ref int status);
    // 读取2D无符号长整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg2duj(IntPtr fptr, int group, uint nulval, long ncols, long naxis1, long naxis2, uint[] array, ref int anynul, ref int status);
    // 读取2D长整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg2dj(IntPtr fptr, int group, int nulval, long ncols, long naxis1, long naxis2, int[] array, ref int anynul, ref int status);
    // 读取2D无符号64位整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg2dujj(IntPtr fptr, int group, ulong nulval, long ncols, long naxis1, long naxis2, ulong[] array, ref int anynul, ref int status);
    // 读取2D 64位整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg2djj(IntPtr fptr, int group, long nulval, long ncols, long naxis1, long naxis2, long[] array, ref int anynul, ref int status);
    // 读取2D无符号整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg2duk(IntPtr fptr, int group, uint nulval, long ncols, long naxis1, long naxis2, uint[] array, ref int anynul, ref int status);
    // 读取2D整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg2dk(IntPtr fptr, int group, int nulval, long ncols, long naxis1, long naxis2, int[] array, ref int anynul, ref int status);
    // 读取2D浮点型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg2de(IntPtr fptr, int group, float nulval, long ncols, long naxis1, long naxis2, float[] array, ref int anynul, ref int status);
    // 读取2D双精度图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg2dd(IntPtr fptr, int group, double nulval, long ncols, long naxis1, long naxis2, double[] array, ref int anynul, ref int status);

    // 读取3D字节图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg3db(IntPtr fptr, int group, byte nulval, long ncols, long nrows, long naxis1, long naxis2, long naxis3, byte[] array, ref int anynul, ref int status);
    // 读取3D有符号字节图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg3dsb(IntPtr fptr, int group, sbyte nulval, long ncols, long nrows, long naxis1, long naxis2, long naxis3, sbyte[] array, ref int anynul, ref int status);
    // 读取3D无符号短整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg3dui(IntPtr fptr, int group, ushort nulval, long ncols, long nrows, long naxis1, long naxis2, long naxis3, ushort[] array, ref int anynul, ref int status);
    // 读取3D短整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg3di(IntPtr fptr, int group, short nulval, long ncols, long nrows, long naxis1, long naxis2, long naxis3, short[] array, ref int anynul, ref int status);
    // 读取3D无符号长整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg3duj(IntPtr fptr, int group, uint nulval, long ncols, long nrows, long naxis1, long naxis2, long naxis3, uint[] array, ref int anynul, ref int status);
    // 读取3D长整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg3dj(IntPtr fptr, int group, int nulval, long ncols, long nrows, long naxis1, long naxis2, long naxis3, int[] array, ref int anynul, ref int status);
    // 读取3D无符号64位整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg3dujj(IntPtr fptr, int group, ulong nulval, long ncols, long nrows, long naxis1, long naxis2, long naxis3, ulong[] array, ref int anynul, ref int status);
    // 读取3D 64位整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg3djj(IntPtr fptr, int group, long nulval, long ncols, long nrows, long naxis1, long naxis2, long naxis3, long[] array, ref int anynul, ref int status);
    // 读取3D无符号整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg3duk(IntPtr fptr, int group, uint nulval, long ncols, long nrows, long naxis1, long naxis2, long naxis3, uint[] array, ref int anynul, ref int status);
    // 读取3D整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg3dk(IntPtr fptr, int group, int nulval, long ncols, long nrows, long naxis1, long naxis2, long naxis3, int[] array, ref int anynul, ref int status);
    // 读取3D浮点型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg3de(IntPtr fptr, int group, float nulval, long ncols, long nrows, long naxis1, long naxis2, long naxis3, float[] array, ref int anynul, ref int status);
    // 读取3D双精度图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffg3dd(IntPtr fptr, int group, double nulval, long ncols, long nrows, long naxis1, long naxis2, long naxis3, double[] array, ref int anynul, ref int status);

    // 读取表格列子区域（字节型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsvb(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, byte nulval, byte[] array, ref int anynul, ref int status);
    // 读取表格列子区域（有符号字节）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsvsb(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, sbyte nulval, sbyte[] array, ref int anynul, ref int status);
    // 读取表格列子区域（无符号短整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsvui(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, ushort nulval, ushort[] array, ref int anynul, ref int status);
    // 读取表格列子区域（短整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsvi(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, short nulval, short[] array, ref int anynul, ref int status);
    // 读取表格列子区域（无符号长整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsvuj(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, uint nulval, uint[] array, ref int anynul, ref int status);
    // 读取表格列子区域（长整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsvj(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, int nulval, int[] array, ref int anynul, ref int status);
    // 读取表格列子区域（无符号64位整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsvujj(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, ulong nulval, ulong[] array, ref int anynul, ref int status);
    // 读取表格列子区域（64位整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsvjj(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, long nulval, long[] array, ref int anynul, ref int status);
    // 读取表格列子区域（无符号整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsvuk(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, uint nulval, uint[] array, ref int anynul, ref int status);
    // 读取表格列子区域（整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsvk(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, int nulval, int[] array, ref int anynul, ref int status);
    // 读取表格列子区域（浮点型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsve(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, float nulval, float[] array, ref int anynul, ref int status);
    // 读取表格列子区域（双精度）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsvd(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, double nulval, double[] array, ref int anynul, ref int status);

    // 读取表格列子区域并标记空值（字节）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsfb(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, byte[] array, IntPtr flagval, ref int anynul, ref int status);
    // 读取表格列子区域并标记空值（有符号字节）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsfsb(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, sbyte[] array, IntPtr flagval, ref int anynul, ref int status);
    // 读取表格列子区域并标记空值（无符号短整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsfui(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, ushort[] array, IntPtr flagval, ref int anynul, ref int status);
    // 读取表格列子区域并标记空值（短整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsfi(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, short[] array, IntPtr flagval, ref int anynul, ref int status);
    // 读取表格列子区域并标记空值（无符号长整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsfuj(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, uint[] array, IntPtr flagval, ref int anynul, ref int status);
    // 读取表格列子区域并标记空值（长整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsfj(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, int[] array, IntPtr flagval, ref int anynul, ref int status);
    // 读取表格列子区域并标记空值（无符号64位整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsfujj(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, ulong[] array, IntPtr flagval, ref int anynul, ref int status);
    // 读取表格列子区域并标记空值（64位整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsfjj(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, long[] array, IntPtr flagval, ref int anynul, ref int status);
    // 读取表格列子区域并标记空值（无符号整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsfuk(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, uint[] array, IntPtr flagval, ref int anynul, ref int status);
    // 读取表格列子区域并标记空值（整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsfk(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, int[] array, IntPtr flagval, ref int anynul, ref int status);
    // 读取表格列子区域并标记空值（浮点型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsfe(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, float[] array, IntPtr flagval, ref int anynul, ref int status);
    // 读取表格列子区域并标记空值（双精度）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffgsfd(IntPtr fptr, int colnum, int naxis, int[] naxes, int[] blc, int[] trc, int[] inc, double[] array, IntPtr flagval, ref int anynul, ref int status);

    // 读取图像组像素（字节）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffggpb(IntPtr fptr, int group, int firstelem, int nelem, byte[] array, ref int status);
    // 读取图像组像素（有符号字节）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffggpsb(IntPtr fptr, int group, int firstelem, int nelem, sbyte[] array, ref int status);
    // 读取图像组像素（无符号短整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffggpui(IntPtr fptr, int group, int firstelem, int nelem, ushort[] array, ref int status);
    // 读取图像组像素（短整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffggpi(IntPtr fptr, int group, int firstelem, int nelem, short[] array, ref int status);
    // 读取图像组像素（无符号长整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffggpuj(IntPtr fptr, int group, int firstelem, int nelem, uint[] array, ref int status);
    // 读取图像组像素（长整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffggpj(IntPtr fptr, int group, int firstelem, int nelem, int[] array, ref int status);
    // 读取图像组像素（无符号64位整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffggpujj(IntPtr fptr, int group, int firstelem, int nelem, ulong[] array, ref int status);
    // 读取图像组像素（64位整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffggpjj(IntPtr fptr, int group, int firstelem, int nelem, long[] array, ref int status);
    // 读取图像组像素（无符号整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffggpuk(IntPtr fptr, int group, int firstelem, int nelem, uint[] array, ref int status);
    // 读取图像组像素（整型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffggpk(IntPtr fptr, int group, int firstelem, int nelem, int[] array, ref int status);
    // 读取图像组像素（浮点型）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffggpe(IntPtr fptr, int group, int firstelem, int nelem, float[] array, ref int status);
    // 读取图像组像素（双精度）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)] public static extern int ffggpd(IntPtr fptr, int group, int firstelem, int nelem, double[] array, ref int status);

    /*--------------------- read column elements -------------*/
    // 通用读取表格列数据（带空值检测）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcv(IntPtr fptr, int datatype, int colnum, long firstrow, long firstelem, long nelem, IntPtr nulval, IntPtr array, ref int anynul, ref int status);
    // 批量读取多列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvn(IntPtr fptr, int ncols, int[] datatype, int[] colnum, long firstrow, long nrows, IntPtr[] nulval, IntPtr[] array, ref int anynul, ref int status);
    // 读取表格列数据（空值标记数组）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcf(IntPtr fptr, int datatype, int colnum, long firstrow, long firstelem, long nelem, IntPtr array, IntPtr nullarray, ref int anynul, ref int status);
    // 读取字符串类型列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvs(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, IntPtr nulval, IntPtr[] array, ref int anynul, ref int status);
    // 读取字符数据（无空值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcl(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, IntPtr array, ref int status);
    // 读取字符类型列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvl(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, char nulval, IntPtr array, ref int anynul, ref int status);
    // 读取无符号字节列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvb(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, byte nulval, byte[] array, ref int anynul, ref int status);
    // 读取有符号字节列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvsb(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, sbyte nulval, sbyte[] array, ref int anynul, ref int status);
    // 读取无符号短整型列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvui(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, ushort nulval, ushort[] array, ref int anynul, ref int status);
    // 读取短整型列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvi(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, short nulval, short[] array, ref int anynul, ref int status);
    // 读取无符号长整型列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvuj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, uint nulval, uint[] array, ref int anynul, ref int status);
    // 读取长整型列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int nulval, int[] array, ref int anynul, ref int status);
    // 读取无符号64位整型列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvujj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, ulong nulval, ulong[] array, ref int anynul, ref int status);
    // 读取64位整型列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvjj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, long nulval, long[] array, ref int anynul, ref int status);
    // 读取无符号整型列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvuk(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, uint nulval, uint[] array, ref int anynul, ref int status);
    // 读取整型列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvk(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int nulval, int[] array, ref int anynul, ref int status);
    // 读取浮点型列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcve(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, float nulval, float[] array, ref int anynul, ref int status);
    // 读取双精度型列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvd(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, double nulval, double[] array, ref int anynul, ref int status);
    // 读取单精度复数列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvc(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, float nulval, float[] array, ref int anynul, ref int status);
    // 读取双精度复数列数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcvm(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, double nulval, double[] array, ref int anynul, ref int status);

    // 读取位数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcx(IntPtr fptr, int colnum, long firstrow, long firstbit, long nbits, IntPtr larray, ref int status);
    // 读取位数据到无符号短整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcxui(IntPtr fptr, int colnum, long firstrow, long nrows, int firstbit, int nbits, ushort[] array, ref int status);
    // 读取位数据到无符号整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcxuk(IntPtr fptr, int colnum, long firstrow, long nrows, int firstbit, int nbits, uint[] array, ref int status);

    // 读取字符串列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfs(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, IntPtr[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取字符列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfl(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, IntPtr array, IntPtr nularray, ref int anynul, ref int status);
    // 读取无符号字节列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfb(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, byte[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取有符号字节列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfsb(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, sbyte[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取无符号短整型列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfui(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, ushort[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取短整型列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfi(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, short[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取无符号长整型列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfuj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, uint[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取长整型列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取无符号64位整型列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfujj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, ulong[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取64位整型列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfjj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, long[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取无符号整型列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfuk(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, uint[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取整型列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfk(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取浮点型列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfe(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, float[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取双精度型列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfd(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, double[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取单精度复数列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfc(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, float[] array, IntPtr nularray, ref int anynul, ref int status);
    // 读取双精度复数列（带空值标记）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgcfm(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, double[] array, IntPtr nularray, ref int anynul, ref int status);

    // 获取可变长度数据描述（32位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgdes(IntPtr fptr, int colnum, long rownum, ref int length, ref int heapaddr, ref int status);
    // 获取可变长度数据描述（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgdesll(IntPtr fptr, int colnum, long rownum, ref long length, ref long heapaddr, ref int status);
    // 批量获取可变长度数据描述（32位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgdess(IntPtr fptr, int colnum, long firstrow, long nrows, ref int length, ref int heapaddr, ref int status);
    // 批量获取可变长度数据描述（64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgdessll(IntPtr fptr, int colnum, long firstrow, long nrows, ref long length, ref long heapaddr, ref int status);
    // 设置可变长度数据描述
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpdes(IntPtr fptr, int colnum, long rownum, long length, long heapaddr, ref int status);
    // 获取堆信息
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int fftheap(IntPtr fptr, ref long heapsize, ref long unused, ref long overlap, ref int valid, ref int status);
    // 压缩堆空间
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffcmph(IntPtr fptr, ref int status);

    // 读取原始表格字节数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgtbb(IntPtr fptr, long firstrow, long firstchar, long nchars, byte[] values, ref int status);

    // 读取扩展数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffgextn(IntPtr fptr, long offset, long nelem, IntPtr array, ref int status);
    // 写入扩展数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpextn(IntPtr fptr, long offset, long nelem, IntPtr array, ref int status);

    /*------------ write primary array or image elements -------------*/
    // 写入图像像素数据（32位起始）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppx(IntPtr fptr, int datatype, int[] firstpix, long nelem, IntPtr array, ref int status);
    // 写入图像像素数据（64位起始）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppxll(IntPtr fptr, int datatype, long[] firstpix, long nelem, IntPtr array, ref int status);
    // 写入图像像素数据（带空值，32位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppxn(IntPtr fptr, int datatype, int[] firstpix, long nelem, IntPtr array, IntPtr nulval, ref int status);
    // 写入图像像素数据（带空值，64位）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppxnll(IntPtr fptr, int datatype, long[] firstpix, long nelem, IntPtr array, IntPtr nulval, ref int status);
    // 写入连续图像数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppr(IntPtr fptr, int datatype, long firstelem, long nelem, IntPtr array, ref int status);
    // 写入无符号字节图像数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpprb(IntPtr fptr, int group, long firstelem, long nelem, byte[] array, ref int status);
    // 写入有符号字节图像数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpprsb(IntPtr fptr, int group, long firstelem, long nelem, sbyte[] array, ref int status);
    // 写入无符号短整型图像数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpprui(IntPtr fptr, int group, long firstelem, long nelem, ushort[] array, ref int status);
    // 写入短整型图像数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppri(IntPtr fptr, int group, long firstelem, long nelem, short[] array, ref int status);
    // 写入无符号长整型图像数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppruj(IntPtr fptr, int group, long firstelem, long nelem, uint[] array, ref int status);
    // 写入长整型图像数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpprj(IntPtr fptr, int group, long firstelem, long nelem, int[] array, ref int status);
    // 写入无符号整型图像数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppruk(IntPtr fptr, int group, long firstelem, long nelem, uint[] array, ref int status);
    // 写入整型图像数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpprk(IntPtr fptr, int group, long firstelem, long nelem, int[] array, ref int status);
    // 写入浮点型图像数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppre(IntPtr fptr, int group, long firstelem, long nelem, float[] array, ref int status);
    // 写入双精度型图像数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpprd(IntPtr fptr, int group, long firstelem, long nelem, double[] array, ref int status);
    // 写入64位整型图像数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpprjj(IntPtr fptr, int group, long firstelem, long nelem, long[] array, ref int status);
    // 写入无符号64位整型图像数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpprujj(IntPtr fptr, int group, long firstelem, long nelem, ulong[] array, ref int status);

    // 填充图像数据为指定值
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppru(IntPtr fptr, int group, long firstelem, long nelem, ref int status);
    // 填充图像数据为空值
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpprn(IntPtr fptr, long firstelem, long nelem, ref int status);

    // 写入图像数据（带空值替换）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppn(IntPtr fptr, int datatype, long firstelem, long nelem, IntPtr array, IntPtr nulval, ref int status);
    // 写入无符号字节（带空值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppnb(IntPtr fptr, int group, long firstelem, long nelem, byte[] array, byte nulval, ref int status);
    // 写入有符号字节（带空值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppnsb(IntPtr fptr, int group, long firstelem, long nelem, sbyte[] array, sbyte nulval, ref int status);
    // 写入无符号短整型（带空值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppnui(IntPtr fptr, int group, long firstelem, long nelem, ushort[] array, ushort nulval, ref int status);
    // 写入短整型（带空值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppni(IntPtr fptr, int group, long firstelem, long nelem, short[] array, short nulval, ref int status);
    // 写入长整型（带空值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppnj(IntPtr fptr, int group, long firstelem, long nelem, int[] array, int nulval, ref int status);
    // 写入无符号长整型（带空值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppnuj(IntPtr fptr, int group, long firstelem, long nelem, uint[] array, uint nulval, ref int status);
    // 写入无符号整型（带空值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppnuk(IntPtr fptr, int group, long firstelem, long nelem, uint[] array, uint nulval, ref int status);
    // 写入整型（带空值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppnk(IntPtr fptr, int group, long firstelem, long nelem, int[] array, int nulval, ref int status);
    // 写入浮点型（带空值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppne(IntPtr fptr, int group, long firstelem, long nelem, float[] array, float nulval, ref int status);
    // 写入双精度型（带空值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppnd(IntPtr fptr, int group, long firstelem, long nelem, double[] array, double nulval, ref int status);
    // 写入64位整型（带空值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppnjj(IntPtr fptr, int group, long firstelem, long nelem, long[] array, long nulval, ref int status);
    // 写入无符号64位整型（带空值）
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffppnujj(IntPtr fptr, int group, long firstelem, long nelem, ulong[] array, ulong nulval, ref int status);

    // 写入2D无符号字节图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp2db(IntPtr fptr, int group, long ncols, long naxis1, long naxis2, byte[] array, ref int status);
    // 写入2D有符号字节图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp2dsb(IntPtr fptr, int group, long ncols, long naxis1, long naxis2, sbyte[] array, ref int status);
    // 写入2D无符号短整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp2dui(IntPtr fptr, int group, long ncols, long naxis1, long naxis2, ushort[] array, ref int status);
    // 写入2D短整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp2di(IntPtr fptr, int group, long ncols, long naxis1, long naxis2, short[] array, ref int status);
    // 写入2D无符号长整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp2duj(IntPtr fptr, int group, long ncols, long naxis1, long naxis2, uint[] array, ref int status);
    // 写入2D长整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp2dj(IntPtr fptr, int group, long ncols, long naxis1, long naxis2, int[] array, ref int status);
    // 写入2D无符号整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp2duk(IntPtr fptr, int group, long ncols, long naxis1, long naxis2, uint[] array, ref int status);
    // 写入2D整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp2dk(IntPtr fptr, int group, long ncols, long naxis1, long naxis2, int[] array, ref int status);
    // 写入2D浮点型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp2de(IntPtr fptr, int group, long ncols, long naxis1, long naxis2, float[] array, ref int status);
    // 写入2D双精度型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp2dd(IntPtr fptr, int group, long ncols, long naxis1, long naxis2, double[] array, ref int status);
    // 写入2D 64位整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp2djj(IntPtr fptr, int group, long ncols, long naxis1, long naxis2, long[] array, ref int status);
    // 写入2D无符号64位整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp2dujj(IntPtr fptr, int group, long ncols, long naxis1, long naxis2, ulong[] array, ref int status);

    // 写入3D无符号字节图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp3db(IntPtr fptr, int group, long ncols, long nrows, long naxis1, long naxis2, long naxis3, byte[] array, ref int status);
    // 写入3D有符号字节图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp3dsb(IntPtr fptr, int group, long ncols, long nrows, long naxis1, long naxis2, long naxis3, sbyte[] array, ref int status);
    // 写入3D无符号短整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp3dui(IntPtr fptr, int group, long ncols, long nrows, long naxis1, long naxis2, long naxis3, ushort[] array, ref int status);
    // 写入3D短整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp3di(IntPtr fptr, int group, long ncols, long nrows, long naxis1, long naxis2, long naxis3, short[] array, ref int status);
    // 写入3D无符号长整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp3duj(IntPtr fptr, int group, long ncols, long nrows, long naxis1, long naxis2, long naxis3, uint[] array, ref int status);
    // 写入3D长整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp3dj(IntPtr fptr, int group, long ncols, long nrows, long naxis1, long naxis2, long naxis3, int[] array, ref int status);
    // 写入3D无符号整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp3duk(IntPtr fptr, int group, long ncols, long nrows, long naxis1, long naxis2, long naxis3, uint[] array, ref int status);
    // 写入3D整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp3dk(IntPtr fptr, int group, long ncols, long nrows, long naxis1, long naxis2, long naxis3, int[] array, ref int status);
    // 写入3D浮点型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp3de(IntPtr fptr, int group, long ncols, long nrows, long naxis1, long naxis2, long naxis3, float[] array, ref int status);
    // 写入3D双精度型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp3dd(IntPtr fptr, int group, long ncols, long nrows, long naxis1, long naxis2, long naxis3, double[] array, ref int status);
    // 写入3D 64位整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp3djj(IntPtr fptr, int group, long ncols, long nrows, long naxis1, long naxis2, long naxis3, long[] array, ref int status);
    // 写入3D无符号64位整型图像
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffp3dujj(IntPtr fptr, int group, long ncols, long nrows, long naxis1, long naxis2, long naxis3, ulong[] array, ref int status);

    // 写入图像子区域数据
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpss(IntPtr fptr, int datatype, int[] fpixel, int[] lpixel, IntPtr array, ref int status);
    // 写入子区域无符号字节
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpssb(IntPtr fptr, int group, int naxis, int[] naxes, int[] fpixel, int[] lpixel, byte[] array, ref int status);
    // 写入子区域有符号字节
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpsssb(IntPtr fptr, int group, int naxis, int[] naxes, int[] fpixel, int[] lpixel, sbyte[] array, ref int status);
    // 写入子区域无符号短整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpssui(IntPtr fptr, int group, int naxis, int[] naxes, int[] fpixel, int[] lpixel, ushort[] array, ref int status);
    // 写入子区域短整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpssi(IntPtr fptr, int group, int naxis, int[] naxes, int[] fpixel, int[] lpixel, short[] array, ref int status);
    // 写入子区域无符号长整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpssuj(IntPtr fptr, int group, int naxis, int[] naxes, int[] fpixel, int[] lpixel, uint[] array, ref int status);
    // 写入子区域长整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpssj(IntPtr fptr, int group, int naxis, int[] naxes, int[] fpixel, int[] lpixel, int[] array, ref int status);
    // 写入子区域无符号整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpssuk(IntPtr fptr, int group, int naxis, int[] naxes, int[] fpixel, int[] lpixel, uint[] array, ref int status);
    // 写入子区域整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpssk(IntPtr fptr, int group, int naxis, int[] naxes, int[] fpixel, int[] lpixel, int[] array, ref int status);
    // 写入子区域浮点型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpsse(IntPtr fptr, int group, int naxis, int[] naxes, int[] fpixel, int[] lpixel, float[] array, ref int status);
    // 写入子区域双精度型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpssd(IntPtr fptr, int group, int naxis, int[] naxes, int[] fpixel, int[] lpixel, double[] array, ref int status);
    // 写入子区域64位整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpssjj(IntPtr fptr, int group, int naxis, int[] naxes, int[] fpixel, int[] lpixel, long[] array, ref int status);
    // 写入子区域无符号64位整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpssujj(IntPtr fptr, int group, int naxis, int[] naxes, int[] fpixel, int[] lpixel, ulong[] array, ref int status);

    // 写入图像组无符号字节
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpgpb(IntPtr fptr, int group, int firstelem, int nelem, byte[] array, ref int status);
    // 写入图像组有符号字节
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpgpsb(IntPtr fptr, int group, int firstelem, int nelem, sbyte[] array, ref int status);
    // 写入图像组无符号短整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpgpui(IntPtr fptr, int group, int firstelem, int nelem, ushort[] array, ref int status);
    // 写入图像组短整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpgpi(IntPtr fptr, int group, int firstelem, int nelem, short[] array, ref int status);
    // 写入图像组无符号长整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpgpuj(IntPtr fptr, int group, int firstelem, int nelem, uint[] array, ref int status);
    // 写入图像组长整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpgpj(IntPtr fptr, int group, int firstelem, int nelem, int[] array, ref int status);
    // 写入图像组无符号整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpgpuk(IntPtr fptr, int group, int firstelem, int nelem, uint[] array, ref int status);
    // 写入图像组整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpgpk(IntPtr fptr, int group, int firstelem, int nelem, int[] array, ref int status);
    // 写入图像组浮点型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpgpe(IntPtr fptr, int group, int firstelem, int nelem, float[] array, ref int status);
    // 写入图像组双精度型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpgpd(IntPtr fptr, int group, int firstelem, int nelem, double[] array, ref int status);
    // 写入图像组64位整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpgpjj(IntPtr fptr, int group, int firstelem, int nelem, long[] array, ref int status);
    // 写入图像组无符号64位整型
    [DllImport(dll, CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffpgpujj(IntPtr fptr, int group, int firstelem, int nelem, ulong[] array, ref int status);

    /*--------------------- iterator functions -------------*/
    // 按列名设置迭代器
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_iter_set_by_name(ref iteratorCol col, IntPtr fptr, string colname, int datatype, int iotype);
    // 按列号设置迭代器
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_iter_set_by_num(ref iteratorCol col, IntPtr fptr, int colnum, int datatype, int iotype);
    // 设置迭代器文件句柄
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_iter_set_file(ref iteratorCol col, IntPtr fptr);
    // 设置迭代器列名
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_iter_set_colname(ref iteratorCol col, string colname);
    // 设置迭代器列号
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_iter_set_colnum(ref iteratorCol col, int colnum);
    // 设置迭代器数据类型
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_iter_set_datatype(ref iteratorCol col, int datatype);
    // 设置迭代器IO类型
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_iter_set_iotype(ref iteratorCol col, int iotype);
    // 获取迭代器文件句柄
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr fits_iter_get_file(ref iteratorCol col);
    // 获取迭代器列名
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr fits_iter_get_colname(ref iteratorCol col);
    // 获取迭代器列号
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_iter_get_colnum(ref iteratorCol col);
    // 获取迭代器数据类型
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_iter_get_datatype(ref iteratorCol col);
    // 获取迭代器IO类型
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_iter_get_iotype(ref iteratorCol col);
    // 获取迭代器数据数组指针
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr fits_iter_get_array(ref iteratorCol col);
    // 获取列TLMIN
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern long fits_iter_get_tlmin(ref iteratorCol col);
    // 获取列TLMAX
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern long fits_iter_get_tlmax(ref iteratorCol col);
    // 获取列重复数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern long fits_iter_get_repeat(ref iteratorCol col);
    // 获取列单位
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr fits_iter_get_tunit(ref iteratorCol col);
    // 获取列显示格式
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr fits_iter_get_tdisp(ref iteratorCol col);
    // 迭代器回调委托
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int IterWorkFn(long totaln, long offset, long firstn, long nvalues, int narrays, ref iteratorCol data, IntPtr userPointer);
    // 启动迭代器
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffiter(int ncols, ref iteratorCol data, int offset, int nPerLoop, IterWorkFn workFn, IntPtr userPointer, ref int status);

    /*--------------------- write column elements -------------*/
    // 写入列数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcl(IntPtr fptr, int datatype, int colnum, long firstrow, long firstelem, long nelem, IntPtr array, ref int status);
    // 批量写入多列数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcln(IntPtr fptr, int ncols, int[] datatype, int[] colnum, long firstrow, long nrows, IntPtr[] array, IntPtr[] nulval, ref int status);
    // 写入字符串列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcls(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, string[] array, ref int status);
    // 写入字符列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcll(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, string array, ref int status);
    // 写入无符号字节列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpclb(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, byte[] array, ref int status);
    // 写入有符号字节列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpclsb(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, sbyte[] array, ref int status);
    // 写入无符号短整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpclui(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, ushort[] array, ref int status);
    // 写入短整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcli(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, short[] array, ref int status);
    // 写入无符号长整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcluj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, uint[] array, ref int status);
    // 写入长整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpclj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int[] array, ref int status);
    // 写入无符号整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcluk(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, uint[] array, ref int status);
    // 写入整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpclk(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int[] array, ref int status);
    // 写入浮点型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcle(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, float[] array, ref int status);
    // 写入双精度型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcld(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, double[] array, ref int status);
    // 写入单精度复数列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpclc(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, float[] array, ref int status);
    // 写入双精度复数列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpclm(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, double[] array, ref int status);
    // 写入空值到列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpclu(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, ref int status);
    // 写入空值到行
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffprwu(IntPtr fptr, long firstrow, long nrows, ref int status);
    // 写入64位整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcljj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, long[] array, ref int status);
    // 写入无符号64位整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpclujj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, ulong[] array, ref int status);
    // 写入位数据列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpclx(IntPtr fptr, int colnum, long frow, int fbit, int nbit, byte[] larray, ref int status);
    // 写入列数据（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcn(IntPtr fptr, int datatype, int colnum, long firstrow, long firstelem, long nelem, IntPtr array, IntPtr nulval, ref int status);
    // 写入字符串列（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcns(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, string[] array, string nulvalue, ref int status);
    // 写入字符列（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcnl(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, string array, char nulvalue, ref int status);
    // 写入无符号字节（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcnb(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, byte[] array, byte nulvalue, ref int status);
    // 写入有符号字节（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcnsb(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, sbyte[] array, sbyte nulvalue, ref int status);
    // 写入无符号短整型（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcnui(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, ushort[] array, ushort nulvalue, ref int status);
    // 写入短整型（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcni(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, short[] array, short nulvalue, ref int status);
    // 写入无符号长整型（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcnuj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, uint[] array, uint nulvalue, ref int status);
    // 写入长整型（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcnj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int[] array, int nulvalue, ref int status);
    // 写入无符号整型（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcnuk(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, uint[] array, uint nulvalue, ref int status);
    // 写入整型（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcnk(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int[] array, int nulvalue, ref int status);
    // 写入浮点型（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcne(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, float[] array, float nulvalue, ref int status);
    // 写入双精度型（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcnd(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, double[] array, double nulvalue, ref int status);
    // 写入64位整型（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcnjj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, long[] array, long nulvalue, ref int status);
    // 写入无符号64位整型（带空值）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcnujj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, ulong[] array, ulong nulvalue, ref int status);
    // 写入原始表格字节
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffptbb(IntPtr fptr, long firstrow, long firstchar, long nchars, byte[] values, ref int status);
    // 插入行
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffirow(IntPtr fptr, long firstrow, long nrows, ref int status);
    // 删除行
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffdrow(IntPtr fptr, long firstrow, long nrows, ref int status);
    // 按范围删除行
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffdrrg(IntPtr fptr, string ranges, ref int status);
    // 删除指定行（32位）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffdrws(IntPtr fptr, int[] rownum, int nrows, ref int status);
    // 删除指定行（64位）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffdrwsll(IntPtr fptr, long[] rownum, long nrows, ref int status);
    // 插入单列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fficol(IntPtr fptr, int numcol, string ttype, string tform, ref int status);
    // 插入多列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fficls(IntPtr fptr, int firstcol, int ncols, string[] ttype, string[] tform, ref int status);
    // 修改列向量长度
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffmvec(IntPtr fptr, int colnum, long newveclen, ref int status);
    // 删除列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffdcol(IntPtr fptr, int numcol, ref int status);
    // 拷贝单列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffcpcl(IntPtr infptr, IntPtr outfptr, int incol, int outcol, int create_col, ref int status);
    // 拷贝多列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffccls(IntPtr infptr, IntPtr outfptr, int incol, int outcol, int ncols, int create_col, ref int status);
    // 拷贝行
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffcprw(IntPtr infptr, IntPtr outfptr, long firstrow, long nrows, ref int status);
    // 拷贝行（带状态）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffcpsr(IntPtr infptr, IntPtr outfptr, long firstrow, long nrows, byte[] row_status, ref int status);
    // 拷贝表格标题
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffcpht(IntPtr infptr, IntPtr outfptr, long firstrow, long nrows, ref int status);

    /*--------------------- WCS Utilities ------------------*/
    // 获取图像WCS
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgics(IntPtr fptr, ref double xrval, ref double yrval, ref double xrpix, ref double yrpix, ref double xinc, ref double yinc, ref double rot, StringBuilder type, ref int status);
    // 获取指定版本WCS
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgicsa(IntPtr fptr, char version, ref double xrval, ref double yrval, ref double xrpix, ref double yrpix, ref double xinc, ref double yinc, ref double rot, StringBuilder type, ref int status);
    // 获取表格WCS
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtcs(IntPtr fptr, int xcol, int ycol, ref double xrval, ref double yrval, ref double xrpix, ref double yrpix, ref double xinc, ref double yinc, ref double rot, StringBuilder type, ref int status);
    // 像素转天球坐标
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffwldp(double xpix, double ypix, double xref, double yref, double xrefpix, double yrefpix, double xinc, double yinc, double rot, string type, ref double xpos, ref double ypos, ref int status);
    // 天球坐标转像素
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffxypx(double xpos, double ypos, double xref, double yref, double xrefpix, double yrefpix, double xinc, double yinc, double rot, string type, ref double xpix, ref double ypix, ref int status);
    // 获取图像WCS头
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgiwcs(IntPtr fptr, ref IntPtr header, ref int status);
    // 获取表格WCS头
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtwcs(IntPtr fptr, int xcol, int ycol, ref IntPtr header, ref int status);

    /*--------------------- lexical parsing routines ------------------*/
    // 表达式解析
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fftexp(IntPtr fptr, string expr, int maxdim, ref int datatype, ref int nelem, ref int naxis, int[] naxes, ref int status);
    // 表达式筛选行
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffrow(IntPtr infptr, string expr, int firstrow, int nrows, ref int n_good_rows, byte[] row_status, ref int status);
    // 查找匹配行
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffffrw(IntPtr fptr, string expr, ref int rownum, ref int status);
    // 时间列匹配
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffrwc(IntPtr fptr, string expr, string timeCol, string parCol, string valCol, int ntimes, double[] times, byte[] time_status, ref int status);
    // 筛选行到输出
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffsrow(IntPtr infptr, IntPtr outfptr, string expr, ref int status);
    // 计算列值
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffcrow(IntPtr fptr, int datatype, string expr, int firstrow, int nelements, IntPtr nulval, IntPtr array, ref int anynul, ref int status);
    // 范围计算
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffcalc_rng(IntPtr infptr, string expr, IntPtr outfptr, string parName, string parInfo, int nRngs, int[] start, int[] end, ref int status);
    // 表达式计算
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffcalc(IntPtr infptr, string expr, IntPtr outfptr, string parName, string parInfo, ref int status);
    // 直方图生成
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffhist2(ref IntPtr fptr, string outfile, int imagetype, int naxis, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] colname, double[] minin, double[] maxin, double[] binsizein, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] minname, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] maxname, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] binname, double weightin, string wtcol, int recip, string rowselect, ref int status);
    // 高级直方图
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr ffhist3(IntPtr fptr, string outfile, int imagetype, int naxis, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] colname, double[] minin, double[] maxin, double[] binsizein, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] minname, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] maxname, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] binname, double weightin, string wtcol, int recip, string selectrow, ref int status);
    // 选择图像区域
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_select_image_section(ref IntPtr fptr, string outfile, string imagesection, ref int status);
    // 拷贝图像区域
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_copy_image_section(IntPtr infptr, IntPtr outfile, string imagesection, ref int status);
    // 计算直方图分箱（浮点）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_calc_binning(IntPtr fptr, int naxis, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] colname, double[] minin, double[] maxin, double[] binsizein, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] minname, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] maxname, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] binname, int[] colnum, int[] haxes, float[] amin, float[] amax, float[] binsize, ref int status);
    // 计算直方图分箱（双精度）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_calc_binningd(IntPtr fptr, int naxis, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] colname, double[] minin, double[] maxin, double[] binsizein, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] minname, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] maxname, [MarshalAs(UnmanagedType.LPArray, SizeConst = 4)] string[] binname, int[] colnum, int[] haxes, double[] amin, double[] amax, double[] binsize, ref int status);
    // 写入直方图关键字
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_write_keys_histo(IntPtr fptr, IntPtr histptr, int naxis, int[] colnum, ref int status);
    // 重采样WCS（浮点）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_rebin_wcs(IntPtr fptr, int naxis, float[] amin, float[] binsize, ref int status);
    // 重采样WCS（双精度）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_rebin_wcsd(IntPtr fptr, int naxis, double[] amin, double[] binsize, ref int status);
    // 生成直方图（浮点）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_make_hist(IntPtr fptr, IntPtr histptr, int bitpix, int naxis, int[] naxes, int[] colnum, float[] amin, float[] amax, float[] binsize, float weight, int wtcolnum, int recip, string selectrow, ref int status);
    // 生成直方图（双精度）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_make_histd(IntPtr fptr, IntPtr histptr, int bitpix, int naxis, int[] naxes, int[] colnum, double[] amin, double[] amax, double[] binsize, double weight, int wtcolnum, int recip, string selectrow, ref int status);

    // 像素过滤器结构体：用于FITS图像像素过滤的参数结构体
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct PixelFilter
    {
        public int count;                // 过滤项数量
        public IntPtr path;              // 文件路径指针数组
        public IntPtr tag;               // 标签指针数组
        public IntPtr ifptr;             // 输入FITS文件指针数组
        public IntPtr expression;        // 过滤表达式字符串指针
        public int bitpix;               // 输出图像位深度
        public long blank;               // 空像素填充值
        public IntPtr ofptr;             // 输出FITS文件指针
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 70)]
        public string keyword;           // 输出关键字
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string comment;           // 关键字注释
    }

    // 执行像素过滤操作
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_pixel_filter(ref PixelFilter filter, ref int status);

    /*--------------------- grouping routines 分组操作函数 ------------------*/
    // 创建分组
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtcr(IntPtr fptr, string grpname, int grouptype, ref int status);
    // 检查分组是否存在
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtis(IntPtr fptr, string grpname, int grouptype, ref int status);
    // 检查分组句柄
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtch(IntPtr gfptr, int grouptype, ref int status);
    // 删除分组成员
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtrm(IntPtr gfptr, int rmopt, ref int status);
    // 复制分组
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtcp(IntPtr infptr, IntPtr outfptr, int cpopt, ref int status);
    // 合并分组
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtmg(IntPtr infptr, IntPtr outfptr, int mgopt, ref int status);
    // 提交分组修改
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtcm(IntPtr gfptr, int cmopt, ref int status);
    // 验证分组失败项
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtvf(IntPtr gfptr, ref int firstfailed, ref int status);
    // 打开主分组
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtop(IntPtr mfptr, int group, ref IntPtr gfptr, ref int status);
    // 添加分组成员
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtam(IntPtr gfptr, IntPtr mfptr, int hdupos, ref int status);
    // 获取分组成员数量
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtnm(IntPtr gfptr, ref int nmembers, ref int status);
    // 获取主分组成员总数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgmng(IntPtr mfptr, ref int nmembers, ref int status);
    // 打开分组中的成员
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgmop(IntPtr gfptr, int member, ref IntPtr mfptr, ref int status);
    // 复制分组成员
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgmcp(IntPtr gfptr, IntPtr mfptr, int member, int cpopt, ref int status);
    // 传输分组成员文件
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgmtf(IntPtr infptr, IntPtr outfptr, int member, int tfopt, ref int status);
    // 删除分组成员
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgmrm(IntPtr fptr, int member, int rmopt, ref int status);

    // 执行分组模板
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_execute_template(IntPtr ff, string ngp_template, ref int status);

    // 图像统计：short类型图像统计计算
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_img_stats_short(short[] array, int nx, int ny, int nullcheck, short nullvalue, ref int ngoodpix, ref short minvalue, ref short maxvalue, ref double mean, ref double sigma, ref double noise1, ref double noise2, ref double noise3, ref double noise5, ref int status);
    // 图像统计：int类型图像统计计算
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_img_stats_int(int[] array, int nx, int ny, int nullcheck, int nullvalue, ref int ngoodpix, ref int minvalue, ref int maxvalue, ref double mean, ref double sigma, ref double noise1, ref double noise2, ref double noise3, ref double noise5, ref int status);
    // 图像统计：float类型图像统计计算
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_img_stats_float(float[] array, int nx, int ny, int nullcheck, float nullvalue, ref int ngoodpix, ref float minvalue, ref float maxvalue, ref double mean, ref double sigma, ref double noise1, ref double noise2, ref double noise3, ref double noise5, ref int status);

    /*--------------------- image compression routines 图像压缩函数 ------------------*/
    // 设置图像压缩类型
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_set_compression_type(IntPtr fptr, int ctype, ref int status);
    // 设置压缩瓦片维度
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_set_tile_dim(IntPtr fptr, int ndim, int[] dims, ref int status);
    // 设置压缩噪声位数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_set_noise_bits(IntPtr fptr, int noisebits, ref int status);
    // 设置量化级别
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_set_quantize_level(IntPtr fptr, float qlevel, ref int status);
    // 设置H压缩缩放比例
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_set_hcomp_scale(IntPtr fptr, float scale, ref int status);
    // 设置H压缩平滑参数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_set_hcomp_smooth(IntPtr fptr, int smooth, ref int status);
    // 设置量化方法
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_set_quantize_method(IntPtr fptr, int method, ref int status);
    // 设置量化抖动参数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_set_quantize_dither(IntPtr fptr, int dither, ref int status);
    // 设置抖动随机种子
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_set_dither_seed(IntPtr fptr, int seed, ref int status);
    // 设置抖动偏移量
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_set_dither_offset(IntPtr fptr, int offset, ref int status);
    // 设置整数有损压缩标志
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_set_lossy_int(IntPtr fptr, int lossy_int, ref int status);
    // 设置超大HDU模式
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_set_huge_hdu(IntPtr fptr, int huge, ref int status);
    // 设置压缩首选项
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_set_compression_pref(IntPtr infptr, IntPtr outfptr, ref int status);

    // 获取图像压缩类型
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_get_compression_type(IntPtr fptr, ref int ctype, ref int status);
    // 获取压缩瓦片维度
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_get_tile_dim(IntPtr fptr, int ndim, int[] dims, ref int status);
    // 获取量化级别
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_get_quantize_level(IntPtr fptr, ref float qlevel, ref int status);
    // 获取压缩噪声位数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_get_noise_bits(IntPtr fptr, ref int noisebits, ref int status);
    // 获取H压缩缩放比例
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_get_hcomp_scale(IntPtr fptr, ref float scale, ref int status);
    // 获取H压缩平滑参数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_get_hcomp_smooth(IntPtr fptr, ref int smooth, ref int status);
    // 获取抖动随机种子
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_get_dither_seed(IntPtr fptr, ref int seed, ref int status);

    // 压缩FITS图像
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_img_compress(IntPtr infptr, IntPtr outfptr, ref int status);
    // 判断是否为压缩图像
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_is_compressed_image(IntPtr fptr, ref int status);
    // 判断库是否可重入
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_is_reentrant();
    // 解压FITS图像
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_decompress_img(IntPtr infptr, IntPtr outfptr, ref int status);
    // 仅解压图像头文件
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_img_decompress_header(IntPtr infptr, IntPtr outfptr, ref int status);
    // 解压FITS图像（通用接口）
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_img_decompress(IntPtr infptr, IntPtr outfptr, ref int status);

    // H压缩算法：压缩int型图像
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_hcompress(int[] a, int nx, int ny, int scale, byte[] output, ref int nbytes, ref int status);
    // H压缩算法：压缩64位整型图像
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_hcompress64(long[] a, int nx, int ny, int scale, byte[] output, ref int nbytes, ref int status);
    // H压缩算法：解压到int型图像
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_hdecompress(byte[] input, int smooth, int[] a, ref int nx, ref int ny, ref int scale, ref int status);
    // H压缩算法：解压到64位整型图像
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_hdecompress64(byte[] input, int smooth, long[] a, ref int nx, ref int ny, ref int scale, ref int status);

    // 压缩FITS表格数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_compress_table(IntPtr infptr, IntPtr outfptr, ref int status);
    // 解压FITS表格数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_uncompress_table(IntPtr infptr, IntPtr outfptr, ref int status);

    /*--------------------- curl https HTTPS网络功能 ------------------*/
    // 初始化HTTPS支持
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_init_https();
    // 清理HTTPS资源
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_cleanup_https();
    // 设置HTTPS日志输出级别
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern void fits_verbose_https(int flag);

    /*--------------------- 全局工具函数 ------------------*/
    // 关闭FITS库
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern void ffshdwn(int flag);
    // 获取当前超时时间
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtmo();
    // 设置IO操作超时时间
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffstmo(int sec, ref int status);

    /*--------------------- 线程同步（POSIX pthread 模拟）------------------*/
    // 仅 _REENTRANT 编译时启用锁，C# 中用 lock/Monitor 替代
    public const bool REENTRANT = false; // 根据编译条件开启

    /*--------------------- 软链接替换（HERA 环境）------------------------*/
    public const int REPLACE_LINKS = 0;
    public const int BUILD_HERA = 0;

    /*--------------------- 图像写入标记 --------------------------------*/
    public const int USE_LARGE_VALUE = -99;

    /*--------------------- 数据缓冲区大小（字节）------------------------*/
    public const int DBUFFSIZE = 28800;

    /*--------------------- 最大打开文件数 ------------------------------*/
    public const int NMAXFILES = 10000;

    /*--------------------- 直接IO最小大小 ------------------------------*/
    public const int MINDIRECT = 8640;

    /*--------------------- 平台机器类型枚举 ----------------------------*/
    public const int NATIVE = 0;
    public const int OTHERTYPE = 1;
    public const int VAXVMS = 3;
    public const int ALPHAVMS = 4;
    public const int IBMPC = 5;
    public const int CRAY = 6;

    public const int GFLOAT = 1;
    public const int IEEEFLOAT = 2;

    /*--------------------- 字节序 / long 位宽（自动适配C#运行环境）-----*/
    public static bool BYTESWAPPED => BitConverter.IsLittleEndian;
    public static int LONGSIZE => IntPtr.Size == 8 ? 64 : 32;
    public static int CFITSIO_MACHINE
    {
        get
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return IBMPC;
            return NATIVE;
        }
    }

    /*--------------------- EOF 处理模式 -------------------------------*/
    public const int IGNORE_EOF = 1;
    public const int REPORT_EOF = 0;
    public const int DATA_UNDEFINED = -1;
    public const int NULL_UNDEFINED = 1234554321;
    public const int ASCII_NULL_UNDEFINED = 1;

    /*--------------------- 最大/最小值快速宏 --------------------------*/
    public static T maxvalue<T>(T a, T b) where T : IComparable => a.CompareTo(b) > 0 ? a : b;
    public static T minvalue<T>(T a, T b) where T : IComparable => a.CompareTo(b) < 0 ? a : b;

    /*--------------------- NaN 掩码（IEEE 浮点）-----------------------*/
    public const ushort FNANMASK = 0x7F80;
    public const ushort DNANMASK = 0x7FF0;

    // C# 等效 fnan / dnan 宏：1=NaN/Inf, 2=非规格化, 0=正常
    public static int fnan(int l) => (l & FNANMASK) == FNANMASK ? 1 : (l & FNANMASK) == 0 ? 2 : 0;
    public static int dnan(long l) => (l & DNANMASK) == DNANMASK ? 1 : (l & DNANMASK) == 0 ? 2 : 0;

    /*--------------------- 数值边界（double → 整型安全范围）------------*/
    public const double DSCHAR_MAX = 127.49;
    public const double DSCHAR_MIN = -128.49;
    public const double DUCHAR_MAX = 255.49;
    public const double DUCHAR_MIN = -0.49;

    public const double DUSHRT_MAX = 65535.49;
    public const double DUSHRT_MIN = -0.49;
    public const double DSHRT_MAX = 32767.49;
    public const double DSHRT_MIN = -32768.49;

    // 32/64位 long 自适应
    public static double DLONG_MAX => LONGSIZE == 32 ? 2147483647.49 : 9.2233720368547752E18;
    public static double DLONG_MIN => LONGSIZE == 32 ? -2147483648.49 : -9.2233720368547752E18;
    public static double DULONG_MAX => LONGSIZE == 32 ? 4294967295.49 : 1.84467440737095504E19;
    public const double DULONG_MIN = -0.49;

    public const double DULONGLONG_MAX = 18446744073709551615.0;
    public const double DULONGLONG_MIN = -0.49;
    public const double DLONGLONG_MAX = 9.2233720368547755807E18;
    public const double DLONGLONG_MIN = -9.2233720368547755808E18;

    public const double DUINT_MAX = 4294967295.49;
    public const double DUINT_MIN = -0.49;
    public const double DINT_MAX = 2147483647.49;
    public const double DINT_MIN = -2147483648.49;

    /*--------------------- 标准32/64位整数边界 ------------------------*/
    public const ulong UINT64_MAX = 18446744073709551615u;
    public const uint UINT32_MAX = 4294967295u;
    public const int INT32_MAX = 2147483647;
    public const int INT32_MIN = -2147483648;

    /*--------------------- 压缩空值 ----------------------------------*/
    public const int COMPRESS_NULL_VALUE = -2147483647;

    /*--------------------- 量化随机数池大小（不可修改）----------------*/
    public const int N_RANDOM = 10000;

    // 读取FITS关键字卡片
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgnky(IntPtr fptr, string card, ref int status);
    // 转换TFORM格式字符串
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern void ffcfmt(string tform, string cform);
    // 转换显示格式字符串
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern void ffcdsp(string tform, string cform);
    // 交换2字节数据字节序
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern void ffswap2(short[] values, int nvalues);
    // 交换4字节数据字节序
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern void ffswap4(int[] values, int nvalues);
    // 交换8字节数据字节序
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern void ffswap8(double[] values, int nvalues);
    // 64位整数转字符串
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi2c(long ival, string cval, ref int status);
    // 无符号64位整数转字符串
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu2c(ulong ival, string cval, ref int status);
    // 整数转字符串
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffl2c(int lval, string cval, ref int status);
    // 字符串转换为C格式字符串
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffs2c(string instr, string outstr, ref int status);
    // 字符串转换为C格式(无填充)
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffs2c_nopad(string instr, string outstr, ref int status);
    // 浮点数转固定格式字符串
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr2f(float fval, int decim, string cval, ref int status);
    // 浮点数转指数格式字符串
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr2e(float fval, int decim, string cval, ref int status);
    // 双精度浮点数转固定格式字符串
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffd2f(double dval, int decim, string cval, ref int status);
    // 双精度浮点数转指数格式字符串
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffd2e(double dval, int decim, string cval, ref int status);
    // 字符串转长整型
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2ii(string cval, ref int ival, ref int status);
    // 字符串转64位整型
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2jj(string cval, ref long ival, ref int status);
    // 字符串转无符号64位整型
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2ujj(string cval, ref ulong ival, ref int status);
    // 字符串转整型
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2ll(string cval, ref int lval, ref int status);
    // 字符串转单精度浮点数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2rr(string cval, ref float fval, ref int status);
    // 字符串转双精度浮点数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2dd(string cval, ref double dval, ref int status);
    // 字符串转通用数据类型
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2x(string cval, string dtype, ref int ival, ref int lval, string sval, ref double dval, ref int status);
    // 字符串转64位通用数据类型
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2xx(string cval, string dtype, ref long ival, ref int lval, string sval, ref double dval, ref int status);
    // 字符串转无符号64位通用数据类型
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2uxx(string cval, string dtype, ref ulong ival, ref int lval, string sval, ref double dval, ref int status);
    // 字符串拷贝转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2s(string instr, string outstr, ref int status);
    // 字符串转长整数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2i(string cval, ref int ival, ref int status);
    // 字符串转64位整数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2j(string cval, ref long ival, ref int status);
    // 字符串转无符号64位整数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2uj(string cval, ref ulong ival, ref int status);
    // 字符串转浮点数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2r(string cval, ref float fval, ref int status);
    // 字符串转双精度数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2d(string cval, ref double dval, ref int status);
    // 字符串转逻辑值
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffc2l(string cval, ref int lval, ref int status);
    // 输出错误消息
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern void ffxmsg(int action, string err_message);
    // 获取关键字计数
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcnt(IntPtr fptr, string value, string comm, ref int status);
    // 获取数值型关键字
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtkn(IntPtr fptr, int numkey, string keyname, ref int value, ref int status);
    // 获取64位数值型关键字
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtknjj(IntPtr fptr, int numkey, string keyname, ref long value, ref int status);
    // 获取字符串型关键字
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fftkyn(IntPtr fptr, int numkey, string keyname, string value, ref int status);
    // 获取图像头数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgphd(IntPtr fptr, int maxdim, ref int simple, ref int bitpix, ref int naxis, long[] naxes, ref int pcount, ref int gcount, ref int extend, ref double bscale, ref double bzero, ref long blank, ref int nspace, ref int status);
    // 获取表格基本信息
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgttb(IntPtr fptr, ref long rowlen, ref long nrows, ref long pcount, ref int tfield, ref int status);
    // 读取关键字值和注释
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffglkut(IntPtr fptr, string keyname, int firstchar, int maxchar, int maxcomchar, string value, ref int valuelen, string comm, ref int comlen, ref int status);
    // 写入关键字卡片
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffmkey(IntPtr fptr, string card, ref int status);
    // 创建长字符串关键字
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_make_longstr_key_util(IntPtr fptr, string keyname, string value, string comm, int position, ref int status);
    // 读取字节数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgbyt(IntPtr fptr, long nbytes, IntPtr buffer, ref int status);
    // 写入字节数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpbyt(IntPtr fptr, long nbytes, IntPtr buffer, ref int status);
    // 偏移读取字节数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgbytoff(IntPtr fptr, int gsize, int ngroups, int offset, IntPtr buffer, ref int status);
    // 偏移写入字节数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpbytoff(IntPtr fptr, int gsize, int ngroups, int offset, IntPtr buffer, ref int status);
    // 加载数据记录
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffldrc(IntPtr fptr, int record, int err_mode, ref int status);
    // 刷新缓冲区
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffwhbf(IntPtr fptr, ref int nbuff);
    // 检查文件结束
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffbfeof(IntPtr fptr, ref int status);
    // 写入缓冲区数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffbfwt(IntPtr Fptr, int nbuff, ref int status);
    // 获取数据类型字节大小
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpxsz(int datatype);
    // 解析URL文件路径
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffourl(string url, string urltype, string outfile, string tmplfile, string compspec, ref int status);
    // 解析压缩规格
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffparsecompspec(IntPtr fptr, string compspec, ref int status);
    // 优化模板
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffoptplt(IntPtr fptr, string tempname, ref int status);
    // 判断是否为副本文件
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_is_this_a_copy(string urltype);
    // 查找匹配分隔符
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern IntPtr fits_find_match_delim(IntPtr str, char delim);
    // 存储文件句柄
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_store_Fptr(IntPtr Fptr, ref int status);
    // 清除文件句柄
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_clear_Fptr(IntPtr Fptr, ref int status);
    // 检查文件是否已打开
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_already_open(ref IntPtr fptr, string url, string urltype, string infile, string extspec, string rowfilter, string binspec, string colspec, int mode, int noextsyn, ref int isopen, ref int status);
    // 编辑表格列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffedit_columns(ref IntPtr fptr, string outfile, string expr, ref int status);
    // 获取列数据最大最小值
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_get_col_minmax(IntPtr fptr, int colnum, ref double datamin, ref double datamax, ref int status);
    // 获取表达式最大最小值
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_get_expr_minmax(IntPtr fptr, string expr, ref double datamin, ref double datamax, ref int datatype, ref int status);
    // 解析分箱规格
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffbinse(string binspec, ref int imagetype, ref int haxis, string[] colname, double[] minin, double[] maxin, double[] binsizein, string[] minname, string[] maxname, string[] binname, ref double weight, string wtname, ref int recip, ref IntPtr exprs, ref int status);
    // 解析分箱范围
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffbinre(ref string binspec, string colname, ref string exprbeg, ref string exprend, ref double minin, ref double maxin, ref double binsizein, string minname, string maxname, string binname, ref int status);
    // 扩展直方图创建
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffhist2e(ref IntPtr fptr, string outfile, int imagetype, int naxis, string[] colname, string[] colexpr, double[] minin, double[] maxin, double[] binsizein, string[] minname, string[] maxname, string[] binname, double weightin, string wtcol, string wtexpr, int recip, string selectrow, ref int status);
    // 计算分箱参数(双精度扩展)
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_calc_binningde(IntPtr fptr, int naxis, string[] colname, string[] colexpr, double[] minin, double[] maxin, double[] binsizein, string[] minname, string[] maxname, string[] binname, ref int colnum, ref int haxis, int[] naxes, ref double amin, ref double amax, ref double binsize, ref int naxesTotal, ref int status);
    // 写入直方图关键字
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_write_keys_histoe(IntPtr fptr, IntPtr histptr, int naxis, int[] colnum, string[] colname, string[] colexpr, ref int status);
    // 创建双精度扩展直方图
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fits_make_histde(IntPtr fptr, IntPtr histptr, int[] datatypes, int bitpix, int naxis, int[] naxes, int[] colnum, string[] colexpr, double[] amin, double[] amax, double[] binsize, double weight, int wtcolnum, string wtexpr, int recip, string selectrow, ref int status);
    // 写入直方图数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffwritehisto(int totaln, int offset, int firstn, int nvalues, int narrays, IntPtr imagepars, IntPtr userPointer);
    // 计算直方图数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffcalchist(int totalrows, int offset, int firstrow, int nrows, int ncols, IntPtr colpars, IntPtr userPointer);
    // 初始化像素数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpinit(IntPtr fptr, ref int status);
    // 初始化数组数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffainit(IntPtr fptr, ref int status);
    // 初始化二进制数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffbinit(IntPtr fptr, ref int status);
    // 切换HDU
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffchdu(IntPtr fptr, ref int status);
    // 写入文件结束
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffwend(IntPtr fptr, ref int status);
    // 刷新PDF数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpdfl(IntPtr fptr, ref int status);
    // 更新文件信息
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffuptf(IntPtr fptr, ref int status);
    // 删除数据块
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffdblk(IntPtr fptr, int nblocks, ref int status);
    // 移动到指定扩展
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgext(IntPtr fptr, int moveto, ref int exttype, ref int status);
    // 获取总字节宽度
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtbc(IntPtr fptr, ref long totalwidth, ref int status);
    // 获取关键字名称值
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgtbp(IntPtr fptr, string name, string value, ref int status);
    // 插入数据块
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffiblk(IntPtr fptr, int nblock, int headdata, ref int status);
    // 数据字节移位
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffshft(IntPtr fptr, long firstbyte, long nbytes, long nshift, ref int status);
    // 获取列参数信息
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcprll(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int writemode, ref double scale, ref double zero, string tform, ref int twidth, ref int tcode, ref int maxelem, ref long startpos, ref long elemnum, ref int incre, ref long repeat, ref long rowlen, ref int hdutype, ref long tnull, string snull, ref int status);
    // 刷新扩展数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffflushx(IntPtr fptr);
    // 文件指针定位
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffseek(IntPtr fptr, long position);
    // 读取文件数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffread(IntPtr fptr, int nbytes, IntPtr buffer, ref int status);
    // 写入文件数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffwrite(IntPtr fptr, int nbytes, IntPtr buffer, ref int status);
    // 截断文件大小
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fftrun(IntPtr fptr, long filesize, ref int status);
    // 写入列空值
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpcluc(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, ref int status);
    // 读取字符列数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcll(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int nultyp, char nulval, string array, string nularray, ref int anynul, ref int status);
    // 读取字符串列数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcls(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int nultyp, string nulval, string[] array, string nularray, ref int anynul, ref int status);
    // 读取字符串列数据(扩展)
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcls2(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int nultyp, string nulval, string[] array, string nularray, ref int anynul, ref int status);
    // 读取无符号字符列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgclb(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int elemincre, int nultyp, byte nulval, byte[] array, string nularray, ref int anynul, ref int status);
    // 读取有符号字符列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgclsb(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int elemincre, int nultyp, sbyte nulval, sbyte[] array, string nularray, ref int anynul, ref int status);
    // 读取无符号短整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgclui(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int elemincre, int nultyp, ushort nulval, ushort[] array, string nularray, ref int anynul, ref int status);
    // 读取短整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcli(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int elemincre, int nultyp, short nulval, short[] array, string nularray, ref int anynul, ref int status);
    // 读取无符号长整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcluj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int elemincre, int nultyp, uint nulval, uint[] array, string nularray, ref int anynul, ref int status);
    // 读取无符号64位整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgclujj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int elemincre, int nultyp, ulong nulval, ulong[] array, string nularray, ref int anynul, ref int status);
    // 读取64位整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcljj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int elemincre, int nultyp, long nulval, long[] array, string nularray, ref int anynul, ref int status);
    // 读取长整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgclj(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int elemincre, int nultyp, int nulval, int[] array, string nularray, ref int anynul, ref int status);
    // 读取无符号整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcluk(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int elemincre, int nultyp, uint nulval, uint[] array, string nularray, ref int anynul, ref int status);
    // 读取整型列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgclk(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int elemincre, int nultyp, int nulval, int[] array, string nularray, ref int anynul, ref int status);
    // 读取单精度浮点列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcle(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int elemincre, int nultyp, float nulval, float[] array, string nularray, ref int anynul, ref int status);
    // 读取双精度浮点列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgcld(IntPtr fptr, int colnum, long firstrow, long firstelem, long nelem, int elemincre, int nultyp, double nulval, double[] array, string nularray, ref int anynul, ref int status);
    // 写入1字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpi1b(IntPtr fptr, int nelem, int incre, byte[] buffer, ref int status);
    // 写入2字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpi2b(IntPtr fptr, int nelem, int incre, short[] buffer, ref int status);
    // 写入4字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpi4b(IntPtr fptr, int nelem, int incre, int[] buffer, ref int status);
    // 写入8字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpi8b(IntPtr fptr, int nelem, int incre, int[] buffer, ref int status);
    // 写入单精度图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpr4b(IntPtr fptr, int nelem, int incre, float[] buffer, ref int status);
    // 写入双精度图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffpr8b(IntPtr fptr, int nelem, int incre, double[] buffer, ref int status);
    // 读取1字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgi1b(IntPtr fptr, long pos, int nelem, int incre, byte[] buffer, ref int status);
    // 读取2字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgi2b(IntPtr fptr, long pos, int nelem, int incre, short[] buffer, ref int status);
    // 读取4字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgi4b(IntPtr fptr, long pos, int nelem, int incre, int[] buffer, ref int status);
    // 读取8字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgi8b(IntPtr fptr, long pos, int nelem, int incre, int[] buffer, ref int status);
    // 读取单精度图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgr4b(IntPtr fptr, long pos, int nelem, int incre, float[] buffer, ref int status);
    // 读取双精度图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffgr8b(IntPtr fptr, long pos, int nelem, int incre, double[] buffer, ref int status);
    // 插入图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffcins(IntPtr fptr, long naxis1, long naxis2, long nbytes, long bytepos, ref int status);
    // 删除图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffcdel(IntPtr fptr, long naxis1, long naxis2, long nbytes, long bytepos, ref int status);
    // 关键字移位
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffkshf(IntPtr fptr, int firstcol, int tfields, int nshift, ref int status);
    // 查找可变列
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffvcl(IntPtr fptr, ref int nvarcols, int[] colnums, ref int status);
    // 1字节数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi1i1(byte[] input, int ntodo, double scale, double zero, int nullcheck, byte tnull, byte nullval, string nullarray, ref int anynull, byte[] output, ref int status);
    // 2字节数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi2i1(short[] input, int ntodo, double scale, double zero, int nullcheck, short tnull, byte nullval, string nullarray, ref int anynull, byte[] output, ref int status);
    // 4字节数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi4i1(int[] input, int ntodo, double scale, double zero, int nullcheck, int tnull, byte nullval, string nullarray, ref int anynull, byte[] output, ref int status);
    // 8字节数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi8i1(long[] input, int ntodo, double scale, double zero, int nullcheck, long tnull, byte nullval, string nullarray, ref int anynull, byte[] output, ref int status);
    // 单精度转1字节数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr4i1(float[] input, int ntodo, double scale, double zero, int nullcheck, byte nullval, string nullarray, ref int anynull, byte[] output, ref int status);
    // 双精度转1字节数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr8i1(double[] input, int ntodo, double scale, double zero, int nullcheck, byte nullval, string nullarray, ref int anynull, byte[] output, ref int status);
    // 字符串转1字节数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffstri1(string input, int ntodo, double scale, double zero, int twidth, double power, int nullcheck, string snull, byte nullval, string nullarray, ref int anynull, byte[] output, ref int status);
    // 无符号char转有符号char数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi1s1(byte[] input, int ntodo, double scale, double zero, int nullcheck, byte tnull, sbyte nullval, string nullarray, ref int anynull, sbyte[] output, ref int status);
    // short转有符号char数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi2s1(short[] input, int ntodo, double scale, double zero, int nullcheck, short tnull, sbyte nullval, string nullarray, ref int anynull, sbyte[] output, ref int status);
    // int转有符号char数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi4s1(int[] input, int ntodo, double scale, double zero, int nullcheck, int tnull, sbyte nullval, string nullarray, ref int anynull, sbyte[] output, ref int status);
    // long long转有符号char数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi8s1(long[] input, int ntodo, double scale, double zero, int nullcheck, long tnull, sbyte nullval, string nullarray, ref int anynull, sbyte[] output, ref int status);
    // float转有符号char数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr4s1(float[] input, int ntodo, double scale, double zero, int nullcheck, sbyte nullval, string nullarray, ref int anynull, sbyte[] output, ref int status);
    // double转有符号char数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr8s1(double[] input, int ntodo, double scale, double zero, int nullcheck, sbyte nullval, string nullarray, ref int anynull, sbyte[] output, ref int status);
    // 字符串转有符号char数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffstrs1(string input, int ntodo, double scale, double zero, int twidth, double power, int nullcheck, string snull, sbyte nullval, string nullarray, ref int anynull, sbyte[] output, ref int status);
    // 无符号char转无符号short数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi1u2(byte[] input, int ntodo, double scale, double zero, int nullcheck, byte tnull, ushort nullval, string nullarray, ref int anynull, ushort[] output, ref int status);
    // short转无符号short数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi2u2(short[] input, int ntodo, double scale, double zero, int nullcheck, short tnull, ushort nullval, string nullarray, ref int anynull, ushort[] output, ref int status);
    // int转无符号short数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi4u2(int[] input, int ntodo, double scale, double zero, int nullcheck, int tnull, ushort nullval, string nullarray, ref int anynull, ushort[] output, ref int status);
    // long long转无符号short数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi8u2(long[] input, int ntodo, double scale, double zero, int nullcheck, long tnull, ushort nullval, string nullarray, ref int anynull, ushort[] output, ref int status);
    // float转无符号short数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr4u2(float[] input, int ntodo, double scale, double zero, int nullcheck, ushort nullval, string nullarray, ref int anynull, ushort[] output, ref int status);
    // double转无符号short数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr8u2(double[] input, int ntodo, double scale, double zero, int nullcheck, ushort nullval, string nullarray, ref int anynull, ushort[] output, ref int status);
    // 字符串转无符号short数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffstru2(string input, int ntodo, double scale, double zero, int twidth, double power, int nullcheck, string snull, ushort nullval, string nullarray, ref int anynull, ushort[] output, ref int status);
    // 无符号char转short数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi1i2(byte[] input, int ntodo, double scale, double zero, int nullcheck, byte tnull, short nullval, string nullarray, ref int anynull, short[] output, ref int status);
    // short转short数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi2i2(short[] input, int ntodo, double scale, double zero, int nullcheck, short tnull, short nullval, string nullarray, ref int anynull, short[] output, ref int status);
    // int转short数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi4i2(int[] input, int ntodo, double scale, double zero, int nullcheck, int tnull, short nullval, string nullarray, ref int anynull, short[] output, ref int status);
    // long long转short数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi8i2(long[] input, int ntodo, double scale, double zero, int nullcheck, long tnull, short nullval, string nullarray, ref int anynull, short[] output, ref int status);
    // float转short数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr4i2(float[] input, int ntodo, double scale, double zero, int nullcheck, short nullval, string nullarray, ref int anynull, short[] output, ref int status);
    // double转short数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr8i2(double[] input, int ntodo, double scale, double zero, int nullcheck, short nullval, string nullarray, ref int anynull, short[] output, ref int status);
    // 字符串转short数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffstri2(string input, int ntodo, double scale, double zero, int twidth, double power, int nullcheck, string snull, short nullval, string nullarray, ref int anynull, short[] output, ref int status);
    // 无符号char转无符号long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi1u4(byte[] input, int ntodo, double scale, double zero, int nullcheck, byte tnull, uint nullval, string nullarray, ref int anynull, uint[] output, ref int status);
    // short转无符号long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi2u4(short[] input, int ntodo, double scale, double zero, int nullcheck, short tnull, uint nullval, string nullarray, ref int anynull, uint[] output, ref int status);
    // int转无符号long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi4u4(int[] input, int ntodo, double scale, double zero, int nullcheck, int tnull, uint nullval, string nullarray, ref int anynull, uint[] output, ref int status);
    // long long转无符号long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi8u4(long[] input, int ntodo, double scale, double zero, int nullcheck, long tnull, uint nullval, string nullarray, ref int anynull, uint[] output, ref int status);
    // float转无符号long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr4u4(float[] input, int ntodo, double scale, double zero, int nullcheck, uint nullval, string nullarray, ref int anynull, uint[] output, ref int status);
    // double转无符号long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr8u4(double[] input, int ntodo, double scale, double zero, int nullcheck, uint nullval, string nullarray, ref int anynull, uint[] output, ref int status);
    // 字符串转无符号long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffstru4(string input, int ntodo, double scale, double zero, int twidth, double power, int nullcheck, string snull, uint nullval, string nullarray, ref int anynull, uint[] output, ref int status);
    // 无符号char转long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi1i4(byte[] input, int ntodo, double scale, double zero, int nullcheck, byte tnull, int nullval, string nullarray, ref int anynull, int[] output, ref int status);
    // short转long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi2i4(short[] input, int ntodo, double scale, double zero, int nullcheck, short tnull, int nullval, string nullarray, ref int anynull, int[] output, ref int status);
    // int转long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi4i4(int[] input, int ntodo, double scale, double zero, int nullcheck, int tnull, int nullval, string nullarray, ref int anynull, int[] output, ref int status);
    // long long转long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi8i4(long[] input, int ntodo, double scale, double zero, int nullcheck, long tnull, int nullval, string nullarray, ref int anynull, int[] output, ref int status);
    // float转long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr4i4(float[] input, int ntodo, double scale, double zero, int nullcheck, int nullval, string nullarray, ref int anynull, int[] output, ref int status);
    // double转long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr8i4(double[] input, int ntodo, double scale, double zero, int nullcheck, int nullval, string nullarray, ref int anynull, int[] output, ref int status);
    // 字符串转long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffstri4(string input, int ntodo, double scale, double zero, int twidth, double power, int nullcheck, string snull, int nullval, string nullarray, ref int anynull, int[] output, ref int status);
    // 无符号char转int数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi1int(byte[] input, int ntodo, double scale, double zero, int nullcheck, byte tnull, int nullval, string nullarray, ref int anynull, int[] output, ref int status);
    // short转int数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi2int(short[] input, int ntodo, double scale, double zero, int nullcheck, short tnull, int nullval, string nullarray, ref int anynull, int[] output, ref int status);
    // int转int数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi4int(int[] input, int ntodo, double scale, double zero, int nullcheck, int tnull, int nullval, string nullarray, ref int anynull, int[] output, ref int status);
    // long long转int数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi8int(long[] input, int ntodo, double scale, double zero, int nullcheck, long tnull, int nullval, string nullarray, ref int anynull, int[] output, ref int status);
    // float转int数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr4int(float[] input, int ntodo, double scale, double zero, int nullcheck, int nullval, string nullarray, ref int anynull, int[] output, ref int status);
    // double转int数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr8int(double[] input, int ntodo, double scale, double zero, int nullcheck, int nullval, string nullarray, ref int anynull, int[] output, ref int status);
    // 字符串转int数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffstrint(string input, int ntodo, double scale, double zero, int twidth, double power, int nullcheck, string snull, int nullval, string nullarray, ref int anynull, int[] output, ref int status);
    // 无符号char转无符号int数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi1uint(byte[] input, int ntodo, double scale, double zero, int nullcheck, byte tnull, uint nullval, string nullarray, ref int anynull, uint[] output, ref int status);
    // short转无符号int数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi2uint(short[] input, int ntodo, double scale, double zero, int nullcheck, short tnull, uint nullval, string nullarray, ref int anynull, uint[] output, ref int status);
    // int转无符号int数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi4uint(int[] input, int ntodo, double scale, double zero, int nullcheck, int tnull, uint nullval, string nullarray, ref int anynull, uint[] output, ref int status);
    // long long转无符号int数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi8uint(long[] input, int ntodo, double scale, double zero, int nullcheck, long tnull, uint nullval, string nullarray, ref int anynull, uint[] output, ref int status);
    // float转无符号int数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr4uint(float[] input, int ntodo, double scale, double zero, int nullcheck, uint nullval, string nullarray, ref int anynull, uint[] output, ref int status);
    // double转无符号int数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr8uint(double[] input, int ntodo, double scale, double zero, int nullcheck, uint nullval, string nullarray, ref int anynull, uint[] output, ref int status);
    // 字符串转无符号int数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffstruint(string input, int ntodo, double scale, double zero, int twidth, double power, int nullcheck, string snull, uint nullval, string nullarray, ref int anynull, uint[] output, ref int status);
    // 无符号char转long long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi1i8(byte[] input, int ntodo, double scale, double zero, int nullcheck, byte tnull, long nullval, string nullarray, ref int anynull, long[] output, ref int status);
    // short转long long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi2i8(short[] input, int ntodo, double scale, double zero, int nullcheck, short tnull, long nullval, string nullarray, ref int anynull, long[] output, ref int status);
    // int转long long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi4i8(int[] input, int ntodo, double scale, double zero, int nullcheck, int tnull, long nullval, string nullarray, ref int anynull, long[] output, ref int status);
    // long long转long long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi8i8(long[] input, int ntodo, double scale, double zero, int nullcheck, long tnull, long nullval, string nullarray, ref int anynull, long[] output, ref int status);
    // float转long long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr4i8(float[] input, int ntodo, double scale, double zero, int nullcheck, long nullval, string nullarray, ref int anynull, long[] output, ref int status);
    // double转long long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr8i8(double[] input, int ntodo, double scale, double zero, int nullcheck, long nullval, string nullarray, ref int anynull, long[] output, ref int status);
    // 字符串转long long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffstri8(string input, int ntodo, double scale, double zero, int twidth, double power, int nullcheck, string snull, long nullval, string nullarray, ref int anynull, long[] output, ref int status);
    // 无符号char转无符号long long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi1u8(byte[] input, int ntodo, double scale, double zero, int nullcheck, byte tnull, ulong nullval, string nullarray, ref int anynull, ulong[] output, ref int status);
    // short转无符号long long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi2u8(short[] input, int ntodo, double scale, double zero, int nullcheck, short tnull, ulong nullval, string nullarray, ref int anynull, ulong[] output, ref int status);
    // int转无符号long long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi4u8(int[] input, int ntodo, double scale, double zero, int nullcheck, int tnull, ulong nullval, string nullarray, ref int anynull, ulong[] output, ref int status);
    // long long转无符号long long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi8u8(long[] input, int ntodo, double scale, double zero, int nullcheck, long tnull, ulong nullval, string nullarray, ref int anynull, ulong[] output, ref int status);
    // float转无符号long long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr4u8(float[] input, int ntodo, double scale, double zero, int nullcheck, ulong nullval, string nullarray, ref int anynull, ulong[] output, ref int status);
    // double转无符号long long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr8u8(double[] input, int ntodo, double scale, double zero, int nullcheck, ulong nullval, string nullarray, ref int anynull, ulong[] output, ref int status);
    // 字符串转无符号long long数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffstru8(string input, int ntodo, double scale, double zero, int twidth, double power, int nullcheck, string snull, ulong nullval, string nullarray, ref int anynull, ulong[] output, ref int status);
    // 无符号char转float数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi1r4(byte[] input, int ntodo, double scale, double zero, int nullcheck, byte tnull, float nullval, string nullarray, ref int anynull, float[] output, ref int status);
    // short转float数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi2r4(short[] input, int ntodo, double scale, double zero, int nullcheck, short tnull, float nullval, string nullarray, ref int anynull, float[] output, ref int status);
    // int转float数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi4r4(int[] input, int ntodo, double scale, double zero, int nullcheck, int tnull, float nullval, string nullarray, ref int anynull, float[] output, ref int status);
    // long long转float数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi8r4(long[] input, int ntodo, double scale, double zero, int nullcheck, long tnull, float nullval, string nullarray, ref int anynull, float[] output, ref int status);
    // float转float数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr4r4(float[] input, int ntodo, double scale, double zero, int nullcheck, float nullval, string nullarray, ref int anynull, float[] output, ref int status);
    // double转float数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr8r4(double[] input, int ntodo, double scale, double zero, int nullcheck, float nullval, string nullarray, ref int anynull, float[] output, ref int status);
    // 字符串转float数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffstrr4(string input, int ntodo, double scale, double zero, int twidth, double power, int nullcheck, string snull, float nullval, string nullarray, ref int anynull, float[] output, ref int status);
    // 无符号char转double数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi1r8(byte[] input, int ntodo, double scale, double zero, int nullcheck, byte tnull, double nullval, string nullarray, ref int anynull, double[] output, ref int status);
    // short转double数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi2r8(short[] input, int ntodo, double scale, double zero, int nullcheck, short tnull, double nullval, string nullarray, ref int anynull, double[] output, ref int status);
    // int转double数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi4r8(int[] input, int ntodo, double scale, double zero, int nullcheck, int tnull, double nullval, string nullarray, ref int anynull, double[] output, ref int status);
    // long long转double数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffi8r8(long[] input, int ntodo, double scale, double zero, int nullcheck, long tnull, double nullval, string nullarray, ref int anynull, double[] output, ref int status);
    // float转double数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr4r8(float[] input, int ntodo, double scale, double zero, int nullcheck, double nullval, string nullarray, ref int anynull, double[] output, ref int status);
    // double转double数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffr8r8(double[] input, int ntodo, double scale, double zero, int nullcheck, double nullval, string nullarray, ref int anynull, double[] output, ref int status);
    // 字符串转double数据转换
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int fffstrr8(string input, int ntodo, double scale, double zero, int twidth, double power, int nullcheck, string snull, double nullval, string nullarray, ref int anynull, double[] output, ref int status);
    // 无符号char转1字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi1fi1(byte[] array, int ntodo, double scale, double zero, byte[] buffer, ref int status);
    // 有符号char转1字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffs1fi1(sbyte[] array, int ntodo, double scale, double zero, byte[] buffer, ref int status);
    // 无符号short转1字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu2fi1(ushort[] array, int ntodo, double scale, double zero, byte[] buffer, ref int status);
    // short转1字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi2fi1(short[] array, int ntodo, double scale, double zero, byte[] buffer, ref int status);
    // 无符号long转1字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu4fi1(uint[] array, int ntodo, double scale, double zero, byte[] buffer, ref int status);
    // long转1字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi4fi1(int[] array, int ntodo, double scale, double zero, byte[] buffer, ref int status);
    // 无符号long long转1字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu8fi1(ulong[] array, int ntodo, double scale, double zero, byte[] buffer, ref int status);
    // long long转1字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi8fi1(long[] array, int ntodo, double scale, double zero, byte[] buffer, ref int status);
    // 无符号int转1字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffuintfi1(uint[] array, int ntodo, double scale, double zero, byte[] buffer, ref int status);
    // int转1字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffintfi1(int[] array, int ntodo, double scale, double zero, byte[] buffer, ref int status);
    // float转1字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr4fi1(float[] array, int ntodo, double scale, double zero, byte[] buffer, ref int status);
    // double转1字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr8fi1(double[] array, int ntodo, double scale, double zero, byte[] buffer, ref int status);
    // 无符号char转2字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi1fi2(byte[] array, int ntodo, double scale, double zero, short[] buffer, ref int status);
    // 有符号char转2字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffs1fi2(sbyte[] array, int ntodo, double scale, double zero, short[] buffer, ref int status);
    // 无符号short转2字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu2fi2(ushort[] array, int ntodo, double scale, double zero, short[] buffer, ref int status);
    // short转2字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi2fi2(short[] array, int ntodo, double scale, double zero, short[] buffer, ref int status);
    // 无符号long转2字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu4fi2(uint[] array, int ntodo, double scale, double zero, short[] buffer, ref int status);
    // long转2字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi4fi2(int[] array, int ntodo, double scale, double zero, short[] buffer, ref int status);
    // 无符号long long转2字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu8fi2(ulong[] array, int ntodo, double scale, double zero, short[] buffer, ref int status);
    // long long转2字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi8fi2(long[] array, int ntodo, double scale, double zero, short[] buffer, ref int status);
    // 无符号int转2字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffuintfi2(uint[] array, int ntodo, double scale, double zero, short[] buffer, ref int status);
    // int转2字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffintfi2(int[] array, int ntodo, double scale, double zero, short[] buffer, ref int status);
    // float转2字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr4fi2(float[] array, int ntodo, double scale, double zero, short[] buffer, ref int status);
    // double转2字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr8fi2(double[] array, int ntodo, double scale, double zero, short[] buffer, ref int status);
    // 无符号char转4字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi1fi4(byte[] array, int ntodo, double scale, double zero, int[] buffer, ref int status);
    // 有符号char转4字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffs1fi4(sbyte[] array, int ntodo, double scale, double zero, int[] buffer, ref int status);
    // 无符号short转4字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu2fi4(ushort[] array, int ntodo, double scale, double zero, int[] buffer, ref int status);
    // short转4字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi2fi4(short[] array, int ntodo, double scale, double zero, int[] buffer, ref int status);
    // 无符号long转4字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu4fi4(uint[] array, int ntodo, double scale, double zero, int[] buffer, ref int status);
    // 无符号long long转4字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu8fi4(ulong[] array, int ntodo, double scale, double zero, int[] buffer, ref int status);
    // long转4字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi4fi4(int[] array, int ntodo, double scale, double zero, int[] buffer, ref int status);
    // long long转4字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi8fi4(long[] array, int ntodo, double scale, double zero, int[] buffer, ref int status);
    // 无符号int转4字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffuintfi4(uint[] array, int ntodo, double scale, double zero, int[] buffer, ref int status);
    // int转4字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffintfi4(int[] array, int ntodo, double scale, double zero, int[] buffer, ref int status);
    // float转4字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr4fi4(float[] array, int ntodo, double scale, double zero, int[] buffer, ref int status);
    // double转4字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr8fi4(double[] array, int ntodo, double scale, double zero, int[] buffer, ref int status);
    // long转8字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi4fi8(int[] array, int ntodo, double scale, double zero, long[] buffer, ref int status);
    // long long转8字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi8fi8(long[] array, int ntodo, double scale, double zero, long[] buffer, ref int status);
    // short转8字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi2fi8(short[] array, int ntodo, double scale, double zero, long[] buffer, ref int status);
    // 无符号char转8字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi1fi8(byte[] array, int ntodo, double scale, double zero, long[] buffer, ref int status);
    // 有符号char转8字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffs1fi8(sbyte[] array, int ntodo, double scale, double zero, long[] buffer, ref int status);
    // float转8字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr4fi8(float[] array, int ntodo, double scale, double zero, long[] buffer, ref int status);
    // double转8字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr8fi8(double[] array, int ntodo, double scale, double zero, long[] buffer, ref int status);
    // int转8字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffintfi8(int[] array, int ntodo, double scale, double zero, long[] buffer, ref int status);
    // 无符号short转8字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu2fi8(ushort[] array, int ntodo, double scale, double zero, long[] buffer, ref int status);
    // 无符号long转8字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu4fi8(uint[] array, int ntodo, double scale, double zero, long[] buffer, ref int status);
    // 无符号long long转8字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu8fi8(ulong[] array, int ntodo, double scale, double zero, long[] buffer, ref int status);
    // 无符号int转8字节图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffuintfi8(uint[] array, int ntodo, double scale, double zero, long[] buffer, ref int status);
    // 无符号char转float图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi1fr4(byte[] array, int ntodo, double scale, double zero, float[] buffer, ref int status);
    // 有符号char转float图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffs1fr4(sbyte[] array, int ntodo, double scale, double zero, float[] buffer, ref int status);
    // 无符号short转float图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu2fr4(ushort[] array, int ntodo, double scale, double zero, float[] buffer, ref int status);
    // short转float图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi2fr4(short[] array, int ntodo, double scale, double zero, float[] buffer, ref int status);
    // 无符号long转float图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu4fr4(uint[] array, int ntodo, double scale, double zero, float[] buffer, ref int status);
    // long转float图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi4fr4(int[] array, int ntodo, double scale, double zero, float[] buffer, ref int status);
    // 无符号long long转float图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu8fr4(ulong[] array, int ntodo, double scale, double zero, float[] buffer, ref int status);
    // long long转float图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi8fr4(long[] array, int ntodo, double scale, double zero, float[] buffer, ref int status);
    // 无符号int转float图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffuintfr4(uint[] array, int ntodo, double scale, double zero, float[] buffer, ref int status);
    // int转float图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffintfr4(int[] array, int ntodo, double scale, double zero, float[] buffer, ref int status);
    // float转float图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr4fr4(float[] array, int ntodo, double scale, double zero, float[] buffer, ref int status);
    // double转float图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr8fr4(double[] array, int ntodo, double scale, double zero, float[] buffer, ref int status);
    // 无符号char转double图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi1fr8(byte[] array, int ntodo, double scale, double zero, double[] buffer, ref int status);
    // 有符号char转double图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffs1fr8(sbyte[] array, int ntodo, double scale, double zero, double[] buffer, ref int status);
    // 无符号short转double图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu2fr8(ushort[] array, int ntodo, double scale, double zero, double[] buffer, ref int status);
    // short转double图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi2fr8(short[] array, int ntodo, double scale, double zero, double[] buffer, ref int status);
    // 无符号long转double图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu4fr8(uint[] array, int ntodo, double scale, double zero, double[] buffer, ref int status);
    // long转double图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi4fr8(int[] array, int ntodo, double scale, double zero, double[] buffer, ref int status);
    // 无符号long long转double图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu8fr8(ulong[] array, int ntodo, double scale, double zero, double[] buffer, ref int status);
    // long long转double图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi8fr8(long[] array, int ntodo, double scale, double zero, double[] buffer, ref int status);
    // 无符号int转double图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffuintfr8(uint[] array, int ntodo, double scale, double zero, double[] buffer, ref int status);
    // int转double图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffintfr8(int[] array, int ntodo, double scale, double zero, double[] buffer, ref int status);
    // float转double图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr4fr8(float[] array, int ntodo, double scale, double zero, double[] buffer, ref int status);
    // double转double图像数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr8fr8(double[] array, int ntodo, double scale, double zero, double[] buffer, ref int status);
    // 无符号char转字符串数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi1fstr(byte[] input, int ntodo, double scale, double zero, string cform, int twidth, string output, ref int status);
    // 有符号char转字符串数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffs1fstr(sbyte[] input, int ntodo, double scale, double zero, string cform, int twidth, string output, ref int status);
    // 无符号short转字符串数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu2fstr(ushort[] input, int ntodo, double scale, double zero, string cform, int twidth, string output, ref int status);
    // short转字符串数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi2fstr(short[] input, int ntodo, double scale, double zero, string cform, int twidth, string output, ref int status);
    // 无符号long转字符串数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu4fstr(uint[] input, int ntodo, double scale, double zero, string cform, int twidth, string output, ref int status);
    // long转字符串数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi4fstr(int[] input, int ntodo, double scale, double zero, string cform, int twidth, string output, ref int status);
    // 无符号long long转字符串数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffu8fstr(ulong[] input, int ntodo, double scale, double zero, string cform, int twidth, string output, ref int status);
    // long long转字符串数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffi8fstr(long[] input, int ntodo, double scale, double zero, string cform, int twidth, string output, ref int status);
    // int转字符串数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffintfstr(int[] input, int ntodo, double scale, double zero, string cform, int twidth, string output, ref int status);
    // 无符号int转字符串数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffuintfstr(uint[] input, int ntodo, double scale, double zero, string cform, int twidth, string output, ref int status);
    // float转字符串数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr4fstr(float[] input, int ntodo, double scale, double zero, string cform, int twidth, string output, ref int status);
    // double转字符串数据
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)] public static extern int ffr8fstr(double[] input, int ntodo, double scale, double zero, string cform, int twidth, string output, ref int status);

    //=============================================================================
    //  VMS 宏函数
    //=============================================================================
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ieevpd(double[] inarray, double[] outarray, ref long nvals);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ieevud(double[] inarray, double[] outarray, ref int nvals);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ieevpr(float[] inarray, float[] outarray, ref int nvals);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ieevur(float[] inarray, float[] outarray, ref int nvals);

    //=============================================================================
    //  词法解析器相关
    //=============================================================================
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffselect_table(IntPtr fptr, string outfile, string expr, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffiprs(
        IntPtr fptr, int compressed, string expr, int maxdim, ref int datatype, ref int nelem, ref int naxis, [Out] int[] naxes, IntPtr parseData, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern void ffcprs(IntPtr parseData);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffcvtn(
        int inputType, IntPtr input, string undef, int ntodo, int outputType, IntPtr nulval, IntPtr output, ref int anynull, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_parser_workfn(int totalrows, int offset, int firstrow, int nrows, int nCols, IntPtr colData, IntPtr userPtr);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_uncompress_hkdata(
        IntPtr parseData, IntPtr fptr, int ntimes, double[] times, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ffffrw_work(int totalrows, int offset, int firstrow, int nrows, int nCols, IntPtr colData, IntPtr userPtr);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_translate_pixkeyword(
        string inrec, string outrec, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPTStr)] string[,] patterns,
        int npat,
        int naxis,
        ref int colnum,
        ref int pat_num,
        ref int i,
        ref int j,
        ref int n,
        ref int m,
        ref int l,
        ref int status);

    //=============================================================================
    //  图像压缩
    //=============================================================================
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_write_compressed_img(
        IntPtr fptr, int datatype, long[] fpixel, long[] lpixel, int nullcheck, IntPtr array, IntPtr nulval, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_write_compressed_pixels(
        IntPtr fptr, int datatype, long fpixel, long npixels, int nullcheck, IntPtr array, IntPtr nulval, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_write_compressed_img_plane(
        IntPtr fptr, int datatype, int bytesperpixel, int nplane, int[] firstcoord, int[] lastcoord, int[] naxes, int nullcheck, IntPtr array, IntPtr nullval, ref int nread, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_init_table(
        IntPtr outfptr, int bitpix, int naxis, int[] naxes, int writebitpix, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_calc_max_elem(int comptype, int nx, int zbitpix, int blocksize);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_copy_imheader(IntPtr infptr, IntPtr outfptr, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_copy_img2comp(IntPtr infptr, IntPtr outfptr, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_copy_comp2img(IntPtr infptr, IntPtr outfptr, int norec, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_copy_prime2img(IntPtr infptr, IntPtr outfptr, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_compress_image(IntPtr infptr, IntPtr outfptr, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_compress_tile(
        IntPtr outfptr, int row, int datatype, IntPtr tiledata, int tilelen, int nx, int ny, int nullcheck, IntPtr nullval, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_nullscale(
        int[] idata, int tilelen, int nullflagval, int nullval, double scale, double zero, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_nullvalues(
        int[] idata, int tilelen, int nullflagval, int nullval, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_scalevalues(
        int[] idata, int tilelen, double scale, double zero, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_nullscalefloats(
        float[] fdata, int tilelen, int[] idata, double scale, double zero, int nullcheck, float nullflagval, int nullval, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_nullfloats(
        float[] fdata, int tilelen, int[] idata, int nullcheck, float nullflagval, int nullval, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_nullscaledoubles(
        double[] fdata, int tilelen, int[] idata, double scale, double zero, int nullcheck, double nullflagval, int nullval, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_nulldoubles(
        double[] fdata, int tilelen, int[] idata, int nullcheck, double nullflagval, int nullval, ref int status);

    //=============================================================================
    //  图像解压缩
    //=============================================================================
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_read_compressed_img(
        IntPtr fptr, int datatype, long[] fpixel, long[] lpixel, long[] inc, int nullcheck, IntPtr nulval, IntPtr array, string nullarray, ref int anynul, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_read_compressed_pixels(
        IntPtr fptr, int datatype, long fpixel, long npixels, int nullcheck, IntPtr nulval, IntPtr array, string nullarray, ref int anynul, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_read_compressed_img_plane(
        IntPtr fptr, int datatype, int bytesperpixel, int nplane, long[] firstcoord, long[] lastcoord, int[] inc, int[] naxes, int nullcheck, IntPtr nullval, IntPtr array, string nullarray, ref int anynul, ref int nread, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_get_compressed_image_par(IntPtr infptr, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_decompress_tile(
        IntPtr infptr, int nrow, int tilesize, int datatype, int nullcheck, IntPtr nulval, IntPtr buffer, string bnullarray, ref int anynul, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_copy_overlap(
        string tile, int pixlen, int ndim, int[] tfpixel, int[] tlpixel, string bnullarray, string image, int[] fpixel, int[] lpixel, int[] inc, int nullcheck, string nullarray, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_test_overlap(
        int ndim, int[] tfpixel, int[] tlpixel, int[] fpixel, int[] lpixel, int[] inc, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_merge_overlap(
        string tile, int pixlen, int ndim, int[] tfpixel, int[] tlpixel, string bnullarray, string image, int[] fpixel, int[] lpixel, int nullcheck, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int imcomp_decompress_img(IntPtr infptr, IntPtr outfptr, int datatype, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_quantize_float(int row, float[] fdata, int nx, int ny, int nullcheck, float in_null_value, float quantize_level, int dither_method, int[] idata, ref double bscale, ref double bzero, ref int iminval, ref int imaxval);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_quantize_double(int row, double[] fdata, int nx, int ny, int nullcheck, double in_null_value, float quantize_level, int dither_method, int[] idata, ref double bscale, ref double bzero, ref int iminval, ref int imaxval);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_rcomp(int[] a, int nx, byte[] c, int clen, int nblock);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_rcomp_short(short[] a, int nx, byte[] c, int clen, int nblock);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_rcomp_byte(sbyte[] a, int nx, byte[] c, int clen, int nblock);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_rdecomp(byte[] c, int clen, uint[] array, int nx, int nblock);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_rdecomp_short(byte[] c, int clen, ushort[] array, int nx, int nblock);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int pl_p2li(int[] pxsrc, int xs, short[] lldst, int npix);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int pl_l2pi(short[] ll_src, int xs, int[] px_dst, int npix);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_init_randoms();

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_unset_compression_param(IntPtr fptr, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_unset_compression_request(IntPtr fptr, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fitsio_init_lock();

    // 底层IO
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_rdecomp_byte(byte[] c, int clen, byte[] array, int nx, int nblock);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int urltype2driver(string urltype, ref int driver);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern void fits_dwnld_prog_bar(int flag);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_net_timeout(int sec);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_register_driver(
        string prefix,
        IntPtr init,
        IntPtr fitsshutdown,
        IntPtr setoptions,
        IntPtr getoptions,
        IntPtr getversion,
        IntPtr checkfile,
        IntPtr fitsopen,
        IntPtr fitscreate,
        IntPtr fitstruncate,
        IntPtr fitsclose,
        IntPtr fremove,
        IntPtr size,
        IntPtr flush,
        IntPtr seek,
        IntPtr fitsread,
        IntPtr fitswrite);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_init();

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_setoptions(int options);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_getoptions(ref int options);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_getversion(ref int version);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_shutdown();

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_checkfile(string urltype, string infile, string outfile);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_open(string filename, int rwmode, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_compress_open(string filename, int rwmode, ref int hdl);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_openfile(string filename, int rwmode, ref IntPtr diskfile);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_create(string filename, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_truncate(int driverhandle, long filesize);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_size(int driverhandle, ref long filesize);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_close(int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_remove(string filename);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_flush(int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_seek(int driverhandle, long offset);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_read(int driverhandle, IntPtr buffer, int nbytes);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_write(int driverhandle, IntPtr buffer, int nbytes);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int file_is_compressed(string filename);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int stream_open(string filename, int rwmode, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int stream_create(string filename, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int stream_size(int driverhandle, ref long filesize);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int stream_close(int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int stream_flush(int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int stream_seek(int driverhandle, long offset);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int stream_read(int driverhandle, IntPtr buffer, int nbytes);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int stream_write(int driverhandle, IntPtr buffer, int nbytes);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_init();

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_setoptions(int options);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_getoptions(ref int options);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_getversion(ref int version);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_shutdown();

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_create(string filename, ref int handle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_create_comp(string filename, ref int handle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_openmem(ref IntPtr buffptr, ref UIntPtr buffsize, UIntPtr deltasize, IntPtr memrealloc, ref int handle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_createmem(UIntPtr memsize, ref int handle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int stdin_checkfile(string urltype, string infile, string outfile);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int stdin_open(string filename, int rwmode, ref int handle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int stdin2mem(int hd);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int stdin2file(int hd);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int stdout_close(int handle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_compress_openrw(string filename, int rwmode, ref int hdl);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_compress_open(string filename, int rwmode, ref int hdl);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_compress_stdin_open(string filename, int rwmode, ref int hdl);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_zuncompress_and_write(int hdl, IntPtr buffer, int nbytes);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_iraf_open(string filename, int rwmode, ref int hdl);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_rawfile_open(string filename, int rwmode, ref int hdl);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_size(int handle, ref long filesize);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_truncate(int handle, long filesize);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_close_free(int handle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_close_keep(int handle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_close_comp(int handle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_seek(int handle, long offset);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_read(int hdl, IntPtr buffer, int nbytes);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_write(int hdl, IntPtr buffer, int nbytes);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_uncompress2mem(string filename, IntPtr diskfile, int hdl);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int iraf2mem(string filename, ref IntPtr buffptr, ref UIntPtr buffsize, ref UIntPtr filesize, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int root_init();

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int root_setoptions(int options);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int root_getoptions(ref int options);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int root_getversion(ref int version);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int root_shutdown();

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int root_open(string filename, int rwmode, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int root_create(string filename, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int root_close(int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int root_flush(int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int root_seek(int driverhandle, long offset);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int root_read(int driverhandle, IntPtr buffer, int nbytes);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int root_write(int driverhandle, IntPtr buffer, int nbytes);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int root_size(int handle, ref long filesize);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int http_checkfile(string urltype, string infile, string outfile);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int http_open(string filename, int rwmode, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int http_file_open(string filename, int rwmode, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int http_compress_open(string filename, int rwmode, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int https_checkfile(string urltype, string infile, string outfile);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int https_open(string filename, int rwmode, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int https_file_open(string filename, int rwmode, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern void https_set_verbose(int flag);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ftps_checkfile(string urltype, string infile, string outfile);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ftps_open(string filename, int rwmode, ref int handle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ftps_file_open(string filename, int rwmode, ref int handle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ftps_compress_open(string filename, int rwmode, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ftp_checkfile(string urltype, string infile, string outfile);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ftp_open(string filename, int rwmode, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ftp_file_open(string filename, int rwmode, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ftp_compress_open(string filename, int rwmode, ref int driverhandle);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int uncompress2mem(
        string filename,
        IntPtr diskfile,
        ref IntPtr buffptr,
        ref UIntPtr buffsize,
        IntPtr mem_realloc,
        ref UIntPtr filesize,
        ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int uncompress2mem_from_mem(
        string inmemptr,
        UIntPtr inmemsize,
        ref IntPtr buffptr,
        ref UIntPtr buffsize,
        IntPtr mem_realloc,
        ref UIntPtr filesize,
        ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int uncompress2file(string filename, IntPtr indiskfile, IntPtr outdiskfile, ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int compress2mem_from_mem(
        string inmemptr,
        UIntPtr inmemsize,
        ref IntPtr buffptr,
        ref UIntPtr buffsize,
        IntPtr mem_realloc,
        ref UIntPtr filesize,
        ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int compress2file_from_mem(
        string inmemptr,
        UIntPtr inmemsize,
        IntPtr outdiskfile,
        ref UIntPtr filesize,
        ref int status);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_strcasecmp(string s1, string s2);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_strncasecmp(string s1, string s2, UIntPtr n);

    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr fits_recalloc(IntPtr ptr, UIntPtr old_num, UIntPtr new_num, UIntPtr size);

    //=============================================================================
    //  通用驱动 回调定义
    //=============================================================================
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverInitDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverShutdownDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverSetOptionsDelegate(int option);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverGetOptionsDelegate(ref int options);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverGetVersionDelegate(ref int version);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverCheckFileDelegate(string urltype, string infile, string outfile);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverOpenDelegate(string filename, int rwmode, ref int driverhandle);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverCreateDelegate(string filename, ref int driverhandle);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverTruncateDelegate(int driverhandle, long filesize);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverCloseDelegate(int driverhandle);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverRemoveDelegate(string filename);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverSizeDelegate(int driverhandle, ref long sizex);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverFlushDelegate(int driverhandle);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverSeekDelegate(int driverhandle, long offset);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverReadDelegate(int driverhandle, IntPtr buffer, long nbytes);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DriverWriteDelegate(int driverhandle, IntPtr buffer, long nbytes);

    //=============================================================================
    //  驱动注册
    //=============================================================================
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int fits_register_driver(
        string prefix, DriverInitDelegate init, DriverShutdownDelegate fitsshutdown, DriverSetOptionsDelegate setoptions, DriverGetOptionsDelegate getoptions, DriverGetVersionDelegate getversion, DriverCheckFileDelegate checkfile, DriverOpenDelegate fitsopen, DriverCreateDelegate fitscreate, DriverTruncateDelegate fitstruncate, DriverCloseDelegate fitsclose, DriverRemoveDelegate fremove, DriverSizeDelegate size, DriverFlushDelegate flush, DriverSeekDelegate seek, DriverReadDelegate fitsread, DriverWriteDelegate fitswrite);

    // 内存驱动 回调
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr MemReallocDelegate(IntPtr p, UIntPtr newsize);

    // 内存/网络/压缩工具 全部对齐 C 原生
    [DllImport("cfitsio", CallingConvention = CallingConvention.Cdecl)]
    public static extern int mem_openmem(
        ref IntPtr buffptr, ref UIntPtr buffsize, UIntPtr deltasize, MemReallocDelegate memrealloc, ref int handle);
}