// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Apple;

internal static partial class Interop
{
    internal static partial class AppleCrypto
    {
        [DllImport(Libraries.AppleCryptoNative, EntryPoint = "AppleCryptoNative_HmacFree")]
        internal static extern void HmacFree(IntPtr handle);

        [DllImport(Libraries.AppleCryptoNative, EntryPoint = "AppleCryptoNative_HmacCreate")]
        internal static extern SafeHmacHandle HmacCreate(PAL_HashAlgorithm algorithm, ref int cbDigest);

        [DllImport(Libraries.AppleCryptoNative, EntryPoint = "AppleCryptoNative_HmacInit")]
        internal static extern unsafe int HmacInit(SafeHmacHandle ctx, byte* pbKey, int cbKey);

        [DllImport(Libraries.AppleCryptoNative, EntryPoint = "AppleCryptoNative_HmacUpdate")]
        internal static extern unsafe int HmacUpdate(SafeHmacHandle ctx, byte* pbData, int cbData);

        [DllImport(Libraries.AppleCryptoNative, EntryPoint = "AppleCryptoNative_HmacFinal")]
        internal static extern unsafe int HmacFinal(SafeHmacHandle ctx, byte* pbOutput, int cbOutput);
    }
}

namespace System.Security.Cryptography.Apple
{
    internal sealed class SafeHmacHandle : SafeHandle
    {
        internal SafeHmacHandle()
            : base(IntPtr.Zero, ownsHandle: true)
        {
        }

        protected override bool ReleaseHandle()
        {
            Interop.AppleCrypto.HmacFree(handle);
            SetHandle(IntPtr.Zero);
            return true;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}
