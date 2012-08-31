using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace Crow.Library.Common
{
    public static class ObjectInitializerFactory
    {
        public class TypeIsNotAnInterface : Exception
        {
            internal TypeIsNotAnInterface(Type type)
                : base("Type should be an interface in order to build. Parameter: " + type.FullName)
            { }
        }

        internal static readonly string DynamicObjectAssemblyName = "GrailDynamicObjectFactory";
        internal static readonly string DynamicObjectModuleName = "InterfaceObjectFactoryModule";
        internal static readonly string DynamicObjectDllName = "InterfaceObjectFactory.dll";

        private static ModuleBuilder m_ModuleBuilder;
        private static Dictionary<Type, Type> m_InterfaceImplementations = new Dictionary<Type, Type>();

        public static TObjectType InitializeClassForType<TObjectType>()
        {
            Type type = typeof(TObjectType);
            bool contains = m_InterfaceImplementations.ContainsKey(type);
            if (!contains)
            {
                CreateTypeForType(type);
            }
            return (TObjectType)Activator.CreateInstance(m_InterfaceImplementations[type]);
        }

        static ObjectInitializerFactory()
        {
            AppDomain appDomain = Thread.GetDomain();
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = DynamicObjectAssemblyName;

            AssemblyBuilder assemblyBuilder = appDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);

            m_ModuleBuilder = assemblyBuilder.DefineDynamicModule(DynamicObjectModuleName, DynamicObjectDllName, true);
        }

        private static void CreateTypeForType(Type type)
        {
            if (!type.IsInterface)
            {
                throw new TypeIsNotAnInterface(type);
            }

            TypeBuilder typeBuilder = m_ModuleBuilder.DefineType("Grail__" + type.Name, TypeAttributes.Class | TypeAttributes.Public);
            typeBuilder.AddInterfaceImplementation(type);

            //Constructor
            ConstructorInfo baseConstructorInfo = typeof(object).GetConstructor(new Type[0]);

            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(
                           MethodAttributes.Public,
                           CallingConventions.Standard,
                           Type.EmptyTypes);

            ILGenerator ilGenerator = constructorBuilder.GetILGenerator();
            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Call, baseConstructorInfo);
            ilGenerator.Emit(OpCodes.Ret);

            //Collect Member
            List<MethodInfo> methods = new List<MethodInfo>();
            AddMethodsToList(methods, type);

            List<PropertyInfo> properties = new List<PropertyInfo>();
            AddPropertiesToList(properties, type);

            //Create Property
            foreach (PropertyInfo pi in properties)
            {
                //TODO: not working efficiently..
                string piName = pi.Name;
                Type propertyType = pi.PropertyType;

                FieldBuilder field = typeBuilder.DefineField("_" + piName, propertyType, FieldAttributes.Private);

                MethodInfo getMethod = pi.GetGetMethod();
                if (null != getMethod)
                {
                    methods.Remove(getMethod);

                    MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                        getMethod.Name,
                        MethodAttributes.Public | MethodAttributes.Virtual,
                        propertyType,
                        Type.EmptyTypes);

                    ilGenerator = methodBuilder.GetILGenerator();

                    ilGenerator.Emit(OpCodes.Ldarg_0);
                    ilGenerator.Emit(OpCodes.Ldfld, field);
                    ilGenerator.Emit(OpCodes.Ret);

                    typeBuilder.DefineMethodOverride(methodBuilder, getMethod);
                }

                MethodInfo setMethod = pi.GetSetMethod();
                if (null != setMethod)
                {
                    methods.Remove(setMethod);

                    MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                        setMethod.Name,
                        MethodAttributes.Public | MethodAttributes.Virtual,
                        typeof(void),
                        new Type[] { pi.PropertyType });

                    ilGenerator = methodBuilder.GetILGenerator();

                    ilGenerator.Emit(OpCodes.Ldarg_0);
                    ilGenerator.Emit(OpCodes.Ldarg_1);
                    ilGenerator.Emit(OpCodes.Stfld, field);
                    ilGenerator.Emit(OpCodes.Ret);

                    typeBuilder.DefineMethodOverride(methodBuilder, setMethod);
                }
            }


            foreach (MethodInfo methodInfo in methods)
            {
                Type returnType = methodInfo.ReturnType;

                List<Type> argumentTypes = new List<Type>();
                foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
                    argumentTypes.Add(parameterInfo.ParameterType);

                MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                    methodInfo.Name,
                    MethodAttributes.Public | MethodAttributes.Virtual,
                    returnType,
                    argumentTypes.ToArray());

                ilGenerator = methodBuilder.GetILGenerator();

                if (returnType != typeof(void))
                {
                    LocalBuilder localBuilder =
                        ilGenerator.DeclareLocal(returnType);
                    ilGenerator.Emit(
                        OpCodes.Ldloc, localBuilder);
                }

                ilGenerator.Emit(OpCodes.Ret);

                typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
            }

            Type createdType = typeBuilder.CreateType();
            m_InterfaceImplementations[type] = createdType;
        }
        private static void AddMethodsToList(List<MethodInfo> methods, Type type)
        {
            methods.AddRange(type.GetMethods());

            foreach (Type subInterface in type.GetInterfaces())
            {
                AddMethodsToList(methods, subInterface);
            }
        }
        private static void AddPropertiesToList(List<PropertyInfo> properties, Type type)
        {
            properties.AddRange(type.GetProperties());

            foreach (Type subInterface in type.GetInterfaces())
                AddPropertiesToList(properties, subInterface);
        }
    }
}
