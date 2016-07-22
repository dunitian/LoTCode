using Autofac;
using PawChina.BLL;
using PawChina.IBLL;

namespace PawChina.IOC
{
    /// <summary>
    /// Autofac IOC类
    /// </summary>
    public class Container
    {
        /// <summary>
        /// IOC 容器
        /// </summary>
        public static IContainer container = null;
        /// <summary>
        /// 获取 IBLL 的实例化对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            try
            {
                if (container == null)
                {
                    Initialise();
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("IOC实例化出错!" + ex.Message);
            }

            return container.Resolve<T>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialise()
        {
            var builder = new ContainerBuilder();
            //InstancePerLifetimeScope：在一个生命周期域中，每一个依赖或调用创建一个单一的共享的实例，且每一个不同的生命周期域，实例是唯一的，不共享的。
            //builder.RegisterType<NoteBLL>().As<INoteBLL>().InstancePerLifetimeScope();

            container = builder.Build();
        }
    }
}
