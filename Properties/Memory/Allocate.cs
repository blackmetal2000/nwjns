using System;

namespace newjeans
{
    class Memory
    {
        public static void Write(IntPtr hProcess, IntPtr BaseAddress, IntPtr Buffer, int RegionSize)
        {
            var status = Win32.NtWriteVirtualMemory(
                hProcess,
                BaseAddress,
                Buffer,
                RegionSize,
                out int NumberOfBytesWritten
            );

            if (status != Win32.NTSTATUS.Success || NumberOfBytesWritten == 0)
            {
                throw new Exception($"NtWriteVirtualMemory ERROR! Status: {status}");
            }
        }

        public static IntPtr Allocate(IntPtr hProcess, int RegionSize)
        {
            IntPtr BaseAddress = IntPtr.Zero;

            var status = Win32.ZwAllocateVirtualMemory(
                hProcess,
                ref BaseAddress,
                0,
                ref RegionSize,
                Win32.VIRTUAL_ALLOCATION_TYPE.MEM_COMMIT | Win32.VIRTUAL_ALLOCATION_TYPE.MEM_RESERVE,
                Win32.PAGE_PROTECTION_FLAGS.PAGE_EXECUTE_READWRITE
            );

            if (status != Win32.NTSTATUS.Success || BaseAddress == IntPtr.Zero)
            {
                throw new Exception($"ZwAllocateVirtualMemory ERROR! Status: {status}");
            }

            return BaseAddress;
        }
    }
}