using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using EMS.CoreLibrary.Helpers;

namespace EMS.Framework.Utils
{
    public static class CopyUtil
    {
        /// <summary>
        /// Generally use to Copy New Object's property to DbContext object
        /// </summary>
        /// <param name="source">New Object to copy</param>
        /// <param name="destination">destination object</param>
        /// <param name="ignores"></param>
        public static void Copy(object source, object destination, params string[] ignores)
        {
            if (source == null || destination == null)
                return;

            //var properties = source.GetType().GetProperties();
            //foreach (PropertyInfo propertyInfo in properties)
            //{
            //    if (propertyInfo.PropertyType.IsClass 
            //        || propertyInfo.PropertyType.IsGenericType)
            //        continue;

            //}

            PropertyDescriptorCollection sourceProps = TypeDescriptor.GetProperties(source),
                destProps = TypeDescriptor.GetProperties(destination);
            foreach (PropertyDescriptor prop in sourceProps)
            {
                if (prop.PropertyType.Name != "String" && !prop.PropertyType.IsValueType)
                    continue;

                //if (prop.PropertyType.IsClass
                //    || prop.PropertyType.IsGenericType)
                //    continue;

                var defaultIgnores = new[] { "Id", "CreateById","CreateDate", "LastChanged", "LastChangedById" };
                if (defaultIgnores.Contains(prop.Name))
                    continue;

                PropertyDescriptor destProp = destProps[prop.Name];
                if (destProp == null)
                    continue;
                //var a = prop.GetType().IsClass;
                if (ignores.Contains(prop.Name))
                    continue;

                var value = prop.GetValue(source);
                //if (value == null)
                //    continue;

                destProp.SetValue(destination, value);
            }
        }

        /// <summary>
        /// Only copy column name exists in "columnsToCopy".
        /// This function will not copy  "Id", "CreateById", "CreateDate", "LastChanged" and "LastChangedById" columns.
        /// This function can't copy nested object.
        /// </summary>
        /// <param name="source">New Object to copy</param>
        /// <param name="destination">destination object</param>
        /// <param name="columnsToCopy">keep it empty to include all columns</param>
        public static void CopySelectedColumns(object source, object destination, params string[] columnsToCopy)
        {
            if (source == null || destination == null)
                return;
           
            columnsToCopy = columnsToCopy.Where(s => s.IsValid()).ToArray();

           PropertyDescriptorCollection sourceProps = TypeDescriptor.GetProperties(source),
                destProps = TypeDescriptor.GetProperties(destination);
            foreach (PropertyDescriptor prop in sourceProps)
            {
                if (prop.PropertyType.Name != "String" && !prop.PropertyType.IsValueType)
                    continue;

                //if (prop.PropertyType.IsClass
                //    || prop.PropertyType.IsGenericType)
                //    continue;

                var defaultIgnores = new[] { "Id", "CreateById", "CreateDate", "LastChanged", "LastChangedById" };
                if (defaultIgnores.Contains(prop.Name))
                    continue;

                PropertyDescriptor destProp = destProps[prop.Name];
                if (destProp == null)
                    continue;
                //var a = prop.GetType().IsClass;
                if (columnsToCopy.Length>0 && !columnsToCopy.Contains(prop.Name))
                    continue;

                var value = prop.GetValue(source);
                //if (value == null)
                //    continue;

                destProp.SetValue(destination, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void CopyBack(object source, object destination)
        {
            if (source == null || destination == null)
                return;

            //var properties = source.GetType().GetProperties();
            //foreach (PropertyInfo propertyInfo in properties)
            //{
            //    if (propertyInfo.PropertyType.IsClass 
            //        || propertyInfo.PropertyType.IsGenericType)
            //        continue;

            //}

            PropertyDescriptorCollection sourceProps = TypeDescriptor.GetProperties(source),
                destProps = TypeDescriptor.GetProperties(destination);
            foreach (PropertyDescriptor prop in sourceProps)
            {
                //if (prop.PropertyType.Name != "String" && !prop.PropertyType.IsValueType)
                //    continue;

                //if (prop.PropertyType.IsClass
                //    || prop.PropertyType.IsGenericType)
                //    continue;

                //var defaultIgnores = new[] { "ID", "LastChanged", "LastChangedBy" };
                //if (defaultIgnores.Contains(prop.Name))
                //    continue;

                PropertyDescriptor destProp = destProps[prop.Name];
                if (destProp == null)
                    continue;
                //var a = prop.GetType().IsClass;
                //if (ignores.Contains(prop.Name))
                //    continue;

                var value = prop.GetValue(source);
                //if (value == null)
                //    continue;

                destProp.SetValue(destination, value);
            }
        }

        public static void CopyExceptDateTime(object source, object destination)
        {
            if (source == null || destination == null)
                return;

            //var properties = source.GetType().GetProperties();
            //foreach (PropertyInfo propertyInfo in properties)
            //{
            //    if (propertyInfo.PropertyType.IsClass 
            //        || propertyInfo.PropertyType.IsGenericType)
            //        continue;

            //}

            PropertyDescriptorCollection sourceProps = TypeDescriptor.GetProperties(source),
                destProps = TypeDescriptor.GetProperties(destination);
            foreach (PropertyDescriptor prop in sourceProps)
            {
                if (prop.PropertyType.Name == "DateTime")
                    continue;

                //if (prop.PropertyType.IsClass
                //    || prop.PropertyType.IsGenericType)
                //    continue;

                //var defaultIgnores = new[] { "ID", "LastChanged", "LastChangedBy" };
                //if (defaultIgnores.Contains(prop.Name))
                //    continue;

                PropertyDescriptor destProp = destProps[prop.Name];
                if (destProp == null)
                    continue;
                //var a = prop.GetType().IsClass;
                //if (ignores.Contains(prop.Name))
                //    continue;

                var value = prop.GetValue(source);
                //if (value == null)
                //    continue;

                destProp.SetValue(destination, value);
            }
        }
        public static object DeepClone(this object source)
        {
            Type sourceType = source.GetType();

            object clone = Activator.CreateInstance(sourceType);

            FieldInfo[] fieldInfos =
                clone.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            for (int index = 0; index < fieldInfos.Length; index++)
            {
                var fieldInfo = fieldInfos[index];

                // check if field type is value or referenced
                if (fieldInfo.FieldType.IsValueType)
                    fieldInfo.SetValue(clone, fieldInfo.GetValue(source));  // assign value to field
                else
                    fieldInfo.SetValue(clone, (fieldInfo.GetValue(source).DeepClone())); // assign clone value of the referenced type
            }

            return clone;
        }
    }
}
