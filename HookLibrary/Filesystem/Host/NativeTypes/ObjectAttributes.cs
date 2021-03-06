using System;
using System.Runtime.InteropServices;

namespace HookLibrary.Filesystem.Host.NativeTypes
{
    /// <summary>
    /// Enumeration for the <see cref="ObjectAttributes.Attributes"/> member of the <see cref="ObjectAttributes"/> class.
    /// </summary>
    /// <remarks>
    /// All documentations here are copied from https://msdn.microsoft.com/en-us/library/windows/hardware/ff564586(v=vs.85).aspx
    /// </remarks>
    [Flags]
    public enum ObjectHandleAttributes : uint
    {
        /// <summary>
        /// This handle can be inherited by child processes of the current process.
        /// </summary>
        Inherit = 0x00000002,

        /// <summary>
        /// This flag only applies to objects that are named within the object manager.
        /// By default, such objects are deleted when all open handles to them are closed.
        /// If this flag is specified, the object is not deleted when all open handles are closed.
        /// </summary>
        Permanent = 0x00000010,

        /// <summary>
        /// If this flag is set and the <see cref="ObjectAttributes"/> structure is passed to a routine that creates an object,
        /// the object can be accessed exclusively. That is, once a process opens such a handle to the object,
        /// no other processes can open handles to this object.
        /// 
        /// If this flag is set and the <see cref="ObjectAttributes"/> structure is passed to a routine that creates an object
        /// handle, the caller is requesting exclusive access to the object for the process context that the handle was created
        /// in. This request can be granted only if the <see cref="Exculsive"/> flag was set when the object was created.
        /// </summary>
        Exculsive = 0x00000020,

        /// <summary>
        /// If this flag is specified, a case-insensitive comparison is used when matching the name pointed to 
        /// by the <see cref="ObjectAttributes.ObjectName"/> member against the names of existing objects. Otherwise,
        /// object names are compared using the default system settings.
        /// </summary>
        CaseInsensitive = 0x00000040,

        /// <summary>
        /// If this flag is specified, by using the object handle, to a routine that creates objects and if 
        /// that object already exists, the routine should open that object. Otherwise, the routine creating 
        /// the object returns an <see cref="NtStatus"/> code of <see cref="NtStatus.ObjectNameCollision"/>.
        /// </summary>
        OpenIf = 0x00000080,

        /// <summary>
        /// If an object handle, with this flag set, is passed to a routine that opens objects and 
        /// if the object is a symbolic link object, the routine should open the symbolic link object itself,
        /// rather than the object that the symbolic link refers to (which is the default behavior).
        /// </summary>
        OpenLink = 0x00000100,

        /// <summary>
        /// The handle is created in system process context and can only be accessed from kernel mode.
        /// </summary>
        KernelHandle = 0x00000200,

        /// <summary>
        /// The routine that opens the handle should enforce all access checks for the object,
        /// even if the handle is being opened in kernel mode.
        /// </summary>
        /// <remarks>
        /// Copied from http://processhacker.sourceforge.net/doc/ntbasic_8h.html.
        /// </remarks>
        ForceAccessCheck = 0x00000400,

        /// <summary>
        /// Reserved. Used for displaying available enumeration values.
        /// </summary>
        /// <remarks>
        /// Source: https://msdn.microsoft.com/en-us/library/windows/hardware/ff564586(v=vs.85).aspx
        /// </remarks>
        ValidAttributes = KernelHandle | OpenLink | OpenIf | CaseInsensitive | Exculsive | Permanent | Inherit,
    }

