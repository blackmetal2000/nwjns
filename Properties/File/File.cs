using System;
using System.Runtime.InteropServices;

namespace newjeans
{
    class File
    {

        private static void FreeLocalMemory(IntPtr BaseAddress)
        {
            int pSize = 0;

            var status = Win32.NtFreeVirtualMemory(
                new IntPtr(-1),
                ref BaseAddress,
                ref pSize,
                Win32.VIRTUAL_FREE_TYPE.MEM_RELEASE
            );

            if (status != Win32.NTSTATUS.Success || BaseAddress == IntPtr.Zero)
            {
                Console.WriteLine($"Failed to free virtual memory: {BaseAddress}! Status: {status}. Proceeding.");
            }
        }

        private static IntPtr AllocateLocalMemory(int regionSize)
        {
            IntPtr baseAddress = IntPtr.Zero;
            IntPtr size = new IntPtr(regionSize);

            var status = Win32.ZwAllocateVirtualMemory(
                new IntPtr(-1),
                ref baseAddress,
                0,
                ref size,
                Win32.VIRTUAL_ALLOCATION_TYPE.MEM_RESERVE | Win32.VIRTUAL_ALLOCATION_TYPE.MEM_COMMIT,
                Win32.PAGE_PROTECTION_FLAGS.PAGE_READWRITE
            );

            if (status != Win32.NTSTATUS.Success || baseAddress == IntPtr.Zero)
            {
                throw new Exception($"ZwAllocateVirtualMemory ERROR! Status: {status}");
            }

            return baseAddress;
        }

        private static IntPtr OpenFile(string args)
        {
            string fp = @"\??\" + args;

            Win32.RtlInitUnicodeString(
                out Win32.UNICODE_STRING path, fp
            );

            IntPtr unicodeAlloc = AllocateLocalMemory(Marshal.SizeOf(path));
            Marshal.StructureToPtr(path, unicodeAlloc, false);

            var oa = new Win32.OBJECT_ATTRIBUTES
            {
                Length = Marshal.SizeOf(typeof(Win32.OBJECT_ATTRIBUTES)),
                ObjectName = unicodeAlloc,
                Attributes = 0x40
            };

            var status = Win32.NtOpenFile(
                out IntPtr hFile,
                0x13019f, // write, read and delete
                ref oa,
                out Win32.IO_STATUS_BLOCK isb,
                Win32.FILE_SHARE_ACCESS.FILE_SHARE_READ,
                Win32.FILE_OPEN_ACCESS.FILE_SYNCHRONOUS_IO_NONALERT | Win32.FILE_OPEN_ACCESS.FILE_SUPERSEDE
            );

            if (status != Win32.NTSTATUS.Success || hFile == IntPtr.Zero)
            {
                throw new Exception($"NtOpenFile ERROR! Status: {status}");
            }

            FreeLocalMemory(unicodeAlloc);
            return hFile;
        }

        private static int GetFileSize(IntPtr hFile)
        {

            int Length = Marshal.SizeOf(typeof(Win32.FILE_STANDARD_INFORMATION));
            IntPtr FileInformation = AllocateLocalMemory(Length);

            var status = Win32.NtQueryInformationFile(
                hFile,
                out Win32.IO_STATUS_BLOCK isb,
                FileInformation,
                Length,
                Win32.FILE_INFORMATION_CLASS.FileStandardInformation
            );

            if (status != Win32.NTSTATUS.Success || hFile == IntPtr.Zero)
            {
                throw new Exception($"NtQueryInformationFile ERROR! Status: {status}");
            }

            var FileStandardInformation = Marshal.PtrToStructure<Win32.FILE_STANDARD_INFORMATION>(FileInformation);
            FreeLocalMemory(FileInformation);

            return FileStandardInformation.EndOfFile.Low;
        }

        public static (IntPtr, int) ReadFile(string path)
        {
            IntPtr hFile = OpenFile(path); int fileSize = GetFileSize(hFile);

            IntPtr content = AllocateLocalMemory(fileSize);

            var status = Win32.NtReadFile(
                hFile,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero,
                out Win32.IO_STATUS_BLOCK isb,
                content,
                fileSize,
                0,
                0
            );

            if (status != Win32.NTSTATUS.Success || hFile == IntPtr.Zero)
            {
                throw new Exception($"NtReadFile ERROR! Status: {status}");
            }

            return (content, fileSize);
        }
    }
}