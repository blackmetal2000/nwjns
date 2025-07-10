using System;

namespace newjeans
{
    class Apc
    {
        public static void QueueApc(IntPtr hThread, IntPtr BaseAddress)
        {
            var status = Win32.NtQueueApcThreadEx2(
                hThread,
                IntPtr.Zero,
                Win32.QUEUE_USER_APC_FLAGS.QUEUE_USER_APC_FLAGS_SPECIAL_USER_APC,
                BaseAddress,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero
            );

            Console.WriteLine(status);
        }
    }
}
