namespace WarehouseTask.Rpts
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

    [Serializable, XmlRoot("RptData"), ToolboxItem(true), XmlSchemaProvider("GetTypedDataSetSchema"), HelpKeyword("vs.data.DataSet"), GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), DesignerCategory("code")]
    public class RptData : DataSet
    {
        private System.Data.SchemaSerializationMode _schemaSerializationMode;
        private tbTaskDtlListDataTable tabletbTaskDtlList;

        [DebuggerNonUserCode]
        public RptData()
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
        protected RptData(SerializationInfo info, StreamingContext context) : base(info, context, false)
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
                    if (dataSet.Tables["tbTaskDtlList"] != null)
                    {
                        base.Tables.Add(new tbTaskDtlListDataTable(dataSet.Tables["tbTaskDtlList"]));
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
            RptData data = (RptData) base.Clone();
            data.InitVars();
            data.SchemaSerializationMode = this.SchemaSerializationMode;
            return data;
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
            RptData data = new RptData();
            XmlSchemaComplexType type = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            XmlSchemaAny item = new XmlSchemaAny {
                Namespace = data.Namespace
            };
            sequence.Items.Add(item);
            type.Particle = sequence;
            XmlSchema schemaSerializable = data.GetSchemaSerializable();
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
            base.DataSetName = "RptData";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/RptData.xsd";
            base.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tabletbTaskDtlList = new tbTaskDtlListDataTable();
            base.Tables.Add(this.tabletbTaskDtlList);
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
            this.tabletbTaskDtlList = (tbTaskDtlListDataTable) base.Tables["tbTaskDtlList"];
            if (initTable && (this.tabletbTaskDtlList != null))
            {
                this.tabletbTaskDtlList.InitVars();
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
                if (dataSet.Tables["tbTaskDtlList"] != null)
                {
                    base.Tables.Add(new tbTaskDtlListDataTable(dataSet.Tables["tbTaskDtlList"]));
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
        protected override bool ShouldSerializeRelations()
        {
            return false;
        }

        [DebuggerNonUserCode]
        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        [DebuggerNonUserCode]
        private bool ShouldSerializetbTaskDtlList()
        {
            return false;
        }

        [DebuggerNonUserCode, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataRelationCollection Relations
        {
            get
            {
                return base.Relations;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true), DebuggerNonUserCode]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DebuggerNonUserCode]
        public DataTableCollection Tables
        {
            get
            {
                return base.Tables;
            }
        }

        [DebuggerNonUserCode, DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public tbTaskDtlListDataTable tbTaskDtlList
        {
            get
            {
                return this.tabletbTaskDtlList;
            }
        }

        [Serializable, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
        public class tbTaskDtlListDataTable : DataTable, IEnumerable
        {
            private DataColumn columncBatchNo;
            private DataColumn columncBNo;
            private DataColumn columncBNoIn;
            private DataColumn columncMNo;
            private DataColumn columncName;
            private DataColumn columncOptDesc;
            private DataColumn columncPosIdFrom;
            private DataColumn columncSpec;
            private DataColumn columncUnit;
            private DataColumn columncWHId;
            private DataColumn columncWKStatusDesc;
            private DataColumn columndOptDate;
            private DataColumn columnfQty;
            private DataColumn columnnItem;
            private DataColumn columnnItemIn;
            private DataColumn columnnOptStation;
            private DataColumn columnnPalletId;
            private DataColumn columnnWorkId;

            public event RptData.tbTaskDtlListRowChangeEventHandler tbTaskDtlListRowChanged;

            public event RptData.tbTaskDtlListRowChangeEventHandler tbTaskDtlListRowChanging;

            public event RptData.tbTaskDtlListRowChangeEventHandler tbTaskDtlListRowDeleted;

            public event RptData.tbTaskDtlListRowChangeEventHandler tbTaskDtlListRowDeleting;

            [DebuggerNonUserCode]
            public tbTaskDtlListDataTable()
            {
                base.TableName = "tbTaskDtlList";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode]
            internal tbTaskDtlListDataTable(DataTable table)
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
            protected tbTaskDtlListDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode]
            public void AddtbTaskDtlListRow(RptData.tbTaskDtlListRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode]
            public RptData.tbTaskDtlListRow AddtbTaskDtlListRow(string cWHId, string cPosIdFrom, string nPalletId, string cBNo, string cOptDesc, string nItem, string cMNo, string cName, string cSpec, string cBatchNo, double fQty, string cUnit, DateTime dOptDate, string cBNoIn, string nItemIn, int nOptStation, string cWKStatusDesc, string nWorkId)
            {
                RptData.tbTaskDtlListRow row = (RptData.tbTaskDtlListRow) base.NewRow();
                row.ItemArray = new object[] { 
                    cWHId, cPosIdFrom, nPalletId, cBNo, cOptDesc, nItem, cMNo, cName, cSpec, cBatchNo, fQty, cUnit, dOptDate, cBNoIn, nItemIn, nOptStation, 
                    cWKStatusDesc, nWorkId
                 };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode]
            public override DataTable Clone()
            {
                RptData.tbTaskDtlListDataTable table = (RptData.tbTaskDtlListDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode]
            protected override DataTable CreateInstance()
            {
                return new RptData.tbTaskDtlListDataTable();
            }

            [DebuggerNonUserCode]
            public virtual IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            [DebuggerNonUserCode]
            protected override Type GetRowType()
            {
                return typeof(RptData.tbTaskDtlListRow);
            }

            [DebuggerNonUserCode]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                RptData data = new RptData();
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
                    FixedValue = data.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "tbTaskDtlListDataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                XmlSchema schemaSerializable = data.GetSchemaSerializable();
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
                this.columncWHId = new DataColumn("cWHId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncWHId);
                this.columncPosIdFrom = new DataColumn("cPosIdFrom", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncPosIdFrom);
                this.columnnPalletId = new DataColumn("nPalletId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnnPalletId);
                this.columncBNo = new DataColumn("cBNo", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncBNo);
                this.columncOptDesc = new DataColumn("cOptDesc", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncOptDesc);
                this.columnnItem = new DataColumn("nItem", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnnItem);
                this.columncMNo = new DataColumn("cMNo", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncMNo);
                this.columncName = new DataColumn("cName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncName);
                this.columncSpec = new DataColumn("cSpec", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncSpec);
                this.columncBatchNo = new DataColumn("cBatchNo", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncBatchNo);
                this.columnfQty = new DataColumn("fQty", typeof(double), null, MappingType.Element);
                base.Columns.Add(this.columnfQty);
                this.columncUnit = new DataColumn("cUnit", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncUnit);
                this.columndOptDate = new DataColumn("dOptDate", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columndOptDate);
                this.columncBNoIn = new DataColumn("cBNoIn", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncBNoIn);
                this.columnnItemIn = new DataColumn("nItemIn", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnnItemIn);
                this.columnnOptStation = new DataColumn("nOptStation", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnnOptStation);
                this.columncWKStatusDesc = new DataColumn("cWKStatusDesc", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columncWKStatusDesc);
                this.columnnWorkId = new DataColumn("nWorkId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnnWorkId);
            }

            [DebuggerNonUserCode]
            internal void InitVars()
            {
                this.columncWHId = base.Columns["cWHId"];
                this.columncPosIdFrom = base.Columns["cPosIdFrom"];
                this.columnnPalletId = base.Columns["nPalletId"];
                this.columncBNo = base.Columns["cBNo"];
                this.columncOptDesc = base.Columns["cOptDesc"];
                this.columnnItem = base.Columns["nItem"];
                this.columncMNo = base.Columns["cMNo"];
                this.columncName = base.Columns["cName"];
                this.columncSpec = base.Columns["cSpec"];
                this.columncBatchNo = base.Columns["cBatchNo"];
                this.columnfQty = base.Columns["fQty"];
                this.columncUnit = base.Columns["cUnit"];
                this.columndOptDate = base.Columns["dOptDate"];
                this.columncBNoIn = base.Columns["cBNoIn"];
                this.columnnItemIn = base.Columns["nItemIn"];
                this.columnnOptStation = base.Columns["nOptStation"];
                this.columncWKStatusDesc = base.Columns["cWKStatusDesc"];
                this.columnnWorkId = base.Columns["nWorkId"];
            }

            [DebuggerNonUserCode]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new RptData.tbTaskDtlListRow(builder);
            }

            [DebuggerNonUserCode]
            public RptData.tbTaskDtlListRow NewtbTaskDtlListRow()
            {
                return (RptData.tbTaskDtlListRow) base.NewRow();
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.tbTaskDtlListRowChanged != null)
                {
                    this.tbTaskDtlListRowChanged(this, new RptData.tbTaskDtlListRowChangeEvent((RptData.tbTaskDtlListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.tbTaskDtlListRowChanging != null)
                {
                    this.tbTaskDtlListRowChanging(this, new RptData.tbTaskDtlListRowChangeEvent((RptData.tbTaskDtlListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.tbTaskDtlListRowDeleted != null)
                {
                    this.tbTaskDtlListRowDeleted(this, new RptData.tbTaskDtlListRowChangeEvent((RptData.tbTaskDtlListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.tbTaskDtlListRowDeleting != null)
                {
                    this.tbTaskDtlListRowDeleting(this, new RptData.tbTaskDtlListRowChangeEvent((RptData.tbTaskDtlListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            public void RemovetbTaskDtlListRow(RptData.tbTaskDtlListRow row)
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
            public DataColumn cBNoColumn
            {
                get
                {
                    return this.columncBNo;
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
            public DataColumn cMNoColumn
            {
                get
                {
                    return this.columncMNo;
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

            [DebuggerNonUserCode]
            public DataColumn cOptDescColumn
            {
                get
                {
                    return this.columncOptDesc;
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
            public DataColumn cPosIdFromColumn
            {
                get
                {
                    return this.columncPosIdFrom;
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
            public DataColumn cWHIdColumn
            {
                get
                {
                    return this.columncWHId;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn cWKStatusDescColumn
            {
                get
                {
                    return this.columncWKStatusDesc;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn dOptDateColumn
            {
                get
                {
                    return this.columndOptDate;
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
            public RptData.tbTaskDtlListRow this[int index]
            {
                get
                {
                    return (RptData.tbTaskDtlListRow) base.Rows[index];
                }
            }

            [DebuggerNonUserCode]
            public DataColumn nItemColumn
            {
                get
                {
                    return this.columnnItem;
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
            public DataColumn nOptStationColumn
            {
                get
                {
                    return this.columnnOptStation;
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

            [DebuggerNonUserCode]
            public DataColumn nWorkIdColumn
            {
                get
                {
                    return this.columnnWorkId;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class tbTaskDtlListRow : DataRow
        {
            private RptData.tbTaskDtlListDataTable tabletbTaskDtlList;

            [DebuggerNonUserCode]
            internal tbTaskDtlListRow(DataRowBuilder rb) : base(rb)
            {
                this.tabletbTaskDtlList = (RptData.tbTaskDtlListDataTable) base.Table;
            }

            [DebuggerNonUserCode]
            public bool IscBatchNoNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.cBatchNoColumn);
            }

            [DebuggerNonUserCode]
            public bool IscBNoInNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.cBNoInColumn);
            }

            [DebuggerNonUserCode]
            public bool IscBNoNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.cBNoColumn);
            }

            [DebuggerNonUserCode]
            public bool IscMNoNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.cMNoColumn);
            }

            [DebuggerNonUserCode]
            public bool IscNameNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.cNameColumn);
            }

            [DebuggerNonUserCode]
            public bool IscOptDescNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.cOptDescColumn);
            }

            [DebuggerNonUserCode]
            public bool IscPosIdFromNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.cPosIdFromColumn);
            }

            [DebuggerNonUserCode]
            public bool IscSpecNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.cSpecColumn);
            }

            [DebuggerNonUserCode]
            public bool IscUnitNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.cUnitColumn);
            }

            [DebuggerNonUserCode]
            public bool IscWHIdNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.cWHIdColumn);
            }

            [DebuggerNonUserCode]
            public bool IscWKStatusDescNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.cWKStatusDescColumn);
            }

            [DebuggerNonUserCode]
            public bool IsdOptDateNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.dOptDateColumn);
            }

            [DebuggerNonUserCode]
            public bool IsfQtyNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.fQtyColumn);
            }

            [DebuggerNonUserCode]
            public bool IsnItemInNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.nItemInColumn);
            }

            [DebuggerNonUserCode]
            public bool IsnItemNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.nItemColumn);
            }

            [DebuggerNonUserCode]
            public bool IsnOptStationNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.nOptStationColumn);
            }

            [DebuggerNonUserCode]
            public bool IsnPalletIdNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.nPalletIdColumn);
            }

            [DebuggerNonUserCode]
            public bool IsnWorkIdNull()
            {
                return base.IsNull(this.tabletbTaskDtlList.nWorkIdColumn);
            }

            [DebuggerNonUserCode]
            public void SetcBatchNoNull()
            {
                base[this.tabletbTaskDtlList.cBatchNoColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcBNoInNull()
            {
                base[this.tabletbTaskDtlList.cBNoInColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcBNoNull()
            {
                base[this.tabletbTaskDtlList.cBNoColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcMNoNull()
            {
                base[this.tabletbTaskDtlList.cMNoColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcNameNull()
            {
                base[this.tabletbTaskDtlList.cNameColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcOptDescNull()
            {
                base[this.tabletbTaskDtlList.cOptDescColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcPosIdFromNull()
            {
                base[this.tabletbTaskDtlList.cPosIdFromColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcSpecNull()
            {
                base[this.tabletbTaskDtlList.cSpecColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcUnitNull()
            {
                base[this.tabletbTaskDtlList.cUnitColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcWHIdNull()
            {
                base[this.tabletbTaskDtlList.cWHIdColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetcWKStatusDescNull()
            {
                base[this.tabletbTaskDtlList.cWKStatusDescColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetdOptDateNull()
            {
                base[this.tabletbTaskDtlList.dOptDateColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetfQtyNull()
            {
                base[this.tabletbTaskDtlList.fQtyColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetnItemInNull()
            {
                base[this.tabletbTaskDtlList.nItemInColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetnItemNull()
            {
                base[this.tabletbTaskDtlList.nItemColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetnOptStationNull()
            {
                base[this.tabletbTaskDtlList.nOptStationColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetnPalletIdNull()
            {
                base[this.tabletbTaskDtlList.nPalletIdColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetnWorkIdNull()
            {
                base[this.tabletbTaskDtlList.nWorkIdColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public string cBatchNo
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbTaskDtlList.cBatchNoColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“cBatchNo”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.cBatchNoColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cBNo
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbTaskDtlList.cBNoColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“cBNo”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.cBNoColumn] = value;
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
                        str = (string) base[this.tabletbTaskDtlList.cBNoInColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“cBNoIn”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.cBNoInColumn] = value;
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
                        str = (string) base[this.tabletbTaskDtlList.cMNoColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“cMNo”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.cMNoColumn] = value;
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
                        str = (string) base[this.tabletbTaskDtlList.cNameColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“cName”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.cNameColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cOptDesc
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbTaskDtlList.cOptDescColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“cOptDesc”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.cOptDescColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cPosIdFrom
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbTaskDtlList.cPosIdFromColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“cPosIdFrom”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.cPosIdFromColumn] = value;
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
                        str = (string) base[this.tabletbTaskDtlList.cSpecColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“cSpec”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.cSpecColumn] = value;
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
                        str = (string) base[this.tabletbTaskDtlList.cUnitColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“cUnit”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.cUnitColumn] = value;
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
                        str = (string) base[this.tabletbTaskDtlList.cWHIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“cWHId”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.cWHIdColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string cWKStatusDesc
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbTaskDtlList.cWKStatusDescColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“cWKStatusDesc”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.cWKStatusDescColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public DateTime dOptDate
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tabletbTaskDtlList.dOptDateColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“dOptDate”的值为 DBNull。", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tabletbTaskDtlList.dOptDateColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public double fQty
            {
                get
                {
                    double num;
                    try
                    {
                        num = (double) base[this.tabletbTaskDtlList.fQtyColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“fQty”的值为 DBNull。", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tabletbTaskDtlList.fQtyColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string nItem
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbTaskDtlList.nItemColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“nItem”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.nItemColumn] = value;
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
                        str = (string) base[this.tabletbTaskDtlList.nItemInColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“nItemIn”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.nItemInColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public int nOptStation
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tabletbTaskDtlList.nOptStationColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“nOptStation”的值为 DBNull。", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tabletbTaskDtlList.nOptStationColumn] = value;
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
                        str = (string) base[this.tabletbTaskDtlList.nPalletIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“nPalletId”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.nPalletIdColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string nWorkId
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tabletbTaskDtlList.nWorkIdColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“tbTaskDtlList”中列“nWorkId”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tabletbTaskDtlList.nWorkIdColumn] = value;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class tbTaskDtlListRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private RptData.tbTaskDtlListRow eventRow;

            [DebuggerNonUserCode]
            public tbTaskDtlListRowChangeEvent(RptData.tbTaskDtlListRow row, DataRowAction action)
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
            public RptData.tbTaskDtlListRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void tbTaskDtlListRowChangeEventHandler(object sender, RptData.tbTaskDtlListRowChangeEvent e);
    }
}