    /// <summary>
    /// Specifies attributes that can be applied to objects or object handles by routines 
    /// that create objects and/or return handles to objects.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct ObjectAttributes : IDisposable
    {
        /// <summary>
        /// Number of bytes of data contained in the attributes.
        /// </summary>
        public int Length;

        /// <summary>
        /// Optional handle to the root object directory for the path name specified by the <see cref="ObjectName"/> member.
        /// If RootDirectoryHandle is NULL, <see cref="ObjectName"/> must contain the full path of the object.
        /// If RootDirectoryHandle is non-NULL, <see cref="ObjectName"/> must contain the relative path of the object.
        /// The RootDirectoryHandle handle can refer to a file system directory or an object directory in the object manager namespace.
        /// </summary>
        public IntPtr RootDirectoryHandle;

        /// <summary>
        /// Pointer to the object name, which is a <see cref="UnicodeString"/> object.
        /// </summary>
        private IntPtr _objectName;

        /// <summary>
        /// TODO
        /// </summary>
        public ObjectHandleAttributes Attributes;

        /// <summary>
        /// Pointer to <see cref="SecurityDescriptor"/> struct.
        /// </summary>
        private IntPtr _ptrSecurityDescriptor;

        /// <summary>
        /// Pointer to <see cref="SecurityQos"/> struct.
        /// </summary>
        private IntPtr _securityQos;

        /// <summary>
        /// Name of the object, referred by <see cref="_objectName"/>.
        /// </summary>
        public UnicodeString ObjectName
        {
            get { return (UnicodeString) Marshal.PtrToStructure(_objectName, typeof(UnicodeString)); }
            set
            {
                if (_objectName == IntPtr.Zero)
                {
                    _objectName = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                }
                else
                {
                    Marshal.DestroyStructure(_objectName, typeof(UnicodeString));
                    Marshal.ReAllocHGlobal(_objectName, (IntPtr)Marshal.SizeOf(value));
                }

                // no need to pass true on the third parameter, since DestroyStructure is called before.
                Marshal.StructureToPtr(value, _objectName, false);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public SecurityQos SecurityQos
        {
            get { return (SecurityQos) Marshal.PtrToStructure(_securityQos, typeof(SecurityQos)); }
            set
            {
                if (_securityQos == IntPtr.Zero)
                {
                    _securityQos = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                }
                else
                {
                    Marshal.DestroyStructure(_securityQos, typeof(SecurityQos));
                    Marshal.ReAllocHGlobal(_securityQos, (IntPtr)Marshal.SizeOf(value));
                }

                // no need to pass true on the third parameter, since DestroyStructure is called before.
                Marshal.StructureToPtr(value, _securityQos, false);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        public SecurityDescriptor SecurityDescriptor
        {
            get { return (SecurityDescriptor) Marshal.PtrToStructure(_securityQos, typeof(SecurityDescriptor)); }
            set
            {
                if (_ptrSecurityDescriptor == IntPtr.Zero)
                {
                    _ptrSecurityDescriptor = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                }
                else
                {
                    Marshal.DestroyStructure(_ptrSecurityDescriptor, typeof(SecurityDescriptor));
                    Marshal.ReAllocHGlobal(_ptrSecurityDescriptor, (IntPtr)Marshal.SizeOf(value));
                }

                // no need to pass true on the third parameter, since DestroyStructure is called before.
                Marshal.StructureToPtr(value, _ptrSecurityDescriptor, false);
            }
        }

        public string GetRootPath()
        {
            var path = "";

            if (RootDirectoryHandle != IntPtr.Zero)
            {
                Utils.GetNtPathFromHandle(RootDirectoryHandle, out path);
            }

            return path;
        }

        public string GetNtPath()
        {
            var rootPath = GetRootPath();

            if (rootPath.Equals(""))
            {
                // TODO: convert properly
                return ObjectName;
            }

            if (ObjectName.IsNtPath())
            {
                return rootPath + ObjectName;
            }

            return rootPath + '\\' + ObjectName;
        }

        public string GetDosPath()
        {
            var rootPath = GetRootPath();
            if (rootPath.Equals("") && ObjectName.IsDosPath())
            {
                return ObjectName.WithoutPrefix();
            }

            var path = GetNtPath();
            Utils.GetDosPathFromNtPath(path, out path);

            return path;
        }

        public void Dispose()
        {
            if (_objectName != IntPtr.Zero)
            {
                Marshal.DestroyStructure(_objectName, typeof(UnicodeString));
                Marshal.FreeHGlobal(_objectName);
                _objectName = IntPtr.Zero;
            }

            if (_securityQos != IntPtr.Zero)
            {
                Marshal.DestroyStructure(_securityQos, typeof(SecurityQos));
                Marshal.FreeHGlobal(_securityQos);
                _securityQos = IntPtr.Zero;
            }

            if (_ptrSecurityDescriptor != IntPtr.Zero)
            {
                Marshal.DestroyStructure(_ptrSecurityDescriptor, typeof(SecurityDescriptor));
                Marshal.FreeHGlobal(_ptrSecurityDescriptor);
                _ptrSecurityDescriptor = IntPtr.Zero;
            }
        }
    }
}