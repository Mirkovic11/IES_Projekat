using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class SeriesCompensator : ConductingEquipment
    {
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

        public SeriesCompensator(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SeriesCompensator s = (SeriesCompensator)obj;
                return (s.r == this.r && s.r0 == this.r0 && s.x == this.x && s.x0 == this.x0);
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
                case ModelCode.SERIESCOMPENS_R:
                case ModelCode.SERIESCOMPENS_R0:
                case ModelCode.SERIESCOMPENS_X:
                case ModelCode.SERIESCOMPENS_X0:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.SERIESCOMPENS_R:
                    prop.SetValue(r);
                    break;

                case ModelCode.SERIESCOMPENS_R0:
                    prop.SetValue(r0);
                    break;

                case ModelCode.SERIESCOMPENS_X:
                    prop.SetValue(x);
                    break;

                case ModelCode.SERIESCOMPENS_X0:
                    prop.SetValue(x0);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SERIESCOMPENS_R:
                    r = property.AsFloat();
                    break;

                case ModelCode.SERIESCOMPENS_R0:
                    r0 = property.AsFloat();
                    break;

                case ModelCode.SERIESCOMPENS_X:
                    x = property.AsFloat();
                    break;

                case ModelCode.SERIESCOMPENS_X0:
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
