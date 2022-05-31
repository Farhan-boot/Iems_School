using System;
using System.Collections.Generic;
using System.Linq;


namespace EMS.CoreLibrary.Helpers
{

    public static class EnumUtil
    {
        public class EnumInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Type EnumType { get; set; }

        }

        //public static EnumInfo GetEnum(int i, string typeName )
        //{
        //    Type type = ReflectionHelper.GetType(typeName);
        //    if (type == null) return null;
        //    return GetEnum(i, type);
        //}

        public static EnumInfo GetEnum(int i, Type eType)
        {
  
            return new EnumInfo()
            {
                EnumType = eType,
                Id = i,
                Name = GetEnumName(eType, i)
            };
        }
        //public static EnumInfo GetEnum(object i, string typeName)
        //{
        //    Type type = ReflectionHelper.GetType(typeName);
        //    if (type == null) return null;
        //    return GetEnum(i, type);
        //}

        public static EnumInfo GetEnum(object i, Type eType)
        {
            return new EnumInfo()
            {
                //EnumType = eType,
                Id = Convert.ToInt32(i),
                Name = GetEnumName(eType, i)
            };
        }
        //public static IList<EnumInfo> GetEnumList( string typeName)
        //{
        //    Type type = ReflectionHelper.GetType(typeName);
        //    if (type == null) return null;
        //    return GetEnumList( type);
        //}
        public static IList<EnumInfo> GetEnumList(Type enumType)
        {
            var enums = new List<EnumInfo>();

            if (!enumType.IsEnum)
                return enums;

            if (enumType == typeof(EnumCollection.Common.BloodGroupEnum))
            {
                enums.AddRange(from object v in Enum.GetValues(enumType)
                               select new EnumInfo()
                               {
                                   //EnumType = enumType,
                                   Id = (int)v,
                                   Name = GetEnumName(enumType, v).AddBloodGroupSignToSentence()
                               });
            }
            else if (enumType == typeof(EnumCollection.AdmissionUG.EducationTypeEnum))
            {
                enums.AddRange(from object v in Enum.GetValues(enumType)
                               select new EnumInfo()
                               {
                                   //EnumType = enumType,
                                   Id = (int)v,
                                   Name = GetEnumName(enumType, v).ToUpper()
                               });
            }
            else if (enumType == typeof(EnumCollection.AdmissionUG.FormUnitEnum))
            {
                enums.AddRange(from object v in Enum.GetValues(enumType)
                               select new EnumInfo()
                               {
                                   //EnumType = enumType,
                                   Id = (int)v,
                                   Name = GetEnumName(enumType, v)
                               });
            }
            else
            {
                enums.AddRange(from object v in Enum.GetValues(enumType)
                               select new EnumInfo()
                               {
                                   //EnumType = enumType,
                                   Id = (int)v,
                                   Name = GetEnumName(enumType, v)
                               });

            }

            return enums;
        }
        public static IList<EnumInfo> GetEnumToUpperList(Type enumType)
        {
            var enums = new List<EnumInfo>();

            if (!enumType.IsEnum)
                return enums;

            enums.AddRange(from object v in Enum.GetValues(enumType)
                           select new EnumInfo()
                           {
                               //EnumType = enumType,
                               Id = (int)v,
                               Name = GetEnumName(enumType, v).ToUpper()
                           });

            return enums;
        }

        public static string GetEnumName(Type type,Object enumObj)
        {
            var name = Enum.GetName(type, enumObj).AddSpacesToSentence();
            name = name.Replace(" Or ", "/").Replace(" Orr ", " or ").Replace(" And ", " & ").Replace(" _ ", " ").Replace("_ ","");
            return name;
        }

     


    }
}