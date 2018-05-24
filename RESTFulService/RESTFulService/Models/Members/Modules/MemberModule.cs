using Autofac;
using RESTFulService.Entity;
using RESTFulService.Models.Members.Implementations;
using RESTFulService.Models.Members.Interfaces;

namespace RESTFulService.Models.Members.Modules
{
    /// <summary>
    /// MemberModule for mapping MemberOperations and MemberRepository
    /// </summary>
    public class MemberModule : Module
    {
        #region Protected methods
        protected override void Load(ContainerBuilder builder)
        {
            //main 
            builder.RegisterType<MemberOperations>().As<IMemberOperations>();
            builder.RegisterType<MemberRepository>().As<IMemberRepository>();

            base.Load(builder);
        } 
        #endregion
    }
}
