namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)//popunjavanje atributa od roditelja
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
                if (cimIdentifiedObject.AliasNameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
                }
                if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				
			}
		}


        public static void PopulatePerLengthImpedanceProperties(FTN.PerLengthImpedance cimPerLenImp, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPerLenImp != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPerLenImp, rd);//popunjavanje atributa od roditelja

            }
        }

        public static void PopulatePerLengthSequenceImpedanceProperties(FTN.PerLengthSequenceImpedance cimPerLenSeqImp, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPerLenSeqImp != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePerLengthImpedanceProperties(cimPerLenSeqImp, rd, importHelper, report);//prvo se popunjavaju atributi od roditelja


                //--------------------popunjavanje atributa od same klase------------------------
                if (cimPerLenSeqImp.B0chHasValue)
                {                              //model kod atributa                        vrijednost atributa
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_B0CH, cimPerLenSeqImp.B0ch));
                }
                if (cimPerLenSeqImp.BchHasValue)
                {                              //model kod atributa                        vrijednost atributa
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_BCH, cimPerLenSeqImp.Bch));
                }
                if (cimPerLenSeqImp.G0chHasValue)
                {                              //model kod atributa                        vrijednost atributa
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_G0CH, cimPerLenSeqImp.G0ch));
                }
                if (cimPerLenSeqImp.GchHasValue)
                {                              //model kod atributa                        vrijednost atributa
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_GCH, cimPerLenSeqImp.Gch));
                }
                if (cimPerLenSeqImp.RHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_R, cimPerLenSeqImp.R));
                }
                if (cimPerLenSeqImp.R0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_R0, cimPerLenSeqImp.R0));
                }
                if (cimPerLenSeqImp.XHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_X, cimPerLenSeqImp.X));
                }
                if (cimPerLenSeqImp.X0HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.PERLENSEQIMPEDANCE_X0, cimPerLenSeqImp.X0));
                }
            }
        }


        public static void PopulateConnectivityNodeProperties(FTN.ConnectivityNode cimConnectivityNode, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimConnectivityNode != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimConnectivityNode, rd);//prvo se popunjavaju atributi od roditelja


                //--------------------popunjavanje atributa od same klase------------------------
                if (cimConnectivityNode.DescriptionHasValue)
                {                              //model kod atributa                        vrijednost atributa
                    rd.AddProperty(new Property(ModelCode.CONNECTNODE_DESC, cimConnectivityNode.Description));
                }
 
            }
        }


        public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimPowerSystemResource != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);

            }
        }

        public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);

              
            }
        }

        public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimConductingEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd, importHelper, report);

                
            }
        }

        public static void PopulateSeriesCompensatorProperties(FTN.SeriesCompensator cimSeriesCompensator, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSeriesCompensator != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(cimSeriesCompensator, rd, importHelper, report);//prvo se popunjavaju atributi od roditelja


                //--------------------popunjavanje atributa od same klase------------------------
                if (cimSeriesCompensator.RHasValue)
                {                              //model kod atributa                        vrijednost atributa
                    rd.AddProperty(new Property(ModelCode.SERIESCOMPENS_R, cimSeriesCompensator.R));
                }
                if (cimSeriesCompensator.R0HasValue)
                {                              //model kod atributa                        vrijednost atributa
                    rd.AddProperty(new Property(ModelCode.SERIESCOMPENS_R0, cimSeriesCompensator.R0));
                }
                if (cimSeriesCompensator.XHasValue)
                {                              //model kod atributa                        vrijednost atributa
                    rd.AddProperty(new Property(ModelCode.SERIESCOMPENS_X, cimSeriesCompensator.X));
                }
                if (cimSeriesCompensator.X0HasValue)
                {                              //model kod atributa                        vrijednost atributa
                    rd.AddProperty(new Property(ModelCode.SERIESCOMPENS_X0, cimSeriesCompensator.X0));
                }

            }
        }

        public static void PopulateConductorProperties(FTN.Conductor cimConductor, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimConductor != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(cimConductor, rd, importHelper, report);//prvo se popunjavaju atributi od roditelja

                if (cimConductor.LengthHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CONDUCTOR_LEN, cimConductor.Length));
                }
            }
        }

        public static void PopulateDCLineSegmentProperties(FTN.DCLineSegment cimDCLineSegment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimDCLineSegment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductorProperties(cimDCLineSegment, rd, importHelper, report);//prvo se popunjavaju atributi od roditelja
            }
        }

        public static void PopulateACLineSegmentProperties(FTN.ACLineSegment cimACLineSegment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimACLineSegment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductorProperties(cimACLineSegment, rd, importHelper, report);//prvo se popunjavaju atributi od roditelja
            }

            if (cimACLineSegment.B0chHasValue)
            {
                rd.AddProperty(new Property(ModelCode.ACLINESEGM_B0CH, cimACLineSegment.B0ch));
            }
            if (cimACLineSegment.BchHasValue)
            {
                rd.AddProperty(new Property(ModelCode.ACLINESEGM_BCH, cimACLineSegment.Bch));
            }
            if (cimACLineSegment.G0chHasValue)
            {
                rd.AddProperty(new Property(ModelCode.ACLINESEGM_G0CH, cimACLineSegment.G0ch));
            }
            if (cimACLineSegment.GchHasValue)
            {
                rd.AddProperty(new Property(ModelCode.ACLINESEGM_GCH, cimACLineSegment.Gch));
            }
            if (cimACLineSegment.RHasValue)
            {
                rd.AddProperty(new Property(ModelCode.ACLINESEGM_R, cimACLineSegment.R));
            }
            if (cimACLineSegment.R0HasValue)
            {
                rd.AddProperty(new Property(ModelCode.ACLINESEGM_R0, cimACLineSegment.R0));
            }
            if (cimACLineSegment.XHasValue)
            {
                rd.AddProperty(new Property(ModelCode.ACLINESEGM_X, cimACLineSegment.X));

            }
            if (cimACLineSegment.X0HasValue)
            {
                rd.AddProperty(new Property(ModelCode.ACLINESEGM_X0, cimACLineSegment.X0));

            }

            if (cimACLineSegment.PerLengthImpedanceHasValue)
            {
                long gid = importHelper.GetMappedGID(cimACLineSegment.PerLengthImpedance.ID);
                if (gid < 0)
                {
                    report.Report.Append("WARNING: Convert ").Append(cimACLineSegment.GetType().ToString()).Append(" rdfID = \"").Append(cimACLineSegment.ID);
                    report.Report.Append("\" - Failed to set reference to PerLengthImpedance: rdfID \"").Append(cimACLineSegment.PerLengthImpedance.ID).AppendLine(" \" is not mapped to GID!");
                }
                rd.AddProperty(new Property(ModelCode.ACLINESEGM_PERLENIMP, gid));
            }
        }

        public static void PopulateTerminalProperties(FTN.Terminal cimTerminal, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimTerminal != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimTerminal, rd);//prvo se popunjavaju atributi od roditelja
            }

            if (cimTerminal.ConnectivityNodeHasValue)
            {
                long gid = importHelper.GetMappedGID(cimTerminal.ConnectivityNode.ID);
                if (gid < 0)
                {
                    report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
                    report.Report.Append("\" - Failed to set reference to ConnectivityNode: rdfID \"").Append(cimTerminal.ConnectivityNode.ID).AppendLine(" \" is not mapped to GID!");
                }
                rd.AddProperty(new Property(ModelCode.TERMINAL_CONNECTNODE, gid));
            }

            if (cimTerminal.ConductingEquipmentHasValue)
            {
                long gid = importHelper.GetMappedGID(cimTerminal.ConductingEquipment.ID);
                if (gid < 0)
                {
                    report.Report.Append("WARNING: Convert ").Append(cimTerminal.GetType().ToString()).Append(" rdfID = \"").Append(cimTerminal.ID);
                    report.Report.Append("\" - Failed to set reference to ConductingEquipment: rdfID \"").Append(cimTerminal.ConductingEquipment.ID).AppendLine(" \" is not mapped to GID!");
                }
                rd.AddProperty(new Property(ModelCode.TERMINAL_CONDEQUIPM, gid));
            }
        }


        
		#endregion Populate ResourceDescription

		#region Enums convert
		/*public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
		{
			switch (phases)
			{
				case FTN.PhaseCode.A:
					return PhaseCode.A;
				case FTN.PhaseCode.AB:
					return PhaseCode.AB;
				case FTN.PhaseCode.ABC:
					return PhaseCode.ABC;
				case FTN.PhaseCode.ABCN:
					return PhaseCode.ABCN;
				case FTN.PhaseCode.ABN:
					return PhaseCode.ABN;
				case FTN.PhaseCode.AC:
					return PhaseCode.AC;
				case FTN.PhaseCode.ACN:
					return PhaseCode.ACN;
				case FTN.PhaseCode.AN:
					return PhaseCode.AN;
				case FTN.PhaseCode.B:
					return PhaseCode.B;
				case FTN.PhaseCode.BC:
					return PhaseCode.BC;
				case FTN.PhaseCode.BCN:
					return PhaseCode.BCN;
				case FTN.PhaseCode.BN:
					return PhaseCode.BN;
				case FTN.PhaseCode.C:
					return PhaseCode.C;
				case FTN.PhaseCode.CN:
					return PhaseCode.CN;
				case FTN.PhaseCode.N:
					return PhaseCode.N;
				case FTN.PhaseCode.s12N:
					return PhaseCode.ABN;
				case FTN.PhaseCode.s1N:
					return PhaseCode.AN;
				case FTN.PhaseCode.s2N:
					return PhaseCode.BN;
				default: return PhaseCode.Unknown;
			}
		}

		public static TransformerFunction GetDMSTransformerFunctionKind(FTN.TransformerFunctionKind transformerFunction)
		{
			switch (transformerFunction)
			{
				case FTN.TransformerFunctionKind.voltageRegulator:
					return TransformerFunction.Voltreg;
				default:
					return TransformerFunction.Consumer;
			}
		}

		public static WindingType GetDMSWindingType(FTN.WindingType windingType)
		{
			switch (windingType)
			{
				case FTN.WindingType.primary:
					return WindingType.Primary;
				case FTN.WindingType.secondary:
					return WindingType.Secondary;
				case FTN.WindingType.tertiary:
					return WindingType.Tertiary;
				default:
					return WindingType.None;
			}
		}

		public static WindingConnection GetDMSWindingConnection(FTN.WindingConnection windingConnection)
		{
			switch (windingConnection)
			{
				case FTN.WindingConnection.D:
					return WindingConnection.D;
				case FTN.WindingConnection.I:
					return WindingConnection.I;
				case FTN.WindingConnection.Z:
					return WindingConnection.Z;
				case FTN.WindingConnection.Y:
					return WindingConnection.Y;
				default:
					return WindingConnection.Y;
			}
		}*/
		#endregion Enums convert
	}
}
