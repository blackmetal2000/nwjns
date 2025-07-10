using System;
using System.Runtime.InteropServices;

namespace newjeans
{
    partial class Win32
    {

        [DllImport("ntdll.dll", ExactSpelling = true)]
        public static extern void RtlInitUnicodeString(
            out UNICODE_STRING DestinationString,
            [MarshalAs(UnmanagedType.LPWStr)] string SourceString
        );

        [DllImport("ntdll.dll", ExactSpelling = true)]
        public static extern NTSTATUS NtOpenProcess(
            out IntPtr ProcessHandle,
            PROCESS_ACCESS_RIGHTS DesiredAccess,
            OBJECT_ATTRIBUTES ObjectAttributes,
            CLIENT_ID ClientId
        );

        [DllImport("ntdll.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Win32.NTSTATUS NtOpenFile(
            out IntPtr handle,
            uint access,
            ref OBJECT_ATTRIBUTES objectAttributes,
            out IO_STATUS_BLOCK ioStatus,
            FILE_SHARE_ACCESS share,
            FILE_OPEN_ACCESS openOptions
        );

        [DllImport("ntdll.dll", ExactSpelling = true, SetLastError = true)]
        public static extern Win32.NTSTATUS NtReadFile(
            IntPtr FileHandle,
            IntPtr Event,
            IntPtr ApcRoutine,
            IntPtr ApcContext,
            out IO_STATUS_BLOCK IoStatusBlock,
            IntPtr Buffer,
            int Length,
            int ByteOffset,
            int Key 
        );

        [DllImport("ntdll.dll", ExactSpelling = true)]
        public static extern Win32.NTSTATUS NtQueryInformationFile(
            IntPtr FileHandle,
            out IO_STATUS_BLOCK IoStatusBlock,
            IntPtr FileInformation,
            int Length,
            FILE_INFORMATION_CLASS FileInformationClass
        );

        [DllImport("ntdll.dll", ExactSpelling = true)]
        public static extern Win32.NTSTATUS ZwAllocateVirtualMemory(
            IntPtr ProcessHandle,
            ref IntPtr BaseAddress,
            int ZeroBits,
            ref int RegionSize,
            VIRTUAL_ALLOCATION_TYPE AllocationType,
            PAGE_PROTECTION_FLAGS Protect
        );

        [DllImport("ntdll.dll", ExactSpelling = true)]
        public static extern Win32.NTSTATUS NtWriteVirtualMemory(
            IntPtr ProcessHandle,
            IntPtr BaseAddress,
            IntPtr Buffer,
            int NumberOfBytesToWrite,
            out int NumberOfBytesWritten
        );


        [DllImport("ntdll.dll", ExactSpelling = true)]
        public static extern Win32.NTSTATUS NtGetNextThread(
            IntPtr ProcessHandle, // PROCESS_QUERY_INFORMATION
            IntPtr ThreadHandle,
            THREAD_ACCESS_RIGHT DesiredAccess,
            int HandleAttributes,
            int Flags, // 0,
            out IntPtr NewThreadHandle

        );

        [DllImport("ntdll.dll", ExactSpelling = true)]
        public static extern Win32.NTSTATUS NtQueueApcThreadEx2(
            IntPtr ThreadHandle,
            IntPtr ReserveHandle,
            QUEUE_USER_APC_FLAGS ApcFlags,
            IntPtr ApcRoutine,
            IntPtr ApcArgument1,
            IntPtr ApcArgument2,
            IntPtr ApcArgument3
        );

        [Flags]
        public enum QUEUE_USER_APC_FLAGS : uint
        {
            QUEUE_USER_APC_FLAGS_NONE = 0x00000000,
            QUEUE_USER_APC_FLAGS_SPECIAL_USER_APC = 0x00000001,
            QUEUE_USER_APC_FLAGS_CALLBACK_DATA_CONTEXT = 0x00010000,

        }

        [Flags]
        public enum THREAD_ACCESS_RIGHT : uint
        {
            THREAD_ALL_ACCESS = 0x001FFFFF,
            THREAD_TERMINATE = 0x00000001,
            THREAD_SUSPEND_RESUME = 0x00000002,
            THREAD_ALERT = 0x00000004,
            THREAD_GET_CONTEXT = 0x00000008,
            THREAD_SET_CONTEXT = 0x00000010,
            THREAD_SET_INFORMATION = 0x00000020,
            THREAD_SET_LIMITED_INFORMATION = 0x00000400,
            THREAD_QUERY_LIMITED_INFORMATION = 0x00000800,
        }

        [Flags]
        public enum PAGE_PROTECTION_FLAGS : uint
        {
            PAGE_NOACCESS = 0x00000001,
            PAGE_READONLY = 0x00000002,
            PAGE_READWRITE = 0x00000004,
            PAGE_WRITECOPY = 0x00000008,
            PAGE_EXECUTE = 0x00000010,
            PAGE_EXECUTE_READ = 0x00000020,
            PAGE_EXECUTE_READWRITE = 0x00000040,
            PAGE_EXECUTE_WRITECOPY = 0x00000080,
            PAGE_GUARD = 0x00000100,
            PAGE_NOCACHE = 0x00000200,
            PAGE_WRITECOMBINE = 0x00000400,
            PAGE_GRAPHICS_NOACCESS = 0x00000800,
            PAGE_GRAPHICS_READONLY = 0x00001000,
            PAGE_GRAPHICS_READWRITE = 0x00002000,
            PAGE_GRAPHICS_EXECUTE = 0x00004000,
            PAGE_GRAPHICS_EXECUTE_READ = 0x00008000,
            PAGE_GRAPHICS_EXECUTE_READWRITE = 0x00010000,
            PAGE_GRAPHICS_COHERENT = 0x00020000,
            PAGE_GRAPHICS_NOCACHE = 0x00040000,
            PAGE_ENCLAVE_THREAD_CONTROL = 0x80000000,
            PAGE_REVERT_TO_FILE_MAP = 0x80000000,
            PAGE_TARGETS_NO_UPDATE = 0x40000000,
            PAGE_TARGETS_INVALID = 0x40000000,
            PAGE_ENCLAVE_UNVALIDATED = 0x20000000,
            PAGE_ENCLAVE_MASK = 0x10000000,
            PAGE_ENCLAVE_DECOMMIT = 0x10000000,
            PAGE_ENCLAVE_SS_FIRST = 0x10000001,
            PAGE_ENCLAVE_SS_REST = 0x10000002,
            SEC_PARTITION_OWNER_HANDLE = 0x00040000,
            SEC_64K_PAGES = 0x00080000,
            SEC_FILE = 0x00800000,
            SEC_IMAGE = 0x01000000,
            SEC_PROTECTED_IMAGE = 0x02000000,
            SEC_RESERVE = 0x04000000,
            SEC_COMMIT = 0x08000000,
            SEC_NOCACHE = 0x10000000,
            SEC_WRITECOMBINE = 0x40000000,
            SEC_LARGE_PAGES = 0x80000000,
            SEC_IMAGE_NO_EXECUTE = 0x11000000,
        }

        [Flags]
        public enum VIRTUAL_ALLOCATION_TYPE : uint
        {
            MEM_COMMIT = 0x00001000,
            MEM_RESERVE = 0x00002000,
            MEM_RESET = 0x00080000,
            MEM_RESET_UNDO = 0x01000000,
            MEM_REPLACE_PLACEHOLDER = 0x00004000,
            MEM_LARGE_PAGES = 0x20000000,
            MEM_RESERVE_PLACEHOLDER = 0x00040000,
            MEM_FREE = 0x00010000,
        }


        [Flags]
        public enum FILE_INFORMATION_CLASS : uint
        {
            FileDirectoryInformation = 1,
            FileFullDirectoryInformation = 2,
            FileBothDirectoryInformation = 3,
            FileBasicInformation = 4,
            FileStandardInformation = 5,
            FileInternalInformation = 6,
            FileEaInformation = 7,
            FileAccessInformation = 8,
            FileNameInformation = 9,
            FileRenameInformation = 10,
            FileLinkInformation = 11,
            FileNamesInformation = 12,
            FileDispositionInformation = 13,
            FilePositionInformation = 14,
            FileFullEaInformation = 15,
            FileModeInformation = 16,
            FileAlignmentInformation = 17,
            FileAllInformation = 18,
            FileAllocationInformation = 19,
            FileEndOfFileInformation = 20,
            FileAlternateNameInformation = 21,
            FileStreamInformation = 22,
            FilePipeInformation = 23,
            FilePipeLocalInformation = 24,
            FilePipeRemoteInformation = 25,
            FileMailslotQueryInformation = 26,
            FileMailslotSetInformation = 27,
            FileCompressionInformation = 28,
            FileObjectIdInformation = 29,
            FileCompletionInformation = 30,
            FileMoveClusterInformation = 31,
            FileQuotaInformation = 32,
            FileReparsePointInformation = 33,
            FileNetworkOpenInformation = 34,
            FileAttributeTagInformation = 35,
            FileTrackingInformation = 36,
            FileIdBothDirectoryInformation = 37,
            FileIdFullDirectoryInformation = 38,
            FileValidDataLengthInformation = 39,
            FileShortNameInformation = 40,
            FileIoCompletionNotificationInformation = 41,
            FileIoStatusBlockRangeInformation = 42,
            FileIoPriorityHintInformation = 43,
            FileSfioReserveInformation = 44,
            FileSfioVolumeInformation = 45,
            FileHardLinkInformation = 46,
            FileProcessIdsUsingFileInformation = 47,
            FileNormalizedNameInformation = 48,
            FileNetworkPhysicalNameInformation = 49,
            FileIdGlobalTxDirectoryInformation = 50,
            FileIsRemoteDeviceInformation = 51,
            FileUnusedInformation = 52,
            FileNumaNodeInformation = 53,
            FileStandardLinkInformation = 54,
            FileRemoteProtocolInformation = 55,
            FileRenameInformationBypassAccessCheck = 56,
            FileLinkInformationBypassAccessCheck = 57,
            FileVolumeNameInformation = 58,
            FileIdInformation = 59,
            FileIdExtdDirectoryInformation = 60,
            FileReplaceCompletionInformation = 61,
            FileHardLinkFullIdInformation = 62,
            FileIdExtdBothDirectoryInformation = 63,
            FileDispositionInformationEx = 64,
            FileRenameInformationEx = 65,
            FileRenameInformationExBypassAccessCheck = 66,
            FileDesiredStorageClassInformation = 67,
            FileStatInformation = 68
        }


        public struct FILE_STANDARD_INFORMATION
        {
            public LARGE_INTEGER AllocationSize;
            public LARGE_INTEGER EndOfFile;
            public ulong NumberOfLinks;
            public bool DeletePending;
            public bool Directory;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 4)]
        public struct LARGE_INTEGER
        {
            [FieldOffset(0)]
            public int Low;
            [FieldOffset(4)]
            public int High;
            [FieldOffset(0)]
            public long QuadPart;

