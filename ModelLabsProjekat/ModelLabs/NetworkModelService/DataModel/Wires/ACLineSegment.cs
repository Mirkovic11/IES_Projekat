using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class ACLineSegment : Conductor
    {
        private float b0ch;

        public float B0ch
        {
            get { return b0ch; }
            set { b0ch = value; }
        }


        private float bch;

        public float Bch
        {
            get { return bch; }
            set { bch = value; }
        }

        private float g0ch;

        public float G0ch
        {
            get { return g0ch; }
            set { g0ch = value; }
        }

        private float gch;

        public float Gch
        {
            get { return gch; }
            set { gch = value; }
        }

        private float r;

        public float R
        {
            get { return r; }
            set { r = value; }
        }

        private float r0;

        public float R0
        {
            get { return r0; }
            set { r0 = value; }
        }

        private float x;

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        private float x0;

        public float X0
        {
            get { return x0; }
            set { x0 = value; }
        }

        private long perLengthImpedance = 0;

        public long PerLengthImpedance
        {
            get { return perLengthImpedance; }
            set { perLengthImpedance = value; }
        }

        public ACLineSegment(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ACLineSegment x = (ACLineSegment)obj;
                return (x.perLengthImpedance == this.perLengthImpedance &&
                        x.b0ch == this.b0ch &&
                        x.bch == this.bch &&
                        x.g0ch == this.g0ch &&
                        x.gch == this.gch &&
                        x.r == this.r &&
                        x.r0 == this.r0 &&
                        x.x == this.x &&
                        x.x0==this.x0);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        #region IAccess implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.ACLINESEGM_B0CH:
                case ModelCode.ACLINESEGM_BCH:
                case ModelCode.ACLINESEGM_G0CH:
                case ModelCode.ACLINESEGM_GCH:
                case ModelCode.ACLINESEGM_R:
                case ModelCode.ACLINESEGM_R0:
                case ModelCode.ACLINESEGM_X:
                case ModelCode.ACLINESEGM_X0:
                case ModelCode.ACLINESEGM_PERLENIMP:

                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.ACLINESEGM_B0CH:
                    property.SetValue(b0ch);
                    break;

                case ModelCode.ACLINESEGM_BCH:
                    property.SetValue(bch);
                    break;

                case ModelCode.ACLINESEGM_G0CH:
                    property.SetValue(g0ch);
                    break;

                case ModelCode.ACLINESEGM_GCH:
                    property.SetValue(gch);
                    break;

                case ModelCode.ACLINESEGM_R:
                    property.SetValue(r);
                    break;

                case ModelCode.ACLINESEGM_R0:
                    property.SetValue(r0);
                    break;

                case ModelCode.ACLINESEGM_X:
                    property.SetValue(x);
                    break;

                case ModelCode.ACLINESEGM_X0:
                    property.SetValue(x0);
                    break;

                case ModelCode.ACLINESEGM_PERLENIMP:
                    property.SetValue(perLengthImpedance);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.ACLINESEGM_B0CH:
                    b0ch = property.AsFloat();
                    break;

                case ModelCode.ACLINESEGM_BCH:
                    bch = property.AsFloat();
                    break;

                case ModelCode.ACLINESEGM_G0CH:
                    g0ch = property.AsFloat();
                    break;

                case ModelCode.ACLINESEGM_GCH:
                    gch = property.AsFloat();
                    break;

                case ModelCode.ACLINESEGM_R:
                    r = property.AsFloat();
                    break;

                case ModelCode.ACLINESEGM_R0:
                    r0 = property.AsFloat();
                    break;

                case ModelCode.ACLINESEGM_X:
                    x = property.AsFloat();
                    break;

                case ModelCode.ACLINESEGM_X0:
                    x0 = property.AsFloat();
                    break;

                case ModelCode.ACLINESEGM_PERLENIMP:
                    perLengthImpedance = property.AsReference();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation


        #region IReference implementation	
/*
        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (perLengthImpedance != 0 && (refType != TypeOfReference.Reference || refType != TypeOfReference.Both))
            {
                references[ModelCode.ACLINESEGM_PERLENIMP] = new List<long>();
                references[ModelCode.ACLINESEGM_PERLENIMP].Add(perLengthImpedance);
            }

            base.GetReferences(references, refType);
        }
        */
        #endregion IReference implementation
    }
}
