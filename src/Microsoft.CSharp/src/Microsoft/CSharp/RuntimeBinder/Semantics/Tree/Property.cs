// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.CSharp.RuntimeBinder.Semantics
{
    internal sealed class ExprProperty : ExprWithType, IExprWithArgs
    {
        // If we have this.prop = 123, but the implementation of the property is in the
        // base class, then the object is of the base class type. Note that to get
        // the object, we must go through the MEMGRP.
        //
        // "throughObject" is
        // of the type we are actually calling through.  (We need to know the
        // "through" type to ensure that protected semantics are correctly enforced.)

        public ExprProperty(CType type, Expr pOptionalObjectThrough, Expr pOptionalArguments, ExprMemberGroup pMemberGroup, PropWithType pwtSlot, MethWithType mwtSet)
            : base(ExpressionKind.Property, type)
        {
            OptionalObjectThrough = pOptionalObjectThrough;
            OptionalArguments = pOptionalArguments;
            MemberGroup = pMemberGroup;

            if (pwtSlot != null)
            {
                PropWithTypeSlot = pwtSlot;
            }

            if (mwtSet != null)
            {
                MethWithTypeSet = mwtSet;
            }
        }

        public Expr OptionalArguments { get; set; }

        public ExprMemberGroup MemberGroup { get; set; }

        public Expr OptionalObject
        {
            get => MemberGroup.OptionalObject;
            set => MemberGroup.OptionalObject = value;
        }

        public Expr OptionalObjectThrough { get; }

        public PropWithType PropWithTypeSlot { get; }

        public MethWithType MethWithTypeSet { get; }

        public bool IsBaseCall => 0 != (Flags & EXPRFLAG.EXF_BASECALL);

        SymWithType IExprWithArgs.GetSymWithType() => PropWithTypeSlot;
    }
}
