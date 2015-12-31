namespace WareStore.Rpts
{
    using CrystalDecisions.CrystalReports.Engine;
    using CrystalDecisions.Shared;
    using System;
    using System.ComponentModel;

    public class InOutRece : ReportClass
    {
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_endTime
        {
            get
            {
                return this.DataDefinition.ParameterFields[1];
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_matInfo
        {
            get
            {
                return this.DataDefinition.ParameterFields[4];
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public IParameterField Parameter_rpsTitleStr
        {
            get
            {
                return this.DataDefinition.ParameterFields[5];
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public IParameterField Parameter_statusTime
        {
            get
            {
                return this.DataDefinition.ParameterFields[0];
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public IParameterField Parameter_userName
        {
            get
            {
                return this.DataDefinition.ParameterFields[2];
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IParameterField Parameter_WHName
        {
            get
            {
                return this.DataDefinition.ParameterFields[3];
            }
        }

        public override string ResourceName
        {
            get
            {
                return "InOutRece.rpt";
            }
            set
            {
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Section Section1
        {
            get
            {
                return this.ReportDefinition.Sections[0];
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Section Section2
        {
            get
            {
                return this.ReportDefinition.Sections[1];
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Section Section3
        {
            get
            {
                return this.ReportDefinition.Sections[2];
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Section Section4
        {
            get
            {
                return this.ReportDefinition.Sections[3];
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Section Section5
        {
            get
            {
                return this.ReportDefinition.Sections[4];
            }
        }
    }
}