            public LARGE_INTEGER(int _low, int _high)
            {
                QuadPart = 0L;
                Low = _low;
                High = _high;
            }

            public LARGE_INTEGER(long _quad)
            {
                Low = 0;
                High = 0;
                QuadPart = _quad;
            }

            public long ToInt64()
            {
                return ((long)High << 32) | (uint)Low;
            }

            public static LARGE_INTEGER FromInt64(long value)
            {
                return new LARGE_INTEGER
                {
                    Low = (int)(value),
                    High = (int)((value >> 32))
                };
            }
        }

        [Flags]
        public enum FILE_SHARE_ACCESS : int
        {
            FILE_SHARE_READ = 1,
            FILE_SHARE_WRITE = 2,
            FILE_SHARE_DELETE = 4
        }


        [Flags]
        public enum FILE_OPEN_ACCESS : uint
        {
            FILE_SUPERSEDE = 0,
            FILE_DIRECTORY_FILE = 1,
            FILE_WRITE_THROUGH = 2,
            FILE_SEQUENTIAL_ONLY = 4,
            FILE_NO_INTERMEDIATE_BUFFERING = 8,
            FILE_SYNCHRONOUS_IO_ALERT = 16,
            FILE_SYNCHRONOUS_IO_NONALERT = 32,
            FILE_NON_DIRECTORY_FILE = 64,
            FILE_CREATE_TREE_CONNECTION = 128,
            FILE_COMPLETE_IF_OPLOCKED = 256,
            FILE_NO_EA_KNOWLEDGE = 512,
            FILE_OPEN_REMOTE_INSTANCE = 1024,
            FILE_RANDOM_ACCESS = 2048,
            FILE_DELETE_ON_CLOSE = 4096,
            FILE_OPEN_BY_FILE_ID = 8192,
            FILE_OPEN_FOR_BACKUP_INTENT = 16384,
            FILE_NO_COMPRESSION = 32768,
            FILE_OPEN_REQUIRING_OPLOCK = 65536,
            FILE_DISALLOW_EXCLUSIVE = 131072,
            FILE_SESSION_AWARE = 262144,
            FILE_RESERVE_OPFILTER = 1048576,
            FILE_OPEN_REPARSE_POINT = 2097152,
            FILE_OPEN_NO_RECALL = 4194304,
            FILE_OPEN_FOR_FREE_SPACE_QUERY = 8388608,
            FILE_CONTAINS_EXTENDED_CREATE_INFORMATION = 268435456
        }


