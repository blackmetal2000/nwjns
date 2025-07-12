# nwjns

<br>
<br>

<p align="center">
  <img src="https://i.pinimg.com/1200x/2f/f6/67/2ff66716b07c1411d16b987783687044.jpg" alt="drawing" width="300"/> 
</p>

#### What is it?

This POC demonstrates the new undocumented API `NtQueueApcThreadEx2`, used for APC code injection. By default, the thread must be in an alertable state. However, with `QUEUE_USER_APC_FLAGS_SPECIAL_USER_APC`, this requirement can be bypassed. Futhermore, this project only use NT APIS.

#### NtQueueApcThreadEx2

This is a new system call, that allows to pass both `UserApcFlags` and `MemoryReserveHandle`. Advantages:

1. the APC ROUTINE accepts 3 arguments!
2. the thread doesn't need to be in alertable state! (new)


#### Signature

```c
NTSTATUS
NtQueueApcThreadEx2(
    IN HANDLE ThreadHandle,
    IN HANDLE UserApcReserveHandle,
    IN QUEUE_USER_APC_FLAGS QueueUserApcFlags,
    IN PPS_APC_ROUTINE ApcRoutine,
    IN PVOID SystemArgument1 OPTIONAL,
    IN PVOID SystemArgument2 OPTIONAL,
    IN PVOID SystemArgument3 OPTIONAL
    );
```

#### new jeans never die!
