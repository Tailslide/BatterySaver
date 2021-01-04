#region Copyright © 2010, Ryan Emerle; All rights reserved

// Copyright © 2010, Ryan Emerle
// All rights reserved.
// http://www.emerle.net/
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions
// are met:
// 
// - Redistributions of source code must retain the above copyright
// notice, this list of conditions and the following disclaimer.
// 
// - Neither the name of the Ryan Emerle, nor the names of any
// contributors may be used to endorse or promote products
// derived from this software without specific prior written 
// permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
// FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE 
// COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES INCLUDING,
// BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER 
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
// LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
// POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;
using System.Security;

using BatterySaver.Lib.Service;

namespace BatterySaver.Lib.Utility
{
    internal static class Win32
    {
        private const string Setupapi = "setupapi.dll";

        [DllImport( "kernel32" )]
        static extern bool SetProcessWorkingSetSize( IntPtr handle, int minSize, int maxSize );

        [DllImport( "kernel32" )]
        public static extern bool GetSystemPowerStatus( SystemPowerStatus systemPowerStatus );

        [DllImport( Setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static extern bool SetupDiCallClassInstaller( DiFunction installFunction,
                                                             SafeDeviceInfoSetHandle deviceInfoSet,
                                                             [In] ref DeviceInfoData deviceInfoData );

        [DllImport( Setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static extern bool SetupDiEnumDeviceInfo( SafeDeviceInfoSetHandle deviceInfoSet, int memberIndex,
                                                         ref DeviceInfoData deviceInfoData );

        [DllImport( Setupapi, CallingConvention = CallingConvention.Winapi, CharSet = CharSet.Unicode,
            SetLastError = true )]
        public static extern SafeDeviceInfoSetHandle SetupDiGetClassDevs( [In] ref Guid classGuid,
                                                                          [MarshalAs( UnmanagedType.LPWStr )] string
                                                                              enumerator, IntPtr hwndParent,
                                                                          SetupDiGetClassDevsFlags flags );

        [DllImport( Setupapi, SetLastError = true, CharSet = CharSet.Auto )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static extern bool SetupDiGetDeviceInstanceId( IntPtr deviceInfoSet, ref DeviceInfoData did,
                                                              [MarshalAs( UnmanagedType.LPTStr )] StringBuilder
                                                                  deviceInstanceId, int deviceInstanceIdSize,
                                                              out int requiredSize );

        [SuppressUnmanagedCodeSecurity, ReliabilityContract( Consistency.WillNotCorruptState, Cer.Success )]
        [DllImport( Setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static extern bool SetupDiDestroyDeviceInfoList( IntPtr deviceInfoSet );

        [DllImport( Setupapi, CallingConvention = CallingConvention.Winapi, SetLastError = true )]
        [return: MarshalAs( UnmanagedType.Bool )]
        public static extern bool SetupDiSetClassInstallParams( SafeDeviceInfoSetHandle deviceInfoSet,
                                                                [In] ref DeviceInfoData deviceInfoData,
                                                                [In] ref PropertyChangeParameters classInstallParams,
                                                                int classInstallParamsSize );

        public static void TrimWorkingSet( Process process )
        {
           SetProcessWorkingSetSize( process.Handle, -1, -1 );
        }
    }

    [Flags]
    internal enum SetupDiGetClassDevsFlags
    {
        Default = 1,
        Present = 2,
        AllClasses = 4,
        Profile = 8,
        DeviceInterface = 16
    }

    internal enum DiFunction
    {
        SelectDevice = 1,
        InstallDevice = 2,
        AssignResources = 3,
        Properties = 4,
        Remove = 5,
        FirstTimeSetup = 6,
        FoundDevice = 7,
        SelectClassDrivers = 8,
        ValidateClassDrivers = 9,
        InstallClassDrivers = 10,
        CalcDiskSpace = 11,
        DestroyPrivateData = 12,
        ValidateDriver = 13,
        Detect = 15,
        InstallWizard = 16,
        DestroyWizardData = 17,
        PropertyChange = 18,
        EnableClass = 19,
        DetectVerify = 20,
        InstallDeviceFiles = 21,
        UnRemove = 22,
        SelectBestCompatDrv = 23,
        AllowInstall = 24,
        RegisterDevice = 25,
        NewDeviceWizardPreSelect = 26,
        NewDeviceWizardSelect = 27,
        NewDeviceWizardPreAnalyze = 28,
        NewDeviceWizardPostAnalyze = 29,
        NewDeviceWizardFinishInstall = 30,
        Unused1 = 31,
        InstallInterfaces = 32,
        DetectCancel = 33,
        RegisterCoInstallers = 34,
        AddPropertyPageAdvanced = 35,
        AddPropertyPageBasic = 36,
        Reserved1 = 37,
        Troubleshooter = 38,
        PowerMessageWake = 39,
        AddRemotePropertyPageAdvanced = 40,
        UpdateDriverUi = 41,
        Reserved2 = 48
    }

    internal enum StateChangeAction
    {
        Enable = 1,
        Disable = 2,
        PropChange = 3,
        Start = 4,
        Stop = 5
    }

    [Flags]
    internal enum Scopes
    {
        Global = 1,
        ConfigSpecific = 2,
        ConfigGeneral = 4
    }

    internal enum SetupApiError
    {
        NoAssociatedClass = unchecked( (int)0xe0000200 ),
        ClassMismatch = unchecked( (int)0xe0000201 ),
        DuplicateFound = unchecked( (int)0xe0000202 ),
        NoDriverSelected = unchecked( (int)0xe0000203 ),
        KeyDoesNotExist = unchecked( (int)0xe0000204 ),
        InvalidDevinstName = unchecked( (int)0xe0000205 ),
        InvalidClass = unchecked( (int)0xe0000206 ),
        DevinstAlreadyExists = unchecked( (int)0xe0000207 ),
        DevinfoNotRegistered = unchecked( (int)0xe0000208 ),
        InvalidRegProperty = unchecked( (int)0xe0000209 ),
        NoInf = unchecked( (int)0xe000020a ),
        NoSuchHDevinst = unchecked( (int)0xe000020b ),
        CantLoadClassIcon = unchecked( (int)0xe000020c ),
        InvalidClassInstaller = unchecked( (int)0xe000020d ),
        DiDoDefault = unchecked( (int)0xe000020e ),
        DiNoFileCopy = unchecked( (int)0xe000020f ),
        InvalidHwProfile = unchecked( (int)0xe0000210 ),
        NoDeviceSelected = unchecked( (int)0xe0000211 ),
        DevinfolistLocked = unchecked( (int)0xe0000212 ),
        DevinfodataLocked = unchecked( (int)0xe0000213 ),
        DiBadPath = unchecked( (int)0xe0000214 ),
        NoClassInstallParams = unchecked( (int)0xe0000215 ),
        FileQueueLocked = unchecked( (int)0xe0000216 ),
        BadServiceInstallSect = unchecked( (int)0xe0000217 ),
        NoClassDriverList = unchecked( (int)0xe0000218 ),
        NoAssociatedService = unchecked( (int)0xe0000219 ),
        NoDefaultDeviceInterface = unchecked( (int)0xe000021a ),
        DeviceInterfaceActive = unchecked( (int)0xe000021b ),
        DeviceInterfaceRemoved = unchecked( (int)0xe000021c ),
        BadInterfaceInstallSect = unchecked( (int)0xe000021d ),
        NoSuchInterfaceClass = unchecked( (int)0xe000021e ),
        InvalidReferenceString = unchecked( (int)0xe000021f ),
        InvalidMachineName = unchecked( (int)0xe0000220 ),
        RemoteCommFailure = unchecked( (int)0xe0000221 ),
        MachineUnavailable = unchecked( (int)0xe0000222 ),
        NoConfigMgrServices = unchecked( (int)0xe0000223 ),
        InvalidPropPageProvider = unchecked( (int)0xe0000224 ),
        NoSuchDeviceInterface = unchecked( (int)0xe0000225 ),
        DiPostProcessingRequired = unchecked( (int)0xe0000226 ),
        InvalidCoInstaller = unchecked( (int)0xe0000227 ),
        NoCompatDrivers = unchecked( (int)0xe0000228 ),
        NoDeviceIcon = unchecked( (int)0xe0000229 ),
        InvalidInfLogConfig = unchecked( (int)0xe000022a ),
        DiDontInstall = unchecked( (int)0xe000022b ),
        InvalidFilterDriver = unchecked( (int)0xe000022c ),
        NonWindowsNtDriver = unchecked( (int)0xe000022d ),
        NonWindowsDriver = unchecked( (int)0xe000022e ),
        NoCatalogForOemInf = unchecked( (int)0xe000022f ),
        DevInstallQueueNonNative = unchecked( (int)0xe0000230 ),
        NotDisableable = unchecked( (int)0xe0000231 ),
        CantRemoveDevinst = unchecked( (int)0xe0000232 ),
        InvalidTarget = unchecked( (int)0xe0000233 ),
        DriverNonNative = unchecked( (int)0xe0000234 ),
        InWow64 = unchecked( (int)0xe0000235 ),
        SetSystemRestorePoint = unchecked( (int)0xe0000236 ),
        IncorrectlyCopiedInf = unchecked( (int)0xe0000237 ),
        SceDisabled = unchecked( (int)0xe0000238 ),
        UnknownException = unchecked( (int)0xe0000239 ),
        PnpRegistryError = unchecked( (int)0xe000023a ),
        RemoteRequestUnsupported = unchecked( (int)0xe000023b ),
        NotAnInstalledOemInf = unchecked( (int)0xe000023c ),
        InfInUseByDevices = unchecked( (int)0xe000023d ),
        DiFunctionObsolete = unchecked( (int)0xe000023e ),
        NoAuthenticodeCatalog = unchecked( (int)0xe000023f ),
        AuthenticodeDisallowed = unchecked( (int)0xe0000240 ),
        AuthenticodeTrustedPublisher = unchecked( (int)0xe0000241 ),
        AuthenticodeTrustNotEstablished = unchecked( (int)0xe0000242 ),
        AuthenticodePublisherNotTrusted = unchecked( (int)0xe0000243 ),
        SignatureOsAttributeMismatch = unchecked( (int)0xe0000244 ),
        OnlyValidateViaAuthenticode = unchecked( (int)0xe0000245 )
    }

    [StructLayout( LayoutKind.Sequential )]
    internal struct DeviceInfoData
    {
        public int Size;
        public Guid ClassGuid;
        public int DevInst;
        public IntPtr Reserved;
    }

    [StructLayout( LayoutKind.Sequential )]
    internal struct PropertyChangeParameters
    {
        public int Size; // part of header. It's flattened out into 1 structure.
        public DiFunction DiFunction;
        public StateChangeAction StateChange;
        public Scopes Scope;
        public int HwProfile;
    }

}