        public struct IO_STATUS_BLOCK
        {
            public AnonymousUnion Union;
            public nuint Information;

            [StructLayout(LayoutKind.Explicit)]
            public struct AnonymousUnion
            {
                [FieldOffset(0)]
                public Win32.NTSTATUS Status;

                [FieldOffset(0)]
                public IntPtr Pointer;
            }
        }


        [Flags]
        public enum PROCESS_ACCESS_RIGHTS : uint
        {
            PROCESS_TERMINATE = 0x00000001,
            PROCESS_CREATE_THREAD = 0x00000002,
            PROCESS_SET_SESSIONID = 0x00000004,
            PROCESS_VM_OPERATION = 0x00000008,
            PROCESS_VM_READ = 0x00000010,
            PROCESS_VM_WRITE = 0x00000020,
            PROCESS_DUP_HANDLE = 0x00000040,
            PROCESS_CREATE_PROCESS = 0x00000080,
            PROCESS_SET_QUOTA = 0x00000100,
            PROCESS_SET_INFORMATION = 0x00000200,
            PROCESS_QUERY_INFORMATION = 0x00000400,
            PROCESS_SUSPEND_RESUME = 0x00000800,
            PROCESS_QUERY_LIMITED_INFORMATION = 0x00001000,
            PROCESS_SET_LIMITED_INFORMATION = 0x00002000,
            PROCESS_ALL_ACCESS = 0x001FFFFF,
            PROCESS_DELETE = 0x00010000,
            PROCESS_READ_CONTROL = 0x00020000,
            PROCESS_WRITE_DAC = 0x00040000,
            PROCESS_WRITE_OWNER = 0x00080000,
            PROCESS_SYNCHRONIZE = 0x00100000,
            PROCESS_STANDARD_RIGHTS_REQUIRED = 0x000F0000,
        }


        public struct OBJECT_ATTRIBUTES
        {
            public Int32 Length;
            public IntPtr RootDirectory;
            public IntPtr ObjectName;
            public UInt32 Attributes;
            public IntPtr SecurityDescriptor;
            public IntPtr SecurityQualityOfService;
        }


        public struct UNICODE_STRING
        {
            public ushort Length;
            public ushort MaximumLength;
            public IntPtr Buffer;
        }


        public struct CLIENT_ID
        {
            public IntPtr UniqueProcess;
            public IntPtr UniqueThread;
        }
    }
}