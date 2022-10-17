using System;
using System.Reflection;

namespace SinanErmis.SimplePooling
{
    public class CSharpObjectPool<T> : BasePool<T> where T : class, new()
    {
        private bool _copy;
        
        /// <summary>
        /// Parameterless constructor calls parameterless constructor of T for creation
        /// </summary>
        public CSharpObjectPool() : base(new T())
        {
            _copy = false;
        }

        /// <summary>
        /// Setting original enables deep-copying for creating new objects
        /// </summary>
        /// <param name="original">Object to deep copy for creating new instances from.</param>
        public CSharpObjectPool(T original) : base(original)
        {
            _copy = true;
        }

        protected override T CreateObject()
        {
            if (_copy)
            {
                return CloneObject(Original) as T;
            }
            return new T();
        }
        
        //Source: https://www.c-sharpcorner.com/UploadFile/ff2f08/deep-copy-of-object-in-C-Sharp/
        private static object CloneObject(object objSource)
        {
            //step : 1 Get the type of source object and create a new instance of that type
            Type typeSource = objSource.GetType();
            object objTarget = Activator.CreateInstance(typeSource);
            //Step2 : Get all the properties of source object type
            PropertyInfo[] propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            //Step : 3 Assign all source property to taget object 's properties
            foreach (PropertyInfo property in propertyInfo)
            {
                //Check whether property can be written to
                if (property.CanWrite)
                {
                    //Step : 4 check whether property type is value type, enum or string type
                    if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(System.String)))
                    {
                        property.SetValue(objTarget, property.GetValue(objSource, null), null);
                    }
                    //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
                    else
                    {
                        object objPropertyValue = property.GetValue(objSource, null);
                        if (objPropertyValue == null)
                        {
                            property.SetValue(objTarget, null, null);
                        }
                        else
                        {
                            property.SetValue(objTarget, CloneObject(objPropertyValue), null);
                        }
                    }
                }
            }
            return objTarget;
        }
    }
}