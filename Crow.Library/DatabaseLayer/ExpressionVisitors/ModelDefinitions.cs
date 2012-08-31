using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Common;

namespace Crow.Library.DatabaseLayer.ExpressionVisitors
{
    public class ModelDefinition
    {
        public ModelDefinition()
        {
            this.FieldDefinitions = new List<FieldDefinition>();
        }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Schema { get; set; }

        public bool IsInSchema { get { return this.Schema != null; } }

        public string ModelName
        {
            get { return this.Alias ?? this.Name; }
        }

        public Type ModelType { get; set; }

        public string SqlSelectAllFromTable { get; set; }

        public FieldDefinition PrimaryKey
        {
            get
            {
                return this.FieldDefinitions.First(x => x.IsPrimaryKey);
            }
        }

        public List<FieldDefinition> FieldDefinitions { get; set; }

        private FieldDefinition[] fieldDefinitionsArray;
        public FieldDefinition[] FieldDefinitionsArray
        {
            get
            {
                if (fieldDefinitionsArray == null)
                {
                    fieldDefinitionsArray = FieldDefinitions.ToArray();
                }
                return fieldDefinitionsArray;
            }
        }
    }


    public static class ModelDefinition<T>
    {
        private static TableInfo definition;
        public static TableInfo Definition
        {
            get { return definition ?? (definition = typeof(T).GetModelDefinition()); }
        }

        private static string primaryKeyName;
        public static string PrimaryKeyName
        {
            get { return primaryKeyName ?? (primaryKeyName = Definition.PrimaryKey); }
        }

    }
}
