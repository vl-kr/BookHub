﻿namespace BusinessLayer.Helpers.DbUpdateExceptionHandler;

public static partial class DbUpdateExceptionHandler
{
    // Used ChatGPT to transform from data at https://www.sqlite.org/rescode.html, might contain mistakes
    public enum SQLiteExtendedErrorCode
    {
        AbortRollback = 516,
        AuthUser = 279,
        BusyRecovery = 261,
        BusySnapshot = 517,
        BusyTimeout = 773,
        CantOpenConvPath = 1038,
        CantOpenDirtyWal = 1294,
        CantOpenFullPath = 782,
        CantOpenIsDir = 526,
        CantOpenNoTempDir = 270,
        CantOpenSymlink = 1550,
        ConstraintCheck = 275,
        ConstraintCommitHook = 531,
        ConstraintDataType = 3091,
        ConstraintForeignKey = 787,
        ConstraintFunction = 1043,
        ConstraintNotNull = 1299,
        ConstraintPinned = 2835,
        ConstraintPrimaryKey = 1555,
        ConstraintRowId = 2579,
        ConstraintTrigger = 1811,
        ConstraintUnique = 2067,
        ConstraintVTab = 2323,
        CorruptIndex = 779,
        CorruptSequence = 523,
        CorruptVTab = 267,
        ErrorMissingCollSeq = 257,
        ErrorRetry = 513,
        ErrorSnapshot = 769,
        IOErrAccess = 3338,
        IOErrAuth = 7178,
        IOErrBeginAtomic = 7434,
        IOErrBlocked = 2826,
        IOErrCheckReservedLock = 3594,
        IOErrClose = 4106,
        IOErrCommitAtomic = 7690,
        IOErrConvPath = 6666,
        IOErrCorruptFS = 8458,
        IOErrData = 8202,
        IOErrDelete = 2570,
        IOErrDeleteNoEnt = 5898,
        IOErrDirClose = 4362,
        IOErrDirFsync = 1290,
        IOErrFstat = 1802,
        IOErrFsync = 1034,
        IOErrGetTempPath = 6410,
        IOErrLock = 3850,
        IOErrMmap = 6154,
        IOErrNoMem = 3082,
        IOErrRdLock = 2314,
        IOErrRead = 266,
        IOErrRollbackAtomic = 7946,
        IOErrSeek = 5642,
        IOErrShmLock = 5130,
        IOErrShmMap = 5386,
        IOErrShmOpen = 4618,
        IOErrShmSize = 4874,
        IOErrShortRead = 522,
        IOErrTruncate = 1546,
        IOErrUnlock = 2058,
        IOErrVNode = 6922,
        IOErrWrite = 778,
        LockedSharedCache = 262,
        LockedVTab = 518,
        NoticeRecoverRollback = 539,
        NoticeRecoverWal = 283,
        OkLoadPermanently = 256,
        ReadonlyCantInit = 1288,
        ReadonlyCantLock = 520,
        ReadonlyDbMoved = 1032,
        ReadonlyDirectory = 1544,
        ReadonlyRecovery = 264,
        ReadonlyRollback = 776,
        WarningAutoIndex = 284
    }
}