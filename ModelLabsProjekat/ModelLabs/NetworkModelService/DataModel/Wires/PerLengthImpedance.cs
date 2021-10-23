using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class PerLengthImpedance : IdentifiedObject
    {
        private List<long> aCLineSegments = new List<long>();

        public List<long> ACLineSegments
        {
            get { return aCLineSegments; }
            set { aCLineSegments = value; }
        }

        public PerLengthImpedance(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                PerLengthImpedance x = (PerLengthImpedance)obj;
                return (CompareHelper.CompareLists(x.aCLineSegments, this.aCLineSegments, true));
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
                case ModelCode.PERLENIMPENDANCE_ACLINESEGMS:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.PERLENIMPENDANCE_ACLINESEGMS:
                    prop.SetValue(aCLineSegments);
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
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return aCLineSegments.Count > 0 || base.IsReferenced;
            }
        }

      /*  public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (aCLineSegments != null && aCLineSegments.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.PERLENIMPENDANCE_ACLINESEGMS] = aCLineSegments.GetRange(0, aCLineSegments.Count);
            }

            base.GetReferences(references, refType);
        }*/

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.ACLINESEGM_PERLENIMP:
                    aCLineSegments.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.ACLINESEGM_PERLENIMP:

                    if (aCLineSegments.Contains(globalId))
                    {
                        aCLineSegments.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        #endregion IReference implementation	
    }
}
