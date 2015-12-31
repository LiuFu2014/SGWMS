namespace WareStore.Rpts
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable, XmlRoot("myDS"), HelpKeyword("vs.data.DataSet"), XmlSchemaProvider("GetTypedDataSetSchema"), GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), DesignerCategory("code"), ToolboxItem(true)]
    public class myDS : DataSet
    {
        private System.Data.SchemaSerializationMode _schemaSerializationMode;
        private matInOutReceDataTable tablematInOutRece;
        private matInOutReceAllDataTable tablematInOutReceAll;
        private SlackMatCountDataTable tableSlackMatCount;
        private SlackMatDtlDataTable tableSlackMatDtl;
        private tbBillCheckDataTable tabletbBillCheck;
        private tbBillCheckDtlDataTable tabletbBillCheckDtl;
        private tbBillCheckListDataTable tabletbBillCheckList;

        [DebuggerNonUserCode]
        public myDS()
        {
            this._schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            base.BeginInit();
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
            base.EndInit();
        }

        [DebuggerNonUserCode]
        protected myDS(SerializationInfo info, StreamingContext context) : base(info, context, false)
        {
            this._schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            if (base.IsBinarySerialized(info, context))
            {
                this.InitVars(false);
                CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += handler;
                this.Relations.CollectionChanged += handler;
            }
            else
            {
                string s = (string) info.GetValue("XmlSchema", typeof(string));
                if (base.DetermineSchemaSerializationMode(info, context) == System.Data.SchemaSerializationMode.IncludeSchema)
                {
                    DataSet dataSet = new DataSet();
                    dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                    if (dataSet.Tables["matInOutRece"] != null)
                    {
                        base.Tables.Add(new matInOutReceDataTable(dataSet.Tables["matInOutRece"]));
                    }
                    if (dataSet.Tables["matInOutReceAll"] != null)
                    {
                        base.Tables.Add(new matInOutReceAllDataTable(dataSet.Tables["matInOutReceAll"]));
                    }
                    if (dataSet.Tables["SlackMatCount"] != null)
                    {
                        base.Tables.Add(new SlackMatCountDataTable(dataSet.Tables["SlackMatCount"]));
                    }
                    if (dataSet.Tables["SlackMatDtl"] != null)
                    {
                        base.Tables.Add(new SlackMatDtlDataTable(dataSet.Tables["SlackMatDtl"]));
                    }
                    if (dataSet.Tables["tbBillCheck"] != null)
                    {
                        base.Tables.Add(new tbBillCheckDataTable(dataSet.Tables["tbBillCheck"]));
                    }
                    if (dataSet.Tables["tbBillCheckList"] != null)
                    {
                        base.Tables.Add(new tbBillCheckListDataTable(dataSet.Tables["tbBillCheckList"]));
                    }
                    if (dataSet.Tables["tbBillCheckDtl"] != null)
                    {
                        base.Tables.Add(new tbBillCheckDtlDataTable(dataSet.Tables["tbBillCheckDtl"]));
                    }
                    base.DataSetName = dataSet.DataSetName;
                    base.Prefix = dataSet.Prefix;
                    base.Namespace = dataSet.Namespace;
                    base.Locale = dataSet.Locale;
                    base.CaseSensitive = dataSet.CaseSensitive;
                    base.EnforceConstraints = dataSet.EnforceConstraints;
                    base.Merge(dataSet, false, MissingSchemaAction.Add);
                    this.InitVars();
                }
                else
                {
                    base.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                }
                base.GetSerializationData(info, context);
                CollectionChangeEventHandler handler2 = new CollectionChangeEventHandler(this.SchemaChanged);
                base.Tables.CollectionChanged += handler2;
                this.Relations.CollectionChanged += handler2;
            }
        }

        [DebuggerNonUserCode]
        public override DataSet Clone()
        {
            myDS yds = (myDS) base.Clone();
            yds.InitVars();
            yds.SchemaSerializationMode = this.SchemaSerializationMode;
            return yds;
        }

        [DebuggerNonUserCode]
        protected override XmlSchema GetSchemaSerializable()
        {
            MemoryStream w = new MemoryStream();
            base.WriteXmlSchema(new XmlTextWriter(w, null));
            w.Position = 0L;
            return XmlSchema.Read(new XmlTextReader(w), null);
        }

        [DebuggerNonUserCode]
        public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
        {
            myDS yds = new myDS();
            XmlSchemaComplexType type = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            XmlSchemaAny item = new XmlSchemaAny {
                Namespace = yds.Namespace
            };
            sequence.Items.Add(item);
            type.Particle = sequence;
            XmlSchema schemaSerializable = yds.GetSchemaSerializable();
            if (xs.Contains(schemaSerializable.TargetNamespace))
            {
                MemoryStream stream = new MemoryStream();
                MemoryStream stream2 = new MemoryStream();
                try
                {
                    XmlSchema current = null;
                    schemaSerializable.Write(stream);
                    IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        current = (XmlSchema) enumerator.Current;
                        stream2.SetLength(0L);
                        current.Write(stream2);
                        if (stream.Length == stream2.Length)
                        {
                            stream.Position = 0L;
                            stream2.Position = 0L;
                            while ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                            {
                            }
                            if (stream.Position == stream.Length)
                            {
                                return type;
                            }
                        }
                    }
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    if (stream2 != null)
                    {
                        stream2.Close();
                    }
                }
            }
            xs.Add(schemaSerializable);
            return type;
        }

        [DebuggerNonUserCode]
        private void InitClass()
        {
            base.DataSetName = "myDS";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/myDS.xsd";
            base.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tablematInOutRece = new matInOutReceDataTable();
            base.Tables.Add(this.tablematInOutRece);
            this.tablematInOutReceAll = new matInOutReceAllDataTable();
            base.Tables.Add(this.tablematInOutReceAll);
            this.tableSlackMatCount = new SlackMatCountDataTable();
            base.Tables.Add(this.tableSlackMatCount);
            this.tableSlackMatDtl = new SlackMatDtlDataTable();
            base.Tables.Add(this.tableSlackMatDtl);
            this.tabletbBillCheck = new tbBillCheckDataTable();
            base.Tables.Add(this.tabletbBillCheck);
            this.tabletbBillCheckList = new tbBillCheckListDataTable();
            base.Tables.Add(this.tabletbBillCheckList);
            this.tabletbBillCheckDtl = new tbBillCheckDtlDataTable();
            base.Tables.Add(this.tabletbBillCheckDtl);
        }

        [DebuggerNonUserCode]
        protected override void InitializeDerivedDataSet()
        {
            base.BeginInit();
            this.InitClass();
            base.EndInit();
        }

        [DebuggerNonUserCode]
        internal void InitVars()
        {
            this.InitVars(true);
        }

        [DebuggerNonUserCode]
        internal void InitVars(bool initTable)
        {
            this.tablematInOutRece = (matInOutReceDataTable) base.Tables["matInOutRece"];
            if (initTable && (this.tablematInOutRece != null))
            {
                this.tablematInOutRece.InitVars();
            }
            this.tablematInOutReceAll = (matInOutReceAllDataTable) base.Tables["matInOutReceAll"];
            if (initTable && (this.tablematInOutReceAll != null))
            {
                this.tablematInOutReceAll.InitVars();
            }
            this.tableSlackMatCount = (SlackMatCountDataTable) base.Tables["SlackMatCount"];
            if (initTable && (this.tableSlackMatCount != null))
            {
                this.tableSlackMatCount.InitVars();
            }
            this.tableSlackMatDtl = (SlackMatDtlDataTable) base.Tables["SlackMatDtl"];
            if (initTable && (this.tableSlackMatDtl != null))
            {
                this.tableSlackMatDtl.InitVars();
            }
            this.tabletbBillCheck = (tbBillCheckDataTable) base.Tables["tbBillCheck"];
            if (initTable && (this.tabletbBillCheck != null))
            {
                this.tabletbBillCheck.InitVars();
            }
            this.tabletbBillCheckList = (tbBillCheckListDataTable) base.Tables["tbBillCheckList"];
            if (initTable && (this.tabletbBillCheckList != null))
            {
                this.tabletbBillCheckList.InitVars();
            }
            this.tabletbBillCheckDtl = (tbBillCheckDtlDataTable) base.Tables["tbBillCheckDtl"];
            if (initTable && (this.tabletbBillCheckDtl != null))
            {
                this.tabletbBillCheckDtl.InitVars();
            }
        }

        [DebuggerNonUserCode]
        protected override void ReadXmlSerializable(XmlReader reader)
        {
            if (base.DetermineSchemaSerializationMode(reader) == System.Data.SchemaSerializationMode.IncludeSchema)
            {
                this.Reset();
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(reader);
                if (dataSet.Tables["matInOutRece"] != null)
                {
                    base.Tables.Add(new matInOutReceDataTable(dataSet.Tables["matInOutRece"]));
                }
                if (dataSet.Tables["matInOutReceAll"] != null)
                {
                    base.Tables.Add(new matInOutReceAllDataTable(dataSet.Tables["matInOutReceAll"]));
                }
                if (dataSet.Tables["SlackMatCount"] != null)
                {
                    base.Tables.Add(new SlackMatCountDataTable(dataSet.Tables["SlackMatCount"]));
                }
                if (dataSet.Tables["SlackMatDtl"] != null)
                {
                    base.Tables.Add(new SlackMatDtlDataTable(dataSet.Tables["SlackMatDtl"]));
                }
                if (dataSet.Tables["tbBillCheck"] != null)
                {
                    base.Tables.Add(new tbBillCheckDataTable(dataSet.Tables["tbBillCheck"]));
                }
                if (dataSet.Tables["tbBillCheckList"] != null)
                {
                    base.Tables.Add(new tbBillCheckListDataTable(dataSet.Tables["tbBillCheckList"]));
                }
                if (dataSet.Tables["tbBillCheckDtl"] != null)
                {
                    base.Tables.Add(new tbBillCheckDtlDataTable(dataSet.Tables["tbBillCheckDtl"]));
                }
                base.DataSetName = dataSet.DataSetName;
                base.Prefix = dataSet.Prefix;
                base.Namespace = dataSet.Namespace;
                base.Locale = dataSet.Locale;
                base.CaseSensitive = dataSet.CaseSensitive;
                base.EnforceConstraints = dataSet.EnforceConstraints;
                base.Merge(dataSet, false, MissingSchemaAction.Add);
                this.InitVars();
            }
            else
            {
                base.ReadXml(reader);
                this.InitVars();
            }
        }

        [DebuggerNonUserCode]
        private void SchemaChanged(object sender, CollectionChangeEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Remove)
            {
                this.InitVars();
            }
        }

        [DebuggerNonUserCode]
        private bool ShouldSerializematInOutRece()
        {
            return false;
        }

        [DebuggerNonUserCode]
        private bool ShouldSerializematInOutReceAll()
        {
            return false;
        }

        [DebuggerNonUserCode]
        protected override bool ShouldSerializeRelations()
        {
            return false;
        }

        [DebuggerNonUserCode]
        private bool ShouldSerializeSlackMatCount()
        {
            return false;
        }

        [DebuggerNonUserCode]
        private bool ShouldSerializeSlackMatDtl()
        {
            return false;
        }

        [DebuggerNonUserCode]
        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        [DebuggerNonUserCode]
        private bool ShouldSerializetbBillCheck()
        {
            return false;
        }

        [DebuggerNonUserCode]
        private bool ShouldSerializetbBillCheckDtl()
        {
            return false;
        }

        [DebuggerNonUserCode]
        private bool ShouldSerializetbBillCheckList()
        {
            return false;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false), DebuggerNonUserCode]
        public matInOutReceDataTable matInOutRece
        {
            get
            {
                return this.tablematInOutRece;
            }
        }

        [DebuggerNonUserCode, Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public matInOutReceAllDataTable matInOutReceAll
        {
            get
            {
                return this.tablematInOutReceAll;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DebuggerNonUserCode]
        public DataRelationCollection Relations
        {
            get
            {
                return base.Relations;
            }
        }

        [Browsable(true), DebuggerNonUserCode, DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override System.Data.SchemaSerializationMode SchemaSerializationMode
        {
            get
            {
                return this._schemaSerializationMode;
            }
            set
            {
                this._schemaSerializationMode = value;
            }
        }

        [Browsable(false), DebuggerNonUserCode, DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public SlackMatCountDataTable SlackMatCount
        {
            get
            {
                return this.tableSlackMatCount;
            }
        }

        [Browsable(false), DebuggerNonUserCode, DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public SlackMatDtlDataTable SlackMatDtl
        {
            get
            {
                return this.tableSlackMatDtl;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DebuggerNonUserCode]
        public DataTableCollection Tables
        {
            get
            {
                return base.Tables;
            }
        }

        [DebuggerNonUserCode, Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public tbBillCheckDataTable tbBillCheck
        {
            get
            {
                return this.tabletbBillCheck;
            }
        }

        [DebuggerNonUserCode, DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public tbBillCheckDtlDataTable tbBillCheckDtl
        {
            get
            {
                return this.tabletbBillCheckDtl;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DebuggerNonUserCode, Browsable(false)]
        public tbBillCheckListDataTable tbBillCheckList
        {
            get
            {
                return this.tabletbBillCheckList;
            }
        }

        [Serializable, XmlSchemaProvider("GetTypedTableSchema"), GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class matInOutReceAllDataTable : DataTable, IEnumerable
        {
            private DataColumn columncBNo;
            private DataColumn columncBoxId;
            private DataColumn columncBTypeNew;
            private DataColumn columncMNo;
            private DataColumn columncReMark;
            private DataColumn columncUnit;
            private DataColumn columncWhId;
            private DataColumn columndInTime;
            private DataColumn columnfQty;
            private DataColumn columnnPalletId;

            public event myDS.matInOutReceAllRowChangeEventHandler matInOutReceAllRowChanged;

            public event myDS.matInOutReceAllRowChangeEventHandler matInOutReceAllRowChanging;

            public event myDS.matInOutReceAllRowChangeEventHandler matInOutReceAllRowDeleted;

            public event myDS.matInOutReceAllRowChangeEventHandler matInOutReceAllRowDeleting;

            [DebuggerNonUserCode]
            public matInOutReceAllDataTable()
            {
                base.TableName = "matInOutReceAll";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode]
            internal matInOutReceAllDataTable(DataTable table)
            {
                base.TableName = table.TableName;
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    base.CaseSensitive = table.CaseSensitive;
                }
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    base.Locale = table.Locale;
                }
                if (table.Namespace != table.DataSet.Namespace)
                {
                    base.Namespace = table.Namespace;
                }
                base.Prefix = table.Prefix;
                base.MinimumCapacity = table.MinimumCapacity;
            }

            [DebuggerNonUserCode]
            protected matInOutReceAllDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode]
            public void AddmatInOutReceAllRow(myDS.matInOutReceAllRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode]
            public myDS.matInOutReceAllRow AddmatInOutReceAllRow(string nPalletId, string cBoxId, string cMNo, string cWhId, string dInTime, string fQty, string cBNo, string cReMark, string cUnit, string cBTypeNew)
            {
                myDS.matInOutReceAllRow row = (myDS.matInOutReceAllRow) base.NewRow();
                row.ItemArray = new object[] { nPalletId, cBoxId, cMNo, cWhId, dInTime, fQty, cBNo, cReMark, cUnit, cBTypeNew };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode]
            public override DataTable Clone()
            {
                myDS.matInOutReceAllDataTable table = (myDS.matInOutReceAllDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode]
            protected override DataTable CreateInstance()
            {
                return new myDS.matInOutReceAllDataTable();
            }

            [DebuggerNonUserCode]
            public virtual IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            [DebuggerNonUserCode]
            protected override Type GetRowType()
            {
                return typeof(myDS.matInOutReceAllRow);
            }

            [DebuggerNonUserCode]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                myDS yds = new myDS();
                XmlSchemaAny item = new XmlSchemaAny {
                    Namespace = "http://www.w3.org/2001/XMLSchema",
                    MinOccurs = 0M,
                    MaxOccurs = 79228162514264337593543950335M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(item);
                XmlSchemaAny any2 = new XmlSchemaAny {
                    Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                    MinOccurs = 1M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(any2);
                XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                    Name = "namespace",
                    FixedValue = yds.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "matInOutReceAllDataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                XmlSchema schemaSerializable = yds.GetSchemaSerializable();
                if (xs.Contains(schemaSerializable.TargetNamespace))
                {
                    MemoryStream stream = new MemoryStream();
                    MemoryStream stream2 = new MemoryStream();
                    try
                    {
                        XmlSchema current = null;
                        schemaSerializable.Write(stream);
                        IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            current = (XmlSchema) enumerator.Current;
                            stream2.SetLength(0L);
                            current.Write(stream2);
                            if (stream.Length == stream2.Length)
                            {
                                stream.Position = 0L;
                                stream2.Position = 0L;
                                while ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                                {
                                }
                                if (stream.Position == stream.Length)
                                {
                                    return type;
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (stream != null)
                        {
                            stream.Close();
                        }
                        if (stream2 != null)
                        {
                            stream2.Close();
                        }
                    }
                }
                xs.Add(schemaSerializable);
                return type;
            }

            [DebuggerNonUserCode]
            private void InitClass()
            {
                this.columnnPalletId = new DataColumn("nPalletId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnnPalletId);
                this.columncBoxId = new DataColumn("cBoxId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncBoxId);
                this.columncMNo = new DataColumn("cMNo", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncMNo);
                this.columncWhId = new DataColumn("cWhId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncWhId);
                this.columndInTime = new DataColumn("dInTime", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columndInTime);
                this.columnfQty = new DataColumn("fQty", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnfQty);
                this.columncBNo = new DataColumn("cBNo", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncBNo);
                this.columncReMark = new DataColumn("cReMark", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncReMark);
                this.columncUnit = new DataColumn("cUnit", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncUnit);
                this.columncBTypeNew = new DataColumn("cBTypeNew", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncBTypeNew);
            }

            [DebuggerNonUserCode]
            internal void InitVars()
            {
                this.columnnPalletId = base.Columns["nPalletId"];
                this.columncBoxId = base.Columns["cBoxId"];
                this.columncMNo = base.Columns["cMNo"];
                this.columncWhId = base.Columns["cWhId"];
                this.columndInTime = base.Columns["dInTime"];
                this.columnfQty = base.Columns["fQty"];
                this.columncBNo = base.Columns["cBNo"];
                this.columncReMark = base.Columns["cReMark"];
                this.columncUnit = base.Columns["cUnit"];
                this.columncBTypeNew = base.Columns["cBTypeNew"];
            }

            [DebuggerNonUserCode]
            public myDS.matInOutReceAllRow NewmatInOutReceAllRow()
            {
                return (myDS.matInOutReceAllRow) base.NewRow();
            }

            [DebuggerNonUserCode]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new myDS.matInOutReceAllRow(builder);
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.matInOutReceAllRowChanged != null)
                {
                    this.matInOutReceAllRowChanged(this, new myDS.matInOutReceAllRowChangeEvent((myDS.matInOutReceAllRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.matInOutReceAllRowChanging != null)
                {
                    this.matInOutReceAllRowChanging(this, new myDS.matInOutReceAllRowChangeEvent((myDS.matInOutReceAllRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.matInOutReceAllRowDeleted != null)
                {
                    this.matInOutReceAllRowDeleted(this, new myDS.matInOutReceAllRowChangeEvent((myDS.matInOutReceAllRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.matInOutReceAllRowDeleting != null)
                {
                    this.matInOutReceAllRowDeleting(this, new myDS.matInOutReceAllRowChangeEvent((myDS.matInOutReceAllRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            public void RemovematInOutReceAllRow(myDS.matInOutReceAllRow row)
            {
                base.Rows.Remove(row);
            }

            [DebuggerNonUserCode]
            public DataColumn cBNoColumn
            {
                get
                {
                    return this.columncBNo;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cBoxIdColumn
            {
                get
                {
                    return this.columncBoxId;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cBTypeNewColumn
            {
                get
                {
                    return this.columncBTypeNew;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cMNoColumn
            {
                get
                {
                    return this.columncMNo;
                }
            }

            [DebuggerNonUserCode, Browsable(false)]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cReMarkColumn
            {
                get
                {
                    return this.columncReMark;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cUnitColumn
            {
                get
                {
                    return this.columncUnit;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cWhIdColumn
            {
                get
                {
                    return this.columncWhId;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn dInTimeColumn
            {
                get
                {
                    return this.columndInTime;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn fQtyColumn
            {
                get
                {
                    return this.columnfQty;
                }
            }

            [DebuggerNonUserCode]
            public myDS.matInOutReceAllRow this[int index]
            {
                get
                {
                    return (myDS.matInOutReceAllRow) base.Rows[index];
                }
            }

            [DebuggerNonUserCode]
            public DataColumn nPalletIdColumn
            {
                get
                {
                    return this.columnnPalletId;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class matInOutReceAllRow : DataRow
        {
            private myDS.matInOutReceAllDataTable tablematInOutReceAll;

            [DebuggerNonUserCode]
            internal matInOutReceAllRow(DataRowBuilder rb) : base(rb)
            {
                this.tablematInOutReceAll = (myDS.matInOutReceAllDataTable) base.Table;
            }

            [DebuggerNonUserCode]
            public bool IscBNoNull()
            {
                return base.IsNull(this.tablematInOutReceAll.cBNoColumn);
            }

            [DebuggerNonUserCode]
            public bool IscBoxIdNull()
            {
                return base.IsNull(this.tablematInOutReceAll.cBoxIdColumn);
            }

            [DebuggerNonUserCode]
            public bool IscBTypeNewNull()
            {
                return base.IsNull(this.tablematInOutReceAll.cBTypeNewColumn);
            }

            [DebuggerNonUserCode]
            public bool IscMNoNull()
            {
                return base.IsNull(this.tablematInOutReceAll.cMNoColumn);
            }

            [DebuggerNonUserCode]
            public bool IscReMarkNull()
            {
                return base.IsNull(this.tablematInOutReceAll.cReMarkColumn);
            }

            [DebuggerNonUserCode]
            public bool IscUnitNull()
            {
                return base.IsNull(this.tablematInOutReceAll.cUnitColumn);
            }

            [DebuggerNonUserCode]
            public bool IscWhIdNull()
            {
                return base.IsNull(this.tablematInOutReceAll.cWhIdColumn);
            }

            [DebuggerNonUserCode]
            public bool IsdInTimeNull()
            {
                return base.IsNull(this.tablematInOutReceAll.dInTimeColumn);
            }

            [DebuggerNonUserCode]
            public bool IsfQtyNull()
            {
                return base.IsNull(this.tablematInOutReceAll.fQtyColumn);
            }

            [DebuggerNonUserCode]
            public bool IsnPalletIdNull()
            {
                return base.IsNull(this.tablematInOutReceAll.nPalletIdColumn);
            }

            [DebuggerNonUserCode]
            public void SetcBNoNull()
            {
                base[this.tablematInOutReceAll.cBNoColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcBoxIdNull()
            {
                base[this.tablematInOutReceAll.cBoxIdColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcBTypeNewNull()
            {
                base[this.tablematInOutReceAll.cBTypeNewColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcMNoNull()
            {
                base[this.tablematInOutReceAll.cMNoColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcReMarkNull()
            {
                base[this.tablematInOutReceAll.cReMarkColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcUnitNull()
            {
                base[this.tablematInOutReceAll.cUnitColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcWhIdNull()
            {
                base[this.tablematInOutReceAll.cWhIdColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetdInTimeNull()
            {
                base[this.tablematInOutReceAll.dInTimeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetfQtyNull()
            {
                base[this.tablematInOutReceAll.fQtyColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetnPalletIdNull()
            {
                base[this.tablematInOutReceAll.nPalletIdColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public string cBNo
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutReceAll.cBNoColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutReceAll”中列“cBNo”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutReceAll.cBNoColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cBoxId
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutReceAll.cBoxIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutReceAll”中列“cBoxId”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutReceAll.cBoxIdColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cBTypeNew
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutReceAll.cBTypeNewColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutReceAll”中列“cBTypeNew”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutReceAll.cBTypeNewColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cMNo
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutReceAll.cMNoColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutReceAll”中列“cMNo”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutReceAll.cMNoColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cReMark
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutReceAll.cReMarkColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutReceAll”中列“cReMark”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutReceAll.cReMarkColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cUnit
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutReceAll.cUnitColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutReceAll”中列“cUnit”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutReceAll.cUnitColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cWhId
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutReceAll.cWhIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutReceAll”中列“cWhId”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutReceAll.cWhIdColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string dInTime
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutReceAll.dInTimeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutReceAll”中列“dInTime”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutReceAll.dInTimeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string fQty
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutReceAll.fQtyColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutReceAll”中列“fQty”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutReceAll.fQtyColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string nPalletId
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutReceAll.nPalletIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutReceAll”中列“nPalletId”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutReceAll.nPalletIdColumn] = value;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class matInOutReceAllRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private myDS.matInOutReceAllRow eventRow;

            [DebuggerNonUserCode]
            public matInOutReceAllRowChangeEvent(myDS.matInOutReceAllRow row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [DebuggerNonUserCode]
            public DataRowAction Action
            {
                get
                {
                    return this.eventAction;
                }
            }

            [DebuggerNonUserCode]
            public myDS.matInOutReceAllRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void matInOutReceAllRowChangeEventHandler(object sender, myDS.matInOutReceAllRowChangeEvent e);

        [Serializable, XmlSchemaProvider("GetTypedTableSchema"), GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class matInOutReceDataTable : DataTable, IEnumerable
        {
            private DataColumn columncMno;
            private DataColumn columncName;
            private DataColumn columncSpec;
            private DataColumn columncUnit;
            private DataColumn columnfQty;

            public event myDS.matInOutReceRowChangeEventHandler matInOutReceRowChanged;

            public event myDS.matInOutReceRowChangeEventHandler matInOutReceRowChanging;

            public event myDS.matInOutReceRowChangeEventHandler matInOutReceRowDeleted;

            public event myDS.matInOutReceRowChangeEventHandler matInOutReceRowDeleting;

            [DebuggerNonUserCode]
            public matInOutReceDataTable()
            {
                base.TableName = "matInOutRece";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode]
            internal matInOutReceDataTable(DataTable table)
            {
                base.TableName = table.TableName;
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    base.CaseSensitive = table.CaseSensitive;
                }
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    base.Locale = table.Locale;
                }
                if (table.Namespace != table.DataSet.Namespace)
                {
                    base.Namespace = table.Namespace;
                }
                base.Prefix = table.Prefix;
                base.MinimumCapacity = table.MinimumCapacity;
            }

            [DebuggerNonUserCode]
            protected matInOutReceDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode]
            public void AddmatInOutReceRow(myDS.matInOutReceRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode]
            public myDS.matInOutReceRow AddmatInOutReceRow(string cMno, string fQty, string cName, string cSpec, string cUnit)
            {
                myDS.matInOutReceRow row = (myDS.matInOutReceRow) base.NewRow();
                row.ItemArray = new object[] { cMno, fQty, cName, cSpec, cUnit };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode]
            public override DataTable Clone()
            {
                myDS.matInOutReceDataTable table = (myDS.matInOutReceDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode]
            protected override DataTable CreateInstance()
            {
                return new myDS.matInOutReceDataTable();
            }

            [DebuggerNonUserCode]
            public virtual IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            [DebuggerNonUserCode]
            protected override Type GetRowType()
            {
                return typeof(myDS.matInOutReceRow);
            }

            [DebuggerNonUserCode]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                myDS yds = new myDS();
                XmlSchemaAny item = new XmlSchemaAny {
                    Namespace = "http://www.w3.org/2001/XMLSchema",
                    MinOccurs = 0M,
                    MaxOccurs = 79228162514264337593543950335M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(item);
                XmlSchemaAny any2 = new XmlSchemaAny {
                    Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                    MinOccurs = 1M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(any2);
                XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                    Name = "namespace",
                    FixedValue = yds.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "matInOutReceDataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                XmlSchema schemaSerializable = yds.GetSchemaSerializable();
                if (xs.Contains(schemaSerializable.TargetNamespace))
                {
                    MemoryStream stream = new MemoryStream();
                    MemoryStream stream2 = new MemoryStream();
                    try
                    {
                        XmlSchema current = null;
                        schemaSerializable.Write(stream);
                        IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            current = (XmlSchema) enumerator.Current;
                            stream2.SetLength(0L);
                            current.Write(stream2);
                            if (stream.Length == stream2.Length)
                            {
                                stream.Position = 0L;
                                stream2.Position = 0L;
                                while ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                                {
                                }
                                if (stream.Position == stream.Length)
                                {
                                    return type;
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (stream != null)
                        {
                            stream.Close();
                        }
                        if (stream2 != null)
                        {
                            stream2.Close();
                        }
                    }
                }
                xs.Add(schemaSerializable);
                return type;
            }

            [DebuggerNonUserCode]
            private void InitClass()
            {
                this.columncMno = new DataColumn("cMno", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncMno);
                this.columnfQty = new DataColumn("fQty", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnfQty);
                this.columncName = new DataColumn("cName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncName);
                this.columncSpec = new DataColumn("cSpec", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncSpec);
                this.columncUnit = new DataColumn("cUnit", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncUnit);
            }

            [DebuggerNonUserCode]
            internal void InitVars()
            {
                this.columncMno = base.Columns["cMno"];
                this.columnfQty = base.Columns["fQty"];
                this.columncName = base.Columns["cName"];
                this.columncSpec = base.Columns["cSpec"];
                this.columncUnit = base.Columns["cUnit"];
            }

            [DebuggerNonUserCode]
            public myDS.matInOutReceRow NewmatInOutReceRow()
            {
                return (myDS.matInOutReceRow) base.NewRow();
            }

            [DebuggerNonUserCode]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new myDS.matInOutReceRow(builder);
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.matInOutReceRowChanged != null)
                {
                    this.matInOutReceRowChanged(this, new myDS.matInOutReceRowChangeEvent((myDS.matInOutReceRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.matInOutReceRowChanging != null)
                {
                    this.matInOutReceRowChanging(this, new myDS.matInOutReceRowChangeEvent((myDS.matInOutReceRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.matInOutReceRowDeleted != null)
                {
                    this.matInOutReceRowDeleted(this, new myDS.matInOutReceRowChangeEvent((myDS.matInOutReceRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.matInOutReceRowDeleting != null)
                {
                    this.matInOutReceRowDeleting(this, new myDS.matInOutReceRowChangeEvent((myDS.matInOutReceRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            public void RemovematInOutReceRow(myDS.matInOutReceRow row)
            {
                base.Rows.Remove(row);
            }

            [DebuggerNonUserCode]
            public DataColumn cMnoColumn
            {
                get
                {
                    return this.columncMno;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cNameColumn
            {
                get
                {
                    return this.columncName;
                }
            }

            [Browsable(false), DebuggerNonUserCode]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cSpecColumn
            {
                get
                {
                    return this.columncSpec;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cUnitColumn
            {
                get
                {
                    return this.columncUnit;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn fQtyColumn
            {
                get
                {
                    return this.columnfQty;
                }
            }

            [DebuggerNonUserCode]
            public myDS.matInOutReceRow this[int index]
            {
                get
                {
                    return (myDS.matInOutReceRow) base.Rows[index];
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class matInOutReceRow : DataRow
        {
            private myDS.matInOutReceDataTable tablematInOutRece;

            [DebuggerNonUserCode]
            internal matInOutReceRow(DataRowBuilder rb) : base(rb)
            {
                this.tablematInOutRece = (myDS.matInOutReceDataTable) base.Table;
            }

            [DebuggerNonUserCode]
            public bool IscMnoNull()
            {
                return base.IsNull(this.tablematInOutRece.cMnoColumn);
            }

            [DebuggerNonUserCode]
            public bool IscNameNull()
            {
                return base.IsNull(this.tablematInOutRece.cNameColumn);
            }

            [DebuggerNonUserCode]
            public bool IscSpecNull()
            {
                return base.IsNull(this.tablematInOutRece.cSpecColumn);
            }

            [DebuggerNonUserCode]
            public bool IscUnitNull()
            {
                return base.IsNull(this.tablematInOutRece.cUnitColumn);
            }

            [DebuggerNonUserCode]
            public bool IsfQtyNull()
            {
                return base.IsNull(this.tablematInOutRece.fQtyColumn);
            }

            [DebuggerNonUserCode]
            public void SetcMnoNull()
            {
                base[this.tablematInOutRece.cMnoColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcNameNull()
            {
                base[this.tablematInOutRece.cNameColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcSpecNull()
            {
                base[this.tablematInOutRece.cSpecColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcUnitNull()
            {
                base[this.tablematInOutRece.cUnitColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetfQtyNull()
            {
                base[this.tablematInOutRece.fQtyColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public string cMno
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutRece.cMnoColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutRece”中列“cMno”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutRece.cMnoColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cName
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutRece.cNameColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutRece”中列“cName”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutRece.cNameColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cSpec
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutRece.cSpecColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutRece”中列“cSpec”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutRece.cSpecColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cUnit
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutRece.cUnitColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutRece”中列“cUnit”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutRece.cUnitColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string fQty
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tablematInOutRece.fQtyColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“matInOutRece”中列“fQty”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tablematInOutRece.fQtyColumn] = value;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class matInOutReceRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private myDS.matInOutReceRow eventRow;

            [DebuggerNonUserCode]
            public matInOutReceRowChangeEvent(myDS.matInOutReceRow row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [DebuggerNonUserCode]
            public DataRowAction Action
            {
                get
                {
                    return this.eventAction;
                }
            }

            [DebuggerNonUserCode]
            public myDS.matInOutReceRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void matInOutReceRowChangeEventHandler(object sender, myDS.matInOutReceRowChangeEvent e);

        [Serializable, XmlSchemaProvider("GetTypedTableSchema"), GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class SlackMatCountDataTable : DataTable, IEnumerable
        {
            private DataColumn columncMatStyle;
            private DataColumn columncMName;
            private DataColumn columncMNo;
            private DataColumn columncSpec;
            private DataColumn columncUnit;
            private DataColumn columndLastDate;
            private DataColumn columnfQty;

            public event myDS.SlackMatCountRowChangeEventHandler SlackMatCountRowChanged;

            public event myDS.SlackMatCountRowChangeEventHandler SlackMatCountRowChanging;

            public event myDS.SlackMatCountRowChangeEventHandler SlackMatCountRowDeleted;

            public event myDS.SlackMatCountRowChangeEventHandler SlackMatCountRowDeleting;

            [DebuggerNonUserCode]
            public SlackMatCountDataTable()
            {
                base.TableName = "SlackMatCount";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode]
            internal SlackMatCountDataTable(DataTable table)
            {
                base.TableName = table.TableName;
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    base.CaseSensitive = table.CaseSensitive;
                }
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    base.Locale = table.Locale;
                }
                if (table.Namespace != table.DataSet.Namespace)
                {
                    base.Namespace = table.Namespace;
                }
                base.Prefix = table.Prefix;
                base.MinimumCapacity = table.MinimumCapacity;
            }

            [DebuggerNonUserCode]
            protected SlackMatCountDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode]
            public void AddSlackMatCountRow(myDS.SlackMatCountRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode]
            public myDS.SlackMatCountRow AddSlackMatCountRow(string cMNo, string fQty, string cMName, string cSpec, string cUnit, string cMatStyle, string dLastDate)
            {
                myDS.SlackMatCountRow row = (myDS.SlackMatCountRow) base.NewRow();
                row.ItemArray = new object[] { cMNo, fQty, cMName, cSpec, cUnit, cMatStyle, dLastDate };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode]
            public override DataTable Clone()
            {
                myDS.SlackMatCountDataTable table = (myDS.SlackMatCountDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode]
            protected override DataTable CreateInstance()
            {
                return new myDS.SlackMatCountDataTable();
            }

            [DebuggerNonUserCode]
            public virtual IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            [DebuggerNonUserCode]
            protected override Type GetRowType()
            {
                return typeof(myDS.SlackMatCountRow);
            }

            [DebuggerNonUserCode]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                myDS yds = new myDS();
                XmlSchemaAny item = new XmlSchemaAny {
                    Namespace = "http://www.w3.org/2001/XMLSchema",
                    MinOccurs = 0M,
                    MaxOccurs = 79228162514264337593543950335M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(item);
                XmlSchemaAny any2 = new XmlSchemaAny {
                    Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                    MinOccurs = 1M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(any2);
                XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                    Name = "namespace",
                    FixedValue = yds.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "SlackMatCountDataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                XmlSchema schemaSerializable = yds.GetSchemaSerializable();
                if (xs.Contains(schemaSerializable.TargetNamespace))
                {
                    MemoryStream stream = new MemoryStream();
                    MemoryStream stream2 = new MemoryStream();
                    try
                    {
                        XmlSchema current = null;
                        schemaSerializable.Write(stream);
                        IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            current = (XmlSchema) enumerator.Current;
                            stream2.SetLength(0L);
                            current.Write(stream2);
                            if (stream.Length == stream2.Length)
                            {
                                stream.Position = 0L;
                                stream2.Position = 0L;
                                while ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                                {
                                }
                                if (stream.Position == stream.Length)
                                {
                                    return type;
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (stream != null)
                        {
                            stream.Close();
                        }
                        if (stream2 != null)
                        {
                            stream2.Close();
                        }
                    }
                }
                xs.Add(schemaSerializable);
                return type;
            }

            [DebuggerNonUserCode]
            private void InitClass()
            {
                this.columncMNo = new DataColumn("cMNo", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncMNo);
                this.columnfQty = new DataColumn("fQty", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnfQty);
                this.columncMName = new DataColumn("cMName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncMName);
                this.columncSpec = new DataColumn("cSpec", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncSpec);
                this.columncUnit = new DataColumn("cUnit", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncUnit);
                this.columncMatStyle = new DataColumn("cMatStyle", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncMatStyle);
                this.columndLastDate = new DataColumn("dLastDate", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columndLastDate);
                this.columncMNo.Caption = "cMno";
            }

            [DebuggerNonUserCode]
            internal void InitVars()
            {
                this.columncMNo = base.Columns["cMNo"];
                this.columnfQty = base.Columns["fQty"];
                this.columncMName = base.Columns["cMName"];
                this.columncSpec = base.Columns["cSpec"];
                this.columncUnit = base.Columns["cUnit"];
                this.columncMatStyle = base.Columns["cMatStyle"];
                this.columndLastDate = base.Columns["dLastDate"];
            }

            [DebuggerNonUserCode]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new myDS.SlackMatCountRow(builder);
            }

            [DebuggerNonUserCode]
            public myDS.SlackMatCountRow NewSlackMatCountRow()
            {
                return (myDS.SlackMatCountRow) base.NewRow();
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.SlackMatCountRowChanged != null)
                {
                    this.SlackMatCountRowChanged(this, new myDS.SlackMatCountRowChangeEvent((myDS.SlackMatCountRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.SlackMatCountRowChanging != null)
                {
                    this.SlackMatCountRowChanging(this, new myDS.SlackMatCountRowChangeEvent((myDS.SlackMatCountRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.SlackMatCountRowDeleted != null)
                {
                    this.SlackMatCountRowDeleted(this, new myDS.SlackMatCountRowChangeEvent((myDS.SlackMatCountRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.SlackMatCountRowDeleting != null)
                {
                    this.SlackMatCountRowDeleting(this, new myDS.SlackMatCountRowChangeEvent((myDS.SlackMatCountRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            public void RemoveSlackMatCountRow(myDS.SlackMatCountRow row)
            {
                base.Rows.Remove(row);
            }

            [DebuggerNonUserCode]
            public DataColumn cMatStyleColumn
            {
                get
                {
                    return this.columncMatStyle;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cMNameColumn
            {
                get
                {
                    return this.columncMName;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cMNoColumn
            {
                get
                {
                    return this.columncMNo;
                }
            }

            [DebuggerNonUserCode, Browsable(false)]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cSpecColumn
            {
                get
                {
                    return this.columncSpec;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cUnitColumn
            {
                get
                {
                    return this.columncUnit;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn dLastDateColumn
            {
                get
                {
                    return this.columndLastDate;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn fQtyColumn
            {
                get
                {
                    return this.columnfQty;
                }
            }

            [DebuggerNonUserCode]
            public myDS.SlackMatCountRow this[int index]
            {
                get
                {
                    return (myDS.SlackMatCountRow) base.Rows[index];
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class SlackMatCountRow : DataRow
        {
            private myDS.SlackMatCountDataTable tableSlackMatCount;

            [DebuggerNonUserCode]
            internal SlackMatCountRow(DataRowBuilder rb) : base(rb)
            {
                this.tableSlackMatCount = (myDS.SlackMatCountDataTable) base.Table;
            }

            [DebuggerNonUserCode]
            public bool IscMatStyleNull()
            {
                return base.IsNull(this.tableSlackMatCount.cMatStyleColumn);
            }

            [DebuggerNonUserCode]
            public bool IscMNameNull()
            {
                return base.IsNull(this.tableSlackMatCount.cMNameColumn);
            }

            [DebuggerNonUserCode]
            public bool IscMNoNull()
            {
                return base.IsNull(this.tableSlackMatCount.cMNoColumn);
            }

            [DebuggerNonUserCode]
            public bool IscSpecNull()
            {
                return base.IsNull(this.tableSlackMatCount.cSpecColumn);
            }

            [DebuggerNonUserCode]
            public bool IscUnitNull()
            {
                return base.IsNull(this.tableSlackMatCount.cUnitColumn);
            }

            [DebuggerNonUserCode]
            public bool IsdLastDateNull()
            {
                return base.IsNull(this.tableSlackMatCount.dLastDateColumn);
            }

            [DebuggerNonUserCode]
            public bool IsfQtyNull()
            {
                return base.IsNull(this.tableSlackMatCount.fQtyColumn);
            }

            [DebuggerNonUserCode]
            public void SetcMatStyleNull()
            {
                base[this.tableSlackMatCount.cMatStyleColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcMNameNull()
            {
                base[this.tableSlackMatCount.cMNameColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcMNoNull()
            {
                base[this.tableSlackMatCount.cMNoColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcSpecNull()
            {
                base[this.tableSlackMatCount.cSpecColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcUnitNull()
            {
                base[this.tableSlackMatCount.cUnitColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetdLastDateNull()
            {
                base[this.tableSlackMatCount.dLastDateColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetfQtyNull()
            {
                base[this.tableSlackMatCount.fQtyColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public string cMatStyle
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatCount.cMatStyleColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatCount”中列“cMatStyle”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatCount.cMatStyleColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cMName
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatCount.cMNameColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatCount”中列“cMName”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatCount.cMNameColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cMNo
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatCount.cMNoColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatCount”中列“cMNo”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatCount.cMNoColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cSpec
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatCount.cSpecColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatCount”中列“cSpec”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatCount.cSpecColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cUnit
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatCount.cUnitColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatCount”中列“cUnit”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatCount.cUnitColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string dLastDate
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatCount.dLastDateColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatCount”中列“dLastDate”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatCount.dLastDateColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string fQty
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatCount.fQtyColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatCount”中列“fQty”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatCount.fQtyColumn] = value;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class SlackMatCountRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private myDS.SlackMatCountRow eventRow;

            [DebuggerNonUserCode]
            public SlackMatCountRowChangeEvent(myDS.SlackMatCountRow row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [DebuggerNonUserCode]
            public DataRowAction Action
            {
                get
                {
                    return this.eventAction;
                }
            }

            [DebuggerNonUserCode]
            public myDS.SlackMatCountRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void SlackMatCountRowChangeEventHandler(object sender, myDS.SlackMatCountRowChangeEvent e);

        [Serializable, XmlSchemaProvider("GetTypedTableSchema"), GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class SlackMatDtlDataTable : DataTable, IEnumerable
        {
            private DataColumn columncMatStyle;
            private DataColumn columncMName;
            private DataColumn columncMNo;
            private DataColumn columncPosId;
            private DataColumn columncSpec;
            private DataColumn columncUnit;
            private DataColumn columndLastDate;
            private DataColumn columnfQty;
            private DataColumn columnnPalletId;

            public event myDS.SlackMatDtlRowChangeEventHandler SlackMatDtlRowChanged;

            public event myDS.SlackMatDtlRowChangeEventHandler SlackMatDtlRowChanging;

            public event myDS.SlackMatDtlRowChangeEventHandler SlackMatDtlRowDeleted;

            public event myDS.SlackMatDtlRowChangeEventHandler SlackMatDtlRowDeleting;

            [DebuggerNonUserCode]
            public SlackMatDtlDataTable()
            {
                base.TableName = "SlackMatDtl";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode]
            internal SlackMatDtlDataTable(DataTable table)
            {
                base.TableName = table.TableName;
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    base.CaseSensitive = table.CaseSensitive;
                }
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    base.Locale = table.Locale;
                }
                if (table.Namespace != table.DataSet.Namespace)
                {
                    base.Namespace = table.Namespace;
                }
                base.Prefix = table.Prefix;
                base.MinimumCapacity = table.MinimumCapacity;
            }

            [DebuggerNonUserCode]
            protected SlackMatDtlDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode]
            public void AddSlackMatDtlRow(myDS.SlackMatDtlRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode]
            public myDS.SlackMatDtlRow AddSlackMatDtlRow(string cMNo, string fQty, string cMName, string cSpec, string cUnit, string cMatStyle, string dLastDate, string cPosId, string nPalletId)
            {
                myDS.SlackMatDtlRow row = (myDS.SlackMatDtlRow) base.NewRow();
                row.ItemArray = new object[] { cMNo, fQty, cMName, cSpec, cUnit, cMatStyle, dLastDate, cPosId, nPalletId };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode]
            public override DataTable Clone()
            {
                myDS.SlackMatDtlDataTable table = (myDS.SlackMatDtlDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode]
            protected override DataTable CreateInstance()
            {
                return new myDS.SlackMatDtlDataTable();
            }

            [DebuggerNonUserCode]
            public virtual IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            [DebuggerNonUserCode]
            protected override Type GetRowType()
            {
                return typeof(myDS.SlackMatDtlRow);
            }

            [DebuggerNonUserCode]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                myDS yds = new myDS();
                XmlSchemaAny item = new XmlSchemaAny {
                    Namespace = "http://www.w3.org/2001/XMLSchema",
                    MinOccurs = 0M,
                    MaxOccurs = 79228162514264337593543950335M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(item);
                XmlSchemaAny any2 = new XmlSchemaAny {
                    Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                    MinOccurs = 1M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(any2);
                XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                    Name = "namespace",
                    FixedValue = yds.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "SlackMatDtlDataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                XmlSchema schemaSerializable = yds.GetSchemaSerializable();
                if (xs.Contains(schemaSerializable.TargetNamespace))
                {
                    MemoryStream stream = new MemoryStream();
                    MemoryStream stream2 = new MemoryStream();
                    try
                    {
                        XmlSchema current = null;
                        schemaSerializable.Write(stream);
                        IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            current = (XmlSchema) enumerator.Current;
                            stream2.SetLength(0L);
                            current.Write(stream2);
                            if (stream.Length == stream2.Length)
                            {
                                stream.Position = 0L;
                                stream2.Position = 0L;
                                while ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                                {
                                }
                                if (stream.Position == stream.Length)
                                {
                                    return type;
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (stream != null)
                        {
                            stream.Close();
                        }
                        if (stream2 != null)
                        {
                            stream2.Close();
                        }
                    }
                }
                xs.Add(schemaSerializable);
                return type;
            }

            [DebuggerNonUserCode]
            private void InitClass()
            {
                this.columncMNo = new DataColumn("cMNo", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncMNo);
                this.columnfQty = new DataColumn("fQty", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnfQty);
                this.columncMName = new DataColumn("cMName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncMName);
                this.columncSpec = new DataColumn("cSpec", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncSpec);
                this.columncUnit = new DataColumn("cUnit", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncUnit);
                this.columncMatStyle = new DataColumn("cMatStyle", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncMatStyle);
                this.columndLastDate = new DataColumn("dLastDate", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columndLastDate);
                this.columncPosId = new DataColumn("cPosId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncPosId);
                this.columnnPalletId = new DataColumn("nPalletId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnnPalletId);
                this.columncMNo.Caption = "cMno";
            }

            [DebuggerNonUserCode]
            internal void InitVars()
            {
                this.columncMNo = base.Columns["cMNo"];
                this.columnfQty = base.Columns["fQty"];
                this.columncMName = base.Columns["cMName"];
                this.columncSpec = base.Columns["cSpec"];
                this.columncUnit = base.Columns["cUnit"];
                this.columncMatStyle = base.Columns["cMatStyle"];
                this.columndLastDate = base.Columns["dLastDate"];
                this.columncPosId = base.Columns["cPosId"];
                this.columnnPalletId = base.Columns["nPalletId"];
            }

            [DebuggerNonUserCode]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new myDS.SlackMatDtlRow(builder);
            }

            [DebuggerNonUserCode]
            public myDS.SlackMatDtlRow NewSlackMatDtlRow()
            {
                return (myDS.SlackMatDtlRow) base.NewRow();
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.SlackMatDtlRowChanged != null)
                {
                    this.SlackMatDtlRowChanged(this, new myDS.SlackMatDtlRowChangeEvent((myDS.SlackMatDtlRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.SlackMatDtlRowChanging != null)
                {
                    this.SlackMatDtlRowChanging(this, new myDS.SlackMatDtlRowChangeEvent((myDS.SlackMatDtlRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.SlackMatDtlRowDeleted != null)
                {
                    this.SlackMatDtlRowDeleted(this, new myDS.SlackMatDtlRowChangeEvent((myDS.SlackMatDtlRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.SlackMatDtlRowDeleting != null)
                {
                    this.SlackMatDtlRowDeleting(this, new myDS.SlackMatDtlRowChangeEvent((myDS.SlackMatDtlRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            public void RemoveSlackMatDtlRow(myDS.SlackMatDtlRow row)
            {
                base.Rows.Remove(row);
            }

            [DebuggerNonUserCode]
            public DataColumn cMatStyleColumn
            {
                get
                {
                    return this.columncMatStyle;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cMNameColumn
            {
                get
                {
                    return this.columncMName;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cMNoColumn
            {
                get
                {
                    return this.columncMNo;
                }
            }

            [DebuggerNonUserCode, Browsable(false)]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cPosIdColumn
            {
                get
                {
                    return this.columncPosId;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cSpecColumn
            {
                get
                {
                    return this.columncSpec;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cUnitColumn
            {
                get
                {
                    return this.columncUnit;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn dLastDateColumn
            {
                get
                {
                    return this.columndLastDate;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn fQtyColumn
            {
                get
                {
                    return this.columnfQty;
                }
            }

            [DebuggerNonUserCode]
            public myDS.SlackMatDtlRow this[int index]
            {
                get
                {
                    return (myDS.SlackMatDtlRow) base.Rows[index];
                }
            }

            [DebuggerNonUserCode]
            public DataColumn nPalletIdColumn
            {
                get
                {
                    return this.columnnPalletId;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class SlackMatDtlRow : DataRow
        {
            private myDS.SlackMatDtlDataTable tableSlackMatDtl;

            [DebuggerNonUserCode]
            internal SlackMatDtlRow(DataRowBuilder rb) : base(rb)
            {
                this.tableSlackMatDtl = (myDS.SlackMatDtlDataTable) base.Table;
            }

            [DebuggerNonUserCode]
            public bool IscMatStyleNull()
            {
                return base.IsNull(this.tableSlackMatDtl.cMatStyleColumn);
            }

            [DebuggerNonUserCode]
            public bool IscMNameNull()
            {
                return base.IsNull(this.tableSlackMatDtl.cMNameColumn);
            }

            [DebuggerNonUserCode]
            public bool IscMNoNull()
            {
                return base.IsNull(this.tableSlackMatDtl.cMNoColumn);
            }

            [DebuggerNonUserCode]
            public bool IscPosIdNull()
            {
                return base.IsNull(this.tableSlackMatDtl.cPosIdColumn);
            }

            [DebuggerNonUserCode]
            public bool IscSpecNull()
            {
                return base.IsNull(this.tableSlackMatDtl.cSpecColumn);
            }

            [DebuggerNonUserCode]
            public bool IscUnitNull()
            {
                return base.IsNull(this.tableSlackMatDtl.cUnitColumn);
            }

            [DebuggerNonUserCode]
            public bool IsdLastDateNull()
            {
                return base.IsNull(this.tableSlackMatDtl.dLastDateColumn);
            }

            [DebuggerNonUserCode]
            public bool IsfQtyNull()
            {
                return base.IsNull(this.tableSlackMatDtl.fQtyColumn);
            }

            [DebuggerNonUserCode]
            public bool IsnPalletIdNull()
            {
                return base.IsNull(this.tableSlackMatDtl.nPalletIdColumn);
            }

            [DebuggerNonUserCode]
            public void SetcMatStyleNull()
            {
                base[this.tableSlackMatDtl.cMatStyleColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcMNameNull()
            {
                base[this.tableSlackMatDtl.cMNameColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcMNoNull()
            {
                base[this.tableSlackMatDtl.cMNoColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcPosIdNull()
            {
                base[this.tableSlackMatDtl.cPosIdColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcSpecNull()
            {
                base[this.tableSlackMatDtl.cSpecColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcUnitNull()
            {
                base[this.tableSlackMatDtl.cUnitColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetdLastDateNull()
            {
                base[this.tableSlackMatDtl.dLastDateColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetfQtyNull()
            {
                base[this.tableSlackMatDtl.fQtyColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetnPalletIdNull()
            {
                base[this.tableSlackMatDtl.nPalletIdColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public string cMatStyle
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatDtl.cMatStyleColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatDtl”中列“cMatStyle”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatDtl.cMatStyleColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cMName
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatDtl.cMNameColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatDtl”中列“cMName”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatDtl.cMNameColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cMNo
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatDtl.cMNoColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatDtl”中列“cMNo”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatDtl.cMNoColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cPosId
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatDtl.cPosIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatDtl”中列“cPosId”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatDtl.cPosIdColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cSpec
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatDtl.cSpecColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatDtl”中列“cSpec”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatDtl.cSpecColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cUnit
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatDtl.cUnitColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatDtl”中列“cUnit”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatDtl.cUnitColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string dLastDate
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatDtl.dLastDateColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatDtl”中列“dLastDate”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatDtl.dLastDateColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string fQty
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatDtl.fQtyColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatDtl”中列“fQty”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatDtl.fQtyColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string nPalletId
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableSlackMatDtl.nPalletIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“SlackMatDtl”中列“nPalletId”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableSlackMatDtl.nPalletIdColumn] = value;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class SlackMatDtlRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private myDS.SlackMatDtlRow eventRow;

            [DebuggerNonUserCode]
            public SlackMatDtlRowChangeEvent(myDS.SlackMatDtlRow row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [DebuggerNonUserCode]
            public DataRowAction Action
            {
                get
                {
                    return this.eventAction;
                }
            }

            [DebuggerNonUserCode]
            public myDS.SlackMatDtlRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void SlackMatDtlRowChangeEventHandler(object sender, myDS.SlackMatDtlRowChangeEvent e);

        [Serializable, XmlSchemaProvider("GetTypedTableSchema"), GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class tbBillCheckDataTable : DataTable, IEnumerable
        {
            private DataColumn columncBNo;
            private DataColumn columncBNoAjust;
            private DataColumn columncBNoBad;
            private DataColumn columncBType;
            private DataColumn columncIsChecked;
            private DataColumn columncIsFinished;
            private DataColumn columncRemark;
            private DataColumn columncUser;
            private DataColumn columncWHId;
            private DataColumn columndDate;

            public event myDS.tbBillCheckRowChangeEventHandler tbBillCheckRowChanged;

            public event myDS.tbBillCheckRowChangeEventHandler tbBillCheckRowChanging;

            public event myDS.tbBillCheckRowChangeEventHandler tbBillCheckRowDeleted;

            public event myDS.tbBillCheckRowChangeEventHandler tbBillCheckRowDeleting;

            [DebuggerNonUserCode]
            public tbBillCheckDataTable()
            {
                base.TableName = "tbBillCheck";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode]
            internal tbBillCheckDataTable(DataTable table)
            {
                base.TableName = table.TableName;
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    base.CaseSensitive = table.CaseSensitive;
                }
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    base.Locale = table.Locale;
                }
                if (table.Namespace != table.DataSet.Namespace)
                {
                    base.Namespace = table.Namespace;
                }
                base.Prefix = table.Prefix;
                base.MinimumCapacity = table.MinimumCapacity;
            }

            [DebuggerNonUserCode]
            protected tbBillCheckDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode]
            public void AddtbBillCheckRow(myDS.tbBillCheckRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode]
            public myDS.tbBillCheckRow AddtbBillCheckRow(string cBNo, string cBType, string cWHId, string dDate, string cIsChecked, string cIsFinished, string cUser, string cRemark, string cBNoAjust, string cBNoBad)
            {
                myDS.tbBillCheckRow row = (myDS.tbBillCheckRow) base.NewRow();
                row.ItemArray = new object[] { cBNo, cBType, cWHId, dDate, cIsChecked, cIsFinished, cUser, cRemark, cBNoAjust, cBNoBad };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode]
            public override DataTable Clone()
            {
                myDS.tbBillCheckDataTable table = (myDS.tbBillCheckDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode]
            protected override DataTable CreateInstance()
            {
                return new myDS.tbBillCheckDataTable();
            }

            [DebuggerNonUserCode]
            public virtual IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            [DebuggerNonUserCode]
            protected override Type GetRowType()
            {
                return typeof(myDS.tbBillCheckRow);
            }

            [DebuggerNonUserCode]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                myDS yds = new myDS();
                XmlSchemaAny item = new XmlSchemaAny {
                    Namespace = "http://www.w3.org/2001/XMLSchema",
                    MinOccurs = 0M,
                    MaxOccurs = 79228162514264337593543950335M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(item);
                XmlSchemaAny any2 = new XmlSchemaAny {
                    Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                    MinOccurs = 1M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(any2);
                XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                    Name = "namespace",
                    FixedValue = yds.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "tbBillCheckDataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                XmlSchema schemaSerializable = yds.GetSchemaSerializable();
                if (xs.Contains(schemaSerializable.TargetNamespace))
                {
                    MemoryStream stream = new MemoryStream();
                    MemoryStream stream2 = new MemoryStream();
                    try
                    {
                        XmlSchema current = null;
                        schemaSerializable.Write(stream);
                        IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            current = (XmlSchema) enumerator.Current;
                            stream2.SetLength(0L);
                            current.Write(stream2);
                            if (stream.Length == stream2.Length)
                            {
                                stream.Position = 0L;
                                stream2.Position = 0L;
                                while ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                                {
                                }
                                if (stream.Position == stream.Length)
                                {
                                    return type;
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (stream != null)
                        {
                            stream.Close();
                        }
                        if (stream2 != null)
                        {
                            stream2.Close();
                        }
                    }
                }
                xs.Add(schemaSerializable);
                return type;
            }

            [DebuggerNonUserCode]
            private void InitClass()
            {
                this.columncBNo = new DataColumn("cBNo", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncBNo);
                this.columncBType = new DataColumn("cBType", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncBType);
                this.columncWHId = new DataColumn("cWHId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncWHId);
                this.columndDate = new DataColumn("dDate", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columndDate);
                this.columncIsChecked = new DataColumn("cIsChecked", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncIsChecked);
                this.columncIsFinished = new DataColumn("cIsFinished", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncIsFinished);
                this.columncUser = new DataColumn("cUser", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncUser);
                this.columncRemark = new DataColumn("cRemark", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncRemark);
                this.columncBNoAjust = new DataColumn("cBNoAjust", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncBNoAjust);
                this.columncBNoBad = new DataColumn("cBNoBad", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncBNoBad);
                this.columncBNo.Caption = "cMno";
                this.columncBType.Caption = "fQty";
                this.columncWHId.Caption = "cMName";
                this.columndDate.Caption = "cSpec";
                this.columncIsChecked.Caption = "cUnit";
                this.columncIsFinished.Caption = "cMatStyle";
            }

            [DebuggerNonUserCode]
            internal void InitVars()
            {
                this.columncBNo = base.Columns["cBNo"];
                this.columncBType = base.Columns["cBType"];
                this.columncWHId = base.Columns["cWHId"];
                this.columndDate = base.Columns["dDate"];
                this.columncIsChecked = base.Columns["cIsChecked"];
                this.columncIsFinished = base.Columns["cIsFinished"];
                this.columncUser = base.Columns["cUser"];
                this.columncRemark = base.Columns["cRemark"];
                this.columncBNoAjust = base.Columns["cBNoAjust"];
                this.columncBNoBad = base.Columns["cBNoBad"];
            }

            [DebuggerNonUserCode]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new myDS.tbBillCheckRow(builder);
            }

            [DebuggerNonUserCode]
            public myDS.tbBillCheckRow NewtbBillCheckRow()
            {
                return (myDS.tbBillCheckRow) base.NewRow();
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.tbBillCheckRowChanged != null)
                {
                    this.tbBillCheckRowChanged(this, new myDS.tbBillCheckRowChangeEvent((myDS.tbBillCheckRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.tbBillCheckRowChanging != null)
                {
                    this.tbBillCheckRowChanging(this, new myDS.tbBillCheckRowChangeEvent((myDS.tbBillCheckRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.tbBillCheckRowDeleted != null)
                {
                    this.tbBillCheckRowDeleted(this, new myDS.tbBillCheckRowChangeEvent((myDS.tbBillCheckRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.tbBillCheckRowDeleting != null)
                {
                    this.tbBillCheckRowDeleting(this, new myDS.tbBillCheckRowChangeEvent((myDS.tbBillCheckRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            public void RemovetbBillCheckRow(myDS.tbBillCheckRow row)
            {
                base.Rows.Remove(row);
            }

            [DebuggerNonUserCode]
            public DataColumn cBNoAjustColumn
            {
                get
                {
                    return this.columncBNoAjust;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cBNoBadColumn
            {
                get
                {
                    return this.columncBNoBad;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cBNoColumn
            {
                get
                {
                    return this.columncBNo;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cBTypeColumn
            {
                get
                {
                    return this.columncBType;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cIsCheckedColumn
            {
                get
                {
                    return this.columncIsChecked;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cIsFinishedColumn
            {
                get
                {
                    return this.columncIsFinished;
                }
            }

            [Browsable(false), DebuggerNonUserCode]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cRemarkColumn
            {
                get
                {
                    return this.columncRemark;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cUserColumn
            {
                get
                {
                    return this.columncUser;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cWHIdColumn
            {
                get
                {
                    return this.columncWHId;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn dDateColumn
            {
                get
                {
                    return this.columndDate;
                }
            }

            [DebuggerNonUserCode]
            public myDS.tbBillCheckRow this[int index]
            {
                get
                {
                    return (myDS.tbBillCheckRow) base.Rows[index];
                }
            }
        }

        [Serializable, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
        public class tbBillCheckDtlDataTable : DataTable, IEnumerable
        {
            private DataColumn columncBatchNo;
            private DataColumn columncBNoIn;
            private DataColumn columncBoxId;
            private DataColumn columncMName;
            private DataColumn columncMNo;
            private DataColumn columncPosId;
            private DataColumn columncQCStatus;
            private DataColumn columncSpec;
            private DataColumn columncUnit;
            private DataColumn columnfBad;
            private DataColumn columnfDiff;
            private DataColumn columnfErpQty;
            private DataColumn columnfQty;
            private DataColumn columnnItemIn;
            private DataColumn columnnPalletId;

            public event myDS.tbBillCheckDtlRowChangeEventHandler tbBillCheckDtlRowChanged;

            public event myDS.tbBillCheckDtlRowChangeEventHandler tbBillCheckDtlRowChanging;

            public event myDS.tbBillCheckDtlRowChangeEventHandler tbBillCheckDtlRowDeleted;

            public event myDS.tbBillCheckDtlRowChangeEventHandler tbBillCheckDtlRowDeleting;

            [DebuggerNonUserCode]
            public tbBillCheckDtlDataTable()
            {
                base.TableName = "tbBillCheckDtl";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode]
            internal tbBillCheckDtlDataTable(DataTable table)
            {
                base.TableName = table.TableName;
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    base.CaseSensitive = table.CaseSensitive;
                }
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    base.Locale = table.Locale;
                }
                if (table.Namespace != table.DataSet.Namespace)
                {
                    base.Namespace = table.Namespace;
                }
                base.Prefix = table.Prefix;
                base.MinimumCapacity = table.MinimumCapacity;
            }

            [DebuggerNonUserCode]
            protected tbBillCheckDtlDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode]
            public void AddtbBillCheckDtlRow(myDS.tbBillCheckDtlRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode]
            public myDS.tbBillCheckDtlRow AddtbBillCheckDtlRow(string cMNo, string cMName, string cSpec, string cBatchNo, string cQCStatus, string fQty, string fDiff, string fBad, string fErpQty, string cUnit, string cPosId, string nPalletId, string cBoxId, string cBNoIn, string nItemIn)
            {
                myDS.tbBillCheckDtlRow row = (myDS.tbBillCheckDtlRow) base.NewRow();
                row.ItemArray = new object[] { cMNo, cMName, cSpec, cBatchNo, cQCStatus, fQty, fDiff, fBad, fErpQty, cUnit, cPosId, nPalletId, cBoxId, cBNoIn, nItemIn };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode]
            public override DataTable Clone()
            {
                myDS.tbBillCheckDtlDataTable table = (myDS.tbBillCheckDtlDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode]
            protected override DataTable CreateInstance()
            {
                return new myDS.tbBillCheckDtlDataTable();
            }

            [DebuggerNonUserCode]
            public virtual IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            [DebuggerNonUserCode]
            protected override Type GetRowType()
            {
                return typeof(myDS.tbBillCheckDtlRow);
            }

            [DebuggerNonUserCode]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                myDS yds = new myDS();
                XmlSchemaAny item = new XmlSchemaAny {
                    Namespace = "http://www.w3.org/2001/XMLSchema",
                    MinOccurs = 0M,
                    MaxOccurs = 79228162514264337593543950335M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(item);
                XmlSchemaAny any2 = new XmlSchemaAny {
                    Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                    MinOccurs = 1M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(any2);
                XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                    Name = "namespace",
                    FixedValue = yds.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "tbBillCheckDtlDataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                XmlSchema schemaSerializable = yds.GetSchemaSerializable();
                if (xs.Contains(schemaSerializable.TargetNamespace))
                {
                    MemoryStream stream = new MemoryStream();
                    MemoryStream stream2 = new MemoryStream();
                    try
                    {
                        XmlSchema current = null;
                        schemaSerializable.Write(stream);
                        IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            current = (XmlSchema) enumerator.Current;
                            stream2.SetLength(0L);
                            current.Write(stream2);
                            if (stream.Length == stream2.Length)
                            {
                                stream.Position = 0L;
                                stream2.Position = 0L;
                                while ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                                {
                                }
                                if (stream.Position == stream.Length)
                                {
                                    return type;
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (stream != null)
                        {
                            stream.Close();
                        }
                        if (stream2 != null)
                        {
                            stream2.Close();
                        }
                    }
                }
                xs.Add(schemaSerializable);
                return type;
            }

            [DebuggerNonUserCode]
            private void InitClass()
            {
                this.columncMNo = new DataColumn("cMNo", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncMNo);
                this.columncMName = new DataColumn("cMName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncMName);
                this.columncSpec = new DataColumn("cSpec", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncSpec);
                this.columncBatchNo = new DataColumn("cBatchNo", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncBatchNo);
                this.columncQCStatus = new DataColumn("cQCStatus", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncQCStatus);
                this.columnfQty = new DataColumn("fQty", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnfQty);
                this.columnfDiff = new DataColumn("fDiff", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnfDiff);
                this.columnfBad = new DataColumn("fBad", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnfBad);
                this.columnfErpQty = new DataColumn("fErpQty", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnfErpQty);
                this.columncUnit = new DataColumn("cUnit", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncUnit);
                this.columncPosId = new DataColumn("cPosId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncPosId);
                this.columnnPalletId = new DataColumn("nPalletId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnnPalletId);
                this.columncBoxId = new DataColumn("cBoxId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncBoxId);
                this.columncBNoIn = new DataColumn("cBNoIn", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncBNoIn);
                this.columnnItemIn = new DataColumn("nItemIn", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnnItemIn);
                this.columncMNo.Caption = "cMno";
                this.columncMName.Caption = "fQty";
                this.columncSpec.Caption = "cMName";
                this.columncBatchNo.Caption = "cSpec";
                this.columncQCStatus.Caption = "cUnit";
                this.columnfQty.Caption = "cMatStyle";
                this.columnfDiff.Caption = "cUser";
                this.columnfBad.Caption = "dLastDate";
                this.columnfErpQty.Caption = "cRemark";
                this.columncUnit.Caption = "cBNoAjust";
            }

            [DebuggerNonUserCode]
            internal void InitVars()
            {
                this.columncMNo = base.Columns["cMNo"];
                this.columncMName = base.Columns["cMName"];
                this.columncSpec = base.Columns["cSpec"];
                this.columncBatchNo = base.Columns["cBatchNo"];
                this.columncQCStatus = base.Columns["cQCStatus"];
                this.columnfQty = base.Columns["fQty"];
                this.columnfDiff = base.Columns["fDiff"];
                this.columnfBad = base.Columns["fBad"];
                this.columnfErpQty = base.Columns["fErpQty"];
                this.columncUnit = base.Columns["cUnit"];
                this.columncPosId = base.Columns["cPosId"];
                this.columnnPalletId = base.Columns["nPalletId"];
                this.columncBoxId = base.Columns["cBoxId"];
                this.columncBNoIn = base.Columns["cBNoIn"];
                this.columnnItemIn = base.Columns["nItemIn"];
            }

            [DebuggerNonUserCode]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new myDS.tbBillCheckDtlRow(builder);
            }

            [DebuggerNonUserCode]
            public myDS.tbBillCheckDtlRow NewtbBillCheckDtlRow()
            {
                return (myDS.tbBillCheckDtlRow) base.NewRow();
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.tbBillCheckDtlRowChanged != null)
                {
                    this.tbBillCheckDtlRowChanged(this, new myDS.tbBillCheckDtlRowChangeEvent((myDS.tbBillCheckDtlRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.tbBillCheckDtlRowChanging != null)
                {
                    this.tbBillCheckDtlRowChanging(this, new myDS.tbBillCheckDtlRowChangeEvent((myDS.tbBillCheckDtlRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.tbBillCheckDtlRowDeleted != null)
                {
                    this.tbBillCheckDtlRowDeleted(this, new myDS.tbBillCheckDtlRowChangeEvent((myDS.tbBillCheckDtlRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.tbBillCheckDtlRowDeleting != null)
                {
                    this.tbBillCheckDtlRowDeleting(this, new myDS.tbBillCheckDtlRowChangeEvent((myDS.tbBillCheckDtlRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            public void RemovetbBillCheckDtlRow(myDS.tbBillCheckDtlRow row)
            {
                base.Rows.Remove(row);
            }

            [DebuggerNonUserCode]
            public DataColumn cBatchNoColumn
            {
                get
                {
                    return this.columncBatchNo;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cBNoInColumn
            {
                get
                {
                    return this.columncBNoIn;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cBoxIdColumn
            {
                get
                {
                    return this.columncBoxId;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cMNameColumn
            {
                get
                {
                    return this.columncMName;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cMNoColumn
            {
                get
                {
                    return this.columncMNo;
                }
            }

            [DebuggerNonUserCode, Browsable(false)]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cPosIdColumn
            {
                get
                {
                    return this.columncPosId;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cQCStatusColumn
            {
                get
                {
                    return this.columncQCStatus;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cSpecColumn
            {
                get
                {
                    return this.columncSpec;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cUnitColumn
            {
                get
                {
                    return this.columncUnit;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn fBadColumn
            {
                get
                {
                    return this.columnfBad;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn fDiffColumn
            {
                get
                {
                    return this.columnfDiff;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn fErpQtyColumn
            {
                get
                {
                    return this.columnfErpQty;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn fQtyColumn
            {
                get
                {
                    return this.columnfQty;
                }
            }

            [DebuggerNonUserCode]
            public myDS.tbBillCheckDtlRow this[int index]
            {
                get
                {
                    return (myDS.tbBillCheckDtlRow) base.Rows[index];
                }
            }

            [DebuggerNonUserCode]
            public DataColumn nItemInColumn
            {
                get
                {
                    return this.columnnItemIn;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn nPalletIdColumn
            {
                get
                {
                    return this.columnnPalletId;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class tbBillCheckDtlRow : DataRow
        {
            private myDS.tbBillCheckDtlDataTable tabletbBillCheckDtl;

            [DebuggerNonUserCode]
            internal tbBillCheckDtlRow(DataRowBuilder rb) : base(rb)
            {
                this.tabletbBillCheckDtl = (myDS.tbBillCheckDtlDataTable) base.Table;
            }

            [DebuggerNonUserCode]
            public bool IscBatchNoNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.cBatchNoColumn);
            }

            [DebuggerNonUserCode]
            public bool IscBNoInNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.cBNoInColumn);
            }

            [DebuggerNonUserCode]
            public bool IscBoxIdNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.cBoxIdColumn);
            }

            [DebuggerNonUserCode]
            public bool IscMNameNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.cMNameColumn);
            }

            [DebuggerNonUserCode]
            public bool IscMNoNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.cMNoColumn);
            }

            [DebuggerNonUserCode]
            public bool IscPosIdNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.cPosIdColumn);
            }

            [DebuggerNonUserCode]
            public bool IscQCStatusNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.cQCStatusColumn);
            }

            [DebuggerNonUserCode]
            public bool IscSpecNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.cSpecColumn);
            }

            [DebuggerNonUserCode]
            public bool IscUnitNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.cUnitColumn);
            }

            [DebuggerNonUserCode]
            public bool IsfBadNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.fBadColumn);
            }

            [DebuggerNonUserCode]
            public bool IsfDiffNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.fDiffColumn);
            }

            [DebuggerNonUserCode]
            public bool IsfErpQtyNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.fErpQtyColumn);
            }

            [DebuggerNonUserCode]
            public bool IsfQtyNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.fQtyColumn);
            }

            [DebuggerNonUserCode]
            public bool IsnItemInNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.nItemInColumn);
            }

            [DebuggerNonUserCode]
            public bool IsnPalletIdNull()
            {
                return base.IsNull(this.tabletbBillCheckDtl.nPalletIdColumn);
            }

            [DebuggerNonUserCode]
            public void SetcBatchNoNull()
            {
                base[this.tabletbBillCheckDtl.cBatchNoColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcBNoInNull()
            {
                base[this.tabletbBillCheckDtl.cBNoInColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcBoxIdNull()
            {
                base[this.tabletbBillCheckDtl.cBoxIdColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcMNameNull()
            {
                base[this.tabletbBillCheckDtl.cMNameColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcMNoNull()
            {
                base[this.tabletbBillCheckDtl.cMNoColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcPosIdNull()
            {
                base[this.tabletbBillCheckDtl.cPosIdColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcQCStatusNull()
            {
                base[this.tabletbBillCheckDtl.cQCStatusColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcSpecNull()
            {
                base[this.tabletbBillCheckDtl.cSpecColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcUnitNull()
            {
                base[this.tabletbBillCheckDtl.cUnitColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetfBadNull()
            {
                base[this.tabletbBillCheckDtl.fBadColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetfDiffNull()
            {
                base[this.tabletbBillCheckDtl.fDiffColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetfErpQtyNull()
            {
                base[this.tabletbBillCheckDtl.fErpQtyColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetfQtyNull()
            {
                base[this.tabletbBillCheckDtl.fQtyColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetnItemInNull()
            {
                base[this.tabletbBillCheckDtl.nItemInColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetnPalletIdNull()
            {
                base[this.tabletbBillCheckDtl.nPalletIdColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public string cBatchNo
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.cBatchNoColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“cBatchNo”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.cBatchNoColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cBNoIn
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.cBNoInColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“cBNoIn”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.cBNoInColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cBoxId
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.cBoxIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“cBoxId”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.cBoxIdColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cMName
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.cMNameColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“cMName”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.cMNameColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cMNo
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.cMNoColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“cMNo”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.cMNoColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cPosId
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.cPosIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“cPosId”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.cPosIdColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cQCStatus
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.cQCStatusColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“cQCStatus”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.cQCStatusColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cSpec
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.cSpecColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“cSpec”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.cSpecColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cUnit
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.cUnitColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“cUnit”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.cUnitColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string fBad
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.fBadColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“fBad”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.fBadColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string fDiff
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.fDiffColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“fDiff”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.fDiffColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string fErpQty
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.fErpQtyColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“fErpQty”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.fErpQtyColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string fQty
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.fQtyColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“fQty”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.fQtyColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string nItemIn
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.nItemInColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“nItemIn”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.nItemInColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string nPalletId
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckDtl.nPalletIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckDtl”中列“nPalletId”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckDtl.nPalletIdColumn] = value;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class tbBillCheckDtlRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private myDS.tbBillCheckDtlRow eventRow;

            [DebuggerNonUserCode]
            public tbBillCheckDtlRowChangeEvent(myDS.tbBillCheckDtlRow row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [DebuggerNonUserCode]
            public DataRowAction Action
            {
                get
                {
                    return this.eventAction;
                }
            }

            [DebuggerNonUserCode]
            public myDS.tbBillCheckDtlRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void tbBillCheckDtlRowChangeEventHandler(object sender, myDS.tbBillCheckDtlRowChangeEvent e);

        [Serializable, XmlSchemaProvider("GetTypedTableSchema"), GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class tbBillCheckListDataTable : DataTable, IEnumerable
        {
            private DataColumn columncBatchNo;
            private DataColumn columncMName;
            private DataColumn columncMNo;
            private DataColumn columncQCStatus;
            private DataColumn columncSpec;
            private DataColumn columncUnit;
            private DataColumn columnfBad;
            private DataColumn columnfDiff;
            private DataColumn columnfErpQty;
            private DataColumn columnfQty;

            public event myDS.tbBillCheckListRowChangeEventHandler tbBillCheckListRowChanged;

            public event myDS.tbBillCheckListRowChangeEventHandler tbBillCheckListRowChanging;

            public event myDS.tbBillCheckListRowChangeEventHandler tbBillCheckListRowDeleted;

            public event myDS.tbBillCheckListRowChangeEventHandler tbBillCheckListRowDeleting;

            [DebuggerNonUserCode]
            public tbBillCheckListDataTable()
            {
                base.TableName = "tbBillCheckList";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode]
            internal tbBillCheckListDataTable(DataTable table)
            {
                base.TableName = table.TableName;
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    base.CaseSensitive = table.CaseSensitive;
                }
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    base.Locale = table.Locale;
                }
                if (table.Namespace != table.DataSet.Namespace)
                {
                    base.Namespace = table.Namespace;
                }
                base.Prefix = table.Prefix;
                base.MinimumCapacity = table.MinimumCapacity;
            }

            [DebuggerNonUserCode]
            protected tbBillCheckListDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode]
            public void AddtbBillCheckListRow(myDS.tbBillCheckListRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode]
            public myDS.tbBillCheckListRow AddtbBillCheckListRow(string cMNo, string cMName, string cSpec, string cBatchNo, string cQCStatus, string fQty, string fDiff, string fBad, string fErpQty, string cUnit)
            {
                myDS.tbBillCheckListRow row = (myDS.tbBillCheckListRow) base.NewRow();
                row.ItemArray = new object[] { cMNo, cMName, cSpec, cBatchNo, cQCStatus, fQty, fDiff, fBad, fErpQty, cUnit };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode]
            public override DataTable Clone()
            {
                myDS.tbBillCheckListDataTable table = (myDS.tbBillCheckListDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode]
            protected override DataTable CreateInstance()
            {
                return new myDS.tbBillCheckListDataTable();
            }

            [DebuggerNonUserCode]
            public virtual IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            [DebuggerNonUserCode]
            protected override Type GetRowType()
            {
                return typeof(myDS.tbBillCheckListRow);
            }

            [DebuggerNonUserCode]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                myDS yds = new myDS();
                XmlSchemaAny item = new XmlSchemaAny {
                    Namespace = "http://www.w3.org/2001/XMLSchema",
                    MinOccurs = 0M,
                    MaxOccurs = 79228162514264337593543950335M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(item);
                XmlSchemaAny any2 = new XmlSchemaAny {
                    Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                    MinOccurs = 1M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(any2);
                XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                    Name = "namespace",
                    FixedValue = yds.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "tbBillCheckListDataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                XmlSchema schemaSerializable = yds.GetSchemaSerializable();
                if (xs.Contains(schemaSerializable.TargetNamespace))
                {
                    MemoryStream stream = new MemoryStream();
                    MemoryStream stream2 = new MemoryStream();
                    try
                    {
                        XmlSchema current = null;
                        schemaSerializable.Write(stream);
                        IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            current = (XmlSchema) enumerator.Current;
                            stream2.SetLength(0L);
                            current.Write(stream2);
                            if (stream.Length == stream2.Length)
                            {
                                stream.Position = 0L;
                                stream2.Position = 0L;
                                while ((stream.Position != stream.Length) && (stream.ReadByte() == stream2.ReadByte()))
                                {
                                }
                                if (stream.Position == stream.Length)
                                {
                                    return type;
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (stream != null)
                        {
                            stream.Close();
                        }
                        if (stream2 != null)
                        {
                            stream2.Close();
                        }
                    }
                }
                xs.Add(schemaSerializable);
                return type;
            }

            [DebuggerNonUserCode]
            private void InitClass()
            {
                this.columncMNo = new DataColumn("cMNo", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncMNo);
                this.columncMName = new DataColumn("cMName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncMName);
                this.columncSpec = new DataColumn("cSpec", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncSpec);
                this.columncBatchNo = new DataColumn("cBatchNo", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncBatchNo);
                this.columncQCStatus = new DataColumn("cQCStatus", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncQCStatus);
                this.columnfQty = new DataColumn("fQty", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnfQty);
                this.columnfDiff = new DataColumn("fDiff", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnfDiff);
                this.columnfBad = new DataColumn("fBad", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnfBad);
                this.columnfErpQty = new DataColumn("fErpQty", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnfErpQty);
                this.columncUnit = new DataColumn("cUnit", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncUnit);
                this.columncMNo.Caption = "cMno";
                this.columncMName.Caption = "fQty";
                this.columncSpec.Caption = "cMName";
                this.columncBatchNo.Caption = "cSpec";
                this.columncQCStatus.Caption = "cUnit";
                this.columnfQty.Caption = "cMatStyle";
                this.columnfDiff.Caption = "cUser";
                this.columnfBad.Caption = "dLastDate";
                this.columnfErpQty.Caption = "cRemark";
                this.columncUnit.Caption = "cBNoAjust";
            }

            [DebuggerNonUserCode]
            internal void InitVars()
            {
                this.columncMNo = base.Columns["cMNo"];
                this.columncMName = base.Columns["cMName"];
                this.columncSpec = base.Columns["cSpec"];
                this.columncBatchNo = base.Columns["cBatchNo"];
                this.columncQCStatus = base.Columns["cQCStatus"];
                this.columnfQty = base.Columns["fQty"];
                this.columnfDiff = base.Columns["fDiff"];
                this.columnfBad = base.Columns["fBad"];
                this.columnfErpQty = base.Columns["fErpQty"];
                this.columncUnit = base.Columns["cUnit"];
            }

            [DebuggerNonUserCode]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new myDS.tbBillCheckListRow(builder);
            }

            [DebuggerNonUserCode]
            public myDS.tbBillCheckListRow NewtbBillCheckListRow()
            {
                return (myDS.tbBillCheckListRow) base.NewRow();
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.tbBillCheckListRowChanged != null)
                {
                    this.tbBillCheckListRowChanged(this, new myDS.tbBillCheckListRowChangeEvent((myDS.tbBillCheckListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.tbBillCheckListRowChanging != null)
                {
                    this.tbBillCheckListRowChanging(this, new myDS.tbBillCheckListRowChangeEvent((myDS.tbBillCheckListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.tbBillCheckListRowDeleted != null)
                {
                    this.tbBillCheckListRowDeleted(this, new myDS.tbBillCheckListRowChangeEvent((myDS.tbBillCheckListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.tbBillCheckListRowDeleting != null)
                {
                    this.tbBillCheckListRowDeleting(this, new myDS.tbBillCheckListRowChangeEvent((myDS.tbBillCheckListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            public void RemovetbBillCheckListRow(myDS.tbBillCheckListRow row)
            {
                base.Rows.Remove(row);
            }

            [DebuggerNonUserCode]
            public DataColumn cBatchNoColumn
            {
                get
                {
                    return this.columncBatchNo;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cMNameColumn
            {
                get
                {
                    return this.columncMName;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cMNoColumn
            {
                get
                {
                    return this.columncMNo;
                }
            }

            [DebuggerNonUserCode, Browsable(false)]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cQCStatusColumn
            {
                get
                {
                    return this.columncQCStatus;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cSpecColumn
            {
                get
                {
                    return this.columncSpec;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cUnitColumn
            {
                get
                {
                    return this.columncUnit;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn fBadColumn
            {
                get
                {
                    return this.columnfBad;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn fDiffColumn
            {
                get
                {
                    return this.columnfDiff;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn fErpQtyColumn
            {
                get
                {
                    return this.columnfErpQty;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn fQtyColumn
            {
                get
                {
                    return this.columnfQty;
                }
            }

            [DebuggerNonUserCode]
            public myDS.tbBillCheckListRow this[int index]
            {
                get
                {
                    return (myDS.tbBillCheckListRow) base.Rows[index];
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class tbBillCheckListRow : DataRow
        {
            private myDS.tbBillCheckListDataTable tabletbBillCheckList;

            [DebuggerNonUserCode]
            internal tbBillCheckListRow(DataRowBuilder rb) : base(rb)
            {
                this.tabletbBillCheckList = (myDS.tbBillCheckListDataTable) base.Table;
            }

            [DebuggerNonUserCode]
            public bool IscBatchNoNull()
            {
                return base.IsNull(this.tabletbBillCheckList.cBatchNoColumn);
            }

            [DebuggerNonUserCode]
            public bool IscMNameNull()
            {
                return base.IsNull(this.tabletbBillCheckList.cMNameColumn);
            }

            [DebuggerNonUserCode]
            public bool IscMNoNull()
            {
                return base.IsNull(this.tabletbBillCheckList.cMNoColumn);
            }

            [DebuggerNonUserCode]
            public bool IscQCStatusNull()
            {
                return base.IsNull(this.tabletbBillCheckList.cQCStatusColumn);
            }

            [DebuggerNonUserCode]
            public bool IscSpecNull()
            {
                return base.IsNull(this.tabletbBillCheckList.cSpecColumn);
            }

            [DebuggerNonUserCode]
            public bool IscUnitNull()
            {
                return base.IsNull(this.tabletbBillCheckList.cUnitColumn);
            }

            [DebuggerNonUserCode]
            public bool IsfBadNull()
            {
                return base.IsNull(this.tabletbBillCheckList.fBadColumn);
            }

            [DebuggerNonUserCode]
            public bool IsfDiffNull()
            {
                return base.IsNull(this.tabletbBillCheckList.fDiffColumn);
            }

            [DebuggerNonUserCode]
            public bool IsfErpQtyNull()
            {
                return base.IsNull(this.tabletbBillCheckList.fErpQtyColumn);
            }

            [DebuggerNonUserCode]
            public bool IsfQtyNull()
            {
                return base.IsNull(this.tabletbBillCheckList.fQtyColumn);
            }

            [DebuggerNonUserCode]
            public void SetcBatchNoNull()
            {
                base[this.tabletbBillCheckList.cBatchNoColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcMNameNull()
            {
                base[this.tabletbBillCheckList.cMNameColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcMNoNull()
            {
                base[this.tabletbBillCheckList.cMNoColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcQCStatusNull()
            {
                base[this.tabletbBillCheckList.cQCStatusColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcSpecNull()
            {
                base[this.tabletbBillCheckList.cSpecColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcUnitNull()
            {
                base[this.tabletbBillCheckList.cUnitColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetfBadNull()
            {
                base[this.tabletbBillCheckList.fBadColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetfDiffNull()
            {
                base[this.tabletbBillCheckList.fDiffColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetfErpQtyNull()
            {
                base[this.tabletbBillCheckList.fErpQtyColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetfQtyNull()
            {
                base[this.tabletbBillCheckList.fQtyColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public string cBatchNo
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckList.cBatchNoColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckList”中列“cBatchNo”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckList.cBatchNoColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cMName
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckList.cMNameColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckList”中列“cMName”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckList.cMNameColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cMNo
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckList.cMNoColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckList”中列“cMNo”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckList.cMNoColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cQCStatus
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckList.cQCStatusColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckList”中列“cQCStatus”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckList.cQCStatusColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cSpec
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckList.cSpecColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckList”中列“cSpec”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckList.cSpecColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cUnit
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckList.cUnitColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckList”中列“cUnit”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckList.cUnitColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string fBad
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckList.fBadColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckList”中列“fBad”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckList.fBadColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string fDiff
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckList.fDiffColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckList”中列“fDiff”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckList.fDiffColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string fErpQty
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckList.fErpQtyColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckList”中列“fErpQty”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckList.fErpQtyColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string fQty
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheckList.fQtyColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheckList”中列“fQty”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheckList.fQtyColumn] = value;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class tbBillCheckListRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private myDS.tbBillCheckListRow eventRow;

            [DebuggerNonUserCode]
            public tbBillCheckListRowChangeEvent(myDS.tbBillCheckListRow row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [DebuggerNonUserCode]
            public DataRowAction Action
            {
                get
                {
                    return this.eventAction;
                }
            }

            [DebuggerNonUserCode]
            public myDS.tbBillCheckListRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void tbBillCheckListRowChangeEventHandler(object sender, myDS.tbBillCheckListRowChangeEvent e);

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class tbBillCheckRow : DataRow
        {
            private myDS.tbBillCheckDataTable tabletbBillCheck;

            [DebuggerNonUserCode]
            internal tbBillCheckRow(DataRowBuilder rb) : base(rb)
            {
                this.tabletbBillCheck = (myDS.tbBillCheckDataTable) base.Table;
            }

            [DebuggerNonUserCode]
            public bool IscBNoAjustNull()
            {
                return base.IsNull(this.tabletbBillCheck.cBNoAjustColumn);
            }

            [DebuggerNonUserCode]
            public bool IscBNoBadNull()
            {
                return base.IsNull(this.tabletbBillCheck.cBNoBadColumn);
            }

            [DebuggerNonUserCode]
            public bool IscBNoNull()
            {
                return base.IsNull(this.tabletbBillCheck.cBNoColumn);
            }

            [DebuggerNonUserCode]
            public bool IscBTypeNull()
            {
                return base.IsNull(this.tabletbBillCheck.cBTypeColumn);
            }

            [DebuggerNonUserCode]
            public bool IscIsCheckedNull()
            {
                return base.IsNull(this.tabletbBillCheck.cIsCheckedColumn);
            }

            [DebuggerNonUserCode]
            public bool IscIsFinishedNull()
            {
                return base.IsNull(this.tabletbBillCheck.cIsFinishedColumn);
            }

            [DebuggerNonUserCode]
            public bool IscRemarkNull()
            {
                return base.IsNull(this.tabletbBillCheck.cRemarkColumn);
            }

            [DebuggerNonUserCode]
            public bool IscUserNull()
            {
                return base.IsNull(this.tabletbBillCheck.cUserColumn);
            }

            [DebuggerNonUserCode]
            public bool IscWHIdNull()
            {
                return base.IsNull(this.tabletbBillCheck.cWHIdColumn);
            }

            [DebuggerNonUserCode]
            public bool IsdDateNull()
            {
                return base.IsNull(this.tabletbBillCheck.dDateColumn);
            }

            [DebuggerNonUserCode]
            public void SetcBNoAjustNull()
            {
                base[this.tabletbBillCheck.cBNoAjustColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcBNoBadNull()
            {
                base[this.tabletbBillCheck.cBNoBadColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcBNoNull()
            {
                base[this.tabletbBillCheck.cBNoColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcBTypeNull()
            {
                base[this.tabletbBillCheck.cBTypeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcIsCheckedNull()
            {
                base[this.tabletbBillCheck.cIsCheckedColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcIsFinishedNull()
            {
                base[this.tabletbBillCheck.cIsFinishedColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcRemarkNull()
            {
                base[this.tabletbBillCheck.cRemarkColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcUserNull()
            {
                base[this.tabletbBillCheck.cUserColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcWHIdNull()
            {
                base[this.tabletbBillCheck.cWHIdColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetdDateNull()
            {
                base[this.tabletbBillCheck.dDateColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public string cBNo
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheck.cBNoColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheck”中列“cBNo”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheck.cBNoColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cBNoAjust
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheck.cBNoAjustColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheck”中列“cBNoAjust”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheck.cBNoAjustColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cBNoBad
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheck.cBNoBadColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheck”中列“cBNoBad”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheck.cBNoBadColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cBType
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheck.cBTypeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheck”中列“cBType”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheck.cBTypeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cIsChecked
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheck.cIsCheckedColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheck”中列“cIsChecked”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheck.cIsCheckedColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cIsFinished
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheck.cIsFinishedColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheck”中列“cIsFinished”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheck.cIsFinishedColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cRemark
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheck.cRemarkColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheck”中列“cRemark”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheck.cRemarkColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cUser
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheck.cUserColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheck”中列“cUser”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheck.cUserColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cWHId
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheck.cWHIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheck”中列“cWHId”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheck.cWHIdColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string dDate
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbBillCheck.dDateColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbBillCheck”中列“dDate”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbBillCheck.dDateColumn] = value;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class tbBillCheckRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private myDS.tbBillCheckRow eventRow;

            [DebuggerNonUserCode]
            public tbBillCheckRowChangeEvent(myDS.tbBillCheckRow row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [DebuggerNonUserCode]
            public DataRowAction Action
            {
                get
                {
                    return this.eventAction;
                }
            }

            [DebuggerNonUserCode]
            public myDS.tbBillCheckRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void tbBillCheckRowChangeEventHandler(object sender, myDS.tbBillCheckRowChangeEvent e);
    }
}

