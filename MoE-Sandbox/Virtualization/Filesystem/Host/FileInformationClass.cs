﻿namespace MoE_Sandbox.Virtualization.Filesystem.Host
{
    /// <summary>
    /// A value that specifies which structure to use to query or set information for a file object.
    /// </summary>
    public enum FileInformationClass
    {
        FileDirectoryInformation = 1,     // 1
        FileFullDirectoryInformation,     // 2
        FileBothDirectoryInformation,     // 3
        FileBasicInformation,         // 4
        FileStandardInformation,      // 5
        FileInternalInformation,      // 6
        FileEaInformation,        // 7
        FileAccessInformation,        // 8
        FileNameInformation,          // 9
        FileRenameInformation,        // 10
        FileLinkInformation,          // 11
        FileNamesInformation,         // 12
        FileDispositionInformation,       // 13
        FilePositionInformation,      // 14
        FileFullEaInformation,        // 15
        FileModeInformation = 16,     // 16
        FileAlignmentInformation,     // 17
        FileAllInformation,           // 18
        FileAllocationInformation,    // 19
        FileEndOfFileInformation,     // 20
        FileAlternateNameInformation,     // 21
        FileStreamInformation,        // 22
        FilePipeInformation,          // 23
        FilePipeLocalInformation,     // 24
        FilePipeRemoteInformation,    // 25
        FileMailslotQueryInformation,     // 26
        FileMailslotSetInformation,       // 27
        FileCompressionInformation,       // 28
        FileObjectIdInformation,      // 29
        FileCompletionInformation,    // 30
        FileMoveClusterInformation,       // 31
        FileQuotaInformation,         // 32
        FileReparsePointInformation,      // 33
        FileNetworkOpenInformation,       // 34
        FileAttributeTagInformation,      // 35
        FileTrackingInformation,      // 36
        FileIdBothDirectoryInformation,   // 37
        FileIdFullDirectoryInformation,   // 38
        FileValidDataLengthInformation,   // 39
        FileShortNameInformation,     // 40
        FileHardLinkInformation = 46    // 46    
    }
}
