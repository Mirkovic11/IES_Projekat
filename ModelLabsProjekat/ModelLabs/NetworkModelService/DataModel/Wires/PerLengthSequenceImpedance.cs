using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class PerLengthSequenceImpedance : PerLengthImpedance
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

        public PerLengthSequenceImpedance(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                PerLengthSequenceImpedance x = (PerLengthSequenceImpedance)obj;
                return (x.b0ch == this.b0ch &&
                        x.bch == this.bch &&
                        x.g0ch == this.g0ch &&
                        x.gch == this.gch &&
                        x.r == this.r &&
                        x.r0 == this.r0 &&
                        x.x == this.x &&
                        x.x0 == this.x0);
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
                case ModelCode.PERLENSEQIMPEDANCE_B0CH:
                case ModelCode.PERLENSEQIMPEDANCE_BCH:
                case ModelCode.PERLENSEQIMPEDANCE_G0CH:
                case ModelCode.PERLENSEQIMPEDANCE_GCH:
                case ModelCode.PERLENSEQIMPEDANCE_R:
                case ModelCode.PERLENSEQIMPEDANCE_R0:
                case ModelCode.PERLENSEQIMPEDANCE_X:
                case ModelCode.PERLENSEQIMPEDANCE_X0:

                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.PERLENSEQIMPEDANCE_B0CH:
                    property.SetValue(b0ch);
                    break;

                case ModelCode.PERLENSEQIMPEDANCE_BCH:
                    property.SetValue(bch);
                    break;

                case ModelCode.PERLENSEQIMPEDANCE_G0CH:
                    property.SetValue(g0ch);
                    break;

                case ModelCode.PERLENSEQIMPEDANCE_GCH:
                    property.SetValue(gch);
                    break;

                case ModelCode.PERLENSEQIMPEDANCE_R:
                    property.SetValue(r);
                    break;

                case ModelCode.PERLENSEQIMPEDANCE_R0:
                    property.SetValue(r0);
                    break;

                case ModelCode.PERLENSEQIMPEDANCE_X:
                    property.SetValue(x);
                    break;

                case ModelCode.PERLENSEQIMPEDANCE_X0:
                    property.SetValue(x0);
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
                case ModelCode.PERLENSEQIMPEDANCE_B0CH:
                    b0ch = property.AsFloat();
                    break;

                case ModelCode.PERLENSEQIMPEDANCE_BCH:
                    bch = property.AsFloat();
                    break;

                case ModelCode.PERLENSEQIMPEDANCE_G0CH:
                    g0ch = property.AsFloat();
                    break;

                case ModelCode.PERLENSEQIMPEDANCE_GCH:
                    gch = property.AsFloat();
                    break;

                case ModelCode.PERLENSEQIMPEDANCE_R:
                    r = property.AsFloat();
                    break;

                case ModelCode.PERLENSEQIMPEDANCE_R0:
                    r0 = property.AsFloat();
                    break;

                case ModelCode.PERLENSEQIMPEDANCE_X:
                    x = property.AsFloat();
                    break;

                case ModelCode.PERLENSEQIMPEDANCE_X0:
                    x0 = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation


    }
}
