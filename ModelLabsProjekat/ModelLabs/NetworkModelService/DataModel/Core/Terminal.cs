using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{

    public class Terminal : IdentifiedObject
    {
        private long connectivityNode = 0;

        public long ConnectivityNode
        {
            get { return connectivityNode; }
            set { connectivityNode = value; }
        }


        private long conductingEquipment = 0;

        public long ConductingEquipment
        {
            get { return conductingEquipment; }
            set { conductingEquipment = value; }
        }

        public Terminal(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Terminal x = (Terminal)obj;
                return ((x.connectivityNode == this.connectivityNode && x.conductingEquipment==this.conductingEquipment));
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
                case ModelCode.TERMINAL_CONNECTNODE:
                case ModelCode.TERMINAL_CONDEQUIPM:

                    return true;
                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TERMINAL_CONNECTNODE:
                    property.SetValue(connectivityNode);
                    break;
                case ModelCode.TERMINAL_CONDEQUIPM:
                    property.SetValue(conductingEquipment);
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
                case ModelCode.TERMINAL_CONNECTNODE:
                    connectivityNode = property.AsReference();
                    break;
                case ModelCode.TERMINAL_CONDEQUIPM:
                    conductingEquipment = property.AsReference();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation
//----------------------------------------za ono NISAM sigurna--------------------------------------
/*
        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (connectivityNode != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CONNECTNODE] = new List<long>();
                references[ModelCode.TERMINAL_CONNECTNODE].Add(connectivityNode);
            }

            if (conductingEquipment != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CONDEQUIPM] = new List<long>();
                references[ModelCode.TERMINAL_CONDEQUIPM].Add(conductingEquipment);
            }

            base.GetReferences(references, refType);
        }*/
//--------------------------------------------------------------------------
        #endregion IReference implementation	
    }
}
