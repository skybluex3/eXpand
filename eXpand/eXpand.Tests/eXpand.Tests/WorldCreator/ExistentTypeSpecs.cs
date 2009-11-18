﻿using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using eXpand.ExpressApp.WorldCreator.ClassTypeBuilder;
using eXpand.Persistent.BaseImpl.PersistentMetaData;
using eXpand.Persistent.BaseImpl.PersistentMetaData.PersistentAttributeInfos;
using eXpand.Xpo;
using Machine.Specifications;
using TypeMock.ArrangeActAssert;

namespace eXpand.Tests.WorldCreator
{
    public abstract class With_Types : With_In_Memory_DataStore
    {
        protected static TypesInfo TypesInfo;

        Establish context = () =>
        {
            TypesInfo = Isolate.Fake.Instance<TypesInfo>();
            Isolate.WhenCalled(() => TypesInfo.ExtendedCoreMemberInfoType).WillReturn(typeof(ExtendedCoreTypeMemberInfo));
            Isolate.WhenCalled(() => TypesInfo.ExtendedCoreMemberInfoType).WillReturn(typeof(ExtendedCoreTypeMemberInfo));
            Isolate.WhenCalled(() => TypesInfo.ExtendedReferenceMemberInfoType).WillReturn(typeof(ExtendedReferenceMemberInfo));
            Isolate.WhenCalled(() => TypesInfo.ExtendedCollectionMemberInfoType).WillReturn(typeof(ExtendedCollectionMemberInfo));
        };
    }
    [Subject(typeof(ExistentTypesMemberCreator))]
    public class When_Creating_ExistentTypes_CoreMembers_that_exist_already : With_Types{
        Establish context = () =>
        {
            var info = new ExtendedCoreTypeMemberInfo(Session.DefaultSession) { DataType = XPODataType.String, Name = "UserName", Owner = typeof(User) };
            info.TypeAttributes.Add(new PersistentSizeAttribute(Session.DefaultSession));
            info.Save();
        };
        Because of = () => new ExistentTypesMemberCreator().CreateMembers(Session.DefaultSession, TypesInfo);

        
        It should_not_throw_any_exceptions = () => XafTypesInfo.XpoTypeInfoSource.XPDictionary.GetClassInfo(typeof(User)).FindMember("UserName");

    }
    [Subject(typeof(ExistentTypesMemberCreator))]
    public class When_Creating_ExistentTypes_CoreMembers : With_Types
    {
        static XPMemberInfo memberInfo;

        

        Establish context = () => {
            var info = new ExtendedCoreTypeMemberInfo(Session.DefaultSession) {DataType = XPODataType.Boolean, Name = "Test", Owner = typeof (User)};
            info.TypeAttributes.Add(new PersistentSizeAttribute(Session.DefaultSession));
            info.Save();
        };

        Because of = () => new ExistentTypesMemberCreator().CreateMembers(Session.DefaultSession, TypesInfo);

        It should_find_that_member_through_xpdictionary =
            () => {
                memberInfo = XafTypesInfo.XpoTypeInfoSource.XPDictionary.GetClassInfo(typeof(User)).FindMember("Test");
                memberInfo.ShouldNotBeNull();
            };

        It should_create_typedattributes =
            () => memberInfo.FindAttributeInfo(typeof (SizeAttribute)).ShouldNotBeNull();

    }
    [Subject(typeof(ExistentTypesMemberCreator))]
    public class When_Creating_ExistentTypes_ReferenceMembers : With_Types
    {

        static XPMemberInfo memberInfo;

        Establish context = () => {
            var info = new ExtendedReferenceMemberInfo(Session.DefaultSession) { Name = "Test", Owner = typeof (User),ReferenceType = typeof(Role)};
            info.TypeAttributes.Add(new PersistentSizeAttribute(Session.DefaultSession));
            info.Save();
        };

        Because of = () => new ExistentTypesMemberCreator().CreateMembers(Session.DefaultSession, TypesInfo);

        It should_find_that_member_through_xpdictionary =
            () => {
                memberInfo = XafTypesInfo.XpoTypeInfoSource.XPDictionary.GetClassInfo(typeof(User)).FindMember("Test");
                memberInfo.ShouldNotBeNull();
            };

        It should_create_typedattributes =
            () => memberInfo.FindAttributeInfo(typeof (SizeAttribute)).ShouldNotBeNull();
    }
    [Subject(typeof(ExistentTypesMemberCreator))]
    public class When_Creating_ExistentTypes_CollectionMembers : With_Types
    {
        

        static XPMemberInfo memberInfo;

        Establish context = () => {
            var info = new ExtendedCollectionMemberInfo(Session.DefaultSession) { Name = "Test", Owner = typeof (User)};
            info.TypeAttributes.Add(new PersistentSizeAttribute(Session.DefaultSession));
            info.Save();
        };

        Because of = () => new ExistentTypesMemberCreator().CreateMembers(Session.DefaultSession, TypesInfo);

        It should_find_that_member_through_xpdictionary =
            () => {
                memberInfo = XafTypesInfo.XpoTypeInfoSource.XPDictionary.GetClassInfo(typeof(User)).FindMember("Test");
                memberInfo.ShouldNotBeNull();
            };

        It should_create_typedattributes =
            () => memberInfo.FindAttributeInfo(typeof (SizeAttribute)).ShouldNotBeNull();


    }

}
