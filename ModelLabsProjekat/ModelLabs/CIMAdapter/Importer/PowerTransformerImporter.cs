using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();//kreira deltu
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

            //// import all concrete model types (DMSType enum)
            ImportPerLengthSequenceImpedances();
			ImportConnectivityNodes();
			ImportSeriesCompensators();
			ImportDCLineSegments();
            ImportACLineSegments();
            ImportTerminals();

			LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

		#region Import
		private void ImportPerLengthSequenceImpedances()
		{
			SortedDictionary<string, object> cimPerLenSeqImps = concreteModel.GetAllObjectsOfType("FTN.PerLengthSequenceImpedance");//iz konkretnog modela uvucem sve (cimovske) entitete tipa BV
			if (cimPerLenSeqImps != null)
			{                        //mrID
				foreach (KeyValuePair<string, object> cimPerLenSeqImpPair in cimPerLenSeqImps)//za svaki ovaj entitet pravim ResourceDescription
				{                           //INSTANCA CIMOVSKE KLASE
					FTN.PerLengthSequenceImpedance cimPerLenSeqImp = cimPerLenSeqImpPair.Value as FTN.PerLengthSequenceImpedance;

					ResourceDescription rd = CreatePerLengthSequenceImpedanceResourceDescription(cimPerLenSeqImp);//od instance klase pravim RD
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);//UKOLIKO RD nije null, dodajem ga u deltu
						report.Report.Append("PerLengthSequenceImpedance ID = ").Append(cimPerLenSeqImp.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("PerLengthSequenceImpedance ID = ").Append(cimPerLenSeqImp.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreatePerLengthSequenceImpedanceResourceDescription(FTN.PerLengthSequenceImpedance cimPerLenSeqImp)//prima cimovku klasu, vraca RD
		{
			ResourceDescription rd = null;
			if (cimPerLenSeqImp != null)
			{                                       //sistem             tip                    negativni brojac
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.PERLENSEQIMPEDANCE, importHelper.CheckOutIndexForDMSType(DMSType.PERLENSEQIMPEDANCE));//kreiram GID

				rd = new ResourceDescription(gid);//pravim RD koji je prazan, ima samo GID, treba da dodam propertije

				importHelper.DefineIDMapping(cimPerLenSeqImp.ID, gid);//nesto moram da registrujem GID u importHelper-u da bi znao da radi

				////populate ResourceDescription
				PowerTransformerConverter.PopulatePerLengthSequenceImpedanceProperties(cimPerLenSeqImp, rd, importHelper, report);//dodavanje propertija u RD
			}
			return rd;
		}





        private void ImportConnectivityNodes()
        {
            SortedDictionary<string, object> cimConnectivityNodes = concreteModel.GetAllObjectsOfType("FTN.ConnectivityNode");//iz konkretnog modela uvucem sve (cimovske) entitete tipa BV
            if (cimConnectivityNodes != null)
            {                        //mrID
                foreach (KeyValuePair<string, object> cimConnectivityNodePair in cimConnectivityNodes)//za svaki ovaj entitet pravim ResourceDescription
                {                           //INSTANCA CIMOVSKE KLASE
                    FTN.ConnectivityNode cimConnectivityNode = cimConnectivityNodePair.Value as FTN.ConnectivityNode;

                    ResourceDescription rd = CreateConnectivityNodesResourceDescription(cimConnectivityNode);//od instance klase pravim RD
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);//UKOLIKO RD nije null, dodajem ga u deltu
                        report.Report.Append("ConnectivityNode ID = ").Append(cimConnectivityNode.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("ConnectivityNode ID = ").Append(cimConnectivityNode.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateConnectivityNodesResourceDescription(FTN.ConnectivityNode cimConnectivityNode)//prima cimovku klasu, vraca RD
        {
            ResourceDescription rd = null;
            if (cimConnectivityNode != null)
            {                                       //sistem             tip                    negativni brojac
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CONNECTNODE, importHelper.CheckOutIndexForDMSType(DMSType.CONNECTNODE));//kreiram GID

                rd = new ResourceDescription(gid);//pravim RD koji je prazan, ima samo GID, treba da dodam propertije

                importHelper.DefineIDMapping(cimConnectivityNode.ID, gid);//nesto moram da registrujem GID u importHelper-u da bi znao da radi

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateConnectivityNodeProperties(cimConnectivityNode, rd, importHelper, report);//dodavanje propertija u RD
            }
            return rd;
        }




        private void ImportSeriesCompensators()
        {
            SortedDictionary<string, object> cimSeriesCompensators = concreteModel.GetAllObjectsOfType("FTN.SeriesCompensator");//iz konkretnog modela uvucem sve (cimovske) entitete tipa BV
            if (cimSeriesCompensators != null)
            {                        //mrID
                foreach (KeyValuePair<string, object> cimSeriesCompensatorPair in cimSeriesCompensators)//za svaki ovaj entitet pravim ResourceDescription
                {                           //INSTANCA CIMOVSKE KLASE
                    FTN.SeriesCompensator cimSeriesCompensator = cimSeriesCompensatorPair.Value as FTN.SeriesCompensator;

                    ResourceDescription rd = CreateSeriesCompensatorResourceDescription(cimSeriesCompensator);//od instance klase pravim RD
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);//UKOLIKO RD nije null, dodajem ga u deltu
                        report.Report.Append("SeriesCompensator ID = ").Append(cimSeriesCompensator.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("SeriesCompensator ID = ").Append(cimSeriesCompensator.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateSeriesCompensatorResourceDescription(FTN.SeriesCompensator cimSeriesCompensator)//prima cimovku klasu, vraca RD
        {
            ResourceDescription rd = null;
            if (cimSeriesCompensator != null)
            {                                       //sistem             tip                    negativni brojac
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SERIESCOMPENS, importHelper.CheckOutIndexForDMSType(DMSType.SERIESCOMPENS));//kreiram GID

                rd = new ResourceDescription(gid);//pravim RD koji je prazan, ima samo GID, treba da dodam propertije

                importHelper.DefineIDMapping(cimSeriesCompensator.ID, gid);//nesto moram da registrujem GID u importHelper-u da bi znao da radi

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateSeriesCompensatorProperties(cimSeriesCompensator, rd, importHelper, report);//dodavanje propertija u RD
            }
            return rd;
        }





        private void ImportDCLineSegments()
        {
            SortedDictionary<string, object> cimDCLineSegments = concreteModel.GetAllObjectsOfType("FTN.DCLineSegment");//iz konkretnog modela uvucem sve (cimovske) entitete tipa BV
            if (cimDCLineSegments != null)
            {                        //mrID
                foreach (KeyValuePair<string, object> cimDCLineSegmentPair in cimDCLineSegments)//za svaki ovaj entitet pravim ResourceDescription
                {                           //INSTANCA CIMOVSKE KLASE
                    FTN.DCLineSegment cimDCLineSegment = cimDCLineSegmentPair.Value as FTN.DCLineSegment;

                    ResourceDescription rd = CreateDCLineSegmentResourceDescription(cimDCLineSegment);//od instance klase pravim RD
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);//UKOLIKO RD nije null, dodajem ga u deltu
                        report.Report.Append("DCLineSegment ID = ").Append(cimDCLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("DCLineSegment ID = ").Append(cimDCLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateDCLineSegmentResourceDescription(FTN.DCLineSegment cimDCLineSegment)//prima cimovku klasu, vraca RD
        {
            ResourceDescription rd = null;
            if (cimDCLineSegment != null)
            {                                       //sistem             tip                    negativni brojac
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.DCLINESEGM, importHelper.CheckOutIndexForDMSType(DMSType.DCLINESEGM));//kreiram GID

                rd = new ResourceDescription(gid);//pravim RD koji je prazan, ima samo GID, treba da dodam propertije

                importHelper.DefineIDMapping(cimDCLineSegment.ID, gid);//nesto moram da registrujem GID u importHelper-u da bi znao da radi

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateDCLineSegmentProperties(cimDCLineSegment, rd, importHelper, report);//dodavanje propertija u RD
            }
            return rd;
        }



        private void ImportACLineSegments()
        {
            SortedDictionary<string, object> cimACLineSegments = concreteModel.GetAllObjectsOfType("FTN.ACLineSegment");//iz konkretnog modela uvucem sve (cimovske) entitete tipa BV
            if (cimACLineSegments != null)
            {                        //mrID
                foreach (KeyValuePair<string, object> cimACLineSegmentPair in cimACLineSegments)//za svaki ovaj entitet pravim ResourceDescription
                {                           //INSTANCA CIMOVSKE KLASE
                    FTN.ACLineSegment cimACLineSegment = cimACLineSegmentPair.Value as FTN.ACLineSegment;

                    ResourceDescription rd = CreateACLineSegmentResourceDescription(cimACLineSegment);//od instance klase pravim RD
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);//UKOLIKO RD nije null, dodajem ga u deltu
                        report.Report.Append("ACLineSegment ID = ").Append(cimACLineSegment.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("ACLineSegment ID = ").Append(cimACLineSegment.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateACLineSegmentResourceDescription(FTN.ACLineSegment cimACLineSegment)//prima cimovku klasu, vraca RD
        {
            ResourceDescription rd = null;
            if (cimACLineSegment != null)
            {                                       //sistem             tip                    negativni brojac
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ACLINESEGM, importHelper.CheckOutIndexForDMSType(DMSType.ACLINESEGM));//kreiram GID

                rd = new ResourceDescription(gid);//pravim RD koji je prazan, ima samo GID, treba da dodam propertije

                importHelper.DefineIDMapping(cimACLineSegment.ID, gid);//nesto moram da registrujem GID u importHelper-u da bi znao da radi

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateACLineSegmentProperties(cimACLineSegment, rd, importHelper, report);//dodavanje propertija u RD
            }
            return rd;
        }


        private void ImportTerminals()
        {
            SortedDictionary<string, object> cimTerminals = concreteModel.GetAllObjectsOfType("FTN.Terminal");//iz konkretnog modela uvucem sve (cimovske) entitete tipa BV
            if (cimTerminals != null)
            {                        //mrID
                foreach (KeyValuePair<string, object> cimTerminalPair in cimTerminals)//za svaki ovaj entitet pravim ResourceDescription
                {                           //INSTANCA CIMOVSKE KLASE
                    FTN.Terminal cimTerminal = cimTerminalPair.Value as FTN.Terminal;

                    ResourceDescription rd = CreateTerminalResourceDescription(cimTerminal);//od instance klase pravim RD
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);//UKOLIKO RD nije null, dodajem ga u deltu
                        report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Terminal ID = ").Append(cimTerminal.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private ResourceDescription CreateTerminalResourceDescription(FTN.Terminal cimTerminal)//prima cimovku klasu, vraca RD
        {
            ResourceDescription rd = null;
            if (cimTerminal != null)
            {                                       //sistem             tip                    negativni brojac
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.TERMINAL, importHelper.CheckOutIndexForDMSType(DMSType.TERMINAL));//kreiram GID

                rd = new ResourceDescription(gid);//pravim RD koji je prazan, ima samo GID, treba da dodam propertije

                importHelper.DefineIDMapping(cimTerminal.ID, gid);//nesto moram da registrujem GID u importHelper-u da bi znao da radi

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateTerminalProperties(cimTerminal, rd, importHelper, report);//dodavanje propertija u RD
            }
            return rd;
        }

		#endregion Import
	}
}

